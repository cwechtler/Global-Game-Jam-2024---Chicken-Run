using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyChick : MonoBehaviour
{
	[SerializeField] private float minSpeed = 3f;
	[SerializeField] private float maxSpeed = 6f;
	[SerializeField] private AudioClip caughtClip;

	private AIPath aipath;
	private AIDestinationSetter destinationSetter;
	private GameObject player;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aipath = GetComponent<AIPath>();
		destinationSetter = GetComponent<AIDestinationSetter>();
		destinationSetter.target = player.transform;
		aipath.maxSpeed = Random.Range(minSpeed, maxSpeed);
	}

	void Update()
	{
		if (destinationSetter.target != player.transform)
		{
			FlipDirectionReversed();
		}
		else
		{
			FlipDirection();
		}

		if (destinationSetter.target == null)
		{
			destinationSetter.target = player.transform;
			gameObject.layer = 9;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Debug.Log("Enemy");
			SoundManager.instance.BabyChickCaughtSound(caughtClip, transform.position);
			GameController.instance.ChicksCaught++;
			GameController.instance.ChicksFollowing--;
			Destroy(gameObject);
		}
	}

	private void FlipDirection()
	{
		if (aipath.desiredVelocity.x >= 0.01f)
		{
			transform.localScale = new Vector3(1f, 1f, 0);
		}
		else if (aipath.desiredVelocity.x <= -0.01f)
		{
			transform.localScale = new Vector3(-1f, 1f, 0);
		}
	}
	private void FlipDirectionReversed()
	{
		if (aipath.desiredVelocity.x >= 0.01f)
		{
			transform.localScale = new Vector3(-1f, 1f, 0);
		}
		else if (aipath.desiredVelocity.x <= -0.01f)
		{
			transform.localScale = new Vector3(1f, 1f, 0);
		}
	}
}
