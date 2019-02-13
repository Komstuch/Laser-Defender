using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    PlayerController playerController;
    GameObject controller;
    Color startButtonColor;
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 newPosition;
    [SerializeField] Vector2 mousePosition;
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 normalizedDirection;
    [SerializeField] float distance;
    [SerializeField] float speedMagnitude;
    float radius, width, scale, moveSpeed, padding;
    float xMin, xMax, yMin, yMax;
    bool movementOn;

    void Start()
    {
        startPosition = transform.position;
        startButtonColor = GetComponent<Image>().color;

        playerController = FindObjectOfType<PlayerController>();
        controller = this.transform.parent.gameObject;

        width = controller.GetComponent<RectTransform>().rect.width;
        scale = controller.GetComponent<RectTransform>().localScale.x;
        radius = (width * scale / 2f) * 0.4f;

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
