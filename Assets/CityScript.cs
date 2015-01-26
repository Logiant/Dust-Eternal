using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PopulationScript))]
[RequireComponent(typeof(InventoryScript))]

public class CityScript : MonoBehaviour {

	public static float DAY_LENGTH = 0.1f; //seconds per day

	PopulationScript population;
	public InventoryScript inventory;

	public float moneyDecayFactor = 0.001f;

	public float foodProduction = 6000;

	public float BASE_MINING_RATE = 100; //materials per day
	public float miningModifier = .7f; //modifier from technology, slaves, etc

	public float BASE_MATERIAL_RATE = 90;
	public float materialModifier = .3f;
	public float materialsPerProducts = 2.5f; //raw ores required per refined material
	
	public List<SatelliteScript> satellites;
	

	void Start() {
		satellites = new List<SatelliteScript> ();
		population = GetComponent<PopulationScript> ();
		inventory = GetComponent<InventoryScript> ();
	}

	// Update is called once per frame
	void Update () {

		populationAndFood ();
		production ();
		inventory.decayResource ("money", moneyDecayFactor * Time.deltaTime / DAY_LENGTH);
	//	munMun -= population * upkeepFactor * Time.deltaTime / DAY_LENGTH / 50;  --- this seems to drop way too fast still. Numbers could be horribly off as well

//		foodSurplus = food - population;


	}

	void production() {
		float oreMined = BASE_MINING_RATE * miningModifier * Time.deltaTime / DAY_LENGTH;
		inventory.addResource ("ore", oreMined);
		float toRefine = Mathf.Min(BASE_MATERIAL_RATE * materialModifier * materialsPerProducts * Time.deltaTime / DAY_LENGTH, inventory.getExtraCapacity("materials") * materialModifier);
		toRefine = Mathf.Min (toRefine, inventory.getResource ("ore"));
		inventory.addResource ("ore", -toRefine);
		inventory.addResource ("materials", toRefine/materialModifier);
	}



	void populationAndFood() {
		float effectiveFood = foodProduction;
		foreach (SatelliteScript s in satellites) {
			effectiveFood += s.getFoodSurplus();
		}
		population.update (effectiveFood); //update population with effective food production
		inventory.addResource ("food", effectiveFood);
	}
}
