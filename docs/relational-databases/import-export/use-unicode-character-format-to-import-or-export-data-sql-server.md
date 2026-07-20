---
title: "Use Unicode Character Format to Import & Export Data"
description: The Unicode character data format allows data to be exported from a SQL Server instance by using a code page that differs from the code page used by the client.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/10/2026
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
helpviewer_keywords:
  - "data formats [SQL Server], Unicode character"
  - "Unicode [SQL Server], bulk importing and exporting"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Use Unicode character format to import or export data (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Unicode character format is recommended for bulk transfer of data between multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended/DBCS characters. The Unicode character data format allows data to be exported from a server, by using a code page that differs from the code page used by the client that is performing the operation. In such cases, use of Unicode character format has the following advantages:

- If the source and destination data are Unicode data types, use of Unicode character format preserves all of the character data.

- If the source and destination data aren't Unicode data types, use of Unicode character format minimizes the loss of extended characters in the source data that can't be represented at the destination.

## Considerations for Using Unicode character format

When using Unicode character format, consider:

- By default, the [bcp utility](../../tools/bcp-utility.md) separates the character-data fields with the tab character and terminates the records with the newline character. For information about how to specify alternative terminators, see [Specify field and row terminators (SQL Server)](specify-field-and-row-terminators-sql-server.md).

- The [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) data that is stored in a Unicode character-format data file operates in the same way it operates in a character-format data file, except that the data is stored as [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md) instead of [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) data. For more information about character format, see [Collation and Unicode support](../collations/collation-and-unicode-support.md).

- Vector type columns don't support conversion to or from character format. Hence bulk import or export of **vector** columns in character format isn't supported.

## Special considerations for Using Unicode character format, bcp, and a format file

Unicode character format data files follow the conventions for Unicode files. The first two bytes of the file are hexadecimal numbers, 0xFFFE. These bytes serve as byte-order marks (BOM), specifying whether the high-order byte is stored first or last in the file. The [bcp Utility](../../tools/bcp-utility.md) might misinterpret the BOM and cause part of your import process to fail; you might receive an error message similar as follows:

```csharp
Starting copy...
SQLState = 22005, NativeError = 0
Error = [Microsoft][ODBC Driver 13 for SQL Server]Invalid character value for cast specification
```

The BOM might be misinterpreted under the following conditions:

- The [bcp Utility](../../tools/bcp-utility.md) is used and the `-w` switch is used to indicate Unicode character

- A format file is used
- The first field in the data file is non-character

Consider whether any of the following workarounds might be available for your *specific* situation:

- Don't use a format file. An example of this workaround is provided in the [Using bcp and Unicode character format to Import Data without a format file](#use-bcp-and-unicode-character-format-to-import-data-without-a-format-file),

- Use the `-c` switch instead of `-w`,

- Re-export the data using a native format,

- Use [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md). Examples of these workarounds are provided in the [Using BULK INSERT and Unicode character format with a non-XML format file](#use-bulk-insert-and-unicode-character-format-with-a-non-xml-format-file) and [Using OPENROWSET and Unicode character format with a non-XML format file](#use-openrowset-and-unicode-character-format-with-a-non-xml-format-file) sections.

- Manually insert first record in destination table and then use `-F 2` switch to have import start on second record,

- Manually insert dummy first record in data file and then use `-F 2` switch to have import start on second record. An example of this workaround is provided in the [Using bcp and Unicode character format to Import Data with a non-XML format file](#use-bcp-and-unicode-character-format-to-import-data-with-a-non-xml-format-file) section,

- Use a staging table where the first column is a character data type, or

- Re-export the data and change the data field order so that the first data field is character. Then use a format file to remap the data field to the actual order in the table. For an example, see [Use a format file to map table columns to data-file fields (SQL Server)](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md).

## Command options for Unicode character format

You can import Unicode character format data into a table using [bcp](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md), or [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md). For a [bcp](../../tools/bcp-utility.md) command or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, you can specify the data format in the statement. For an [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) statement, you must specify the data format in a format file.

Unicode character format is supported by the following command options:

| Command | Option | Description |
| --- | --- | --- |
| `bcp` | `-w` | Uses the Unicode character format. |
| `BULK INSERT` | `DATAFILETYPE ='widechar'` | Uses Unicode character format when bulk importing data. |
| `OPENROWSET` | N/A | Must use a format file |

> [!NOTE]  
> Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [format files to import or export data (SQL Server)](format-files-for-importing-or-exporting-data-sql-server.md).

## Example test conditions

The examples in this article are based on the following table and format file.

### Sample table

The following script creates a test database, a table named `myWidechar` and populates the table with some initial values. Execute the following Transact-SQL in Microsoft [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;

CREATE TABLE dbo.myWidechar
(
    PersonID SMALLINT NOT NULL,
    FirstName NVARCHAR (25) NOT NULL,
    LastName NVARCHAR (30) NOT NULL,
    BirthDate DATE,
    AnnualSalary MONEY
);

-- Populate table
INSERT TestDatabase.dbo.myWidechar
VALUES (1, N'ϴAnthony', N'Grosse', '02-23-1980', 65000.00),
       (2, N'❤Alica', N'Fatnowna', '11-14-1963', 45000.00),
       (3, N'☎Stella', N'Rossenhain', '03-02-1992', 120000.00);

-- Review data
SELECT * FROM TestDatabase.dbo.myWidechar;
```

### Sample non-XML format file

SQL Server support two types of format file: non-XML format and XML format. The non-XML format is the original format that is supported by earlier versions of SQL Server. For more information, see [Use non-XML format files (SQL Server)](non-XML-format-files-sql-server.md).

The following command uses the [bcp utility](../../tools/bcp-utility.md) to generate a non-XML format file, `myWidechar.fmt`, based on the schema of `myWidechar`. To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the `format` argument and use `nul` instead of a data-file path. The format option also requires the `-f` option. In addition, for this example, the qualifier `c` is used to specify character data, and `T` is used to specify a trusted connection using integrated security. At a command prompt, enter the following commands:

```batch
bcp TestDatabase.dbo.myWidechar format nul -f D:\BCP\myWidechar.fmt -T -w

REM Review file
Notepad D:\BCP\myWidechar.fmt
```

> [!IMPORTANT]  
> Ensure your non-XML format file ends with a carriage return\line feed. Otherwise you might receive the following error message:
>
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

## Examples

The following examples use the database, and format files created previously.

### Use bcp and Unicode character format to Export Data

`-w` switch and `OUT` command. The data file created in this example is used in all subsequent examples. At a command prompt, enter the following commands:

```batch
bcp TestDatabase.dbo.myWidechar OUT D:\BCP\myWidechar.bcp -T -w

REM Review results
NOTEPAD D:\BCP\myWidechar.bcp
```

### Use bcp and Unicode character format to Import Data without a format file

`-w` switch and `IN` command. At a command prompt, enter the following commands:

```batch
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidechar;"

REM Import data
bcp TestDatabase.dbo.myWidechar IN D:\BCP\myWidechar.bcp -T -w

REM Review results is SSMS
```

### Use bcp and Unicode character format to Import Data with a non-XML format file

`-w` and `-f` switches and `IN` command. A workaround needs to be used since this example involves bcp, a format file, Unicode character, and the first data field in the data file is non-character. See [Special Considerations for Using Unicode character format, bcp, and a format file](#special-considerations-for-using-unicode-character-format-bcp-and-a-format-file) earlier in the article. The data file `myWidechar.bcp` is altered by adding an extra record as a "dummy" record which is then skipped with the `-F 2` switch.

At a command prompt, enter the following commands and follow the modification steps:

```batch
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidechar;"

REM Open data file
Notepad D:\BCP\myWidechar.bcp
REM Copy first record and then paste as new first record.  This additional record is the "dummy" record.
REM Close file.

REM Import data instructing bcp to skip dummy record with the -F 2 switch.
bcp TestDatabase.dbo.myWidechar IN D:\BCP\myWidechar.bcp -f D:\BCP\myWidechar.fmt -T -F 2

REM Review results is SSMS

REM Return data file to original state for usage in other examples
bcp TestDatabase.dbo.myWidechar OUT D:\BCP\myWidechar.bcp -T -w
```

### Use BULK INSERT and Unicode character format without a format file

`DATAFILETYPE` argument. Execute the following Transact-SQL in Microsoft [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myWidechar; -- for testing

BULK INSERT TestDatabase.dbo.myWidechar FROM 'D:\BCP\myWidechar.bcp'
    WITH (DATAFILETYPE = 'widechar');

-- review results
SELECT * FROM TestDatabase.dbo.myWidechar;
```

### Use BULK INSERT and Unicode character format with a non-XML format file

`FORMATFILE` argument. Execute the following Transact-SQL in Microsoft [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myWidechar; -- for testing

BULK INSERT TestDatabase.dbo.myWidechar FROM 'D:\BCP\myWidechar.bcp'
    WITH (FORMATFILE = 'D:\BCP\myWidechar.fmt');

-- review results
SELECT * FROM TestDatabase.dbo.myWidechar;
```

### Use OPENROWSET and Unicode character format with a non-XML format file

`FORMATFILE` argument. Execute the following Transact-SQL in Microsoft [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myWidechar; -- for testing

INSERT INTO TestDatabase.dbo.myWidechar
SELECT * FROM OPENROWSET (
    BULK 'D:\BCP\myWidechar.bcp',
    FORMATFILE = 'D:\BCP\myWidechar.fmt'
) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myWidechar;
```

## Related tasks

To use data formats for bulk import or bulk export

- [Import native and character format data from earlier versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)
- [Use character format to import or export data (SQL Server)](use-character-format-to-import-or-export-data-sql-server.md)
- [Use native format to import or export data (SQL Server)](use-native-format-to-import-or-export-data-sql-server.md)
- [Use Unicode native format to import or export Data (SQL Server)](use-unicode-native-format-to-import-or-export-data-sql-server.md)

## Related content

- [bcp utility](../../tools/bcp-utility.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [Collation and Unicode support](../collations/collation-and-unicode-support.md)
