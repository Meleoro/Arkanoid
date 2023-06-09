using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickManager : MonoBehaviour
{
    [HideInInspector] public List<Brick> brickList = new List<Brick>();

    public List<GameObject> bricksGameObjects = new List<GameObject>();

    public float offsetx;
    public float offsety;

    public float startPaireX;
    public float startImpaireX;

    [Header("Paramètres")]
    [Range(0, 100)] public float probaSpawnBrick;
    [Range(0f, 2f)] public float fallSpeed;
    public List<Material> materials;

    [Header("Autres")] 
    private float compteurDescente;
    private bool lignePaire;
    private bool isCreatingLigne;

    [Header("Augmentation Difficulté")]
    [SerializeField] [Range(0, 100)] private float maxProbaSpawnBrick;
    [SerializeField] [Range(0f, 2f)] private float maxFallSpeed;
    private int currentBricks;
    private int iterations;


    private void Start()
    {
        isCreatingLigne = true;

        currentBricks = 2;
        
        StartCoroutine(Initialise());
        StartCoroutine(UpDifficulty());
    }


    private void Update()
    {
        if (!isCreatingLigne)
        {
            for (int i = 0; i < brickList.Count; i++)
            {
                brickList[i].MoveBrick(Vector2.down * fallSpeed);
            }
            
            compteurDescente += fallSpeed * Time.deltaTime;

            if (compteurDescente >= offsety)
            {
                NewLigne(9.5f);

                compteurDescente = 0;
            }
        }
    }


    private IEnumerator Initialise()
    {
        isCreatingLigne = true;
        NewLigne(9.5f);

        yield return new WaitForSeconds(0.7f);
        
        NewLigne(8f);
        
        yield return new WaitForSeconds(0.8f);
        
        NewLigne(6.5f);

        yield return new WaitForSeconds(0.8f);
        isCreatingLigne = false;
    }
    

    private void NewLigne(float startPosY)
    {
        Vector2 startPos;
        int nbrBrick;

        if (lignePaire)
        {
            startPos = new Vector2(startPaireX, startPosY);
            nbrBrick = 7;
        }
        else
        {
            startPos = new Vector2(startImpaireX, startPosY);
            nbrBrick = 6;
        }

        lignePaire = !lignePaire;

        
        List<Vector2> posToSpawn = new List<Vector2>();

        for (int i = 0; i < nbrBrick; i++)
        {
            posToSpawn.Add(new Vector2(startPos.x + offsetx * i, startPos.y));
        }

        StartCoroutine(InstantiateLigne(posToSpawn));
    }


    private IEnumerator InstantiateLigne(List<Vector2> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; i++)
        {
            int brickSpawn = Random.Range(0, 100);

            if (brickSpawn < probaSpawnBrick)
            {
                int min = Mathf.Clamp(currentBricks - 3, 0, 100);
                int currentBrick = Random.Range(min, currentBricks);

                GameObject newBrick = Instantiate(bricksGameObjects[currentBrick], spawnPos[i], Quaternion.identity, transform);

                Brick scriptNewBrick = newBrick.GetComponent<Brick>();
                brickList.Add(scriptNewBrick);
                scriptNewBrick.refBrickManager = this;
                
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator UpDifficulty()
    {

        yield return new WaitForSeconds(15);

        if(probaSpawnBrick < maxProbaSpawnBrick)
        {
            probaSpawnBrick += 3f;
        }

        if(fallSpeed < maxFallSpeed)
        {
            if(currentBricks < bricksGameObjects.Count)
                fallSpeed += 0.04f;
            
            else
                fallSpeed += 0.07f;
        }

        if(currentBricks < bricksGameObjects.Count)
        {
            iterations += 1;
            if(iterations == 2)
            {
                iterations = 0;
                currentBricks += 1;
            }
        }

        StartCoroutine(UpDifficulty());
    }
}
