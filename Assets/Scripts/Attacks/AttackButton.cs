using UnityEngine;

/// <summary>
/// Vai nos botões de ataque, (obj: MagicButton) e tem a função de chamar o método Input4 do BattleStateMachine, 
/// passando o ataque específico que aquele botão representa. O método Input4 é responsável por 
/// processar o ataque selecionado e realizar as ações necessárias para executá-lo durante a 
/// batalha.
/// 
/// </summary>
public class AttackButton : MonoBehaviour
{
    public BaseAttack magicAttackToPerform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastMagicAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input4(magicAttackToPerform);//Atenção ao nome do obj na string, deve ser igual ao que está na cena
    } 


}
