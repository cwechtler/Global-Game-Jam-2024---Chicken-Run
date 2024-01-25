using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
	//[SerializeField] private AudioClip deathClip;
	[SerializeField] private float speed = 9f;

	private AIPath aipath;
	private AIDestinationSetter destinationSetter;
	private GameObject player;
	//private float damageTimer;
	//private bool isDead;


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

		//if (damageTimer > 0)
		//{
		//	damageTimer -= Time.deltaTime;
		//}

		if (destinationSetter.target == null)
		{
			destinationSetter.target = player.transform;
			aipath.maxSpeed = speed;
			gameObject.layer = 9;
		}
	}

	//public void reduceHealth(float damage)
	//{
	//	health -= damage;
	//	if (health <= 0 && !isDead)
	//	{
	//		isDead = true;
	//		Destroy(gameObject);
	//		SoundManager.instance.EnemyDeathSound(deathClip);
	//		GameController.instance.EnemiesKilled++;
	//	}
	//}

	//private void DamagePlayer()
	//{
	//	damageTimer = 1;
	//	player.GetComponent<PlayerController>().ReduceHealth(damage);
	//}

	//private void OnCollisionStay2D(Collision2D collision)
	//{
	//	if (collision.gameObject.CompareTag("Player"))
	//	{
	//		if (damageTimer <= 0)
	//		{
	//			DamagePlayer();
	//		}
	//	}
	//}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Skill"))
		{

		}
	}

	//private void OnParticleCollision(GameObject particle)
	//{
	//	SkillConfig particleParent = particle.GetComponentInParent<SkillConfig>();
	//	if (particleParent.SkillElementType == skillElementTypeToDestroy)
	//	{
	//		reduceHealth(particleParent.GetDamage());
	//	}
	//}

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
