---
title: SSMS Options page - Query Execution
description: SSMS Options (Query Execution)
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 01/26/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "VS.ToolsOptionsPages.Query_Execution.Sql_Server.General"
dev_langs:
  - TSQL
---

# Options (Query Execution - General)

[!INCLUDE[SQL Server Azure SQL Database PDW](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

Use this page to specify the options for running Microsoft SQL Server queries. To access this dialog box, right-click the body of a Query Editor window, and then select **Query Options** or on the **Tools** menu,  select **Options**, then the **Query Execution** folder.

- **SET ROWCOUNT**
    The default value of 0 indicates that SQL Server will wait for results until all results are received. Provide a value greater than 0 if you want SQL Server to halt the query after obtaining the specified number of rows. To turn off this option (so that all rows are returned), specify SET ROWCOUNT 0.

- **SET TEXTSIZE**
    The default value of 2,147,483,647 bytes indicates that SQL Server will provide a complete data field up to the limit of text, ntext, nvarchar(max), and varchar(max) data fields. It doesn't affect the XML data type. Provide a smaller number to limit results of large values. Columns greater than the number provided will be truncated.

- **Execution time-out**
    Indicates the number of seconds to wait before canceling the query. A value of 0 indicates an infinite wait, or no time-out.

- **Batch separator**
    Type a word that you use to separate Transact-SQL statements into batches. The default is GO.

- **By default, open new queries in SQLCMD Mode**
    Select this check box to open new queries in SQLCMD mode. This check box is visible only when the dialog box is opened through the Tools menu.

    When you select this option, be aware of the following limitations:

  - IntelliSense in the Database Engine Query Editor is turned off.

  - Because Query Editor doesn't run from the command line, you can't pass in command-line parameters such as variables.

  - Because Query Editor can't respond to operating-system prompts, you must be careful not to run interactive statements.

- **Reset to Default**
    Resets all values on this page to the original default values.

- **Prompt to save unsaved T-SQL query windows on close**
    By default, a prompt appears asking if you want to save changes to file(s) with unsaved changes before closing them or exiting SSMS.  Unchecking this option could result in the loss of query editor information when closing query windows or SSMS.

- **Check for open transactions before closing T-SQL query windows**
    By default, SSMS will check to see if there are open transactions before closing a query editor window.  Unchecking this option bypasses this check, potentially resulting in open transactions not being committed.
