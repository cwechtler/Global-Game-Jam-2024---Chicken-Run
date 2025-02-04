﻿//using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	public GameObject playerGO { get; private set; }
	public bool isPaused { get; private set; }
	public float timeDeltaTime { get; private set; }
	public int Score { get; set; } = 0;
	public int EggsLayed { get; set; } = 0;
	public int EggsBroken { get; set; } = 0;
	public int ChicksHatched { get; set; } = 0;
	public int ChicksStomped { get; set; } = 0;
	public int ChicksFollowing { get; set; } = 0;

	private GameObject fadePanel;

	private void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		//PlayerPrefsManager.DeleteAllPlayerPrefs();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Pause")) {
			if (!isPaused) {
				PauseGame();
			}
			else {
				ResumeGame();
			}
		}
	}

	public void resetGame() {
		Score = 0;
		EggsLayed = 0;
		EggsBroken = 0;
		ChicksHatched = 0;
		ChicksStomped = 0;
		ChicksFollowing = 0;
	}

	public void FadePanel()
	{	
		fadePanel = GameObject.FindGameObjectWithTag("Fade Panel");
		fadePanel.GetComponent<Animator>().SetBool("FadeIn", true);
	}

	private void PauseGame()
	{
		timeDeltaTime = Time.deltaTime;
		isPaused = true;
		Time.timeScale = 0;
	}

	private void ResumeGame()
	{
		Time.timeScale = 1;
		isPaused = false;
	}

	private IEnumerator RespawnPlayer(int waitToSpawn)
	{
		yield return new WaitForSeconds(waitToSpawn);
		//playerGO.transform.position = spawnPoint.transform.position;
		playerGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		playerGO.gameObject.SetActive(true);
		playerGO.GetComponent<Rigidbody2D>().isKinematic = false;
		yield return new WaitForSeconds(1);
	}

	public IEnumerator FadeCanvasGroup_TimeScale_0(CanvasGroup canvasGroup, bool isPanelOpen, float fadeTime)
	{
		float counter = 0f;

		if (isPanelOpen) {
			while (counter < fadeTime) {
				counter += timeDeltaTime;
				canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTime / timeDeltaTime);
			}
		}
		else {
			while (counter < fadeTime) {
				counter += timeDeltaTime;
				canvasGroup.alpha = Mathf.Lerp(0, 1, fadeTime / timeDeltaTime);
			}
		}
		yield return null;
	}
}
