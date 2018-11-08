---
title: "Import and Export Bulk Data by Using the bcp Utility (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bulk exporting [SQL Server], bcp utility"
  - "bulk importing [SQL Server], bcp utility"
  - "bcp utility [SQL Server], about bcp utility"
ms.assetid: 73e949de-67a3-4c84-9735-7da1ad4ba34a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Import and Export Bulk Data by Using the bcp Utility (SQL Server)
  This topic provides an overview for using the [bcp utility](../../tools/bcp-utility.md) to export data from anywhere in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database where a SELECT statement works, including partitioned views.  
  
 The bcp utility (Bcp.exe) is a command-line tool that uses the Bulk Copy Program (BCP) API. The bcp utility performs the following tasks:  
  
-   Bulk exports data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table into a data file.  
  
-   Bulk exports data from a query.  
  
-   Bulk imports data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
-   Generates format files.  
  
 The bcp utility is accessed by the **bcp** command. To use the **bcp** command to bulk import data, you must understand the schema of the table and the data types of its columns, unless you are using a pre-existing format file.  
  
 The bcp utility can export data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table to a data file for use in other programs. The utility can also import data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table from another program, usually another database management system (DBMS). The data is first exported from the source program to a data file and then, in a separate operation, copied from the data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 The **bcp** command provides switches that you use to specify the data type of the data file and other information. If these switches are not specified, the command prompts for formatting information, such as the type of data fields in a data file. The command then asks whether you want to create a format file that contains your interactive responses. If you want flexibility for future bulk-import or bulk-export operations, a format file is often useful. You can specify the format file on later **bcp** commands for equivalent data files. For more information, see [Specify Data Formats for Compatibility when Using bcp &#40;SQL Server&#41;](specify-data-formats-for-compatibility-when-using-bcp-sql-server.md).  
  
> [!NOTE]  
>  The bcp utility is written by using the ODBC bulk-copy  
  
 For a description of the **bcp** command syntax, see [bcp Utility](../../tools/bcp-utility.md).  
  
## Examples  
 For **bcp** examples, see:  
  
-   [bcp Utility](../../tools/bcp-utility.md)  
  
-   [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md)  
  
-   [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)  
  
-   [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](keep-identity-values-when-bulk-importing-data-sql-server.md)  
  
-   [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  
  
-   [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md)  
  
-   [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](use-a-format-file-to-bulk-import-data-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/insert-transact-sql)   
 [SELECT Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-clause-transact-sql)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [Prepare to Bulk Import Data &#40;SQL Server&#41;](prepare-to-bulk-import-data-sql-server.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [Bulk Import and Export of Data &#40;SQL Server&#41;](bulk-import-and-export-of-data-sql-server.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md)  
  
  
