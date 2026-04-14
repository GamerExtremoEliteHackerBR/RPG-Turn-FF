using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public Button btnQuitGame;
    //[SerializeField] private string sceneName;

    //TangramPieceDataManager tangramPieceDataManager;

    // Start is called before the first frame update
    void Start()
    {
        //tangramPieceDataManager = FindObjectOfType<TangramPieceDataManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("BackMainMenu().");
    }

    public void QuitGame()
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
}
