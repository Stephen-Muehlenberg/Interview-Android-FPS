using UnityEngine;

/**
 * Allows the player to move around via a joystick.
 */
public class PlayerMovement : MonoBehaviour {
  public CharacterController characterController;
  public FixedJoystick joystickMove;
  public float movementSpeed = 1f;

  // Cached for re-use; avoids allocating a new variable every update.
  private Vector3 moveDirection;

  void Update() {
    moveDirection = (transform.forward * joystickMove.Vertical) +
                    (transform.right * joystickMove.Horizontal);
    if (moveDirection.magnitude > 0) {
      characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }
  }
}
