using UnityEngine;

namespace Puppeteer
{
    public class NavigationThread : ThreadBase
    {
        [Header("Input Settings")]
        public Puppet puppet;
        public InputState inputState;

        [Header("General Settings")]
        public ThreadActivationType activationType = ThreadActivationType.Manual;
        public ThreadPriority priority = ThreadPriority.High;

        [Header("Navigation Settings")]
        public Transform target;

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
            // Check if you're within the target range, if so remove the listener
            if (Vector2.Distance(transform.position, target.position) < 0.1f) RemoveListener();

            // Set the move direction to the direction to the target
            Vector2 direction = target.position - transform.position;
            SetMoveDirection(direction.normalized);

            // Update the input state
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
            return inputState.moveDirection;
        }

        public override bool ThreadActive()
        {
            return puppet.threads.Contains(this);
        }

        public bool GetAutomatic()
        {
            return activationType == ThreadActivationType.Automatic;
        }
    }
}