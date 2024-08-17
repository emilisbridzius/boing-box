using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    Transform playerInputSpace;

    public OrbitCamera mainCam;
    public Transform model;
    public Transform centreCam, leftCam, rightCam;
    [Range(0f, 100f)]
    public float maxSpeed = 10f;
    [Range(0f, 100f)]
    public float maxAcceleration = 10f;

    bool canMove;
    bool isCrouched;

    Rigidbody body;
    Vector3 velocity, desiredVelocity;
    Coroutine evading;

    private void Awake()
    {
        mainCam.focus = centreCam;
        canMove = true;
        body = GetComponent<Rigidbody>();
        playerInputSpace = mainCam.transform;
    }

    public void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            velocity = body.velocity;
            float maxSpeedChange = maxAcceleration * Time.deltaTime;

            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

            body.velocity = velocity;
        }
    }

    private void HandleInputs()
    {
        #region Basic Movement - WASD
        Vector2 playerInput;

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        if (playerInputSpace)
        {
            Vector3 forward = playerInputSpace.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = playerInputSpace.right;
            right.y = 0f;
            right.Normalize();

            desiredVelocity = (forward * playerInput.y + right * playerInput.x) * maxSpeed;
        }
        #endregion

        #region Advanced Movement - WASD + Spacebar / Crouching
        if (Input.GetKey(KeyCode.Space) && evading == null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Forward lunge
                evading = StartCoroutine(DoEvade(model.forward));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //Backward evade
                evading = StartCoroutine(DoEvade(-model.forward));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //Left evade
                evading = StartCoroutine(DoEvade(-model.right));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                //Right evade
                evading = StartCoroutine(DoEvade(model.right));
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Crouch(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Crouch(false);
        }
        #endregion

        #region Cam Shoulder Switching - Left/Right/Centre
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchShoulderCam("left");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchShoulderCam("right");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchShoulderCam("centre");
        }
        #endregion
    }

    void Crouch(bool isCrouched)
    {
        if (isCrouched)
        {
            //Halve the height of the player's collider.
            float colliderSize = GetComponent<CapsuleCollider>().height;
            colliderSize = 1f;
            GetComponent<CapsuleCollider>().height = colliderSize;

            //Change the height of the player's model.
            model.localScale = new Vector3(0.5f, 1f, 0.5f);
        }
        else
        {
            //Change the height back to 2f.
            float colliderSize = GetComponent<CapsuleCollider>().height;
            colliderSize = 2f;
            GetComponent<CapsuleCollider>().height = colliderSize;

            //Change the height of the player's model back to 2f.
            model.localScale = new Vector3(0.5f, 2f, 0.5f);
        }
    }

    IEnumerator DoEvade(Vector3 evadeDirection)
    {
        //Simple coroutine to quickly lerp the player in a desired direction.
        float duration = 0.3f; //Time it takes to complete the evade.
        float startTime = Time.time;
        float distance = 1.2f; //Distance to move in the evade.
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (evadeDirection * distance);
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float lerpSpeed = Mathf.Lerp(1f, 3f, t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, lerpSpeed * t);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        body.velocity = velocity;
        evading = null;
    }

    void SwitchShoulderCam(string overShoulder)
    {
        if (overShoulder == "left")
        {
            mainCam.focus = leftCam;
        }
        else if (overShoulder == "right")
        {
            mainCam.focus = rightCam;
        }
        else if (overShoulder == "centre")
        {
            mainCam.focus = centreCam;
        }
    }
}
