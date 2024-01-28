using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn, audioPrefabToSpawn;
	[SerializeField] private AudioClip hatchClip;
    
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
		}
		if (collision.CompareTag("Enemy"))
		{
			StopCoroutine(hatch());
			audioSource.Stop();
			GameObject audioObject = Instantiate(audioPrefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			GameController.instance.EggsBroken++;
			Destroy(this.gameObject);
		}
	}

	IEnumerator hatch() {
		audioSource.clip = hatchClip;
		audioSource.Play();
		yield return new WaitForSeconds(hatchClip.length);
		GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
		enemy.transform.SetParent(parent.transform);
		GameController.instance.ChicksHatched++;
		GameController.instance.ChicksFollowing++;
		Destroy(this.gameObject);
	}
}
