using Data.ValueType.UICard;
using Enums;
using UnityEngine;

namespace Data.ScriptableObject.CardButton
{
    [CreateAssetMenu(fileName = "CardButton", menuName = "Scriptable Objects/CardButton", order = 0)]
    public class CardButtonSo : UnityEngine.ScriptableObject
    {
        public CardType CardType;
        
        public UICardElixirData UICardElixirData;
    }
}