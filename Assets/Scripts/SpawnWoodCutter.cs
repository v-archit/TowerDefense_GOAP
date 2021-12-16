using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnWoodCutter : ActivityClass
{
    private bool spawnedCutter = false;

    public Transform spawnPoint;
    public Manager manager;

    public float spawnDuration = 2;

    public GameObject cutterPrefab;

    private void Start()
    {
        AddRequiredStates("hasFoodWorker", true);
        AddRequiredStates("hasWoodCutter", true);
        AddRequiredStates("isSpaceAvailable", true);

        AddChangedStates("isCutterPresent", true);

        //activityObject = treeObject;

    }

    public override void ResetState()
    {
        spawnedCutter = false;
    }

    public override bool CheckDone()
    {
        return spawnedCutter;
    }

    public override void DoActivity()
    {
        StartCoroutine(SpawnCutter());
    }


    public IEnumerator SpawnCutter()
    {
        //if it doesnt work start and stop coroutine with strings


        yield return new WaitForSeconds(spawnDuration);

        GameObject g = Instantiate(cutterPrefab);
        g.transform.position = spawnPoint.position;

        manager.AddWoodCutter(g);

        spawnedCutter = true;

        manager.inventoryFood -= 1;
        manager.inventoryWood -= 1;

        manager.nWoodCutter += 1;
        manager.population += 1;



        Debug.Log("Wood Cutter is spawned");

        yield break;
    }



}
