using System;
using UnityEngine;

public class collectible : MonoBehaviour
{
    [SerializeField] private float BonusPoints = 5;
    [SerializeField] private float RotateSpeed = 2;
    [SerializeField] private MeshRenderer _renderer;

    private void OnEnable()
    {
        InSceneLevelSwitcher.OnLevelStart += () => Reset();
    }

    private void Reset()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        InSceneLevelSwitcher.Get().AddBonus(BonusPoints);
    }

    private void Update()
    {
        this.transform.RotateAround(Vector3.up, RotateSpeed * Time.deltaTime);
    }
}