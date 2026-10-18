---
title: Transact-SQL Editor Options
description: Become familiar with Transact-SQL Editor options. Learn about query execution properties and query result properties, and see how to adjust the values.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: concept-article
f1_keywords:
  - "sql.data.tools.SqlExecutionAdvancedSettingsOption"
  - "sql.data.tools.SqlExecutionAnsiSettingsDlg"
  - "sql.data.tools.SqlResultToTextSettingsDlg"
  - "sql.data.tools.SqlExecutionGeneralSettingsDlg"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_RESULTS.GENERAL"
  - "sql.data.tools.unittesting.tsqleditor"
  - "sql.data.tools.SqlResultsToGridSettingsDlg"
  - "sql.data.tools.queryresults.executequeryfailure"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.GENERAL"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.EDITOR_TAB_AND_STATUS_BAR"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.GENERAL"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_RESULTS.RESULTS_TO_TEXT"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_EXECUTION.ANSI"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_EXECUTION.GENERAL"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_RESULTS.RESULTS_TO_GRID"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.ONLINE_EDITING"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_EXECUTION.ADVANCED"
---

# Transact-SQL Editor options

This article describes some of the options of the Transact-SQL Editor. To set these options, go to the **Option** dialog through the **Tools** > **Options** menu.

## Query execution

| Property | Description |
| --- | --- |
| `SET ROWCOUNT` | The default value of `0` indicates that SQL Server waits for results until all results are received. Provide a value greater than `0` if you want SQL Server to halt the query after it obtains the specified number of rows. To turn off this option (so that all rows are returned), specify `SET ROWCOUNT 0`. |
| `SET TEXTSIZE` | The default value of 2,147,483,647 bytes indicates that SQL Server provides a complete data field up to the limit of the **text**, **ntext**, **nvarchar(max)**, and **varchar(max)** data fields. It doesn't affect the XML data type. Provide a smaller number to limit results when there are large values. Columns greater than the provided number are truncated. |
| **Execution time-out** | This value indicates the number of seconds to wait before canceling the query. A value of `0` indicates an infinite wait, or no time-out. |
| **By default, open new queries in SQLCMD Mode** | Select this checkbox to open new queries in SQLCMD mode. This checkbox is visible only when you open the dialog through the **Tools** menu.<br /><br />When you select this option, be aware of the following limitations:<br /><br />- IntelliSense in the Database Engine Query Editor is turned off.<br /><br />- Because the Query Editor doesn't run from the command line, you can't pass in command-line parameters such as variables.<br /><br />- Because the Query Editor can't respond to operating-system prompts, you must be careful not to run interactive statements. |
| `SET NOCOUNT` | This property stops the message that indicates the number of rows affected by a Transact-SQL statement from being returned as part of the results. For more information, see [SET NOCOUNT](../t-sql/statements/set-nocount-transact-sql.md). |
| `SET NOEXEC` | When the value is `ON`, this property tells [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to compile each batch of Transact-SQL statements but not to run them. When the value is `OFF`, the property tells [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to run all batches after compilation. For more information, see [SET NOEXEC](../t-sql/statements/set-noexec-transact-sql.md). |
| `SET PARSEONLY` | This property checks the syntax of each Transact-SQL statement and returns any error messages without compiling or running the statement. For more information, see [SET PARSEONLY](../t-sql/statements/set-parseonly-transact-sql.md). |
| `SET CONCAT_NULL_YIELDS_NULL` | This property controls whether concatenation results are treated as null or empty string values. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../t-sql/statements/set-concat-null-yields-null-transact-sql.md). |
| `SET ARITHABORT` | This property terminates a query when an overflow or divide-by-zero error occurs during query execution. For more information, see [SET ARITHABORT](../t-sql/statements/set-arithabort-transact-sql.md). |
| `SET SHOWPLAN_TEXT` | This property causes [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] not to run Transact-SQL statements. Instead, SQL Server returns detailed information about how the statements are run. For more information, see [SET SHOWPLAN_TEXT](../t-sql/statements/set-showplan-text-transact-sql.md). |
| `SET STATISTICS TIME` | This property displays the number of milliseconds required to parse, compile, and run each statement. |
| `SET STATISTICS IO` | This property causes [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to display information about the amount of disk activity that Transact-SQL statements generate. |
| `SET TRANSACTION ISOLATION LEVEL` | This property controls the default transaction-locking behavior for all [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] `SELECT` statements that a connection issues. For more information, see [SET TRANSACTION ISOLATION LEVEL](../t-sql/statements/set-transaction-isolation-level-transact-sql.md). |
| `SET LOCK_TIMEOUT` | This property specifies the number of milliseconds that a statement waits for a lock to be released. For more information, see [SET LOCK_TIMEOUT](../t-sql/statements/set-lock-timeout-transact-sql.md). |
| `SET QUERY_GOVERNOR_COST_LIMIT` | This property overrides the currently configured value for the current connection. For more information, see [SET QUERY_GOVERNOR_COST_LIMIT](../t-sql/statements/set-query-governor-cost-limit-transact-sql.md). |
| `SET ANSI_DEFAULTS` | This property controls a group of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] settings that collectively specify some SQL-92 standard behavior. For more information, see [SET ANSI_DEFAULTS](../t-sql/statements/set-ansi-defaults-transact-sql.md). |
| `SET QUOTED_IDENTIFIER` | This property causes [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to follow the SQL-92 rules about quotation-mark-delimiting identifiers and literal strings. Identifiers delimited by double quotation marks can be Transact-SQL reserved keywords, or they can contain characters that the Transact-SQL syntax rules for identifiers don't typically allow. For more information, see [SET QUOTED_IDENTIFIER](../t-sql/statements/set-quoted-identifier-transact-sql.md). |
| `SET ANSI_NULL_DFLT_ON` | This property alters the session's behavior to override default nullability of new columns when the ANSI null default option for the database is `false`. For more information, see [SET ANSI_NULL_DFLT_ON](../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md). |
| `SET IMPLICIT_TRANSACTIONS` | When the value is `ON`, this property sets the connection to implicit transaction mode. When the value is `OFF`, the property returns the connection to autocommit transaction mode. For more information, see [SET IMPLICIT_TRANSACTIONS](../t-sql/statements/set-implicit-transactions-transact-sql.md). |
| `SET CURSOR_CLOSE_ON_COMMIT` | This property controls whether or not a cursor is closed when a transaction is committed. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](../t-sql/statements/set-cursor-close-on-commit-transact-sql.md). |
| `SET ANSI_PADDING` | This property controls the way the column stores values shorter than the defined size of the column. It also controls the way the column stores values that have trailing blanks in `char`, `varchar`, `binary`, and `varbinary` data. For more information, see [SET ANSI_PADDING](../t-sql/statements/set-ansi-padding-transact-sql.md). |
| `SET ANSI_WARNINGS` | This property specifies SQL-92 standard behavior for several error conditions. For more information, see [SET ANSI_WARNINGS](../t-sql/statements/set-ansi-warnings-transact-sql.md). |
| `SET ANSI_NULLS` | This property specifies SQL-92 compliant behavior for the equals (`=`) and not-equal-to (`<>`) comparison operators when they're used with null values. For more information, see [SET ANSI_NULLS](../t-sql/statements/set-ansi-nulls-transact-sql.md). |

## Query results

| Property | Description |
| --- | --- |
| **Include the query in the result set** | Return the text of the query as part of the result set. |
| **Include column headers when copying or saving the results** | Include column headers (titles) when results are copied to the clipboard or saved in a file. Clear this checkbox if you want saved or copied result data to have only the data and not the column headings. |
| **Discard results after execution** | Free memory by discarding the query results after the screen display receives them. |
| **Display results in a separate tab** | Display the result set in a new document window, instead of at the bottom of the query document window. |
| **Switch to results tab after the query executes** | Automatically set the screen focus to the result set. |
| **Maximum Characters Retrieved** | For non-XML data:<br /><br />Enter a number from 1 through 65,535 to specify the maximum number of characters that can be displayed in each cell.<br /><br />Specifying a large number of characters might cause data in the result set to appear truncated. The maximum number of characters displayed in each cell depends on the font size. When large result sets are returned, a high value in this box can cause SQL Server Management Studio to run low on memory and hinder system performance.<br /><br />For XML data:<br /><br />Select **1 MB**, **2 MB**, or **5 MB**. Select **Unlimited** to retrieve all characters. |
| **Output format** | By default, the output is displayed in columns created by padding the results with spaces. Other options are using commas, tabs, or spaces to separate columns. Select the **Custom delimiter** checkbox to specify a different delimiting character in the **Custom delimiter** box. |
| **Custom delimiter** | Specify the character of your choice to separate columns. This option is available only if the **Custom delimiter** checkbox is selected in the **Output format** box. |
| **Include column headers in the result set** | Clear this checkbox if you don't want each column labeled with a column title. |
| **Scroll as results are received** | Select this checkbox to keep the display focus on the most recently returned records at the bottom. Clear this checkbox to keep the display focus on the first rows received. |
| **Right align numeric values** | Select this checkbox to align numeric values to the right of the column. This option can make it easier to review numbers with a fixed number of decimal places. |
| **Discard result after query executes** | Select this checkbox to free memory by discarding the query results after the screen display receives them. |
| **Display results in a separate tab** | Select this checkbox to display the result set in a new document window instead of at the bottom of the query document window. |
| **Switch to results tab after the query executes** | Select this checkbox to automatically set the screen focus to the result set. |
| **Maximum number of characters displayed in each column** | The value defaults to 256. Increase this value to display larger result sets without truncation. |
| **Reset to Default** | Reset all values on this page to the original default values. |

## Related content

- [Set Operators](../mdx/set-operators.md)
- [Transact-SQL syntax conventions (Transact-SQL)](../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
- [SQL Server Data Tools](sql-server-data-tools.md)
