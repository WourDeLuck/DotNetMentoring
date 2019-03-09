using System;
using System.Collections.Generic;
using System.Linq;
using FileWatch.Interfaces;
using FileWatch.Models;
using FileWatch.Services;
using NUnit.Framework;
using Moq;

namespace FileWatch.Tests
{
	[TestFixture]
    public class FileSystemWatchTests
	{
		private Mock<ISystemFactory> _mockFileSystem;
		private IEnumerable<string> _predefinedFileCollection;
		private FileSystemWatch _fileSystemWatch;

		[SetUp]
	    public void Initialize()
	    {
		    _mockFileSystem = new Mock<ISystemFactory>();

		    _predefinedFileCollection = new List<string>
		    {
			    @"D:\TestFolder\text1.txt",
			    @"D:\TestFolder\myeapp.exe",
			    @"D:\TestFolder\guns.sai",
			    @"D:\TestFolder\OneMoreFolder\fatalities.mp3",
			    @"D:\TestFolder\OneMoreFolder\deltarune554.mp3",
			    @"D:\TestFolder\OneMoreFolder\sodevilish.mp3",
			    @"D:\TestFolder\OneMoreFolder\RIP\springtrap2.png"
		    };
	    }

		[Test]
	    public void FileSystemWatch_NoFilterConditions()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string");
			
			Assert.IsNotNull(collection);
			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count(), 7);
		}

		[TestCase("")]
		[TestCase(null)]
		[Test]
		public void FileSystemWatch_NoFilterCondition_NullOrEmptyString(string str)
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object);

			Assert.Throws(typeof(ArgumentNullException), () => _fileSystemWatch.CreateFileSequence(str));
		}

		[TestCase(".mp3", 3)]
		[TestCase(".exe", 1)]
		[TestCase(".txt", 1)]
		[TestCase(".png", 1)]
		[TestCase(".sai", 1)]
		[Test]
		public void FileSystemWatch_WithFilterCondition_FilterByExtension(string extension, int expectedCount)
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.Extension.Equals(extension));
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, expectedCount);
		}

		[Test]
		public void FileSystemWatch_WithFilterCondition_FilterByExtension_EmptyCollection()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.Extension.Equals(".jpg"));
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsEmpty(collection);
		}

		[Test]
		public void FileSystemWatch_WithFilterCondition_FilterByName()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.Name.Contains("delta"));
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 1);
			Assert.AreEqual(collection[0].Name, "deltarune554.mp3");
		}

		[Test]
		public void FileSystemWatch_WithFilterCondition_FilterByName_CharacterCount()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.Name.Length < 10);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 2);
		}

		[Test]
		public void FileSystemWatch_WithFilterCondition_FilterByParent()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.ParentFolder.Equals(@"D:\TestFolder\OneMoreFolder"));
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 3);
		}

		[Test]
		public void FileSystemWatch_EventDemo_FileFound_Stop()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			_fileSystemWatch.FileFound += EventTestMethods.Event_FileFound_Stop;

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsTrue(collection.Count(x => x.Extension.Equals(".mp3")) == 1);
		}

		[Test]
		public void FileSystemWatch_EventDemo_FileFound_DeleteElement()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			_fileSystemWatch.FileFound += EventTestMethods.Event_FileFound_Delete;

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsFalse(collection.Any(x => x.ParentFolder.Contains("OneMoreFolder")));
		}

		[Test]
		public void FileSystemWatch_EventDemo_FilteredFileFound_Stop()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.Name.Length > 10);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			_fileSystemWatch.FilteredFileFound += EventTestMethods.Event_FilteredFileFound_Stop;

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsTrue(collection.Count(x => x.Extension.Equals(".mp3")) == 1);
		}

		[Test]
		public void FileSystemWatch_EventDemo_FilteredFileFound_DeleteElement()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object, x => x.Name.Length > 10);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedFileCollection);

			_fileSystemWatch.FilteredFileFound += EventTestMethods.Event_FilteredFileFound_Delete;

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsEmpty(collection);
		}
	}
}