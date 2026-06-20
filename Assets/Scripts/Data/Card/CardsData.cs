using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsData", menuName = "Order Rush/Cards Data")]
public class CardsData : ScriptableObject
{
    public List<CardData> Cards;
}
