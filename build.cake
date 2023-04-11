#tool nuget:?package=GitVersion.CommandLine&version=5.6.3
#tool nuget:?package=GitReleaseManager&version=0.12.1
#tool nuget:?package=NuGet.CommandLine&version=6.0.0

using System.Xml.Serialization;

const string GITHUB_OWNER = "testcentric";
const string GITHUB_REPO = "testcentric-gui";

const string DEFAULT_VERSION = "2.0.0";
static string[] VALID_CONFIGS = new [] { "Release", "Debug" };

// NOTE: This must match what is actually referenced by
// the GUI test model project. Hopefully, this is a temporary
// fix, which we can get rid of in the future.
const string REF_ENGINE_VERSION = "2.0.0-dev00053";

const string PACKAGE_NAME = "testcentric-gui";
const string NUGET_PACKAGE_NAME = "TestCentric.GuiRunner";

const string GUI_RUNNER = "testcentric.exe";
const string GUI_TESTS = "*.Tests.dll";

// Load recipe after defining constants
#load nuget:?package=TestCentric.Cake.Recipe&version=1.0.0-dev00039

#load "./package-tests.cake"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//
// Arguments taking a value may use  `=` or space to separate the name
// from the value. Examples of each are shown here.
//
// --target=TARGET
// -t Target
//
//    The name of the task to be run, e.g. Test. Defaults to Build.
//
// --configuration=CONFIG
// -c CONFIG
//
//     The name of the configuration to build, test and/or package, e.g. Debug.
//     Defaults to Release.
//
// --packageVersion=VERSION
// --package=VERSION
//     Specifies the full package version, including any pre-release
//     suffix. This version is used directly instead of the default
//     version from the script or that calculated by GitVersion.
//     Note that all other versions (AssemblyVersion, etc.) are
//     derived from the package version.
//
//     NOTE: We can't use "version" since that's an argument to Cake itself.
//
// --testLevel=LEVEL
// --level=LEVEL
//     Specifies the level of package testing, which is normally set
//     automatically for different types of builds like CI, PR, etc.
//     Used by developers to test packages locally without creating
//     a PR or publishing the package. Defined levels are
//       1 = Normal CI tests run every time you build a package
//       2 = Adds more tests for PRs and Dev builds uploaded to MyGet
//       3 = Adds even more tests prior to publishing a release
//
// --nopush
//     Indicates that no publishing or releasing should be done. If
//     publish or release targets are run, a message is displayed.
//
//////////////////////////////////////////////////////////////////////

using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading.Tasks;

//////////////////////////////////////////////////////////////////////
// INITIALIZATION
//////////////////////////////////////////////////////////////////////

BuildSettings.Initialize(
	context: Context,
	title: "TestCentric.GuiRunner",
	solutionFile: "testcentric-gui.sln",
	unitTestRunner: new GuiSelfTester(),
	exemptFiles: new [] { "Resource.cs", "TextCode.cs" }
);

DefinePackageTests();

static readonly string[] ENGINE_FILES = {
        "testcentric.engine.dll", "testcentric.engine.core.dll", "nunit.engine.api.dll", "testcentric.engine.metadata.dll"};
static readonly string[] ENGINE_CORE_FILES = {
        "testcentric.engine.core.dll", "nunit.engine.api.dll", "testcentric.engine.metadata.dll" };
static readonly string[] NET_FRAMEWORK_AGENT_FILES = {
        "testcentric-agent.exe", "testcentric-agent.exe.config", "testcentric-agent-x86.exe", "testcentric-agent-x86.exe.config" };
static readonly string[] NET_CORE_AGENT_FILES = {
        "testcentric-agent.dll", "testcentric-agent.dll.config" };
static readonly string[] GUI_FILES = {
        "testcentric.exe", "testcentric.exe.config", "nunit.uiexception.dll",
        "TestCentric.Gui.Runner.dll", "TestCentric.Gui.Model.dll", "Mono.Options.dll" };
static readonly string[] TREE_ICONS_JPG = {
        "Success.jpg", "Failure.jpg", "Ignored.jpg", "Inconclusive.jpg", "Skipped.jpg" };
static readonly string[] TREE_ICONS_PNG = {
        "Success.png", "Failure.png", "Ignored.png", "Inconclusive.png", "Skipped.png" };

var nugetPackage = new NuGetPackage(
	id: "TestCentric.GuiRunner",
	source: "nuget/TestCentric.GuiRunner.nuspec",
	basePath: BuildSettings.OutputDirectory,
	testRunner: new GuiSelfTester(BuildSettings.NuGetTestDirectory + "TestCentric.GuiRunner/tools/testcentric.exe"),
	checks: new PackageCheck[] {
		HasFiles("CHANGES.txt", "LICENSE.txt", "NOTICES.txt", "testcentric.png"),
		HasDirectory("tools").WithFiles(GUI_FILES).AndFiles(ENGINE_FILES).AndFile("testcentric.nuget.addins"),
		HasDirectory("tools/agents/net462").WithFiles(NET_FRAMEWORK_AGENT_FILES).AndFiles(ENGINE_CORE_FILES).AndFile("testcentric-agent.nuget.addins"),
		HasDirectory("tools/agents/netcoreapp3.1").WithFiles(NET_CORE_AGENT_FILES).AndFiles(ENGINE_CORE_FILES).AndFile("testcentric-agent.nuget.addins"),
		HasDirectory("tools/agents/net5.0").WithFiles(NET_CORE_AGENT_FILES).AndFiles(ENGINE_CORE_FILES).AndFile("testcentric-agent.nuget.addins"),
		HasDirectory("tools/agents/net6.0").WithFiles(NET_CORE_AGENT_FILES).AndFiles(ENGINE_CORE_FILES).AndFile("testcentric-agent.nuget.addins"),
		HasDirectory("tools/agents/net7.0").WithFiles(NET_CORE_AGENT_FILES).AndFiles(ENGINE_CORE_FILES).AndFile("testcentric-agent.nuget.addins"),
		HasDirectory("tools/Images").WithFiles("DebugTests.png", "RunTests.png", "StopRun.png", "GroupBy_16x.png", "SummaryReport.png"),
		HasDirectory("tools/Images/Tree/Circles").WithFiles(TREE_ICONS_JPG),
		HasDirectory("tools/Images/Tree/Classic").WithFiles(TREE_ICONS_JPG),
		HasDirectory("tools/Images/Tree/Default").WithFiles(TREE_ICONS_PNG),
		HasDirectory("tools/Images/Tree/Visual Studio").WithFiles(TREE_ICONS_PNG)
	},
	tests: PackageTests);

var chocolateyPackage = new ChocolateyPackage(
	id: "testcentric-gui",
	source: BuildSettings.ChocolateyDirectory + "testcentric-gui.nuspec",
	basePath: BuildSettings.OutputDirectory,
	testRunner: new GuiSelfTester(BuildSettings.ChocolateyTestDirectory + "testcentric-gui/tools/testcentric.exe"),
	checks: new PackageCheck[] {
		HasDirectory("tools").WithFiles("CHANGES.txt", "LICENSE.txt", "NOTICES.txt", "VERIFICATION.txt", "testcentric.choco.addins").AndFiles(GUI_FILES).AndFiles(ENGINE_FILES).AndFile("testcentric.choco.addins"),
		HasDirectory("tools/agents/net462").WithFiles(NET_FRAMEWORK_AGENT_FILES).AndFile("testcentric-agent.choco.addins"),
		HasDirectory("tools/agents/netcoreapp3.1").WithFiles(NET_CORE_AGENT_FILES).AndFile("testcentric-agent.choco.addins"),
		HasDirectory("tools/agents/net5.0").WithFiles(NET_CORE_AGENT_FILES).AndFile("testcentric-agent.choco.addins"),
		HasDirectory("tools/agents/net6.0").WithFiles(NET_CORE_AGENT_FILES).AndFile("testcentric-agent.choco.addins"),
		HasDirectory("tools/agents/net7.0").WithFiles(NET_CORE_AGENT_FILES).AndFile("testcentric-agent.choco.addins"),
		HasDirectory("tools/Images").WithFiles("DebugTests.png", "RunTests.png", "StopRun.png", "GroupBy_16x.png", "SummaryReport.png"),
		HasDirectory("tools/Images/Tree/Circles").WithFiles(TREE_ICONS_JPG),
		HasDirectory("tools/Images/Tree/Classic").WithFiles(TREE_ICONS_JPG),
		HasDirectory("tools/Images/Tree/Default").WithFiles(TREE_ICONS_PNG),
		HasDirectory("tools/Images/Tree/Visual Studio").WithFiles(TREE_ICONS_PNG)
	},
	tests: PackageTests);

var zipPackage = new ZipPackage(
	id: "TestCentric.GuiRunner",
	source: BuildSettings.ZipDirectory + "TestCentric.GuiRunner.zspec",
	basePath: BuildSettings.OutputDirectory,
	testRunner: new GuiSelfTester(BuildSettings.ZipTestDirectory + "TestCentric.GuiRunner/bin/testcentric.exe"),
	checks: new PackageCheck[] {
		HasFiles("CHANGES.txt", "LICENSE.txt", "NOTICES.txt"),
		HasDirectory("bin").WithFiles(GUI_FILES).AndFiles(ENGINE_FILES).AndFile("testcentric.zip.addins"),
		HasDirectory("bin/agents/net462").WithFiles(NET_FRAMEWORK_AGENT_FILES),
		HasDirectory("bin/agents/netcoreapp3.1").WithFiles(NET_CORE_AGENT_FILES),
		HasDirectory("bin/agents/net5.0").WithFiles(NET_CORE_AGENT_FILES),
		HasDirectory("bin/agents/net6.0").WithFiles(NET_CORE_AGENT_FILES),
		HasDirectory("bin/agents/net7.0").WithFiles(NET_CORE_AGENT_FILES),
		HasDirectory("bin/Images").WithFiles("DebugTests.png", "RunTests.png", "StopRun.png", "GroupBy_16x.png", "SummaryReport.png"),
		HasDirectory("bin/Images/Tree/Circles").WithFiles(TREE_ICONS_JPG),
		HasDirectory("bin/Images/Tree/Classic").WithFiles(TREE_ICONS_JPG),
		HasDirectory("bin/Images/Tree/Default").WithFiles(TREE_ICONS_PNG),
		HasDirectory("bin/Images/Tree/Visual Studio").WithFiles(TREE_ICONS_PNG)
	},
	tests: PackageTests);

BuildSettings.Packages.Add(nugetPackage);
BuildSettings.Packages.Add(chocolateyPackage);
BuildSettings.Packages.Add(zipPackage);

//////////////////////////////////////////////////////////////////////
// POST-BUILD ACTION
//////////////////////////////////////////////////////////////////////

// The engine package does not restore correctly. As a temporary
// fix, we install a local copy and then copy agents and
// content to the output directory.
TaskTeardown(context =>
{
	if (context.Task.Name == "Build")
	{
		string tempEngineInstall = BuildSettings.ProjectDirectory + "tempEngineInstall/";

		CleanDirectory(tempEngineInstall);

		NuGetInstall("TestCentric.Engine", new NuGetInstallSettings()
		{
			Version = REF_ENGINE_VERSION,
			OutputDirectory = tempEngineInstall,
			ExcludeVersion = true
		});

		CopyFileToDirectory(
			tempEngineInstall + "TestCentric.Engine/content/testcentric.nuget.addins",
			BuildSettings.OutputDirectory);
		Information("Copied testcentric.nuget.addins");
		CopyDirectory(
			tempEngineInstall + "TestCentric.Engine/tools",
			BuildSettings.OutputDirectory);
		Information("Copied engine files");
	}
});

//////////////////////////////////////////////////////////////////////
// UNIT AND PACKAGE TEST RUNNER
//////////////////////////////////////////////////////////////////////

public class GuiSelfTester : TestRunner
{
	// NOTE: When constructed as an argument to BuildSettings.Initialize(),
	// the executable path is not yet known and should not be provided.
	public GuiSelfTester(string executablePath = null)
	{
		ExecutablePath = executablePath;
	}

	public override int Run(string arguments)
	{


		if (!arguments.Contains(" --run"))
			arguments += " --run";
		if (!arguments.Contains(" --unattended"))
			arguments += " --unattended";
		if (!arguments.Contains(" --full-gui"))
			arguments += " --full-gui";

		if (ExecutablePath == null)
			ExecutablePath = BuildSettings.OutputDirectory + "testcentric.exe";

		return base.Run(arguments);
	}
}

//////////////////////////////////////////////////////////////////////
// INDIVIDUAL PACKAGES
//////////////////////////////////////////////////////////////////////

Task("PackageNuGet")
	.Does(() =>
	{
		nugetPackage.BuildVerifyAndTest();
	});

Task("PackageChocolatey")
	.Does(() =>
	{
		chocolateyPackage.BuildVerifyAndTest();
	});

Task("PackageZip")
	.Does(() =>
	{
		zipPackage.BuildVerifyAndTest();
	});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("AppVeyor")
	.IsDependentOn("DumpSettings")
	.IsDependentOn("Build")
	.IsDependentOn("Test")
	.IsDependentOn("Package")
	.IsDependentOn("Publish")
	.IsDependentOn("CreateDraftRelease")
	.IsDependentOn("CreateProductionRelease");

Task("Travis")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

Task("BuildTestAndPackage")
	.IsDependentOn("DumpSettings")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(Argument("target", Argument("t", "Default")));
