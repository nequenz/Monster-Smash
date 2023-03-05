using UnityEngine;
using System;


[Serializable]
public class ItemHolder
{
    private Item _currentItem;


    public void SetItem(Item item)
    {
        _currentItem = item;
    }

    public bool TryUse()
    {
        if (_currentItem is null)
            return false;

        return _currentItem.TryUse();
    }

    public void AcceptItemDispather(IItemDispatcher dispatcher)
    {
        if (_currentItem is null)
            _currentItem.AcceptDispatch(dispatcher);
    }
}