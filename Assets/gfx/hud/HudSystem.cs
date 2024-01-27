using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudSystem : MonoBehaviour
{
    private static HudSystem hudSystem;

    [SerializeField]
    public TopHudControl topHud;

    public static HudSystem Get()
    {
        if (hudSystem == null)
        {
            hudSystem = FindObjectOfType<HudSystem>();
            if (hudSystem == null)
                return null;
        }
        return hudSystem;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
