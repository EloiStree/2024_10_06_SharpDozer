using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class KillDozerControlFacadeMono : MonoBehaviour
{

    #region LEARN: Serializble Class and Relay
    public AxisEventRelay m_moveBackForwardRelay;
    public AxisEventRelay m_rotateLeftRightRelay;
    public AxisEventRelay m_rocketEnginePowerRelay;

    [System.Serializable]
    public class AxisEventRelay {

        [Tooltip("Axis last pushed state -1,1")]
        [Range(-1,1)]
        public float m_axisState;
        [Tooltip("Trigger every time it received data to relay")]
        public UnityEvent<float> m_onAxisStateEmitted;

        public void Push(float percent)
        {
            m_axisState = percent;
            m_onAxisStateEmitted.Invoke(percent);
        }
    }
    #endregion

    #region LEARN: Surcharge des opérateurs 
    public void MoveForward(float percent) { m_moveBackForwardRelay.Push(percent); }
    public void MoveBackward(float percent) { m_moveBackForwardRelay.Push(-percent); }
    public void TurnLeftRight(float percent) { m_rotateLeftRightRelay.Push(percent); }
    /// <summary>
    /// Engine power allows to move only up with percent 0-1 value
    /// </summary>
    /// <param name="percent">Go from 0 to 1 percent of power</param>
    public void SetJetEngineUpPower(float percent) { m_rocketEnginePowerRelay.Push(percent); }

    public void SetJetEngineUpPower(bool isOn) { m_rocketEnginePowerRelay.Push(isOn ? 1 : 0);}
    public void MoveForward(bool isOn) { m_moveBackForwardRelay.Push(isOn ? 1 : 0); }
    public void MoveBackward(bool isOn) { m_moveBackForwardRelay.Push(isOn ? -1 : 0); }
    public void TurnLeft(bool isOn) { m_rotateLeftRightRelay.Push(isOn ? -1 : 0); }
    public void TurnRight(bool isOn) { m_rotateLeftRightRelay.Push(isOn ? 1 : 0); }
    #endregion


    #region Learn: TDD Helper

    public void PushRandomValueToAll()
    {
        PushRandomValueWithoutEngine();
        SetJetEngineUpPower(UnityEngine.Random.Range(0, 1));
    }

    public void PushRandomValueWithoutEngine() {
        MoveForward(UnityEngine.Random.Range(-1, 1));
        MoveBackward(UnityEngine.Random.Range(-1, 1));
        TurnLeftRight(UnityEngine.Random.Range(-1, 1));
    }
    #endregion

}
