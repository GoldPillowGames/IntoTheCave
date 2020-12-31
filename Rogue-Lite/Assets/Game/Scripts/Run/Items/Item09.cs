public class Item09 : Item
{
    public override void Start()
    {
        id = 9;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.hasSpecialSkillOverpowered = true;
    }
}
