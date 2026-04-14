using UnityEngine;

public class Fire1Spell : BaseAttack
{
    public Fire1Spell()
    {

        attackName = "Fire 1";
        attackDescription = "Basic Fire Spell which burns nothing.";
        attackDamage = 20f;
        attackCost = 10f;

        //Debug.Log("Spell Fire 1 executada.");
    }


}
