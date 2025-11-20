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

        public override void AddListener() => puppet.RegisterListener(this);

        public override void RemoveListener() => puppet.UnregisterListener(this);

        public override void SetPriority(int index) => priority = (ThreadPriority)index;

        public override void SetMoveDirection(Vector2 moveDirection) => inputState.moveDirection = moveDirection;

        public override int GetPriority() => (int)priority;

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

        public override bool ThreadActive() => puppet.threads.Contains(this);

        public bool GetAutomatic() => activationType == ThreadActivationType.Automatic;

        private Vector3 GetArrowDirection(Vector3 direction, bool orthographic = true)
        {
            Vector3 arrowDirection = Vector3.zero;
            if (orthographic) arrowDirection = direction;
            else arrowDirection = new Vector3(direction.x, 0, direction.y);
            return arrowDirection;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            // Check if the enter interaction is null or if the move direction is zero
            if (GetMoveDirection() != Vector2.zero)
            {
                // Get the position of the passage and the direction of the enter interaction
                Vector3 pos = transform.position;

                // Get the direction in 3D space
                Vector3 direction = GetArrowDirection(GetMoveDirection(), Camera.main.orthographic);

                // Draw the arrow
                DrawArrow.ForGizmo(pos, direction, Color.green);
            }
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

    public static class DrawArrow
    {
        public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            ForGizmo(pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        public static void ForGizmoTwoPoints(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            ForGizmoTwoPoints(from, to, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);
            DrawArrowEnd(true, pos, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        public static void ForGizmoTwoPoints(Vector3 from, Vector3 to, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Gizmos.DrawLine(from, to);
            Vector3 direction = to - from;
            DrawArrowEnd(true, from, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            ForDebug(pos, direction, Color.white, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Debug.DrawRay(pos, direction, color);
            DrawArrowEnd(false, pos, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        }

        private static void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1f)
        {
            Vector3 right = (Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
            Vector3 left = (Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
            Vector3 up = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;
            Vector3 down = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;

            Vector3 arrowTip = pos + (direction * arrowPosition);

            if (gizmos)
            {
                Gizmos.color = color;
                Gizmos.DrawRay(arrowTip, right);
                Gizmos.DrawRay(arrowTip, left);
                Gizmos.DrawRay(arrowTip, up);
                Gizmos.DrawRay(arrowTip, down);
            }
            else
            {
                Debug.DrawRay(arrowTip, right, color);
                Debug.DrawRay(arrowTip, left, color);
                Debug.DrawRay(arrowTip, up, color);
                Debug.DrawRay(arrowTip, down, color);
            }
        }
    }
}



