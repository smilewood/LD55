using System;

namespace LD55
{
    public interface IState
    {
        public bool CheckStateTransition(IState nextState);

        public void Start();
        public void Update();
        public void OnStateExit();

    }
}