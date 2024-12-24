using System.Collections.Generic;
using UnityEngine;

namespace Puppeteer
{
    public interface IPuppet
    {
        public InputState GetState();

        public Vector2 GetMoveDirection();

        public void RegisterListener(ThreadBase thread);

        public void UnregisterListener(ThreadBase thread);

        public List<ThreadBase> SortThreadsByPriority();

        public void SetPriority(ThreadBase thread, int priority);

        public void SetPriority(int thread, int priority);
    }
}
