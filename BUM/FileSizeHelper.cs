using System;

namespace BUM {

   public static class FileSizeHelper {

      public static long KB = 1024;
      public static long MB = KB * 1024;
      public static long GB = MB * 1024;
      public static long TB = GB * 1024;

      public static string LongAsSizeStrBestApprox(long bytes, int numDecDigs = 2) {
         // go from highest to lowest for best number
         if (bytes >= TB)
            return TBFromBytes(bytes, numDecDigs).ToString() + " TB";
         if (bytes >= GB)
            return GBFromBytes(bytes, numDecDigs).ToString() + " GB";;
         if (bytes >= MB)
            return MBFromBytes(bytes, numDecDigs).ToString() + " MB";;
         return KBFromBytes(bytes, numDecDigs).ToString() + " KB";;
      }

      /// <summary>Calculate KB from a long</summary>
      /// <param name="bytes">number of total bytes to convert</param>
      /// <param name="numDecDigs">number of decimal digits to round to; default is two</param>
      /// <returns>floating point rounded to numDecDigs decimal digits</returns>
      public static double KBFromBytes(long bytes, int numDecDigs = 2) {
         return Math.Round(((double) bytes / (double) KB), numDecDigs);
      }

      /// <summary>Calculate MB from a long</summary>
      /// <param name="bytes">number of total bytes to convert</param>
      /// <param name="numDecDigs">number of decimal digits to round to; default is two</param>
      /// <returns>floating point rounded to numDecDigs decimal digits</returns>
      public static double MBFromBytes(long bytes, int numDecDigs = 2) {
         return Math.Round(((double) bytes / (double) MB), numDecDigs);
      }

      /// <summary>Calculate GB from a long</summary>
      /// <param name="bytes">number of total bytes to convert</param>
      /// <param name="numDecDigs">number of decimal digits to round to; default is two</param>
      /// <returns>floating point rounded to numDecDigs decimal digits</returns>
      public static double GBFromBytes(long bytes, int numDecDigs = 2) {
         return Math.Round(((double) bytes / (double) GB), numDecDigs);
      }

      /// <summary>Calculate TB from a long</summary>
      /// <param name="bytes">number of total bytes to convert</param>
      /// <param name="numDecDigs">number of decimal digits to round to; default is two</param>
      /// <returns>floating point rounded to numDecDigs decimal digits</returns>
      public static double TBFromBytes(long bytes, int numDecDigs = 2) {
         return Math.Round(((double) bytes / (double) TB), numDecDigs);
      }

   }

}
