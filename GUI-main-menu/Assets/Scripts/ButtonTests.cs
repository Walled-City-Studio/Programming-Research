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

    private UQueryBuilder<Button> but;

    private UQueryBuilder<Button> but2;

    private void Start()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        itemUIQuery = panelRenderer.visualTree.Query<VisualElement>(classes: "screen");
        //but = panelRenderer.visualTree.Query<Button>(classes: "menu-button");

        Button but2 = panelRenderer.visualTree.Query<Button>("playButton").First();

        but2.clickable.clicked += delegate { test(1); };

        // but.ForEach(asd => {
        //     asd.clickable.clicked += delegate { Debug.Log("huiyioogr"); };
        // });
    }

    void Update()
    {

    }

    void test(int i)
    {
        Debug.Log(i);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
