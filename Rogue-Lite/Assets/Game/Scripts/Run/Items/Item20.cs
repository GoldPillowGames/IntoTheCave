public class Item20 : Item
{
    public override void Awake()
    {
        id = 20;
        base.Awake();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.healthSteal -= 3;
        player.damage += player.damage * 10 / 100;
    }
}
