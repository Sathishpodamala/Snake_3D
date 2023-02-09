using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    [RequireComponent(typeof(Rigidbody), typeof(InputManager))]
    public class Movement : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float turnSpeed = 10f;


        [SerializeField] private BodyPart bodyPartPrefab;
        [SerializeField] private uint startingLength = 2;
        [SerializeField] private float pathNodeSpacing = 0.2f;
        LinkedList<Vector3> path = new();
        List<BodyPart> tail = new();

        private Rigidbody rb;
        private InputManager input;
        Quaternion nextRot;
        #endregion Variables

        #region Unity Methods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<InputManager>();
            nextRot = transform.rotation;

            InitPath();

            for (int i = 0; i < startingLength; i++)
            {
                AddBodyPart();
            }
        }
        void Update()
        {
            if(tail.Count>1)
            {
                MoveBodyParts();
            }

            if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                AddBodyPart();
            }
        }
        void FixedUpdate()
        {
            MoveSnake();

            if((transform.position-path.First.Value).magnitude>pathNodeSpacing)
            {
                path.AddFirst(transform.position);
            }
        }
        #endregion Unity Methods

        #region Public Methods
        #endregion Public Methods

        #region Private Methods
        private void MoveSnake()
        {
            Vector3 direction = new Vector3(input.HorizontalInput, 0f, input.VerticalInput);
            if (direction != Vector3.zero)
                nextRot = Quaternion.LookRotation(direction);
            Quaternion finalRotation = Quaternion.Slerp(transform.rotation, nextRot, 0.9f);
            rb.MoveRotation(finalRotation);

            rb.velocity = Vector3.zero;

            rb.AddForce(transform.forward * moveSpeed * Time.fixedDeltaTime, ForceMode.Impulse);

        }

        private void MoveBodyParts()
        {
            LinkedListNode<Vector3> lastNode = path.First;

            foreach (BodyPart part in tail)
            {
                lastNode = part.FollowTrail(path);
            }

            while (lastNode != path.Last)
            {
                path.RemoveLast();
            }
        }

        private void InitPath()
        {
            for (int i = 0; i < startingLength / pathNodeSpacing; i++)
            {
                path.AddFirst(transform.position + Vector3.back * i * pathNodeSpacing);
            }
        }

        [DebugButton("Add Body")]
        private void AddBodyPart()
        {
            BodyPart newBodyPart;

            if (tail.Count == 0)
            {
                newBodyPart = Instantiate(bodyPartPrefab, transform.position, transform.rotation);
            }
            else
            {
                Vector3 newPartPos = tail[tail.Count - 1].transform.position;
                Quaternion newPartRotation = tail[tail.Count - 1].transform.localRotation;
                newBodyPart = Instantiate(bodyPartPrefab, newPartPos, newPartRotation);
            }

            if (newBodyPart != null)
                tail.Add(newBodyPart);

            if (tail.Count > 1)
            {
                tail[tail.Count - 1].SetParent(tail[tail.Count - 2].transform);
            }
            else
            {
                tail[tail.Count - 1].SetParent(transform);
            }
        }
        #endregion Private Methods

        #region Callbacks
        #endregion Callbacks

    }
}
