using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Rythme Mont�e de Niveau")]
    [SerializeField] private int xpNeededStart;
    [SerializeField] private int xpNeededAddition;
    private int currentXPNeeded;
    private int currentXP;
    private int currentLevel;

    [Header("Upgrades")]
    [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();
    public int currentStrength;
    public int currentSpeed;
    public int currentWidth;
    public int currentTurret;

    [Header("Autres")]
    private bool arretDuTemps;
    private Upgrade upgrade1;
    private Upgrade upgrade2;
    private GameObject upgrade1Object;
    private GameObject upgrade2Object;
    [HideInInspector] public bool isLevelingUp;
    private int levelMax = 12;

    [Header("References")] 
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private Image xpBarImage;
    [SerializeField] private Image fond;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private ParticleSystem VFXLevelUp;
    private StatsManager statsManagerScript;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }


    private void Start()
    {
        currentLevel = 1;
        currentXP = 0;
        currentXPNeeded = xpNeededStart;

        xpBarImage.fillAmount = currentXP / currentXPNeeded;

        button1.enabled = false;
        button2.enabled = false;

        statsManagerScript = GetComponent<StatsManager>();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            AddXP(5);
    }



    private void FixedUpdate()
    {
        if(arretDuTemps)
        {
            float currentTime = Time.timeScale - Time.unscaledDeltaTime;

            if (currentTime > 0)
            {
                Time.timeScale -= Time.unscaledDeltaTime;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
        else
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale += Time.unscaledDeltaTime;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
    }





    public void AddXP(int xpAmount)
    {
        if (!isLevelingUp && currentLevel < levelMax)
        {
            currentXP += xpAmount;

            if (currentXP >= currentXPNeeded)
            {
                isLevelingUp = true;
                StartCoroutine(LevelUp());
            }
            else
            {
                xpBarImage.fillAmount = (float)currentXP / (float)currentXPNeeded;
            }
        }
    }


    public IEnumerator LevelUp()
    {
        PauseManager.Instance.canPause = false;
        
        currentXP = 0;
        currentXPNeeded += xpNeededAddition;

        arretDuTemps = true;

        CameraManager.Instance.transform.DOMove(CameraManager.Instance.transform.position + new Vector3(3, 2, 4), 1f).SetUpdate(true).SetEase(Ease.InFlash); 

        yield return new WaitForSecondsRealtime(1f);

        currentLevel += 1;
        if (currentLevel < levelMax)
            lvlText.text = "Lvl. " + currentLevel;

        else
            lvlText.text = "Lvl. MAX";

        xpBarImage.fillAmount = (float)currentXP / (float)currentXPNeeded;

        VFXLevelUp.Play();

        yield return new WaitForSecondsRealtime(0.5f);

        CameraManager.Instance.transform.DOMove(CameraManager.Instance.transform.position + new Vector3(-3, -2, -8), 0.5f).SetUpdate(true);

        fond.DOFade(0.7f, 0.5f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(0.5f);

        ChoseUpgrades();

        upgrade1Object = Instantiate(upgrade1.upgradeCard, pos1.transform.position, Quaternion.identity, pos1);
        upgrade2Object = Instantiate(upgrade2.upgradeCard, pos2.transform.position, Quaternion.identity, pos2);

        yield return new WaitForSecondsRealtime(0.5f);

        button1.enabled = true;
        button2.enabled = true;
    }


    public void ChoseUpgrades()
    {

            upgrade1 = null;
            upgrade2 = null;

            bool oneUpdate = false;
            bool moreUpdate = false;

            for (int i = 0; i < upgrades.Count; i++)
            {
                if(upgrades[i].appearanceNumber > 0)
                {
                    if (!oneUpdate)
                        oneUpdate = true;

                    else
                        moreUpdate = true;
                }
            }

            while(upgrade1 == null || upgrade2 == null)
            {
                if (upgrade1 != null && !moreUpdate)
                    upgrade2 = upgrade1;

                int index = Random.Range(0, upgrades.Count);

                if(upgrades[index].appearanceNumber >= 1 && upgrades[index] != upgrade1)
                {
                    if (upgrade1 == null)
                        upgrade1 = upgrades[index];

                    else
                        upgrade2 = upgrades[index];
                }
            }
        
    }


    public void ClickButton(int buttonClicked)
    {
        Upgrade.Effect effectChose;

        if(buttonClicked == 1)
        {
            effectChose = upgrade1.upgradeType;

            upgrade1.appearanceNumber -= 1;
        }
        else
        {
            effectChose = upgrade2.upgradeType;

            upgrade2.appearanceNumber -= 1;
        }

        switch (effectChose)
        {
            case Upgrade.Effect.strength:
                currentStrength += 1;
                break;

            case Upgrade.Effect.speed:
                currentSpeed += 1;
                break;

            case Upgrade.Effect.width:
                currentWidth += 1;
                break;

            case Upgrade.Effect.ball:
                BallManager.Instance.currentMax += 1;
                BallManager.Instance.currentBallStock += 1;
                BallManager.Instance.ActualiseBalls();
                break;
            
            case Upgrade.Effect.turret:
                currentTurret += 1;
                break;
        }

        button1.enabled = false;
        button2.enabled = false;

        StartCoroutine(CardChoseEffects(buttonClicked));
    }


    public IEnumerator CardChoseEffects(int numberCard)
    {
        if (numberCard == 1)
            pos1.DOScale(pos1.transform.localScale + new Vector3(0.2f, 0.2f, 0.2f), 0.2f).SetUpdate(true);

        else
            pos2.DOScale(pos2.transform.localScale + new Vector3(0.2f, 0.2f, -0.2f), 0.2f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(0.2f);

        if(numberCard == 1)
            pos1.DOScale(pos1.transform.localScale - new Vector3(0.2f, 0.2f, 0.2f), 0.2f).SetUpdate(true);

        else
            pos2.DOScale(pos2.transform.localScale - new Vector3(0.2f, 0.2f, 0.2f), 0.2f).SetUpdate(true);

        upgrade1Object.GetComponent<Animator>().SetTrigger("Disappear");
        upgrade2Object.GetComponent<Animator>().SetTrigger("Disappear");

        Destroy(upgrade1Object, 0.5f);
        Destroy(upgrade2Object, 0.5f);

        CameraManager.Instance.transform.DOMove(CameraManager.Instance.transform.position + new Vector3(0, 0, 4), 0.5f).SetUpdate(true);

        arretDuTemps = false;
        fond.DOFade(0f, 0.5f);
        
        statsManagerScript.ApplyStats();

        PauseManager.Instance.canPause = true;
        isLevelingUp = false;
    }


    public void OnCard(int cardNumber)
    {
        if(cardNumber == 1)
        {
            pos1.DOScale(Vector3.one * 1.5f, 0.2f).SetUpdate(true);
        }
        else
        {
            pos2.DOScale(new Vector3(1, 1, -1) * 1.5f, 0.2f).SetUpdate(true);
        }
    }

    public void OutCard(int cardNumber)
    {
        if (cardNumber == 1)
        {
            pos1.DOScale(Vector3.one * 1.2f, 0.2f).SetUpdate(true);
        }
        else
        {
            pos2.DOScale(new Vector3(1, 1, -1) * 1.2f, 0.2f).SetUpdate(true);
        }
    }
}



[Serializable]
public class Upgrade
{
    public GameObject upgradeCard;
    public int appearanceNumber;

    public enum Effect
    {
        strength,
        speed,
        width,
        ball,
        turret
    }

    public Effect upgradeType;
}
