using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HasAbilityUI : AbilityUI, IEndDragHandler
{
    public RectTransform targetRectTrs;
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = rectTrs.localPosition;
        
        Vector3 minPosition = targetRectTrs.rect.min - rectTrs.rect.min;
        Vector3 maxPosition = targetRectTrs.rect.max - rectTrs.rect.max;
        
        if (minPosition.y > pos.y || maxPosition.y < pos.y)
        {
            rectTrs.position = PrevRectTrs.position;
        }

        if (minPosition.x > pos.x + 500f || maxPosition.x < pos.x + 500f)
        {
            rectTrs.position = PrevRectTrs.position;
        }
        
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        if (results.Count <= 0) return;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name != "StoreBuy") continue;
            abilityStore.buyShellUI[1].SetActive(true);
            abilityStore.buyShellUI[1].transform.SetAsLastSibling();
            abilityStore.AbilityUI = gameObject;
            abilityStore.AbilityData = AbilityData;
        }
    }
}
