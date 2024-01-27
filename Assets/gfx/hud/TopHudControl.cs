using UnityEngine;
using UnityEngine.UI;


public class TopHudControl : MonoBehaviour
{
    [SerializeField]
     private Slider speedSlider;
    [SerializeField]
     private Slider boostSlider;


    public void UpdateBoostLevel(float boostLevel, float maxBoost)
    {
        boostSlider.value = boostLevel;
        boostSlider.maxValue = maxBoost;
    }

    public void UpdateSpeedLevel(float currentSpeed, float maxSpeed)
    {
        speedSlider.value = currentSpeed;
        speedSlider.maxValue = maxSpeed;
    }
}
