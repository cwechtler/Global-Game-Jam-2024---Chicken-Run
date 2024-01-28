using Pathfinding;
using System.Collections;
using UnityEngine;

public class FarmerSpawner : MonoBehaviour
{
	[SerializeField] float waitToSpawn;
	[SerializeField] GameObject farmerPrefab;

	private Camera myCamera;
	private GridGraph grid;
	private GraphNode randomNode;
	private Vector3 destination1;

	void Start()
    {
		myCamera = Camera.main;
		grid = AstarPath.active.data.gridGraph;
		randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
		destination1 = (Vector3)randomNode.position;
		//gameObject.transform.position = destination1;

		StartCoroutine(SpawnTimer(waitToSpawn));
	}

	private bool IsInCamera(Vector3 node) {
		Vector3 viewPos = myCamera.WorldToViewportPoint(node);
		if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
		{
			return true;
			// Your object is in view of the camera.
		}
		else {
			return false;
		}

	}

	private void PickNewLocation()
	{
		randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
		destination1 = (Vector3)randomNode.position;

		if (!IsInCamera(destination1)) {
			StartCoroutine(spawnFarmer(destination1));
		}
		else {
			PickNewLocation();
		}
	}

	IEnumerator SpawnTimer(float time) {
		yield return new WaitForSeconds(1f);
		if (!IsInCamera(destination1))
		{
			StartCoroutine(spawnFarmer(destination1));
		}
		else
		{
			PickNewLocation();
		}
	}


	IEnumerator spawnFarmer(Vector3 location)
	{
		SoundManager.instance.PlayRooster();
		yield return new WaitForSeconds(SoundManager.instance.roosterClipLength);
		GameObject farmer = Instantiate(farmerPrefab, location, Quaternion.identity) as GameObject;
		farmer.transform.SetParent(this.transform);
	}
}
