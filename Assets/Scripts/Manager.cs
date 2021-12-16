using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public int inventoryFood = 2;
	public int inventoryWood = 2;
	public int foragedFood = 0;
	public int cutWood = 0;

	public int population = 0;
	public int nWoodCutter = 0;
	public int nForager = 0;
	public int nCombat = 0;
	public int nSiege = 0;

	public int playerPopulation = 0;
	public int playerCombat = 0;
	public int playerSiege = 0;
	public int playerFood = 2;
	public int playerWood = 2;
	public int playerForager = 0;
	public int playerCutter = 0;

	private int weightLane1 = 0;
	private int weightLane2 = 0;
	private int weightLane3 = 0;

	public Text aiText;
	public Text playerText;

	public Transform spawnPointWorker;
	public Transform[] spawnPointsBattle;

	public GameObject siegePrefab;
	public GameObject combatPrefab;
	public GameObject foragerPrefab;
	public GameObject cutterPrefab;

	//private int priorityLane;

	private List<GameObject> woodCutters = new List<GameObject>();
	private List<GameObject> foragers = new List<GameObject>();

	private List<GameObject> woodCuttersPlayer = new List<GameObject>();
	private List<GameObject> foragersPlayer = new List<GameObject>();

	public Dictionary<string, bool> GlobalState()
	{
		Dictionary<string, bool> state = new Dictionary<string, bool>();

		state.Add("hasFood", false);
		state.Add("hasWood", false);
		state.Add("hasForagedFood", false);
		state.Add("hasCutWood", false);
		
		state.Add("defendTower", false);

		state.Add("hasFoodCombat", (inventoryFood > 10));
		state.Add("hasFoodSiege", (inventoryFood > 4));
		state.Add("hasFoodWorker", (inventoryFood > 4));

		state.Add("hasWoodCombat", (inventoryWood > 4));
		state.Add("hasWoodSiege", (inventoryWood > 8));
		state.Add("hasWoodCutter", (inventoryFood > 2));

		state.Add("isCutterPresent", (nWoodCutter > 0));
		state.Add("isForagerPresent", (nForager > 0));
		state.Add("isSpaceAvailable", (population < 10));

		return state;
	}

	private void Update()
	{


		aiText.text = "Food: " + inventoryFood + "\nWood: " + inventoryWood + "\nPopulation(/10): " + population;
		playerText.text = "Food: " + playerFood + "\nWood: " + playerWood + "\nPopulation(/10): " + playerPopulation;
	}

	public int PriorityLane()
	{
		if (Mathf.Min(weightLane1, weightLane2, weightLane3) == weightLane1)
			return 1;
		else if (Mathf.Min(weightLane1, weightLane2, weightLane3) == weightLane2)
			return 2;
		else if (Mathf.Min(weightLane1, weightLane2, weightLane3) == weightLane3)
			return 3;
		else
			return 1;
	}

	public void AddWeightLane(int l)
	{
		if (l == 1)
			weightLane1 += 1;
		else if (l == 2)
			weightLane2 += 1;
		else if (l == 3)
			weightLane3 += 1;
	}

	public void SubtractWeightLane(int l)
	{
		if (l == 1)
			weightLane1 -= 1;
		else if (l == 2)
			weightLane2 -= 1;
		else if (l == 3)
			weightLane3 -= 1;
	}

	public void AddWoodCutter(GameObject g)
	{
		woodCutters.Add(g);
	}

	public void DeleteWoodCutter()
	{
		Destroy(woodCutters[0]);
	}

	public void AddForager(GameObject g)
	{
		foragers.Add(g);
	}

	public void DeleteForager()
	{
		Destroy(foragers[0]);
	}

	public void SpawnSiege(int p)
	{
		if (playerFood > 1 && playerWood > 4 && playerPopulation < 10)
		{
			GameObject g = Instantiate(siegePrefab);
			g.transform.position = spawnPointsBattle[p - 1].position;
			g.tag = "Player";
			SubtractWeightLane(p);

			playerFood -= 1;
			playerWood -= 4;
			playerSiege += 1;
			playerPopulation += 1;
		}
	}

	public void SpawnCombat(int p)
	{
		if (playerFood > 5 && playerWood > 2 && playerPopulation < 10)
		{
			GameObject g = Instantiate(combatPrefab);
			g.transform.position = spawnPointsBattle[p - 1].position;
			g.tag = "Player";
			SubtractWeightLane(p);

			playerFood -= 5;
			playerWood -= 2;
			playerCombat += 1;
			playerPopulation += 1;
		}
	}

	public void SpawnForager()
	{
		if (playerFood > 2 && population < 10)
		{
			GameObject g = Instantiate(foragerPrefab);
			g.transform.position = spawnPointWorker.position;
			g.GetComponent<GetFood>().type = "Player";
			g.GetComponent<DeliverFood>().type = "Player";
			g.SetActive(true);
			foragersPlayer.Add(g);

			playerFood -= 2;
			playerForager += 1;
			playerPopulation += 1;
		}
	}

	public void SpawnCutter()
	{
		if (playerFood > 1 && playerWood > 1 && population < 10)
		{
			GameObject g = Instantiate(cutterPrefab);
			g.transform.position = spawnPointWorker.position;
			g.GetComponent<GetWood>().type = "Player";
			g.GetComponent<DeliverWood>().type = "Player";
			g.SetActive(true);
			woodCuttersPlayer.Add(g);

			playerWood -= 1;
			playerFood -= 1;
			playerCutter += 1;
			playerPopulation += 1;
		}
	}

	public void KillPlayerForager()
	{
		Destroy(foragersPlayer[0]);
	}
	
	public void KillPlayerWoodCutter()
	{
		Destroy(woodCuttersPlayer[0]);
	}
}
