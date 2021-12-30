using UnityEngine;
using UnityEngine.UI;

/**
 * Draws locked-on icons over the targets defined in PlayerLockOn.
 */
public class LockOnCanvas : MonoBehaviour {
  public new Camera camera;
  public Image[] lockOnIcons;

  private void Update() {
    for (int i = 0, j = lockOnIcons.Length; i < j; i++) {
      if (PlayerLockOn.targets.Count <= i) {
        lockOnIcons[i].enabled = false;
      }
      else {
        lockOnIcons[i].enabled = true;
        lockOnIcons[i].transform.position = camera.WorldToScreenPoint(PlayerLockOn.targets[i].position);
      }
    }
  }
}
