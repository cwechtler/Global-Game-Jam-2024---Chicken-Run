using System.Collections;
using UnityEngine;

public class SpeechBubleFollow : MonoBehaviour
{
    [SerializeField] private float displayDuration; 
    [SerializeField] private GameObject speechBubblePositionGO;
	[SerializeField] private GameObject speechBubbleGO2;

	private void Awake()
	{
		StartCoroutine(DisableGO());
	}
	void Update()
    {
        this.transform.position = speechBubblePositionGO.transform.position;
    }

	public void ActivateBubble() {
		speechBubbleGO2.SetActive(true);
		StartCoroutine(DisableGO());
	}

	IEnumerator DisableGO()
	{
		yield return new WaitForSeconds(displayDuration);
		speechBubbleGO2.SetActive(false);
	}
}
