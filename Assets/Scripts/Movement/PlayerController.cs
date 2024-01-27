using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private int health = 100;
	[SerializeField] private Vector2 speed = new Vector2(10, 10);
	[Space]
	[SerializeField] private Transform chicken;
	[SerializeField] private GameObject rigFront, rigBack;
	[Space]
	[SerializeField] private CanvasController canvasController;

	public float FireX { get => fireX; }
	public float FireY { get => fireY; }

	private Rigidbody2D myRigidbody2D;
	public Animator[] animators;

	private bool moveHorizontaly, moveVertically;
	private bool isDead = false;

	private float fireX, fireY;


	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		animators = GetComponentsInChildren<Animator>(true);
	}

	void Update()
	{
		if (!isDead) {
			Move();
			Fire();
		}
	}

	//public void ReduceHealth(int damage)
	//{
	//	if (!isDead) {
	//		SoundManager.instance.PlayHurtClip();

	//		health -= damage;
	//		canvasController.ReduceHealthBar(health);

	//		if (health <= 0) {
	//			StartCoroutine(PlayerDeath());
	//		}
	//	}
	//}

	private void Move()
	{
		float inputY = Input.GetAxis("Vertical");
		float inputX = Input.GetAxis("Horizontal");

		if (Input.GetMouseButton(0)) {
			Vector3 direction = MousePointerDirection();

			inputX = Mathf.Clamp(direction.x, -1, 1);
			inputY = Mathf.Clamp(direction.y, -1, 1);
		}

		myRigidbody2D.velocity = new Vector2(speed.x * inputX, speed.y * inputY);
		moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
		moveVertically = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

		SetAnimations();
		FlipDirection();
	}

	private IEnumerator PlayerDeath()
	{
		isDead = true;
		myRigidbody2D.isKinematic = true;
		myRigidbody2D.velocity = new Vector3(0, 0, 0);
		foreach (var animator in animators) {
			animator.SetBool("IsDead", true);
		}
		SoundManager.instance.PlayDeathClip();
		yield return new WaitForSeconds(2f);
		LevelManager.instance.LoadLevel(LevelManager.LoseLevelString);
	}

	private void Fire() {
		fireY = Input.GetAxis("SpellVertical");
		fireX = Input.GetAxis("SpellHorizontal");

		if (Input.GetMouseButton(1)) {
			Vector3 direction = MousePointerDirection();

			fireX = Mathf.Clamp(direction.x, -1, 1);
			fireY = Mathf.Clamp(direction.y, -1, 1);
		}
	}

	private Vector3 MousePointerDirection()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z += Camera.main.nearClipPlane;

		Vector3 heading = mousePosition - transform.position;
		float distance = heading.magnitude - 5;
		Vector3 direction = heading / distance;
		return direction;
	}

	private void SetAnimations()
	{
		if (moveHorizontaly || moveVertically)
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
		if (moveHorizontaly ) {
			float DirectionX = Mathf.Sign(myRigidbody2D.velocity.x);
			if (DirectionX == 1) {
				chicken.localScale = new Vector2(.2f, .2f);
			}
			if (DirectionX == -1) {
				chicken.localScale = new Vector2(-.2f, .2f);
			}
		}

		if (moveVertically) {
			float DirectionY = Mathf.Sign(myRigidbody2D.velocity.y);
			if (DirectionY == 1) {
				rigFront.SetActive(false);
				rigBack.SetActive(true);
			}
			if (DirectionY == -1) {
				rigFront.SetActive(true);
				rigBack.SetActive(false);
			}
		}
	}
}
