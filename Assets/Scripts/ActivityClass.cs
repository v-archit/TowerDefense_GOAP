using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ActivityClass : MonoBehaviour
{
	public GameObject activityObject;
	private Dictionary<string, bool> requiredStates;
	private Dictionary<string, bool> changedStates;
	public float runCost = 0;
	public Dictionary<string, bool> GetRequiredStates()
	{
		return requiredStates;
	}

	public void AddRequiredStates(string key, bool value)
	{
		requiredStates.Add(key, value);
	}

	public void AddChangedStates(string key, bool value)
	{
		changedStates.Add(key, value);
	}

	public virtual void ResetState() { Debug.Log("Reset parent"); }

	public virtual bool CheckDone() { return false; }

	public virtual bool InRange(GameObject g) { return false; }

	public virtual void DoActivity() { }

	public Dictionary<string, bool> GetChangedStates()
	{
		return changedStates;
	}

	public ActivityClass()
	{
		requiredStates = new Dictionary<string, bool>();
		changedStates = new Dictionary<string, bool>();
	}

}