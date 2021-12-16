using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnForager : ActivityClass
{
    private bool spawnedForager = false;

    public Transform spawnPoint;
    public Manager manager;

    public float spawnDuration = 2;

    public GameObject foragerPrefab;

    private void Start()
    {
        AddRequiredStates("hasFoodWorker", true);
        AddRequiredStates("isSpaceAvailable", true);

        AddChangedStates("isForagerPresent", true);

        //activityObject = treeObject;

    }

    public override void ResetState()
    {
        spawnedForager = false;
    }

    public override bool CheckDone()
    {
        return spawnedForager;
    }

    public override void DoActivity()
    {
        StartCoroutine(SpawnForagerPerson());
    }


    public IEnumerator SpawnForagerPerson()
    {
        //if it doesnt work start and stop coroutine with strings


        yield return new WaitForSeconds(spawnDuration);

        GameObject g = Instantiate(foragerPrefab);
        g.transform.position = spawnPoint.position;

        manager.AddForager(g);

        spawnedForager = true;

        manager.inventoryFood -= 2;
        manager.inventoryWood -= 1;

        manager.nForager += 1;
        manager.population += 1;



        Debug.Log("Forager is spawned");

        yield break;
    }



}
