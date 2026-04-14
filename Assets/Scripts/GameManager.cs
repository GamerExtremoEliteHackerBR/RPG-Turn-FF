using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    ////CLASS RANDOM MONSTER
    //[System.Serializable]
    //public class RegionData
    //{
    //    public string regionName;
    //    public int maxAmountEnemys = 4;
    //    public string BattleScene;//nome da cena do sistema de batalha para carregar
    //    public List<GameObject> possibleEnemys = new List<GameObject>();

    //}

    //public int curRegion;
    public RegionData curRegion;

    //public List<RegionData> Regions = new List<RegionData>();

    //SPAWN POINTS
    public string nextSpawnPoint;

    //HERO
    public GameObject heroCharacter;

    //POSITIONS
    public Vector3 nextHeroPosition;
    public Vector3 lastHeroPosition;//BATTLE

    //SCENES
    public string sceneToLoad;
    public string lastScene;//BATTLE

    //BOOLS
    public bool isWalking = false;
    public bool canGetEncounter = false;
    public bool gotAttacked = false;

    //ENUM
    public enum GameState
    {
        WORLD_STATE,
        TOWN_STATE,
        BATTLE_STATE,
        IDLE

    }

    //BATTLE
    public int enemyAmount;
    public List<GameObject> enemysToBattle = new List<GameObject>();

    public GameState gameState;

    void Awake()
    {
        //check if instance exist
        if (instance == null)
        {
            //se not set the instance to this
            instance = this;
        }
        //is it exist but is not this instance 
        else if (instance != this)
        {
            //destroy it
            Destroy(gameObject);

        }
        //set this to be not destroyable
        DontDestroyOnLoad(gameObject);

        if (!GameObject.Find("HeroCharacter"))
        {
            //GameObject Hero = Instantiate(heroCharacter, Vector3.zero, Quaternion.identity) as GameObject;
            GameObject Hero = Instantiate(heroCharacter, nextHeroPosition, Quaternion.identity) as GameObject;
            Hero.name = "HeroCharacter";
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case GameState.WORLD_STATE:
                if (isWalking)
                {
                    RandownEcounter();

                }
                if (gotAttacked)
                {
                    gameState = GameState.BATTLE_STATE;
                }
                break;

            case GameState.TOWN_STATE:
                //canGetEncounter = false;
                break;
            case GameState.BATTLE_STATE:
                // LOAD BATTLE SCENE
                StartBattle();
                gameState = GameState.IDLE;
                //GO TO IDLE

                break;

            case GameState.IDLE:
                //canGetEncounter = false;

                break;

            default:
                Debug.Log("States out list, processing states...");
                //gameState = GameState.IDLE;
                //canGetEncounter = false;
                break;
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void LoadSceneAfterBattle()
    {
        SceneManager.LoadScene(lastScene);
    }

    void RandownEcounter()
    {
        if (isWalking && canGetEncounter)
        {
            if(Random.Range(0, 1000) < 10)
            {
                Debug.Log("I got attacked");
                gotAttacked = true;
            }
        }
    }

    void StartBattle()
    {
        //AMOUNT OF ENEMYS
        //int enemyAmount = Random.Range(1, Regions[curRegion].maxAmountEnemys + 1);
        //enemyAmount = Random.Range(1, Regions[curRegion].maxAmountEnemys + 1);
        enemyAmount = Random.Range(1, curRegion.maxAmountEnemys + 1);
        //WHICH ENEMYS
        for (int i = 0; i < enemyAmount; i++)
        {
            //enemysToBattle.Add(Regions[curRegion].possibleEnemys[Random.Range(0, Regions[curRegion].possibleEnemys.Count)]);
            enemysToBattle.Add(curRegion.possibleEnemys[Random.Range(0, curRegion.possibleEnemys.Count)]);
        }
        //HERO
        lastHeroPosition = GameObject.Find("HeroCharacter").gameObject.transform.position;
        nextHeroPosition = lastHeroPosition;
        lastScene = SceneManager.GetActiveScene().name;
        //LOAD LEVEL
        //SceneManager.LoadScene("BattleScene");
        //SceneManager.LoadScene(Regions[curRegion].BattleScene);
        SceneManager.LoadScene(curRegion.BattleScene);
        //RESET HERO
        isWalking = false;
        gotAttacked = false;
        canGetEncounter = false;

    }

}
