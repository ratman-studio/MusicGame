using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Animator3d : SpriteAnimator
{
    override public void StartAnimation(string a_Name)
    {
        a_Name = Map3dName(a_Name);
        UpdateCharacterDirection();
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
    }

    float m_LastYRot = 0f;
    private void UpdateCharacterDirection()
    {
        float yRot = 0.0f;
        if (m_CurrentAnimationName != "")
        {
            var scale = (m_LastGoodDirection.x >= 0.0f) ? 1.0f : -1.0f;

            yRot =  90.0f*scale* Mathf.Rad2Deg;
            m_SpriteTransformHook.transform.rotation = Quaternion.Euler( 0.0f,yRot ,0.0f);
        }
        m_LastYRot = yRot;
    }

    private string Map3dName(string name)
    {
        switch (name)
        {
            case "Run":
                return "Fly";
            case "JumpStraight":
            case "JumpSide":
            case "FallUpStraight":
            case "FallUpSide":
            case "FallStraight":
            case "FallSide":
            case "Dangling":
                return "WalkCycle";

            case "Idle" :
            default:
                return "Idle";
        }
        return "Idle";
    }
}

