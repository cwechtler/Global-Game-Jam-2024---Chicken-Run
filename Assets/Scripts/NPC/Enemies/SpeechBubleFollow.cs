using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubleFollow : MonoBehaviour
{
    [SerializeField] private GameObject speechBubblePositionGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = speechBubblePositionGO.transform.position;
    }
}
