using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("SpawnManager properties")]
    public Transform minSpawnTransfromLeft = null;
    public Transform maxSpawnTransfromLeft = null;
    public Transform minSpawnTransfromRight = null;
    public Transform maxSpawnTransfromRight = null;
    public float spawnRoutine = 0.5f;
    public SpeciesSet[] allSpecies     = null;

    public void Init()
    {
    }

    public void GameSpawnLoop()
    {
        StartCoroutine("CheckTick");
    }

    public void PopulateWorld()
    {
        foreach (var species in allSpecies)
        {
            SpawnSpecies(species, true);
        }
    }


    private IEnumerator CheckTick()
    {
        while (GameManager.Get.gameHasStarted)
        {
            foreach (var species in allSpecies)
            {
                //int amountAlive = CheckSpeciesAmount(species);

                SpawnSpecies(species);
            }

            yield return new WaitForSeconds(spawnRoutine);
        }

        StopCoroutine("CheckTick");
    }

    private void SpawnSpecies(SpeciesSet species, bool inScreen = false)
    {
        int amountToSpawn = (int)Random.Range(species.spawnBurst.x, species.spawnBurst.y);

        for (int i = 0; i < amountToSpawn; i++)
        {
            if (species.amountAlive >= species.speciesMinMax.y)
                break;

            bool shouldBeChild = Random.value < species.childChange;

            Vector3 spawnPosition = Vector3.zero;

            if (!inScreen)
            { 
                if (Random.value < 0.5f)
                {
                    spawnPosition = new Vector3(Random.Range(minSpawnTransfromRight.position.x, maxSpawnTransfromRight.position.x), minSpawnTransfromRight.position.y);
                }
                else
                {
                    spawnPosition = new Vector3(Random.Range(minSpawnTransfromLeft.position.x, maxSpawnTransfromLeft.position.x), minSpawnTransfromLeft.position.y);
                }
            }
            else
            {

                spawnPosition = new Vector3(Random.Range(maxSpawnTransfromLeft.position.x, minSpawnTransfromRight.position.x), minSpawnTransfromLeft.position.y);
            }

            GameObject dinoObject = Instantiate(species.speciesPrefab, spawnPosition, Quaternion.identity);
            DinoBase dinoBase = dinoObject.GetComponent<DinoBase>();
            dinoBase.Init(shouldBeChild);    
            species.amountAlive += 1;
        }


    }

    public void SpeciesDied(string speciesName)
    {
        foreach (SpeciesSet species in allSpecies)
        {
            if (species.speciesName == speciesName)
            {
                species.amountAlive--;
            }
        }
    }

    private int CheckSpeciesAmount(SpeciesSet species)
    {
        int speciesAmount = 0;

        GameObject[] allDinosaurs = GameObject.FindGameObjectsWithTag("Dino");

        foreach (GameObject dino in allDinosaurs)
        {
            if (dino.GetComponent<DinoBase>().speciesName == species.speciesPrefab.GetComponent<DinoBase>().speciesName)
            {
                speciesAmount++;
            }
        }

        return speciesAmount;
    }
}