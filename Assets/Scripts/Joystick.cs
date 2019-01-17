using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    PlayerController playerController;
    GameObject controller;
    Color startButtonColor;
    [SerializeField] Vector2 startPosition, newPosition, mousePosition, direction, normalizedDirection;
    [SerializeField] float distance;
    float radius, width, scale;

    void Start()
    {
        startPosition = transform.position;
        startButtonColor = GetComponent<Image>().color;

        playerController = FindObjectOfType<PlayerController>();
        controller = this.transform.parent.gameObject;

        width = controller.GetComponent<RectTransform>().rect.width;
        scale = controller.GetComponent<RectTransform>().localScale.x;
        radius = (width * scale / 2f) * 0.4f;
    }

    public void DragStart()
    {
        SetDragColor();
    }

    public void OnDragJoystick()
    {
        mousePosition = Input.mousePosition;
        direction = mousePosition - startPosition;
        normalizedDirection = direction.normalized;
        if (direction.magnitude > radius)
        {
            transform.position = startPosition + (normalizedDirection) * radius;
        } else
        {
            transform.position = startPosition + direction;
        } 
    }

    public void DragEnd()
    {
        SetIdleColor();
        transform.position = startPosition;
    }


    public void SetDragColor()
    {
        GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f);
    }

    public void SetIdleColor()
    {
        GetComponent<Image>().color = startButtonColor;
    }
}
