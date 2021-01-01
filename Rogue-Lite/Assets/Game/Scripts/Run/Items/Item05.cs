public class Item05 : Item
{
    public override void Awake()
    {
        id = 5;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.gold += player.gold * 25 / 100;
    }
}
