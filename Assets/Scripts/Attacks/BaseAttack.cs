using UnityEngine;


[System.Serializable]
public class BaseAttack : MonoBehaviour
//public class BaseAttack
//public class BaseAttack
{

    public string attackName;// Name of the attack, for example "Fireball", "Slash", etc.
    public string attackDescription;// Description of the attack, for example "A powerful fire attack that burns the enemy", "A quick slash that deals moderate damage", etc.
    public float attackDamage;// Base Damage 15, meLLee lvl 10 stamina 35 = basedmg + stamina + lvl = 60
    public float attackCost;// ManaCost or StaminaCost, mpCost, etc.


}
