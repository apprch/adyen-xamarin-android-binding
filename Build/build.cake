//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var projects = new[] {
    "AdyenCse",
	"BaseUI",
    "BaseV3",
    "CardBase",
    "CardUI",
	"CoreV3",
    "Cse"
};

var artifactDir = "../Artifacts";
var solutionFile = "../Approach.AdyenCheckout.Droid.sln";
var projectBaseDir = "..";
string GetBuildDir(string project) => Directory($"{projectBaseDir}/{project}/bin/{configuration}");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactDir);
        foreach(var project in projects)
            CleanDirectory(GetBuildDir(project));
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        NuGetRestore(solutionFile);
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
        DownloadAdyenPackages(force: false);

        if(IsRunningOnWindows())
        {
            // Use MSBuild
            MSBuild(solutionFile, settings => {
                settings.SetConfiguration(configuration);
                settings.ToolVersion = MSBuildToolVersion.VS2019;
            });
        }
        else
        {
            // // Use XBuild
            // XBuild(solutionFile, settings =>
            //     settings.SetConfiguration(configuration));
            Error("ERROR! Builds are currently only supported on Windows!");
        }

    });

Task("DownloadAdyenPackages")
    .Does(() =>
    {
        DownloadAdyenPackages(force: true);
    });

void DownloadAdyenPackages(bool force = false)
{
    var packages = new Dictionary<string, string> {
        { "https://jcenter.bintray.com/com/adyen/cse/adyen-cse/1.0.5/adyen-cse-1.0.5.aar", $"{projectBaseDir}/AdyenCse/Jars/adyen-cse-1.0.5.aar" },
		{ "https://jcenter.bintray.com/com/adyen/checkout/base-ui/3.2.0/base-ui-3.2.0.aar", $"{projectBaseDir}/BaseUI/Jars/base-ui-3.2.0.aar"},
		{ "https://jcenter.bintray.com/com/adyen/checkout/base-v3/3.2.0/base-v3-3.2.0.aar", $"{projectBaseDir}/BaseV3/Jars/base-v3-3.2.0.aar"},
		{ "https://jcenter.bintray.com/com/adyen/checkout/card-base/3.2.0/card-base-3.2.0.aar", $"{projectBaseDir}/CardBase/Jars/card-base-3.2.0.aar"},
		/*{ "https://jcenter.bintray.com/com/adyen/checkout/card-ui/3.2.0/card-ui-3.2.0.aar", $"{projectBaseDir}/CardUI/Jars/card-ui-3.2.0.aar"},*/
		{ "https://jcenter.bintray.com/com/adyen/checkout/core-v3/3.2.0/core-v3-3.2.0.aar", $"{projectBaseDir}/CoreV3/Jars/core-v3-3.2.0.aar"},
		{ "https://jcenter.bintray.com/com/adyen/checkout/cse/3.2.0/cse-3.2.0.aar", $"{projectBaseDir}/Cse/Jars/cse-3.2.0.aar"}
    };

    foreach(var package in packages) {
        if (force || !FileExists(package.Value)) {
            DownloadFile(package.Key, package.Value);
            Information($"Downloaded file {package.Key} to {package.Value}");
        }
    }
}

Task("BuildAllPackages")
    .IsDependentOn("Build")
    .Does(() => {
        foreach(var project in projects)
            BuildNuGetPackage(project);
    });

Task("BuildPackage")
    .Does(() => {
        var project = Argument("project", string.Empty);
        if (string.IsNullOrWhiteSpace(project))
            throw new Exception("You need to specify project with -project ProjectName, not building any packages");

        BuildNuGetPackage(project);
    });

void BuildNuGetPackage(string project) {
    Information($"Creating package for {project}");
    NuGetPack($"{projectBaseDir}/{project}/{project}.csproj", new NuGetPackSettings {
        Properties = new Dictionary<string, string> {
            { "Configuration", configuration }
        },
        OutputDirectory = artifactDir
    });
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
