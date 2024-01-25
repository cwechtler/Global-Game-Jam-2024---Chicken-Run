using UnityEngine;

public class Depositor : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Baby Chick"))
		{
			//SoundManager.instance.BabyChickCaughtSound(caughtClip, transform.position);
			GameController.instance.Score += GameController.instance.ChicksFollowing;
			GameController.instance.ChicksFollowing = 0;
			Destroy(collision.gameObject);
		}

	}
}
