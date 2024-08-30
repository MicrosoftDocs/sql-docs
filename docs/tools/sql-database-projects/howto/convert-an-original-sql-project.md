---
title: Convert an original SQL project
description: "Create an SDK-style SQL project from an existing project."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: how-to
zone_pivot_groups: sq1-sql-projects-tools
---

# Convert an original SQL project to an SDK-style project

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creating a new SDK-style SQL project is a [quick task](../getting-started.md#create-a-new-project). However, if you have existing SQL projects you can convert them to SDK-style SQL projects in place to take advantage of the new features.

Once you convert the project, you can use the new features of the SDK-style project, such as:

- cross-platform build support
- simplified project file format
- package references

To complete the conversion carefully, we will:

1. Create a backup of the original project file.
2. Build a `.dacpac` file from the original project for comparison.
3. Modify the project file to an SDK-style project.
4. Build a `.dacpac` file from the modified project for comparison.
5. Verify that the `.dacpac` files are the same.

SDK-style projects aren't supported by SQL Server Data Tools (SSDT) in Visual Studio. Once converted, you must use one of the following to build or edit the project:

- the command line
- the SQL Database Projects extension in Visual Studio Code
- the SQL Database Projects extension in Azure Data Studio

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools (SSDT) installed in Visual Studio 2022](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview) installed in Visual Studio 2022](../../sql/ssdt/download-sql-server-data-tools-ssdt-sdk.md)

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [VS Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension for Azure Data Studio](/azure-data-studio/extensions/sql-database-project-extension) or [SQL Database Projects extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode)

::: zone-end

::: zone pivot="sq1-command-line"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

::: zone-end

## Step 1: Create a backup of the original project file

Before you convert the project, create a backup of the original project file. This way, you can revert to the original project if needed.

In file explorer, create a copy of the `.sqlproj` file for the project you want to convert with `.original` appended on the end of the file extension. For example, `MyProject.sqlproj` becomes `MyProject.sqlproj.original`.

## Step 2: Build a `.dacpac` file from the original project for comparison

::: zone pivot="sq1-visual-studio"

Open the project in Visual Studio 2022. The `.sqlproj` file is still in the original format, so you open it in the original SQL Server Data Tools.

Build the project in Visual Studio by right-clicking on the database node in **Solution Explorer** and selecting **Build**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

Open the project folder in VS Code or Azure Data Studio. In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Build**.

::: zone-end

::: zone pivot="sq1-command-line"

SQL database projects can be built from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

::: zone-end

The build process creates a `.dacpac` file in the `bin\Debug` folder of the project by default. Using file explorer, locate the `.dacpac` created by the build process and copy it into a new folder outside of the project directory as `original_project.dacpac`. We use this `.dacpac` file for comparison to validate our conversion later.

## Step 3: Modify the project file to an SDK-style project

Modifying the project file is a manual process, best performed in a text editor. Open the `.sqlproj` file in a text editor and make the following changes:

### Required: Add the SDK reference

Inside the project element, add an `Sdk` item to reference Microsoft.Build.Sql and the latest version from <https://www.nuget.org/packages/Microsoft.build.sql>.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="4.0">
  <Sdk Name="Microsoft.Build.Sql" Version="0.2.0-preview" />
...
```

### Required: Remove unnecessary build target imports

Original SQL projects reference several build targets and properties in Import statements. Except for `<Import/>` items you explicitly added, which is a unique and deliberate change, remove lines that begin with `<Import ...>`.
Examples to remove if present in your `.sqlproj`:

```xml
...
<Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
<Import Condition="..." Project="...\Microsoft.Data.Tools.Schema.SqlTasks.targets"/>
<Import Condition="'$(SQLDBExtensionsRefPath)' != ''" Project="$(SQLDBExtensionsRefPath)\Microsoft.Data.Tools.Schema.SqlTasks.targets" />
<Import Condition="'$(SQLDBExtensionsRefPath)' == ''" Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets" />
...
```

### Required: Remove Properties folder

Original SQL projects have an entry for a `Properties` folder that represented access to the project properties in solution explorer. This item needs to be removed from the project file.

Example to remove if present in your `.sqlproj`:

```xml
<ItemGroup>
  <Folder Include="Properties" />
</ItemGroup>
```

### Optional: Remove SSDT references

The original SQL Server Data Tools (SSDT) required extra content in the project file to detect the Visual Studio install. These lines are unnecessary in SDK-style SQL projects and can be removed:

```xml
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">11.0</VisualStudioVersion>
    <!-- Default to the v11.0 targets path if the targets file for the current VS version is not found -->
    <SSDTExists Condition="Exists('$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets')">True</SSDTExists>
    <VisualStudioVersion Condition="'$(SSDTExists)' == ''">11.0</VisualStudioVersion>
  </PropertyGroup>
```

### Optional: Remove default build settings

Original SQL projects include two large blocks for Release and Debug build settings, while in SDK-style SQL projects the defaults for these options are known by the SDK. If you don't have customizations to the build settings, consider removing these blocks:

```xml
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
```

## Step 4: Build a `.dacpac` file from the modified project for comparison

::: zone pivot="sq1-visual-studio"

The SQL project is no longer compatible with Visual Studio 2022. To build or edit the project, you must use one of:

- the command line
- the SQL Database Projects extension in Visual Studio Code
- the SQL Database Projects extension in Azure Data Studio

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

Open the project folder in VS Code or Azure Data Studio. In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Build**.

::: zone-end

::: zone pivot="sq1-command-line"

SQL database projects can be built from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

::: zone-end

The build process creates a `.dacpac` file in the `bin\Debug` folder of the project by default. Using file explorer, locate the `.dacpac` created by the build process and copy it into a new folder outside of the project directory. We use this `.dacpac` file for comparison to validate our conversion later.

## Step 5: Verify that the `.dacpac` files are the same

To verify that the conversion was successful, compare the `.dacpac` files created from the original and modified projects. The [schema comparison](../concepts/schema-comparison.md) capabilities of SQL projects allow us to visualize the difference in database models.

You can use the schema compare tool in Visual Studio, Visual Studio Code, or Azure Data Studio to compare the `.dacpac` files. Community tools based on the [DacFx .NET library](/dotnet/api/microsoft.sqlserver.dac.compare) are also available.

::: zone pivot="sq1-visual-studio"

Launch Visual Studio without a project loaded. Go to **Tools** > **SQL Server** > **New Schema Comparison**. Select the original `.dacpac` file as the source and the modified `.dacpac` file as the target. For more on using Schema Compare in Visual Studio, see [using schema compare to compare different database definitions](../../../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md).

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

Schema comparison isn't available in Visual Studio Code. Use Azure Data Studio or Visual Studio to compare schemas.

In Azure Data Studio, install the **SQL Server Schema Compare** extension if it isn't already installed. Launch a new schema comparison from the command palette by opening the command palette with `Ctrl/Cmd+Shift+P` and typing `Schema Compare`.

Select the original `.dacpac` file as the source and the modified `.dacpac` file as the target.

::: zone-end

::: zone pivot="sq1-command-line"

Graphical schema comparison is available in Visual Studio and Azure Data Studio.

::: zone-end

When schema comparison is run, no results should be displayed. The lack of differences indicates that the original and modified projects are equivalent, producing the same database model in the `.dacpac` file.

> [!NOTE]  
> The comparison of `.dacpac` files through schema comparison doesn't validate pre/post-deployment scripts, refactorlog, or other project settings. It only validates the database model. Converting the `.dacpac` to a `.zip` archive and manually comparing the contents can provide a more detailed comparison.

## Related content

- [What are SQL database projects?](../sql-database-projects.md)
- [Get started with SQL database projects](../getting-started.md)
- [SQL projects package references overview](../concepts/package-references.md)
