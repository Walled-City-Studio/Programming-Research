
using System.Reflection;
using Unity.Collections;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class scri : MonoBehaviour
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
        but = panelRenderer.visualTree.Query<Button>(classes: "menu-button");

        but2 = panelRenderer.visualTree.Query<Button>(name: "I-button");

        but2.ForEach(aa => {
            but2.text = "asdaaaa11111";
        });

        but.ForEach(asd => {
 
            asd.text = "adsasd";
            //asd.RegisterCallback<KeyDownEvent>((evt) => Debug.Log(evt.keyCode));
            asd.clickable.clicked += delegate { test(1); };

           /* Clickable clickable = asd.clickable;
            MouseDownEvent mouseDownEvent = MouseDownEvent.GetPooled();
            //clickable.Invoke();
            MethodInfo dynMethod = clickable.GetType().GetMethod("Invoke",
            BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod.Invoke(clickable, new object[] { mouseDownEvent });
            mouseDownEvent.Dispose();*/
            //asd.onClick.AddListener(() => test(button1));

        });
    }
    void Update()
    {
      
    }

    void test(int i)
    {
        Debug.Log(i);
    }

}

