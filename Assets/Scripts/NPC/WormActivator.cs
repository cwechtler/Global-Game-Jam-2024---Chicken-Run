using System.Collections;
using UnityEngine;

public class WormActivator : MonoBehaviour
{
	[SerializeField] int spawnDelay;
	[SerializeField] GameObject wormGameobject;

	private void Awake()
	{
		wormGameobject.SetActive(false);
	}
	void Start()
    {
		StartCoroutine(setActive());
	}

	IEnumerator setActive()
	{
		yield return new WaitForSeconds(spawnDelay);
		wormGameobject.SetActive(true);
	}
}
