namespace BrokenSigilCollection.Interface;

/// <summary>
/// Enum representing rarity levels.
/// </summary>
public enum RarityEnum : byte
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5,
    Mythical = 6,
}

/// <summary>
/// Interface for objects with a rarity.
/// </summary>
public interface IRarity
{
    /// <summary>
    /// Gets the rarity of the object.
    /// </summary>
    public RarityEnum Rarity { get; }
}
