using UnityEngine;

namespace Prechool.Character
{
    public interface IMoveable
    {
        public void Move(Vector3 movement);
    }
    public interface ILookable
    {
        /// <summary>
        /// A one time method to look at a position
        /// </summary>
        /// <param name="direction"></param>
        public void LookAt(Vector3 position);
    }
    public interface ISpeakable
    {
        public void Speak(string message);
    }
}