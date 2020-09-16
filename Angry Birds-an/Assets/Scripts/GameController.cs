using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController trailController;
    public List<Bird> Birds;
    public List<Enemy> enemies;
    public BoxCollider2D tapCollider;
    private Bird shotBird;

    private bool isGameEnd = false;
    private MenuController menu;

    void Start()
    {
        menu = GameObject.Find("UIController").GetComponent<MenuController>();

        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        shotBird = Birds[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.PauseGame();
        }
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;
        if (isGameEnd)
        {
            menu.SuccessGame();
            //return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            shotBird = Birds[0];
        }

        else if (Birds.Count == 0 && enemies.Count == 0)
        {
            isGameEnd = true;
            menu.SuccessGame();
        }

        else if(Birds.Count == 0 && enemies.Count > 0)
        {
            isGameEnd = true;
            menu.GameOver();
        }
    }

    void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if (enemies.Count == 0)
        {
            isGameEnd = true;
            menu.SuccessGame();
        }
    }

    void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    private void OnMouseUp()
    {
        if (shotBird != null)
        {
            shotBird.OnTap();
        }
    }

}