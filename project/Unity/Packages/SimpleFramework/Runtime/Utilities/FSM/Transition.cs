using System;

namespace LK.SimpleFramework
{
    [Serializable]
    public readonly struct Transition<T> where T : Enum
    {
        private readonly T target;
        private readonly int weight;
        private readonly Func<bool> check;

        public Transition(T target, int weight, Func<bool> check)
        {
            this.weight = weight;
            this.check = check;
            this.target = target;
        }

        public T Target => target;
        public int Weight => weight;
        public Func<bool> Check => check;
    }
}

