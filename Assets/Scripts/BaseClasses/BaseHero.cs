using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseHero : BaseClass
{
    //public string name;

    //public float baseHP;
    //public float curHP;

    //public float baseMP;
    //public float curMP;

    public int stamina;
    public int intellect;
    public int dexterity;
    public int agility;

    public List<BaseAttack> MagicAttacks = new List<BaseAttack>();

}
