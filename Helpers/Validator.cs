namespace Helpers
{
  public static class Validator
  {
    public static int GetZip(string[] args)
    {
      if (!ValidArgs(args))
      {
        Console.WriteLine("Please provide a valid US zip code.");
        Environment.Exit(1);
      }

      return int.Parse(args[0]);
    }

    private static bool ValidArgs(string[] args)
    {
      if (args.Length == 0)
      {
        return false;
      }

			try
      {
        // int.Parse will throw an exception if the string is not a valid integer
        int.Parse(args[0]);

        // US zip codes are 5 digits long
        if (args[0].Length < 5)
        {
          return false;
        }
      }
      catch (System.Exception)
      {
        return false;
      }

      return true;
    }
  }
}