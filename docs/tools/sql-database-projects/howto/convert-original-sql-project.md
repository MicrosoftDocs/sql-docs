---
title: Convert an Original SQL Project
description: Create an SDK-style SQL project from an existing project.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 07/17/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - ignite-2024
zone_pivot_groups: sq1-sql-projects-tools
---

# Convert an original SQL project to an SDK-style project

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Creating a new SDK-style SQL project is a [quick task](../get-started.md#step-1-create-a-new-project). However, if you have existing SQL projects, you can convert them to SDK-style SQL projects in place to take advantage of the new features.

After you convert the project, you can use the new features of the SDK-style project, such as:

- Cross-platform build support
- Simplified project file format
- Package references

To complete the conversion carefully, follow these steps:

1. Create a backup of the original project file.
1. Build a `.dacpac` file from the original project for comparison.
1. Modify the project file to an SDK-style project.
1. Build a `.dacpac` file from the modified project for comparison.
1. Verify that the `.dacpac` files are the same.

SQL Server Data Tools (SSDT) in Visual Studio doesn't support SDK-style projects. After you convert the project, use one of the following tools to build or edit the project:

- The SQL Database Projects extension in Visual Studio Code
- Database DevOps in SQL Server Management Studio (SSMS)
- The command line
- The SQL Server Data Tools, SDK-style (preview) in Visual Studio 2022

> [!NOTE]  
> You might find that your SQL project contains customization that extends the changes required beyond these steps. In addition to this article, the [DacFx GitHub repository](https://github.com/microsoft/dacfx) can be used to understand the changes necessary to upgrade from an original SQL project to SDK-style SQL projects.

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [Install SQL Server Data Tools (SSDT) for Visual Studio](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview)](../../../ssdt/sql-server-data-tools-sdk-style.md)

::: zone-end

::: zone pivot="sq1-visual-studio-code"

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension](../../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)

::: zone-end

:::zone pivot="sq1-sql-server-management-studio"

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server Management Studio (SSMS)](/ssms/install/install)
- [Database DevOps workload installed in SSMS](/ssms/install/modify)

:::zone-end

::: zone pivot="sq1-command-line"

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

::: zone-end

## Step 1: Create a backup of the original project file

Before you convert the project, create a backup of the original project file. This way, you can revert to the original project if needed.

In File Explorer, create a copy of the `.sqlproj` file for the project you want to convert with `.original` appended to the file extension. For example, `MyProject.sqlproj` becomes `MyProject.sqlproj.original`.

## Step 2: Build a `.dacpac` file from the original project for comparison

::: zone pivot="sq1-visual-studio"

Open the project in Visual Studio. The `.sqlproj` file is still in the original format, so you open it in the original SQL Server Data Tools.

Build the project in Visual Studio by right-clicking on the database node in **Solution Explorer** and selecting **Build**.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

To build a `.dacpac` file from the original project, you must use the original SQL Server Data Tools (SSDT) in Visual Studio. Open the project file in Visual Studio with the original SQL Server Data Tools installed.

Build the project in Visual Studio by right-clicking on the database node in **Solution Explorer** and selecting **Build**.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

Open the project folder in Visual Studio Code. In the **Database Projects** view of Visual Studio Code, right-click the project node and select **Build**.

::: zone-end

:::zone pivot="sq1-sql-server-management-studio"

To build a `.dacpac` file from the original project, you must use the original SQL Server Data Tools (SSDT) in Visual Studio. Open the project file in Visual Studio with the original SQL Server Data Tools installed.

Build the project in Visual Studio by right-clicking on the database node in **Solution Explorer** and selecting **Build**.

::: zone-end

::: zone pivot="sq1-command-line"

You can build SQL database projects from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

::: zone-end

The build process creates a `.dacpac` file in the `bin\Debug` folder of the project by default. Using File Explorer, locate the `.dacpac` created by the build process and copy it into a new folder outside of the project directory as `original_project.dacpac`. Use this `.dacpac` file for comparison to validate your conversion later.

## Step 3: Modify the project file to an SDK-style project

Modifying the project file is a manual process, best performed in a text editor. Open the `.sqlproj` file in a text editor and make the following changes:

### Required: Add the SDK reference

Inside the project element, add an `Sdk` item to reference Microsoft.Build.Sql and the latest version from <https://www.nuget.org/packages/Microsoft.build.sql> where `#.#.#` is included in the snippet below.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="4.0">
  <Sdk Name="Microsoft.Build.Sql" Version="#.#.#" />
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

Original SQL projects have an entry for a `Properties` folder that represented access to the project properties in solution explorer. Remove this item from the project file.

Example to remove if present in your `.sqlproj`:

```xml
<ItemGroup>
  <Folder Include="Properties" />
</ItemGroup>
```

### Required: Remove Build items included by default

Original SQL projects list all `.sql` files representing database objects explicitly in the project file as `<Build Include="..." />` items. In SDK-style SQL projects, any `.sql` files in the project folder tree (`**/*.sql`) are included by default. Remove the `.sql` files specified in `<Build Include="...." />` items for those files to avoid build performance problems.

Remove lines like the following from the project file:

```xml
  <Build Include="SalesLT/Products.sql" />
  <Build Include="SalesLT/SalesLT.sql" />
  <Build Include="SalesLT/Categories.sql" />
  <Build Include="SalesLT/CategoriesProductCount.sql" />
```

Don't remove:

- `<Build Include="..." />` items for `.sql` files that aren't in the SQL project folder tree
- `<PreDeploy Include="..." />` or `<PostDeploy Include="..." />` items,  because these nodes dictate [specific behavior](../concepts/pre-post-deployment-scripts.md) for those files
- Items that aren't `.sql` files, such as `.publish.xml` files in `<None Include="..." />` items, `.refactorlog.xml` files in `<RefactorLog Include="..." />` items, or `.xsd` files in `<Build Include="..." />` items

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

Original SQL projects include two large blocks for Release and Debug build settings, while in SDK-style SQL projects the SDK knows the defaults for these options. If you don't have customizations to the build settings, consider removing these blocks:

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

The [project properties](../concepts/project-properties.md) reference lists the available properties and their defaults.

## Step 4: Solution files

Your project file might be referenced in a solution file (`.sln`). If you have a solution file, update it to reference the new SDK-style project file. If you don't have a solution file, you can skip this section and proceed to Step 5.

### Option 1: Create a new solution file

If the solution file contains only the SQL project, it's easier to remove the solution file and create a new solution file with the SDK-style project.

```bash
dotnet new sln --name MySolution
dotnet sln MySolution.sln add MyDatabaseProject\MyDatabaseProject.sqlproj
```

### Option 2: Edit the solution file

If the solution file contains multiple projects, update the solution file to reference the new SDK-style project file. You can edit the solution file in a text editor and change the project reference to the new SDK-style project file. The project reference in the solution file should look like this:

```xml
Project("{PROJECT_TYPE_GUID}") = "MyDatabaseProject", "MyDatabaseProject\MyDatabaseProject.sqlproj", "{PROJECT_GUID}"
EndProject
```

The `PROJECT_TYPE_GUID` value for a Microsoft.Build.Sql project is `42EA0DBD-9CF1-443E-919E-BE9C484E4577`. The `PROJECT_GUID` is a unique identifier for the project found in the project file `<ProjectGuid>` element. If you have a solution file with your project, you don't need to change the `PROJECT_GUID` value. Change the `PROJECT_TYPE_GUID` value to the Microsoft.Build.Sql project type GUID.

## Step 5: Build a `.dacpac` file from the modified project for comparison

::: zone pivot="sq1-visual-studio"

The SQL project is no longer compatible with Visual Studio 2022. To build or edit the project, use one of the following options:

- The command line
- The SQL Database Projects extension in Visual Studio Code
- The SQL Server Data Tools, SDK-style (preview) in Visual Studio 2022
- SQL Server Management Studio (SSMS) with the Database DevOps workload (preview)

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

The project file is now in the SDK-style format, but to open it in Visual Studio 2022, you must have the SQL Server Data Tools, SDK-style (preview) installed. Open the project in Visual Studio 2022 with [SQL Server Data Tools, SDK-style (preview)](../../../ssdt/sql-server-data-tools-sdk-style.md) installed.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

Open the project folder in Visual Studio Code. In the **Database Projects** view of Visual Studio Code, right-click the project node and select **Build**.

::: zone-end

:::zone pivot="sq1-sql-server-management-studio"

Open the project file in SQL Server Management Studio (SSMS) with the Database DevOps workload (preview) installed. In the Object Explorer, right-click on the database project and select **Build**.

::: zone-end

::: zone pivot="sq1-command-line"

You can build SQL database projects from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

::: zone-end

The build process creates a `.dacpac` file in the `bin\Debug` folder of the project by default. Using File Explorer, locate the `.dacpac` created by the build process and copy it into a new folder outside of the project directory. Use this `.dacpac` file for comparison to validate your conversion later.

## Step 6: Verify that the `.dacpac` files are the same

To verify that the conversion was successful, compare the `.dacpac` files created from the original and modified projects. Use the [schema comparison](../concepts/schema-comparison.md) capabilities of SQL projects to visualize the difference in database models between the two `.dacpac` files. Alternatively, use the DacpacVerify command-line utility to compare the two `.dacpac` files, including their pre/post-deployment scripts and project settings.

You can install DacpacVerify as a [dotnet tool](https://www.nuget.org/packages/Microsoft.dacpacverify). To install the tool, run the following command:

```bash
dotnet tool install --global Microsoft.DacpacVerify --prerelease
```

The syntax for DacpacVerify is to specify the filepath to two `.dacpac` files as `dacpacverify <source DACPAC path> <target DACPAC path>`. To compare the two `.dacpac` files, run the following command:

```bash
DacpacVerify original_project.dacpac modified_project.dacpac
```

You can use the schema compare tool to compare objects in the `.dacpac` files.

::: zone pivot="sq1-visual-studio"

Launch Visual Studio without a project loaded. Go to **Tools** > **SQL Server** > **New Schema Comparison**. Select the original `.dacpac` file as the source and the modified `.dacpac` file as the target. For more on using Schema Compare in Visual Studio, see [using schema compare to compare different database definitions](../../../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Graphical schema comparison isn't yet available in the SDK-style SQL projects preview in Visual Studio. Use Visual Studio Code to compare schemas.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In Visual Studio Code, install the **SQL Server Schema Compare** extension if it isn't already installed. Launch a new schema comparison from the command palette by opening the command palette with `Ctrl/Cmd+Shift+P` and typing `Schema Compare`.

Select the original `.dacpac` file as the source and the modified `.dacpac` file as the target.

::: zone-end

:::zone pivot="sq1-sql-server-management-studio"

Graphical schema comparison isn't available in SQL Server Management Studio. Use Visual Studio Code or Visual Studio to compare schemas.

::: zone-end

::: zone pivot="sq1-command-line"

Graphical schema comparison is available in Visual Studio and Visual Studio Code.

::: zone-end

When you run schema comparison, no results should be displayed. The lack of differences indicates that the original and modified projects are equivalent, producing the same database model in the `.dacpac` file.

> [!NOTE]  
> The comparison of `.dacpac` files through schema comparison doesn't validate pre/post-deployment scripts, refactorlog, or other project settings. It only validates the database model. Using the DacpacVerify command-line utility is the recommended way to validate that the two `.dacpac` files are equivalent.

## Related content

- [What are SQL database projects?](../sql-database-projects.md)
- [Get started with SQL database projects](../get-started.md)
- [SQL projects package references](../concepts/package-references.md)
