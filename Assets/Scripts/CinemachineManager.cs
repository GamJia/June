using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CinemachineManager : MonoBehaviour
{
    public Transform targetTransform; 
    private CinemachineVirtualCamera cinemachineCamera;
    public float zoomSpeed = 1f; 
    public float minOrthographicSize = 2.2f; 
    public float maxOrthographicSize = 5.4f; 
    public float moveSpeed = 0.5f; 
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
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cinemachineCamera.m_Lens.OrthographicSize -= scroll * zoomSpeed;
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.m_Lens.OrthographicSize, minOrthographicSize, maxOrthographicSize);
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0))
        {
            float moveX = -Input.GetAxis("Mouse X") * moveSpeed;
            float moveY = -Input.GetAxis("Mouse Y") * moveSpeed;
            transform.position += new Vector3(moveX, moveY, 0);
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
