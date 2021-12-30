using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * Allows the player to fire missiles.
 * 
 * Multiple missiles are fired in quick succession, but there's a cooldown
 * after firing. If the player is looking at a valid target when the missile
 * is fired, the missile will lock onto it and fly towards it.
 */
public class PlayerShoot : MonoBehaviour {
  private const int MISSILES_PER_SHOT = 6;
  private const float DELAY_BETWEEN_MISSILES = 0.1f;
  private const float COOLDOWN = 2.5f;
  private const string LOADING_MESSAGE = "Loading...";
  private const string READY_MESSAGE = "Fire!";

  public Camera mainCamera;
  public GameObject missilePrefab;
  public Transform[] missileSpawnPoints;
  public Image[] reloadFills;
  public Text[] fireButtonTexts;

  private float cooldownRemaining = 0;
  private GameObject missile;
  private int nextMissileSpawnPoint = 0;
  private int nextLockOnTarget = 0;

  public void Start() {
    foreach (Text text in fireButtonTexts) text.text = READY_MESSAGE;
  }

  public void Update() {
    if (cooldownRemaining == 0) return;

    cooldownRemaining -= Time.deltaTime;
    if (cooldownRemaining <= 0) {
      cooldownRemaining = 0;
      foreach (Text text in fireButtonTexts) text.text = READY_MESSAGE;
    }

    foreach (Image reloadFill in reloadFills) {
      // If the calculation were more complicated, or there were more fills expected,
      // we might cache the result of this equation. This is fine for now.
      reloadFill.fillAmount = 1 - (cooldownRemaining / COOLDOWN);
    }
  }

  public void pressFire() {
    if (cooldownRemaining > 0) return;

    foreach (Text text in fireButtonTexts) text.text = LOADING_MESSAGE;
    StartCoroutine(fireMissiles(MISSILES_PER_SHOT));
    cooldownRemaining = COOLDOWN;
  }

  private IEnumerator fireMissiles(int missileCount) {
    Quaternion forward = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles.x,
                                          transform.rotation.eulerAngles.y,
                                          0);

    nextLockOnTarget = 0;
    for (int i = 0; i < missileCount; i++) {
      // Spawn missile.
      // We could use an object pool here, since we'll be firing a lot of them, but this
      // is fine for a demo.
      missile = Instantiate(
        original: missilePrefab,
        position: missileSpawnPoints[nextMissileSpawnPoint].position,
        rotation: forward);

      // Set the missile's target. Loop over the targets between missiles.
      if (PlayerLockOn.targets.Count > 0) {
        if (nextLockOnTarget >= PlayerLockOn.targets.Count) nextLockOnTarget = 0;
        missile.GetComponent<Missile>().setTarget(PlayerLockOn.targets[nextLockOnTarget]);
        nextLockOnTarget++;
      }

      // Set the next missile spawn point.
      nextMissileSpawnPoint++;
      if (nextMissileSpawnPoint >= missileSpawnPoints.Length) nextMissileSpawnPoint = 0;

      // Wait for next missile
      yield return new WaitForSeconds(DELAY_BETWEEN_MISSILES);
    }
  }
}
