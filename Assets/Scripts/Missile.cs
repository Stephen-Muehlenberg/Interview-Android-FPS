using UnityEngine;

/**
 * Flies towards a specified target, or forwards if no target is specified.
 * When it hits an object, it sends a damage message to it. Missiles cannot
 * collide with the player or other missiles.
 */
public class Missile : MonoBehaviour {
  private const float ACCELERATION = 12f;
  private const float ROTATION = 5f;

  public Rigidbody rb;

  private float lifetimeRemaining = 5;
  private Transform target;

  /**
   * Missile will fly towards the target until it hits something or it times out.
   * Missiles do not collide with the player or each other, only enemies and the 
   * environment.
   */
  public void setTarget(Transform target) {
    this.target = target;
  }

  private void Awake() {
    rb.velocity = transform.forward * ACCELERATION;
  }

  void FixedUpdate() {
    if (target != null) {
      // Rotate towards the target.
      Vector3 direction = target.position - transform.position;
      Vector3 rotationAmount = Vector3.Cross(direction.normalized, transform.forward);
      rb.angularVelocity = -rotationAmount * ROTATION;

      // Update movement direction based on current facing.
      rb.velocity = transform.forward * ACCELERATION;
    }
  }

  void Update() {
    // Self destruct if the missile survives too long. 
    // Safety feature to prevent hundreds of missiles from accidentally hanging around.
    lifetimeRemaining -= Time.deltaTime;
    if (lifetimeRemaining <= 0) Destroy(this.gameObject);
  }

  private void OnCollisionEnter(Collision collision) {
    collision.collider.SendMessageUpwards("takeDamage", 1, SendMessageOptions.DontRequireReceiver);
    Destroy(this.gameObject);
  }
}
