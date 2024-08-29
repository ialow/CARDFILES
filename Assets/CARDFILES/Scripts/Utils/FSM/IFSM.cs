using System;

namespace Untils
{
    public interface IFSM<TEnum, TStates> where TEnum : Enum 
    {
        public TStates States { get; }
        public IFSMState<TEnum> CurrentState { get; }
    }
}