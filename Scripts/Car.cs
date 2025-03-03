using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    //--------------------------
    public bool isKamyon = false;
    public Camera oyuncuCam;
    public Camera cam1;
    public Camera cam2;
    //--------------------------

    [SerializeField] public float motorForce;
    [SerializeField] public float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        if (!FirstPersonLook.car_driving_mode && !isKamyon) {
            currentbreakForce = 5000f;
            isBreaking = true;
        }
    }



    private void GetInput()
    {
        if (FirstPersonLook.car_driving_mode && !isKamyon) {
            horizontalInput = Input.GetAxis(HORIZONTAL);
            verticalInput = Input.GetAxis(VERTICAL);
            isBreaking = Input.GetKey(KeyCode.Space);
        }

        
    }

    private void Start() {
        if (PlayerPrefs.GetInt("simüle") == 1 && isKamyon) {
            cam1.gameObject.SetActive(true);
            StartCoroutine(SimuleEt());
            PlayerPrefs.SetInt("simüle", 0);
        } else if (isKamyon) {
            gameObject.SetActive(false);
        }
    }

    IEnumerator SimuleEt()
    {
        gameObject.SetActive(true);
        oyuncuCam.gameObject.SetActive(false);
        isBreaking = false;
        transform.position = new Vector3(-139.240005f, 0f, -38.2299995f);
        transform.rotation = new Quaternion(0f, 0.716735244f, 0f, 0.697345376f);

        // Düz git
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            verticalInput = 1f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Frenle
        elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            isBreaking = true;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Sola dön
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        isBreaking = false;
        elapsedTime = 0f;
        while (elapsedTime < 3.9f)
        {
            verticalInput = 1f;
            horizontalInput = -1f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Düz git
        elapsedTime = 0f;
        while (elapsedTime < 3.2f)
        {
            horizontalInput = 0f;
            verticalInput = 1f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isBreaking = true;

        elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            verticalInput = 0f;
            horizontalInput = 0f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        oyuncuCam.gameObject.SetActive(true);
        cam2.gameObject.SetActive(false);

        transform.position = new Vector3(-139.240005f, 0f, -38.2299995f);
        gameObject.SetActive(false);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}