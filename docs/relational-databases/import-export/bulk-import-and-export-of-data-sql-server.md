---
title: "Bulk Import and Export of Data (SQL Server)"
description: SQL Server supports exporting data in bulk from a SQL Server table and importing bulk data into a SQL Server table or nonpartitioned view.
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/25/2017"
ms.service: sql
ms.subservice: data-movement
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
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Bulk Import and Export of Data (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports exporting data in bulk (*bulk data*) from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and importing bulk data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or nonpartitioned view.

- *Bulk exporting* refers to copying data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table to a data file.
- *Bulk importing* refers to loading data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. For example, you can export data from a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel application to a data file and then bulk import that data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

## <a name="MethodsForBuliIE"></a> Methods for bulk importing and exporting data

 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports bulk exporting data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and for bulk importing data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or nonpartitioned view. The following basic methods are available.

|Method|Description|Imports data|Exports data|
|------------|-----------------|------------------|------------------|
|[bcp utility](../../relational-databases/import-export/import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)|A command-line utility (Bcp.exe) that bulk exports and bulk imports data and generates format files.|Yes|Yes|
|[BULK INSERT statement](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)|A [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that imports data directly from a data file into a database table or nonpartitioned view.|Yes|No|
|[INSERT ... SELECT * FROM OPENROWSET(BULK...) statement](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)|A [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that uses the OPENROWSET bulk rowset provider to bulk import data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table by specifying the OPENROWSET(BULK...) function to select data in an INSERT statement.|Yes|No|
|[SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md)|The wizard creates simple packages that import and export data between many popular data formats including databases, spreadsheets, and text files.|Yes|Yes|

> [!IMPORTANT]
> For rules about using a comma-separated value (CSV) file as the data file for a bulk import of data into SQL Server, see [Prepare Data for Bulk Export or Import (SQL Server)](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).

> [!NOTE]
> Only the bcp utility is supported by Azure Synapse Analytics for importing and exporting delimited files.

## <a name="FFs"></a> Format files

The [bcp utility](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md), and [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) all support the use of a specialized *format file* that stores format information for each field in a data file. A format file might also contain information about the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. The format file can be used to provide all the format information that is required to bulk export data from and bulk import data to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]
> You cannot use BCP to import data from or export data to Azure Blob Storage into Azure SQL Database. Use [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) to import from or export to Azure Blob Storage.

Format files provide a flexible way to interpret data as it is in the data file during import, and also to format data in the data file during export. This flexibility eliminates the need to write special-purpose code to interpret the data or reformat the data to the specific requirements of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the external application. For example, if you are bulk exporting data to be loaded into an application that requires comma-separated values, you can use a format file to insert commas as field terminators in the exported data.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports two kinds of format files: XML format files and non-XML format files.

The [bcp utility](../../tools/bcp-utility.md) is the only tool that can generate a format file. For more information, see [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md). For more information about format files, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).

> [!NOTE]
> In cases when a format file is not supplied during a bulk export or import operations, you can override the default formatting at the command line.

|Related Topics|
|---|
|[Prepare Data for Bulk Export or Import (SQL Server)](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)|
|[Data Formats for Bulk Import or Bulk Export (SQL Server)](../../relational-databases/import-export/data-formats-for-bulk-import-or-bulk-export-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Native Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Character Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Unicode Native Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use Unicode Character Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)|
|[Specify Data Formats for Compatibility when Using bcp (SQL Server)](../../relational-databases/import-export/specify-data-formats-for-compatibility-when-using-bcp-sql-server.md)<br />&emsp;&#9679;&emsp;[Specify File Storage Type by Using bcp (SQL Server)](../../relational-databases/import-export/specify-file-storage-type-by-using-bcp-sql-server.md)<br />&emsp;&#9679;&emsp;[Specify Prefix Length in Data Files by Using bcp (SQL Server)](../../relational-databases/import-export/specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)<br />&emsp;&#9679;&emsp;[Specify Field Length by Using bcp (SQL Server)](../../relational-databases/import-export/specify-field-length-by-using-bcp-sql-server.md)<br />&emsp;&#9679;&emsp;[Specify Field and Row Terminators (SQL Server)](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)|
|[Keep Nulls or Use Default Values During Bulk Import (SQL Server)](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)|
|[Keep Identity Values When Bulk Importing Data (SQL Server)](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)|
|[Format Files for Importing or Exporting Data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Bulk Import Data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Skip a Table Column (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Skip a Data Field (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)<br />&emsp;&#9679;&emsp;[Use a Format File to Map Table Columns to Data-File Fields (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)|

## More information

- [Prerequisites for Minimal Logging in Bulk Import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md)
- [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)
- [Copy Databases to Other Servers](../../relational-databases/databases/copy-databases-to-other-servers.md)
- [Performing Bulk Load of XML Data &#40;SQLXML 4.0&#41;](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md)
- [Performing Bulk Copy Operations](../../relational-databases/native-client/features/performing-bulk-copy-operations.md)
