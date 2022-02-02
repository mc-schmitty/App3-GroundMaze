using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTrigger : MonoBehaviour
{
    [SerializeField] TriggeredEvent objectToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !objectToTrigger.triggered)
        {
            objectToTrigger.Activate();
        }
    }
}
