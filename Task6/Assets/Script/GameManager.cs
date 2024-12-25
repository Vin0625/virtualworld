using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    public GameObject titleScreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame(int diffculty){
        spawnRate/=diffculty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score=0;
        scoreText.text="Score: "+score;
        titleScreen.gameObject.SetActive(false);
    }
    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive=false;
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator SpawnTarget(){
        while(isGameActive){
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd){
        score+=scoreToAdd;
        scoreText.text="Score: "+score;
    }
}
