using UnityEngine;

public class GameOverOnDeath : MonoBehaviour {
  public SceneManager sceneManager;

  public void die() {
    sceneManager.enterDefeat();
  }
}
