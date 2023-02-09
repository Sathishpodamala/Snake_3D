using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class CamerFollow : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject focusObject;
        [SerializeField] private Vector3 cameraOffset;

        [SerializeField] private Vector2 minCameraValue;
        [SerializeField] private Vector2 maxCameraValue;
        private Vector3 focusPos;
        #endregion Variables

        #region Unity Methods
        void Update()
        {
            focusPos = focusObject.transform.position;

            Vector3 pos = focusPos + cameraOffset;

            transform.position = pos;
            // Clamp camera position
            //transform.position = new Vector3(
            //    Mathf.Clamp(pos.x, minCameraValue.x, maxCameraValue.x),
            //    pos.y,
            //    Mathf.Clamp(pos.z, minCameraValue.y, maxCameraValue.y)
            //);
        }
        #endregion Unity Methods

        #region Public Methods
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Callbacks
        #endregion Callbacks

    }
}
