namespace FoxSnipTool {
    public class CombBoxItem {
        private string _text = null;
        private object _value = null;
        public string Text { get { return this._text; } set { this._text = value; } }
        public object Value { get { return this._value; } set { this._value = value; } }
        public override string ToString() {
            return this._text;
        }
    }
}
