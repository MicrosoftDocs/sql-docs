---
title: "SQL code analysis"
description: "Analyzing Database Code to Improve Code Quality"
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
zone_pivot_groups: sq1-sql-projects-tools
---

# SQL code analysis to improve code quality

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can eliminate potential design and naming problems and avoid performance pitfalls by analyzing your database code. The concepts are very similar to performing static analysis to detect and correct defects in managed code. You configure which analysis rules you want to apply to your database code, analyze the code, and then correct or ignore the issues that you identify. Before you can analyze your database code, you must first import your database schema into a database project. For more information, see [Start from an existing database](../../tutorials/start-from-existing-database.md).

By performing static analysis with the [provided rules](#provided-rules), you can identify problems that fall into the following categories:

- [T-SQL Design Issues](#t-sql-design-issues)
  Design issues include code that might not behave the way in which you expect, deprecated syntax, and issues that could cause problems when the design of your database changes.

- [T-SQL Naming Issues](#t-sql-naming-issues)
  Naming issues arise if the name of a database object might cause unexpected problems or violate generally accepted conventions.

- [T-SQL Performance Issues](#t-sql-performance-issues)
  Performance issues include code that might noticeably reduce the speed in which database operations are completed. Many of these issues identify code that will cause a table scan when the code is executed.

    :::image type="content" source="media/sql-code-analysis/static-analysis-rules.png" alt-text="Screenshot of SQL Server Data Tools project settings for code analysis rules." lightbox="media/sql-code-analysis/static-analysis-rules.png":::

Code analysis rules are extensible. You can create your own rules to enforce your own coding standards. For more information, see [Code analysis rules extensibility overview](../code-analysis-extensibility.md).

## SQL project file sample and syntax

A SQL project file can contain two properties, `RunSqlCodeAnalysis` and `SqlCodeAnalysisRules`. The `RunSqlCodeAnalysis` element specifies whether code analysis is run when the project is built. By default all included rules are run and rule pattern detection results in a build warning.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="0.2.0-preview" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
    <RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>
  </PropertyGroup>
...
```

The `SqlCodeAnalysisRules` element specifies the rules and their error/warning behavior. In the following example the rules Microsoft.Rules.Data.SR0006 and Microsoft.Rules.Data.SR0007 are disabled and a detection for the rule Microsoft.Rules.Data.SR0008 will result in a build error.

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

A file `StaticCodeAnalysis.SuppressMessages.xml` can be added to the project to suppress specific code analysis findings. The following example suppresses the warning `SR0001` for the stored procedure in the file `StoredProcedures/uspGetEmployeeManagers.sql`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<StaticCodeAnalysis version="2" xmlns="urn:Microsoft.Data.Tools.Schema.StaticCodeAnalysis">
  <SuppressedFile FilePath="StoredProcedures/uspGetEmployeeManagers.sql">
    <SuppressedRule Category="Microsoft.Rules.Data" RuleId="SR0001" />
  </SuppressedFile>
</StaticCodeAnalysis>
```

## Provided rules

### T-SQL Design Issues

When you analyze the T-SQL code in your database project, one or more warnings might be categorized as design issues. You should address design issues to avoid the following situations:

- Subsequent changes to your database might break applications that depend on it.
- The code might not produce the expected result.
- The code might break if you run it with future releases of SQL Server.

In general, you shouldn't suppress a design issue because it might break your application, either now or in the future.

The provided rules identify the following design issues:

- [SR0001: Avoid SELECT * in stored procedures, views, and table-valued functions](./t-sql-design-issues.md#sr0001-avoid-select--in-stored-procedures-views-and-table-valued-functions)
- [SR0008: Consider using SCOPE_IDENTITY instead of @@IDENTITY](./t-sql-design-issues.md#sr0008-consider-using-scope_identity-instead-of-identity)
- [SR0009: Avoid using types of variable length that are size 1 or 2](./t-sql-design-issues.md#sr0009-avoid-using-types-of-variable-length-that-are-size-1-or-2)
- [SR0010: Avoid using deprecated syntax when you join tables or views](./t-sql-design-issues.md#sr0010-avoid-using-deprecated-syntax-when-you-join-tables-or-views)
- [SR0013: Output parameter (parameter) isn't populated in all code paths](./t-sql-design-issues.md#sr0013-output-parameter-parameter-isnt-populated-in-all-code-paths)
- [SR0014: Data loss might occur when casting from {Type1} to {Type2}](./t-sql-design-issues.md#sr0014-data-loss-might-occur-when-casting-from-type1-to-type2)

### T-SQL Naming Issues

When you analyze the T-SQL code in your database project, one or more warnings might be categorized as naming issues. You should address naming issues to avoid the following situations:

- The name that you specified for an object might conflict with the name of a system object.
- The name that you specified will always need to be enclosed in escape characters (in SQL Server, '[' and ']').
- The name that you specified might confuse others who try to read and understand your code.
- The code might break if you run it with future releases of SQL Server.

In general, you might suppress a naming issue if other applications that you can't change depend on the current name.

The provided rules identify the following design issues:

- [SR0011: Avoid using special characters in object names](./t-sql-naming-issues.md#sr0011-avoid-using-special-characters-in-object-names)
- [SR0012: Avoid using reserved words for type names](./t-sql-naming-issues.md#sr0012-avoid-using-reserved-words-for-type-names)
- [SR0016: Avoid using sp_ as a prefix for stored procedures](./t-sql-naming-issues.md#sr0016-avoid-using-sp_-as-a-prefix-for-stored-procedures)

### T-SQL Performance Issues

When you analyze the T-SQL code in your database project, one or more warnings might be categorized as performance issues. You should address a performance issue to avoid the following situation:

- A table scan will occur when the code is executed.

In general, you might suppress a performance issue if the table contains so little data that a scan will not cause performance to drop significantly.

The provided rules identify the following design issues:

- [SR0004: Avoid using columns that do not have indexes as test expressions in IN predicates](./t-sql-performance-issues.md#sr0004-avoid-using-columns-that-dont-have-indexes-as-test-expressions-in-in-predicates)
- [SR0005: Avoid using patterns that start with "%" in LIKE predicates](./t-sql-performance-issues.md#sr0005-avoid-using-patterns-that-start-with--in-like-predicates)
- [SR0006: Move a column reference to one side of a comparison operator to use a column index](./t-sql-performance-issues.md#sr0006-move-a-column-reference-to-one-side-of-a-comparison-operator-to-use-a-column-index)
- [SR0007: Use ISNULL(column, default_value) on nullable columns in expressions](./t-sql-performance-issues.md#sr0007-use-isnullcolumn-default_value-on-nullable-columns-in-expressions)
- [SR0015: Extract deterministic function calls from WHERE predicates](./t-sql-performance-issues.md#sr0015-extract-deterministic-function-calls-from-where-predicates)

## Enable and disable code analysis

::: zone pivot="sq1-visual-studio"

To enable or disable SQL code analysis in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Code Analysis** tab of the properties window, select the desired code analysis settings.

To disable or enable a specific rule, select the rule from the table. To change the severity of a rule, select the box for **Treat Warning as Error** for that rule from the list.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

To enable or disable SQL code analysis in the SDK-style SQL projects (preview), edit the `.sqlproj` file directly. Open the `.sqlproj` file from the **Solution Explorer** view by double-clicking on the project.

From the text editor, add an element `<RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>` to the first `<PropertyGroup>` block to enable code analysis. To disable code analysis change the value of the `RunSqlCodeAnalysis` element to `True` or `False` or remove the element entirely.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

To enable or disable SQL code analysis in the SQL Database Projects extension, edit the `.sqlproj` file directly. Open the `.sqlproj` file from the **Explorer** view or by right-clicking on the project in the **Database Projects** view and selecting **Edit .sqlproj File**.

From the text editor, add an element `<RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>` to the first `<PropertyGroup>` block to enable code analysis. To disable code analysis change the value of the `RunSqlCodeAnalysis` element to `True` or `False` or remove the element entirely.

::: zone-end

::: zone pivot="sq1-command-line"

To override the code analysis settings in the project file, you can use the `/p:RunSqlCodeAnalysis` and `/p:SqlCodeAnalysisRules` properties with the `dotnet build` command. For example, to build with code analysis disabled:

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
