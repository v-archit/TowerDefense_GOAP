using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner : MonoBehaviour
{
	private class TreeNode
	{
		public float useCost;

		public TreeNode parentNode;

		public Dictionary<string, bool> activeState;

		public ActivityClass activityClass;

		public TreeNode(float uCost, TreeNode pNode, ActivityClass aClass, Dictionary<string, bool> aState)
		{
			activeState = aState;

			useCost = uCost;

			parentNode = pNode;

			activityClass = aClass;
		}
	}

	private bool gotSolution = true;
	private bool checkMatch = false;

	private bool PopulateTree(TreeNode parentNode, List<TreeNode> treeNodes, List<ActivityClass> activitiesList, Dictionary<string, bool> finalState)
	{
		gotSolution = false;

		for (int i = 0; i < activitiesList.Count; i++)
		{
			Dictionary<string, bool> tempActivityState = activitiesList[i].GetRequiredStates();
			Dictionary<string, bool> tempState = parentNode.activeState;

			checkMatch = StateCheck(tempState, tempActivityState);
			
			if (checkMatch)
			{
				Dictionary<string, bool> currentState = StateChange(parentNode.activeState, activitiesList[i].GetChangedStates());

				TreeNode tNode = new TreeNode(parentNode.useCost + activitiesList[i].runCost, parentNode, activitiesList[i], currentState);

				if (StateCheck(currentState, finalState))
				{
					treeNodes.Add(tNode);
					gotSolution = true;
				}
				else
				{
					List<ActivityClass> remainingActivities = new List<ActivityClass>();

					for (int j = 0; j < activitiesList.Count; j++)
					{
						if (activitiesList[j] != activitiesList[i])
						{
							remainingActivities.Add(activitiesList[j]);
						}
					}

					bool tempSolution = PopulateTree(tNode, treeNodes, remainingActivities, finalState);
					if (tempSolution)
						gotSolution = true;
				}
			}
		}
		return gotSolution;
	}


	//private bool StateCheck(Dictionary<string, bool> firstState, Dictionary<string, bool> secondState)
	//{
	//	DisplayState(firstState);
	//	DisplayState(secondState);

	//	bool matchBool = true;

	//	if (firstState.Count == secondState.Count)
	//	{

	//		foreach (string stateString in firstState.Keys)
	//		{
	//			if (firstState[stateString] != secondState[stateString])
	//			{
	//				matchBool = false;
	//				break;
	//			}
	//		}
	//	}
	//	else
	//	{
	//		Debug.Log("Count different");
	//		matchBool = false;
	//	}

	//	return matchBool;
	//}

	private bool StateCheck(Dictionary<string, bool> firstState, Dictionary<string, bool> secondState)
	{
		DisplayState(firstState);
		DisplayState(secondState);

		bool matchBool = true;

		foreach (string stateString in secondState.Keys)
		{
			if (firstState[stateString] != secondState[stateString])
			{
				matchBool = false;
				break;
			}
		}

		return matchBool;
	}

	private Dictionary<string, bool> StateChange(Dictionary<string, bool> prevState, Dictionary<string, bool> newState)
	{
		Dictionary<string, bool> tempState = new Dictionary<string, bool>();

		foreach (string keys in prevState.Keys)
		{
			tempState.Add(keys, prevState[keys]);
		}

		foreach (string keys in newState.Keys)
		{
			if (tempState.ContainsKey(keys))
			{
				tempState[keys] = newState[keys];
			}
			else
			{
				tempState.Add(keys, newState[keys]);
			}
		}

		return tempState;
	}

	public List<ActivityClass> FindSolution(GameObject activityObject, List<ActivityClass> activitiesList, Dictionary<string, bool> currentState, Dictionary<string, bool> finalState)
	{
		List<ActivityClass> newActivities = new List<ActivityClass>();

		foreach (ActivityClass aClass in activitiesList)
		{
			aClass.ResetState();
			newActivities.Add(aClass);
		}

		List<TreeNode> treeNodes = new List<TreeNode>();

		TreeNode startNode = new TreeNode(0, null, null, currentState);

		bool foundPlan = PopulateTree(startNode, treeNodes, newActivities, finalState);

		if (foundPlan)
		{
			Debug.Log("Plan Found " + activityObject.name);
		}
		else
		{
			Debug.Log("Plan Not Found " + activityObject.name);
			return null;
		}

		TreeNode cheapestNode = null;
		foreach (TreeNode tNode in treeNodes)
		{
			if (cheapestNode == null)
				cheapestNode = tNode;
			else
			{
				if (tNode.useCost < cheapestNode.useCost)
					cheapestNode = tNode;
			}
		}

		List<ActivityClass> aList = new List<ActivityClass>();

		TreeNode t = cheapestNode;
		while (t != null)
		{
			if (t.activityClass != null)
			{
				aList.Insert(0, t.activityClass);
			}
			t = t.parentNode;
		}

		return aList;
	}

	private void DisplayState(Dictionary<string, bool> state)
	{
		foreach (string key in state.Keys)
		{
			//Debug.Log(key + "-" + state[key]);
		}
	}
}
