using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetFood : ActivityClass
{
    private bool takenFood = false;

    public GameObject shrubObject;
    public Manager manager;

    public float takeDuration = 3;

    public string type;
    

    private void Start()
    {
        AddRequiredStates("hasForagedFood", false);
        AddChangedStates("hasForagedFood", true);


        activityObject = shrubObject;



    }

    public override void ResetState()
    {
        takenFood = false;
    }

    public override bool CheckDone()
    {
        return takenFood;
    }

    public override void DoActivity()
    {
        StartCoroutine(TakeFood());
    }

    public override bool InRange(GameObject agent)
    {
        if (agent.transform.position == activityObject.transform.position)
        {
            return true;
        }
        else
            return false;
    }

    public IEnumerator TakeFood()
    {
        //if it doesnt work start and stop coroutine with strings

        yield return new WaitForSeconds(takeDuration);


        takenFood = true;

        if (type == "AI")
        {
            manager.foragedFood += 3;
        }
        else
        {

        }

        //manager.foodForDelivery += 3;
        Debug.Log("Food is taken");

        yield break;
    }

    

}
