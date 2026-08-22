namespace Currency_Convert.Service
{
  public static class FormatData
  {
    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Wednesday, August 19</returns>
    public static string ToComplicateDay(string input)  //2026-08-19
    {
      var index = input.IndexOf("T");
      input = input.Substring(0, index);
      DateTime dt = DateTime.Parse(input);
      return dt.ToString("dddd, MMMM d");
    }

    /// <summary>
    /// Converts a date string to a short day format.
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Today or the abbreviated day name (e.g., Mon, Tue, Wed)</returns>
    public static string ToShortDay(string input)  //2026-08-19T09:30
    {

      var index = input.IndexOf("T");
      input = input.Substring(0, index);
      DateTime dt = DateTime.Parse(input).Date;

      if (dt == DateTime.Today)
        return "Today";

      return dt.ToString("ddd");
    }

    /// <summary>   retuns the time in a 12-hour format with AM/PM. </summary>
    /// <param name="input"></param>
    /// <returns>The time in a 12-hour format with AM/PM (e.g., 9 AM, 3 PM)</returns>
    public static string ToComplicateTime(string input)  //2026-08-19T09:30
    {
      var index = input.IndexOf("T") + 1;
      input = input.Substring(index);
      DateTime dt = DateTime.Parse(input);
      return dt.ToString("h tt");
    }

  }
}
