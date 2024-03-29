﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
	public int health;
	public int attackToSeige;
	public int attackToCombat;
	public int attackToTower;

	private bool inCombat = false;
	private Manager manager;

	public string type;

	private Vector3 direction;

	private void Start()
	{
		//type = "Seige";
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

		if (gameObject.tag == "Player")
		{
			direction = Vector3.right;
		}
		else if (gameObject.tag == "AI")
		{
			direction = -Vector3.right;
		}

		if (type == "Siege")
		{
			health = 100;
			attackToSeige = 10;
			attackToCombat = 5;
			attackToTower = 20;
		}
		else if (type == "Combat")
		{
			health = 100;
			attackToSeige = 20;
			attackToCombat = 10;
			attackToTower = 5;
		}
		else if (type == "Tower")
		{
			health = 200;
		}
	}

	private void Update()
	{
		//if (health < 0)
		//	Destroy(gameObject);

		//Debug.Log(gameObject.name + health);

		if (!inCombat)
		{
			if (this.type == "Siege")
			{
				transform.Translate(direction * Time.deltaTime * 0.2f);
			}
			else if (this.type == "Combat")
			{
				transform.Translate(direction * Time.deltaTime * 0.3f);
			}
		}
	}

	//private void OnCollisionEnter(Collider other)
	//{
	//	if (gameObject.tag == "Player")
	//	{
	//		if (other.tag == "AI")
	//		{
	//			inCombat = true;
	//			StartCoroutine(Attack(other));
	//		}
	//	}
	//	if (gameObject.tag == "AI")
	//	{
	//		if (other.tag == "Player")
	//		{
	//			inCombat = true;
	//			StartCoroutine(Attack(other));
	//		}
	//	}
	//}

	//private void OnCollisionExit(Collider other)
	//{
	//	if (gameObject.tag == "Player")
	//	{
	//		if (other.tag == "AI")
	//		{
	//			inCombat = false;
	//		}
	//	}
	//	if (gameObject.tag == "AI")
	//	{
	//		if (other.tag == "Player")
	//		{
	//			inCombat = false;
	//		}
	//	}
	//}

	private void OnTriggerEnter(Collider other)
	{
		if (gameObject.tag == "Player")
		{
			if (other.tag == "AI")
			{
				Debug.Log("Player to AI" + other.name);
				inCombat = true;

				this.StartCoroutine(AttackAI(other.gameObject));

			}
		}
		if (gameObject.tag == "AI")
		{
			if (other.tag == "Player")
			{
				Debug.Log("AI to Player" + other.name);

				inCombat = true;
				this.StartCoroutine(AttackPlayer(other.gameObject));

			}
		}
	}

	public IEnumerator AttackPlayer(GameObject other)
	{
		Debug.Log(other.name);

		while (inCombat)
		{
			Debug.Log("Inside combat Player");
			if (other.GetComponent<BattleUnit>().type == "Siege")
			{
				other.GetComponent<BattleUnit>().health -= attackToSeige;
			}
			else if (other.GetComponent<BattleUnit>().type == "Combat")
			{
				other.GetComponent<BattleUnit>().health -= attackToCombat;
			}
			else if (other.GetComponent<BattleUnit>().type == "Tower")
			{
				other.GetComponent<BattleUnit>().health -= attackToTower;
			}

			if (other.GetComponent<BattleUnit>().health < 0)
			{
				manager.UpdateKilled(other.tag, other.GetComponent<BattleUnit>().type);
				Debug.Log("Killed: " + other.tag + other.GetComponent<BattleUnit>().type);
				Destroy(other);
				inCombat = false;
			}

			yield return new WaitForSeconds(1);
		}

		yield break;
	}

	public IEnumerator AttackAI(GameObject other)
	{
		Debug.Log(other.name);
		while (inCombat)
		{
			Debug.Log("Inside combat AI");

			if (other.GetComponent<BattleUnit>().type == "Siege")
			{
				other.GetComponent<BattleUnit>().health -= attackToSeige;
			}
			else if (other.GetComponent<BattleUnit>().type == "Combat")
			{
				other.GetComponent<BattleUnit>().health -= attackToCombat;
			}
			else if (other.GetComponent<BattleUnit>().type == "Tower")
			{
				other.GetComponent<BattleUnit>().health -= attackToTower;
			}

			if (other.GetComponent<BattleUnit>().health < 0)
			{
				manager.UpdateKilled(other.tag, other.GetComponent<BattleUnit>().type);
				Debug.Log("Killed: " + other.tag + other.GetComponent<BattleUnit>().type);
				Destroy(other);
				inCombat = false;
			}

			yield return new WaitForSeconds(1);
		}

		yield break;
	}

	

}
