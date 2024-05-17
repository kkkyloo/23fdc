using System;

namespace Kosmos6
{
    public interface IInputShipMovement
    {

        public float CurrentInputMoveY { get; }
        public float CurrentInputMoveX { get; }

        public float CurrentInputRotatePitch { get; }
        public float CurrentInputRotateYaw { get; }
        public float CurrentInputRotateRoll { get; }
        public bool CurrentInputAcceleration { get; }

    }
    public interface IInputShipWeapons
    {
        public bool CurrentInputAttack { get; }
        public event Action OnAttackInput;
    }
}
