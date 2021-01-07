public class Item24 : Item
{
    public override void Awake()
    {
        id = 24;
        base.Awake();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.enemiesThreshold += 0.03f;
    }
}
