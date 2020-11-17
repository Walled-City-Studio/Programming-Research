using Code.Scripts.Tools;
using System;
using System.Collections.Generic;
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

    // Panel renderer (display UI in runtime scene) 
    private PanelRenderer panelRenderer;

    // Virtual Tree (root) for querys
    public VisualElement root;

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
            if(GUIContainer == null)
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


/*    void AddGUI(string uXmlPath)
    {
        VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uXmlPath);
        
    }*/

}
