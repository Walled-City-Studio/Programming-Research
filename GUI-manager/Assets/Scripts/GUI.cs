using Code.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIElements.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GUI : SingletonMonoBehaviour<GUI>
{
    // Root GUI uxml visual tree assets (to be loaded)
    public List<VisualTreeAsset> GUIContainerTrees = new List<VisualTreeAsset>();

    /// <summary>
    ///     Uxml/Uss ui element class selectors.
    ///     Match strings with Uxml classes, names and types.
    ///     
    ///     @ele VisualElement CLASS_GUI_CONTAINER      Container that will be appended with GUI's
    /// </summary>
    public const string NAME_GUI_CONTAINER = "GUI-container";
    public const string CLASS_SCREEN_CONTAINER = "screen-container";

    // Panel renderer (display UI in runtime scene) 
    private PanelRenderer panelRenderer;

    // Virtual Tree (root) for querys
    public VisualElement root;
    public VisualElement currentScreen;

    public List<VisualElement> screenContainers = new List<VisualElement>();

    // UI containers for toggeling visibility 
    private List<VisualElement> GUIContainers = new List<VisualElement>();

    // GUI container that will be appended with other GUI's
    private VisualElement GUIContainer;

    void Start()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        root = panelRenderer.visualTree;

        // Set main UI elements
        try
        {
            GUIContainer = root.Query<VisualElement>(name: NAME_GUI_CONTAINER).First();
            screenContainers = root.Query<VisualElement>(classes: CLASS_SCREEN_CONTAINER).ToList();
            if (GUIContainer == null)
            {
                throw new NullReferenceException("Can't find GUI container with name '" + NAME_GUI_CONTAINER + "'.");
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Can't set UI element(s). \n Got error: " + e.Message);
        }

        // Set GUI's
        SetGUIs();
        SetScreenDefaults();
    }
    
    void SetGUIs()
    {
        foreach(VisualTreeAsset GUI in GUIContainerTrees)
        {
            VisualElement gui = GUI.CloneTree();
            gui.style.display = DisplayStyle.None;

            GUIContainer.Add(gui);      // Add uxml to the actual GUI root
            GUIContainers.Add(gui);     // Add in-mem object so we dont have to Query
        }
    }

    public void SetScreen(string viewKey)
    {
        try
        {
            currentScreen = screenContainers.Where(x => x.viewDataKey == viewKey).First();
            HideScreens(true);
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Can't find screen with viewDataKey '" + viewKey + "'. Got error: " + e.Message);
        }
    }

    void SetScreenDefaults()
    {
        HideScreens(true);
    }

    public void HideScreens(bool exceptCurrentScreen = false)
    {
        try
        {
            List<VisualElement> screens = exceptCurrentScreen
                ? screenContainers
                    .Where(x => x != currentScreen)
                    .ToList()
                : screenContainers;

            foreach (VisualElement screen in screens)
            {
                screen.style.display = DisplayStyle.None;
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Can't find any screens. Got error: " + e.Message);
        }
    }

    public bool ScreenContainerExists(string viewKey)
    {
        return screenContainers.Any(x => x.viewDataKey == viewKey);
    }

    public void ShowScreen(string screenKey)
    {
        bool screenExists = screenContainers.Any(x => x.viewDataKey == screenKey);
        if (!screenExists)
        {
            Debug.LogError("Can't show screen, no screen container contains '" + screenKey + "' viewDataKey.");
            return;
        }

        foreach (VisualElement x in screenContainers)
        {
            x.style.display = x.viewDataKey == screenKey
               ? DisplayStyle.Flex
               : DisplayStyle.None;
        }
    }

}
