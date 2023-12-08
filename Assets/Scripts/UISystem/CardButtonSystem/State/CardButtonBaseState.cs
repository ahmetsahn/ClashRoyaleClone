using Signal;
using Zenject;

namespace UISystem.CardButtonSystem.State
{
    public abstract class CardButtonBaseState
    {
        protected ButtonSignals ButtonSignals;
        
        [Inject]
        private void Construct(ButtonSignals buttonSignals)
        {
            ButtonSignals = buttonSignals;
        }
        public virtual void OnButtonClick() { }
    }
}