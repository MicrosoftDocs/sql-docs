---
title: Import & Export Bulk Data with bcp
description: Use bcp to export data from anywhere in a SQL Server database that SELECT works. Bulk export data from a table or from a query and bulk import from a file.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 08/21/2026
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
helpviewer_keywords:
  - "bulk exporting [SQL Server], bcp utility"
  - "bulk importing [SQL Server], bcp utility"
  - "bcp utility [SQL Server], about bcp utility"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Import and export bulk data using bcp (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article provides an overview of using the [bcp utility](../../tools/bcp/bcp-utility.md) to export data from anywhere in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database where a `SELECT` statement works, including partitioned views.

The **`bcp`** utility (`bcp.exe`) is a command-line tool that uses the Bulk Copy Program (BCP) API. The **`bcp`** utility performs the following tasks:

- Bulk exports data from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table into a data file.

- Bulk exports data from a query.

- Bulk imports data from a data file into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table.

- Generates format files.

To use **`bcp`** to bulk import data, you must understand the schema of the table and the data types of its columns, unless you're using a pre-existing format file.

The **`bcp`** utility can export data from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table to a data file for use in other programs. The utility can also import data into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table from another program, usually another database management system (DBMS). The data is first exported from the source program to a data file and then, in a separate operation, copied from the data file into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table.

The **`bcp`** utility provides switches that you use to specify the data type of the data file and other information. If you don't specify these switches, **`bcp`** prompts for formatting information, such as the type of data fields in a data file. It then asks whether you want to create a format file that contains your interactive responses. If you want flexibility for future bulk-import or bulk-export operations, a format file is often useful. You can specify the format file on later **`bcp`** runs for equivalent data files. For more information, see [Specify compatibility data formats when using bcp](specify-data-formats-for-compatibility-when-using-bcp-sql-server.md).

> [!NOTE]  
> The **`bcp`** utility is written by using the ODBC bulk-copy.

For a description of the **`bcp`** syntax, see [bcp Utility](../../tools/bcp/bcp-utility.md).

## Examples

- [bcp utility](../../tools/bcp/bcp-utility.md)

- Data formats for bulk import or bulk export:

  - [Use native format to import or export data](use-native-format-to-import-or-export-data-sql-server.md)
  - [Use character format to import or export data](use-character-format-to-import-or-export-data-sql-server.md)
  - [Use Unicode Native Format to Import or Export Data](use-unicode-native-format-to-import-or-export-data-sql-server.md)
  - [Use Unicode character format to import or export data](use-unicode-character-format-to-import-or-export-data-sql-server.md)

- [Specify field and row terminators](specify-field-and-row-terminators-sql-server.md)

- [Keep nulls or default values during bulk import](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)

- [Keep identity values when bulk importing data](keep-identity-values-when-bulk-importing-data-sql-server.md)

- Format files for importing or exporting data (SQL Server)

  - [Create a format file with bcp](create-a-format-file-sql-server.md)
  - [Use a format file to bulk import data](use-a-format-file-to-bulk-import-data-sql-server.md)
  - [Use a format file to skip a table column](use-a-format-file-to-skip-a-table-column-sql-server.md)
  - [Use a format file to skip a data field](use-a-format-file-to-skip-a-data-field-sql-server.md)
  - [Use a format file to map table columns to data-file fields](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

- [Examples of bulk import and export of XML documents](examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)

## Related content

- [INSERT (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md)
- [SELECT clause (Transact-SQL)](../../t-sql/queries/select-clause-transact-sql.md)
- [bcp utility](../../tools/bcp/bcp-utility.md)
- [Prepare to Bulk Import Data (SQL Server)](prepare-to-bulk-import-data-sql-server.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [Bulk import and export of data (SQL Server)](bulk-import-and-export-of-data-sql-server.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [Create a format file with bcp (SQL Server)](create-a-format-file-sql-server.md)
