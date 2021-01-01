public class Item02 : Item
{
    public override void Awake()
    {
        id = 2;
        base.Awake();
    }


    public override void OnPickUpItem(PlayerStatus player)
    {
        player.damage += player.damage * 5 / 100;
    }
}
