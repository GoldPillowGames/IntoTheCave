public class Item07 : Item
{
    public override void Start()
    {
        id = 7;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.GetComponent<PlayerWeaponry>().SwapWeapon(1);
    }
}
