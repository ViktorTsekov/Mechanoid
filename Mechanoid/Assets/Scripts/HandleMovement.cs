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

    private KeyCode jump;
    private KeyCode sprint;
    private KeyCode unlockCursor;
    private KeyCode tiltTowerUp;
    private KeyCode tiltTowerDown;

    private AudioSource walkingSfx;
    private AudioSource boosterSfx;

    private GameObject controlsManager;
    private GameObject soundManager;
    private Animation animationController;
    private CharacterController controller;
    private Vector3 velocity;

    void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        controlsManager = GameObject.FindGameObjectWithTag("ControlsManager");

        jump = controlsManager.GetComponent<ControlsManager>().getKey("jump");
        sprint = controlsManager.GetComponent<ControlsManager>().getKey("sprint");
        unlockCursor = controlsManager.GetComponent<ControlsManager>().getKey("unlockCursor");
        tiltTowerUp = controlsManager.GetComponent<ControlsManager>().getKey("tiltTowerUp");
        tiltTowerDown = controlsManager.GetComponent<ControlsManager>().getKey("tiltTowerDown");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animationController = mainMesh.GetComponent<Animation>();
        controller = gameObject.GetComponent<CharacterController>();

        walkingSfx = soundManager.GetComponent<SoundManager>().getTrack("robotMovementSfx");
        boosterSfx = soundManager.GetComponent<SoundManager>().getTrack("rocketBoostSfx");
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

        if (collisionVector == "up")
        {
            velocity.y = -1f;
        }
        else if (collisionVector == "forward")
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

        if (Input.GetKeyDown(unlockCursor))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(lockCursor(0.2f));
        }

        if (Input.GetKey(sprint))
        {
            speed *= 2;
        }

        if (Input.GetKey(tiltTowerUp))
        {
            battleStation.transform.localRotation = Quaternion.Euler(new Vector3(10f, 0f, 0f));
            playAnimation = false;
        }

        if (Input.GetKey(tiltTowerDown))
        {
            battleStation.transform.localRotation = Quaternion.Euler(new Vector3(-10f, 0f, 0f));
            playAnimation = false;
        }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            StartCoroutine(DelayedAnimation("Jump"));
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            burn.Play();

            if (!boosterSfx.isPlaying)
            {
                boosterSfx.Play();
            }
        } 
        else if(direction.magnitude >= 0.1f)
        {
            move(direction);

            if (isGrounded)
            {
                animationController.Play("Walk");

                if(!walkingSfx.isPlaying)
                {
                    walkingSfx.Play();
                }
            }
        }
        else
        {
            rotate(direction);

            if (isGrounded && playAnimation)
            {
                animationController.Play("Idle");
            }
        }

    }

    void move(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    void rotate(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    string checkForCollision()
    {
        Ray upRay = new Ray(distanceCheck.position, distanceCheck.up);
        Ray forwardRay = new Ray(distanceCheck.position, distanceCheck.forward);
        Ray backwardRay = new Ray(distanceCheck.position, -1f * distanceCheck.forward);
        Ray rightRay = new Ray(distanceCheck.position, distanceCheck.right);
        Ray leftRay = new Ray(distanceCheck.position, -1f * distanceCheck.right);
        RaycastHit hit;

        if (Physics.Raycast(upRay, out hit, 3))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "up";
        }
        else if (Physics.Raycast(forwardRay, out hit, 4))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "forward";
        }
        else if (Physics.Raycast(backwardRay, out hit, 3))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "backward";
        }
        else if (Physics.Raycast(rightRay, out hit, 3))
        {
            return hit.transform.gameObject.tag == "Projectile" ? "none" : "right";
        }
        else if (Physics.Raycast(leftRay, out hit, 3))
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

    private IEnumerator lockCursor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Cursor.lockState = CursorLockMode.Locked;
    }

}