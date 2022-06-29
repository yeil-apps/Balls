using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControll : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Vector3 MoveDirection { get; private set; }

    public delegate void DirectionChanged();
    public event DirectionChanged OnDirectionChanged;

    public delegate void EndDrag();
    public event EndDrag OnDragEnd;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.delta.x < 0 && MoveDirection!=Vector3.left)
            ChangeDirection(Vector3.left);
        else if (eventData.delta.x > 0 && MoveDirection != Vector3.right)
            ChangeDirection(Vector3.right);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (MoveDirection != Vector3.zero)
        {
            ChangeDirection(Vector3.zero);
            OnDragEnd();
        }
            
    }
    public void ChangeDirection(Vector3 Direction)
    {
        MoveDirection = Direction;
        OnDirectionChanged();
    }

}
