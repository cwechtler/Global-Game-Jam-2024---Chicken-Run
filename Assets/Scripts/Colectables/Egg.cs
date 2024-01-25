using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
	[SerializeField] private AudioClip hatchClip;
	[SerializeField] private AudioClip breakClip;
    
	private GameObject parent;
	private AudioSource audioSource;
	private bool canHatch;

	void Start()
    {
		parent = GameObject.FindGameObjectWithTag("Chick Container");
		audioSource = gameObject.GetComponent<AudioSource>();
		canHatch = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && canHatch) {
			canHatch = false;
			StartCoroutine(hatch());
			//GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			//enemy.transform.SetParent(parent.transform);
			//audioSource.PlayOneShot(hatchClip);
			//GameController.instance.EggsHatched++;
			//GameController.instance.ChicksFollowing++;
			//Destroy(this.gameObject);
		}
		if (collision.CompareTag("Enemy"))
		{
			audioSource.PlayOneShot(breakClip);
			GameController.instance.EggsBroken++;
			Destroy(this.gameObject);
		}
	}

	IEnumerator hatch() {
		audioSource.PlayOneShot(hatchClip);
		yield return new WaitForSeconds(hatchClip.length);
		GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
		enemy.transform.SetParent(parent.transform);
		GameController.instance.EggsHatched++;
		GameController.instance.ChicksFollowing++;
		Destroy(this.gameObject);
	}
}
