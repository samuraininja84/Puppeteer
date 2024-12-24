using UnityEngine;

namespace Puppeteer
{
    [System.Serializable]
    public struct InputState
    {
        public Vector2 moveDirection;
        public bool crouching;
        public bool sprinting;
        public bool canJump;
    }
}
