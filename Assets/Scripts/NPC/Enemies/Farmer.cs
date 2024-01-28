using Pathfinding;
using System.Collections;
using UnityEngine;

public class Farmer : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	[Space]
	[SerializeField] private Transform farmer;
	[SerializeField] private GameObject rigFront, rigBack;

	private Animator[] animators;
	private AIPath aipath;
	private AIDestinationSetter destinationSetter;
	private GameObject player;

	private bool speedIncreased = true;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		animators = GetComponentsInChildren<Animator>(true);
		aipath = GetComponent<AIPath>();
		destinationSetter = GetComponent<AIDestinationSetter>();
		destinationSetter.target = player.transform;
		aipath.maxSpeed = speed;
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

		if (speedIncreased) {
			StartCoroutine(IncreaseSpeed());
			speedIncreased = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Skill"))
		{

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
			farmer.localScale = new Vector2(.5f, .5f);
		}
		if (DirectionX == -1)
		{
			farmer.localScale = new Vector2(-.5f, .5f);
		}

		if (DirectionY == 1)
		{
			if (destinationSetter.target == this.transform)
			{
				rigFront.SetActive(true);
				rigBack.SetActive(false);
			}
			else {
				rigFront.SetActive(false);
				rigBack.SetActive(true);
			}
		}
		if (DirectionY == -1)
		{
			rigFront.SetActive(true);
			rigBack.SetActive(false);
		}
	}

	private IEnumerator IncreaseSpeed() {
		yield return new WaitForSeconds(15f);
		speed = speed + .5f;
		aipath.maxSpeed = speed;
		speedIncreased	= true;
	}
}
