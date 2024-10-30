using System.Collections.Generic;
using System;

namespace LK.SimpleFramework
{
    /// <summary>
    /// 基于有向图的实现的有限状态机。
    /// </summary>
    /// <typeparam name="T">用于标识状态的枚举</typeparam>
    public class FiniteStateMachine<T> where T : Enum
    {
        private readonly Dictionary<T, List<Transition<T>>> stateGraph = new Dictionary<T, List<Transition<T>>>();
        private readonly Dictionary<T, IState> stateInstance = new Dictionary<T, IState>();
        private T currentState;
        public FiniteStateMachine()
        {
            currentState = default;
            Add(currentState, new EmptyState());
        }
        public FiniteStateMachine(T tag, IState initState)
        {
            currentState = tag;
            Add(currentState, initState);
        }
        public T CurrentState => currentState;

        /// <summary>
        /// 更新状态机
        /// </summary>
        public void Update()
        {
            bool needChange = false;
            int weight = 0;
            foreach (var transition in stateGraph[currentState])
            {
                if (transition.Check())
                {
                    if (needChange && transition.Weight > weight)
                    {
                        currentState = transition.Target;
                    }
                    else if (!needChange)
                    {
                        stateInstance[currentState].Exit();
                        weight = transition.Weight;
                        needChange = true;
                        currentState = transition.Target;
                    }
                }
            }
            if (needChange)
            {
                stateInstance[currentState].Enter();
            }
            stateInstance[currentState].Update();
        }

        /// <summary>
        /// 添加一个状态到状态机中。
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Add(T tag, IState state)
        {
            if (state is null)
            {
                throw new ArgumentNullException("state");
            }

            if (stateInstance.ContainsKey(tag))
            {
                throw new ArgumentException("A state with the same label already exists in the FiniteStateMachine.");
            }

            stateInstance.Add(tag, state);
            stateGraph.Add(tag, new List<Transition<T>>());
        }

        /// <summary>
        /// 将指定的标签替换一个状态实例。
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Replace(T tag, IState state)
        {
            if (state is null)
            {
                throw new ArgumentNullException("state");
            }

            if (!stateInstance.ContainsKey(tag))
            {
                throw new ArgumentException("There is no state with the specified label in the FiniteStateMachine.");
            }

            stateInstance[tag] = state;
        }

        /// <summary>
        /// 移除一个指定状态。此操作会移除所有与此状态关联的过渡。
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool Remove(T tag)
        {
            if (currentState.Equals(tag))
            {
                throw new InvalidOperationException("Cannot remove a state that is currently active.");
            }

            if (stateInstance.ContainsKey(tag))
            {
                stateInstance.Remove(tag);
                stateGraph.Remove(tag);
                foreach (var transitions in stateGraph.Values)
                {
                    transitions.RemoveAll((t) => t.Target.Equals(tag));
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 绑定一个过渡到状态机中。
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="transition"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Bind(T tag, Transition<T> transition)
        {
            if (!stateInstance.ContainsKey(tag))
            {
                throw new ArgumentException("There is no state with the specified label in the FiniteStateMachine.");
            }

            if (!stateInstance.ContainsKey(transition.Target))
            {
                throw new ArgumentException("Invalid transitions. The transition targets a state that does not exist in the FiniteStateMachine");
            }

            if (ContainsTransition(tag, transition.Target))
            {
                throw new ArgumentException("Invalid transitions. A transition already exists in the FiniteStateMachine with a specified state and target.");
            }

            stateGraph[tag].Add(transition);
        }

        /// <summary>
        /// 从状态机中解除一个过渡的绑定。
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Unbind(T tag, T target)
        {
            if (stateInstance.ContainsKey(tag) && stateInstance.ContainsKey(target))
            {
                var list = stateGraph[tag];
                int index = list.FindIndex((t) => t.Target.Equals(target));
                if (index != -1)
                {
                    list.RemoveAt(index);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 查询状态机中是否包含指定的节点
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool ContainsState(T tag)
        {
            return stateInstance.ContainsKey(tag);
        }

        /// <summary>
        /// 查询状态机中是否包含指定的过渡
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool ContainsTransition(T tag, T target)
        {
            if (stateInstance.ContainsKey(tag) && stateInstance.ContainsKey(target))
            {
                foreach (var t in stateGraph[tag])
                {
                    if (t.Target.Equals(target))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
