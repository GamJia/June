using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Illustration : MonoBehaviour
{
    private GameObject illustration; // 현재 활성화된 GameObject를 추적

    void Start()
    {
        // 시작 시 모든 자식 GameObject를 비활성화
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void SetIllustration(int index)
    {
        Image background = GetComponent<Image>(); // 배경 이미지 컴포넌트 가져오기
        background.enabled = true; // 배경 이미지 활성화

        if (illustration != null)
        {
            illustration.SetActive(false); // 이전에 활성화된 GameObject를 비활성화
        }

        // 새로운 GameObject를 활성화하고 illustration 변수에 저장
        illustration = transform.GetChild(index).gameObject;
        illustration.SetActive(true);
    }
}
