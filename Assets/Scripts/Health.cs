using UnityEngine;

/**
 * Tracks health, sending a "die" message when it runs out.
 */
public class Health : MonoBehaviour {
  public int max = 5;

  // Read-only public access.
  public int remaining { get { return _remaining; } }
  private int _remaining;

  void Start()  {
    _remaining = max;
  }
  
  public void takeDamage(int amount) {
    _remaining -= amount;
    if (_remaining <= 0) this.SendMessage("die");
  }
}
