using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SelectListItemModel
    {
        private int _value;
        private string _text;
        private bool _selected;
        private bool _disabled;


        public SelectListItemModel() { }

        public SelectListItemModel(string text, int value)
        {
            this._text = text;
            this._value = value;
        }

        public SelectListItemModel(string text, string value)
        {
            this._text = text;
            int id = 0;

            if (int.TryParse(value, out id))
                this._value = id;
        }

        public SelectListItemModel(string text, int value, bool selected)
        {
            this._text = text;
            this._value = value;
            this._selected = selected;
        }

        public SelectListItemModel(string text, int value, bool selected, bool disabled)
        {
            this._text = text;
            this._value = value;
            this._disabled = disabled;
        }

        public int Value { get => this._value; set => this._value = value; }
        public string Text { get => this._text; set => this._text = value; }
        public bool Selected { get => this._selected; set => this._selected = value; }
        public bool Disable { get => this._disabled; set => this._disabled = value; }

    }
}
