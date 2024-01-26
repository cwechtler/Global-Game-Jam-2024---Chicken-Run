using Pathfinding;
using System.Collections;
using UnityEngine;

public class ChickenNavigator : MonoBehaviour
{
	//[SerializeField] private float speed = 9f;
	[SerializeField] private Vector2 speed = new Vector2(10, 10);
	[Space]
	[SerializeField] private GameObject prefabToSpawn;
	[SerializeField] private AudioClip layClip;
	[Space]
	[SerializeField] private Transform chicken;
	[SerializeField] private GameObject rigFront, rigBack;

	private GameObject parent;
	private AudioSource audioSource;
	private Rigidbody2D myRigidbody2D;
	public Animator[] animators;
	private AIPath aipath;
	private IAstarAI ai;
	private GridGraph grid;
	private GraphNode randomNode;
	private Vector3 destination1;
	private bool reroute, moveHorizontaly, moveVertically;

	void Start()
    {
		parent = GameObject.FindGameObjectWithTag("Egg Container");
		audioSource = gameObject.GetComponent<AudioSource>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
		animators = GetComponentsInChildren<Animator>(true);

		aipath = GetComponent<AIPath>();
		ai = GetComponent<IAstarAI>();
		grid = AstarPath.active.data.gridGraph;
		randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
		destination1 = (Vector3)randomNode.position;

		ai.destination = destination1;
		ai.SearchPath();
		reroute = true;
	}

	void Update()
    {
		if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath) && reroute)
		{
			StartCoroutine(dropEgg());
			reroute = false;
		}

		myRigidbody2D.velocity = new Vector2(speed.x, speed.y);
		moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
		moveVertically = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

		SetAnimations();
		FlipDirection();
	}

	IEnumerator dropEgg()
	{
		yield return new WaitForSeconds(1);

		GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
		enemy.transform.SetParent(parent.transform);
		audioSource.PlayOneShot(layClip);

		yield return new WaitForSeconds(layClip.length);

		randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
		destination1 = (Vector3)randomNode.position;
		ai.destination = destination1;
		ai.SearchPath();
		reroute = true;
	}

	private void SetAnimations()
	{
		if (moveHorizontaly || moveVertically)
		{
			foreach (var animator in animators)
			{
				if (animator.isActiveAndEnabled)
				{
					animator.SetBool("Move", true);
				}
			}
		}
		else
		{
			foreach (var animator in animators)
			{
				if (animator.isActiveAndEnabled)
				{
					animator.SetBool("Move", false);
				}
			}
		}
	}

	private void FlipDirection()
	{
		Debug.Log(moveHorizontaly);
		Debug.Log(moveVertically);
		if (moveHorizontaly)
		{
			//var flipX = (aipath.desiredVelocity.x > 0.0f);
			float DirectionX = Mathf.Sign(aipath.desiredVelocity.x);
			Debug.Log(DirectionX);
			if (DirectionX == 1)
			{
				chicken.localScale = new Vector2(.1f, .1f);
			}
			if (DirectionX == -1)
			{
				chicken.localScale = new Vector2(-.1f, .1f);
			}
		}

		if (moveVertically)
		{
			float DirectionY = Mathf.Sign(aipath.desiredVelocity.y);
			Debug.Log(DirectionY);
			if (DirectionY == 1)
			{
				rigFront.SetActive(false);
				rigBack.SetActive(true);
			}
			if (DirectionY == -1)
			{
				rigFront.SetActive(true);
				rigBack.SetActive(false);
			}
		}
	}
}
