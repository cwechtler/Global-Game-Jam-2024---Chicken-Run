using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlip : MonoBehaviour
{
	[SerializeField] GameObject speechBubble;
	private GameObject player;
	private bool canSlip = true;
	AIDestinationSetter destinationSetter;
	Animator[] enemyAnimators;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void PlayHit()
	{
		SoundManager.instance.PlaySlipClip();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy") && collision.isTrigger)
		{
			if (canSlip) {
				canSlip = false;
				destinationSetter = collision.GetComponent<AIDestinationSetter>();
				enemyAnimators = collision.GetComponentsInChildren<Animator>(true);

				destinationSetter.target = collision.transform;
				speechBubble.SetActive(true);
				foreach (var enemyAnimator in enemyAnimators)
				{
					enemyAnimator.SetBool("Move", false);
					enemyAnimator.SetBool("Slip", true);
				}

				StartCoroutine(Slip());
			}		
		}
	}

	private IEnumerator Slip()
	{
		yield return new WaitForSeconds(3f);
		foreach (var enemyAnimator in enemyAnimators)
		{
			enemyAnimator.SetBool("Move", true);
			enemyAnimator.SetBool("Slip", false);
		}

		destinationSetter.target = player.transform;

		speechBubble.SetActive(false);
		yield return new WaitForSeconds(3f);
		canSlip = true;
	}
}
