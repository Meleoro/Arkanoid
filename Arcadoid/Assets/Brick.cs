using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Brick : MonoBehaviour
{
    public ParticleSystem particleDegats;
    private ParticleSystem.MainModule particleDegatsParameters;
    public ParticleSystem particleDestroy;
    private ParticleSystem.MainModule particleDestroyParameters;
    public BrickManager refBrickManager;

    public int health;

    [Header("Game Feel")]
    [SerializeField] private float dureeLight;
    [SerializeField] private float intensiteLight;
    [SerializeField] private float cameraShakeDuration;
    [SerializeField] private float cameraShakeStrength;
    private float timerEffects;
    private Light blockLight;

    private bool isInvincible;


    private void Start()
    {
        blockLight = GetComponent<Light>();

        particleDegatsParameters = particleDegats.main;
        particleDestroyParameters = particleDestroy.main;
    }



    private void Update()
    {
        if(timerEffects > 0)
        {
            timerEffects -= Time.deltaTime;

            float avanceeEffects = timerEffects / dureeLight;

            blockLight.intensity = Mathf.Lerp(intensiteLight, 0, Mathf.Abs((avanceeEffects - 0.5f) * 2));
        }
    }



    public void MoveBrick(Vector2 velocity)
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
    }

    public void LoseHealth(int damages, bool disableBoxCollider)
    {
        if (!isInvincible)
        {
            StartCoroutine(SetInvincible());

            health -= 1;

            if (health > 0)
            {
                LoseHealthEffects();

                GetComponent<MeshRenderer>().material = refBrickManager.materials[health - 1];

                if (disableBoxCollider)
                    GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                CameraManager.Instance.DoCameraShake(cameraShakeDuration, cameraShakeDuration);
                LevelManager.Instance.AddXP(1);

                refBrickManager.brickList.Remove(this);

                Instantiate(particleDestroy, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }

    public void LoseHealthEffects()
    {
        blockLight.color = refBrickManager.materials[health - 1].color;
        timerEffects = dureeLight;

        particleDegatsParameters.startColor = refBrickManager.materials[health - 1].color;
        Instantiate(particleDegats, transform.position, Quaternion.identity);

        CameraManager.Instance.DoCameraShake(cameraShakeDuration, cameraShakeDuration);

        StartCoroutine(SetInvincible());
    }


    private IEnumerator SetInvincible()
    {
        isInvincible = true;

        yield return new WaitForSeconds(0.03f);

        isInvincible = false;
    }
}
