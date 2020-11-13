﻿using System;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private PanelRenderer pr;

    private UQueryBuilder<VisualElement> screenContainers;
    
    private UQueryBuilder<Button> buttons;

    private Button closeButton;

    private VisualElement vt;
    private VisualElement menuContainer;
    private VisualElement currentScreen;

    private void Start()
    {
        // Set panel renderer values
        pr = GetComponent<PanelRenderer>();
        vt = pr.visualTree;

        // Set UI elements
        buttons = vt.Query<Button>(classes: "menu-button");
        screenContainers = vt.Query<VisualElement>(classes: "screen-container");
        menuContainer = vt.Query<VisualElement>("screen").First();
        closeButton = vt.Query<Button>("cl-button").First();

        // Hard coded, should change basted on userinput (key "i" opens "inventory", "c" "character", etc.)
        currentScreen = vt.Query<VisualElement>("screen-I").First();
        
        HideScreens();
        SetScreenButtonEvents();
        SetCloseScreenEvent();
    }

    void HideScreens()
    {
        screenContainers.ForEach(screen => 
        {
            if(screen != currentScreen)
            {
                screen.style.display = DisplayStyle.None;
            }
        });
    }

    void SetScreenButtonEvents()
    {
        buttons.ForEach(button => 
        {
            if (!String.IsNullOrEmpty(button.viewDataKey))
            {
                 button.clickable.clicked += delegate { SetScreen(button.viewDataKey); };
            }
        });
    }

    void SetCloseScreenEvent()
    {
        closeButton.clickable.clicked += () => gameObject.SetActive(!gameObject.activeSelf);
    }

    void SetScreen(string screenKey, bool hideOthers = true)
    {
        if(hideOthers)
        {
            screenContainers.ForEach(screen =>
            {
                screen.style.display = screen.viewDataKey == screenKey  ? DisplayStyle.Flex : DisplayStyle.None;
            });
        }
        else
        {
            vt.Query<VisualElement>(screenKey + "-screen").First().style.display = DisplayStyle.Flex;
        }
    }

}
