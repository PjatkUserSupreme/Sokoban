using System;

namespace Level
{
    public static class LevelEvents
    {
        public static event Action<int> OnLevelEnd;
        public static void EndLevel(int id)
        {
            OnLevelEnd?.Invoke(id);
        }

    }
}