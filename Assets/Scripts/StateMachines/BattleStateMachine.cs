using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }
    public PerformAction battleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>(); // Turn list
    public List<GameObject> HerosInBattle = new List<GameObject>(); // List of Heros in battle
    public List<GameObject> EnemysInBattle = new List<GameObject>(); // List of Enemys in battle

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE

    }

    public HeroGUI HeroInput;

    public List<GameObject> HerosToManage = new List<GameObject>();
    private HandleTurn HeroChoise;

    public GameObject enemyButton;
    public Transform Spacer;//ref obj SelectTargetPanel/Spacer aqui é onde os bts de seleção dos inimigos serão instanciados

    public GameObject AttackPanel; // ref obj ActionPanel
    public GameObject EnemySelectPanel;// ref obj SelectTargetPanel
    public GameObject MagicPanel;// ref obj MagicPanel

    //attack of heros
    public Transform actionSpacer;//ref obj ActionPanel/actionSpacer aqui é onde os bts de seleção dos ataques serão instanciados
    public Transform magicSpacer;//ref obj MagicPanel/magicSpacer aqui é onde os bts de seleção dos ataques mágicos serão instanciados
    public GameObject actionButton;//ref o prefab ActionButton
    public GameObject magicButton;//ref o prefab MagicButton
    private List<GameObject> atkBtns = new List<GameObject>();

    //enemy buttons
    private List<GameObject> enemyBtns = new List<GameObject>();

    //SPAWN POINTS
    public List<Transform> spawnPoints = new List<Transform>();

    void Awake()
    {
        for(int i = 0; i < GameManager.instance.enemyAmount; i++)
        {
            //GameObject NewEnemys = Instantiate(GameManager.instance.enemysToBattle[Random.Range(0, GameManager.instance.enemysToBattle.Count)], spawnPoints[i].position, Quaternion.identity) as GameObject;
            GameObject NewEnemy = Instantiate(GameManager.instance.enemysToBattle[i], spawnPoints[i].position, Quaternion.identity) as GameObject;
            NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().enemy.theName + "_" + (i + 1);//Set the name of the enemy to the name of the prefab, this is used to set the text of the enemy selection buttons
            NewEnemy.GetComponent<EnemyStateMachine>().enemy.theName = NewEnemy.name;//Set the name of the enemy to the name of the prefab, this is used to set the text of the enemy selection buttons
            EnemysInBattle.Add(NewEnemy);

        }


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleStates = PerformAction.WAIT;
        //EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        HeroInput = HeroGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        MagicPanel.SetActive(false);

        EnemyButtons();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(battleStates);

        switch (battleStates)
        {
            case (PerformAction.WAIT):
                if(PerformList.Count > 0)
                {
                    battleStates |= PerformAction.TAKEACTION;
                }

                break;

            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    for (int i = 0; i < HerosInBattle.Count; i++)
                    {
                        if (PerformList[0].AttackersTarget == HerosInBattle[i])
                        {
                            ESM.HeroToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                        {
                            //Debug.Log("Target not found, processing next action");
                            PerformList[0].AttackersTarget = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
                            ESM.HeroToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        }
                        //ESM.HeroToAttack = PerformList[0].AttackersTarget;
                        //ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                    }
                }

                if (PerformList[0].Type == "Hero")
                {
                    Debug.Log("Hero is here to perform");
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                }
                battleStates = PerformAction.PERFORMACTION;

                break;

            case (PerformAction.PERFORMACTION):
                //idle

                break;
            
            case (PerformAction.CHECKALIVE):
                if(HerosInBattle.Count < 1)
                {
                    battleStates = PerformAction.LOSE;
                    //Lose game
                }
                else if (EnemysInBattle.Count < 1)
                {
                    battleStates = PerformAction.WIN;
                    //win the battle
                }
                else
                {
                    //call function
                    ClearAttackPanel();
                    HeroInput = HeroGUI.ACTIVATE;
                    Debug.Log("Checking alive, processing next action...");


                }
                break;

            case (PerformAction.LOSE):
                Debug.Log("You Lost the battle!");

                break;


            case (PerformAction.WIN):
                {//
                    Debug.Log("You Win the battle!");
                    for (int i = 0; i < HerosInBattle.Count; i++)
                    {
                        HerosInBattle[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
                    }
                    GameManager.instance.LoadSceneAfterBattle();
                    GameManager.instance.gameState = GameManager.GameState.WORLD_STATE;
                    GameManager.instance.enemysToBattle.Clear();
                }//
                break;


            default:
                Debug.Log("States out list, processing states...");
                battleStates = PerformAction.WAIT;
                break;
        }


        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if(HerosToManage.Count > 0)
                {
                    //HerosToManage[0].transform.FindChild("Selector").gameObject.SetActive(true);
                    HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    //create new handleturn instance
                    HeroChoise = new HandleTurn();

                    AttackPanel.SetActive(true);
                    //populate action buttons
                     CreateAttackButtons();

                    HeroInput |= HeroGUI.WAITING;

                   
                }
                break;
            case (HeroGUI.WAITING):
                // idle

                break;
            //case (HeroGUI.INPUT1):
            //    break;
            //case (HeroGUI.INPUT2):
            //    break;
            case (HeroGUI.DONE):
                HeroInpuDone();

                break;
            default:
                Debug.Log("States out list, processing states...");
                HeroInput = HeroGUI.WAITING;
                break;
        }

    }


    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }

    public void EnemyButtons()
    {

        //cleanup
        foreach (GameObject enemyBtn in enemyBtns)
        {
            Destroy(enemyBtn);
        }
        enemyBtns.Clear();

        //create buttons
        foreach (GameObject enemy in EnemysInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();
            //o nome do objeto deve ser identico ao nome na hieraquia, aqui é um filho do prefab Target1Button
            //Text buttonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();

            //buttonText.text = cur_enemy.enemy.name;
            buttonText.text = cur_enemy.enemy.theName;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer, false);
            enemyBtns.Add(newButton);
        }
    }

    public void Input1() // Attack button
    {
        HeroChoise.Attacker = HerosToManage[0].name;
        HeroChoise.AttackersGameObject = HerosToManage[0];
        HeroChoise.Type = "Hero";
        HeroChoise.choosenAttack = HerosToManage[0].GetComponent<HeroStateMachine>().hero.attacks[0];
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);

        
    }

    public void Input2(GameObject chooseEnemy)// enemy selection
    {
        HeroChoise.AttackersTarget = chooseEnemy;
        HeroInput = HeroGUI.DONE;


    }

    void HeroInpuDone()
    {
        PerformList.Add(HeroChoise);

        //EnemySelectPanel.SetActive(false);

        ////clean the attackpanel
        ClearAttackPanel();
        //foreach (GameObject atkBtn in atkBtns)
        //{
        //    Destroy(atkBtn);
        //}
        //atkBtns.Clear();

        //HerosToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HerosToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    void ClearAttackPanel()
    {
        EnemySelectPanel.SetActive(false);
        AttackPanel.SetActive(false);
        MagicPanel.SetActive(false);

        //clean the attackpanel
        foreach (GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();


    }

    //Creat actionbuttons
    void CreateAttackButtons()
    {
        // Attack Button
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        Text AttackButtonText = AttackButton.transform.Find("Text").gameObject.GetComponent<Text>();//Atenção ao nome do obj
        //Text AttackButtonText = AttackButton.transform.Find("Text (Legacy)").gameObject.GetComponent<Text>();//Atenção ao nome do obj
        AttackButtonText.text = "Attack";
        //AttackButtons.GetComponent<Button>().onClick.AddListener(delegate { Input1(); });
        AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(AttackButton);

        // Magic Button
        GameObject MagicAttackButton = Instantiate(actionButton) as GameObject;
        Text MagicAttackButtonText = MagicAttackButton.transform.Find("Text").gameObject.GetComponent<Text>();//Atenção ao nome do obj
        MagicAttackButtonText.text = "Magic";
        //MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());

        MagicAttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(MagicAttackButton);

        if (HerosToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks.Count > 0)
        {
            foreach (BaseAttack magicAtk in HerosToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks)
            {
                GameObject MagicButton = Instantiate(magicButton) as GameObject;
                Text MagicButtonText = MagicButton.transform.Find("Text").gameObject.GetComponent<Text>();//Atenção ao nome do obj
                MagicButtonText.text = magicAtk.attackName;
                AttackButton ATB = MagicButton.GetComponent<AttackButton>();
                ATB.magicAttackToPerform = magicAtk;
                MagicButton.transform.SetParent(magicSpacer, false);
                atkBtns.Add(MagicButton);
            }
        }
        else
        {
            Debug.Log("No magic attacks to show");
            MagicAttackButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Input3() //switching to magic attacks
    {
        AttackPanel.SetActive(false);
        MagicPanel.SetActive(true);


    }

    public void Input4(BaseAttack chooseMagic)//choose magic attack
    {
        HeroChoise.Attacker = HerosToManage[0].name;
        HeroChoise.AttackersGameObject = HerosToManage[0];
        HeroChoise.Type = "Hero";

        HeroChoise.choosenAttack = chooseMagic;
        MagicPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);

        //AttackPanel.SetActive(false);
        //EnemySelectPanel.SetActive(true);

    }

    

}
