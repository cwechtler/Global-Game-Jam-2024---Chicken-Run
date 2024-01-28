using Pathfinding;
using System.Collections;
using UnityEngine;

public class Farmer : MonoBehaviour
{
	[SerializeField] private float speed = 5f;

	private AIPath aipath;
	private AIDestinationSetter destinationSetter;
	private GameObject player;

	private bool speedIncreased = true;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aipath = GetComponent<AIPath>();
		destinationSetter = GetComponent<AIDestinationSetter>();
		destinationSetter.target = player.transform;
		aipath.maxSpeed = speed;
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
			//aipath.maxSpeed = speed;
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

	private void FlipDirection()
	{
		if (aipath.desiredVelocity.x >= 0.01f)
		{
			transform.localScale = new Vector3(0.5f, 0.5f, 0);
		}
		else if (aipath.desiredVelocity.x <= -0.01f)
		{
			transform.localScale = new Vector3(-0.5f, 0.5f, 0);
		}
	}
	private void FlipDirectionReversed()
	{
		if (aipath.desiredVelocity.x >= 0.01f)
		{
			transform.localScale = new Vector3(-0.5f, 0.5f, 0);
		}
		else if (aipath.desiredVelocity.x <= -0.01f)
		{
			transform.localScale = new Vector3(0.5f, 0.5f, 0);
		}
	}

	private IEnumerator IncreaseSpeed() {
		yield return new WaitForSeconds(15f);
		speed = speed + .5f;
		aipath.maxSpeed = speed;
		speedIncreased	= true;
	}
}
