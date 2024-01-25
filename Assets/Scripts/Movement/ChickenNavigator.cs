using Pathfinding;
using System.Collections;
using UnityEngine;

public class ChickenNavigator : MonoBehaviour
{
	[SerializeField] private GameObject prefabToSpawn;
	[SerializeField] private AudioClip layClip;

	private GameObject parent;
	private AudioSource audioSource;
	private IAstarAI ai;
	private GridGraph grid;
	private GraphNode randomNode;
	private Vector3 destination1;
	private bool reroute;

	void Start()
    {
		parent = GameObject.FindGameObjectWithTag("Egg Container");
		audioSource = gameObject.GetComponent<AudioSource>();

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
}
