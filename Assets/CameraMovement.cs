using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float scrollSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = new Vector3 ();
		velocity.x = Input.GetAxis ("Horizontal") * scrollSpeed * Time.deltaTime;
		velocity.z = Input.GetAxis ("Vertical") * scrollSpeed * Time.deltaTime;

		transform.position = new Vector3 (transform.position.x + velocity.x, transform.position.y, transform.position.z + velocity.z);

	}
}
