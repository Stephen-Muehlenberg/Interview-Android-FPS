using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * Controls the overall state of the game, and handles a few simple triggers
 * and housekeeping.
 */
public class SceneManager : MonoBehaviour {
  private enum SceneState {
    INITIAL_STATE,
    FIGHT_1,
    INTERMISSION,
    FIGHT_2,
    AFTERMATH,
    VICTORY,
    DEFEAT
  }

  public Transform player;
  public Transform door1;
  public Transform door2;
  public Transform door3;
  public Text gameOverText;
  private SceneState state = SceneState.INITIAL_STATE;

  void Start() {
    Screen.orientation = ScreenOrientation.LandscapeLeft;
    TargetManager.enemiesDestroyedCallback = onAllEnemiesDestroyed;
    openDoor(door1);
  }

  void Update() {
    // Lazy switch. Ideally, each state should have its own update method, and we just
    // call state.update();
    if (state == SceneState.INITIAL_STATE) {
      // Once player enters arena 1, begin fight.
      if (player.position.x < 18) enterFight1();
    }
    else if (state == SceneState.INTERMISSION) {
      // Once player enters arena 2, begin fight.
      if (player.position.x < -32) enterFight2();
    }
    else if (state == SceneState.AFTERMATH) {
      if (player.position.x < -80) enterVictory();
    }
  }

  private void onAllEnemiesDestroyed() {
    if (state == SceneState.FIGHT_1) enterIntermission();
    else enterAftermath();
  }

  private void enterFight1() {
    closeDoor(door1);
    spawnEnemies(
      // Random, arbitrary spawn points. Could be chosen dynamically. Hardcoded for simplicity.
      new Vector3(-13, 0, 14),
      new Vector3(-8, 0, 8),
      new Vector3(-1, 0, -11),
      new Vector3(-15, 0, 12),
      new Vector3(-18, 0, -5));
    state = SceneState.FIGHT_1;
  }

  private void enterIntermission() {
    openDoor(door2);
    state = SceneState.INTERMISSION;
  }

  private void enterFight2() {
    closeDoor(door2);
    spawnEnemies(
      // Random, arbitrary spawn points. Could be chosen dynamically. Hardcoded for simplicity.
      new Vector3(-53, 0, 21),
      new Vector3(-62, 0, 7),
      new Vector3(-71, 0, -10),
      new Vector3(-65, 0, 2),
      new Vector3(-52, 0, -5));
    state = SceneState.FIGHT_2;
  }

  private void enterAftermath() {
    openDoor(door3);
    state = SceneState.AFTERMATH;
  }

  private void enterVictory() {
    gameOverText.enabled = true;
    gameOverText.text = "Victory!";
    state = SceneState.VICTORY;

    StartCoroutine(waitThenQuit());
  }

  public void enterDefeat() {
    gameOverText.enabled = true;
    gameOverText.text = "You Died";
  //  mainUiCanvas.SetActive(false);
  //  lockOnCanvas.SetActive(false);
    Time.timeScale = 0; // Pause game
    state = SceneState.DEFEAT;
  }

  private void openDoor(Transform door) {
    door.transform.position += Vector3.up * 4;
  }

  private void closeDoor(Transform door) {
    door.transform.position -= Vector3.up * 4;
  }

  private void spawnEnemies(params Vector3[] spawnPositions) {
    // This loaded resource could be cached if we were spawning enemies frequently,
    // but this is fine for now.
    GameObject enemyPrefab = Resources.Load<GameObject>("Enemy");

    foreach (Vector3 spawnPosition in spawnPositions) {
      GameObject.Instantiate(original: enemyPrefab,
                             position: spawnPosition,
                             rotation: Quaternion.identity);
    }
  }

  private IEnumerator waitThenQuit() {
    yield return new WaitForSeconds(2);
    Application.Quit();
  }
}
