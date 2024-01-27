using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	[SerializeField] private AudioClip eggBreakClip;

	void Start()
	{
		StartCoroutine(DestroyGO());
	}

	IEnumerator DestroyGO()
	{
		yield return new WaitForSeconds(eggBreakClip.length);
		Destroy(this.gameObject);
	}
}
