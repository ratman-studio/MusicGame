using UnityEngine;
using UnityEngine.UI;


public class TopHudControl : MonoBehaviour
{
    [SerializeField]
     private Slider speedSlider;
    [SerializeField]
     private Slider boostSlider;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBoostLevel(float boostLevel, float maxBoost)
    {
        boostSlider.maxValue = maxBoost;
        boostSlider.value = boostLevel;
    }

    public void UpdateSpeedLevel(float boostLevel, float maxBoost)
    {
        boostSlider.maxValue = maxBoost;
        boostSlider.value = boostLevel;
    }
}
