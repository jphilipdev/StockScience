namespace StockScience.PriceApi.Utils
{
    public static class DateTimeUtils
    {
        /// <summary>
        /// Round DateTime, e.g. to nearest second by passing TimeSpan.TicksPerSecond
        /// </summary>
        public static DateTime Trim(this DateTime date, long ticks)
        {
            return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
        }
    }
}
