using UnityEngine;

public class ShipAttribute : BaseAttribute
{
    [SerializeField] private int _health;

    public int Health => _health;
}
