using UnityEngine;
using System;


[Serializable]
public class PlayerInteraction
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;
    private Player _player;
    private PlayerMove _move;
    private GameCamera _camera;
    private Item _currentItem;


    public event Action<Item> ItemEquipped;
    public event Action<Item> ItemDropped;
    public event Action<Item> ItemUsed;


    public Transform InteractionObject => _camera.transform;


    public void Init(Player player, PlayerMove move, GameCamera camera)
    {
        _player = player;
        _move = move;
        _camera = camera;
    }

    public void Update(float deltaTime)
    {

    }

    public void UseItem()
    {
        if (_currentItem is null)
            return;

        _currentItem.Use();
        ItemUsed?.Invoke(_currentItem);
    }

    public void EquipItem(Item item)
    {
        if (item is null)
            return;

        DropItem();

        _currentItem = item;
        _currentItem.EquipToObject(InteractionObject, _player.transform);
        ItemEquipped?.Invoke(_currentItem);
    }

    public void DropItem()
    {
        if (_currentItem is null)
            return;

        _currentItem.Unequip();
        ItemDropped?.Invoke(_currentItem);

        _currentItem = null;
    }
}