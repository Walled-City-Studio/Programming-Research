using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QSystem
{
    public class QGiver : MonoBehaviour
    {
        [SerializeField] 
        private string name = "NPC";
        [SerializeField] 
        private float interactRadius = 3f;
        [SerializeField] 
        private List<Quest> quests;

        private Quest nextQuest;
        private SphereCollider sphereCollider;

        private void Start()
        {
            nextQuest = quests.First();
            AddSphereCollider();
        }

        void AddSphereCollider()
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.center = Vector3.zero;
            sphereCollider.radius = interactRadius;
        }

        public void RemoveQuest(Quest quest)
        {
            quests.Remove(quest);
            nextQuest = quests.Count > 0 ? quests.First() : null;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player") && nextQuest != null)
            {
                QHandler.Instance.AddQuestDiscovered(nextQuest);
                QHandler.Instance.SetCurrentDialogueQuest(nextQuest, this);
                DialogueManager.Instance.SetAgreeButton(true);
                DialogueManager.Instance.StartDialogue(nextQuest.dialogue);
            }
        }

        void OnTriggerExit(Collider other)
        {
            QHandler.Instance.SetCurrentDialogueQuest();
            DialogueManager.Instance.SetAgreeButton(false);
            DialogueManager.Instance.EndDialogue();
        }
 
    }
}