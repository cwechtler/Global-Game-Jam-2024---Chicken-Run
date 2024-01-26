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
		}
		if (collision.CompareTag("Enemy"))
		{
			StopCoroutine(hatch());
			audioSource.Stop();
			audioSource.clip = breakClip;
			audioSource.Play();
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
		GameController.instance.EggsHatched++;
		GameController.instance.ChicksFollowing++;
		Destroy(this.gameObject);
	}
}
