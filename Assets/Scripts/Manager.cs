using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public int inventoryFood = 100;
	public int inventoryWood = 80;
	public int foragedFood = 0;
	public int cutWood = 0;

	public int population = 0;
	public int nWoodCutter = 0;
	public int nForager = 0;
	public int nCombat = 0;
	public int nSeige = 0;

	public Text text;
	public Toggle toggle;

	public Dictionary<string, bool> GlobalState()
	{
		Dictionary<string, bool> state = new Dictionary<string, bool>();

		state.Add("hasFood", (inventoryFood > 200));
		state.Add("hasWood", (inventoryWood > 160));
		state.Add("hasForagedFood", false);
		state.Add("hasCutWood", false);

		//state.Add("hasIngredients", (ingredients >= 2));
		//state.Add("hasFoodForDelivery", (foodForDelivery >= 3));
		//state.Add("hasIngredientsForDelivery", (ingredientForDelivery >= 4));
		//state.Add("hasFoodDelivered", !isFoodOrdered);
		//state.Add("hasIngredientsDelivered", (ingredients >= 15));
		//state.Add("hasIngredientsInStore", true);
		//state.Add("takeRest", !(cookedFood >= 10));

		return state;
	}

	private void Update()
	{
		//isFoodOrdered = toggle.isOn;
		//text.text = "Ingredients " + ingredients + "\nCooked Food " + cookedFood + "\nFood Delivery " + foodForDelivery + "\nIngredient Delivery " + ingredientForDelivery + "\nIngredients Required " + !(ingredients >= 15) + "\nTake Rest " + (cookedFood >= 10);
	}

	
}
