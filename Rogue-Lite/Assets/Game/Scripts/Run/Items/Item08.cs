public class Item08 : Item
{
    public override void Start()
    {
        id = 8;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.GetComponent<PlayerWeaponry>().SwapWeapon(2);
    }
}
