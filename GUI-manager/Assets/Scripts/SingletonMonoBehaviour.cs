using UnityEngine;

namespace Code.Scripts.Tools
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static bool isQuitting = false;

        private static T instance = null;


        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                }
                if (instance == null && isQuitting != true)
                {
                    Debug.LogError("Cannot find object of type: " + typeof(T).ToString() + "Cannot instanciate. Make sure one exists in the scene");
                }
                return instance;
            }

        }

        public virtual void Awake()
        {
            instance = this as T;
            //Instance.gameObject.SetActive(!hideOnLoad);
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }
    }
}