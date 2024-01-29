using FMODUnity;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private GroundedCharacterController _characterController;

    private void OnEnable() => InSceneLevelSwitcher.OnLevelStart += Reset;

    private void OnDisable() => InSceneLevelSwitcher.OnLevelStart -= Reset;

    public void Reset()
    {
        SetMistake(1f);
        SetPlayerSpeed(0f);
        pitchCureent = 0f;
        SetCollectiblePitch(0f);
    }

    private static MusicChanger _musicChanger;
    public static MusicChanger Get()
    {
        if (_musicChanger == null)
        {
            _musicChanger = FindObjectOfType<MusicChanger>();
            if (_musicChanger == null)
                return null;
        }
        return _musicChanger;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePitch();
    }


    public void SetMistake(float f)
    {
        var mistake = 0f;
        if (f > .3)
            mistake = .5f;
        if (f > .6)
            mistake = 1f;
        RuntimeManager.StudioSystem.setParameterByName("Mistake", mistake);
    }

    public void SetPlayerSpeed(float f)
    {
        var speed = 0f;
        if (f > .1)
            speed = .2f;
        if (f > .3)
            speed = .4f;
        if (f > .5)
            speed = .6f;
        if (f > .7)
            speed = .8f;
        if (f > .8)
            speed = 1f;
        RuntimeManager.StudioSystem.setParameterByName("Player Speed", speed);
    }

    private float pitchCureent = 0;
    private float pitchMaxValue = 1;
    private float pitchDecreasRatio = 1;
    private float pitchIncreaseStep = .1f;

    public void AddCollectiblePitch()
    {
        pitchCureent += pitchIncreaseStep;
        SetCollectiblePitch(pitchCureent);
    }

    private void UpdatePitch()
    {
        var pitchNew = pitchCureent - Time.deltaTime * pitchDecreasRatio;
        pitchNew = ClampPitch(pitchNew);
        if (pitchNew != pitchCureent)
        {
            pitchCureent = pitchNew;
            SetCollectiblePitch(pitchCureent);
        }
    }

    private float ClampPitch(float f) => Mathf.Clamp(f, 0, pitchMaxValue);

    private void SetCollectiblePitch(float f)
    {
        var clampPitch = ClampPitch(f);
        RuntimeManager.StudioSystem.setParameterByName("Collecting Speed", clampPitch);
    }

}