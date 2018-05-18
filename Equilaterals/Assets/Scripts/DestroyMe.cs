using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	public float timeToDestory;

	// Use this for initialization
	void Start () {
		if (timeToDestory == null) {
			timeToDestory = 10;
		}
		StartCoroutine (DestroyThis ());
	
	}

	IEnumerator DestroyThis(){

		yield return new WaitForSeconds (timeToDestory);
		Destroy (gameObject);
	}
	

}
