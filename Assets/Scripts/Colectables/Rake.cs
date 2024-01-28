using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rake : MonoBehaviour
{
	[SerializeField] GameObject speechBubble;
	private GameObject player;
    private Animator animator;

    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		animator = GetComponent<Animator>();
    }

    public void PlayHit() {
        SoundManager.instance.PlayRakeClip();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy") && collision.isTrigger) {
            animator.SetTrigger("Flip");
			StartCoroutine(Smack(collision.gameObject));

		}
    }

	private IEnumerator Smack(GameObject farmer)
	{
		speechBubble.SetActive(true);
		AIDestinationSetter destinationSetter = farmer.GetComponent<AIDestinationSetter>();
		Animator[] enemyAnimators = farmer.GetComponentsInChildren<Animator>(true);
		foreach (var enemyAnimator in enemyAnimators)
		{
			if (enemyAnimator.isActiveAndEnabled)
			{
				enemyAnimator.SetBool("Move", false);
			}
		}

		destinationSetter.target = farmer.transform;

		yield return new WaitForSeconds(3f);
		foreach (var enemyAnimator in enemyAnimators)
		{
			if (enemyAnimator.isActiveAndEnabled)
			{
				enemyAnimator.SetBool("Move", true);
			}
		}

		destinationSetter.target = player.transform;

		speechBubble.SetActive(false);
	}
}
