using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Andr3wDown.Cooldown
{
    [System.Serializable]
    public class Cooldown
    {
        public float delay;
        float c = 0;

        public Cooldown(float d, bool reset = false)
        {
            delay = d;
            if (reset)
            {
                c = d;
            }
        }
        public bool CountDown(float timeDelta, float rate = 1f, bool trigger = false, float triggerChange = -1f)
        {
            c -= rate * timeDelta;
            if (trigger)
            {
                if(c <= 0)
                {
                    c = triggerChange > 0 ? triggerChange : delay;
                    return true;
                }
            }
            return false;
        }
        public bool TriggerReady(bool reset = true, float triggerChange = -1f)
        {
            if (c <= 0)
            {
                if (reset)
                {
                    c = triggerChange > 0 ? triggerChange : delay;
                }
                return true;
            }
            return false;
        }
        public void Set(float value = -1)
        {
            c = value > 0 ? value : delay;
        }
        public void Zero()
        {
            c = 0;
        }
    }
}

