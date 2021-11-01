using System;
using UnityEngine;

namespace World.Crow
{
    public class Player
    {
        public static float HorizontalInputAxis() => Input.GetAxisRaw("Horizontal");
        static float VerticalInputAxis() => Input.GetAxisRaw("Vertical");

        public static Vector2 GetInput() => new Vector2(HorizontalInputAxis(), VerticalInputAxis());

        public bool WantsToHopLeft() => HopInput(IsTowardsLeft);
        public bool WantsToHopRight() => HopInput(IsTowardsRight);
        public bool WantsToFly() => FlightInput(IsTowardsUp) && HopInput(Any);
        public bool WantsToLand() => FlightInput(IsTowardsDown);
        public bool WantsToDrop() => FlightInput(IsTowardsDown) && HopInput(Any);

        static bool HopInput(Func<float, bool> read) => read(HorizontalInputAxis());
        static bool FlightInput(Func<float, bool> read) => read(VerticalInputAxis());

        static bool IsTowardsLeft(float axisValue) => IsNegative(axisValue);
        static bool IsTowardsRight(float axisValue) => IsPositive(axisValue);
        static bool IsTowardsUp(float axisValue) => IsPositive(axisValue);
        static bool IsTowardsDown(float axisValue) => IsNegative(axisValue);

        static bool Any(float value) => value != 0f;
        static bool IsPositive(float axisValue) => axisValue > 0;
        static bool IsNegative(float axisValue) => axisValue < 0;
    }
}
