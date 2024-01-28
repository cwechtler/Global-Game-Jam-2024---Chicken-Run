using System.Collections;
using UnityEngine;

public class SpeechBubleFollow : MonoBehaviour
{
    [SerializeField] private float displayDuration; 
    [SerializeField] private GameObject speechBubblePositionGO;

	private void Awake()
	{
		StartCoroutine(DisableGO());
	}
	void Update()
    {
        this.transform.position = speechBubblePositionGO.transform.position;
    }

	IEnumerator DisableGO()
	{
		yield return new WaitForSeconds(displayDuration);
		gameObject.SetActive(false);
	}
}
