using UnityEngine;

namespace Puppeteer
{
    public abstract class ThreadBase : MonoBehaviour
    {
        public virtual void Process() { }

        public abstract void AddListener();

        public abstract void RemoveListener();

        public abstract void SetPriority(int index);

        public abstract void SetMoveDirection(Vector2 moveDirection);

        public abstract int GetPriority();

        public abstract Vector2 GetMoveDirection();

        public abstract bool ThreadActive();
    }

    public enum ThreadActivationType
    {
        Automatic,
        Manual
    }

    public enum ThreadPriority
    {
        Low,
        Medium,
        High
    }
}
