namespace BrokenSigilCollection.Modification;

using Godot;
using System;

using BrokenSigilCollection.Interface;
using System.Collections.Generic;
using System.Linq;
using BrokenSigilCollection.Utility;

/// <summary>
/// Abstract base class for managing 3D modification kits, handling parts and slots.
/// </summary>
public abstract partial class ModificationKit3D : Kit3D<IPart>, IIdentification<ushort>, IType<ushort>
{
    #region Signals
    // Signals region (currently empty)
    #endregion

    #region Actions
    /// <summary>
    /// Invoked when a part is added.
    /// </summary>
    public Action<IPart> OnPartAdded;
    /// <summary>
    /// Invoked when a part is removed.
    /// </summary>
    public Action<IPart> OnPartRemoved;
    /// <summary>
    /// Invoked when a part is replaced.
    /// </summary>
    public Action<IPart> OnPartReplaced;
    #endregion

    /// <summary>
    /// Unique identifier for the kit.
    /// </summary>
    public abstract ushort ID { get; protected set; }
    /// <summary>
    /// Type identifier for the kit.
    /// </summary>
    public abstract ushort Type { get; protected set; }

    /// <summary>
    /// Dictionary of parts by slot name.
    /// </summary>
    protected abstract Dictionary<StringName, IPart> parts { get; set; }
    /// <summary>
    /// Dictionary of slots by slot name.
    /// </summary>
    protected abstract Dictionary<StringName, Slot3D> slots { get; set; }
    /// <summary>
    /// Blackboard for storing arbitrary data.
    /// </summary>
    [Export]
    protected Godot.Collections.Dictionary<string, Variant> blackboard { get; set; } = new();
    /// <summary>
    /// List of blacklisted items.
    /// </summary>
    protected List<string> blackList { get; set; } = new();

    /// <summary>
    /// Number of parts in the kit.
    /// </summary>
    public override int Count => parts.Count;
    /// <summary>
    /// Number of slots in the kit.
    /// </summary>
    public int SlotCount => slots.Count;

    /// <summary>
    /// Indexer to get/set part by slot name.
    /// </summary>
    public IPart this[StringName slotName]
    {
        get => parts[slotName];
        set => Add(value);
    }

    /// <summary>
    /// Called when node enters the scene tree.
    /// </summary>
    public override void _EnterTree()
    {
        Construct();
    }

    /// <summary>
    /// Gets a value from the blackboard.
    /// </summary>
    public T GetBlackboardValue<[MustBeVariant] T>(string name) => blackboard[name].As<T>();

    /// <summary>
    /// Tries to get a value from the blackboard.
    /// </summary>
    public bool CheckGetBlackboardValue<[MustBeVariant] T>(string name, out T value)
    {
        if (!blackboard.ContainsKey(name))
        {
            value = default(T);
            return false;
        }

        value = blackboard[name].As<T>();
        return true;
    }

    /// <summary>
    /// Adds a part to the kit if compatible and no conflicts.
    /// </summary>
    public override void Add(IPart item)
    {
        if (!Compatible(item))
            return;

        if (CheckConflict(item))
            return;

        if (slots[item.SlotName].Filled)
            return;

        _add(item);

    }

    /// <summary>
    /// Replaces an existing part in the kit.
    /// </summary>
    protected virtual IPart Replace(IPart item)
    {
        if (!Compatible(item))
            return null;

        if (CheckConflict(item))
            return null;

        if (slots[item.SlotName].Filled)
            return _replace(item);
        else
            _add(item);

        return null;
    }

    /// <summary>
    /// Removes a part from the kit, optionally freeing its node.
    /// </summary>
    public bool Remove(IPart item, bool queueFree)
    {
        if (item is Node part)
        {
            if (queueFree)
                part.QueueFree();
            else
                RemoveChild(part);
        }

        return Remove(item);
    }

    /// <summary>
    /// Removes a part from the kit.
    /// </summary>
    public override bool Remove(IPart item)
    {
        if (Contains(item) && !slots[item.SlotName].Main)
        {
            parts.Remove(item.SlotName);

            Construct();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes a part by slot name, optionally freeing its node.
    /// </summary>
    public bool Remove(StringName slotName, bool queueFree)
    {
        if (parts[slotName] is Node part)
        {
            if (queueFree)
                part.QueueFree();
            else
                RemoveChild(part);
        }

        return Remove(slotName);
    }

    /// <summary>
    /// Removes a part by slot name.
    /// </summary>
    public bool Remove(StringName slotName)
    {
        if (Contains(slotName) && !slots[slotName].Main)
        {
            var part = parts[slotName] as Node;

            parts.Remove(slotName);

            if (part.GetParent() is not null)
                RemoveChild(part);
            else
                part.QueueFree();

            Construct();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the kit contains the specified part.
    /// </summary>
    public override bool Contains(IPart item) => parts.ContainsKey(item.SlotName) && parts[item.SlotName].ID == item.ID;
    /// <summary>
    /// Checks if the kit contains a part in the specified slot.
    /// </summary>
    public bool Contains(StringName slotName) => parts.ContainsKey(slotName);

    /// <summary>
    /// Checks for conflicts with the specified part.
    /// </summary>
    public bool CheckConflict(IPart item)
    {
        foreach (var part in parts.Values)
        {
            foreach (var Incompatible in part.Incompatibles)
            {
                if (_checkSyntax(item, Incompatible))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks for conflicts and returns conflicting parts.
    /// </summary>
    public bool CheckConflict(IPart item, out IPart[] conflictParts)
    {
        List<IPart> conflictList = new(parts.Count);

        foreach (var part in parts.Values)
        {
            foreach (var Incompatible in part.Incompatibles)
            {
                if (_checkSyntax(item, Incompatible))
                    conflictList.Add(part);
            }
        }

        conflictParts = conflictList.ToArray();

        return conflictParts.Length > 0;
    }

    /// <summary>
    /// Checks if the kit contains the specified slot.
    /// </summary>
    public bool ContainsSlot(StringName slotName) => slots.ContainsKey(slotName);

    /// <summary>
    /// Checks if the part is compatible with the kit.
    /// </summary>
    public bool Compatible(IPart item)
    {

        if (!IsSimular(item))
            return false;

        if (item.Filters.Length > 0 && !_checkFilters(item))
            return false;

        if (blackList.Count > 0 && !_checkBlacklist(item))
            return false;

        if (!_checkDependencies(slots[item.SlotName]))
            return false;

        return true;

    }

    /// <summary>
    /// Checks if the slot is filled.
    /// </summary>
    public bool IsSlotFilled(StringName slotName) => slots[slotName].Filled;

    /// <summary>
    /// Clears all parts and the blackboard.
    /// </summary>
    public override void Clear()
    {
        foreach (var kv in slots)
        {
            var slot = kv.Value;

            if (!slot.Main)
            {
                (parts[slot.Name] as Node).QueueFree();
                parts.Remove(slot.Name);
            }
        }
        blackboard.Clear();
    }

    /// <summary>
    /// Constructs the kit, updating slot states and blackboard.
    /// </summary>
    public override void Construct()
    {
        // initialize Filled flag from whether there is a part in the slot
        foreach (var kv in slots)
        {
            var slot = kv.Value;
            slot.Filled = parts.ContainsKey(slot.Name);
        }

        _positionDependencies();

        _fillBlackboard();

    }

    /// <summary>
    /// Copies parts to an array.
    /// </summary>
    public override void CopyTo(IPart[] array, int arrayIndex) => parts.Values.CopyTo(array, arrayIndex);

    /// <summary>
    /// Gets an enumerator for the parts.
    /// </summary>
    public override IEnumerator<IPart> GetEnumerator() => parts.Values.GetEnumerator();

    /// <summary>
    /// Checks if types are equal.
    /// </summary>
    public bool Equals(IType<ushort> other) => Type.Equals(other.Type);

    /// <summary>
    /// Checks if IDs are equal.
    /// </summary>
    public bool Equals(IIdentification<ushort> other) => ID.Equals(other.ID);

    /// <summary>
    /// Checks if types are similar.
    /// </summary>
    public bool IsSimular(IType<ushort> other) => (Type & other.Type) != 0;

    /// <summary>
    /// Internal add logic for a part.
    /// </summary>
    private void _add(IPart part)
    {
        parts.Add(part.SlotName, part);
        AddChild(part as Node, forceReadableName: true);

        Construct();
    }

    /// <summary>
    /// Internal replace logic for a part.
    /// </summary>
    private IPart _replace(IPart part)
    {

        IPart prevPart = parts[part.SlotName];

        if (Remove(prevPart))
        {
            parts[part.SlotName] = part;
            AddChild(part as Node, forceReadableName: true);

            Construct();

            return prevPart;
        }

        return null;

    }

    /// <summary>
    /// Checks if part is blacklisted.
    /// </summary>
    private bool _checkBlacklist(IPart part)
    {
        foreach (var black in blackList)
        {
            if (_checkSyntax(part, black))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if part passes filters.
    /// </summary>
    private bool _checkFilters(IPart part)
    {
        bool isInFilter = false;

        foreach (string filter in part.Filters)
        {
            if (filter[0] == '@')
            {
                ushort id = ushort.Parse(filter.Remove(0, 1));

                if (ID == id)
                {
                    isInFilter = true;
                    break;
                }

                continue;
            }
            else
            {
                if (Name == filter)
                {
                    isInFilter = true;
                    break;
                }
            }
        }

        return isInFilter;
    }

    /// <summary>
    /// Checks slot dependencies.
    /// </summary>
    private bool _checkDependencies(Slot slot)
    {
        foreach (var dep in slot.Dependencies)
        {
            if (slots[dep].Filled)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks syntax for incompatibility or blacklist.
    /// </summary>
    private bool _checkSyntax(IPart part, string text)
    {
        if (text[0] == '@')
        {
            ushort id = ushort.Parse(text.Remove(0, 1));

            if (part.ID == id)
                return true;
        }
        else if (text[0] == '#')
        {
            StringName tag = text.Remove(0, 1);

            if (part.ContainsTag(tag))
                return true;
        }
        else
        {
            if ((part as Node).Name == text)
                return true;
        }

        return false;
    }


    //TODO: Optimization. Make it catch positions and update them only if needed.
    //TODO: Use Transform3D for better part placement.
    /// <summary>
    /// Positions parts based on slot dependencies.
    /// </summary>
    private void _positionDependencies()
    {

        // Process main slots first so anchors are set before children.
        // Then process other slots.
        var orderedSlots = slots.Values
            .OrderByDescending(s => s.Main) // Main true first
            .ThenBy(s => s.Priority)
            .ToArray();

        foreach (var slot in orderedSlots)
        {

            // Determine the chosen dependency:
            // * pick the first dependency in slot.Dependencies that is already filled.
            // * if none found:
            //   remove the part

            if (!slot.Filled)
            {
                //TODO: Remove part
                continue;
            }

            if (parts[slot.Name] is Node3D part3D)
                part3D.Position = _getSlotPosition(slot) * Transform;

        }

    }

    /// <summary>
    /// Fills the blackboard with part data.
    /// </summary>
    private void _fillBlackboard()
    {
        var orderedParts = parts.Values
            .OrderByDescending(p => p.Priority) // Main true first
            .ToArray();

        blackboard.Clear();

        foreach (var part in orderedParts)
        {
            foreach (var kv in part.Blackboard)
            {
                if (kv.Value.VariantType is Variant.Type.String)
                {
                    string text = kv.Value.AsString();

                    if (SigilSyntax.IsSyntaxChar(text[0]))
                    {
                        blackboard[kv.Key] = SigilSyntax.ProcessNumbers(blackboard[kv.Key], text);
                        continue;
                    }

                }

                blackboard[kv.Key] = kv.Value;

            }
        }
    }

    /// <summary>
    /// Gets the position of a slot by name.
    /// </summary>
    private Vector3 _getSlotPosition(StringName slotName)
    {
        var slot = slots[slotName];
        return _getSlotPosition(slot);
    }

    /// <summary>
    /// Gets the position of a slot.
    /// </summary>
    private Vector3 _getSlotPosition(Slot3D slot)
    {
        Vector3 position = slot.Position;

        if (!slot.Main)
            foreach (var depName in slot.Dependencies)
            {
                var depSlot = slots[depName];
                if (depSlot.Filled)
                {

                    Vector3 offset = Vector3.Zero;
                    if (parts[depSlot.Name] is ISubParts3D subPart)
                        offset = subPart.SubPartsLocation[slot.Name];

                    if (depSlot.Main)
                        position += depSlot.Position + offset;
                    else
                        position += _getSlotPosition(depSlot) + offset;

                    break;

                }

            }

        return position;
    }

}
