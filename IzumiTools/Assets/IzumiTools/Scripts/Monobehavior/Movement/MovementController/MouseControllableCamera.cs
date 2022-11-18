using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Controls camera look direction and fieldOfView by mouse, and manages cursor lock.
    /// </summary>
    [DisallowMultipleComponent]
    public class MouseControllableCamera : MonoBehaviour
    {
        //inspector
        [Header("Sensitivity")]
        [SerializeField]
        float sensitivityMouse = 2f;
        [SerializeField]
        float sensitivetyMouseWheel = 10f;

        [Header("Option")]
        [SerializeField]
        bool limitXAngleBetweenPoles = true;
        [SerializeField]
        bool lockCursorOnAwake = true;

        [Header("Reference")]
        [SerializeField]
        new Camera camera;
        [SerializeField]
        Transform xAxis, yAxis;
        //
        public Camera Camera => camera;
        private void Awake()
        {
            if (lockCursorOnAwake)
                LockCursor(true);
        }
        void Update()
        {
            //transform
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                camera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * sensitivetyMouseWheel;
            }
            if(limitXAngleBetweenPoles)
            {

                Vector3 xAxisEularAngles = xAxis.localEulerAngles;
                xAxisEularAngles.x = ExtendedMath.RotateWithClamp(xAxisEularAngles.x, -Input.GetAxis("Mouse Y") * sensitivityMouse, 270, 90);
                xAxis.localEulerAngles = xAxisEularAngles;
                yAxis.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X") * sensitivityMouse;
            }
            else
            {
                yAxis.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * sensitivityMouse, Space.Self);
                yAxis.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivityMouse, Space.Self);
            }
            //cursor
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                LockCursor(false);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LockCursor(true);
            }
        }
        public void LockCursor(bool cond)
        {
            if (cond)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        private void OnDestroy()
        {
            LockCursor(false);
        }
    }
}