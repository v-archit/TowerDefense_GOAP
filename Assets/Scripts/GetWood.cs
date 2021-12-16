using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetWood : ActivityClass
{
    private bool takenFood = false;

    public GameObject treeObject;
    public Manager manager;

    public float takeDuration = 4;

    public string type;

    private void Start()
    {
        AddRequiredStates("hasCutWood", false);
        AddChangedStates("hasCutWood", true);


        activityObject = treeObject;



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
        StartCoroutine(TakeWood());
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

    public IEnumerator TakeWood()
    {
        //if it doesnt work start and stop coroutine with strings

        yield return new WaitForSeconds(takeDuration);


        takenFood = true;
        if (type == "AI")
        {
            manager.cutWood += 4;
        }
        else
        {
            
        }
        //manager.foodForDelivery += 3;
        Debug.Log("Wood is taken");

        yield break;
    }

    

}
