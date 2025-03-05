using UnityEngine;
using UnityEngine.AI;

namespace Prechool.Character
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour, IMoveable, ILookable, ISpeakable
    {
        [Header("Animations")]
        public bool lookValid;
        [Range(0, 90)]
        [SerializeField] private float maxLookAngle;
        [Range(0, 5)]
        [Tooltip("How fast the character rotates its head to look at target")]
        [SerializeField] private float lookSpeed = 1;
        [Tooltip("How fast the character rotates on the Y axis")]
        [SerializeField] private float rotSpeed = 1;
        [SerializeField] private float minMoveDistance = 0.1f;
        private Vector3 targetLookPosition;
        private Vector3 lookPosition;
        private float targetLookWeight;
        private float lookWeight;
        private Quaternion targetRot;
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
            if(movement.magnitude < minMoveDistance)
                return;
            targetRot = Quaternion.LookRotation(movement, Vector3.up);
            animator.SetFloat("Vel Forward", movement.magnitude);
        }

        public void LookAt(Vector3 position)
        {
            targetLookPosition = position;
        }

        public void Speak(string message)
        {
            throw new System.NotImplementedException();
        }

        private void LocomotionUpdate()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotSpeed);

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