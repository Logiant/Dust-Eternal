using UnityEngine;
using System.Collections;

public class SatelliteScript : MonoBehaviour {

	public float BASE_FOOD_PRODUCTION = 500;
	public float BASE_POP_GROWTH = 0.05f;
	public float BASE_MAX_POP = 200;
	public float BASE_FOOD_STORAGE = 1000;

	public float DAY_LENGTH; //seconds per day

	public float population = 200;
	public float food = 200;
	public int level = 1;


	// Use this for initialization
	void Start () {
		DAY_LENGTH = CityScript.DAY_LENGTH;
	}
	
	// Update is called once per frame
	void Update () {
		if (population < BASE_MAX_POP * level) {
			population += BASE_POP_GROWTH * level * Time.deltaTime / DAY_LENGTH;
		}
		population = Mathf.Clamp (population, 0, BASE_MAX_POP * level);
		food += (BASE_FOOD_PRODUCTION - population) * Time.deltaTime / DAY_LENGTH;

		food = Mathf.Clamp (food, 0, BASE_FOOD_STORAGE * level);
	}


	public float getFoodSurplus() {
		return BASE_FOOD_PRODUCTION * level - population;
	}
}
