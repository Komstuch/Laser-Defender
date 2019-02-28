using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    PlayerController playerController;
    GameObject controller;
    Color startButtonColor;
    EngineThruster engineThruster;

    Vector2 startPosition;
    Vector2 newPosition;
    Vector2 mousePosition;
    Vector2 direction;
    Vector2 normalizedDirection;
    float distance;
    float speedMagnitude;
    float radius, width, scale, moveSpeed, padding;
    float xMin, xMax, yMin, yMax;
    bool movementOn;
    public float baseXLocation = 170f;

    void Start()
    {
        engineThruster = FindObjectOfType<EngineThruster>();

        startPosition = transform.position;
        startButtonColor = GetComponent<Image>().color;

        playerController = FindObjectOfType<PlayerController>();
        controller = this.transform.parent.gameObject;

        width = controller.GetComponent<RectTransform>().rect.width;
        scale = startPosition.x/baseXLocation;
        radius = (width*scale / 2f);

        movementOn = false;
        moveSpeed = playerController.moveSpeed;
        padding = playerController.padding;
        SetMoveboundaries();
    }

    private void Update()
    {
        if (movementOn & playerController) {
          Move();
        }
    }

    public void DragStart()
    {
        SetDragColor();
        movementOn = true;
    }

    public void OnDragJoystick(Touch touch)
    {
        mousePosition = touch.position;
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
        movementOn = false;
    }

    private void Move()
    {
        float deltaX = 0f;
        float deltaY = 0f;

        deltaX = normalizedDirection.x;
        deltaY = normalizedDirection.y;
        speedMagnitude = Mathf.Clamp(direction.magnitude / radius, 0f, 1f);

        var newXPos = playerController.transform.position.x + deltaX * Time.deltaTime * moveSpeed * speedMagnitude; //Frame-rate independent
        var newYPos = playerController.transform.position.y + deltaY * Time.deltaTime * moveSpeed * speedMagnitude; //Frame-rate independent

        // Restrict the player to the gamespace
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        playerController.transform.position = new Vector3(newXPos, newYPos, transform.position.z);
        engineThruster.transform.position = playerController.transform.position + new Vector3(0f, -0.4f, 0f);
    }

    public void HandleMultiTouch()
    {
        foreach (Touch touch in Input.touches)
        {
           float touchDistance = Vector2.Distance(startPosition, touch.position);
           if (touchDistance <= (1.5*width)) // Chyba wyłączy się przy przeciągnięciu kursora za daleko - tak, poprawić
            {
                OnDragJoystick(touch);
           }
        }
    }

    private void SetMoveboundaries()
    {
        Camera gameCamera = Camera.main;
        float distance = transform.position.z - Camera.main.transform.position.z; // Distance between object and the camera in z direction

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y - padding;
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
