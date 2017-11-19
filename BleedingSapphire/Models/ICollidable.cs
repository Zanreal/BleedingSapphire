using System;
namespace BleedingSapphire.Models
{
    internal interface ICollidable
    {
        float Mass { get; }
        bool Fixed { get; }
    }
}
