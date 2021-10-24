using UnityEngine;

public class Shoot : MonoBehaviour {

	public float damage = 20;
	public int bullets = 100;

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				bullets--;

				Enemy e = hit.collider.GetComponent<Enemy>();
				if (e != null) 
				{
					e.health -= damage;
				}
			}
		}
	}
}
