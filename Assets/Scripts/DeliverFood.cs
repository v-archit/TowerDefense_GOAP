using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliverFood : ActivityClass
{
    private bool deliveredFood = false;

    public GameObject inventoryObject;
    public Manager manager;

    
    public float deliveryDuration = 1;

    public string type;

    private void Start()
    {
        AddRequiredStates("hasForagedFood", true);
        AddChangedStates("hasFood", true);
        //AddChangedStates("hasFoodDelivered", true);


        activityObject = inventoryObject;



    }

    public override void ResetState()
    {
        deliveredFood = false;
    }

    public override bool CheckDone()
    {
        return deliveredFood;
	}

	public override void DoActivity()
	{
		StartCoroutine(GetFoodDelivered());
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

    public IEnumerator GetFoodDelivered()
    {
        //if it doesnt work start and stop coroutine with strings

        yield return new WaitForSeconds(deliveryDuration);


        deliveredFood = true;

        if (type == "AI")
        {
            manager.inventoryFood += 3;
        }
        else
        {
            manager.playerFood += 3;
        }

        Debug.Log("Food is delivered");

        yield break;
    }

    

}
