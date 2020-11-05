using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Environment
{
    public class TimeManagerTest : MonoBehaviour
    {
        // UnityEvents
        // Pros:
        //     inspector support for designers
        // Cons:
        //     Loses inspector support when static
        //     Inaccessible in other classes without instance if static

        [Serializable]
        public class OnGameTickEvent : UnityEvent<int, int, int>
        {
        }

        // Usage: onGameTick1.AddEventlistener(MyMethod);
        [SerializeField] public OnGameTickEvent onGameTick1;

        /********************************************************************************/

        // C# Events
        // Pros:
        //     Accessible in other classes without instance
        // Cons:
        //     No inspector support

        public class GameTickEventArgs1 : EventArgs
        {
            public GameDateTime GameDateTime;
        }

        // Usage: onGameTick2 += MyMethod;
        public static event EventHandler<GameTickEventArgs1> onGameTick2;

        /********************************************************************************/

        // Delegate model versus plain delegates

        // 1. Delegate model (https://docs.microsoft.com/en-us/dotnet/standard/events)
        // Pros: Complies with .NET events convention and includes sender information
        // Cons: More code, possible overcomplicated for the use case
        public class GameTickEventArgs2 : EventArgs
        {
            public GameDateTime GameDateTime;
        }

        // Usage: onGameTick3 += MyMethod;
        public static event EventHandler<GameTickEventArgs2> onGameTick3;

        // 2. With custom delegates
        // Pros: Alternative which differs from the usual .NET implementations
        // Cons: Easier to use compared to the delegate model events

        // Usage: onGameTick4 += MyMethod;
        public static event Action<GameDateTime> onGameTick4;
    }
}