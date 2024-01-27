using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyChick : MonoBehaviour
{
	[SerializeField] private float minSpeed = 3f;
	[SerializeField] private float maxSpeed = 6f;
	[Space]
	[SerializeField] private Transform chicken;
	[SerializeField] private GameObject rigFront, rigBack;

	private AIPath aipath;
	private AIDestinationSetter destinationSetter;
	private GameObject player;
	public Animator[] animators;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		animators = GetComponentsInChildren<Animator>(true);

		aipath = GetComponent<AIPath>();
		destinationSetter = GetComponent<AIDestinationSetter>();
		destinationSetter.target = player.transform;
		aipath.maxSpeed = Random.Range(minSpeed, maxSpeed);
	}

	void Update()
	{
		SetAnimations();
		FlipDirection();

		if (destinationSetter.target == null)
		{
			destinationSetter.target = player.transform;
			gameObject.layer = 9;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Enemy"))
		{
			Debug.Log("Col: Enemy");
			SoundManager.instance.BabyChickCaughtSound();
			GameController.instance.ChicksCaught++;
			GameController.instance.ChicksFollowing--;
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Debug.Log("Enemy");
			SoundManager.instance.BabyChickCaughtSound();
			GameController.instance.ChicksCaught++;
			GameController.instance.ChicksFollowing--;
			Destroy(gameObject);
		}
	}

	private void SetAnimations()
	{
		bool isMovingX = aipath.desiredVelocity.x == 0f;
		bool isMovingY = aipath.desiredVelocity.y == 0f;
		if (!isMovingX || !isMovingY)
		{
			foreach (var animator in animators)
			{
				if (animator.isActiveAndEnabled)
				{
					animator.SetBool("Move", true);
				}
			}
		}
		else
		{
			foreach (var animator in animators)
			{
				if (animator.isActiveAndEnabled)
				{
					animator.SetBool("Move", false);
				}
			}
		}
	}

	private void FlipDirection()
	{
		float DirectionX = Mathf.Sign(aipath.desiredVelocity.x);
		float DirectionY = Mathf.Sign(aipath.desiredVelocity.y);
		if (DirectionX == 1)
		{
			chicken.localScale = new Vector2(.1f, .1f);
		}
		if (DirectionX == -1)
		{
			chicken.localScale = new Vector2(-.1f, .1f);
		}


		if (DirectionY == 1)
		{
			rigFront.SetActive(false);
			rigBack.SetActive(true);
		}
		if (DirectionY == -1)
		{
			rigFront.SetActive(true);
			rigBack.SetActive(false);
		}
	}
}
