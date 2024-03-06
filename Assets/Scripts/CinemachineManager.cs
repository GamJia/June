using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CinemachineManager : MonoBehaviour
{
    public Transform targetTransform; 
    private CinemachineVirtualCamera cinemachineCamera;
    public float zoomSpeed = 0.01f; 
    public float minOrthographicSize = 2.2f; 
    public float maxOrthographicSize = 5.4f; 
    public float moveSpeed = 0.01f; 
    public static bool isPuzzleDragging = false;

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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; 
        }
        

        if (cinemachineCamera.Follow == null)
        {
            if(!isPuzzleDragging)
            {
                HandleZoom();
                HandleMovement();
            }
        }
    }

    public static void SetPuzzleDragging(bool dragging)
    {
        isPuzzleDragging = dragging;
    }

    public bool ChangeTarget(bool isAvailable)
    {        
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = isAvailable ? targetTransform : null;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleZoom()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cinemachineCamera.m_Lens.OrthographicSize - scroll * zoomSpeed;
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(newSize, minOrthographicSize, maxOrthographicSize);
        #elif UNITY_ANDROID || UNITY_IOS
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

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0))
        {
            float moveX = -Input.GetAxis("Mouse X") * moveSpeed;
            float moveY = -Input.GetAxis("Mouse Y") * moveSpeed;

            // 목표 위치 계산
            Vector3 targetPosition = transform.position + new Vector3(moveX, moveY, 0);

            // 부드러운 이동을 위해 Lerp 함수 사용
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }


    private bool IsPointerOverObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return true; 
        }

        return false; 
    }
}
