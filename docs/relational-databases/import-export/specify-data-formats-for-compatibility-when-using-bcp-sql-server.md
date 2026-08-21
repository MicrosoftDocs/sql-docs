---
title: Specify Compatibility Data Formats with bcp
description: For bulk exports in SQL Server with bcp, data formats might be incompatible with expected layout. A non-xml format file specifies compatibility data formats.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 08/21/2026
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
helpviewer_keywords:
  - "bulk exporting [SQL Server], compatibility"
  - "bulk importing [SQL Server], compatibility"
  - "compatibility [SQL Server], data formats"
  - "data formats [SQL Server], compatibility"
  - "bcp utility [SQL Server], compatibility"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Specify compatibility data formats when using bcp (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

This article describes the data-format attributes, field-specific prompts, and how to store field-by-field data in a non-XML format file of the **`bcp`** utility. This information can help when you bulk export [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data for bulk import into another program, such as another database program. The default data formats (native, character, or Unicode) in the source table might be incompatible with the data layout that the other program expects. If an incompatibility exists, you must describe the data layout when you export the data.

> [!NOTE]  
> For more information about data formats for importing or exporting data, see [Data formats for bulk import or bulk export](data-formats-for-bulk-import-or-bulk-export-sql-server.md).

<a id="bcpDataFormatAttr"></a>

## bcp data-format attributes

Use **`bcp`** to specify the structure of each field in a data file through the following data-format attributes:

- File storage type

  The *file storage type* describes how data is stored in the data file. You can export data to a data file as its database table type (native format), in its character representation (character format), or as any data type that supports implicit conversion. For example, you can copy a **smallint** as an **int**. User-defined data types are exported as their base types. For more information, see [Specify file storage type using bcp](specify-file-storage-type-by-using-bcp-sql-server.md).

- Prefix length

  To provide the most compact file storage for the bulk export of data in native format to a data file, **`bcp`** precedes each field with one or more characters that indicate the length of the field. These characters are called *length prefix characters*. For more information, see [Specify prefix length in data files using bcp](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md).

- Field length

  The field length indicates the maximum number of characters that are required to represent data in character format. The field length is already known if the data is stored in the native format. For more information, see [Specify field length by using bcp](specify-field-length-by-using-bcp-sql-server.md).

- Field terminator

  For character data fields, optional terminating characters let you mark the end of each field in a data file (using a *field terminator*) and the end of each row (using a *row terminator*). Terminating characters are one way to indicate to programs reading the data file where one field or row ends and another begins. For more information, see [Specify field and row terminators](specify-field-and-row-terminators-sql-server.md).

<a id="FieldSpecificPrompts"></a>

## Overview of the field-specific prompts

If you run **`bcp`** interactively with the `in` or `out` option, but don't include either the format file switch (`-f`) or a data-format switch (`-n`, `-c`, `-w`, or `-N`) for each column in the source or target table, **`bcp`** prompts for each of the preceding attributes, in turn. In each prompt, **`bcp`** provides a default value based on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type of the table column. Accepting the default value for all of the prompts produces the same result as specifying native format (`-n`) on the command line. Each prompt displays a default value in brackets: [*default*]. Press ENTER to accept the displayed default. To specify a value other than the default, enter the new value at the prompt.

### Example

The following example uses **`bcp`** to bulk export data from the `HumanResources.myTeam` table interactively to the `myTeam.txt` file. Before you can run the example, you must create this table. For information about the table and how to create it, see [HumanResources.myTeam Sample Table](humanresources-myteam-sample-table-sql-server.md).

The command specifies neither a format file nor a data type, so **`bcp`** prompts for data-format information. At the Windows command prompt, enter:

```cmd
bcp AdventureWorks.HumanResources.myTeam out myTeam.txt -T
```

For each column, **`bcp`** prompts for field-specific values. The following example shows the field-specific prompts for the `EmployeeID` and `Name` columns of the table, and suggests the default file storage type (the native format) for each column. The prefix lengths of the `EmployeeID` and `Name` columns are 0 and 2, respectively. The user specifies a comma (`,`) as the terminator of each field.

```output
Enter the file storage type of field EmployeeID [smallint]:

Enter prefix-length of field EmployeeID [0]:

Enter field terminator [none]:,

Enter the file storage type of field Name [nvarchar]:

Enter prefix length of field Name [2]:

Enter field terminator [none]:,

.

.

.
```

Equivalent prompts appear as needed for each of the table columns in order.

<a id="FieldByFieldNonXmlFF"></a>

## Store field-by-field data in a non-XML format file

After you specify all of the table columns, **`bcp`** prompts you to optionally generate a non-XML format file that stores the field-by-field information you just supplied (see the preceding example). If you choose to generate a format file, you can use it whenever you export data out of that table or import like-structured data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> You can use the format file to bulk import data from the data file into an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or to bulk export data from the table, without needing to respecify the format. For more information, see [Format files to import or export data](format-files-for-importing-or-exporting-data-sql-server.md).

The following example creates a non-XML format file named `myFormatFile.fmt`:

```output
Do you want to save this format information in a file? [Y/n] y

Host filename: [bcp.fmt]myFormatFile.fmt
```

The default name for the format file is `bcp.fmt`, but you can specify a different file name if you choose.

> [!NOTE]  
> For a data file that uses a single data format for its file-storage type, such as character or native format, you can quickly create a format file without exporting or importing data by using the `format` option. This approach is easy and lets you create either an XML format file or a non-XML format file. For more information, see [Create a format file with bcp](create-a-format-file-sql-server.md).

## Related tasks

- [Specify file storage type using bcp](specify-file-storage-type-by-using-bcp-sql-server.md)
- [Specify prefix length in data files using bcp](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)
- [Specify field length by using bcp](specify-field-length-by-using-bcp-sql-server.md)
- [Specify field and row terminators](specify-field-and-row-terminators-sql-server.md)

## Related content

- [Bulk import and export of data (SQL Server)](bulk-import-and-export-of-data-sql-server.md)
- [Data formats for bulk import or bulk export (SQL Server)](data-formats-for-bulk-import-or-bulk-export-sql-server.md)
- [bcp Utility](../../tools/bcp/bcp-utility.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
