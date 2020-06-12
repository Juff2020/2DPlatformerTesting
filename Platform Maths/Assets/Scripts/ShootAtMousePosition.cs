using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtMousePosition : MonoBehaviour
{

    public PlayerController playerController;

    public float startingArrows;
    public float remainingArrows;

    public float arrowForce;

    public Transform playerFirePosition;
   
    public GameObject arrow;

    public Vector2 mousePosition;
    public float angleToMouse;

    public Vector2 arrowPointingDirection;

    private void Start()
    {
        remainingArrows = startingArrows;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angleToMouse = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleToMouse);

        if (Input.GetMouseButtonDown(0) && remainingArrows >= 1 && playerController.starUpgrades >= 1)
        {
            remainingArrows--;
            FireArrow();
        }
    }

    private void FireArrow()
    {
        GameObject firedArrow = Instantiate(arrow, playerFirePosition.position, playerFirePosition.rotation);
        firedArrow.GetComponent<Rigidbody2D>().velocity = playerFirePosition.right * arrowForce;
    }
}
