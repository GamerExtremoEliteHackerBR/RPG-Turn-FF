using UnityEngine;

public class Poinson1Spell : BaseAttack
{
    public Poinson1Spell()
    {
        attackName = "Poinson 1";
        attackDescription = "Basic Poinson Spell which drags damage over time.";
        attackDamage = 5f;
        attackCost = 5f;

        //Debug.Log("Spell Poinson 1 executada.");
    }

}
