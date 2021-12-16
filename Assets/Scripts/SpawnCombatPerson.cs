using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnCombatPerson : ActivityClass
{
    private bool spawnedCombat = false;

    public Transform[] spawnPoints;
    public Manager manager;

    public float spawnDuration = 3;

    public GameObject combatPrefab;

    private void Start()
    {
        AddRequiredStates("hasFoodCombat", true);
        AddRequiredStates("hasWoodCombat", true);
        AddRequiredStates("isCutterPresent", true);
        AddRequiredStates("isForagerPresent", true);
        AddRequiredStates("isSpaceAvailable", true);


        AddChangedStates("defendTower", true);

        //activityObject = treeObject;

    }

    public override void ResetState()
    {
        spawnedCombat = false;
    }

    public override bool CheckDone()
    {
        return spawnedCombat;
    }

    public override void DoActivity()
    {
        StartCoroutine(SpawnCombat());
    }


    public IEnumerator SpawnCombat()
    {
        //if it doesnt work start and stop coroutine with strings


        yield return new WaitForSeconds(spawnDuration);

        int p = manager.PriorityLane();
        GameObject g = Instantiate(combatPrefab);
        g.transform.position = spawnPoints[p-1].position;
        manager.AddWeightLane(p);


        spawnedCombat = true;

        manager.inventoryFood -= 5;
        manager.inventoryWood -= 2;

        manager.nCombat += 1;
        manager.population += 1;



        Debug.Log("Combat is spawned");

        yield break;
    }



}
