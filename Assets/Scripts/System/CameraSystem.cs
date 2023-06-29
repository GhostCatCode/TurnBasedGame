using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSystem : BaseSystem<CameraSystem>
{
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float scaleSpeed;

    private float scale = 5f;

    private void Update()
    {
        if (!GameDataMgr.Instance.IsGameOver)
        {
            Vector2 moveDir = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveDir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDir.y = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDir.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDir.x = -1;
            }

            if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                moveDir.x = -Input.GetAxis("Mouse X") * 3;
                moveDir.y = -Input.GetAxis("Mouse Y") * 3;
            }

            transform.position = transform.position + (Vector3)(moveDir * moveSpeed * Time.deltaTime);

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                scale += scaleSpeed * Time.deltaTime;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                scale -= scaleSpeed * Time.deltaTime;
            }
            scale = Mathf.Clamp(scale, 3f, 10f);
            cinemachine.m_Lens.OrthographicSize = scale;
        }
    }

    public void SetCameraPosition(Vector2 position)
    {
        transform.position = position;
    }
}
