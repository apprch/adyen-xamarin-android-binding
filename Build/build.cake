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
    "Base",
    "Core",
    "CoreCard",
    "UI",
    "Util"
};

var artifactDir = "../Artifacts";
var solutionFile = "../Approach.AdyenCheckout.Droid.sln";
string GetBuildDir(string project) => Directory($"../{project}/bin/{configuration}");

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
        { "https://jcenter.bintray.com/com/adyen/cse/adyen-cse/1.0.5/adyen-cse-1.0.5.aar", "../AdyenCse/Jars/adyen-cse-1.0.5.aar" },
        { "https://jcenter.bintray.com/com/adyen/checkout/base/2.4.5/base-2.4.5.aar", "../Base/Jars/base-2.4.5.aar" },
        { "https://jcenter.bintray.com/com/adyen/checkout/core/2.4.5/core-2.4.5.aar", "../Core/Jars/core-2.4.5.aar" },
        { "https://jcenter.bintray.com/com/adyen/checkout/core-card/2.4.5/core-card-2.4.5.aar", "../CoreCard/Jars/core-card-2.4.5.aar" },
        { "https://jcenter.bintray.com/com/adyen/checkout/ui/2.4.5/ui-2.4.5.aar", "../UI/Jars/ui-2.4.5.aar" },
        { "https://jcenter.bintray.com/com/adyen/checkout/util/2.4.5/util-2.4.5.aar", "../Util/Jars/util-2.4.5.aar" }
    };

    foreach(var package in packages) {
        if (force || !FileExists(package.Value)) {
            DownloadFile(package.Key, package.Value);
            Information($"Downloaded file {package.Key} to {package.Value}");
        }
    }
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
