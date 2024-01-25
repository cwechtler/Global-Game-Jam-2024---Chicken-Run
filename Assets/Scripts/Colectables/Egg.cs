using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    
	private GameObject parent;

	// Start is called before the first frame update
	void Start()
    {
		parent = GameObject.FindGameObjectWithTag("Chick Container");
	}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) {
			GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			enemy.transform.SetParent(parent.transform);
			//SoundManager.instance.EnemyDeathSound(hatchClip);
			GameController.instance.EggsHatched++;
			GameController.instance.ChicksFollowing++;
			Destroy(this.gameObject);
		}
		if (collision.CompareTag("Enemy"))
		{
			//SoundManager.instance.EnemyDeathSound(breakClip);
			GameController.instance.EggsBroken++;
			Destroy(this.gameObject);
		}
	}
}
