using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {

    int collectableIndex;

    public List<GameObject> collectables = new List<GameObject>();

    void Awake()
    {

        collectables.Add(Resources.Load<GameObject>("Prefabs/blue"));
        collectables.Add(Resources.Load<GameObject>("Prefabs/red"));
        collectables.Add(Resources.Load<GameObject>("Prefabs/yellow"));

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

        Instantiate(collectables[collectableIndex], new Vector3(this.transform.position.x, 4, this.transform.position.z + 7.5f), Quaternion.identity, this.transform);

        //this.transform.GetChild(lastChildIndex).transform.localScale = new Vector3(0.5f, 1, 0.5f);

    }

    void OnCollisionExit(Collision collision) {

        if (collision.gameObject.tag == "Bullet") {

            Destroy(collision.gameObject);
            Destroy(this.transform.GetChild(4).gameObject);
            Instantiate(collectables[collectableIndex], new Vector3(this.transform.position.x, 1, this.transform.position.z), Quaternion.identity);

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
