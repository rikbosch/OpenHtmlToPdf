using System.Globalization;

namespace Core.OpenHtmlToPdf
{
    public sealed class Length
    {
        private readonly double _length;
        private readonly object _unitOfLength;

        private Length(double length, string unitOfLength)
        {
            _length = length;
            _unitOfLength = unitOfLength;
        }

        public static Length Zero() => new Length(0, "");

        public static Length Millimeters(double length) => new Length(length, "mm");

        public static Length Centimeters(double length) => new Length(length, "cm");

        public static Length Inches(double length) => new Length(length, "in");

        public string SettingString => string.Format("{0}{1}", _length.ToString("0.##", CultureInfo.InvariantCulture), _unitOfLength);
    }
}
