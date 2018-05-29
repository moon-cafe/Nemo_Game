using UnityEngine;
using System;

[Serializable]
class Stat
{
    
    public BarScript bar;

    private float maxVal;

    private float currentVal;

    public float CurrentValue
    {
        get
        {
            return currentVal;
        }
        set
        {
            this.currentVal = Mathf.Clamp(value, 0, maxVal);

            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            Bar.MaxValue = value;
            this.maxVal = value;
        }
    }
    public float Atk_Speed;

    public BarScript Bar
    {
        get
        {
            return bar;
        }
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentValue = currentVal;
    }
}
