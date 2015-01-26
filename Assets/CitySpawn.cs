using UnityEngine;
using System.Collections;

public class CitySpawn : MonoBehaviour {

	public GameObject satellitePrefab;
	
	CityScript city;

	void Start() {
		city = GetComponent<CityScript> ();
	}

	// Update is called once per frame
	void Update () {
		if (city.inventory.munMun > 2000) {
			city.inventory.addResource ("money", -1000);
			GameObject s = (GameObject)Instantiate (satellitePrefab, new Vector3(transform.position.x + Random.Range (1, 25) - 12, transform.position.y, transform.position.z + + Random.Range (1, 25) - 12), new Quaternion());
			city.satellites.Add (s.GetComponent<SatelliteScript>());
			Debug.Log("Satellite Added!");
		}
	}
}
