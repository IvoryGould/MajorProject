using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {

    int collectableIndex;

    List<GameObject> collectables = new List<GameObject>();

    public GameObject shotgun;
    public GameObject dualRevolver;
    public GameObject bandage;

    void Awake()
    {

        collectables.Add(shotgun);
        collectables.Add(dualRevolver);
        collectables.Add(bandage);

    }

    // Use this for initialization
    void Start () {

        collectableIndex = Random.Range(0, 3);
        CollectableSpawn();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CollectableSpawn()
    {

        Instantiate(collectables[collectableIndex], new Vector3(this.transform.position.x, 2, this.transform.position.z), Quaternion.identity, this.transform);
        int lastChildIndex = this.transform.childCount - 1;
        this.transform.GetChild(lastChildIndex).transform.localScale = new Vector3(0.5f, 1, 0.5f);

    }

    void OnCollisionExit(Collision collision) {

        if (collision.gameObject.tag == "Bullet") {

            Destroy(this.transform.GetChild(0).gameObject);
            Instantiate(collectables[collectableIndex], new Vector3(this.transform.position.x, 1, this.transform.position.z), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "TrainDeleter")
        {

            DeleteTrain();

        }

    }

    void DeleteTrain() {

        Destroy(this.transform.parent.gameObject);

    }
}
