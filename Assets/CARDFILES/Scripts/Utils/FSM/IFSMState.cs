using System;

namespace Untils.FSM
{
    public interface IFSMState<TEnum> where TEnum : Enum
    {
        void Enter();
        void Exit();
    }

    public interface IFSMStateModified<TEnum> : IFSMState<TEnum> where TEnum : Enum
    {
        void Enter(int parameters);
        void Update(object obj);
    }

    public interface IFSMStateExit<T> : IFSMState<T> where T : Enum
    {
        void ExitFSM(int parameters);
    }
}