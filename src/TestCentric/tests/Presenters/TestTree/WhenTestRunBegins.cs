// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System.Windows.Forms;
using NSubstitute;
using NUnit.Framework;
using TestCentric.Gui.Model;

namespace TestCentric.Gui.Presenters.TestTree
{
    public class WhenTestRunBegins : TreeViewPresenterTestBase
    {
        [Test]
        public void WhenTestRunStarts_TreeNodeImagesAreReset()
        {
            // Arrange
            var tv = new TreeView();
            _view.TreeView.Returns(tv);

            var project = new TestCentricProject(_model, "Dummy.dll");
            _model.TestCentricProject.Returns(project);
            TestNode testNode = new TestNode("<test-suite id='1'/>");
            _model.LoadedTests.Returns(testNode);
            FireTestLoadedEvent(testNode);

            // Act
            FireRunStartingEvent(1234);

            // Assert
            _view.Received().ResetAllTreeNodeImages();
        }

        // TODO: Version 1 Test - Make it work if needed.
        //[Test]
        //public void WhenTestRunStarts_ResultsAreCleared()
        //{
        //    _settings.RunStarting += Raise.Event<TestEventHandler>(new TestEventArgs(TestAction.RunStarting, "Dummy", 1234));

        //    _view.Received().ClearResults();
        //}
    }
}
