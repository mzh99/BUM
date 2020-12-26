using System;
using System.Text;
using System.IO;

namespace BUM {

   public static class FileUtils {

      public static DirectoryInfo CreateDestFolderStructure(string folderName) {
         //if (Directory.Exists(folderName) == false)
         return Directory.CreateDirectory(folderName);
      }

      public static string AppendTextToFile(string data, string fileName, int blockSize = 1024 * 1024) {
         var utfWithNoBom = new UTF8Encoding(false, true);
         try {
            using (StreamWriter file = new StreamWriter(fileName, true, utfWithNoBom, blockSize)) {
               file.Write(data);
            }
         }
         catch (UnauthorizedAccessException) {
            return $"Security permissions error deleting file: {fileName}";
         }
         catch (IOException e) {
            return $"IO error deleting file: {fileName}. Message: {e.Message}";
         }
         return string.Empty;
      }

      public static string DeleteFile(string fileName) {
         try {
            File.Delete(fileName);
         }
         catch (UnauthorizedAccessException) {
            return $"Security permissions error deleting file: {fileName}";
         }
         catch (IOException e) {
            return $"IO error deleting file: {fileName}. Message: {e.Message}";
         }
         return string.Empty;
      }

      /// <summary>Copy a file</summary>
      /// <param name="srcFileName">source file name</param>
      /// <param name="destFileName">destination file name</param>
      /// <param name="matchingCreateTm">flag to copy source file create time stamp to destination file</param>
      /// <param name="matchingLastUpdTm">flag to copy source file last-update time stamp to destination file</param>
      /// <param name="createDestFolders">flag to create folder hierarchy for destination file. If false, path must exist or an error will be returned.</param>
      /// <returns>an empty string for success; otherwise, an error message</returns>
      public static string CopyFile(string srcFileName, string destFileName, bool matchingCreateTm, bool matchingLastUpdTm, bool createDestFolders, int blockSize = 1024 * 1024) {
         DateTime createTm = DateTime.MinValue;
         DateTime lastUpdTm = DateTime.MinValue;
         if (createDestFolders)
            CreateDestFolderStructure(Path.GetDirectoryName(destFileName));
         try {
            if (matchingCreateTm)
               createTm = File.GetCreationTime(srcFileName);
            if (matchingLastUpdTm)
               lastUpdTm = File.GetLastWriteTime(srcFileName);
            // not using File.Copy as it uses a pathetic buffer size
            //File.Copy(srcFileName, destFileName, true);
            // Setup read buffers
            byte[] buffer = new byte[blockSize];  // 1M buffer is blockSize default
            byte[] buffer2 = new byte[blockSize];
            using (FileStream sourceStream = new FileStream(srcFileName, FileMode.Open, FileAccess.Read, FileShare.Read, blockSize))
            using (FileStream destStream = new FileStream(destFileName, FileMode.Create, FileAccess.Write, FileShare.None, blockSize)) {
               long sz = sourceStream.Length;
               // process full blocks of data; files are equal in size so
               while (sz >= blockSize) {
                  ReadFullyIntoBuffer(sourceStream, buffer, blockSize);
                  destStream.Write(buffer, 0, blockSize);
                  sz -= blockSize;
               }
               // process leftover block (if needed)
               if (sz > 0) {
                  int remaining = (int) sz;
                  ReadFullyIntoBuffer(sourceStream, buffer, remaining);
                  destStream.Write(buffer, 0, remaining);
               }
            }
            if (matchingCreateTm)
               File.SetCreationTime(destFileName, createTm);
            if (matchingLastUpdTm)
               File.SetLastWriteTime(destFileName, lastUpdTm);
         }
         catch (UnauthorizedAccessException) {
            return $"Security permissions error copying file {srcFileName} to {destFileName}.";
         }
         catch (IOException e) {
            return $"IO error copying file {srcFileName} to {destFileName}. Error: {e.Message}";
         }
         return string.Empty;
      }

      /// <summary>Compares two files to detect a difference.</summary>
      /// <param name="fileName1">The first file name</param>
      /// <param name="fileName2">The second file name</param>
      /// <returns>True if files are different; otherwise, false</returns>
      public static bool FileInfoDiffers(string fileName1, string fileName2) {
         return (fileName1 == fileName2) ? false : FileInfoDiffers(new FileInfo(fileName1), new FileInfo(fileName2));
      }

      /// <summary>Compares two files to detect a difference.</summary>
      /// <param name="fileInfo">the FileInfo for the first file</param>
      /// <param name="compareFileName">the file name of the file to compare to</param>
      /// <returns>True if files are different; otherwise, false</returns>
      public static bool FileInfoDiffers(FileInfo fileInfo, string compareFileName) {
         return FileInfoDiffers(fileInfo, new FileInfo(compareFileName));
      }

      /// <summary>Compares two files to detect a difference.</summary>
      /// <param name="fileInfo">the Fileinfo for the first file</param>
      /// <param name="fileInfo2">the fileInfo for the second file</param>
      /// <returns>True if files are different; otherwise, false</returns>
      public static bool FileInfoDiffers(FileInfo fileInfo, FileInfo fileInfo2) {
         // if the length or significant times differ, return true
         return (fileInfo.Length != fileInfo2.Length || fileInfo.CreationTime != fileInfo2.CreationTime || fileInfo.LastWriteTime != fileInfo2.LastWriteTime);
      }

      public static bool ContentsDiffer(string filename1, string filename2, int blockSize = 1024 * 1024) {
         var fileInfo1 = new FileInfo(filename1);
         var fileInfo2 = new FileInfo(filename2);
         long sz = fileInfo1.Length;
         if (sz != fileInfo2.Length)   // size differs, so no need to read file contents
            return true;
         // setup read buffers
         byte[] buffer = new byte[blockSize];  // 1M buffer is blockSize default
         byte[] buffer2 = new byte[blockSize];
         using (FileStream fs1 = new FileStream(filename1, FileMode.Open, FileAccess.Read, FileShare.Read, blockSize))
         using (FileStream fs2 = new FileStream(filename2, FileMode.Open, FileAccess.Read, FileShare.Read, blockSize)) {
            // process full blocks of data; files are equal in size so
            while (sz >= blockSize) {
               ReadFullyIntoBuffer(fs1, buffer, blockSize);
               ReadFullyIntoBuffer(fs2, buffer2, blockSize);
               for (int ndx = 0; ndx < blockSize; ndx++) {
                  if (buffer[ndx] != buffer2[ndx])
                     return true;
               }
               sz -= blockSize;
            }
            // process leftover block (if needed)
            if (sz > 0) {
               int remaining = (int) sz;
               ReadFullyIntoBuffer(fs1, buffer, remaining);
               ReadFullyIntoBuffer(fs2, buffer2, remaining);
               for (int ndx = 0; ndx < remaining; ndx++) {
                  if (buffer[ndx] != buffer2[ndx])
                     return true;
               }
            }
         }
         return false;
      }

       /// <summary>Read a number of bytes into a buffer</summary>
      /// <param name="buffer">buffer to read into</param>
      /// <remarks>
      ///   This is needed because Stream.Read is not guaranteed to read the number of requested bytes.
      ///   For example, the following code is incorrect and NOT guaranteed to fill the buffer.
      ///      FileStream fs = File.OpenRead(filename);
      ///      byte[] data = new byte[fs.Length];
      ///      fs.Read (data, 0, data.Length);
      ///   Generally, this shouldn't happen with FileStream, but it's the safest for all streams.
      ///   Refer: https://jonskeet.uk/csharp/readbinary.html
      /// </remarks>
      private static void ReadFullyIntoBuffer(FileStream inStream, byte[] buffer, int numBytesToRead) {
         if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
         int offset = 0;   // starting read offset
         while (numBytesToRead > 0) {
            int numRead = inStream.Read(buffer, offset, numBytesToRead);
            if (numRead <= 0)
               throw new EndOfStreamException($"Premature end of stream reached with {numBytesToRead} bytes left to read");
            offset += numRead;
            numBytesToRead -= numRead;
         }
      }


   }

}
