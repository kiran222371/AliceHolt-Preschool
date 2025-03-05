
using UnityEngine;

namespace Prechool.Character
{
    public class MoveTo : MonoBehaviour
    {
        private IMoveable moveable;
        public Transform moveTarget;
        
        void Start()
        {
            moveable = GetComponent<IMoveable>();
        }
        void Update()
        {
            moveable.Move(moveTarget.position - transform.position);
        }
    }
}