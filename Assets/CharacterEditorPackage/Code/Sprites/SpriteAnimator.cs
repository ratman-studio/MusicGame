using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimationOffset
{
    public string m_Name;
    public Vector3 m_Offset;
}
//--------------------------------------------------------------------
//The SpriteAnimator class controls the sprite animations in Unity's animator system
//It gets a sprite state from the current module or entity controller and displays the appropriate sprite
//It has additional settings for offsets and rotations
//The SpriteAnimator also interpolates sprite position for smooth movement because Unity's Update and FixedUpdate are not synched.
//--------------------------------------------------------------------
public class SpriteAnimator : MonoBehaviour {
    public enum SpriteInterpolation
    {
        None,
        Interpolate,
        Extrapolate
    }
    [SerializeField] protected Animator m_SpriteAnimator = null;
    [SerializeField] protected AnimationOffset[] m_AnimOffsets = null;
    [SerializeField] protected string m_Prefix = "";
    [SerializeField] protected Transform m_SpriteTransformHook = null;

    [SerializeField] protected CharacterControllerBase m_CharacterController = null;
    [SerializeField] protected ControlledCapsuleCollider m_CapsuleCollider = null;
    [SerializeField] protected float m_MoveSpeedThreshhold = 0.0f;
    [SerializeField] SpriteInterpolation m_SpriteInterpolation = SpriteInterpolation.None;
    [SerializeField] float m_InterpolationLimit = 2.0f;
    [SerializeField] protected bool m_BlockRotation = false;

    protected string m_CurrentAnimationName;
    int m_CurrentIndex;
    float m_CurrentSpeed;
    protected Vector2 m_LastGoodDirection;

    Vector3 m_LastFixedUpdatePosition;
    Vector3 m_CurrentFixedUpdatePosition;
    protected float m_LastZRot;
    protected float m_StartHeight;

    void Start()
    {
        //Make sure that we start at the right height, and that the parent is aligned to the lower demisphere
        //Prevents having to manually adjust both sprite and parent
        Vector3 originalPosition = transform.position;
        m_SpriteTransformHook.transform.position = m_CapsuleCollider.GetDownCenter();
        transform.position = originalPosition;
        m_StartHeight = transform.localPosition.y;

    }
    void FixedUpdate()
    {
        m_LastFixedUpdatePosition = m_CurrentFixedUpdatePosition;
        m_CurrentFixedUpdatePosition = m_CapsuleCollider.GetCapsuleTransform().GetPosition();

        float difference = (m_CurrentFixedUpdatePosition - m_LastFixedUpdatePosition).magnitude;
        //We have likely been teleported. Interpolation would be annoying at this point.
        if (difference > m_InterpolationLimit)
        {
            m_LastFixedUpdatePosition = m_CurrentFixedUpdatePosition;
        }
    }
	
	void Update ()
	{
        if (m_CharacterController == null || m_CharacterController.GetCollider() == null)
        {
            Debug.LogError("Sprite animator can't find properly set-up character");
            this.enabled = false;
            return;
        }
        if (Mathf.Abs(m_CharacterController.GetCollider().GetVelocity().x) >= m_MoveSpeedThreshhold)
        {
            m_LastGoodDirection = m_CharacterController.GetCollider().GetVelocity().normalized;
        }
        else if (Mathf.Abs(m_CharacterController.GetInputMovement().x) > 0.0f)
        {
            m_LastGoodDirection = Vector2.right * m_CharacterController.GetInputMovement().x;
        }
        //Rotate and position the capsule according to character velocity and context


        Vector3 startPosition = Vector3.zero;
        //We are being controlled by a different entity, block interpolation
        if (m_CharacterController.IsMovementLocked())
        {
            startPosition = m_CharacterController.transform.position;
        }
        else
        {
            startPosition = GetInterpolatedPosition();
        }
        //Take the bottom of the capsule collider to rotate from
        //Animations themselves might adjust position via localposition
        Vector3 heightAdjustment = m_CapsuleCollider.GetUpDirection() * (-m_CapsuleCollider.GetLength()*0.5f);
        m_SpriteTransformHook.transform.position = startPosition + heightAdjustment;
        StartAnimation(m_CharacterController.GetCurrentSpriteState());
    }

    protected Vector3 GetInterpolatedPosition()
    {
        //By taking the difference between Time.time and Time.fixedTime, the SpriteAnimator can determine an interpolationfactor between 0 and 1
        //This essentially places the character somewhere between the last two fixedupdate positions (interpolation) essentially roughly an update in the past), 
        //or between the last fixedupdate position and a projected next fixedupdate (extrapolation)
        Vector3 position = m_CapsuleCollider.GetCapsuleTransform().GetPosition();
        float interpolationFactor = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;

        interpolationFactor = Mathf.Clamp01(interpolationFactor);
        switch (m_SpriteInterpolation)
        {
            case SpriteInterpolation.Interpolate:
                position = Vector3.Lerp(m_LastFixedUpdatePosition, m_CurrentFixedUpdatePosition, interpolationFactor);
                break;
            case SpriteInterpolation.Extrapolate:
                position = Vector3.Lerp(m_CurrentFixedUpdatePosition, m_CurrentFixedUpdatePosition + new Vector3(m_CapsuleCollider.GetVelocity().x, m_CapsuleCollider.GetVelocity().y) * Time.fixedDeltaTime, interpolationFactor);
                break;
        }
        return position;
    }

    virtual public void StartAnimation(string a_Name)
    {
        if (m_CurrentAnimationName == a_Name)
        {
            return;
        }

        if (m_SpriteAnimator != null)
        {
            if (m_SpriteAnimator.HasState(0, Animator.StringToHash(m_Prefix + a_Name)))
            {
                m_CurrentAnimationName = a_Name;
                m_SpriteAnimator.Play(m_Prefix + a_Name);
            }
            else
            {
                //Special case for characters that don't have Dangling as a state
                if (a_Name == "Dangling")
                {
                    m_CurrentAnimationName = "Idle";
                    m_SpriteAnimator.Play(m_Prefix + "Idle");
                }
            }
        }

        //See if an offset has been registered for this animation
        Vector3 localPos = Vector3.zero;
        localPos.y = m_StartHeight;

        for (int i = 0; i < m_AnimOffsets.Length; i++)
        {
            if (m_AnimOffsets[i].m_Name == a_Name)
            {
                localPos += m_AnimOffsets[i].m_Offset;
                break;
            }
        }
        transform.localPosition = localPos;
    }
}
