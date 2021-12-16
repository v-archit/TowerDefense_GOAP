using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBarrier : MonoBehaviour
{
	private Manager manager;
	private void Start()
	{
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		if (other.gameObject.GetComponent<BattleUnit>().type == "Combat" || other.gameObject.GetComponent<BattleUnit>().type == "Siege")
		{
			manager.UpdateKilled(other.gameObject.tag, other.gameObject.GetComponent<BattleUnit>().type);
			Destroy(other.gameObject);
		}
	}
}
