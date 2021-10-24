using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	public static bool enterMorgue = false;
	private bool notEnteredYet = true;
	public AudioSource aud;
	public AudioClip[] clips;
	public GameObject parentWalkPoint, morgueParent;
	public float health;

	public LayerMask obstacles;
	public float viewAngle;

	private Animator anim;
	private bool dead = false;
	private Transform[] walkPoints;
	private NavMeshAgent agent;

	public float leaveDistance = 0.5f;

	public float seeRange;
	public float attackRange;

	private Transform target;
	private Transform player;

	public float checkTime = 1;
	private float countdown = 0;

	bool seeing()
	{
		float dist = Vector3.Distance (transform.position, player.position);
		if (dist <= seeRange)
		{
			if (Vector3.Angle (transform.position, player.position) <= viewAngle) 
			{
				if (!Physics.Raycast (transform.position, player.position, dist, obstacles)) 
				{
					return true;
				}
			}
		}
		
		return false;
	}

	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		enterMorgue = false;
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

		walkPoints = new Transform[parentWalkPoint.transform.childCount];

		for (int i = 0; i < walkPoints.Length; i++) 
		{
			walkPoints [i] = parentWalkPoint.transform.GetChild (i);
		}

		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update()
	{
		if (dead)
			return;
		if (health <= 0) 
		{
			dead = true;
			anim.SetTrigger ("Die");
			agent.Stop ();
			Destroy (gameObject, 4);
			return;
		}

		if (countdown > 0) 
		{
			countdown -= Time.deltaTime;
			return;
		}

		countdown = checkTime;

		if (enterMorgue && notEnteredYet) 
		{
			notEnteredYet = false;
			walkPoints = new Transform[parentWalkPoint.transform.childCount + morgueParent.transform.childCount];

			for(int i = 0; i < parentWalkPoint.transform.childCount; i++)
			{
				walkPoints [i] = parentWalkPoint.transform.GetChild (i);
			}

			for(int i = parentWalkPoint.transform.childCount; i < parentWalkPoint.transform.childCount + morgueParent.transform.childCount; i++)
			{
				walkPoints [i] = morgueParent.transform.GetChild (i - parentWalkPoint.transform.childCount);
			}
		}

		if (!seeing ())
		{
			bool timeForNew = true;
			
			if (target != null) {
				if (Vector3.Distance (transform.position, target.position) > leaveDistance) {
					timeForNew = false;
				}
			}

			if (timeForNew) {
				target = walkPoints [Random.Range (0, walkPoints.Length)];

				agent.SetDestination (target.position);
			}
		} else
		{
			float dist = Vector3.Distance (transform.position, player.position);

			if (dist > attackRange)
				agent.SetDestination (player.position);
			else 
			{
				agent.Stop ();
				anim.SetTrigger ("Hit");
				aud.clip = clips [Random.Range (0, clips.Length)];
				aud.Play ();
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, seeRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, attackRange);
	}
}
