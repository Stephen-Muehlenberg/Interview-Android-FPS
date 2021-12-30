using System.Collections.Generic;
using UnityEngine;

/**
 * Maintains a publically visible list of all Targetable entities
 * within the player's lock-on bounds. This list can be consumed
 * by other classes for e.g. displaying lock on icons or choosing
 * which target to fire at.
 */
public class PlayerLockOn : MonoBehaviour {
  // Public read-only access.
  public static List<Transform> targets { get { return _targets; } }
  private static List<Transform> _targets = new List<Transform>();

  // Ideally the lock-on bounds image's anchors would be set based
  // on these values. But for a demo, hardcoding is fine.
  private const float VIEWPORT_BOUND_LEFT = 0.25f;
  private const float VIEWPORT_BOUND_RIGHT = 0.75f;
  private const float VIEWPORT_BOUND_TOP = 0.75f;
  private const float VIEWPORT_BOUND_BOTTOM = 0.25f;

  public new Camera camera;
  public LockOnCanvas lockOnCanvas;

  // Cached for re-use; avoids allocating a new variable every update.
  private Vector3 viewportPoint;

  void Update() {
    _targets.Clear();

    foreach (Transform target in TargetManager.targets) {
      viewportPoint = camera.WorldToViewportPoint(target.position);

      // If z is negative, then the target is behind the camera.
      if (viewportPoint.z < 0) continue;

      // If target is outside of the lock-on zone, ignore it.
      if (viewportPoint.x < VIEWPORT_BOUND_LEFT ||
          viewportPoint.x > VIEWPORT_BOUND_RIGHT ||
          viewportPoint.y > VIEWPORT_BOUND_TOP ||
          viewportPoint.y < VIEWPORT_BOUND_BOTTOM) continue;

      _targets.Add(target);
    }
  }
}
