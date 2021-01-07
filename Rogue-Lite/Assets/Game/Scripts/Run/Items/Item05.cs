public class Item05 : Item
{
    public override void Awake()
    {
        id = 5;
        base.Awake();
    }

    // 25% current gold
    public override void OnPickUpItem(PlayerStatus player)
    {
        Config.data.gold += Config.data.gold * 10 / 100;
    }
}
