using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class CinemachineManager : MonoBehaviour
{
    public Transform targetTransform; 
    private CinemachineVirtualCamera cinemachineCamera;
    private float minOrthographicSize = 0.9f; 
    private float maxOrthographicSize = 5.4f; 
    private float zoomSpeed = 0.1f; 
    private float moveSpeed = 0.7f; 
    public static bool isDragging = false;

    private Coroutine sizeChangeCoroutine;

    public static CinemachineManager Instance => instance;
    private static CinemachineManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineCamera.m_Lens.OrthographicSize=5.4f;
        targetTransform = cinemachineCamera.Follow;
    }

    void Update()
    {
        if ((SceneManager.GetActiveScene().buildIndex == 0))
        {
            return; 
        }

        if (isDragging)
        {
            return;
        }

        if (cinemachineCamera.Follow == null)
        {
            HandleMovement();
        }
    }

    public void SetDragging(bool dragging)
    {
        isDragging = dragging;
    }

    public void ChangeTarget(bool isAvailable)
    {        
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = isAvailable ? null : targetTransform;
        }
    }

    private void HandleMovement()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        zoomSpeed = 1f; 
        moveSpeed = 5f; 

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float targetSize = Mathf.Clamp(cinemachineCamera.m_Lens.OrthographicSize - (scroll * 6f), minOrthographicSize, maxOrthographicSize);
            if (sizeChangeCoroutine != null)
            {
                StopCoroutine(sizeChangeCoroutine); 
            }
            sizeChangeCoroutine = StartCoroutine(ChangeSizeSmoothly(cinemachineCamera.m_Lens.OrthographicSize, targetSize));
        }

        if (Input.GetMouseButton(0))
        {
            float moveX = -Input.GetAxis("Mouse X") * moveSpeed;
            float moveY = -Input.GetAxis("Mouse Y") * moveSpeed;

            Vector3 targetPosition = transform.position + new Vector3(moveX, moveY, 0);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
        
        #elif UNITY_ANDROID || UNITY_IOS

        if (Input.GetMouseButton(0))
        {
            float moveX = -Input.GetAxis("Mouse X") * moveSpeed;
            float moveY = -Input.GetAxis("Mouse Y") * moveSpeed;

            Vector3 targetPosition = transform.position + new Vector3(moveX, moveY, 0);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            deltaMagnitudeDiff *= zoomSpeed * 0.1f; 

            float newSize = cinemachineCamera.m_Lens.OrthographicSize + deltaMagnitudeDiff;

            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.m_Lens.OrthographicSize, Mathf.Clamp(newSize, minOrthographicSize, maxOrthographicSize), Time.deltaTime * 5f);

        }
        #endif
    }

    IEnumerator ChangeSizeSmoothly(float fromSize, float toSize)
    {
        float elapsedTime = 0f;
        float duration = 0.2f;
        while (elapsedTime < duration)
        {
            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(fromSize, toSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cinemachineCamera.m_Lens.OrthographicSize = toSize;
    }


}
