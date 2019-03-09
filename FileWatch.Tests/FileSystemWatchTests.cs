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
		private Mock<IFileSystemWrapper> _mockFileSystem;
		private IEnumerable<string> _predefinedFileCollection;
		private IEnumerable<string> _predefinedDirectoryCollection;
		private FileSystemWatch _fileSystemWatch;

		[SetUp]
	    public void Initialize()
	    {
		    _mockFileSystem = new Mock<IFileSystemWrapper>();

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
	    public void FileSystemWatch_NoFilterConditions()
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object);
			_mockFileSystem.Setup(x => x.GetFiles(It.IsAny<string>())).Returns(_predefinedFileCollection);
			_mockFileSystem.Setup(x => x.GetDirectories(It.IsAny<string>())).Returns(_predefinedDirectoryCollection);

			var collection = _fileSystemWatch.CreateFileSequence(@"Some string");
			
			Assert.IsNotNull(collection);
			Assert.IsNotEmpty(collection);
		}

		[TestCase("")]
		[TestCase(null)]
		[Test]
		public void FileSystemWatch_NoFilterCondition_NullOrEmptyString(string str)
		{
			_fileSystemWatch = new FileSystemWatch(_mockFileSystem.Object);

			Assert.Throws(typeof(ArgumentNullException), () => _fileSystemWatch.CreateFileSequence(str));
		}
    }
}
