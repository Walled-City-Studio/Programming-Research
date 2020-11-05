using CustomQuest;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QHandler : QManager<QHandler>
{
    private void Start()
    {

#if UNITY_EDITOR
        QSettings.Start();
#endif
    }
}
