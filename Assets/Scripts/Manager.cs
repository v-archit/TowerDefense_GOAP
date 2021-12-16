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

	private int weightLane1 = 0;
	private int weightLane2 = 0;
	private int weightLane3 = 0;

	public Text text;
	public Toggle toggle;

	//private int priorityLane;

	private List<GameObject> woodCutters = new List<GameObject>();
	private List<GameObject> foragers = new List<GameObject>();

	public Dictionary<string, bool> GlobalState()
	{
		Dictionary<string, bool> state = new Dictionary<string, bool>();

		state.Add("hasFood", false);
		state.Add("hasWood", false);
		state.Add("hasForagedFood", false);
		state.Add("hasCutWood", false);

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


		//isFoodOrdered = toggle.isOn;
		//text.text = "Ingredients " + ingredients + "\nCooked Food " + cookedFood + "\nFood Delivery " + foodForDelivery + "\nIngredient Delivery " + ingredientForDelivery + "\nIngredients Required " + !(ingredients >= 15) + "\nTake Rest " + (cookedFood >= 10);
	}

	public int PriorityLane()
	{
		if (weightLane1 < 0)
			return 1;
		else if (weightLane2 < 0)
			return 2;
		else if (weightLane3 < 0)
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
}
