---
title: Verify a DACPAC File
description: "Use a command line tool to verify a DACPAC after project conversion."
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 03/11/2025
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Verify a DACPAC file

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The process of converting an [existing SQL project to an SDK-style project](../../howto/convert-original-sql-project.md) is done by manually editing the `.sqlproj` file to include the Microsoft.Build.Sql SDK-style project format. Backing up the project file and archiving a `.dacpac` of the project before beginning the conversion is recommended. By comparing a "before" and "after" `.dacpac` built from the project, you can ensure that the conversion process was correctly completed.

The [DacpacVerify CLI (preview)](https://www.nuget.org/packages/Microsoft.DacpacVerify/1.0.0-rc) is a command line tool that compares two `.dacpac` files and outputs the differences between the files. The tool is useful for verifying that a project conversion was successful by comparing the `.dacpac` files before and after the conversion.

## Prerequisites

Similar to the SqlPackage command line tool, the DacpacVerify CLI (preview) is available as a [dotnet tool](/dotnet/core/tools/global-tools). DacpacVerify can be installed on Windows, macOS, and Linux and requires the [.NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0) to be installed on your machine. To install the DacpacVerify CLI, run the following command:

```bash
dotnet tool install --global Microsoft.DacpacVerify --prerelease
```

## DacpacVerify tool

The basic usage of the DacpacVerify tool involves running the `dacpacverify` command followed by the paths to the two `.dacpac` files that you want to compare.

```bash
dacpacverify before.dacpac after.dacpac
```

The tool outputs a summary of the differences between the two files. The verification of the `.dacpac` files includes:

- pre-deployment scripts
- post-deployment scripts
- SQLCMD variables
- database references
- database properties/options
- database objects (tables, views, stored procedures, etc.)

<a id="verifying-a-converted-project"></a>

## Verify a converted project

In this example, we'll verify a project conversion by comparing the `.dacpac` files before and after a SQL project conversion. Before the conversion, we build the project and create a `.dacpac` file, which defaults to the `bin/Debug` folder. Make a copy of the `.dacpac` file for comparison later with the name `before.dacpac` in a separate folder.

# [Original SQL project](#tab/original-sql-project)

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003" ToolsVersion="4.0">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <Name>ConversionTest</Name>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectVersion>4.1</ProjectVersion>
    <ProjectGuid>{<unique identifier>}</ProjectGuid>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <OutputType>Database</OutputType>
    <RootPath>
    </RootPath>
    <RootNamespace>ConversionTest</RootNamespace>
    <AssemblyName>ConversionTest</AssemblyName>
    <ModelCollation>1033, CI</ModelCollation>
    <DefaultFileStructure>BySchemaAndSchemaType</DefaultFileStructure>
    <DeployToDatabase>True</DeployToDatabase>
    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
    <TargetLanguage>CS</TargetLanguage>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <SqlServerVerification>False</SqlServerVerification>
    <IncludeCompositeObjects>True</IncludeCompositeObjects>
    <TargetDatabaseSet>True</TargetDatabaseSet>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <OutputPath>bin\Release\</OutputPath>
    <BuildScriptName>$(MSBuildProjectName).sql</BuildScriptName>
    <TreatWarningsAsErrors>False</TreatWarningsAsErrors>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <DefineDebug>false</DefineDebug>
    <DefineTrace>true</DefineTrace>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <OutputPath>bin\Debug\</OutputPath>
    <BuildScriptName>$(MSBuildProjectName).sql</BuildScriptName>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <DefineDebug>true</DefineDebug>
    <DefineTrace>true</DefineTrace>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">11.0</VisualStudioVersion>
    <!-- Default to the v11.0 targets path if the targets file for the current VS version isn't found -->
    <SSDTExists Condition="Exists('$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets')">True</SSDTExists>
    <VisualStudioVersion Condition="'$(SSDTExists)' == ''">11.0</VisualStudioVersion>
  </PropertyGroup>
  <Import Condition="'$(SQLDBExtensionsRefPath)' != ''" Project="$(SQLDBExtensionsRefPath)\Microsoft.Data.Tools.Schema.SqlTasks.targets" />
  <Import Condition="'$(SQLDBExtensionsRefPath)' == ''" Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets" />
  <ItemGroup>
    <Folder Include="Properties" />
  </ItemGroup>
  <ItemGroup>
    <Build Include="Table1.sql" />
    <Build Include="View1.sql" />
    <Build Include="View2.sql" />
  </ItemGroup>
  <ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
      <DefaultValue>
      </DefaultValue>
      <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

# [Minimally converted SQL project](#tab/minimally-converted-sql-project)

By following only the **required** steps from the [conversion guide](../../howto/convert-original-sql-project.md) in VS Code or other text editor, the project file now builds using `dotnet build` and create a `.dacpac` file. In addition to adding the SDK reference, we removed:

- `<Import>` elements
- `<Folder>` elements
- `<Build>` elements that are inside the `.sqlproj` folder

The project file look like this after the minimal conversion:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="4.0">
  <Sdk Name="Microsoft.Build.Sql" Version="1.0.0" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <Name>ConversionTest</Name>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectVersion>4.1</ProjectVersion>
    <ProjectGuid>{<unique identifier>}</ProjectGuid>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <OutputType>Database</OutputType>
    <RootPath>
    </RootPath>
    <RootNamespace>ConversionTest</RootNamespace>
    <AssemblyName>ConversionTest</AssemblyName>
    <ModelCollation>1033, CI</ModelCollation>
    <DefaultFileStructure>BySchemaAndSchemaType</DefaultFileStructure>
    <DeployToDatabase>True</DeployToDatabase>
    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
    <TargetLanguage>CS</TargetLanguage>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <SqlServerVerification>False</SqlServerVerification>
    <IncludeCompositeObjects>True</IncludeCompositeObjects>
    <TargetDatabaseSet>True</TargetDatabaseSet>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <OutputPath>bin\Release\</OutputPath>
    <BuildScriptName>$(MSBuildProjectName).sql</BuildScriptName>
    <TreatWarningsAsErrors>False</TreatWarningsAsErrors>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <DefineDebug>false</DefineDebug>
    <DefineTrace>true</DefineTrace>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <OutputPath>bin\Debug\</OutputPath>
    <BuildScriptName>$(MSBuildProjectName).sql</BuildScriptName>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <DefineDebug>true</DefineDebug>
    <DefineTrace>true</DefineTrace>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">11.0</VisualStudioVersion>
    <!-- Default to the v11.0 targets path if the targets file for the current VS version isn't found -->
    <SSDTExists Condition="Exists('$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets')">True</SSDTExists>
    <VisualStudioVersion Condition="'$(SSDTExists)' == ''">11.0</VisualStudioVersion>
  </PropertyGroup>
  <ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
      <DefaultValue>
      </DefaultValue>
      <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

If we accidentally removed the `<SqlCmdVariable>` element, the project would still build, but the `.dacpac` file wouldn't contain the SQLCMD variable. The DacpacVerify output would reflect this difference and we would retrace our steps to correct the difference before proceeding.

The project file is now converted to the Microsoft.Build.Sql SDK-style format, but the project file can be further simplified.

# [Fully converted SQL project](#tab/Fully-converted-sql-project)

More changes can be made to the project file to remove properties and elements that match the defaults from the SDK. [Project properties](../project-properties.md) can be removed if they match the default values, and DacpacVerify compares the database properties in the `.dacpac` files.

If we'd like to remove the `<PropertyGroup>` elements for the `Release` and `Debug` configurations, we should run the `dotnet build` command with the `-c Release` or `-c Debug` option to build the project with each configuration. By generating a `.dacpac` file for each configuration, we can compare the two `.dacpac` files against the original to ensure that we're still producing the same `.dacpac` output.

When we've complete the conversion all the way to a minimal Microsoft.Build.Sql project, the project file look like this after a full conversion:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="4.0">
  <Sdk Name="Microsoft.Build.Sql" Version="1.0.0" />
  <PropertyGroup>
    <Name>ConversionTest</Name>
    <ProjectGuid>{<unique-identifier>}</ProjectGuid>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
  </PropertyGroup>
  <ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
      <DefaultValue>
      </DefaultValue>
      <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

After the conversion, we'll build the project again and create a new `.dacpac` file. Finally, we use the DacpacVerify CLI to compare the original and post-conversion `.dacpac` files.

---

## Related content

- [Convert an original SQL project to an SDK-style project](../../howto/convert-original-sql-project.md)
- [SQL projects tools](../../sql-projects-tools.md)
