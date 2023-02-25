using UnityEngine;
using System;

[Serializable]
public class PlayerInteraction
{
    [SerializeField, SubclassSelector] private Transform _rightHand;
    [SerializeField, SubclassSelector] private Transform _leftHand;
    private Player _player;
    private PlayerMove _move;
    private Item _currentRightItem;
    private Item _currentLeftItem;


    public void Init(Player player, PlayerMove move)
    {
        _player = player;
        _move = move;
    }

    public void Update(float deltaTime)
    {

    }

    public void EquipItemToRightHand(Item item)
    {

    }
}