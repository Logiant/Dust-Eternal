using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour {


	public float food = 100000;
	public float foodStorage = 100000;

	public float munMun = 1000;
	public float munMunStorage = 50000;
	
	public float ore = 0;
	public float oreStorage = 10000;

	public float materialStorage = 10000;
	public float materials = 0;



	public void addResource(string resource, float amt) { //replace with an ENUM or class variable!!!
		if (resource.Equals ("food")) {
			food += amt;
			food = Mathf.Clamp (food, 0, foodStorage);
		} else if (resource.Equals ("ore")) {
			ore += amt;
			ore = Mathf.Clamp (ore, 0, oreStorage);
		} else if (resource.Equals ("materials")) {
			materials += amt;
			materials = Mathf.Clamp (materials, 0, materialStorage);
		}
	}

	public void decayResource(string resource, float ratio) { //replace with an ENUM or class variable!!!
		if (resource.Equals ("food")) {
			food -= food*ratio;
			food = Mathf.Clamp (food, 0, foodStorage);
		} else if (resource.Equals ("ore")) {
			ore -= ore*ratio;
			ore = Mathf.Clamp (ore, 0, oreStorage);
		} else if (resource.Equals ("materials")) {
			materials -= ratio*materials;
			materials = Mathf.Clamp (materials, 0, materialStorage);
		}else if (resource.Equals ("money")) {
			munMun -= ratio*munMun;
			munMun = Mathf.Clamp (munMun, 0, munMunStorage);
		}
	}


	public float getResource(string resource) {
		float amt = -1;
		if (resource.Equals ("food")) {
			amt = food;
		} else if (resource.Equals ("ore")) {
			amt = ore;
		} else if (resource.Equals("materials")) {
			amt = materials;
		}
		return amt;
	}

	public float getExtraCapacity(string resource) {
		float amt = 0;
		if (resource.Equals ("food")) {
			amt = foodStorage - food;
		} else if (resource.Equals ("ore")) {
			amt = oreStorage - ore;
		} else if (resource.Equals("materials")) {
			amt = materialStorage - materials;
		}
		return amt;
	}
}
