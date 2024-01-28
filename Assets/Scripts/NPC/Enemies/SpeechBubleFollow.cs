using UnityEngine;

public class SpeechBubleFollow : MonoBehaviour
{
    [SerializeField] private GameObject speechBubblePositionGO;

    void Update()
    {
        this.transform.position = speechBubblePositionGO.transform.position;
    }
}
