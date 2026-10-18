---
title: SQL Code Analysis
description: Analyze database code to improve code quality.
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 04/07/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - ignite-2024
zone_pivot_groups: sq1-sql-projects-tools
---

# SQL code analysis to improve code quality

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

You can eliminate potential design and naming problems and avoid performance pitfalls by analyzing your database code. The concepts are similar to performing static analysis to detect and correct defects in managed code. You configure which analysis rules you want to apply to your database code, analyze the code, and then correct or ignore the issues that you identify. Before you can analyze your database code, you must first import your database schema into a database project. For more information, see [Start from an existing database](../../tutorials/start-from-existing-database.md).

By performing static analysis with the [provided rules](#provided-rules), you can identify problems that fall into the following Transact-SQL (T-SQL) categories:

- [T-SQL design problems](#t-sql-design-problems): Design problems include code that might not behave the way you expect, deprecated syntax, and issues that could cause problems when the design of your database changes.

- [T-SQL naming problems](#t-sql-naming-problems): Naming problems arise if the name of a database object might cause unexpected problems or violate generally accepted conventions.

- [T-SQL performance problems](#t-sql-performance-problems): Performance problems include code that might noticeably reduce the speed of database operations. Many of these problems identify code that causes a table scan when the code runs.

  :::image type="content" source="media/sql-code-analysis/static-analysis-rules.png" alt-text="Screenshot of SQL Server Data Tools project settings for code analysis rules.":::

Code analysis rules are extensible. You can create your own rules to enforce your own coding standards. For more information, see [Code analysis rules extensibility overview](../code-analysis-extensibility.md).

## SQL project file sample and syntax

A SQL project file can contain two properties, `RunSqlCodeAnalysis` and `SqlCodeAnalysisRules`. The `RunSqlCodeAnalysis` element specifies whether to run code analysis when the project is built. By default, the build runs all included rules, and rule pattern detection results in a build warning.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="1.0.0" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
    <RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>
  </PropertyGroup>
...
```

The `SqlCodeAnalysisRules` element specifies the rules and their error or warning behavior. In the following example, the rules `Microsoft.Rules.Data.SR0006` and `Microsoft.Rules.Data.SR0007` are disabled, and a detection for the rule `Microsoft.Rules.Data.SR0008` results in a build error.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="1.0.0" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
    <RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>
    <SqlCodeAnalysisRules>-Microsoft.Rules.Data.SR0006;-Microsoft.Rules.Data.SR0007;+!Microsoft.Rules.Data.SR0008</SqlCodeAnalysisRules>
  </PropertyGroup>
...
```

You can add a `StaticCodeAnalysis.SuppressMessages.xml` file to the project to suppress specific code analysis findings. The following example suppresses the warning `SR0001` for the stored procedure in the file `StoredProcedures/uspGetEmployeeManagers.sql`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<StaticCodeAnalysis version="2" xmlns="urn:Microsoft.Data.Tools.Schema.StaticCodeAnalysis">
  <SuppressedFile FilePath="StoredProcedures/uspGetEmployeeManagers.sql">
    <SuppressedRule Category="Microsoft.Rules.Data" RuleId="SR0001" />
  </SuppressedFile>
</StaticCodeAnalysis>
```

## Provided rules

### T-SQL design problems

When you analyze the T-SQL code in your database project, you might see one or more warnings categorized as design problems. Address these problems to avoid the following situations:

- Subsequent changes to your database might break applications that depend on it.
- The code might not produce the expected result.
- The code breaks if you run it with future releases of SQL Server.

In general, don't suppress a design problem because it might break your application, either now or in the future.

The provided rules identify the following design problems:

- [SR0001: Avoid SELECT * in stored procedures, views, and table-valued functions](t-sql-design-issues.md#sr0001-avoid-select--in-stored-procedures-views-and-table-valued-functions)
- [SR0008: Consider using SCOPE_IDENTITY instead of @@IDENTITY](t-sql-design-issues.md#sr0008-consider-using-scope_identity-instead-of-identity)
- [SR0009: Avoid using types of variable length that are size 1 or 2](t-sql-design-issues.md#sr0009-avoid-using-types-of-variable-length-that-are-size-1-or-2)
- [SR0010: Avoid using deprecated syntax when you join tables or views](t-sql-design-issues.md#sr0010-avoid-using-deprecated-syntax-when-you-join-tables-or-views)
- [SR0013: Output parameter (parameter) isn't populated in all code paths](t-sql-design-issues.md#sr0013-output-parameter-parameter-isnt-populated-in-all-code-paths)
- [SR0014: Data loss might occur when casting from {Type1} to {Type2}](t-sql-design-issues.md#sr0014-data-loss-might-occur-when-casting-from-type1-to-type2)

### T-SQL naming problems

When you analyze the T-SQL code in your database project, you might see one or more warnings categorized as naming problems. Address these problems to avoid the following situations:

- The name you specify for an object conflicts with the name of a system object.
- You must always enclose the specified name in escape characters (for example in SQL Server, `[` and `]`).
- The name you specify confuses others who try to read and understand your code.
- The code breaks if you run it with future releases of SQL Server.

In general, suppress a naming problem if other applications that you can't change depend on the current name.

The provided rules identify the following design problems:

- [SR0011: Avoid using special characters in object names](t-sql-naming-issues.md#sr0011-avoid-using-special-characters-in-object-names)
- [SR0012: Avoid using reserved words for type names](t-sql-naming-issues.md#sr0012-avoid-using-reserved-words-for-type-names)
- [SR0016: Avoid using sp_ as a prefix for stored procedures](t-sql-naming-issues.md#sr0016-avoid-using-sp_-as-a-prefix-for-stored-procedures)

### T-SQL performance problems

When you analyze the T-SQL code in your database project, you might see one or more warnings categorized as performance problems. Address a performance problem to avoid the following situation:

- A table scan occurs when you run the code.

In general, you can suppress a performance problem if the table contains so little data that a scan doesn't significantly affect performance.

The provided rules identify the following design problems:

- [SR0004: Avoid using columns that do not have indexes as test expressions in IN predicates](t-sql-performance-issues.md#sr0004-avoid-using-columns-that-dont-have-indexes-as-test-expressions-in-in-predicates)
- [SR0005: Avoid using patterns that start with "%" in LIKE predicates](t-sql-performance-issues.md#sr0005-avoid-using-patterns-that-start-with--in-like-predicates)
- [SR0006: Move a column reference to one side of a comparison operator to use a column index](t-sql-performance-issues.md#sr0006-move-a-column-reference-to-one-side-of-a-comparison-operator-to-use-a-column-index)
- [SR0007: Use ISNULL(column, default_value) on nullable columns in expressions](t-sql-performance-issues.md#sr0007-use-isnullcolumn-default_value-on-nullable-columns-in-expressions)
- [SR0015: Extract deterministic function calls from WHERE predicates](t-sql-performance-issues.md#sr0015-extract-deterministic-function-calls-from-where-predicates)

## Enable and disable code analysis

::: zone pivot="sq1-visual-studio-code"

To enable or disable SQL code analysis in the SQL Database Projects extension, edit the `.sqlproj` file directly or use the code analysis settings dialog.

### Use the Code Analysis Settings dialog in Visual Studio Code

The SQL Database Projects extension provides a settings dialog to configure code analysis rules without directly editing the `.sqlproj` file.

To open the Code Analysis Settings dialog, right-click your project in the **Database Projects** view and select **Code Analysis Settings**.

:::image type="content" source="media/sql-code-analysis/code-analysis-settings-dialog.png" alt-text="Screenshot of the Code Analysis Settings dialog in Visual Studio Code showing the list of rules grouped by category.":::

In the dialog, you can:

- **Enable or disable code analysis on build** using the **Enable Code Analysis on Build** toggle at the top of the dialog.
- **Enable or disable a category of rules** by selecting or deselecting the checkbox next to the category name (Design, Naming, Performance).
- **Change the severity of a rule** by selecting a severity level from the dropdown list next to the rule. Available options are **Warning**, **Error**, and **None**.
- **Search for a rule** using the search bar at the top of the rules list.
- **Filter rules by severity** using the **All severities** dropdown list.

Select **OK** to save your changes and close the dialog, or **Apply** to save without closing. Select **Reset** to revert to the default settings.

### Edit the SQL project file to modify code analysis settings

From the text editor, add the `<RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>` element to the first `<PropertyGroup>` block to enable code analysis. To disable code analysis, change the value of the `RunSqlCodeAnalysis` element to `False` or remove the element entirely.

::: zone-end
::: zone pivot="sq1-sql-server-management-studio"

To enable or disable SQL code analysis in SQL Server Management Studio (SSMS), right-click the project in **Solution Explorer** and select **Properties**. In the **Code Analysis** tab of the properties window, select the desired code analysis settings.

To disable a specific rule or to change the severity of a rule, select the corresponding option from the dropdown list for that rule in the rule list.

::: zone-end
::: zone pivot="sq1-visual-studio"

To enable or disable SQL code analysis in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Code Analysis** tab of the properties window, select the desired code analysis settings.

To disable a specific rule or to change the severity of a rule, select the corresponding option from the dropdown list for that rule in the rule list.

::: zone-end
::: zone pivot="sq1-visual-studio-sdk"

To enable or disable SQL code analysis in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Code Analysis** tab of the properties window, select the desired code analysis settings.

To disable a specific rule or to change the severity of a rule, select the corresponding option from the dropdown list for that rule in the rule list.

::: zone-end
::: zone pivot="sq1-command-line"

To override the code analysis settings in the project file, use the `/p:RunSqlCodeAnalysis` and `/p:SqlCodeAnalysisRules` properties with the `dotnet build` command. For example, to build with code analysis disabled:

```bash
dotnet build /p:RunSqlCodeAnalysis=False
```

To build with specific SQL code analysis rule settings:

```bash
dotnet build /p:RunSqlCodeAnalysis=True /p:SqlCodeAnalysisRules="+!Microsoft.Rules.Data.SR0001;+!Microsoft.Rules.Data.SR0008"
```

::: zone-end

## Related content

- [Analyze T-SQL code to find defects](../../howto/analyze-t-sql-code-to-find-defects.md)
- [T-SQL design issues](t-sql-design-issues.md)
- [T-SQL naming issues](t-sql-naming-issues.md)
- [T-SQL performance issues](t-sql-performance-issues.md)
- [Code analysis rules extensibility overview](../code-analysis-extensibility.md)
