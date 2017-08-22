using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private bool active, visited;
    private List<GameObject> enemies;

    public bool big;

	// Use this for initialization
	void Start () {
        IEnemy[] enem = GetComponentsInChildren<IEnemy>();

        foreach(IEnemy e in enem)
        {
            enemies.Add(e.gameObject);
        }

        UpdateContent(active);
	}

    void UpdateContent(bool status)
    {
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(status);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
