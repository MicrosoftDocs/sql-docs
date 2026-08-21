---
title: Bulk Import and Export of Data (SQL Server)
description: SQL Server supports exporting data in bulk from a SQL Server table and importing bulk data into a SQL Server table or nonpartitioned view.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/21/2026
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
ms.custom:
  - ignite-2025
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
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Bulk import and export of data (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports exporting data in bulk (*bulk data*) from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table and importing bulk data into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table or nonpartitioned view.

- *Bulk exporting* refers to copying data from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table to a data file.

- *Bulk importing* refers to loading data from a data file into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table. For example, you can export data from a [!INCLUDE [msCoName](../../includes/msconame-md.md)] Excel application to a data file and then bulk import that data into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table.

<a id="MethodsForBuliIE"></a>

## Methods for bulk importing and exporting data

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports bulk exporting data from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table and bulk importing data into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table or nonpartitioned view. The following basic methods are available.

| Method | Description | Imports data | Exports data |
| --- | --- | --- | --- |
| [bcp utility](import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md) | A command-line utility (`bcp.exe`) that bulk exports and bulk imports data and generates format files. | Yes | Yes |
| [BULK INSERT statement](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md) | A [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that imports data directly from a data file into a database table or nonpartitioned view. | Yes | No |
| [INSERT ... SELECT * FROM OPENROWSET(BULK...) statement](import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md) | A [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that uses the `OPENROWSET` bulk rowset provider to bulk import data into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table by specifying the OPENROWSET(BULK...) function to select data in an `INSERT` statement. | Yes | No |
| [SQL Server Import and Export Wizard](../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md) | The wizard creates basic packages that import and export data between many popular data formats including databases, spreadsheets, and text files. | Yes | Yes |

For rules about using a comma-separated value (CSV) file as the data file for a bulk import of data into SQL Server, see [Prepare data for bulk export or import](prepare-data-for-bulk-export-or-import-sql-server.md).

> [!NOTE]  
> Azure Synapse Analytics supports only the **`bcp`** utility to import and export delimited files.

<a id="FFs"></a>

## Format files

The [bcp utility](../../tools/bcp/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md), and [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md) all support the use of a specialized *format file* that stores format information for each field in a data file. A format file might also contain information about the corresponding [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table. Use the format file to provide all the format information required to bulk export data from and bulk import data to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> You can't use **`bcp`** to import data from or export data to Azure Blob Storage into Azure SQL Database. Use [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md) to import from or export to Azure Blob Storage.

Format files provide a flexible way to interpret data as it's in the data file during import, and also to format data in the data file during export. This flexibility removes the need to write special-purpose code to interpret the data or reformat it to the specific requirements of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or the external application. For example, if you're bulk exporting data to be loaded into an application that requires comma-separated values, use a format file to insert commas as field terminators in the exported data.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports two types of format files: XML format files and non-XML format files.

The [bcp utility](../../tools/bcp/bcp-utility.md) is the only tool that can generate a format file. For more information, see [Create a format file with bcp](create-a-format-file-sql-server.md). For more information about format files, see [Format files to import or export data](format-files-for-importing-or-exporting-data-sql-server.md).

> [!NOTE]  
> If you don't supply a format file during a bulk export or import operation, you can override the default formatting at the command line.

## Related articles

- [Prepare data for bulk export or import](prepare-data-for-bulk-export-or-import-sql-server.md)

- [Data formats for bulk import or bulk export](data-formats-for-bulk-import-or-bulk-export-sql-server.md)

  - [Use native format to import or export data](use-native-format-to-import-or-export-data-sql-server.md)
  - [Use character format to import or export data](use-character-format-to-import-or-export-data-sql-server.md)
  - [Use Unicode Native Format to Import or Export Data](use-unicode-native-format-to-import-or-export-data-sql-server.md)
  - [Use Unicode character format to import or export data](use-unicode-character-format-to-import-or-export-data-sql-server.md)
  - [Import native and character format data from earlier versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)

- [Specify compatibility data formats when using bcp](specify-data-formats-for-compatibility-when-using-bcp-sql-server.md)

  - [Specify file storage type using bcp](specify-file-storage-type-by-using-bcp-sql-server.md)
  - [Specify prefix length in data files using bcp](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)
  - [Specify field length by using bcp](specify-field-length-by-using-bcp-sql-server.md)
  - [Specify field and row terminators](specify-field-and-row-terminators-sql-server.md)

- [Keep nulls or default values during bulk import](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)

- [Keep identity values when bulk importing data](keep-identity-values-when-bulk-importing-data-sql-server.md)

- [Format files to import or export data](format-files-for-importing-or-exporting-data-sql-server.md)

  - [Create a format file with bcp](create-a-format-file-sql-server.md)
  - [Use a format file to bulk import data](use-a-format-file-to-bulk-import-data-sql-server.md)
  - [Use a format file to skip a table column](use-a-format-file-to-skip-a-table-column-sql-server.md)
  - [Use a format file to skip a data field](use-a-format-file-to-skip-a-data-field-sql-server.md)
  - [Use a format file to map table columns to data-file fields](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

## Related content

- [Prerequisites for minimal logging in bulk import](prerequisites-for-minimal-logging-in-bulk-import.md)
- [Examples of bulk import and export of XML documents (SQL Server)](examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)
- [Copy databases to other servers](../databases/copy-databases-to-other-servers.md)
- [Performing Bulk Load of XML Data (SQLXML 4.0)](../sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md)
- [Performing Bulk Copy Operations in SQL Server Native Client](../native-client/features/performing-bulk-copy-operations.md)
