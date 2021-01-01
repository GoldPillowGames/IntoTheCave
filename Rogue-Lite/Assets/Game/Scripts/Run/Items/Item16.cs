public class Item16 : Item
{
    public override void Awake()
    {
        id = 16;
        base.Awake();
    }

    // Less Initial Life For Enemies
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.lessInitialLifeForEnemies += 10;
    }
}
