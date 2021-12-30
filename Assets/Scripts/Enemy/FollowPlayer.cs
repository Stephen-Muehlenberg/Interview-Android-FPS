using UnityEngine;
using UnityEngine.AI;

/**
 * Causes this object to navigate towards the player.
 */
[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour {
  public NavMeshAgent navAgent;
  private Transform player;

  void Start() {
    player = GameObject.Find("Player").transform;      
  }

  void Update() {
    navAgent.SetDestination(player.position);
  }
}
