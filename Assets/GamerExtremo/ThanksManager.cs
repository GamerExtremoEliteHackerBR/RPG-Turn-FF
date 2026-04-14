using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThanksManager : MonoBehaviour
{
    public Button btnBackMainMenu;
    public Text thanksText; // Referência para o componente Text que exibe a mensagem

    //[SerializeField] private string sceneName;

    //TangramPieceDataManager tangramPieceDataManager;


    //3. Alternativa se você quiser manter o texto no Inspector:
    //Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
    //[SerializeField] private Text thanksText;
    //// Ou
    //[SerializeField] private string thanksMessageTemplate;
    //3. Alternativa se você quiser manter o texto no Inspector:
    //Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:


    // Start is called before the first frame update
    void Start()
    {
        //tangramPieceDataManager = FindObjectOfType<TangramPieceDataManager>();

        // Atualiza o texto de agradecimento com o nome do jogo
        UpdateThanksText();

        // Configura o botão (se necessário)
        if (btnBackMainMenu != null)
        {
            btnBackMainMenu.onClick.AddListener(() => BackMainMenu("Menu"));//Aqui, volta pra cena Menu
            Debug.Log("ATENÇÃO AO NOME DA CENA.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateThanksText()
    {
        if (thanksText != null)
        {
            string gameName = Application.productName; // Obtém o nome do jogo das Player Settings
            string thanksMessage = GenerateThanksMessage(gameName);
            thanksText.text = thanksMessage;
        }

        ////3. Alternativa se você quiser manter o texto no Inspector:
        ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
        ////E usar string.Format() para substituir o nome do jogo:
        //if (thanksText != null)
        //{
        //    string gameName = Application.productName;
        //    thanksText.text = string.Format(thanksMessageTemplate, gameName);
        //}
        ////3. Alternativa se você quiser manter o texto no Inspector:
        ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
        ////E usar string.Format() para substituir o nome do jogo:

    }

    // Gera a mensagem de agradecimento com o nome do jogo
    private string GenerateThanksMessage(string gameName)
    {
        return $@"Agradecimentos Especiais

Nós da Gamer Extremo queremos expressar nossa mais profunda gratidão a todos vocês que embarcaram conosco na incrível jornada de {gameName}. Este projeto foi construído com muito empenho, paixão e carinho, e sua participação foi fundamental para que ele se tornasse realidade.

Agradecemos a cada jogador que acreditou no nosso trabalho e se desafiou neste universo único que criamos. O seu apoio, feedback e entusiasmo nos motivaram a criar uma experiência envolvente e memorável.

Nosso sincero agradecimento também vai para todos que contribuíram direta ou indiretamente para o desenvolvimento de {gameName}. O talento e dedicação de cada colaborador foram essenciais para dar vida a este projeto.

Esperamos que tenham se divertido, aprendido e sentido a magia que colocamos em cada detalhe do jogo.

E lembrem-se: essa é apenas mais uma das aventuras que queremos compartilhar com vocês. Fiquem atentos para novos desafios e surpresas no futuro!

Com muita gratidão,

Equipe Gamer Extremo";
    }

    //    // Gera a mensagem de agradecimento com o nome do jogo
    //    private string GenerateThanksMessage(string gameName)
    //    {
    //        return $@"Agradecimentos Especiais

    //Nós da Gamer Extremo queremos expressar nossa mais profunda gratidão a todos vocês que embarcaram conosco na incrível jornada de {gameName}. Este projeto foi construído com muito empenho, paixão e carinho, e sua participação foi fundamental para que ele se tornasse realidade.

    //Agradecemos a cada jogador que acreditou no nosso trabalho e se desafiou em um universo repleto de matemática e diversão. O seu apoio, feedback e entusiasmo nos motivaram a criar uma experiência única e envolvente.

    //Nosso sincero agradecimento também vai para todos que contribuíram direta ou indiretamente para o desenvolvimento de {gameName}. O talento e dedicação de cada colaborador foram essenciais para dar vida a este projeto.

    //Esperamos que tenham se divertido, aprendido e sentido a magia que colocamos em cada fase do jogo.

    //E lembrem-se: essa é apenas a primeira de muitas aventuras que queremos compartilhar com vocês. Fiquem atentos para novos desafios e surpresas no futuro!

    //Com muita gratidão,

    //Equipe Gamer Extremo";
    //    }




    public void BackMainMenu(string sceneName)//chamado no clique do botão BackMainMenuButton da cena Thanks, passe o nome da cena
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("BackMainMenu().");
        Debug.Log("Lembre de add as cenas: Thanks, e outras no BuildIndex.");
    }

    public void ExitGame()//chamado no clique do botão QuitButton da cena Thanks
    {
        // Exibe a mensagem no console
        Debug.Log("Fechando aplicação...");

        // Para o jogo em builds
        Application.Quit();

        // No Editor do Unity, parar o modo Play
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Debug.Log("Quit.");
    }

    //public void ClaearData()//chamar no clique dos botões
    //{

    //    TangramPieceDataManager.instance.TangramDeleteDataFile();

    //    Debug.Log("Thanks ClaearData()");
    //}



   




    /*
     * 
     * MENSAGEM DEAGRECIMENTO NO FINAL DO JOGO
Agradecimentos Especiais

Nós da Gamer Extremo queremos expressar nossa mais profunda gratidão a todos vocês que embarcaram conosco na incrível jornada de Math Dash. Este projeto foi construído com muito empenho, paixão e carinho, e sua participação foi fundamental para que ele se tornasse realidade.

Agradecemos a cada jogador que acreditou no nosso trabalho e se desafiou em um universo repleto de matemática e diversão. O seu apoio, feedback e entusiasmo nos motivaram a criar uma experiência única e envolvente.

Nosso sincero agradecimento também vai para todos que contribuíram direta ou indiretamente para o desenvolvimento de Math Dash. O talento e dedicação de cada colaborador foram essenciais para dar vida a este projeto.

Esperamos que tenham se divertido, aprendido e sentido a magia que colocamos em cada fase do jogo.

E lembrem-se: essa é apenas a primeira de muitas aventuras que queremos compartilhar com vocês. Fiquem atentos para novos desafios e surpresas no futuro!

Com muita gratidão,

Equipe Gamer Extremo
     */









}

/*
 * 
    
 */