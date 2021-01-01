public class Item23 : Item
{
    public override void Awake()
    {
        id = 23;
        base.Awake();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.spawnGrenadeWhenRolls = true;
    }
}
