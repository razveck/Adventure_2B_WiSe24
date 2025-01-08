using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

	#region private variables

	private InputAction moveAction;
	private InputAction lookAction;
	private InputAction interactAction;

	private float cameraXRotation;
	private Vector3 velocity;

	private Interactable currentInteractable;

	#endregion


	#region public variables

	public PlayerInput playerInputActions;
	public float speed = 10f;
	public float turnSpeed = 10f;
	public float gravity = -9.8f;
	public float mouseSensitivity = 1f;
	public bool invertMouseX;
	public bool invertMouseY;
	public CharacterController controller;
	public Transform cameraPivotX;
	public Transform cameraPivotY;
	public Animator animator;
	public FootstepsPlayer footsteps;



	#endregion


	// Start is called before the first frame update
	void Start() {
		moveAction = playerInputActions.currentActionMap.FindAction("Movement");
		lookAction = playerInputActions.currentActionMap.FindAction("Look");
		interactAction = playerInputActions.currentActionMap.FindAction("Interact");
	}

	// Update is called once per frame
	void Update() {
		//&& = UND
		if(interactAction.WasPressedThisFrame() && currentInteractable != null) {
			currentInteractable.OnInteract();
		}


		//y = WS, x = AD
		Vector2 moveInput = moveAction.ReadValue<Vector2>();

		Vector3 moveAmount = transform.right * moveInput.x + transform.forward * moveInput.y;
		moveAmount *= speed * Time.deltaTime;

		velocity.x = moveAmount.x;
		velocity.y += gravity * Time.deltaTime * Time.deltaTime;
		velocity.z = moveAmount.z;


		if(controller.Move(velocity) == CollisionFlags.Below) {
			velocity.y = 0f;
		}


		Vector2 lookInput = lookAction.ReadValue<Vector2>() * Time.timeScale;

		if(invertMouseX)
			lookInput.x *= -1;
		if(invertMouseY)
			lookInput.y *= -1;

		lookInput *= mouseSensitivity;
		
		//camera rotation
		cameraXRotation -= lookInput.y;
		cameraXRotation = Mathf.Clamp(cameraXRotation, 0, 80);
		cameraPivotX.eulerAngles = new Vector3(cameraXRotation, cameraPivotX.eulerAngles.y, cameraPivotX.eulerAngles.z);

		cameraPivotY.Rotate(0f, lookInput.x, 0f, Space.World);

		if(moveInput != Vector2.zero) {
			//camera ist ein Child, wird also mitgedreht. Vorher die Richtung abspeichern
			Vector3 cameraForward = cameraPivotY.forward;

			transform.forward = Vector3.Lerp(transform.forward, cameraPivotY.forward, turnSpeed * Time.deltaTime);

			//Richtung nochmal zurücksetzen
			cameraPivotY.forward = cameraForward;
		}

		//camera ist kein Child vom Player
		//cameraPivotY.position = transform.position;

		animator.SetFloat("speed", moveInput.y);



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

	void OnTriggerEnter(Collider other) {
		//variabel mit Typ "Interactable"
		//beim "other" wird nach einem Interactable component gesucht und das Ergebnis gespeichert
		Interactable interactable = other.GetComponent<Interactable>();

		//wenn "other" kein Interactable component hat, wird "null" rausgegeben, und es muss geprüft werden
		if(interactable != null) {
			currentInteractable = interactable;
		}

		if(other.CompareTag("Concrete") || other.CompareTag("Forest") || other.CompareTag("Leaves"))
			footsteps.ChangeUntergrund(other.tag);
	}

	void OnTriggerExit(Collider other) {
		Interactable interactable = other.GetComponent<Interactable>();
		if(interactable == currentInteractable) {
			currentInteractable = null;
		}

		if(other.CompareTag("Concrete") || other.CompareTag("Forest"))
			footsteps.ChangeUntergrund("Leaves");
	}

}
