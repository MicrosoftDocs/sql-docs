---
title: Format T-SQL in the MSSQL Extension for Visual Studio Code
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to format T-SQL in Visual Studio Code with the SQL formatter in the MSSQL extension, including format on demand, format on save, and formatter configuration settings.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: yoleichen
ms.date: 09/28/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: how-to
ms.collection:
  - data-tools
ai-usage: ai-assisted
---
# Format Transact-SQL in the MSSQL extension for Visual Studio Code

Consistent formatting makes Transact-SQL (T-SQL) easier to read, review, and maintain, especially when multiple people contribute to the same codebase. The MSSQL extension for Visual Studio Code includes a built-in SQL formatter that you can run on demand, configure for automatic formatting on save, and customize through Visual Studio Code settings.

The T-SQL formatting functionality in the MSSQL extension is built on [ScriptDOM](https://github.com/microsoft/sqlscriptdom), an open-source .NET library that parses T-SQL and generates scripts based on abstract syntax trees.

## Format on demand

You can format T-SQL in any editor window. The formatter works on the whole document or only on the text that you select.

To format T-SQL on demand, use one of the following methods:

- **Context menu**: Right-click in a T-SQL editor window and select **Format Document** or **Format Selection**.

- **Command Palette**: Run **Format Document** or **Format Selection**.

- **Keyboard shortcut**: For **Format Document**, press <kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>F</kbd> on Windows and Linux, or <kbd>Shift</kbd>+<kbd>Option</kbd>+<kbd>F</kbd> on macOS. For **Format Selection**, press <kbd>Ctrl</kbd>+<kbd>K</kbd>, <kbd>Ctrl</kbd>+<kbd>F</kbd> on Windows and Linux, or <kbd>Cmd</kbd>+<kbd>K</kbd>, <kbd>Cmd</kbd>+<kbd>F</kbd> on macOS.

## Format on save

In Visual Studio Code, the standard editor setting controls format on save, rather than a dedicated MSSQL formatter setting.

Use the following settings in your Visual Studio Code `settings.json` file to format T-SQL automatically whenever you save a file:

```json
{
  "[sql]": {
    "editor.formatOnSave": true
  }
}
```

## Configure formatting options

Configure formatting in the Visual Studio Code Settings UI or in user or workspace `settings.json`.

In the Settings editor, search for **Mssql** > **Format** to view the available options. In `settings.json`, use the corresponding `mssql.format.*` settings.

## Supported settings

The following settings configure the SQL formatter.

#### General

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.showParseErrorNotification` | bool | `true` | Show a notification when the formatter can't fully parse the T-SQL. |
| `mssql.format.options.sqlVersion` | enum | `sql170` | T-SQL version used to parse and generate formatted scripts. |
| `mssql.format.options.sqlEngineType` | enum | `all` | [!INCLUDE [ssde-md](../../../includes/ssde-md.md)] type used to parse and generate formatted scripts. Valid values are `all`, `standalone`, and `sqlAzure`. |

#### Alignment

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.alignClauseBodies` | bool | `true` | Align bodies of `FROM`, `WHERE`, `GROUP BY`, and similar clauses. |
| `mssql.format.options.alignColumnDefinitionFields` | bool | `true` | Align column-definition fields, such as names, data types, and constraints. |
| `mssql.format.options.alignSetClauseItem` | bool | `true` | Align `SET` clause items in `UPDATE` statements. |
| `mssql.format.options.clauseBodyAlignment` | enum | `aligned` | Keep clause bodies `aligned` with their keywords or place them on the next line as `indented`. |

#### Casing and identifiers

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.builtInFunctionCasing` | enum | `preserve` | Casing style for supported built-in function names, such as `GETDATE` and `COALESCE`. Valid values are `preserve`, `uppercase`, `lowercase`, and `pascalCase`. |
| `mssql.format.options.identifierBracketing` | enum | `preserve` | Preserve, add, or remove optional square brackets around identifiers. Valid values are `preserve`, `includeBrackets`, and `excludeBrackets`. Required brackets are retained. |
| `mssql.format.options.identifierCasing` | enum | `preserve` | Casing style for object identifiers. Valid values are `preserve`, `uppercase`, `lowercase`, and `pascalCase`. |
| `mssql.format.options.keywordCasing` | enum | `uppercase` | Keyword casing style. Valid values are `uppercase`, `lowercase`, and `pascalCase`. |

#### Paths

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.allowExternalLanguagePaths` | bool | `true` | Allow external language content to use file paths. |
| `mssql.format.options.allowExternalLibraryPaths` | bool | `true` | Allow external library content to use file paths. |

#### Formatting

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.asKeywordOnOwnLine` | bool | `true` | Place `AS` on its own line. |
| `mssql.format.options.columnAliasStyle` | enum | `asKeyword` | Format column aliases using `AS`, an equals sign, or their original syntax. Valid values are `asKeyword`, `equalsSign`, and `preserve`. |
| `mssql.format.options.commaPlacement` | enum | `trailing` | Place commas at the end of list items (`trailing`) or at the beginning of the next item (`leading`). |
| `mssql.format.options.leadingCommaSpaceCount` | integer | `1` | Number of spaces after a leading comma. Valid values are `0` and `1`. |
| `mssql.format.options.persistTrailingGo` | bool | `false` | Preserve trailing `GO` batch separators from the original script. |
| `mssql.format.options.preserveComments` | bool | `true` | Preserve comments during formatting. |
| `mssql.format.options.terminateBlockStatements` | bool | `false` | Add semicolon terminators after `BEGIN...END` and `TRY...CATCH` blocks. |

#### Indentation

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.indentSetClause` | bool | `false` | Indent `SET` clauses in `UPDATE` statements. |
| `mssql.format.options.indentViewBody` | bool | `false` | Indent `VIEW` bodies. |

#### Multiline

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.multilineGroupByElementsList` | bool | `false` | Format `GROUP BY` elements as a multiline list. |
| `mssql.format.options.multilineHavingPredicatesList` | bool | `true` | Format `HAVING` predicates separated by `AND` or `OR` on multiple lines. |
| `mssql.format.options.multilineInsertSourcesList` | bool | `true` | Format `INSERT` sources as multiline lists. |
| `mssql.format.options.multilineInsertTargetsList` | bool | `true` | Format `INSERT` columns as multiline lists. |
| `mssql.format.options.multilineInValuesList` | bool | `false` | Format values in an `IN` predicate as a multiline list. |
| `mssql.format.options.multilineNestedFunctionCalls` | bool | `false` | Format nested function calls on separate indented lines while keeping isolated function calls on one line. |
| `mssql.format.options.multilineOrderByElementsList` | bool | `false` | Format `ORDER BY` elements as a multiline list. |
| `mssql.format.options.multilinePartitionByElementsList` | bool | `false` | Format `PARTITION BY` elements in window specifications as a multiline list. |
| `mssql.format.options.multilineProcedureParametersList` | bool | `false` | Format procedure and function parameters on separate lines. |
| `mssql.format.options.multilineSelectElementsList` | bool | `true` | Format `SELECT` columns as multiline lists. |
| `mssql.format.options.multilineSetClauseItems` | bool | `true` | Format `SET` clause items as multiline lists. |
| `mssql.format.options.multilineViewColumnsList` | bool | `true` | Format `VIEW` columns as multiline lists. |
| `mssql.format.options.multilineWherePredicatesList` | bool | `true` | Format `WHERE` predicates as multiline lists. |
| `mssql.format.options.multilineWithOptionsList` | bool | `false` | Format supported `WITH` and `OPTION` clause values on separate lines. |

#### New line

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.newLineAfterJoinKeyword` | bool | `true` | Place the joined table source on a new line after the `JOIN` keyword. |
| `mssql.format.options.newLineBeforeCloseParenthesisInMultilineList` | bool | `true` | Place a new line before the closing parenthesis of a multiline list. |
| `mssql.format.options.newLineBeforeFromClause` | bool | `true` | Place a new line before the `FROM` clause. |
| `mssql.format.options.newLineBeforeGroupByClause` | bool | `true` | Place a new line before the `GROUP BY` clause. |
| `mssql.format.options.newLineBeforeHavingClause` | bool | `true` | Place a new line before the `HAVING` clause. |
| `mssql.format.options.newLineBeforeJoinClause` | bool | `true` | Place a new line before `JOIN` clauses. |
| `mssql.format.options.newLineBeforeOffsetClause` | bool | `true` | Place a new line before the `OFFSET` clause. |
| `mssql.format.options.newLineBeforeOnClause` | bool | `true` | Place the `ON` clause of a join on a new line. |
| `mssql.format.options.newLineBeforeOpenParenthesisInMultilineList` | bool | `false` | Place a new line before the opening parenthesis of a multiline list. |
| `mssql.format.options.newLineBeforeOrderByClause` | bool | `true` | Place a new line before the `ORDER BY` clause. |
| `mssql.format.options.newLineBeforeOutputClause` | bool | `true` | Place a new line before the `OUTPUT` clause. |
| `mssql.format.options.newLineBeforeWhereClause` | bool | `true` | Place a new line before the `WHERE` clause. |
| `mssql.format.options.newLineBeforeWindowClause` | bool | `true` | Place a new line before the `WINDOW` clause. |
| `mssql.format.options.newlineFormattedCheckConstraint` | bool | `false` | Place the `CHECK` clause of a constraint on its own line. |
| `mssql.format.options.newLineFormattedIndexDefinition` | bool | `false` | Place `UNIQUE`, `INCLUDE`, and `WHERE` portions of inline index definitions on separate lines. |

#### Statement and batch spacing

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.numNewlinesAfterBatches` | integer | `1` | Number of line breaks after each `GO` batch separator, from `0` through `5`. |
| `mssql.format.options.numNewlinesAfterBatchStatement` | integer | `2` | Number of line breaks after each top-level statement in a batch, from `0` through `5`. |
| `mssql.format.options.numNewlinesAfterStatement` | integer | `1` | Number of line breaks after each statement, from `0` through `5`. |

#### Spacing

| Setting | Type | Default | Description |
| --- | --- | --- | --- |
| `mssql.format.options.spaceBetweenDataTypeAndParameters` | bool | `true` | Insert a space between a data type and its parentheses, for example `VARCHAR (255)`. |
| `mssql.format.options.spaceBetweenParametersInDataType` | bool | `true` | Insert spaces between parameters in a data type, for example `DECIMAL (10, 2)`. |

### Example settings file

```json
{
  "mssql.format.options.keywordCasing": "lowercase",
  "mssql.format.options.alignClauseBodies": false,
  "mssql.format.options.numNewlinesAfterStatement": 2,
  "[sql]": {
    "editor.formatOnSave": true
  }
}
```

### Set the default formatter

To set the MSSQL extension as the default, select **Configure Default Formatter...** > **SQL Server (mssql)**, or add the following configuration to `settings.json`:

```json
{
  "[sql]": {
    "editor.defaultFormatter": "ms-mssql.mssql"
  }
}
```

## Related content

- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md)
- [ScriptDOM GitHub repository](https://github.com/microsoft/sqlscriptdom)
