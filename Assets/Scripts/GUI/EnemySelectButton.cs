using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;
    //private GameObject Selector;
    private bool showSelector;


    public void SelectEnemy()
    {
        //GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();//save input enemy prefab
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(EnemyPrefab);//save input enemy prefab
    }

    //public void ToggleSelector()
    //{
    //    if (showSelector)
    //    {
    //        //EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(showSelector);
    //        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(showSelector);
    //        showSelector = !showSelector;
    //    }
    //    if (!showSelector)
    //    {
    //        //EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(showSelector);
    //        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(showSelector);
    //        showSelector = !showSelector;
    //    }


    //}

    public void HideSelector()
    {
        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(false);


    }
    public void ShowSelector()
    {
        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(true);


    }




}
