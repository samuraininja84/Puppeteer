using UnityEngine;

namespace Puppeteer
{
    public class DirectionalThread : ThreadBase
    {
        [Header("Input Settings")]
        public Puppet puppet;
        public Direction direction = Direction.None;
        private InputState inputState;

        [Header("General Settings")]
        public ThreadActivationType activationType = ThreadActivationType.Manual;
        public ThreadPriority priority = ThreadPriority.High;

        private void OnEnable()
        {
            if (GetAutomatic() && puppet != null) puppet.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (puppet != null) puppet.UnregisterListener(this);
        }

        public override void Process()
        {
            SetMoveDirection(GetMoveDirection());
            if (puppet != null) puppet.inputState = inputState;
        }

        public override void AddListener()
        {
            puppet.RegisterListener(this);
        }

        public override void RemoveListener()
        {
            puppet.UnregisterListener(this);
        }

        public override void SetPriority(int index)
        {
            priority = (ThreadPriority)index;
        }

        public override void SetMoveDirection(Vector2 moveDirection)
        {
            inputState.moveDirection = moveDirection;
        }

        public override int GetPriority()
        {
            return (int)priority;
        }

        public override Vector2 GetMoveDirection()
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2.up;
                case Direction.Down:
                    return Vector2.down;
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                    return Vector2.right;
                case Direction.None:
                    return Vector2.zero;
                default:
                    return Vector2.zero;
            }
        }

        public override bool ThreadActive()
        {
            return puppet.threads.Contains(this);
        }

        public bool GetAutomatic()
        {
            return activationType == ThreadActivationType.Automatic;
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }
    }
}
