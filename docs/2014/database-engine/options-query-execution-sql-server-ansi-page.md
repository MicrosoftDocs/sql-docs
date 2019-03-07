---
title: "Options (Query Execution-SQL Server-ANSI Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.QueryExecution.SqlServer.SqlExecutionAnsi"
ms.assetid: 0f4c6887-0562-417e-806c-b5cffb1e7c5c
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Query Execution-SQL Server-ANSI Page)
  Together, these ANSI (ISO) standard SET options define the query processing environment for the duration of the user's query, a running trigger, or a stored procedure. These SET options, however, do not include all of the options required to conform to the ISO standard. Use this page to specify that [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will run the queries using all or a portion of the settings specified in the ISO standard. Changes to these options are applied only to new [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] queries. To change the options for the current queries, click **Query Options** on the **Query** menu, or right-click in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Query window and select **Query Options**. In the **Query Options** dialog box, under **Execution**, click **ANSI**.  
  
## UIElement List  
 **SET ANSI_DEFAULTS**  
 Select this check box to select all of the default ISO settings. Not all ISO options are selected by default.  
  
 **SET QUOTED_IDENTIFIER**  
 When this check box is selected, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] follows the ISO rules regarding quotation mark delimiting identifiers and literal strings. Identifiers delimited by quotation marks can be either Transact-SQL reserved keywords or can contain characters not usually allowed by the Transact-SQL syntax rules for identifiers. This check box is selected by default.  
  
 **SET ANSI_NULL_DFLT_ON**  
 When this value is set, all user-defined data types or columns that are not explicitly defined as NOT NULL during a CREATE TABLE or ALTER TABLE statement default to allowing null values. This check box is selected by default.  
  
 **SET IMPLICIT_TRANSACTIONS**  
 When this check box is selected, SET IMPLICIT_TRANSACTIONS sets the connection into implicit transaction mode. When this check box is cleared, it returns the connection to autocommit transaction mode. To review the statements that start an implicit transaction when selected, see [SET IMPLICIT_TRANSACTIONS &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-implicit-transactions-transact-sql). This check box is cleared by default.  
  
 **SET CURSOR_CLOSE_ON_COMMIT**  
 When this check box is selected, any open cursors are closed automatically (in compliance with ISO) when a transaction is committed. When this value is set to OFF, cursors remain open across transaction boundaries, closing only when the connection is closed or when they are explicitly closed. This check box is cleared by default.  
  
 **SET ANSI_PADDING**  
 Controls the way the column stores value names shorter than the defined size of the column, and the way the column stores values that have trailing blanks in **char**, **varchar**, **binary**, and **varbinary** data. This setting affects only the definition of new columns. After the column is created, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] stores the values based on the setting when the column was created. Existing columns are not affected by a later change to this setting. This check box is selected by default.  
  
 **SET ANSI_WARNINGS**  
 Specifies ISO standard behavior for several error conditions:  
  
-   When this check box is selected, if null values appear in aggregate functions (such as SUM, AVG, MAX, MIN, STDEV, STDEVP, VAR, VARP, or COUNT), a warning message is generated. When OFF, no warning is issued.  
  
-   When this check box is cleared, divide-by-zero and arithmetic overflow errors cause the statement to be rolled back and an error message to be generated. When OFF, divide-by-zero and arithmetic overflow errors cause null values to be returned. The behavior in which a divide-by-zero or arithmetic overflow error causes null values to be returned occurs if an INSERT or UPDATE operation is attempted on a **character**, **Unicode**, or **binary** column in which the length of a new value exceeds the maximum size of the column. If SET ANSI_WARNINGS is ON, the INSERT or UPDATE operation is canceled as specified by the ISO standard. Trailing blanks are ignored for character columns and trailing nulls are ignored for binary columns. When OFF, data is truncated to the size of the column and the statement succeeds.  
  
 This check box is selected by default.  
  
 **SET ANSI_NULLS**  
 -   Specifies ISO compliant behavior of the equals (=) and not equal to (<>) comparison operators when used with null values. When SET ANSI_NULLS is selected, all comparisons against a null value evaluate to UNKNOWN, the ISO compliant behavior. When SET ANSI_NULLS is not selected, comparisons of all data against a null value evaluate to TRUE. This check box is selected by default.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
