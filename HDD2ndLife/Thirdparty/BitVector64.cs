using System;
using System.Text;

namespace HDD2ndLife.Thirdparty;

/// <summary>
/// Taken from System.Collections.Specialized.BitVector32.cs
/// </summary>
public struct BitVector64
{
    private long data;

    public BitVector64(long data) => this.data = data;

    public BitVector64(BitVector64 value) => data = value.data;

    public bool this[long singleBit]
    {
        get => (data & singleBit) == (long)(ulong)singleBit;
        set
        {
            if (value)
                data |= singleBit;
            else
                data &= ~singleBit;
        }
    }

    public long this[BitVector64.Section section]
    {
        get => (data >> section.Offset) & section.Mask;

        set
        {
            if (value < 0)
                throw new ArgumentException("Section can't hold negative values");
            if (value > section.Mask)
                throw new ArgumentException("Value too large to fit in section");
            value <<= section.Offset;
            var num = ((long)uint.MaxValue & (long)section.Mask) << section.Offset;
            data = ((long)data & ~num | value & num);

            //data &= ~(section.Mask << section.Offset);
            //data |= (value << section.Offset);
        }
    }

    public long Data => data;

    private static int CountBitsSet(int mask)
    {
        var num = 0;
        for (; ((long)mask & 1) != 0; mask >>= 1)
            ++num;
        return num;
    }

    public static int CreateMask() => BitVector64.CreateMask(0);

    public static int CreateMask(int previous)
    {
        return previous switch
        {
            0 => 1,
            int.MinValue => throw new InvalidOperationException(@"BitVectorFull"),
            _ => previous << 1
        };
    }

    private static int CreateMaskFromHighValue(int highValue)
    {
        var num1 = 32;
        for (; ((long)highValue & (uint)int.MaxValue + 1) == 0; highValue <<= 1)
            --num1;
        uint num2 = 0;
        while (num1 > 0)
        {
            --num1;
            num2 = (uint)((ulong)(uint)(num2 << 1) | 1U);
        }
        return (int)num2;
    }

    public static Section CreateSection(int maxValue) => CreateSectionHelper(maxValue, 0, 0);

    public static Section CreateSection(int maxValue, BitVector64.Section previous) => CreateSectionHelper(maxValue, previous.Mask, previous.Offset);

    private static Section CreateSectionHelper(int maxValue, int priorMask, int priorOffset)
    {
        if (maxValue < 1)
            throw new ArgumentException(nameof(maxValue));
        var offset = (int)(priorOffset + (long)CountBitsSet(priorMask));
        return offset < 64 ? new Section(CreateMaskFromHighValue(maxValue), offset) : throw new InvalidOperationException(@"BitVectorFull");
    }

    public override bool Equals(object o) => o is BitVector64 bitVector64 && data == bitVector64.data;

    public override int GetHashCode() => base.GetHashCode();

    public static string ToString(BitVector64 value)
    {
        var stringBuilder = new StringBuilder(0x2d);
        stringBuilder.Append(@"BitVector64{");
        var data = (ulong)value.data;
        for (var index = 0; index < 0x40; ++index)
        {
            stringBuilder.Append((data & 0x8000000000000000) != 0L ? "1" : "0");
            data <<= 1;
        }
        stringBuilder.Append("}");
        return stringBuilder.ToString();
    }

    public override string ToString() => ToString(this);

    public readonly struct Section
    {
        private readonly int mask;
        private readonly int offset;

        internal Section(int mask, int offset)
        {
            this.mask = mask;
            this.offset = offset;
        }

        public int Mask => mask;

        public int Offset => offset;

        public override bool Equals(object o) => o is Section section && Equals(section);

        public bool Equals(Section obj) =>
            obj.mask == (long)mask && obj.offset == (long)offset;

        public static bool operator ==(Section a, Section b) => a.Equals(b);

        public static bool operator !=(Section a, Section b) => !(a == b);

        public override int GetHashCode() => base.GetHashCode();

        public static string ToString(Section value) => @"Section{0x" +
                                                        Convert.ToString(value.Mask, 16) + ", 0x" +
                                                        Convert.ToString(value.Offset, 16) + "}";

        public override string ToString() => ToString(this);
    }
}