using UnityEngine;

public class Door : MonoBehaviour {

	public GameObject morgue;
	public bool morgueDoor = false;
	public bool locked = false;
	private Animator anim;

	private bool near = false, opened = false;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

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
		if (near && !opened && Input.GetKeyDown ("e") && !locked) 
		{
			anim.SetTrigger ("Open");
			opened = true;
			if (morgueDoor) 
			{
				Enemy.enterMorgue = true;
				morgue.SetActive (true);
			}
		}
	}
}
