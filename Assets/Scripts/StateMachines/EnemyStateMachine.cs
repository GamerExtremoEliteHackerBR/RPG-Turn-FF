using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    public enum TurnState
    {
        PROCESSING,
        //ADDTOLIST,
        CHOOSEACTION,
        WAITING,
        //SELECTING,
        ACTION,
        DEAD

    }

    public TurnState currentState;

    // for the ProgressBar
    private float cur_coolDown = 0f;
    //private float max_coolDown = 5f;
    private float max_coolDown = 10f;// tempo para o inimigo atacar, pode ser diferente para cada inimigo
    //public Image ProgressBar;// não veremos a barra do inimigo

    //this gameObject
    private Vector3 startPosition;
    public GameObject Selector;

    //timeforaction stuff
    private bool actionStarted = false;
    public GameObject HeroToAttack;

    [SerializeField] private float animSpeed = 5f;

    //alive
    private bool alive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = TurnState.PROCESSING;
        Selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);

        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();

                break;

            //case (TurnState.ADDTOLIST):
            //    break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):
                // idle state
                break;

            //case (TurnState.SELECTING):
            //    break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    //change tag of enemy
                    this.gameObject.tag = "DeadEnemy";//lembre de criar a tag "DeadEnemy" no projeto
                    //not attackable by heros
                    BSM.EnemysInBattle.Remove(this.gameObject);
                    //desable the selector
                    Selector.SetActive(false);
                    //remove all inputs enemyattacks
                    if(BSM.EnemysInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if(i != 0)
                            {
                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]);
                                }
                                if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.EnemysInBattle[Random.Range(0, BSM.EnemysInBattle.Count)];
                                }
                            }
                            
                        }
                    }
                    ///Era assim, START
                    ///for (int i = 0; i < BSM.PerformList.Count; i++)
                    ///{
                    ///    if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                    ///    {
                    ///        BSM.PerformList.Remove(BSM.PerformList[i]);
                    ///    }
                    ///}
                    ///Era assim, END
                    
                    //change the color to gray / play dead animation
                    //this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    //this.gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);
                    //set alive to false
                    alive = false;
                    //reset EnemyButtons
                    BSM.EnemyButtons();
                    //check alive
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                }
                break;

            default:
                Debug.Log("States out list, processing states...");
                //currentState = TurnState.PROCESSING;
                break;
        }
    }


    void UpgradeProgressBar()
    {
        cur_coolDown = cur_coolDown + Time.deltaTime;
        //float calc_coolDown = cur_coolDown / max_coolDown;
        //ProgressBar.transform.localScale = new Vector3(
        //    Mathf.Clamp(calc_coolDown, 0, 1),
        //    ProgressBar.transform.localScale.y,
        //    ProgressBar.transform.localScale.z);

        if (cur_coolDown >= max_coolDown)
        {
            //currentState = TurnState.ADDTOLIST;
            currentState = TurnState.CHOOSEACTION;
            //Debug.Log("Enemy: " + currentState + " " + gameObject.name);
            Debug.Log(gameObject.name + " > " + currentState + " of enemys.");
        }

    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        //myAttack.Attacker = enemy.name;
        myAttack.Attacker = enemy.theName;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];

        int num = Random.Range(0, enemy.attacks.Count);
        myAttack.choosenAttack = enemy.attacks[num];
        Debug.Log(this.gameObject.name + " has choosen " + 
            myAttack.choosenAttack.attackName + " and do " + 
            myAttack.choosenAttack.attackDamage + " damage!");

        BSM.CollectActions(myAttack);

    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //animate the enemy near the hero to attack
        Vector3 heroPosition = new Vector3(
            HeroToAttack.transform.position.x - 1.5f, 
            HeroToAttack.transform.position.y, 
            HeroToAttack.transform.position.z);
        while (MoveTowardsEnemy(heroPosition)){ yield return null; }
        //wait abilt
        yield return new WaitForSeconds(0.5f);
        //do damage
        DoDamage();

        //animate back to startposition
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition)) { yield return null; }

        //remove this perform from the list BSM
        BSM.PerformList.RemoveAt(0);

        //reset BSM -> WAIT
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;

        //reset this enemy state
        cur_coolDown = 0f;
        currentState = TurnState.PROCESSING;


    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void DoDamage()
    {
        float calc_damage = enemy.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
        HeroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calc_damage);
    }


    public void TakeDamage(float getDamageAmount)
    {
        enemy.curHP -= getDamageAmount;
        if (enemy.curHP <= 0)
        {
            enemy.curHP = 0;
            currentState = TurnState.DEAD;
            //BSM.HerosToManage.Remove(this.gameObject);
            //Selector.SetActive(false);

        }

        

    }

}
