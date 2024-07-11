using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera _mainCamera;
    private UiGridInfo _uiGridInfo;
    private GridBehaviour _gridBehaviour;

    [SerializeField] private float sensitivity = 2;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _uiGridInfo = FindAnyObjectByType<UiGridInfo>();
        _gridBehaviour = FindAnyObjectByType<GridBehaviour>();
    }

    private void Update()
    {
        CameraMovementInput();
        CameraRaycast();
    }

    private void CameraMovementInput()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _mainCamera.transform.position += Vector3.left * Time.deltaTime * sensitivity;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _mainCamera.transform.position += Vector3.right * Time.deltaTime * sensitivity;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            _mainCamera.transform.position += Vector3.forward * Time.deltaTime * sensitivity;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _mainCamera.transform.position += Vector3.back * Time.deltaTime * sensitivity;
        }
    }

    private void CameraRaycast()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);


        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if(hitInfo.collider.gameObject.TryGetComponent<GridStat>(out GridStat gridStat))
            {
                _uiGridInfo.ShowInfo(gridStat.x, gridStat.y,gridStat.type);

                if(Input.GetMouseButtonDown(0))
                {
                    _gridBehaviour.SetEndPosition(gridStat.x, gridStat.y);

                }

            }
        }

    }
}
