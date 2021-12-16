using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnSiegePerson : ActivityClass
{
    private bool spawnedSiege = false;

    public Transform[] spawnPoints;
    public Manager manager;

    public float spawnDuration = 5;

    public GameObject siegePrefab;

    private void Start()
    {
        AddRequiredStates("hasFoodSiege", true);
        AddRequiredStates("hasWoodSiege", true);
        AddRequiredStates("isCutterPresent", true);
        AddRequiredStates("isForagerPresent", true);
        AddRequiredStates("isSpaceAvailable", true);


        AddChangedStates("defendTower", true);

        //activityObject = treeObject;

    }

    public override void ResetState()
    {
        spawnedSiege = false;
    }

    public override bool CheckDone()
    {
        return spawnedSiege;
    }

    public override void DoActivity()
    {
        StartCoroutine(SpawnSiege());
    }


    public IEnumerator SpawnSiege()
    {
        //if it doesnt work start and stop coroutine with strings


        yield return new WaitForSeconds(spawnDuration);

        int p = manager.PriorityLane();
        GameObject g = Instantiate(siegePrefab);
        g.transform.position = spawnPoints[p - 1].position;
        manager.AddWeightLane(p);


        spawnedSiege = true;

        manager.inventoryFood -= 1;
        manager.inventoryWood -= 4;

        manager.nSiege += 1;
        manager.population += 1;



        Debug.Log("Siege is spawned");

        yield break;
    }



}
