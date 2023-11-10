using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageSwaper : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform contentPanel;
    [SerializeField] RectTransform sampleListItem;

    [SerializeField] HorizontalLayoutGroup HLG;

    bool isSnapped;
    float snapSpeed;
    float snapForce = 100;

    void Update()
    {
        if (Input.touchCount > 0) return;

        int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)));

        if (scrollRect.velocity.magnitude < 200 && !isSnapped)
        {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;

            contentPanel.localPosition = new Vector3(
            Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
                contentPanel.localPosition.y,
                contentPanel.localPosition.z);

            if (contentPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
            {
                isSnapped = true;
            }
        }

        if (scrollRect.velocity.magnitude > 0)
        {
            isSnapped = false;
            snapSpeed = 0;
        }
    }
}



