
using Unity.Collections;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class scri : MonoBehaviour
{

    //UI Elements
    private PanelRenderer panelRenderer;
    private UQueryBuilder<VisualElement> itemUIQuery;

    private UQueryBuilder<Button> but;

    void Update()
    {
        //Get Entities And Data
        //var playerDirtyEntities = playerDirtyEntityQuery.ToEntityArray(Allocator.TempJob);
        //var itemEntities = itemEntityQuery.ToEntityArray(Allocator.TempJob);
        //var itemOwners = itemEntityQuery.ToComponentDataArray<ActorInventoryItemOwnerComponent>(Allocator.TempJob);

        //If Inventory Updated Do Logic
        //if (playerDirtyEntities.Length != 0)
        //{
        Debug.Log("asdasd");
        //Get Panel Renderer | Get All VisualElements with "Inventory-Item" Class
        panelRenderer = GetComponent<PanelRenderer>();
            itemUIQuery = panelRenderer.visualTree.Query<VisualElement>(classes: "screen");


        but = panelRenderer.visualTree.Query<Button>(classes: "I-button");

        but.ForEach(asd => {
            Debug.Log("123");
            asd.text = "adsasd";
        });
            //Loop Through All Ivnentory Items VisualElements
            itemUIQuery.ForEach(inventoryItemVisualElement =>
            {
                Debug.Log("asdasd");
                //inventoryItemVisualElement.style.top = 20;
                //if (itemUIQueryIndex < itemEntities.Length)
                //{
                //Get InventoryItemUISHaredComponent
                /*       var actorInventoryItemUISharedComponent = EntityManager.GetSharedComponentData<ActorInventoryItemUISharedComponent>(itemEntities[itemUIQueryIndex]);

                       //Create Icon
                       var iconVisualElement = new VisualElement();
                       iconVisualElement.AddToClassList("Inventory-Item-Icon");
                       iconVisualElement.style.backgroundImage = actorInventoryItemUISharedComponent.icon;
                       inventoryItemVisualElement.Add(iconVisualElement);

                       //Create Amount
                       var amountVisualElement = new Label();
                       amountVisualElement.AddToClassList("Inventory-Item-Amount");
                       amountVisualElement.text = "1";
                       inventoryItemVisualElement.Add(amountVisualElement);*/
                //}
                //else
                //{
                //Get Rid Of Icon
           /*     if (inventoryItemVisualElement.childCount > 0)
                        inventoryItemVisualElement.RemoveAt(0);

                    //Get Rid Of Amount
                    if (inventoryItemVisualElement.childCount > 0)
                        inventoryItemVisualElement.RemoveAt(0);*/
               //}

                //itemUIQueryIndex++;
            });
        //}

        //Dispose
        //playerDirtyEntities.Dispose();
        //itemEntities.Dispose();
        //itemOwners.Dispose();
    }
}

