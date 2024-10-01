using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region private variables

    private InputAction moveAction;
    private InputAction lookAction;
        
    private float cameraXRotation;
    
    #endregion
    
    
    #region public variables
    
    public PlayerInput playerInputActions;
    public float speed = 10f;
    public float mouseSensitivity = 1f;
    public CharacterController controller;
    public Transform camera;
        
    #endregion

    
    // Start is called before the first frame update
    void Start()
    {
        moveAction = playerInputActions.currentActionMap.FindAction("Movement");
        lookAction = playerInputActions.currentActionMap.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        //y = WS, x = AD
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 moveAmount = transform.right * moveInput.x + transform.forward * moveInput.y;
        moveAmount *= speed * Time.deltaTime;
        
        
        controller.Move(moveAmount);
        
        
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        
        //camera rotation
        cameraXRotation -= lookInput.y * mouseSensitivity;
        cameraXRotation = Mathf.Clamp(cameraXRotation, 0, 80);
        camera.eulerAngles = new Vector3(cameraXRotation, camera.eulerAngles.y, camera.eulerAngles.z);
        
        //player rotation
        transform.Rotate(0f, lookInput.x, 0f);
        
        
        
        
        
        
        
        
        //Kamera position aus der Rotation berechnen
        //x = cos
        //y = sin
        //Winkel muss in Radiant umgewandelt werden
        //float angle = cameraXRotation * Mathf.Deg2Rad;
        //float z = -Mathf.Cos(angle);
        //float y = Mathf.Sin(angle);
        //camera.localPosition = new Vector3(camera.localPosition.x, y, z) * 10f;

        //wenn Kamera kein child vom Player ist        
        //camera.position = transform.position + new Vector3(camera.position.x, y, z);
        
        //camera.Rotate(lookInput.y, 0f, 0f, Space.World);
        
    }
}
