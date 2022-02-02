using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggeredEvent : MonoBehaviour
{
    public abstract void Activate();
    public bool triggered = false;
}
