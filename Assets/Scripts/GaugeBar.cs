using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    public Slider gaugeSlider;
    public float increaseInterval = 60f;
    public int increaseAmount = 1;
    private Coroutine gaugeCoroutine = null;

    [SerializeField] private Text countText;
    [SerializeField] private Text timerText;

    public static GaugeBar Instance => instance;
    private static GaugeBar instance;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        gaugeSlider = GetComponent<Slider>();
    }

    void Start()
    {
        LoadGauge();
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
        PlayerPrefs.SetInt("CurrentGaugeValue", Mathf.RoundToInt(gaugeSlider.value)); 
        PlayerPrefs.Save();
    }

    public void UpdateGauge()
    {
        int currentGaugeValue = PlayerPrefs.GetInt("CurrentGaugeValue", Mathf.RoundToInt(gaugeSlider.maxValue));
        currentGaugeValue = Mathf.Max(0, currentGaugeValue - 1);
        PlayerPrefs.SetInt("CurrentGaugeValue", currentGaugeValue);
        PlayerPrefs.Save();
        
        InitializeGauge();
    }

    public void InitializeGauge()
    {
        int currentGaugeValue = PlayerPrefs.GetInt("CurrentGaugeValue", 0);
        gaugeSlider.value = currentGaugeValue;

        if (gaugeSlider.value >= gaugeSlider.maxValue)
        {
            timerText.gameObject.SetActive(false);
            countText.text="MAX";

            PlayerPrefs.SetInt("LastGaugeTime", 0);
            PlayerPrefs.Save();
        }

        else
        {
            countText.text = gaugeSlider.value.ToString();
            int lastGaugeTime = PlayerPrefs.GetInt("LastGaugeTime", 60);
            if (gaugeCoroutine != null)
            {
                StopCoroutine(gaugeCoroutine);
                gaugeCoroutine = null;
            }
            gaugeCoroutine = StartCoroutine(GaugeCoroutine(lastGaugeTime > 0 ? lastGaugeTime : 60));
        }        
        
    }

    public void TutorialData()
    {
        gaugeSlider.value = 10;
        countText.text = "10";
        PlayerPrefs.SetInt("CurrentGaugeValue", 10);
        PlayerPrefs.Save();
        timerText.gameObject.SetActive(false);
    }

    public void TutorialCoroutine()
    {
        if (gaugeSlider.value < gaugeSlider.maxValue)
        {
            StartCoroutine(TutorialAnimation());
        }
    }

    private IEnumerator TutorialAnimation()
    {
        float startValue = gaugeSlider.value;
        float elapsedTime = 0f;

        while (elapsedTime < 4)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / 4;
            gaugeSlider.value = Mathf.Lerp(startValue, gaugeSlider.maxValue, progress);
            countText.text = Mathf.RoundToInt(gaugeSlider.value).ToString();

            yield return null;
        }
        gaugeSlider.value = Mathf.RoundToInt(gaugeSlider.maxValue);
        PlayerPrefs.SetInt("CurrentGaugeValue", Mathf.RoundToInt(gaugeSlider.value)); // Saving as int
        PlayerPrefs.Save();
    }

    private IEnumerator GaugeCoroutine(int lastGaugeTime)
    {
        
        while (gaugeSlider.value < gaugeSlider.maxValue)  
        {
            float elapsedTime = 0f;

            countText.text = gaugeSlider.value.ToString();
            timerText.gameObject.SetActive(true);

            while (elapsedTime < lastGaugeTime)
            {
                timerText.text = $"0:{(int)(lastGaugeTime - elapsedTime):D2}";
                PlayerPrefs.SetInt("LastGaugeTime", (int)(lastGaugeTime - elapsedTime));
                PlayerPrefs.Save();
                yield return new WaitForSeconds(1f);
                elapsedTime++;
            }

            gaugeSlider.value++;
            PlayerPrefs.SetInt("CurrentGaugeValue", Mathf.RoundToInt(gaugeSlider.value));
            PlayerPrefs.Save();

            lastGaugeTime = 60;
        }

        InitializeGauge();

    }


    void LoadGauge()
    {
        string lastExitTimeStr = PlayerPrefs.GetString("LastExitTime", string.Empty);
        if (!string.IsNullOrEmpty(lastExitTimeStr))
        {
            System.DateTime lastExitTime = System.DateTime.Parse(lastExitTimeStr);
            System.TimeSpan timeSinceExit = System.DateTime.UtcNow - lastExitTime;

            int incrementsSinceLastExit = (int)(timeSinceExit.TotalSeconds / increaseInterval) * increaseAmount;
            int currentGaugeValue = PlayerPrefs.GetInt("CurrentGaugeValue", 0);

            currentGaugeValue = Mathf.Min(currentGaugeValue + incrementsSinceLastExit, Mathf.RoundToInt(gaugeSlider.maxValue));

            // 업데이트된 게이지 값을 PlayerPrefs에 저장
            PlayerPrefs.SetInt("CurrentGaugeValue", currentGaugeValue);
            PlayerPrefs.Save();
        }

        InitializeGauge();
    }
}
