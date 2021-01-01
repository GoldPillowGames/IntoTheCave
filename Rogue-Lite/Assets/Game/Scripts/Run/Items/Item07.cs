public class Item07 : Item
{
    public override void Awake()
    {
        id = 7;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.GetComponent<PlayerWeaponry>().SwapWeapon(1);
    }
}
