using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.UIElements.Runtime;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        ///     Uxml/Uss ui element class selectors.
        ///     Match strings with Uxml class names and types.
        ///     
        ///     C_MENU_BUTTON type = Button
        ///     C_CLOSE_BUTTON type = Button
        ///     C_SCREEN_CONTAINER type = VisualElement
        ///     C_TEMPLATE_CONTAINER type = TemplateContainer (embedded uxml)
        ///     N_DEFAULT_SCREEN = VisualElement
        ///     
        ///     C = Class selector
        ///     N = Name selector
        /// </summary>
        public const string C_MENU_BUTTON = "menu-button";              
        public const string C_CLOSE_BUTTON = "close-button";
        public const string C_SCREEN_CONTAINER = "screen-container";
        public const string C_TEMPLATE_CONTAINER = "screen-template";
        public const string N_DEFAULT_SCREEN = "screen-I";

        public Button closeButton;

        public VisualElement currentScreen;

        public List<Button> menuButtons = new List<Button>();

        public List<VisualElement> screenContainers = new List<VisualElement>();

        // Store screen templates for targeting 
        public List<TemplateContainer> screens = new List<TemplateContainer>();

        // Panel renderer (display UI in runtime scene) 
        private PanelRenderer panelRenderer;

        // Virtual Tree (root) for querys
        private VisualElement root;

        void Start()
        {
            // Set panel renderer values
            panelRenderer = GetComponent<PanelRenderer>();
            root = panelRenderer.visualTree;

            // Set main UI elements
            try
            {
                closeButton = root.Query<Button>(C_CLOSE_BUTTON).First();
                menuButtons = root.Query<Button>(classes: C_MENU_BUTTON).ToList();
                screenContainers = root.Query<VisualElement>(classes: C_SCREEN_CONTAINER).ToList();

                // TODO: Hard coded, should change based on userinput (key "i" opens "inventory", "c" "character", etc.) 
                // See: https://trello.com/c/wUNnTvBN/18-p-rebind-rebind-controls
                // See: https://trello.com/c/z4tsq8aW/59-pause-menu-ui-controls-toevoegen
                currentScreen = root.Query<VisualElement>(N_DEFAULT_SCREEN).First();
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Can't set UI element(s). Got error: " + e.Message);
            }

            SetScreens(true, true);   // Set screen template containers and hide all except current
            SetEvents();              // Set callback for button events
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

        void SetScreens(bool hideScreens = false, bool showCurrentScreen = false) 
        {
            try
            {
                screens.Clear();
                screens.AddRange(root.Query<TemplateContainer>(classes: C_TEMPLATE_CONTAINER).ToList());
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Can't find screen template containers with classname " +
                                "'" + C_TEMPLATE_CONTAINER + "'. Got error: " + e.Message);
            }

            if (hideScreens || showCurrentScreen)
            {
                HideScreens(showCurrentScreen);
            }
        }

        void SetEvents()
        {
            SetMenuButtonEvents();      
            SetCloseScreenEvent(); 
        }

        void HideScreens(bool exceptCurrentScreen = false)
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

        void SetMenuButtonEvents()
        {
            menuButtons.ForEach(button =>
            {
                if (!String.IsNullOrEmpty(button.viewDataKey))
                {
                    // Check if button viewDataKey matches viewDataKey from any screen container
                    bool screenExists = ScreenContainerExists(button.viewDataKey);
                    if (!screenExists)
                    {
                        Debug.LogError("'" + button.name + "' viewDataKey (" + button.viewDataKey + ") " +
                                       "doesn't match any viewDataKey with '" + C_SCREEN_CONTAINER + "' class appended.");
                        return;
                    }

                    button.clickable.clicked += () => ShowScreen(button.viewDataKey);
                }
            });
        }

        void SetCloseScreenEvent()
        {
            try
            {
                closeButton.clickable.clicked += () => gameObject.SetActive(!gameObject.activeSelf);
            }
            catch(NullReferenceException e)
            {
                Debug.LogError("Can't set close button event. No button with class '" + C_CLOSE_BUTTON + "' listed. Got Error: " + e.Message);
            }
        }

        bool ScreenContainerExists(string viewKey)
        {
            return screenContainers.Any(x => x.viewDataKey == viewKey);
        }

        void ShowScreen(string screenKey)
        {
            bool screenExists = screenContainers.Any(x => x.viewDataKey == screenKey);
            if (!screenExists)
            {
                Debug.LogError("Can't show screen, no screen container contains '" + screenKey + "' viewDataKey.");
                return;
            }

            foreach(VisualElement x in screenContainers)
            {
                x.style.display = x.viewDataKey == screenKey
                   ? DisplayStyle.Flex
                   : DisplayStyle.None;
            }
        }

    }
}

