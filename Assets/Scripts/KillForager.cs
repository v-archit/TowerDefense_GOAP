using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillForager : ActivityClass
{
    private bool killedForager = false;

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
        killedForager = false;
    }

    public override bool CheckDone()
    {
        return killedForager;
    }

    public override void DoActivity()
    {
        StartCoroutine(KillForagerPerson());
    }


    public IEnumerator KillForagerPerson()
    {
        //if it doesnt work start and stop coroutine with strings


        yield return new WaitForSeconds(killDuration);

        manager.DeleteForager();

        killedForager = true;

        manager.nForager -= 1;
        manager.population -= 1;



        Debug.Log("Forager is killed");

        yield break;
    }



}
