using System.Collections.Generic;

using Untils;

namespace Game.Management
{
    public class FSMGameplay : FSMDictionary<StateGameplay>
    {
        public FSMGameplay() 
        {
            States = new Dictionary<StateGameplay, IFSMState<StateGameplay>>();
        }

        public FSMGameplay AddState(params (StateGameplay nameState, IFSMState<StateGameplay>)[] states)
        {
            var countState = states.Length;

            for (int i = 0; i < countState; i++)
                States.Add(states[i].Item1, states[i].Item2);

            return this;
        }
    }
}