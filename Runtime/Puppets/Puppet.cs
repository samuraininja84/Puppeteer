using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puppeteer
{
    [CreateAssetMenu(fileName = "New Puppet", menuName = "Puppeteer/New Puppet")]
    public class Puppet : ScriptableObject, IPuppet
    {
        [Header("State")]
        public InputState inputState;
        public Action OnJump;

        [Header("Threads")]
        public List<ThreadBase> threads = new List<ThreadBase>();

        public InputState GetState()
        {
            // Sort the threads by priority
            SortThreadsByPriority();

            // Process the threads in order from lowest to highest priority
            for (int i = 0; i < threads.Count; i++) threads[i].Process();

            // Check if the jump button is pressed
            if (inputState.canJump) OnJump?.Invoke();

            // Return the input state
            return inputState;
        }

        public Vector2 GetMoveDirection()
        {
            return inputState.moveDirection;
        }

        public void RegisterListener(ThreadBase thread)
        {
            threads.Add(thread);
            SortThreadsByPriority();
        }

        public void UnregisterListener(ThreadBase thread)
        {
            threads.Remove(thread);
            SortThreadsByPriority();
        }

        public List<ThreadBase> SortThreadsByPriority()
        {
            // Set the highest priority thread to the last in the list
            threads.Sort((a, b) => a.GetPriority().CompareTo(b.GetPriority()));
            return threads;
        }

        public void SetPriority(ThreadBase thread, int priority)
        {
            if (priority < 0) priority = 0;
            else if (priority > 10) priority = 10;
            thread.SetPriority(priority);
            SortThreadsByPriority();
        }

        public void SetPriority(int thread, int priority)
        {
            if (priority < 0) priority = 0;
            else if (priority > 10) priority = 10;
            threads[thread].SetPriority(priority);
            SortThreadsByPriority();
        }
    }
}