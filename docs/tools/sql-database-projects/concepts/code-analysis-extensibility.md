---
title: Code analysis rules extensibility overview
description: "Create custom T-SQL code analysis rules for use with SQL projects."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
---

# Code analysis rules extensibility overview

The [provided code analysis rules](./sql-code-analysis/sql-code-analysis.md#provided-rules) report on Transact-SQL design, naming, and performance warnings in your database code. If the built-in code analysis rules don't include coverage for a specific T-SQL issue or antipattern that you want to automatically detect, you can create custom database code analysis rules.

For example, you might want to create a custom rule that avoids using the WAITFOR DELAY statement, as demonstrated in [Author custom code analysis rules](../howto/author-custom-code-analysis-rules.md). To create custom database code analysis rules, you use the classes in the [CodeAnalysis](/dotnet/api/microsoft.sqlserver.dac.codeanalysis) namespace.

This overview covers the basic architecture among the various components of database code analysis rules.

## Database code analysis rules components

The following diagram illustrates how database code analysis rules components are processed:

:::image type="content" source="media/code-analysis-extensibility/code-analysis-extensibility.png" alt-text="Screenshot of Database code analysis processing flow." lightbox="media/code-analysis-extensibility/code-analysis-extensibility.png":::

When you use the database code analysis rules feature, all the rules are loaded and used according to how you configured them in your project. For more information, see [How to: Enable and Disable Specific Rules for Static Analysis of Database Code](/previous-versions/visualstudio/visual-studio-2010/dd172131(v=vs.100)). The Extension Manager also loads any custom rule assemblies that you created and registered.

A custom code analysis rule class inherits from [SqlCodeAnalysisRule](/dotnet/api/microsoft.sqlserver.dac.codeanalysis.sqlcodeanalysisrule). The custom rule class can access useful objects via its rule execution context. These objects include:

- Metadata about the rule itself.
- The `Dac.Model.TSqlModel` representing the database's schema, including all the model elements, relationships between these and any properties of the elements.
- For rules that examine specific elements the `Dac.Model.TSqlObject` representing that schema element in the model is included in the context.
- Many schema objects also have a [ScriptDom](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom) representation, which can be accessed via this context. This is an AST-based representation of an element that can be useful when trying to see potential syntax issues such as the presence of [SelectStarExpression](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom.selectstarexpression).

For any problems found by the rule, a `Dac.CodeAnalysis.SqlRuleProblem` object is created. When creating this `SqlRuleProblem` object, the relevant `Dac.Model.TSqlObject` and possibly a [ScriptDom](/dotnet/api/microsoft.sqlserver.transactsql.scriptdom) representation element are passed into the constructor, and these are used to determine the location of the problem in your source code files. At the end of analysis, all of these problems are passed to the Error Manager and displayed in the Error List.

## Incorporate custom rules in a SQL project

With the Microsoft.Build.Sql SDK-style SQL projects, you include custom code analysis rules in the project by adding a package reference that contains the rules.

## Related content

- [SQL code analysis to improve code quality](sql-code-analysis/sql-code-analysis.md)
- [Analyze T-SQL code to find defects](../howto/analyze-t-sql-code-to-find-defects.md)
- [Author custom code analysis rules](../howto/author-custom-code-analysis-rules.md)
