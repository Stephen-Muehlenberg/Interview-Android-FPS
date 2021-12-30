using UnityEngine;

/**
 * Smoothly moves the object up and down at random.
 * Just adds a bit of motion. Good for making a target slightly harder to hit.
 */
public class VerticleWobble : MonoBehaviour {
  public float baseHeight = 2;
  public float randomHeightVariation = 1.5f;
  public float randomHeightSpeed = 0.5f;

  private float noiseSeed;

  private void Start() {
    noiseSeed = Random.Range(0f, 100f); // Generate a random seed value for vertical noise.
  }

  void Update() {
    transform.position = new Vector3(transform.position.x,
                                     baseHeight + (Mathf.PerlinNoise(noiseSeed, Time.time * randomHeightSpeed) * 2 * randomHeightVariation) - randomHeightVariation,
                                     transform.position.z);
  }
}
