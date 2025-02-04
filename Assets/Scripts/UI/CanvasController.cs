using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private GameObject pausePanel;
	[Space]
	[SerializeField] private GameObject caughtHud;
	[SerializeField] private Slider playerCaughtBar;
	[Space]
	[SerializeField] private TextMeshProUGUI ScoreText;
	[SerializeField] private TextMeshProUGUI EggsHatchedText;
	[SerializeField] private TextMeshProUGUI EggsBrokenText;
	[SerializeField] private TextMeshProUGUI ChicksStompedText;
	[SerializeField] private TextMeshProUGUI ChicksFollowingText;

	private Animator animator;

	private void Start()
	{
		playerCaughtBar.value = 10;	
	}

	private void Update()
	{
		ScoreText.text = GameController.instance.Score.ToString();
		EggsHatchedText.text = GameController.instance.ChicksHatched.ToString();
		EggsBrokenText.text = GameController.instance.EggsBroken.ToString();
		ChicksStompedText.text = GameController.instance.ChicksStomped.ToString();
		ChicksFollowingText.text = GameController.instance.ChicksFollowing.ToString();

		if (GameController.instance.isPaused) {
			pausePanel.SetActive(true);
		}
		else {
			pausePanel.SetActive(false);
		}
	}
	public void ActivateCaughtHud(bool isActive) { 
		caughtHud.SetActive(isActive);
	}

	public void ReduceCaughthBar(int amount) {
		playerCaughtBar.value = amount;
	}

	public void MainMenu()
	{
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);
	}

	public void Options()
	{
		animator.SetBool("FadeOut", true);
		LevelManager.instance.LoadLevel(LevelManager.OptionsString, .9f);
	}

	public void QuitGame()
	{
		LevelManager.instance.QuitRequest();
	}
}
