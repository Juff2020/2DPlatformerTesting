using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtMousePosition : MonoBehaviour
{

    public PlayerController playerController;
    public SpriteRenderer targetPoint;

    public float startingArrows;
    public float remainingArrows;

    public float arrowForce;

    public Transform playerFirePosition;
   
    public GameObject arrow;

    public Vector2 mousePosition;
    public Vector2 controllerDirection;
    public float angleToMouse;
    public float controllerAngle;

    public float arrowH;
    public float arrowV;

    public Vector2 arrowPointingDirection;

    private void Start()
    {
        remainingArrows = startingArrows;
    }

    private void Update()
    {
        /*
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angleToMouse = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleToMouse);

        if (Input.GetMouseButtonDown(0) && remainingArrows >= 1 && playerController.starUpgrades >= 1)
        {
            remainingArrows--;
            FireArrowMouse();
        }
        */

        arrowH = Input.GetAxisRaw("ArrowH");
        arrowV = Input.GetAxisRaw("ArrowV");

        controllerDirection = new Vector2(Input.GetAxisRaw("ArrowH"), Input.GetAxisRaw("ArrowV"));
        controllerAngle = Mathf.Atan2(Input.GetAxisRaw("ArrowH"), Input.GetAxisRaw("ArrowV")) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, controllerAngle);

        if (arrowH == 0 && arrowV == 0)
        {
            targetPoint.enabled = false;
        }
        else
        {
            targetPoint.enabled = true;
        }

        if (Input.GetButtonDown("Fire1") && remainingArrows >= 1 && playerController.starUpgrades >= 1)
        {
            remainingArrows--;
            FireArrowController();
        }

    }

    private void FireArrowMouse()
    {
        GameObject firedArrow = Instantiate(arrow, playerFirePosition.position, playerFirePosition.rotation);
        firedArrow.GetComponent<Rigidbody2D>().velocity = playerFirePosition.right * arrowForce;
    }

    private void FireArrowController()
    {
        GameObject firedArrow = Instantiate(arrow, playerFirePosition.position, playerFirePosition.rotation);
        firedArrow.GetComponent<Rigidbody2D>().velocity = playerFirePosition.right * arrowForce;
    }
}
