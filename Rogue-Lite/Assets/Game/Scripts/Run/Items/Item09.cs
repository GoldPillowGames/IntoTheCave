public class Item09 : Item
{
    public override void Awake()
    {
        id = 9;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.hasSpecialSkillOverpowered = true;
    }
}
