using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD

    }

    public TurnState currentState;

    // for the ProgressBar
    private float cur_coolDown = 0f;
    private float max_coolDown = 5f; // tempo para o herói atacar, pode ser diferente para cada herói
    public Image ProgressBar;
    public GameObject Selector;

    //IeNumerator
    public GameObject EnemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 10f;
    //dead
    private bool alive = true;
    //heroPanel
    private HeroPanelStats stats;
    public GameObject HeroPanel;// referencie o prefab HeroBar
    [SerializeField] private Transform HeroPanelSpacer; // referencie o objeto HeroPanelSpacer do canvas, ou encontre ele por código, como abaixo:
    //private Transform HeroPanelSpacer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //find spacer   
        //Também Posso tornar HeroPanelSpacer seializado e arrastar o objeto HeroPanelSpacer para ele, ou posso encontrar o objeto HeroPanelSpacer por código, como abaixo:
        //HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.FindChild("HeroPanel").transform.FindChild("HeroPanelSpacer");
        //HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").Find("HeroPanelSpacer");
        HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").transform.Find("HeroPanelSpacer");
        Debug.Log("PANEL encotrado: " + HeroPanelSpacer.gameObject.name);

        //creat panel 
        CreateHeroPanel();

       

        startPosition = transform.position;
        cur_coolDown = UnityEngine.Random.Range(0, 2.5f);
        Selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;


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

            case (TurnState.ADDTOLIST):
                BSM.HerosToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;

                break;

            case (TurnState.WAITING): 
                //idle
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
                    //change tag
                    this.gameObject.tag = "DeadHero";
                    //not atttackable by enemy / remove from herolist
                    BSM.HerosInBattle.Remove(this.gameObject);
                    //not managable
                    BSM.HerosToManage.Remove(this.gameObject);
                    //deative the selector
                    Selector.SetActive(false);
                    //reset gui
                    BSM.AttackPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);
                    //remove item from perform list
                    if(BSM.PerformList.Count > 0)
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]);
                                }

                                if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.HerosInBattle[UnityEngine.Random.Range(0, BSM.HerosInBattle.Count)];
                                }
                            }
                            
                        }
                    }
                    
                    /*
                    //if (BSM.HerosInBattle.Count > 0)
                    //{
                    //    for (int i = 0; i < BSM.PerformList.Count; i++)
                    //    {
                    //        //if (BSM.PerformList[i].attackerGO == this.gameObject)
                    //        if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                    //        {
                    //            BSM.PerformList.Remove(BSM.PerformList[i]);
                    //        }
                    //        //if (BSM.PerformList[i].attackTarget == this.gameObject)
                    //        if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                    //        {
                    //            //BSM.PerformList[i].attackTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
                    //            BSM.PerformList[i].AttackersTarget = BSM.HerosInBattle[UnityEngine.Random.Range(0, BSM.HerosInBattle.Count)];
                    //        }
                    //    }
                    //}
                    */


                    //change color / play animation
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);
                    //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(155, 155, 155, 255);
                    //reset heroinput
                    BSM.HeroInput = BattleStateMachine.HeroGUI.ACTIVATE;//<<<<<<<
                    //BSM.HeroInput = BattleStateMachine.PerformAction.CHECKALIVE;
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                    alive = false;

                    
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
        float calc_coolDown = cur_coolDown / max_coolDown;
        ProgressBar.transform.localScale = new Vector3(
            Mathf.Clamp(calc_coolDown, 0, 1), 
            ProgressBar.transform.localScale.y, 
            ProgressBar.transform.localScale.z);

        if(cur_coolDown >= max_coolDown)
        {
            currentState = TurnState.ADDTOLIST;
            Debug.Log(gameObject.name + " > " + currentState + " of characters.");
        }

    }


    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //animate the enemy near the hero to attack
        Vector3 enemyPosition = new Vector3(
            EnemyToAttack.transform.position.x + 1.5f,
            EnemyToAttack.transform.position.y,
            EnemyToAttack.transform.position.z);
        while (MoveTowardsEnemy(enemyPosition)) { yield return null; }
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
        ///Evita que a bara de ação do herói seja resetada para WAIT se a batalha já tiver terminado (WIN ou LOSE),
        ///permitindo que as animações de vitória ou derrota sejam exibidas corretamente.
        if (BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

            //reset this enemy state
            cur_coolDown = 0f;
            currentState = TurnState.PROCESSING;
        }
        else
        {
            currentState = TurnState.WAITING;
        }
        ///Era assim antes,INICIO
        ///barra de ação do herói só é resetada para WAIT se a batalha ainda estiver em andamento, 
        ///evitando que as animações de vitória ou derrota sejam interrompidas prematuramente.
        //BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

        ////end coroutine
        //actionStarted = false;

        ////reset this enemy state
        //cur_coolDown = 0f;
        //currentState = TurnState.PROCESSING;
        ///Era assim antes,FIM


        //end coroutine
        actionStarted = false;


    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void TakeDamage(float getDamageAmount)
    {
        hero.curHP -= getDamageAmount;
        if (hero.curHP <= 0)
        {
            hero.curHP = 0;
            currentState = TurnState.DEAD;
            //BSM.HerosToManage.Remove(this.gameObject);
            //Selector.SetActive(false);

        }

        UpdateHeroPanel();

    }

    //do damage
    void DoDamage()
    {
        float calc_damage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
        EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage);
    }

    /// <summary>
    /// Create a hero panel for the hero, fill the info and set the parent to the HeroPanelSpacer
    /// </summary>
    void CreateHeroPanel()
    {
        HeroPanel = Instantiate(HeroPanel) as GameObject;
        stats = HeroPanel.GetComponent<HeroPanelStats>();
        stats.HeroName.text = hero.theName;

        stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP; // para mostrar o HP atual e o HP base (50/1000)
        //stats.HeroHP.text = "HP: " + hero.curHP;

        //stats.HeroMP.text = "MP: " + hero.curMP;
        stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP; // para mostrar o MP atual e o MP base (50/1000)

        ProgressBar = stats.ProgressBar;
        HeroPanel.transform.SetParent(HeroPanelSpacer, false);

    }


    /// <summary>
    /// update stats on gamage / heal
    /// 
    /// Atualiza o painel do herói com as informações atuais do herói, como HP, MP, etc. 
    /// Deve ser chamado sempre que houver uma mudança nas informações do herói para manter o painel atualizado.
    /// </summary>
    void UpdateHeroPanel()
    {
        stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP; // para mostrar o HP atual e o HP base (50/1000)
        //stats.HeroHP.text = "HP: " + hero.curHP;
        //stats.HeroMP.text = "MP: " + hero.curMP;
        stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP; // para mostrar o MP atual e o MP base (50/1000)


    }


}
