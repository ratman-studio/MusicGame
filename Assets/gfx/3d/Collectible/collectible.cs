using UnityEngine;

public class collectible : MonoBehaviour
{
    [SerializeField] private float BonusPoints = 5;
    [SerializeField] private MeshRenderer _renderer;


    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        InSceneLevelSwitcher.Get().AddBonus(BonusPoints);
    }

}