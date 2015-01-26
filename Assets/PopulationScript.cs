using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryScript))]

public class PopulationScript : MonoBehaviour {

	public float population;
	public float STORAGE_FACTOR = 0.005f; //percent of produced food to store
	public float GROWTH_CONSTANT = 0.1f;
	public float SHRINK_CONSTANT = 3.2f;

	InventoryScript inventory;

	void Start() {
		inventory = GetComponent<InventoryScript> ();
	}

	public void update(float foodProduced) {

		float foodConsumed = population * Time.deltaTime / CityScript.DAY_LENGTH;

		inventory.addResource ("food", -foodConsumed);

		float extraFood = foodProduced - population - (STORAGE_FACTOR * foodProduced); //extra produced food
		float popGrowth = (1/(1+Mathf.Exp (-extraFood)) - 0.5f) * GROWTH_CONSTANT * population / CityScript.DAY_LENGTH; //sigmoid function to model growth per day
		
		if (inventory.getResource("food") == 0)
			popGrowth = -0.15;
		
		
		population += popGrowth * Time.deltaTime;
		
		population = Mathf.Max (0, population); //population can't drop below zero

	}
}
