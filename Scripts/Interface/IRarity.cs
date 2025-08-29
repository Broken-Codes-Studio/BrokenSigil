namespace BrokenSigilCollection.Interface;

public enum RarityEnum : byte
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5,
    Mythical = 6,

}

public interface IRarity
{
    public RarityEnum Rarity { get; }
}
