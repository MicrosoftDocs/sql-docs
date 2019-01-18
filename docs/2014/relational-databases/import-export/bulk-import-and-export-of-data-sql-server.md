---
title: "Bulk Import and Export of Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "exporting data"
  - "bulk importing [SQL Server], about bulk importing"
  - "data [SQL Server], bulk export and import"
  - "transferring data"
  - "importing data, (See also bulk importing [SQL Server])"
  - "copying data [SQL Server]"
  - "bulk exporting [SQL Server]"
  - "importing data, bulk import"
  - "copying data [SQL Server], bulk export and import"
  - "exporting data,(See also bulk exporting [SQL Server])"
  - "bulk exporting [SQL Server], about bulk exporting"
  - "bulk importing [SQL Server]"
  - "importing data"
ms.assetid: 19049021-c048-44a2-b38d-186d9f9e4a65
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Bulk Import and Export of Data (SQL Server)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports exporting data in bulk (*bulk data*) from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and importing bulk data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or nonpartitioned view. Bulk importing and bulk exporting are essential to efficient transfer data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and heterogeneous data sources. *Bulk exporting* refers to copying data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table to a data file. *Bulk importing* refers to loading data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. For example, you can export data from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel application to a data file and then bulk import that data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 **In this Topic:**  
  
-   [Introduction to Bulk Import and Bulk Export Operations](#Intro)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Intro"></a> Bulk Import and Bulk Export Overview  
 This section lists and briefly compares the various methods that are available for bulk importing and exporting data. The section also introduces format files.  
  
 **In This Topic:**  
  
-   [Methods for Bulk Importing and Exporting Data](#MethodsForBuliIE)  
  
-   [Format Files](#FFs)  
  
###  <a name="MethodsForBuliIE"></a> Methods for Bulk Importing and Exporting Data  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports bulk exporting data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and for bulk importing data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or nonpartitioned view. The following basic methods are available.  
  
|Method|Description|Imports data|Exports data|  
|------------|-----------------|------------------|------------------|  
|[bcp utility](import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)|A command-line utility (Bcp.exe) that bulk exports and bulk imports data and generates format files.|Yes|Yes|  
|[BULK INSERT statement](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)|A [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that imports data directly from a data file into a database table or nonpartitioned view.|Yes|No|  
|[INSERT ... SELECT * FROM OPENROWSET(BULK...) statement](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)|A [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that uses the OPENROWSET bulk rowset provider to bulk import data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table by specifying the OPENROWSET(BULK...) function to select data in an INSERT statement.|Yes|No|  
  
> [!IMPORTANT]  
>  Comma-separated value (CSV) files are not supported by SQL Server bulk-import operations. However, in some cases, a CSV file can be used as the data file for a bulk import of data into SQL Server. Note that the field terminator of a CSV file does not have to be a comma. For more information, see [Prepare Data for Bulk Export or Import &#40;SQL Server&#41;](prepare-data-for-bulk-export-or-import-sql-server.md).  
  
###  <a name="FFs"></a> Format Files  
 The **bcp** utility, BULK INSERT, and INSERT ... SELECT \* FROM OPENROWSET(BULK...) all support the use of a specialized *format file* that stores format information for each field in a data file. A format file might also contain information about the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. The format file can be used to provide all the format information that is required to bulk export data from and bulk import data to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Format files provide a flexible way to interpret data as it is in the data file during import, and also to format data in the data file during export. This flexibility eliminates the need to write special-purpose code to interpret the data or reformat the data to the specific requirements of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the external application. For example, if you are bulk exporting data to be loaded into an application that requires comma-separated values, you can use a format file to insert commas as field terminators in the exported data.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports two kinds of format files: XML format files and non-XML format files.  
  
 The **bcp** utility is the only tool that can generate a format file. For more information, see [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md). For more information about format files, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md).  
  
> [!NOTE]  
>  In cases when a format file is not supplied during a bulk export or import operations, you can override the default formatting at the command line.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Import and Export Bulk Data by Using the bcp Utility &#40;SQL Server&#41;](import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)  
  
-   [Import Bulk Data by Using BULK INSERT or OPENROWSET&#40;BULK...&#41; &#40;SQL Server&#41;](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)  
  
-   [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](keep-identity-values-when-bulk-importing-data-sql-server.md)  
  
-   [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  
  
-   [Prepare Data for Bulk Export or Import &#40;SQL Server&#41;](prepare-data-for-bulk-export-or-import-sql-server.md)  
  
 **To use a format file**  
  
-   [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md)  
  
-   [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](use-a-format-file-to-bulk-import-data-sql-server.md)  
  
-   [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
-   [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
-   [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](use-a-format-file-to-skip-a-table-column-sql-server.md)  
  
 **To use data formats for bulk import or bulk export**  
  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
 **To specify data formats for compatibility when using bcp**  
  
1.  [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md)  
  
2.  [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  
  
3.  [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](specify-file-storage-type-by-using-bcp-sql-server.md)  
  
## See Also  
 [Prerequisites for Minimal Logging in Bulk Import](prerequisites-for-minimal-logging-in-bulk-import.md)   
 [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md)   
 [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)   
 [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)   
 [Copy Databases to Other Servers](../databases/copy-databases-to-other-servers.md)   
 [Performing Bulk Load of XML Data &#40;SQLXML 4.0&#41;](../sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md)   
 [Performing Bulk Copy Operations](../native-client/features/performing-bulk-copy-operations.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)  
  
  
