---
title: "Import and Export Bulk Data by Using the bcp Utility (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/28/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bulk exporting [SQL Server], bcp utility"
  - "bulk importing [SQL Server], bcp utility"
  - "bcp utility [SQL Server], about bcp utility"
ms.assetid: 73e949de-67a3-4c84-9735-7da1ad4ba34a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Import and Export Bulk Data by Using the bcp Utility (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  This topic provides an overview for using the [bcp utility](../../tools/bcp-utility.md) to export data from anywhere in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database where a SELECT statement works, including partitioned views.  
  
 The bcp utility (Bcp.exe) is a command-line tool that uses the Bulk Copy Program (BCP) API. The bcp utility performs the following tasks:  
  
-   Bulk exports data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table into a data file.  
  
-   Bulk exports data from a query.  
  
-   Bulk imports data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
-   Generates format files.  
  
 The bcp utility is accessed by the **bcp** command. To use the **bcp** command to bulk import data, you must understand the schema of the table and the data types of its columns, unless you are using a pre-existing format file.  
  
 The bcp utility can export data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table to a data file for use in other programs. The utility can also import data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table from another program, usually another database management system (DBMS). The data is first exported from the source program to a data file and then, in a separate operation, copied from the data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 The **bcp** command provides switches that you use to specify the data type of the data file and other information. If these switches are not specified, the command prompts for formatting information, such as the type of data fields in a data file. The command then asks whether you want to create a format file that contains your interactive responses. If you want flexibility for future bulk-import or bulk-export operations, a format file is often useful. You can specify the format file on later **bcp** commands for equivalent data files. For more information, see [Specify Data Formats for Compatibility when Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-data-formats-for-compatibility-when-using-bcp-sql-server.md).  
  
>**Note!!** The bcp utility is written by using the ODBC bulk-copy.
  
 For a description of the **bcp** command syntax, see [bcp Utility](../../tools/bcp-utility.md).  
  
## Examples  
|The following topics contain examples of using bcp: |
|---|
|[bcp Utility](../../tools/bcp-utility.md)<br /><br />Data Formats for Bulk Import or Bulk Export (SQL Server)<br />&emsp;&#9679;&emsp;[Use Native Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Character Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Unicode Native Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Unicode Character Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)<br /><br />[Specify Field and Row Terminators (SQL Server)](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)<br /><br />[Keep Nulls or Use Default Values During Bulk Import (SQL Server)](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)<br /><br />[Keep Identity Values When Bulk Importing Data (SQL Server)](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)<br /><br />Format Files for Importing or Exporting Data (SQL Server))<br />&emsp;&#9679;&emsp;[Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Bulk Import Data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Skip a Table Column (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Skip a Data Field (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Map Table Columns to Data-File Fields (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)<br /><br />[Examples of Bulk Import and Export of XML Documents (SQL Server)](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)<br /><p>                                                                                                                                                                                                                  </p>|

  
## More examples and information  
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [SELECT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-clause-transact-sql.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [Prepare to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/prepare-to-bulk-import-data-sql-server.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md)  
  
  
