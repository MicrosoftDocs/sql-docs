---
title: "Query Options Execution (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.query.general.f1"
ms.assetid: 858a0263-2f04-4692-b8bf-63e93c998ead
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Query Options Execution (General Page)
  Use this page to specify the options for running [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] queries. To access this dialog box, right-click the body of a Query Editor window, and then click **Query Options**.  
  
## UIElement List  
 **SET ROWCOUNT**  
 The default value of 0 indicates that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will wait for results until all results are received. Provide a value greater than 0 if you want [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to halt the query after obtaining the specified number of rows. To turn this option off (so that all rows are returned), specify SET ROWCOUNT 0.  
  
 **SET TEXTSIZE**  
 The default value of 2,147,483,647 bytes indicates that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will provide a complete data field up to the limit of `text`, `ntext`, `nvarchar(max)`, and `varchar(max)` data fields. It does not affect the XML data type. Provide a smaller number to limit results in the case of large values. Columns greater than the number provided will be truncated.  
  
 **Execution time-out**  
 Indicates the number of seconds to wait before canceling the query. A value of 0 indicates an infinite wait, or no time-out.  
  
 **Batch separator**  
 Type a word that you use to separate Transact-SQL statements into batches. The default is GO.  
  
 **By default, open new queries in SQLCMD Mode**  
 Select this check box to open new queries in SQLCMD mode. This check box is visible only when the dialog box is opened through the **Tools** menu.  
  
 When you select this option, be aware of the following limitations:  
  
-   IntelliSense in the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor is turned off.  
  
-   Because Query Editor does not run from the command line, you cannot pass in command-line parameters such as variables.  
  
-   Because Query Editor cannot respond to operating-system prompts, you must be careful not to run interactive statements.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
