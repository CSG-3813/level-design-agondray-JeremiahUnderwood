using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    private const float _threshold = 0.01f;

    private float verticalVelocity = 0f;

    public float Gravity = -9.81f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    public float speed = 10f;
    public float accel = 1f;
    public float rotateSpeed = 10f;
    public float rotAccel = 1f;

    private float currentSpeed = 0;
    private float currentRotSpeed = 0; 

    private CharacterController _controller;

    private StarterAssets.StarterAssetsInputs _input;
    private GameObject _mainCamera;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    public float health = 100;

    void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        _controller = GetComponent<CharacterController>();
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float targetSpeed = speed * _input.move.y;
        if (currentSpeed < targetSpeed)
        {
            currentSpeed += accel * Time.deltaTime;
            if (currentSpeed > targetSpeed) currentSpeed = targetSpeed;
        }
        else if (currentSpeed > targetSpeed)
        {
            currentSpeed -= accel * Time.deltaTime;
            if (currentSpeed < targetSpeed) currentSpeed = targetSpeed;
        }


        float targetRotSpeed = rotateSpeed * _input.move.x;
        if (currentRotSpeed < targetRotSpeed)
        {
            currentRotSpeed += rotAccel * Time.deltaTime;
            if (currentRotSpeed > targetRotSpeed) currentRotSpeed = targetRotSpeed;
        }
        else if (currentRotSpeed > targetRotSpeed)
        {
            currentRotSpeed -= rotAccel * Time.deltaTime;
            if (currentRotSpeed < targetRotSpeed) currentRotSpeed = targetRotSpeed;
        }

        GroundedCheck();
        if (Grounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += Gravity * Time.deltaTime;
            if (verticalVelocity < -30f) verticalVelocity = -30f;
        }

        _controller.Move(this.transform.forward * currentSpeed * Time.deltaTime + new Vector3(0f, verticalVelocity, 0f));
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + currentRotSpeed, this.transform.rotation.eulerAngles.z), rotateSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = true ? 1.0f : Time.deltaTime; //replacing a boolean because I'm lazy

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }
}
