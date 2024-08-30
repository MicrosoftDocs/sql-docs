---
title: Analyze T-SQL code to find defects
description: "How to detect antipatterns and defects with code analysis."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: how-to
zone_pivot_groups: sq1-sql-projects-tools
---

# Analyze T-SQL code to find defects

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can improve the quality of the Transact-SQL code in a database schema by importing it into a database project and analyzing the code against a set of rules. For example, you might want to find any errors in a schema that you didn't develop and whose quality has not been verified. For more information, see the [code analysis overview](../concepts/sql-code-analysis/sql-code-analysis.md).

For this initial assessment, you want to find all the potential problems in the database code. You review the warnings and the code that caused those warnings. To improve the T-SQL code, you correct warnings, potentially suppress a warning, and iteratively analyze the database project.

## Prerequisites

Before you can analyze the code in a database project, you must already have a SQL project. For more information on using an existing database to create a project, see [Tutorial: start from an existing database](../tutorials/start-from-an-existing-database.md).

## Enable SQL code analysis on project build

::: zone pivot="sq1-visual-studio"

To enable SQL code analysis in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Code Analysis** tab of the properties window, select the checkbox for **Enable Code Analysis on Build**.

Save the project properties window and return to solution explorer.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

To enable SQL code analysis in the SQL Database Projects extension, edit the `.sqlproj` file directly. Open the `.sqlproj` file from the **Explorer** view or by right-clicking on the project in the **Database Projects** view and selecting **Edit .sqlproj File**.

From the text editor, add an element `<RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>` to the first `<PropertyGroup>` block to enable code analysis.

::: zone-end

::: zone pivot="sq1-command-line"

To enable SQL code analysis in a SQL project, edit the `.sqlproj` file directly. Open the `.sqlproj` file and add an element `<RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>` to the first `<PropertyGroup>` block to enable code analysis.

::: zone-end

## Analyze the code

::: zone pivot="sq1-visual-studio"

To analyze the code in a database project with code analysis enabled on build, right-click the project in **Solution Explorer** and select **Build**.

The **output** window displays the results of the overall build process.

The T-SQL code in your database project is analyzed during build. Errors and warnings from code analysis appear in the **Error List**. If the **Error List** doesn't appear, open the View menu, and select **Error List**. You can double-click a warning to navigate to the line of code that caused the warning.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

To analyze the code in a database project with code analysis enabled on build, right-click the project in the **Database Projects** view and select **Build**.

The **output** window displays the results of the overall build process and any errors or warnings from code analysis. The files specified in each warning or error are interactive links that navigate to the line of code that caused the warning.

::: zone-end

::: zone pivot="sq1-command-line"

To analyze the code in a database project with code analysis enabled on build, run the `dotnet build` command from the command line in the project directory.

```bash
dotnet build MyDatabaseProject.sqlproj
```

The output from the command displays the results of the overall build process and any errors or warnings from code analysis.

::: zone-end

## Configure code analysis rules

::: zone pivot="sq1-visual-studio"

To disable or enable a specific rule in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Code Analysis** tab of the properties window, select the rule from the table. To change the severity of a rule, select the box for **Treat Warning as Error** for that rule from the list.

Save the project properties window and return to solution explorer.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

To disable or enable a specific rule in the SQL Database Projects extension, edit the `.sqlproj` file directly. Open the `.sqlproj` file from the **Explorer** view or by right-clicking on the project in the **Database Projects** view and selecting **Edit .sqlproj File**.

Add or modify the element for `SqlCodeAnalysisRules` in the first `<PropertyGroup>` block to specify the rules to enable or disable. The following sample configuration disables two rules (SR0007 and SR0006) and switches SR0008 to result in a build error. The rest of the rules are enabled by default.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="0.2.0-preview" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
    <RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>
    <SqlCodeAnalysisRules>-Microsoft.Rules.Data.SR0006;-Microsoft.Rules.Data.SR0007;+!Microsoft.Rules.Data.SR0008</SqlCodeAnalysisRules>
  </PropertyGroup>
...
```

::: zone-end

::: zone pivot="sq1-command-line"

To disable or enable a specific rule in a SQL project, edit the `.sqlproj` file directly. Open the `.sqlproj` file and add or modify the element for `SqlCodeAnalysisRules` in the first `<PropertyGroup>` block to specify the rules to enable or disable. The following sample configuration disables two rules (SR0007 and SR0006) and switches SR0008 to result in a build error. The rest of the rules are enabled by default.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="0.2.0-preview" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
    <RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>
    <SqlCodeAnalysisRules>-Microsoft.Rules.Data.SR0006;-Microsoft.Rules.Data.SR0007;+!Microsoft.Rules.Data.SR0008</SqlCodeAnalysisRules>
  </PropertyGroup>
...
```

::: zone-end

## Suppress code analysis warnings

::: zone pivot="sq1-visual-studio"

To suppress a code analysis error or warning for a specific `.sql` file in Visual Studio, right-click the warning in the **Error List** and select **Suppress Static Code Analysis Message(s)**. The code analysis result for that rule and `.sql` file is suppressed and no longer appears in the **Error List** or the build output.

> [!NOTE]  
> Suppressing a warning doesn't fix the underlying issue. Suppress warnings only when you have a valid reason to do so.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

To suppress a code analysis error or warning for a specific `.sql` file in the SQL Database Projects extension, add a `StaticCodeAnalysis.SuppressMessages.xml` file to the project. In the file, specify the rule ID and the file to suppress the warning for.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<StaticCodeAnalysis version="2" xmlns="urn:Microsoft.Data.Tools.Schema.StaticCodeAnalysis">
  <SuppressedFile FilePath="Views/SelectStarView.sql">
    <SuppressedRule Category="Microsoft.Rules.Data" RuleId="SR0001" />
  </SuppressedFile>
</StaticCodeAnalysis>
```

If the file doesn't exist, create it in the root of the project. If the file already exists, suppress an additional warning to the existing `StaticCodeAnalysis.SuppressMessages.xml` file by creating a new `<SuppressedFile><SuppressedRule /></SuppressedFile>` element.

The code analysis result for that rule and `.sql` file is suppressed and no longer appears in the build output.

::: zone-end

::: zone pivot="sq1-command-line"

To suppress a code analysis error or warning for a specific `.sql` file in a SQL project, add a `StaticCodeAnalysis.SuppressMessages.xml` file to the project. In the file, specify the rule ID and the file to suppress the warning for.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<StaticCodeAnalysis version="2" xmlns="urn:Microsoft.Data.Tools.Schema.StaticCodeAnalysis">
  <SuppressedFile FilePath="Views/SelectStarView.sql">
    <SuppressedRule Category="Microsoft.Rules.Data" RuleId="SR0001" />
  </SuppressedFile>
</StaticCodeAnalysis>
```

If the file doesn't exist, create it in the root of the project. If the file already exists, suppress an additional warning to the existing `StaticCodeAnalysis.SuppressMessages.xml` file by creating a new `<SuppressedFile><SuppressedRule /></SuppressedFile>` element.

The code analysis result for that rule and `.sql` file is suppressed and no longer appears in the build output.

::: zone-end

## Related content

- [T-SQL design issues](../concepts/sql-code-analysis/t-sql-design-issues.md)
- [T-SQL naming issues](../concepts/sql-code-analysis/t-sql-naming-issues.md)
- [T-SQL performance issues](../concepts/sql-code-analysis/t-sql-performance-issues.md)
