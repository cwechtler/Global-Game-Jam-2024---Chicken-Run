using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoseCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI eggText, eggCrushedText, chicksHatchedText, chicksStompedText, total;
	[SerializeField] private GameObject mainMenuButton;

	private Animator animator;
	private bool selected;

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		eggText.text = GameController.instance.EggsLayed.ToString();
		eggCrushedText.text = GameController.instance.EggsBroken.ToString();
		chicksHatchedText.text = GameController.instance.ChicksHatched.ToString();
		chicksStompedText.text = GameController.instance.ChicksStomped.ToString();
		total.text = GameController.instance.Score.ToString();
	}

	private void Update()
	{
		if (!selected) {
			selected = true;
			EventSystem.current.SetSelectedGameObject(mainMenuButton);
		}
	}

	public void CallLoadLevel(string name)
	{
		LevelManager.instance.LoadLevel(name);
	}

	public void MainMenu()
	{
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);
	}

	public void RestartGame()
	{
		LevelManager.instance.LoadLevel(LevelManager.Level1String, true);
	}

	public void StopAnim() {
		animator.SetBool("Flash", false);
	}
}
