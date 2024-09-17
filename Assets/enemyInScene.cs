using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyInScene : MonoBehaviour
{
    public List<GameObject> enemies;

    public string sceneToLoad;
    void Start()
    {
        foreach (GameObject fooobj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(fooobj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if (enemies.Count == 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
