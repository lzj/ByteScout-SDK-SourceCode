## How to find text in PDF with PDF extractor SDK in ASP.NET and ByteScout PDF Suite

### Step-by-step tutorial on how to find text in PDF with PDF extractor SDK in ASP.NET

We made thousands of pre-made source code pieces for easy implementation in your own programming projects. ByteScout PDF Suite is the set that includes 6 SDK products to work with PDF from generating rich PDF reports to extracting data from PDF documents and converting them to HTML. This bundle includes PDF (Generator) SDK, PDF Renderer SDK, PDF Extractor SDK, PDF to HTML SDK, PDF Viewer SDK and PDF Generator SDK for Javascript. It can find text in PDF with PDF extractor SDK in ASP.NET.

 These ASP.NET code samples for ASP.NET guide developers to speed up coding of the application when using ByteScout PDF Suite. IF you want to implement the functionality, just copy and paste this code for ASP.NET below into your code editor with your app, compile and run your application. Check ASP.NET sample code samples to see if they respond to your needs and requirements for the project.

If you want to try other source code samples then the free trial version of ByteScout PDF Suite is available for download from our website. Just try other source code samples for ASP.NET.

## REQUEST FREE TECH SUPPORT

[Click here to get in touch](https://bytescout.zendesk.com/hc/en-us/requests/new?subject=ByteScout%20PDF%20Suite%20Question)

or just send email to [support@bytescout.com](mailto:support@bytescout.com?subject=ByteScout%20PDF%20Suite%20Question) 

## ON-PREMISE OFFLINE SDK 

[Get Your 60 Day Free Trial](https://bytescout.com/download/web-installer?utm_source=github-readme)
[Explore SDK Docs](https://bytescout.com/documentation/index.html?utm_source=github-readme)
[Sign Up For Online Training](https://academy.bytescout.com/)


## ON-DEMAND REST WEB API

[Get your API key](https://pdf.co/documentation/api?utm_source=github-readme)
[Explore Web API Documentation](https://pdf.co/documentation/api?utm_source=github-readme)
[Explore Web API Samples](https://github.com/bytescout/ByteScout-SDK-SourceCode/tree/master/PDF.co%20Web%20API)

## VIDEO REVIEW

[https://www.youtube.com/watch?v=NEwNs2b9YN8](https://www.youtube.com/watch?v=NEwNs2b9YN8)




<!-- code block begin -->

##### ****Default.aspx:**
    
```
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FindText._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>

```

<!-- code block end -->    

<!-- code block begin -->

##### ****Default.aspx.cs:**
    
```
using System;
using Bytescout.PDFExtractor;

namespace FindText
{
    public partial class _Default : System.Web.UI.Page
    {
        /*
        IF YOU SEE TEMPORARY FOLDER ACCESS ERRORS: 

        Temporary folder access is required for web application when you use ByteScout SDK in it.
        If you are getting errors related to the access to temporary folder like "Access to the path 'C:\Windows\TEMP\... is denied" then you need to add permission for this temporary folder to make ByteScout SDK working on that machine and IIS configuration because ByteScout SDK requires access to temp folder to cache some of its data for more efficient work.

        SOLUTION:

        If your IIS Application Pool has "Load User Profile" option enabled the IIS provides access to user's temp folder. Check user's temporary folder

        If you are running Web Application under an impersonated account or IIS_IUSRS group, IIS may redirect all requests into separate temp folder like "c:\temp\".

        In this case
        - check the User or User Group your web application is running under
        - then add permissions for this User or User Group to read and write into that temp folder (c:\temp or c:\windows\temp\ folder)
        - restart your web application and try again

        */

        protected void Page_Load(object sender, EventArgs e)
        {
            String inputFile = Server.MapPath(@".\bin\sample1.pdf");

            // Create Bytescout.PDFExtractor.TextExtractor instance
            TextExtractor extractor = new TextExtractor();
            extractor.RegistrationName = "demo";
            extractor.RegistrationKey = "demo";
            
            // Load sample PDF document
            extractor.LoadDocumentFromFile(inputFile);
            
            // Set the matching mode.
            // WordMatchingMode.None - treats the search string as substring
            // WordMatchingMode.ExactMatch - treats the search string as separate word
            // WordMatchingMode.SmartMatch - will find the word in various forms (like Adobe Reader).
            extractor.WordMatchingMode = WordMatchingMode.ExactMatch;

            Response.Clear();
            Response.ContentType = "text/html";

            Response.Write("Searching for \"ipsum\" string:<br>");
            
            // Search for "ipsum" string
            if (extractor.Find(0, "ipsum", false))
            {
                do
                {
                    Response.Write("<br/>");
                    Response.Write("Found on page 1 at location " + extractor.FoundText.Bounds + "<br/>");
                    Response.Write("<br/>");
                    
                    // The found text may be splitted to parts.
                    // Iterate through each part of the found text.
                    for (var i = 0; i < extractor.FoundText.Elements.Count; i++)
                    {
                        ISearchResultElement element = extractor.FoundText.Elements[i];
                        
                        Response.Write("Element #" + i + " at " + element.Bounds + "<br/>");
                        Response.Write("Text: " + element.Text + "<br/>");
                        Response.Write("Font is bold: " + element.FontIsBold + "<br/>");
                        Response.Write("Font is italic:" + element.FontIsItalic + "<br/>");
                        Response.Write("Font name: " + element.FontName + "<br/>");
                        Response.Write("Font size:" + element.FontSize + "<br/>");
                        Response.Write("Font color:" + element.FontColor + "<br/>");
                    }
                }
                while (extractor.FindNext());
            }

            Response.End();
        }
    }
}

```

<!-- code block end -->    

<!-- code block begin -->

##### ****Default.aspx.designer.cs:**
    
```
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FindText
{
    
    
    /// <summary>
    /// _Default class.
    /// </summary>
    /// <remarks>
    /// Auto-generated class.
    /// </remarks>
    public partial class _Default {
        
        /// <summary>
        /// form1 control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlForm form1;
    }
}

```

<!-- code block end -->    

<!-- code block begin -->

##### ****FindText.csproj:**
    
```
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003" ToolsVersion="4.0">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{0C256397-34FA-4067-98A7-01D3D2BE0F7E}</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>FindText</RootNamespace>
    <AssemblyName>FindText</AssemblyName>
    <OldToolsVersion>4.0</OldToolsVersion>
    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
    <FileUpgradeFlags>
    </FileUpgradeFlags>
    <UpgradeBackupLocation>
    </UpgradeBackupLocation>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Bytescout.PDFExtractor, Version=1.0.0.12, Culture=neutral, processorArchitecture=MSIL">
      <SpecificVersion>False</SpecificVersion>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Data" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Web" />
    <Reference Include="System.Xml" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.Web.Services" />
    <Reference Include="System.EnterpriseServices" />
    <Reference Include="System.Web.Mobile" />
  </ItemGroup>
  <ItemGroup>
  </ItemGroup>
  <ItemGroup>
    <Content Include="Default.aspx" />
    <Content Include="Web.config" />
    <Content Include="..\..\sample1.pdf">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      <Link>sample1.pdf</Link>
    </Content>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Default.aspx.cs">
      <SubType>ASPXCodeBehind</SubType>
      <DependentUpon>Default.aspx</DependentUpon>
    </Compile>
    <Compile Include="Default.aspx.designer.cs">
      <DependentUpon>Default.aspx</DependentUpon>
    </Compile>
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">10.0</VisualStudioVersion>
    <VSToolsPath Condition="'$(VSToolsPath)' == ''">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\WebApplications\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
  <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\WebApplications\Microsoft.WebApplication.targets" Condition="false" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
  <ProjectExtensions>
    <VisualStudio>
      <FlavorProperties GUID="{349c5851-65df-11da-9384-00065b846f21}">
        <WebProjectProperties>
          <UseIIS>False</UseIIS>
          <AutoAssignPort>True</AutoAssignPort>
          <DevelopmentServerPort>27830</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl>
          </IISUrl>
          <NTLMAuthentication>False</NTLMAuthentication>
        </WebProjectProperties>
      </FlavorProperties>
    </VisualStudio>
  </ProjectExtensions>
</Project>
```

<!-- code block end -->    

<!-- code block begin -->

##### ****FindText.sln:**
    
```

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 2013
VisualStudioVersion = 12.0.40629.0
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "FindText", "FindText.csproj", "{0C256397-34FA-4067-98A7-01D3D2BE0F7E}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{0C256397-34FA-4067-98A7-01D3D2BE0F7E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{0C256397-34FA-4067-98A7-01D3D2BE0F7E}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{0C256397-34FA-4067-98A7-01D3D2BE0F7E}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{0C256397-34FA-4067-98A7-01D3D2BE0F7E}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal

```

<!-- code block end -->    

<!-- code block begin -->

##### ****Web.config:**
    
```
<?xml version="1.0"?>

<configuration>
  
    <appSettings/>
    <connectionStrings/>
  
    <system.web>
        <!-- 
            Set compilation debug="true" to insert debugging 
            symbols into the compiled page. Because this 
            affects performance, set this value to true only 
            during development.
        -->
        <compilation debug="true" />
        <!--
            The <authentication> section enables configuration 
            of the security authentication mode used by 
            ASP.NET to identify an incoming user. 
        -->
        <authentication mode="Windows" />
        <!--
            The <customErrors> section enables configuration 
            of what to do if/when an unhandled error occurs 
            during the execution of a request. Specifically, 
            it enables developers to configure html error pages 
            to be displayed in place of a error stack trace.

        <customErrors mode="RemoteOnly" defaultRedirect="GenericErrorPage.htm">
            <error statusCode="403" redirect="NoAccess.htm" />
            <error statusCode="404" redirect="FileNotFound.htm" />
        </customErrors>
        -->
    </system.web>
</configuration>

```

<!-- code block end -->