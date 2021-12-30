using System.Collections.Generic;
using UnityEngine;

/**
 * Singleton class which maintains a list of Targetable Transforms.
 * As Targetable entities are spawned and destroyed, they must register 
 * and unregister themselves here.
 */
public class TargetManager {
  public delegate void AllTargetsDestroyedCallback();

  // Read-only public access.
  public static List<Transform> targets { get { return _targets; } }
  private static List<Transform> _targets = new List<Transform>();

  // Ideally this class would support an arbitrary number of simultaneous
  // listeners, but for this demo we know we only need one.
  public static AllTargetsDestroyedCallback enemiesDestroyedCallback;

  public static void add(Transform target) {
    _targets.Add(target);
  }

  public static void remove(Transform target) {
    _targets.Remove(target);
    if (targets.Count == 0) enemiesDestroyedCallback?.Invoke();
  }
}
