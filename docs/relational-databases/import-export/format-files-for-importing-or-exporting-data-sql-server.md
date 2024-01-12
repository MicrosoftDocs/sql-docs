---
title: "Format files to import and export data"
description: When you bulk import to a SQL Server table or bulk export from a table, a format file can store field format information for a data file relative to a table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/20/2023
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
helpviewer_keywords:
  - "bulk exporting [SQL Server], format files"
  - "bulk importing [SQL Server], format files"
  - "format files [SQL Server]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Format files to import or export data (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database PDW](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

When you bulk import data into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table or bulk export data from a table, you can use a *format file* to store all the format information that is required to bulk export or bulk import data. This includes format information for each field in a data file relative to that table.

[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] supports two types of format files: XML formats and non-XML format files. Both non-XML format files and XML format files contain descriptions of every field in a data file, and XML format files also contain descriptions of the corresponding table columns. Generally, XML and non-XML format files are interchangeable. However, we recommend that you use the XML syntax for new format files because they provide several advantages over non-XML format files. For more information, see [XML Format Files (SQL Server)](xml-format-files-sql-server.md).

> [!NOTE]  
> This syntax, including bulk insert, isn't supported in Azure Synapse Analytics. [!INCLUDE [Use ADF or PolyBase instead of Synapse Bulk Insert](includes/bulk-insert-synapse.md)]

## Benefits of format files

Format files provide a flexible system for writing data files that requires little or no editing to comply with other data formats or to read data files from other software.

You can bulk import data without having to add or delete unnecessary data or to reorder existing data in the data file. Format files can be useful when a mismatch exists between fields in the data file and columns in the table.

## Examples of format files

The following examples show the layout of a non-XML format file and of an XML format file. These format files correspond to the `HumanResources.myTeam` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. This table contains four columns: `EmployeeID`, `Name`, `Title`, and `ModifiedDate`.

> [!NOTE]  
> For information about this table and how to create it, see [HumanResources.myTeam sample table (SQL Server)](humanresources-myteam-sample-table-sql-server.md).

### A. Use a non-XML format file

The following non-XML format file uses the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] native data format for the `HumanResources.myTeam` table. This format file was created by using the following `bcp` command.

```cmd
bcp AdventureWorks2022.HumanResources.myTeam format nul -f myTeam.Fmt -n -T
```

The `bcp` command defaults to a local, default instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with Windows Authentication. You can specify other instance and login information as desired, for more information, see [bcp Utility](../../tools/bcp-utility.md). For example, to specify a remote server named instance with Windows Authentication, use:

```cmd
bcp AdventureWorks2022.HumanResources.myTeam format nul -f myTeam.Fmt -n -T -S servername/instancename
```

The contents of this format file are as follows, starting with the major version number of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], and the table metadata information.

```output
14.0
4
1       SQLSMALLINT   0       2       ""   1     EmployeeID               ""
2       SQLNCHAR      2       100     ""   2     Name                     SQL_Latin1_General_CP1_CI_AS
3       SQLNCHAR      2       100     ""   3     Title                    SQL_Latin1_General_CP1_CI_AS
4       SQLNCHAR      2       100     ""   4     Background               SQL_Latin1_General_CP1_CI_AS
```

For more information, see [Use Non-XML format files (SQL Server)](non-xml-format-files-sql-server.md).

### B. Use an XML format file

The following XML format file uses the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] native data format for the `HumanResources.myTeam` table. This format file was created by using the following `bcp` command.

```cmd
bcp AdventureWorks2022.HumanResources.myTeam format nul -f myTeam.xml -x -n -T
```

The format file contains:

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="http://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<RECORD>
  <FIELD ID="1" xsi:type="NativePrefix" LENGTH="1"/>
  <FIELD ID="2" xsi:type="NCharPrefix" PREFIX_LENGTH="2" MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="3" xsi:type="NCharPrefix" PREFIX_LENGTH="2" MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="4" xsi:type="NCharPrefix" PREFIX_LENGTH="2" MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
</RECORD>
<ROW>
  <COLUMN SOURCE="1" NAME="EmployeeID" xsi:type="SQLSMALLINT"/>
  <COLUMN SOURCE="2" NAME="Name" xsi:type="SQLNVARCHAR"/>
  <COLUMN SOURCE="3" NAME="Title" xsi:type="SQLNVARCHAR"/>
  <COLUMN SOURCE="4" NAME="Background" xsi:type="SQLNVARCHAR"/>
</ROW>
</BCPFORMAT>
```

For more information, see [XML Format Files (SQL Server)](xml-format-files-sql-server.md).

## When is a format file required?

Format files are usually required in the following circumstances:

- When you use an `INSERT ... SELECT * FROM OPENROWSET(BULK...)` statement.
- For complex bulk-import situations using  **bcp** or `BULK INSERT`.
- The same data file is used as a source for multiple tables that have different schemas.
- The data file has a different number of fields that the target table has columns; for example:

  - The target table contains at least one column for which either a default value is defined or `NULL` is allowed.
  - The users don't have `SELECT`/`INSERT` permissions on one or more columns in the table.
  - A single data file is used with two or more tables that have different schemas.

- The column order is different for the data file and table.
- The terminating characters or prefix lengths differ among the columns of the data file.

> [!NOTE]  
> In the absence of a format file, if a **bcp** command specifies a data-format switch (`-n`, `-c`, `-w`, or `-N`) or a `BULK INSERT` operation specifies the `DATAFILETYPE` option, the specified data format is used as the default method of interpreting the fields of the data file.

## Related tasks

- [Using BCP native/format file vs text file and the BOM](https://techcommunity.microsoft.com/t5/sql-server-blog/using-sqlbcp-native-format-file-vs-text-file-and-the-bom/ba-p/3194629)
- [Use Non-XML format files (SQL Server)](non-xml-format-files-sql-server.md)
- [XML Format Files (SQL Server)](xml-format-files-sql-server.md)
- [Data formats for bulk import or bulk export (SQL Server)](data-formats-for-bulk-import-or-bulk-export-sql-server.md)
- [Create a format file with bcp (SQL Server)](create-a-format-file-sql-server.md)

## Related content

- [bcp Utility](../../tools/bcp-utility.md)
- [Use a format file to bulk import data (SQL Server)](use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use a Format file to skip a table column (SQL Server)](use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a format file to skip a data field (SQL Server)](use-a-format-file-to-skip-a-data-field-sql-server.md)
- [Use a format file to map table columns to data-file fields (SQL Server)](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)
