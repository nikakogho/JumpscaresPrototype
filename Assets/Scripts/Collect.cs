using UnityEngine;

public class Collect : MonoBehaviour {

	public GameObject toActivate;
	public Door morgueDoor;
	public enum itemType { pistol, key };
	public itemType type;
	bool near = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player"))
			near = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player"))
			near = false;
	}

	void Update()
	{
		if (near && Input.GetKeyDown ("e"))
		{
			switch (type) 
			{
			case itemType.pistol:
				toActivate.SetActive (true);
				break;
			case itemType.key:
				morgueDoor.locked = false;
				break;
			}
			Destroy (gameObject);
		}
	}
}
