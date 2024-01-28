using Pathfinding;
using System.Collections;
using UnityEngine;

public class EggChickenSpawner : MonoBehaviour
{
	[SerializeField] int numberOfChickensToSpawn = 3;
	[SerializeField] float chickenSpawnInterval = 3f;
	[SerializeField] GameObject eggChickenPrefab;

	private Vector3 destination1;

	void Start()
    {
		GraphNode node = AstarPath.active.GetNearest(this.transform.position).node;
		destination1 = (Vector3)node.position;

		StartCoroutine(SpawnChickens(numberOfChickensToSpawn));
    }

    IEnumerator SpawnChickens(int numberOfChickens) {

        for (int i = 0; i < numberOfChickens; i++)
        {
			GameObject eggChicken = Instantiate(eggChickenPrefab, destination1, Quaternion.identity) as GameObject;
			eggChicken.transform.SetParent(this.transform);
			yield return new WaitForSeconds(chickenSpawnInterval);
		}
	}
}
