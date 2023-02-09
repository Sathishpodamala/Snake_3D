using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public class BodyPart : MonoBehaviour
    {
        #region Variables
        private Transform parent;
        [SerializeField]private float margin;
        #endregion Variables

        #region Unity Methods
        void Start()
        {
        
        }
        #endregion Unity Methods

        #region Public Methods
        public void SetParent(Transform bodyParent)
        {
            parent = bodyParent;
        }


        public LinkedListNode<Vector3> FollowTrail(LinkedList<Vector3> path)
        {
            LinkedListNode<Vector3> nextNode = path.Last;
            Vector3 partPos = nextNode.Value;

            float distanceToParent = (partPos - parent.position).magnitude - margin;
            float distanceToNextNode = (partPos - nextNode.Value).magnitude;

            // Iterate through the path nodes to place the tail part in the correct place
            while (distanceToNextNode < distanceToParent && nextNode != null)
            {
                partPos = nextNode.Value;
                distanceToParent = (partPos - parent.position).magnitude - margin;

                nextNode = nextNode.Previous;
                if (nextNode != null)
                    distanceToNextNode = (partPos - nextNode.Value).magnitude;
            }
            transform.position = partPos;

            transform.LookAt(nextNode.Value);
            transform.Translate(Vector3.forward * distanceToParent);

            return nextNode;
        }
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Callbacks
        #endregion Callbacks

    }
}
