using UnityEngine;

/**
 * Allows the player to rotate the camera via a joystick.
 */
public class PlayerLook : MonoBehaviour {
  public Transform cameraTransform;
  public FixedJoystick joystickAim;
  
  public float horizontalRotationSpeed = 1f;
  public float verticalRotationSpeed = 1f;

  // Cached for re-use; avoids allocating new variables every update.
  private float newVerticalAngle;
  private bool lookingUp;

  void Update() {
    rotateHorizontal(joystickAim.Horizontal);
    rotateVertical(joystickAim.Vertical);
  }

  private void rotateHorizontal(float horizontalInput) {
    transform.Rotate(xAngle: 0,
                     yAngle: horizontalInput * horizontalRotationSpeed,
                     zAngle: 0);
  }

  private void rotateVertical(float verticalInput) {
    newVerticalAngle = cameraTransform.localRotation.eulerAngles.x +
                    (verticalInput * verticalRotationSpeed);

    // Need to prevent camera from looking beyond straight up or straight down.
    // 0 = straight up, 90 = straight down, 270 (ie -90) = straight up.
    lookingUp = newVerticalAngle > 180;
    if (lookingUp) {
      // If vertical rotation is beyond straight up, clamp it at straight up.
      if (newVerticalAngle < 270) {
        cameraTransform.localRotation = Quaternion.Euler(270, 0, 0);
        return;
      }
    }
    else {
      // If vertical rotation is beyond straight down, clamp it at straight down.
      if (newVerticalAngle > 90) {
        cameraTransform.localRotation = Quaternion.Euler(90, 0, 0);
        return;
      }
    }

    // If vertical rotation is valid, then assign it.
    cameraTransform.localRotation = Quaternion.Euler(newVerticalAngle, 0, 0);
  }
}
