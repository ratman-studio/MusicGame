using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterShitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ShitPrefab;
    // Start is called before the first frame update
    private GroundedCharacterController _characterController;

    void Start()
    {
        _characterController = FindObjectOfType<GroundedCharacterController>();
         _characterController.OnShit += () =>
         {
             var clone = Instantiate(ShitPrefab, transform.position, Quaternion.identity,transform.parent);
             var random = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
             clone.GetComponent<Rigidbody>()?.AddForce((Vector3.down + random) * 2f,ForceMode.Impulse);
             DOVirtual.DelayedCall(5, () => Destroy(clone));
         };
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
