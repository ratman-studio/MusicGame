using System;
using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//When the player enters, respawn them
//--------------------------------------------------------------------
public class DeathTrigger : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _renderer;

    private void OnEnable() => _renderer?.gameObject.SetActive(false);


    void OnTriggerEnter(Collider a_Collider)
    {
        ControlledCapsuleCollider controlledCapsuleCollider = a_Collider.GetComponent<ControlledCapsuleCollider>();
        if (controlledCapsuleCollider != null)
        {
            //Prevent death state to be used if the collider is no-clipping
            if (controlledCapsuleCollider.AreCollisionsActive())
            { 
                Debug.Log("Death triggered by: " + transform.name);
                if (InSceneLevelSwitcher.Get())
                {
                    InSceneLevelSwitcher.Get().Respawn();
                }
            }
        }
    }
}
