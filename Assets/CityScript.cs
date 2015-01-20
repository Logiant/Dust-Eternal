using UnityEngine;
using System.Collections.Generic;

public class CityScript : MonoBehaviour {

	public static float DAY_LENGTH = 120; //seconds per day


	public float population = 5000;
	public float food = 100000;
	public float foodStorage = 100000;

	public float foodProduction = 1000; //units per second

	public float munMun = 1000;
	public float upkeepFactor = 0.999f;

	public float STORAGE_FACTOR = 0.005f; //percent of produced food to store
	public float GROWTH_CONSTANT = 0.1f;
	public float SHRINK_CONSTANT = 3.2f;


	public float materialStorage = 10000;
	public float oreStorage = 10000;

	public float ore = 0;
	public float materials = 0;

	public float BASE_MINING_RATE = 100; //materials per day
	public float miningModifier = .7f; //modifier from technology, slaves, etc

	public float BASE_MATERIAL_RATE = 90;
	public float materialModifier = .3f;
	public float materialsPerProducts = 2.5f; //raw ores required per refined material

	public float foodSurplus;

	public List<SatelliteScript> satellites;

	void Start() {
		satellites = new List<SatelliteScript> ();
	}

	// Update is called once per frame
	void Update () {

		populationAndFood ();
		production ();

	//	munMun -= population * upkeepFactor * Time.deltaTime / DAY_LENGTH / 50;  --- this seems to drop way too fast still. Numbers could be horribly off as well

		foodSurplus = food - population;


	}

	void production() {
		float toRefine = BASE_MATERIAL_RATE * materialModifier * materialsPerProducts * Time.deltaTime / DAY_LENGTH;

		ore += BASE_MINING_RATE * miningModifier * Time.deltaTime / DAY_LENGTH;

		Debug.Log ("Ore: " + ore + ", to refine: " + toRefine);


		if (materials < materialStorage && ore > toRefine) {
			ore -= toRefine;
			materials += toRefine / materialsPerProducts;
		} else if (materials < materialStorage) {
			materials += ore;
			ore = 0;
		}

		ore = Mathf.Clamp (ore, 0, oreStorage);
		materials = Mathf.Clamp (materials, 0, materialStorage);
	}



	void populationAndFood() {
		float effectiveFood = foodProduction;
		
		foreach (SatelliteScript s in satellites) {
			effectiveFood += s.getFoodSurplus();
		}
		
		food -= population * Time.deltaTime / DAY_LENGTH; //one unit per person per day
		food += effectiveFood * Time.deltaTime / DAY_LENGTH; //one haul of food per day
		
		food = Mathf.Clamp (food, 0, foodStorage); //clamp food between 0 and max storage
		
		float extraFood = effectiveFood - population - (STORAGE_FACTOR * effectiveFood); //extra produced food
		float popGrowth = (1/(1+Mathf.Exp (-extraFood)) - 0.5f) * GROWTH_CONSTANT * population / DAY_LENGTH; //sigmoid function to model growth per day
		
		if (food == 0)
			popGrowth *= SHRINK_CONSTANT;
		
		
		population += popGrowth * Time.deltaTime;
		
		population = Mathf.Max (0, population); //population can't drop below zero
	}
}
