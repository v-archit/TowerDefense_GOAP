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

    //public CookFood()
    //{
    //    AddRequiredStates("hasIngredients", true);
    //    AddRequiredStates("hasFood", false);
    //    AddChangedStates("hasIngredients", false);
    //    AddChangedStates("hasFood", true);
    //    Debug.Log("Activity Object set");


    //    activityObject = kitchenObject;



    //}

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
        manager.foragedFood += 3;
        //manager.foodForDelivery += 3;
        Debug.Log("Food is taken");

        yield break;
    }

    

}
