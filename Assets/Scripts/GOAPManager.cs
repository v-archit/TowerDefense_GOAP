using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPManager : MonoBehaviour
{
	private List<ActivityClass> activitiesList;
	private List<ActivityClass> currentActivity;

	public Manager manager;
	public Planner planner;

	private int currentState = 1;
	private bool startedActivity = false;

	private void Start()
	{
		activitiesList = new List<ActivityClass>();
		currentActivity = new List<ActivityClass>();

		FindActivities();
	}

	private void Update()
	{
		//Debug.Log("Current State " + currentState);
		if (currentState == 1)
		{
			Dictionary<string, bool> globalState = manager.GlobalState();
			Dictionary<string, bool> goalState = GoalState();

			//DisplayState(globalState);
			//DisplayState(goalState);

			List<ActivityClass> solution = planner.FindSolution(gameObject, activitiesList, globalState, goalState);

			if (solution != null)
			{
				//Debug.Log("Plan found Car Driver");
				currentActivity = solution;

				currentState = 2;              //move state

			}
			else
			{
				//Debug.Log("Plan not found Car Driver");
			}
		}
		else if (currentState == 2)
		{
			if (ActivityPlanPresent())
			{
				ActivityClass activity = currentActivity[0];

				if (!startedActivity)
				{
					activity.DoActivity();
					startedActivity = true;
				}
				else
				{
					if (activity.CheckDone())
					{
						currentActivity.RemoveAt(0);
						startedActivity = false;
					}
				}
			}
			else
			{
				Debug.Log("Activities Completed");

				currentState = 1;
			}
		}

	}

	public void SetState(int state)
	{
		currentState = state;
	}

	public int GetState()
	{
		return currentState;
	}

	public Dictionary<string, bool> GoalState()
	{
		Dictionary<string, bool> goalState = new Dictionary<string, bool>();
		goalState.Add("defendTower", true);
		return goalState;
	}

	private bool ActivityPlanPresent()
	{
		return currentActivity.Count > 0;
	}

	private void FindActivities()
	{
		ActivityClass[] activities = gameObject.GetComponents<ActivityClass>();
		foreach (ActivityClass activity in activities)
		{
			//Debug.Log("Activity name " + activity);
			activitiesList.Add(activity);
		}
		//Debug.Log("Found Activities");
	}

	private void DisplayState(Dictionary<string, bool> state)
	{
		foreach (string key in state.Keys)
		{
			Debug.Log(key + "-" + state[key]);
		}
	}
}
