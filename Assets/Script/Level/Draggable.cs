using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform placeholderParent = null;
    public Transform parentToReturnTo = null;
    public Transform trash;

    int newSiblingIndex = 0;


    //GameObject placeholder = null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        /*placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());*/

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        this.transform.position = eventData.position;

        /*if(placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }*/

        //parentToReturnTo.transform.GetChild(newSiblingIndex).gameObject.SetActive(true);

        newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                /*if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;*/

                break;

            }
        }
        //placeholder.transform.SetSiblingIndex(newSiblingIndex);

        //parentToReturnTo.transform.GetChild(newSiblingIndex).gameObject.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.SetParent(parentToReturnTo);
        /*this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        Destroy(placeholder);*/
        Destroy(parentToReturnTo.gameObject.transform.GetChild(newSiblingIndex).gameObject);
        this.transform.SetSiblingIndex(newSiblingIndex);
    }
}
