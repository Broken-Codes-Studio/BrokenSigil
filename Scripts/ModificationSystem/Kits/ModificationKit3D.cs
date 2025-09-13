namespace BrokenSigilCollection.Modification;

using Godot;
using System;

using BrokenSigilCollection.Interface;
using System.Collections.Generic;
using System.Linq;
using BrokenSigilCollection.Utility;

public abstract partial class ModificationKit3D : Kit3D<IPart>, IIdentification<ushort>, IType<ushort>
{
    #region Signals

    #endregion

    #region Actions

    public Action<IPart> OnPartAdded;
    public Action<IPart> OnPartRemoved;
    public Action<IPart> OnPartReplaced;
    #endregion

    public abstract ushort ID { get; protected set; }
    public abstract ushort Type { get; protected set; }

    protected abstract Dictionary<StringName, IPart> parts { get; set; }
    protected abstract Dictionary<StringName, Slot3D> slots { get; set; }
    [Export]
    protected Godot.Collections.Dictionary<string, Variant> blackboard { get; set; } = new();
    protected List<string> blackList { get; set; } = new();

    public override int Count => parts.Count;
    public int SlotCount => slots.Count;

    public IPart this[StringName slotName]
    {
        get => parts[slotName];
        set => Add(value);
    }

    public override void _EnterTree()
    {
        Construct();
    }

    public T GetBlackboardValue<[MustBeVariant] T>(string name) => blackboard[name].As<T>();
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

    public override bool Contains(IPart item) => parts.ContainsKey(item.SlotName) && parts[item.SlotName].ID == item.ID;
    public bool Contains(StringName slotName) => parts.ContainsKey(slotName);

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

    public bool ContainsSlot(StringName slotName) => slots.ContainsKey(slotName);

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

    public bool IsSlotFilled(StringName slotName) => slots[slotName].Filled;

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

    public override void CopyTo(IPart[] array, int arrayIndex) => parts.Values.CopyTo(array, arrayIndex);

    public override IEnumerator<IPart> GetEnumerator() => parts.Values.GetEnumerator();

    public bool Equals(IType<ushort> other) => Type.Equals(other.Type);

    public bool Equals(IIdentification<ushort> other) => ID.Equals(other.ID);

    public bool IsSimular(IType<ushort> other) => (Type & other.Type) != 0;

    private void _add(IPart part)
    {
        parts.Add(part.SlotName, part);
        AddChild(part as Node, forceReadableName: true);

        Construct();
    }

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

    private bool _checkBlacklist(IPart part)
    {
        foreach (var black in blackList)
        {
            if (_checkSyntax(part, black))
                return false;
        }

        return true;
    }

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

    private bool _checkDependencies(Slot slot)
    {
        foreach (var dep in slot.Dependencies)
        {
            if (slots[dep].Filled)
                return true;
        }

        return false;
    }

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

    private Vector3 _getSlotPosition(StringName slotName)
    {
        var slot = slots[slotName];
        return _getSlotPosition(slot);
    }

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
