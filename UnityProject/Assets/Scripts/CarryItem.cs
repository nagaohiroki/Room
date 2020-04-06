public class CarryItem : CheckObject
{
	public override void Check(Player inPlayer)
	{
		base.Check(inPlayer);
		inPlayer.TakeHand(this);
	}
}
