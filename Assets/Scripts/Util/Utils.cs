using UnityEngine;

namespace Util
{
    public class Utils
    {
        public static Camera MainCamera => Camera.main;

        public static Ray Ray => MainCamera.ScreenPointToRay(Input.mousePosition);
    }
}