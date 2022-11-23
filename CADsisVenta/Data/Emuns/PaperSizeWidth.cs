using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Data.Emuns
{
    public sealed class PaperSizeWidth
    {
        public static readonly PaperSizeWidth I32_characters;//01
        public static readonly PaperSizeWidth I40_characters;//01
        public static readonly PaperSizeWidth I48_characters;//01
        private static readonly List<PaperSizeWidth> valueList;

        private byte _charLenght;
        private string nameSize;
        private int index;
        private PaperSizeWidthEnum enun;

        public PaperSizeWidth()
        {

        }
        public enum PaperSizeWidthEnum : int
        {
            I32_characters = 0,
            I40_characters = 1,
            I48_characters = 2,
        }
        private static readonly PaperSizeWidth[] Values;

        static PaperSizeWidth()
        {
            I32_characters = new PaperSizeWidth("32 caracteres", 0, 32, PaperSizeWidthEnum.I32_characters);
            I40_characters = new PaperSizeWidth("40 caracteres", 1, 40, PaperSizeWidthEnum.I40_characters);
            I48_characters = new PaperSizeWidth("48 caracteres", 2, 48, PaperSizeWidthEnum.I48_characters);
            valueList = new List<PaperSizeWidth>();

            valueList.Add(I32_characters);
            valueList.Add(I40_characters);
            valueList.Add(I48_characters);


        }
        private PaperSizeWidth(string _name, ushort _index, byte charlenth, PaperSizeWidthEnum _enum)
        {
            this._charLenght = charlenth;
            this.nameSize = _name;
            this.index = _index;
            this.enun = _enum;
        }

        public static List<PaperSizeWidth> GetValues()
        {
            return valueList;
        }

        public static PaperSizeWidthEnum GetEnumWithValue(int _value)
        {

            if (_value == -1)
                throw new Exception("the value to search for has not been determined");
            foreach (PaperSizeWidth enumInstance in PaperSizeWidth.valueList)
            {
                if (enumInstance.Index == _value)
                {
                    return enumInstance.enun;
                }
            }
            throw new System.ArgumentException($"Not fount token name: {_value}");

        }

        public static byte GetCharLenght(byte _value)
        {
            foreach (PaperSizeWidth enumInstance in PaperSizeWidth.valueList)
            {
                if (enumInstance.Index == _value)
                {
                    return enumInstance.CharLenght;
                }
            }
            throw new System.ArgumentException($"Not fount token name: {_value}");

        }


        public string NameSize { get => this.nameSize; set => this.nameSize = value; }
        public int Index { get => index; set => index = value; }
        public byte CharLenght { get => _charLenght; set => _charLenght = value; }
        public PaperSizeWidthEnum Emunm { get => this.enun; set => this.enun = value; }
    }
}
