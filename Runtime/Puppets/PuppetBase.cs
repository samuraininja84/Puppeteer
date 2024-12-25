using System.Collections.Generic;
using UnityEngine;

namespace Puppeteer
{
    public abstract class PuppetBase : ScriptableObject
    {
        public abstract Vector2 GetMoveDirection();

        public abstract void RegisterListener(ThreadBase thread);

        public abstract void UnregisterListener(ThreadBase thread);

        public abstract List<ThreadBase> SortThreadsByPriority();

        public abstract void SetPriority(ThreadBase thread, int priority);

        public abstract void SetPriority(int thread, int priority);
    }
}
