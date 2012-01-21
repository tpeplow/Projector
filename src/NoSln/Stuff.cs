namespace Projector
{
    public class Stuff
    {
        public static string DebugReleaseInfo =
            "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x86' \">" +
            "<PlatformTarget>x86</PlatformTarget>" +
            "<DebugSymbols>true</DebugSymbols>" +
            "<DebugType>full</DebugType>" +
            "<Optimize>false</Optimize>" +
            "<OutputPath>bin\\Debug\\</OutputPath>" +
            "<DefineConstants>DEBUG;TRACE</DefineConstants>" +
            "<ErrorReport>prompt</ErrorReport>" +
            "<WarningLevel>4</WarningLevel>" +
            "</PropertyGroup>" +
            "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|x86' \">" +
            "<PlatformTarget>x86</PlatformTarget>" +
            "<DebugType>pdbonly</DebugType>" +
            "<Optimize>true</Optimize>" +
            "<OutputPath>bin\\Release\\</OutputPath>" +
            "<DefineConstants>TRACE</DefineConstants>" +
            "<ErrorReport>prompt</ErrorReport>" +
            "<WarningLevel>4</WarningLevel>" +
            "</PropertyGroup>" +
            "<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />";
    }
}