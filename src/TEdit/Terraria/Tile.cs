﻿using System;
using System.ComponentModel;
using TEdit.Geometry.Primitives;

namespace TEdit.Terraria
{

    public enum BrickStyle : byte
    {
        [Description("Full Brick")]
        Full = 0x0,
        HalfBrick = 0x1,
        SlopeTopRight = 0x2,
        SlopeTopLeft = 0x3,
        SlopeBottomRight = 0x4,
        SlopeBottomLeft = 0x5,
    }

    public enum LiquidType : byte
    {
        None = 0x0,
        Water = 0x01,
        Lava = 0x02,
        Honey = 0x03
    }

    public enum TileType : int
    {
        DirtBlock = 0,
        StoneBlock = 1,
        Torch = 4,
        Tree = 5,
        Platform = 19,
        Chest = 21,
        Sunflower = 27,
        Chandelier = 34,
        Sign = 55,
        MushroomTree = 72,
        GraveMarker = 85,
        Dresser = 88,
        EbonsandBlock = 112,
        PearlsandBlock = 116,
        CrimsandBlock = 234,
        PlanteraBulb = 238,
        IceByRod = 127,
        TrappedChest = 441,
        Chest2 = 467,
        TrappedChest2 = 468,
        AnnouncementBox = 425,
        TatteredSign = 573,
        ChristmasTree = 171,
        MinecartTrack = 314,
        // Tile Entities
        MannequinLegacy = 128,
        WomannequinLegacy = 269,
        DisplayDoll = 470, // aka Mannequin
        FoodPlatter = 520, // aka plate
        Timer = 144,
        TrainingDummy = 378,
        ItemFrame = 395,
        LogicSensor = 423,
        WeaponRack = 471,
        WeaponRackLegacy = 334,
        HatRack = 475,
        TeleportationPylon = 597,
        DoorClosed = 10,
        DoorOpen = 11,
        TrapDoor = 386,
        TrapDoorOpen = 387,
        TallGate = 388,
        TallGateClosed = 389,
        JunctionBox = 424
    }


    [Serializable]
    public class Tile
    {
        public static readonly Tile Empty = new Tile();

        public bool IsEmpty { get => !IsActive && Wall == 0 && !HasLiquid && !HasWire; }
        public bool HasWire { get => WireBlue || WireRed || WireGreen || WireYellow; }
        public bool HasLiquid { get => LiquidAmount > 0 && LiquidType != LiquidType.None; }

        public bool HasMultipleWires
        {
            get
            {
                if (WireRed && (WireGreen || WireBlue || WireYellow)) return true;
                if (WireGreen && (WireBlue || WireYellow)) return true;
                if (WireBlue && WireYellow) return true;

                return false;
            }
        }

        public bool Actuator;
        public BrickStyle BrickStyle;
        public bool InActive;
        public bool IsActive;
        public byte LiquidAmount;
        public LiquidType LiquidType;
        public byte TileColor;
        public ushort Type;
        public Int16 U;
        public Int16 V;
        public ushort Wall;
        public byte WallColor;
        public bool WireBlue;
        public bool WireGreen;
        public bool WireRed;
        public bool WireYellow;

        [NonSerialized] /* Heathtech */
        public ushort uvTileCache = 0xFFFF; //Caches the UV position of a tile, since it is costly to generate each frame
        [NonSerialized] /* Heathtech */
        public ushort uvWallCache = 0xFFFF; //Caches the UV position of a wall tile
        [NonSerialized] /* Heathtech */
        public byte lazyMergeId = 0xFF; //The ID here refers to a number that helps blocks know whether they are actually merged with a nearby tile
        [NonSerialized] /* Heathtech */
        public bool hasLazyChecked = false; //Whether the above check has taken place

        public static bool StopsWalls(ushort type)
        {
            return type == (ushort)TileType.DoorClosed ||
                   type == (ushort)TileType.DoorOpen ||
                   type == (ushort)TileType.TrapDoor ||
                   type == (ushort)TileType.TrapDoorOpen ||
                   type == (ushort)TileType.TallGate ||
                   type == (ushort)TileType.DoorClosed ||
                   type == (ushort)TileType.TallGateClosed;
        }

        #region BitFlags
        //private const int IsActivePosition = 0;
        //private const int IsLavaPosition = 1;
        //private const int HasWirePosition = 2;

        // Condense all bools to one byte, slow saves ~100mb memory in large world,
        //private byte _flags;
        //public bool IsActive
        //{
        //    set
        //    {
        //        if (value)
        //            _flags = (byte)(_flags | (1 << (IsActivePosition%8)));
        //        else
        //            _flags = (byte)(_flags & ~(1 << (IsActivePosition % 8)));
        //    }
        //    get
        //    {
        //        return (_flags & (1 << (IsActivePosition % 8))) != 0;
        //    }
        //}

        //public bool WireRed
        //{
        //    set
        //    {
        //        if (value)
        //            _flags = (byte)(_flags | (1 << (HasWirePosition % 8)));
        //        else
        //            _flags = (byte)(_flags & ~(1 << (HasWirePosition % 8)));
        //    }
        //    get
        //    {
        //        return (_flags & (1 << (HasWirePosition % 8))) != 0;
        //    }
        //}
        //public bool IsLava
        //{
        //    set
        //    {
        //        if (value)
        //            _flags = (byte)(_flags | (1 << (IsLavaPosition % 8)));
        //        else
        //            _flags = (byte)(_flags & ~(1 << (IsLavaPosition % 8)));
        //    }
        //    get
        //    {
        //        return (_flags & (1 << (IsLavaPosition % 8))) != 0;
        //    }
        //}
        #endregion

        public Vector2Short GetUV() => new Vector2Short(U, V);

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Reset()
        {
            Actuator = false;
            BrickStyle = 0;
            InActive = false;
            IsActive = false;
            LiquidAmount = 0;
            LiquidType = 0;
            TileColor = 0;
            Type = 0;
            U = 0;
            V = 0;
            Wall = 0;
            WallColor = 0;
            WireBlue = false;
            WireGreen = false;
            WireRed = false;
            WireYellow = false;
        }

        protected bool Equals(Tile other)
        {
            return 
                Actuator == other.Actuator &&
                BrickStyle == other.BrickStyle &&
                InActive == other.InActive &&
                IsActive==other.IsActive &&
                LiquidAmount == other.LiquidAmount  &&
                LiquidType == other.LiquidType &&
                TileColor == other.TileColor &&
                Type == other.Type &&
                U == other.U &&
                V == other.V &&                
                Wall == other.Wall &&
                WallColor == other.WallColor &&
                WireBlue == other.WireBlue &&
                WireGreen == other.WireGreen &&
                WireRed == other.WireRed && 
                WireYellow == other.WireYellow;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tile)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsActive.GetHashCode();
                hashCode = (hashCode * 397) ^ Actuator.GetHashCode();
                hashCode = (hashCode * 397) ^ BrickStyle.GetHashCode();
                hashCode = (hashCode * 397) ^ InActive.GetHashCode();
                hashCode = (hashCode * 397) ^ IsActive.GetHashCode();
                hashCode = (hashCode * 397) ^ LiquidAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ LiquidType.GetHashCode();
                hashCode = (hashCode * 397) ^ TileColor.GetHashCode();
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                hashCode = (hashCode * 397) ^ U.GetHashCode();
                hashCode = (hashCode * 397) ^ V.GetHashCode();
                hashCode = (hashCode * 397) ^ Wall.GetHashCode();
                hashCode = (hashCode * 397) ^ WallColor.GetHashCode();
                hashCode = (hashCode * 397) ^ WireBlue.GetHashCode();
                hashCode = (hashCode * 397) ^ WireGreen.GetHashCode();
                hashCode = (hashCode * 397) ^ WireRed.GetHashCode();
                hashCode = (hashCode * 397) ^ WireYellow.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Tile left, Tile right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tile left, Tile right)
        {
            return !Equals(left, right);
        }

        public static bool IsChest(int tileType)
        {
            return tileType == (int)TileType.Chest || tileType == (int)TileType.Dresser || tileType == (int)TileType.Chest2 || tileType == (int)TileType.TrappedChest2 || tileType == (int)TileType.TrappedChest;
        }

        public static bool IsSign(int tileType)
        {
            return tileType == (int)TileType.Sign || tileType == (int)TileType.GraveMarker || tileType == (int)TileType.AnnouncementBox || tileType == (int)TileType.TatteredSign;
        }

        public bool IsTileEntity()
        {
            return Tile.IsTileEntity(this.Type);
        }

        public static bool IsTileEntity(int tileType)
        {
            return tileType == (int)TileType.DisplayDoll
                || tileType == (int)TileType.MannequinLegacy
                || tileType == (int)TileType.WomannequinLegacy
                || tileType == (int)TileType.FoodPlatter
                || tileType == (int)TileType.TrainingDummy
                || tileType == (int)TileType.ItemFrame
                || tileType == (int)TileType.LogicSensor
                || tileType == (int)TileType.WeaponRackLegacy
                || tileType == (int)TileType.WeaponRack
                || tileType == (int)TileType.HatRack
                || tileType == (int)TileType.TeleportationPylon;
        }

    }
}
