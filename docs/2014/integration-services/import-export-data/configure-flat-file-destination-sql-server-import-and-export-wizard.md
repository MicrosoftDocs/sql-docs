---
title: "Configure Flat File Destination (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.configureflatfiledest.f1"
ms.assetid: 318e8da0-37d3-46cd-943a-fc5d66aad93a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Configure Flat File Destination (SQL Server Import and Export Wizard)
  Use the **Configure Flat File Destination** page to specify formatting options for the destination flat file, and to preview results before continuing.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, as well as the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **Source flat file**  
 The name of the destination file.  
  
 **Row delimiter**  
 Select from the list of delimiters for rows.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|The row is delimited by a carriage return-line feed combination.|  
|**{CR}**|The row is delimited by a carriage return.|  
|**{LF}**|The row is delimited by a line feed.|  
|**Semicolon {;}**|The row is delimited by a semicolon.|  
|**Colon {:}**|The row is delimited by a colon.|  
|**Comma {,}**|The row is delimited by a comma.|  
|**Tab {t}**|The row is delimited by a tab.|  
|**Vertical bar {&#124;}**|The row is delimited by a vertical bar.|  
  
 **Column delimiter**  
 Select from the list of delimiters for columns.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|The columns are delimited by a carriage return-line feed combination.|  
|**{CR}**|The columns are delimited by a carriage return.|  
|**{LF}**|The columns are delimited by a line feed.|  
|**Semicolon {;}**|The columns are delimited by a semicolon.|  
|**Colon {:}**|The columns are delimited by a colon.|  
|**Comma {,}**|The columns are delimited by a comma.|  
|**Tab {t}**|The columns are delimited by a tab.|  
|**Vertical bar {&#124;}**|The columns are delimited by a vertical bar.|  
  
 **Preview**  
 View in the **Preview Data** dialog box the results of the selected formatting options for the destination flat file.  
  
 **Edit transform**  
 Delete rows, append rows, and configure columns in the destination file by using the **Column Mappings** dialog box.  
  
  
