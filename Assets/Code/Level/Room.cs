using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private bool active, visited;
    private List<GameObject> enemies;
    private GameObject activeImage;

    public bool big;

	// Use this for initialization
	void Start () {
        activeImage = transform.Find("Active").gameObject;
        enemies = new List<GameObject>();

        IEnemy[] enem = GetComponentsInChildren<IEnemy>();

        foreach(IEnemy e in enem)
        {
            enemies.Add(e.gameObject);
        }

        UpdateContent();
	}

    public void Enter()
    {
        active = true;
        visited = true;

        UpdateContent();
    }

    public void Exit()
    {
        active = false;

        UpdateContent();
    }

    void UpdateContent()
    {
        activeImage.SetActive(!active);

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(active);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
