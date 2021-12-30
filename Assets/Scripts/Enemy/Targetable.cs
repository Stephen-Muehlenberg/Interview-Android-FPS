using UnityEngine;

/**
 * Registers the [target] Transform with the TargetManager, allowing
 * e.g. missiles to lock on to it.
 * Unregisters and self-destructs on death.
 */
public class Targetable : MonoBehaviour {
  public Transform target;

  void Start() {
    TargetManager.add(target);
  }

  void die() {
    TargetManager.remove(target);
    Destroy(this.gameObject);
  }
}
