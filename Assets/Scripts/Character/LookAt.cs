
using UnityEngine;

namespace Prechool.Character
{
    public class LookAt : MonoBehaviour
    {
        private ILookable lookable;
        public Transform lookTarget;
        
        void Start()
        {
            lookable = GetComponent<ILookable>();
        }
        void Update()
        {
            lookable.LookAt(lookTarget.position);
        }
    }
}