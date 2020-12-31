public class Item23 : Item
{
    public override void Start()
    {
        id = 23;
        base.Start();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.spawnGrenadeWhenRolls = true;
    }
}
