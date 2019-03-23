using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControl : MonoBehaviour {

    public Transform playerResp;
    public Transform[] enemyResps;
    public GameObject player;
    public GameObject enemy;
    public GameObject gameoverScreen;
    public GameObject finishScreen;
    public Text scoreCount;
    public Text scoreEndScreenCount;
    public Text scoreEndScreenHighScore;

    public float enemyShotChance;
    public float enemyMoveSpeed;
    public float step;
    public float spawnsCount;
    private float timePassed;

    public bool gameOver;
    public bool loadLevel;

    public float spawnDelay;
    private float spawnDelayCounter;

    public TextMesh lifesText;
    public TextMesh scoreText;

    public static int lifes;
    private int lifesCount;

    public static float score;
    public static float highScore;

    public static bool changeColor;

    // Use this for initialization
    void Start()
    {
        changeColor = false; 

        if ( !loadLevel )
        {
            score = 0;
            lifes = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColor)
        {
            scoreText.color = Color.red;
            lifesText.color = Color.red;
        }

        AdjustDifficulty();

        if (lifes < 0 && !gameOver && score < 500)
        {
            gameOver = true;
            FindObjectOfType<PlayerTank>().gameObject.SetActive(false);
        }
        else if (lifes < 0 && !gameOver && score < 1000 && score > 500 && loadLevel)
        {
            gameOver = true;
            FindObjectOfType<PlayerTank>().gameObject.SetActive(false);
        }

        if (FindObjectOfType<PlayerTank>() == null && !gameOver)
        {
            lifes--;
            Instantiate(player, playerResp.position, playerResp.rotation);
        }

        if (gameOver)
        {
            gameoverScreen.SetActive(true);
            scoreCount.text = score.ToString();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
            }

        }

        spawnDelayCounter -= Time.deltaTime;
        if (spawnDelayCounter < 0 &&
            (FindObjectsOfType<EnemyTank>().Length +
             FindObjectsOfType<Spawner>().Length) < spawnsCount)
        {
            Instantiate(enemy, enemyResps[Random.Range(0, 3)].position, playerResp.rotation);
            spawnDelayCounter = spawnDelay;
        }

        if (lifes >= 0)
        {
            lifesText.text = lifes.ToString();
        }

        scoreText.text = score.ToString();

        if (score == 500 && !loadLevel)
        {
            loadLevel = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
        }
        else if (score > 1000 && loadLevel && lifes < 0 && !gameOver)
        {
            finishScreen.SetActive(true);
            scoreEndScreenCount.text = score.ToString();
            highScore = PlayerPrefs.GetFloat("highScore", highScore);
            scoreEndScreenHighScore.text = highScore.ToString();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
            }
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }
    }

    void AdjustDifficulty()
    {
        enemyMoveSpeed += Time.deltaTime / 300;
        enemyShotChance += Time.deltaTime / 60;
        spawnsCount += Time.deltaTime / 60;
        timePassed += Time.deltaTime;
    }

    public static void addScore(float amount)
    {
        score += amount;
    }

    public int GetMoveSpeed()
    {
        return (int)enemyMoveSpeed;
    }

    public int GetShotChance()
    {
        return (int)enemyShotChance;
    }

    public float GetStep()
    {
        return step;
    }

    public static void EnterBattleField()
    {
        changeColor = true;
    }
}
