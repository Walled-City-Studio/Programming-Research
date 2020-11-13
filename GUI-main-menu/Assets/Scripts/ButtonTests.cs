using System.Reflection;
using Unity.Collections;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ButtonTests : MonoBehaviour
{
    //UI Elements
    private PanelRenderer panelRenderer;

    private UQueryBuilder<VisualElement> itemUIQuery;

    private VisualElement startScreen;
    private VisualElement mainScreen;

    private Button startButton;

    private Button contineuButton;
    private Button newGameButton;
    private Button loadGameButton;
    private Button optionsButton;
    private Button creditsButton;
    private Button quitButton;

    private void Start()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        itemUIQuery = panelRenderer.visualTree.Query<VisualElement>(classes: "screen");

        startScreen = panelRenderer.visualTree.Query<VisualElement>("startScreen").First();
        mainScreen = panelRenderer.visualTree.Query<VisualElement>("mainScreen").First();

        //StartScreen
        startButton = panelRenderer.visualTree.Query<Button>("startButton").First();
        startButton.clickable.clicked += delegate { ToMainMenu(); };

        //MainScreen
        contineuButton = panelRenderer.visualTree.Query<Button>("contineuButton").First();
        contineuButton.clickable.clicked += delegate { ContineuGame(); };
        newGameButton = panelRenderer.visualTree.Query<Button>("newGameButton").First();
        newGameButton.clickable.clicked += delegate { NewGame(); };
        loadGameButton = panelRenderer.visualTree.Query<Button>("loadGameButton").First();
        loadGameButton.clickable.clicked += delegate { LoadGame(); };
        optionsButton = panelRenderer.visualTree.Query<Button>("optionsButton").First();
        optionsButton.clickable.clicked += delegate { Options(); };
        creditsButton = panelRenderer.visualTree.Query<Button>("creditsButton").First();
        creditsButton.clickable.clicked += delegate { Credits(); };
        quitButton = panelRenderer.visualTree.Query<Button>("quitButton").First();
        quitButton.clickable.clicked += delegate { QuitGame(); };
    }

    public void ToMainMenu()
    {
        Debug.Log("Start");
        startScreen.style.display = DisplayStyle.None;
        mainScreen.style.display = DisplayStyle.Flex;
    }

    public void ContineuGame()
    {
        Debug.Log("Contineu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void NewGame()
    {
        Debug.Log("New Game");
    }

    void LoadGame()
    {
        Debug.Log("Load Game");
    }

    void Options()
    {
        Debug.Log("Options");
    }

    void Credits()
    {
        Debug.Log("Credits");
    }

    void QuitGame()
    {
        Debug.Log("Quit");
        //Editor quit
        UnityEditor.EditorApplication.isPlaying = false;

        //Application quit
        Application.Quit();
    }
}
