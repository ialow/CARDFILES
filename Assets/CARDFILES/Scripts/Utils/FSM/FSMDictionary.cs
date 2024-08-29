using System;
using System.Collections.Generic;

namespace Untils
{
    public abstract class FSMDictionary<TEnum> : IFSM<TEnum, Dictionary<TEnum, IFSMState<TEnum>>> where TEnum : Enum
    {
        public Dictionary<TEnum, IFSMState<TEnum>> States { get; protected set; }
        public IFSMState<TEnum> CurrentState { get; protected set; }

        /// <summary>
        /// Переход в стадию
        /// </summary>
        public virtual void EnterIn(TEnum state)
        {
            if (States.TryGetValue(state, out IFSMState<TEnum> stObj))
            {
                CurrentState?.Exit();
                CurrentState = stObj;
                CurrentState?.Enter();
            }
        }

        /// <summary>
        /// Переход в стадию c int параметром
        /// </summary>
        public void EnterIn<TInterface>(TEnum state, int parameter) where TInterface : IFSMState<TEnum>
        {
            if (States.TryGetValue(state, out IFSMState<TEnum> stObj))
            {
                var stateModifed = stObj as IFSMStateModified<TEnum>;

                CurrentState?.Exit();
                CurrentState = stObj;
                stateModifed?.Enter(parameter);
            }
        }

        /// <summary>
        /// Выход из FSM с int параметром
        /// </summary>
        public virtual void Exit<TInterface>(TEnum state, int parameter) where TInterface : IFSMStateExit<TEnum>
        {
            if (States.TryGetValue(state, out IFSMState<TEnum> stObj))
            {
                CurrentState = null;
                var stateModifed = stObj as IFSMStateExit<TEnum>;
                stateModifed?.ExitFSM(parameter);
            }
        }
    }
}
