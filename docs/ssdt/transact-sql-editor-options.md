---
title: "Transact-SQL Editor Options | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_RESULTS.RESULTS_TO_GRID"
  - "sql.data.tools.SqlExecutionAdvancedSettingsOption"
  - "sql.data.tools.SqlExecutionAnsiSettingsDlg"
  - "sql.data.tools.SqlResultToTextSettingsDlg"
  - "sql.data.tools.SqlExecutionGeneralSettingsDlg"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_RESULTS.GENERAL"
  - "sql.data.tools.unittesting.tsqleditor"
  - "sql.data.tools.SqlResultsToGridSettingsDlg"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.GENERAL"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.EDITOR_TAB_AND_STATUS_BAR"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.GENERAL"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_RESULTS.RESULTS_TO_TEXT"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_EXECUTION.ANSI"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_EXECUTION.GENERAL"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.ONLINE_EDITING"
  - "VS.TOOLSOPTIONSPAGES.SQL_SERVER_TOOLS.TRANSACT-SQL_EDITOR.QUERY_EXECUTION.ADVANCED"
ms.assetid: fa9a250f-7feb-433e-91bd-a09779d74c8b
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Transact-SQL Editor Options
This topic contains information about some of the options of the Transact-SQL Editor. To set these options, navigate to the **Option** dialog through the **Tools\Options** menu.  
  
[Query Execution](#QueryExecution)  
  
[Query Results](#QueryResults)  
  
## <a name="QueryExecution"></a>Query Execution  
  
|Property|Description|  
|------------|---------------|  
|**SET ROWCOUNT**|The default value of 0 indicates that SQL Server will wait for results until all results are received. Provide a value greater than 0 if you want SQL Server to halt the query after obtaining the specified number of rows. To turn this option off (so that all rows are returned), specify SET ROWCOUNT 0.|  
|**SET TEXTSIZE**|The default value of 2,147,483,647 bytes indicates that SQL Server will provide a complete data field up to the limit of text, ntext, nvarchar(max), and varchar(max) data fields. It does not affect the XML data type. Provide a smaller number to limit results in the case of large values. Columns greater than the number provided will be truncated.|  
|**Execution time-out**|Indicates the number of seconds to wait before canceling the query. A value of 0 indicates an infinite wait, or no time-out.|  
|**By default, open new queries in SQLCMD Mode**|Select this check box to open new queries in SQLCMD mode. This check box is visible only when the dialog box is opened through the Tools menu.<br /><br />When you select this option, be aware of the following limitations:<br /><br />-   IntelliSense in the Database Engine Query Editor is turned off.<br />-   Because Query Editor does not run from the command line, you cannot pass in command-line parameters such as variables.<br />-   Because Query Editor cannot respond to operating-system prompts, you must be careful not to run interactive statements.|  
|**SET NOCOUNT**|Stops the message indicating the number of rows affected by a Transact-SQL statement from being returned as part of the results. For more information, see [SET NOCOUNT](https://go.microsoft.com/fwlink/?LinkID=238731).|  
|**SET NOEXEC**|When **ON**, tells Microsoft® SQL Server™ to compile each batch of Transact-SQL statements but not to execute them. When **OFF**, tells Microsoft® SQL Server™ to execute all batches after compilation.For more information, see [SET NOEXEC](https://go.microsoft.com/fwlink/?LinkId=238770).|  
|**SET PARSEONLY**|Checks the syntax of each Transact-SQL statement and returns any error messages without compiling or executing the statement. For more information, see [SET PARSEONLY](https://go.microsoft.com/fwlink/?LinkId=238734).|  
|**SET CONCAT_NULL_YIELDS_NULL**|Controls whether or not concatenation results are treated as null or empty string values.For more information, see [SET CONCAT_NULL_YIELDS_NULL](https://go.microsoft.com/fwlink/?LinkId=238733).|  
|**SET ARITHABORT**|Terminates a query when an overflow or divide-by-zero error occurs during query execution. For more information, see  [SET ARITHABORT](https://msdn.microsoft.com/library/aa259212(v=SQL.80).aspx).|  
|**SET SHOWPLAN_TEXT**|Causes Microsoft® SQL Server™ not to execute Transact-SQL statements. Instead, SQL Server returns detailed information about how the statements are executed. For more information, see [SET SHOWPLAN_TEXT](https://go.microsoft.com/fwlink/?LinkID=238737).|  
|**SET STATISTICS TIME**|Displays the number of milliseconds required to parse, compile, and execute each statement.|  
|**SET STATISTICS IO**|Causes Microsoft® SQL Server™ to display information regarding the amount of disk activity generated by Transact-SQL statements.|  
|**SET TRANSACTION ISOLATION LEVEL**|Controls the default transaction locking behavior for all Microsoft® SQL Server™ **SELECT** statements issued by a connection. For more information, see  [SET TRANSACTION ISOLATION LEVEL](https://go.microsoft.com/fwlink/?LinkId=238730).|  
|**SET LOCK_TIMEOUT**|Specifies the number of milliseconds that a statement waits for a lock to be released. For more information, see [SET LOCK_TIMEOUT](https://go.microsoft.com/fwlink/?LinkId=238747)|  
|**SET QUERY_GOVERNOR_COST_LIMIT**|Overrides the currently configured value for the current connection. For more information, see [SET QUERY_GOVERNOR_COST_LIMIT](https://go.microsoft.com/fwlink/?LinkId=238749).|  
|**SET ANSI_DEFAULTS**|Controls a group of Microsoft® SQL Server™ settings that collectively specify some SQL-92 standard behavior. For more information, see [SET ANSI_DEFAULTS](https://go.microsoft.com/fwlink/?LinkId=238750).|  
|**SET QUOTED_IDENTIFIER**|Causes Microsoft® SQL Server™ to follow the SQL-92 rules regarding quotation mark delimiting identifiers and literal strings. Identifiers delimited by double quotation marks either can be Transact-SQL reserved keywords or can contain characters not usually allowed by the Transact-SQL syntax rules for identifiers.For more information, see [SET QUOTED_IDENTIFIER](https://go.microsoft.com/fwlink/?LinkId=238751).|  
|**SET ANSI_NULL_DFLT_ON**|Alters the session's behavior to override default nullability of new columns when the ANSI null default option for the database is false. For more information, see [SET ANSI_NULL_DFLT_ON](https://go.microsoft.com/fwlink/?LinkID=238752).|  
|**SET IMPLICIT_TRANSACTIONS**|When **ON**, sets the connection into implicit transaction mode. When **OFF**, returns the connection to autocommit transaction mode. For more information, see [SET IMPLICIT_TRANSACTIONS](https://go.microsoft.com/fwlink/?LinkId=238753).|  
|**SET CURSOR_CLOSE_ON_COMMIT**|Controls whether or not a cursor is closed when a transaction is committed. For more information, see [SET CURSOR_CLOSE_ON_COMMIT](https://go.microsoft.com/fwlink/?LinkId=238754).|  
|**SET ANSI_PADDING**|Controls the way the column stores values shorter than the defined size of the column and the way the column stores values that have trailing blanks in **char**, **varchar**, **binary**, and **varbinary** data. For more information, see [SET ANSI_PADDING](https://go.microsoft.com/fwlink/?LinkId=238755).|  
|**SET ANSI_WARNINGS**|Specifies SQL-92 standard behavior for several error conditions.For more information, see [SET ANSI_WARNINGS](https://go.microsoft.com/fwlink/?LinkId=238758).|  
|**SET ANSI_NULLS**|Specifies SQL-92 compliant behavior for the Equals (**=**) and Not Equal to (**<>**) comparison operators when they are used with null values.For more information, see [SET ANSI_NULLS](https://go.microsoft.com/fwlink/?LinkId=238759).|  
  
## <a name="QueryResults"></a>Query Results  
  
|Property|Description|  
|------------|---------------|  
|**Include the query in the result set**|Returns the text of the query as part of the result set.|  
|**Include column headers when copying or saving the results**|Include column headers (titles) when results are copied to the clipboard, or saved in a file. Clear this check box if you do not want saved or copied result data to have only the data, and not the column headings.|  
|**Discard results after execution**|Free memory by discarding the query results after the screen display has received them.|  
|**Display results in a separate tab**|Display the result set in a new document window, instead of at the bottom of the query document window.|  
|**Switch to results tab after the query executes**|Automatically set the screen focus to the result set.|  
|**Maximum Characters Retrieved**|Non XML data:<br /><br />Enter a number from 1 through 65535 to specify the maximum number of characters that will be displayed in each cell. **Note:** Specifying a large number of characters may cause data in the result set to appear truncated. The maximum number of characters displayed in each cell is dependent on the font size. When large result sets are returned, a high value in this box can cause SQL Server Management Studio to run low on memory and hinder system performance.<br /><br />XML data:<br /><br />Select 1 MB, 2 MB, or 5 MB. Select Unlimited to retrieve all characters.|  
|**Output format**|By default the output is displayed in columns created by padding the results with spaces. Other options are using commas, tabs, or spaces to separate columns. Select the **Custom delimiter** check box to specify a different delimiting character in the **Custom delimiter** box.|  
|**Custom delimiter**|Specify the character of your choice to separate columns. This option is available only if the **Custom delimiter** check box is selected in the **Output format** box.|  
|**Include column headers in the result set**|Clear this check box if you do not want each column labeled with a column title.|  
|**Scroll as results are received**|Select this check box to keep the display focus on the most recently returned records at the bottom. Clear this check box to keep the display focus on the first rows received.|  
|**Right align numeric values**|Select this check box to align numeric values to the right of the column. This option can make it easier to review numbers with a fixed number of decimal places.|  
|**Discard result after query executes**|Frees memory by discarding the query results after the screen display has received them.|  
|**Display results in a separate tab**|Select this check box to display the result set in a new document window instead of at the bottom of the query document window.|  
|**Switch to results tab after the query executes**|Click to automatically set the screen focus to the result set.|  
|**Maximum number of characters displayed in each column**|This value defaults to 256. Increase this value to display larger result sets without truncation.|  
|**Reset to Default**|Resets all values on this page to the original default values.|  
  
