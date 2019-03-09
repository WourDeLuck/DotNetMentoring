using System;
using System.Collections.Generic;
using System.Linq;
using FileWatch.Interfaces;
using FileWatch.Services;
using NUnit.Framework;
using Moq;

namespace FileWatch.Tests
{
	[TestFixture]
	public class DirectorySystemWatchTests
	{
		private Mock<ISystemFactory> _mockFileSystem;
		private IEnumerable<string> _predefinedDirectoryCollection;
		private DirectorySystemWatch _directorySystemWatch;

		[SetUp]
		public void Initialize()
		{
			_mockFileSystem = new Mock<ISystemFactory>();

			_predefinedDirectoryCollection = new List<string>
			   {
				@"D:\TestFolder",
				@"D:\TestFolder\OneMoreFolder",
				@"D:\TestFolder\Whosnext",
				@"D:\TestFolder\Whosnext\Yourenext",
				@"D:\TestFolder\Whosnext\Yourenext\Whome",
				@"D:\TestFolder\Whosnext\Yourenext\Whome\Yesyou",
				@"D:\TestFolder\OneMoreFolder\RIP",
				@"D:\TestFolder\OneMoreFolder\RIP\Sup"
			};
		}

		[Test]
		public void DirectorySystemWatch_NoFilterConditions()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotNull(collection);
			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 8);
		}

		[TestCase("")]
		[TestCase(null)]
		[Test]
		public void DirectorySystemWatch_NoFilterCondition_NullOrEmptyString(string str)
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object);

			Assert.Throws(typeof(ArgumentNullException), () => _directorySystemWatch.CreateFileSequence(str));
		}

		[Test]
		public void DirectorySystemWatch_WithFilterCondition_FilterByName()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object, x => x.Name.Contains("Folder"));
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 2);
		}

		[Test]
		public void DirectorySystemWatch_WithFilterCondition_FilterByName_CharacterCount()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object, x => x.Name.Length < 10);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 6);
		}

		[Test]
		public void DirectorySystemWatch_WithFilterCondition_FilterByParent()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object, x => x.ParentFolder.Contains("Whosnext"));
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.AreEqual(collection.Count, 1);
		}

		[Test]
		public void DirectorySystemWatch_EventDemo_DirectoryFound_Stop()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			_directorySystemWatch.DirectoryFound += EventTestMethods.Event_DirectoryFound_Stop;

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsFalse(collection.Any(x => x.Name.Equals("RIP")));
		}

		[Test]
		public void DirectorySystemWatch_EventDemo_DirectoryFound_DeleteElement()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			_directorySystemWatch.DirectoryFound += EventTestMethods.Event_DirectoryFound_Delete;

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsFalse(collection.Any(x => x.ParentFolder.Contains("OneMoreFolder")));
		}

		[Test]
		public void DirectorySystemWatch_EventDemo_FilteredDirectoryFound_Stop()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object, x => x.Name.Length > 10);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			_directorySystemWatch.FilteredDirectoryFound += EventTestMethods.Event_FilteredDirectoryFound_Stop;

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsFalse(collection.Any(x => x.Name.Equals("RIP")));
		}

		[Test]
		public void DirectorySystemWatch_EventDemo_FilteredDirectoryFound_DeleteElement()
		{
			_directorySystemWatch = new DirectorySystemWatch(_mockFileSystem.Object, x => x.Name.Length > 10);
			_mockFileSystem.Setup(x => x.GetFileSystemContent(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			_directorySystemWatch.FilteredDirectoryFound += EventTestMethods.Event_FilteredDirectoryFound_Delete;

			var collection = _directorySystemWatch.CreateFileSequence(@"Some string").ToList();

			Assert.IsNotEmpty(collection);
			Assert.IsTrue(collection.Any(x => x.Name.Equals("OneMoreFolder")));
		}
	}
}
