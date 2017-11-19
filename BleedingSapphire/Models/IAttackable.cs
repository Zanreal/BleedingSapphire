using System;
namespace BleedingSapphire.Models
{
    public interface IAttackable
	{
        int MaxHitPoints { get; }
        int HitPoints { get; }
	}
}
