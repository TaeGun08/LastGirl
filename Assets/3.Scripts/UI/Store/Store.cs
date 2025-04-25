using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Store : MonoBehaviour, IDropHandler
{
    public abstract void OnDrop(PointerEventData eventData);
}
