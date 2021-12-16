using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillWoodCutter : ActivityClass
{
    private bool killedCutter = false;

    public Manager manager;

    public float killDuration = 0;


    private void Start()
    {
        AddRequiredStates("isSpaceAvailable", false);

        AddChangedStates("isSpaceAvailable", true);

        //activityObject = treeObject;

    }

    public override void ResetState()
    {
        killedCutter = false;
    }

    public override bool CheckDone()
    {
        return killedCutter;
    }

    public override void DoActivity()
    {
        StartCoroutine(KillCutter());
    }


    public IEnumerator KillCutter()
    {
        //if it doesnt work start and stop coroutine with strings


        yield return new WaitForSeconds(killDuration);

        manager.DeleteWoodCutter();

        killedCutter = true;

        manager.nWoodCutter -= 1;
        manager.population -= 1;



        Debug.Log("Wood Cutter is killed");

        yield break;
    }



}
