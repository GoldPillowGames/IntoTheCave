public class Item20 : Item
{
    public override void Start()
    {
        id = 20;
        base.Start();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.healthSteal -= 3;
        player.damage += player.damage * 10 / 100;
    }
}
