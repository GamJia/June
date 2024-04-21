using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    public Slider gaugeSlider; 
    public float increaseInterval = 60f; 
    public int increaseAmount = 1; 
    private float timer = 0f;

    public static GaugeBar Instance => instance;
    private static GaugeBar instance;
    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        gaugeSlider=GetComponent<Slider>();
    }

    void Start()
    {
        LoadGauge(); 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= increaseInterval)
        {
            gaugeSlider.value = Mathf.Min(gaugeSlider.value + increaseAmount, gaugeSlider.maxValue);
            timer = 0f;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveExitTime();
        }

        else
        {
            LoadGauge();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveExitTime();
        }
        else
        {
            LoadGauge();
        }
    }

    void OnApplicationQuit()
    {
        SaveExitTime();
    }

    void SaveExitTime()
    {
        PlayerPrefs.SetString("LastExitTime", System.DateTime.UtcNow.ToString());
        PlayerPrefs.SetFloat("CurrentGaugeValue", gaugeSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateGauge()
    {
        gaugeSlider.value = Mathf.Max(0, gaugeSlider.value - 1);
    }


    void LoadGauge()
    {
        string lastExitTimeStr = PlayerPrefs.GetString("LastExitTime", string.Empty);

        if (!string.IsNullOrEmpty(lastExitTimeStr))
        {
            System.DateTime lastExitTime = System.DateTime.Parse(lastExitTimeStr);
            System.TimeSpan timeSinceExit = System.DateTime.UtcNow - lastExitTime;

            int incrementsSinceLastExit = (int)(timeSinceExit.TotalSeconds / increaseInterval) * increaseAmount;
            float currentGaugeValue = PlayerPrefs.GetFloat("CurrentGaugeValue", 0f) + incrementsSinceLastExit;

            gaugeSlider.value = Mathf.Min(currentGaugeValue, gaugeSlider.maxValue);
        }
    }
}
