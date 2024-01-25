using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenNavigator : MonoBehaviour
{
	[SerializeField] private GameObject prefabToSpawn;

	private GameObject parent;
	private IAstarAI ai;
	private GridGraph grid;
	private GraphNode randomNode;
	private Vector3 destination1;
	private bool reroute;


	// Start is called before the first frame update
	void Start()
    {
		parent = GameObject.FindGameObjectWithTag("Egg Container");

		ai = GetComponent<IAstarAI>();
		grid = AstarPath.active.data.gridGraph;
		randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
		destination1 = (Vector3)randomNode.position;

		ai.destination = destination1;
		ai.SearchPath();
		reroute = true;
	}

	// Update is called once per frame
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

		yield return new WaitForSeconds(1);

		randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
		destination1 = (Vector3)randomNode.position;
		ai.destination = destination1;
		ai.SearchPath();
		reroute = true;
	}
}
