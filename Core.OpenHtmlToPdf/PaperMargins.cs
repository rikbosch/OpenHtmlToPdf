namespace Core.OpenHtmlToPdf
{
    public sealed class PaperMargins
    {
        private readonly Length _top;
        private readonly Length _right;
        private readonly Length _bottom;
        private readonly Length _left;

        private PaperMargins(Length allMargins)
            : this(allMargins, allMargins, allMargins, allMargins)
        {
        }

        private PaperMargins(Length top, Length right, Length bottom, Length left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
        }

        public static PaperMargins All(Length allMargins) => new PaperMargins(allMargins);

        public static PaperMargins None() => new PaperMargins(Length.Zero());

        public PaperMargins Top(Length top) => new PaperMargins(top, _right, _bottom, _left);

        public PaperMargins Right(Length right) => new PaperMargins(_top, right, _bottom, _left);

        public PaperMargins Botton(Length botton) => new PaperMargins(_top, _right, botton, _left);

        public PaperMargins Left(Length left) => new PaperMargins(_top, _right, _bottom, left);

        public static implicit operator PaperMargins(Length allMargins)
        {
            return new PaperMargins(allMargins);
        }

        public string TopSetting => _top.SettingString;

        public string RightSetting => _right.SettingString;

        public string BottomSetting => _bottom.SettingString;

        public string LeftSetting => _left.SettingString;
    }
}