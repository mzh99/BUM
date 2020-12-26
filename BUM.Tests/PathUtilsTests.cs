using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BUM.Tests {

   [TestClass]
   public class VersioningTests {
      private static readonly string Stamp1 = "20210102030405";
      private static readonly string BackupLoc1 = @"g:\backups";
      private static readonly string TestFile1 = @"c:\test\file1.txt";
      private static readonly string TestFile1Hash = "9238e77b7213d74";

      private static readonly string HistFolder = "Hist";
      private static readonly string TrashFolder = "Trash";
      private static readonly string SegSep = "_";

      [TestMethod]
      public void HashedVerBasicWorks() {
         string verName = PathUtils.HashedVersionName(TestFile1, BackupLoc1, Stamp1, SegSep);
         // should match: "g:\backups\9238e77b7213d74_file1_20210102030405.txt"
         Assert.AreEqual(BackupLoc1 + Path.DirectorySeparatorChar + TestFile1Hash + SegSep + "file1" + SegSep + Stamp1 + ".txt", verName);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void HashedVerWithNullFileRaisesArgumentException() {
         PathUtils.HashedVersionName(null, BackupLoc1, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void HashedVerWithEmptyFileRaisesArgumentException() {
         PathUtils.HashedVersionName(string.Empty, BackupLoc1, Stamp1, SegSep);
      }

      [TestMethod]
      public void VersionedBaseWorks() {
         string verName = PathUtils.GetVersionedBaseName(TestFile1, Stamp1, SegSep);
         Assert.AreEqual("file1_20210102030405.txt", verName);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void VersionedWithNullFileRaisesException() {
         PathUtils.GetVersionedBaseName(null, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void VersionedWithEmptyFileRaisesException() {
         PathUtils.GetVersionedBaseName(string.Empty, Stamp1, SegSep);
      }

      [TestMethod]
      public void HistBasicWorks() {
         string verName = PathUtils.GetVersionedHistFileName(TestFile1, HistFolder, BackupLoc1, Stamp1, SegSep);
         Assert.AreEqual(BackupLoc1 + Path.DirectorySeparatorChar + HistFolder + @"\test\file1_" + Stamp1 + ".txt", verName);
      }

      [TestMethod]
      public void HistBasicWithTrailingSlashOnDestWorks() {
         string verName = PathUtils.GetVersionedHistFileName(TestFile1, HistFolder, BackupLoc1 + Path.DirectorySeparatorChar, Stamp1, SegSep);
         Assert.AreEqual(BackupLoc1 + Path.DirectorySeparatorChar + HistFolder + @"\test\file1_" + Stamp1 + ".txt", verName);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void HistWithNullDestRaisesArgumentException() {
         PathUtils.GetVersionedHistFileName(TestFile1, HistFolder, null, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void HistWithEmptyDestRaisesArgumentException() {
         PathUtils.GetVersionedHistFileName(TestFile1, HistFolder, string.Empty, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void HistWithNullFileRaisesArgumentException() {
         PathUtils.GetVersionedHistFileName(null, HistFolder, BackupLoc1, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void HistWithEmptyFileRaisesArgumentException() {
         PathUtils.GetVersionedHistFileName(string.Empty, HistFolder, BackupLoc1, Stamp1, SegSep);
      }

      [TestMethod]
      public void TrashVerBasicWorks() {
         string verName = PathUtils.GetVersionedTrashFileName(@"g:\backups\test\file1.txt", TrashFolder, BackupLoc1, Stamp1, SegSep);
         Assert.AreEqual(BackupLoc1 + Path.DirectorySeparatorChar + TrashFolder + @"\test\file1_" + Stamp1 + ".txt", verName);
      }

      [TestMethod]
      public void TrashVerBasicWithTrailingSlashOnDestWorks() {
         string verName = PathUtils.GetVersionedTrashFileName(@"g:\backups\test\file1.txt", TrashFolder, BackupLoc1 + Path.DirectorySeparatorChar, Stamp1, SegSep);
         Assert.AreEqual(BackupLoc1 + Path.DirectorySeparatorChar + TrashFolder + @"\test\file1_" + Stamp1 + ".txt", verName);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TrashWithNullDestRaisesArgumentException() {
         PathUtils.GetVersionedTrashFileName(@"g:\backups\test\file1.txt", TrashFolder, null, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TrashWithEmptyDestRaisesArgumentException() {
         PathUtils.GetVersionedTrashFileName(@"g:\backups\test\file1.txt", TrashFolder, string.Empty, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TrashWithFileNotChildOfDestRaisesArgumentException() {
         PathUtils.GetVersionedTrashFileName(@"g:\file1.txt", TrashFolder, BackupLoc1, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TrashWithNullFileRaisesArgumentException() {
         PathUtils.GetVersionedTrashFileName(null, TrashFolder, BackupLoc1, Stamp1, SegSep);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TrashWithEmptyFileRaisesArgumentException() {
         PathUtils.GetVersionedTrashFileName(string.Empty, TrashFolder, BackupLoc1, Stamp1, SegSep);
      }

   }

   [TestClass]
   public class GraftTests {

      [TestMethod]
      public void GraftRootToRootWorks() {
         Assert.AreEqual(@"e:\", PathUtils.GraftPath(@"c:\", @"e:\"));
      }

      [TestMethod]
      public void GraftRootFileToRootWorks() {
         Assert.AreEqual(@"e:\file1.txt", PathUtils.GraftPath(@"c:\file1.txt", @"e:\"));
      }

      [TestMethod]
      public void GraftRootToRootWithoutTrailingSlashesWorks() {
         Assert.AreEqual(@"e:\", PathUtils.GraftPath(@"c:", @"e:"));
      }

      [TestMethod]
      public void GraftRootFileToRootWithoutTrailingSlashWorks() {
         Assert.AreEqual(@"e:\file1.txt", PathUtils.GraftPath(@"c:\file1.txt", @"e:"));
      }

      [TestMethod]
      public void GraftRootToNonRootWorks() {
         Assert.AreEqual(@"e:\data\", PathUtils.GraftPath(@"c:\", @"e:\data"));
      }

      [TestMethod]
      public void GraftRootFileToNonRootWorks() {
         Assert.AreEqual(@"e:\data\file1.txt", PathUtils.GraftPath(@"c:\file1.txt", @"e:\data"));
      }

      [TestMethod]
      public void GraftRootWithNoSlashToNonRootWorks() {
         Assert.AreEqual(@"e:\data\", PathUtils.GraftPath(@"c:", @"e:\data"));
      }

      [TestMethod]
      public void GraftRootWithSlashToNonRootWorks() {
         Assert.AreEqual(@"e:\data\", PathUtils.GraftPath(@"c:\", @"e:\data"));
      }

      [TestMethod]
      public void GraftRootToNonRooWithTrailingSlashWorks() {
         Assert.AreEqual(@"e:\data\", PathUtils.GraftPath(@"c:\", @"e:\data\"));
      }

      [TestMethod]
      public void GraftPathWithNoslashWorks() {
         Assert.AreEqual(@"e:\edata\cdata", PathUtils.GraftPath(@"c:\cdata", @"e:\edata"));
      }

      [TestMethod]
      public void GraftPathWithSlashesWorks() {
         Assert.AreEqual(@"e:\edata\cdata\", PathUtils.GraftPath(@"c:\cdata\", @"e:\edata\"));
      }

      [TestMethod]
      public void GraftFileToPathWithNoSlashWorks() {
         Assert.AreEqual(@"e:\edata\cdata\file1.txt", PathUtils.GraftPath(@"c:\cdata\file1.txt", @"e:\edata"));
      }

      [TestMethod]
      public void GraftFileToPathWithSlashWorks() {
         Assert.AreEqual(@"e:\edata\cdata\file1.txt", PathUtils.GraftPath(@"c:\cdata\file1.txt", @"e:\edata\"));
      }

      [TestMethod]
      public void GraftSourceEmptyReturnsDest() {
         Assert.AreEqual(@"e:\", PathUtils.GraftPath(string.Empty, @"e:\"));
      }

      [TestMethod]
      public void GraftRelativePathSingleWorks() {
         Assert.AreEqual(@"e:\test", PathUtils.GraftPath(@"\test", @"e:\"));
      }

      [TestMethod]
      public void GraftRelativePathSingleWithTrailingSlashWorks() {
         Assert.AreEqual(@"e:\test\", PathUtils.GraftPath(@"\test\", @"e:\"));
      }

      [TestMethod]
      public void GraftRelativePathMultWorks() {
         Assert.AreEqual(@"e:\root\test\sub1", PathUtils.GraftPath(@"\test\sub1", @"e:\root"));
      }

      [TestMethod]
      public void GraftRelativePathFileWorks() {
         Assert.AreEqual(@"e:\root\test\sub1\dummy.txt", PathUtils.GraftPath(@"\test\sub1\dummy.txt", @"e:\root"));
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void GraftDestEmptyRaisesArgumentException() {
         Assert.AreEqual(string.Empty, PathUtils.GraftPath(@"c:\cdata\file1.txt", string.Empty));
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void GraftDestNullRaisesArgumentException() {
         Assert.AreEqual(string.Empty, PathUtils.GraftPath(@"c:\cdata\file1.txt", null));
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void GraftSourceNullRaisesArgumentException() {
         Assert.AreEqual(string.Empty, PathUtils.GraftPath(null, @"e:\"));
      }

   }

}
