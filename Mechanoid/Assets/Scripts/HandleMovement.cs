using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMovement : MonoBehaviour
{
    public Camera cam;
    public GameObject mainMesh;
    public GameObject battleStation;
    public Transform distanceCheck;
    public Transform groundCheck;
    public ParticleSystem burn;
    public LayerMask groundMask;

    private int forceMultiplier = 16;
    private float turnSmoothVelocity;
    private float speed = 6f;
    private float turnSmoothTime = 0.1f;
    private float jumpHeight = 15f;
    private float gravity = -20f;
    private float groundDistance = 0.4f;

    private bool isGrounded;
    private bool playAnimation;

    private Animation animationController;
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animationController = mainMesh.GetComponent<Animation>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        string collisionVector = checkForCollision();
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        speed = 6f;

        battleStation.transform.localRotation = Quaternion.identity;
        playAnimation = true;

        if (collisionVector == "forward")
        {
            Vector3 normalVector = transform.rotation * -Vector3.forward;
            velocity = new Vector3(forceMultiplier * normalVector.x, velocity.y, forceMultiplier * normalVector.z);
        }
        else if(collisionVector == "right")
        {
            Vector3 normalVector = transform.rotation * -Vector3.right;
            velocity = new Vector3(forceMultiplier * normalVector.x, velocity.y, forceMultiplier * normalVector.z);
        }
        else if (collisionVector == "left")
        {
            Vector3 normalVector = transform.rotation * Vector3.right;
            velocity = new Vector3(forceMultiplier * normalVector.x, velocity.y, forceMultiplier * normalVector.z);
        }
        else
        {
            velocity = new Vector3(0f, velocity.y, 0f);
        }

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            velocity.z += 0.1f;
            battleStation.transform.localRotation = Quaternion.Euler(new Vector3(10f, 0f, 0f));
            playAnimation = false;
        }

        if (Input.GetKey(KeyCode.E))
        {
            battleStation.transform.localRotation = Quaternion.Euler(new Vector3(-10f, 0f, 0f));
            playAnimation = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(DelayedAnimation("Jump"));
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            burn.Play();
        } 
        else if(direction.magnitude >= 0.1f)
        {
            moveRobot(direction);

            if (isGrounded)
            {
                animationController.Play("Walking_Cycle");
            }
        }
        else
        {
            if (isGrounded && playAnimation)
            {
                animationController.Play("Idle");
            }
        }

    }

    void moveRobot(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    string checkForCollision()
    {
        int maxDistance = 4;
        Ray forwardRay = new Ray(distanceCheck.position, distanceCheck.forward);
        Ray backwardRay = new Ray(distanceCheck.position, -1f * distanceCheck.forward);
        Ray rightRay = new Ray(distanceCheck.position, distanceCheck.right);
        Ray leftRay = new Ray(distanceCheck.position, -1f * distanceCheck.right);
        RaycastHit hit;

        if(Physics.Raycast(forwardRay, out hit, maxDistance))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "forward";
        }
        else if (Physics.Raycast(backwardRay, out hit, maxDistance))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "backward";
        }
        else if (Physics.Raycast(rightRay, out hit, maxDistance))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "right";
        }
        else if (Physics.Raycast(leftRay, out hit, maxDistance))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "left";
        }

        return "none";
    }

    IEnumerator DelayedAnimation(string animationName)
    {
        yield return new WaitForSeconds(0.05f);
        animationController.Play(animationName);
    }

}