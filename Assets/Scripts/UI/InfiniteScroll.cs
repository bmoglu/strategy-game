using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    private ScrollRect _scrollRect;
    private ContentSizeFitter _contentSizeFitter;
    private VerticalLayoutGroup _verticalLayoutGroup;
    private readonly List<RectTransform> _items = new List<RectTransform>();

    private Vector2 _newAnchoredPosition = Vector2.zero;

    private bool _isVertical = false;
    private bool _hasDisabledGridComponents = false;

    private float _disableMarginY = 0;
    private float _threshold = 100f;
    private float _recordOffsetY = 0;

    private int _itemCount = 0;
    
    private void Awake()
    {
        Initialize();
    }

    //Initialize and setup scroll rect
    private void Initialize()
    {
        if (GetComponent<ScrollRect>() != null)
        {
            _scrollRect = GetComponent<ScrollRect>();
            
            //Listening value
            _scrollRect.onValueChanged.AddListener(OnScroll);

            _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

            for (var i = 0; i < _scrollRect.content.childCount; i++)
            {
                _items.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
            }
            if (_scrollRect.content.GetComponent<VerticalLayoutGroup>() != null)
            {
                _verticalLayoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
            }
            if (_scrollRect.content.GetComponent<ContentSizeFitter>() != null)
            {
                _contentSizeFitter = _scrollRect.content.GetComponent<ContentSizeFitter>();
            }

            _isVertical = _scrollRect.vertical;

            _itemCount = _scrollRect.content.childCount;
        }
        else
        {
            Debug.LogError("No ScrollRect component found");
        }
    }

    //Disabling items 
    private void DisableGridComponents()
    {
        if (_isVertical)
        {
            _recordOffsetY = _items[1].GetComponent<RectTransform>().anchoredPosition.y - _items[0].GetComponent<RectTransform>().anchoredPosition.y;
            if (_recordOffsetY < 0)
            {
                _recordOffsetY *= -1;
            }
            _disableMarginY = _recordOffsetY * _itemCount / 2; // _scrollRect.GetComponent<RectTransform>().rect.height/2 + items[0].sizeDelta.y;
        }
 
        if (_verticalLayoutGroup)
        {
            _verticalLayoutGroup.enabled = false;
        }
     
        if (_contentSizeFitter)
        {
            _contentSizeFitter.enabled = false;
        }
     
        _hasDisabledGridComponents = true;
    }

    //For scrolling event
    private void OnScroll(Vector2 pos)
    {
        if (!_hasDisabledGridComponents)
            DisableGridComponents();

        for (var i = 0; i < _items.Count; i++)
        {
            if (!_isVertical) continue;

            if (_scrollRect.transform.InverseTransformPoint(_items[i].gameObject.transform.position).y > _disableMarginY + _threshold)
            {
                _newAnchoredPosition = _items[i].anchoredPosition;
                _newAnchoredPosition.y -= _itemCount * _recordOffsetY;
                _items[i].anchoredPosition = _newAnchoredPosition;
                _scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
            }
            else if (_scrollRect.transform.InverseTransformPoint(_items[i].gameObject.transform.position).y < -_disableMarginY)
            {
                _newAnchoredPosition = _items[i].anchoredPosition;
                _newAnchoredPosition.y += _itemCount * _recordOffsetY;
                _items[i].anchoredPosition = _newAnchoredPosition;
                _scrollRect.content.GetChild(0).transform.SetAsLastSibling();
            }
        }
    }
}