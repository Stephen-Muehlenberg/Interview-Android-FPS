using UnityEngine;

/**
 * Periodically damages the player if close enough.
 */
public class AttackIfInRange : MonoBehaviour {
  private const float ATTACK_RANGE_SQ = 2.25f;
  private const float ATTACK_COOLDOWN = 1.5f;

  private Transform player;
  private float cooldown = 0;

  void Start() {
    player = GameObject.Find("Player").transform; // Lazy, but good enough for a demo.
  }

  void Update() {
    if (cooldown > 0) {
      cooldown -= Time.deltaTime;
    }
    else if ((player.position - transform.position).sqrMagnitude <= ATTACK_RANGE_SQ) {
      cooldown = ATTACK_COOLDOWN;
      player.SendMessage("takeDamage", 1);
    }
  }
}
