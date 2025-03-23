using UnityEngine;
using UnityEngine.AI;

namespace Prechool.Character
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour, IMoveable, ILookable, ISpeakable
    {
        [Header("Animations")]
        [Tooltip("Rotate the body towards the look direction when the character movement is below minMoveDistance")]
        public bool faceLookStill;
        [Range(0, 90)]
        [SerializeField] private float maxLookAngle = 60;
        [Range(0, 5)]
        [Tooltip("How fast the character rotates its head to look at target")]
        [SerializeField] private float lookSpeed = 1;
        [Tooltip("How fast the character rotates (deg/sec) on the Y axis")]
        [SerializeField] private float rotSpeed = 180;
        [Tooltip("How fast the character moves")]
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float minMoveDistance = 0.1f;
        private bool lookValid;
        private Vector3 targetLookPosition;
        private Vector3 lookPosition;
        private float targetLookWeight;
        private float lookWeight;
        private Quaternion targetRot;
        private Vector3 targetMovement;
        private float targetVelForward;
        [Header("Agent")]
        // components
        private NavMeshAgent agent;
        private Animator animator;
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.applyRootMotion = true;

            agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.updateRotation = false;
                agent.updatePosition = false;
            }

        }
        void Update()
        {
            LocomotionUpdate();
            LookUpdate();
            if (agent != null)
                AgentUpdate();
        }
        void OnAnimatorIK(int layerIndex)
        {
            animator.SetLookAtWeight(lookWeight);
            animator.SetLookAtPosition(lookPosition);

        }
        public void Move(Vector3 movement)
        {
            targetMovement += movement;
        }

        public void LookAt(Vector3 position)
        {
            targetLookPosition = position;
        }

        public void Speak(string message)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Only manually update the rotation when running, otherwise use root motions
        /// </summary>
        private void LocomotionUpdate()
        {
            if (targetMovement.magnitude > minMoveDistance)
            {
                targetVelForward = Mathf.Clamp(targetMovement.magnitude, 0, 2);

                targetRot = Quaternion.LookRotation(targetMovement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotSpeed);
                animator.SetInteger("Rot Y Deg", 0);
            }
            else
            {
                targetVelForward = 0;
                if (faceLookStill)
                {
                    targetRot = Quaternion.LookRotation(targetLookPosition - transform.position, Vector3.up);
                    float yDeg = targetRot.eulerAngles.y - transform.rotation.eulerAngles.y;
                    animator.SetInteger("Rot Y Deg", 90 * Mathf.RoundToInt(yDeg / 90));
                }
            }
            targetMovement.Zero();

            animator.SetFloat("Vel Forward", Mathf.Lerp(animator.GetFloat("Vel Forward"), targetVelForward, Time.deltaTime * moveSpeed));

        }

        private void LookUpdate()
        {
            // If targetLookPosition is within maxLookAngle, then targetLookWeight is 1
            // If look valid (targetLookWeight is 1), then 
            var minLookDot = 1 - maxLookAngle / 90;
            lookValid = Vector3.Dot(targetLookPosition - transform.position, transform.forward) > minLookDot;
            targetLookWeight = lookValid ? 1 : 0;

            lookWeight = Mathf.Lerp(lookWeight, targetLookWeight, Time.deltaTime * lookSpeed);
            // don't update the position if look not valid so that head ik slowly transitions back to animation position
            if (lookValid)
            {
                lookPosition = Vector3.Lerp(lookPosition, targetLookPosition, Time.deltaTime * lookSpeed);
            }
        }

        private void AgentUpdate()
        {
            Move(agent.nextPosition - transform.position);
        }
    }
}