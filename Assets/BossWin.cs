using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossWin : MonoBehaviour
{
  [SerializeField]
  private UnityEvent onBossWin;

  private void OnTriggerEnter(Collider other)
  {
    if (isPlayer(other)) onBossWin?.Invoke();
  }

  private bool isPlayer(Collider other)
  {
      return other.gameObject.layer == LayerMask.NameToLayer("Player");
  }
}
