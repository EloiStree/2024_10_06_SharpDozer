using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillDozerJetEngineMono : MonoBehaviour
{
    public Rigidbody m_rigidbodyToAffect = null;
    public float m_forcePush = 1000.0f;
    public ForceMode m_forceMode = ForceMode.Force;

    [Range(0,1)]
    public float m_enginePowerInPercent;

    [Range(0, 1)]
    public float m_enginePowerInPercentState;
    public float m_lerpMultiplicator=2;
    public Transform m_upDirection;

    public UnityEvent<float> m_onEngineStateChanged;
    
    public void SetEnginePowerInPercent(float percent)
    {
        m_enginePowerInPercent = percent;

    }
    public void Update()
    {
        m_enginePowerInPercentState = Mathf.Lerp(m_enginePowerInPercentState, m_enginePowerInPercent, Time.deltaTime*m_lerpMultiplicator);
        bool hasSomeChanged = m_enginePowerInPercentState != m_enginePowerInPercent;
        
        if (m_enginePowerInPercentState!=0) {

            m_rigidbodyToAffect.AddForce(m_upDirection.up * m_forcePush*Time.deltaTime*m_enginePowerInPercentState, m_forceMode);
        }

        if (hasSomeChanged)
        {
            m_onEngineStateChanged.Invoke(m_enginePowerInPercentState);
        }
    }
    
}
