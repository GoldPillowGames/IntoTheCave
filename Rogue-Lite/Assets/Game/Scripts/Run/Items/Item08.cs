public class Item08 : Item
{
    public override void Awake()
    {
        id = 8;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.GetComponent<PlayerWeaponry>().SwapWeapon(2);
    }
}
