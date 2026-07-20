---
title: "Use Unicode Native Format to Import or Export Data (SQL Server)"
description: Use Unicode native format for bulk transfer of data between instances of SQL Server, which eliminates conversion of data types to and from character format.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/27/2025
ms.service: sql
ms.subservice: data-movement
ms.topic: how-to
helpviewer_keywords:
  - "Unicode [SQL Server], bulk importing and exporting"
  - "data formats [SQL Server], Unicode native"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Use Unicode Native Format to Import or Export Data (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Unicode native format is helpful when information must be copied from one [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation to another. The use of native format for noncharacter data saves time, eliminating unnecessary conversion of data types to and from character format. The use of Unicode character format for all character data prevents loss of any extended characters during bulk transfer of data between servers using different code pages. A data file in Unicode native format can be read by any bulk-import method.  

 Unicode native format is recommended for the bulk transfer of data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended or DBCS characters. For noncharacter data, Unicode native format uses native (database) data types. For character data, such as [char](../../t-sql/data-types/char-and-varchar-transact-sql.md), [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md), [nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), [text](../../t-sql/data-types/ntext-text-and-image-transact-sql.md), [varchar(max)](../../t-sql/data-types/char-and-varchar-transact-sql.md), [nvarchar(max)](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), and [ntext](../../t-sql/data-types/ntext-text-and-image-transact-sql.md), the Unicode native format uses Unicode character data format.  

 The [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) data that is stored as a SQLVARIANT in a Unicode native-format data file operates in the same manner as it does in a native-format data file, except that [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) and [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) values are converted to [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md) and [nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), which doubles the amount of storage required for the affected columns. The original metadata is preserved, and the values are converted back to their original [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) and [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) data type when bulk imported into a table column.  

<a id="command_options"></a>

## Command options for unicode native format

You can import Unicode native format data into a table using [bcp](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md). 

- For a [bcp](../../tools/bcp-utility.md) command or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, you can specify the data format in the statement. 
- For an [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md) statement, you must specify the data format in a format file.  

Unicode native format is supported by the following command options:  

|Command|Option|Description|  
|-------------|------------|-----------------|  
| `bcp` |`-N`|Causes the `bcp` utility to use the Unicode native format, which uses native (database) data types for all noncharacter data and Unicode character data format for all character (**char**, **nchar**, **varchar**, **nvarchar**, **text**, and **ntext**) data.|  
| `BULK INSERT` |`DATAFILETYPE ='widenative'`|Uses Unicode native format when bulk importing data.|  
| `OPENROWSET` |N/A|Must use a format file|

> [!NOTE]
>  Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [Format files to import or export data (SQL Server)](format-files-for-importing-or-exporting-data-sql-server.md).

<a id="etc"></a>

## Example test conditions

The examples in this topic are based on the sample table `myWidenative` and format file `myWidenative.fmt`. Replace the local file paths with a local file path on your system.

<a id="sample_table"></a>

### Sample table

The following script creates a test database, a table named `myWidenative` and populates the table with some initial values. Execute the following Transact-SQL:
```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE dbo.myWidenative ( 
    PersonID smallint NOT NULL,
    FirstName nvarchar(25) NOT NULL,
    LastName nvarchar(30) NOT NULL,
    BirthDate date,
    AnnualSalary money
);

-- Populate table
INSERT TestDatabase.dbo.myWidenative
VALUES (1, N'ϴAnthony', N'Grosse', '02-23-1980', 65000.00),
       (2, N'❤Alica', N'Fatnowna', '11-14-1963', 45000.00),
       (3, N'☎Stella', N'Rossenhain', '03-02-1992', 120000.00);

-- Review Data
SELECT * FROM TestDatabase.dbo.myWidenative;
```

<a id="nonxml_format_file"></a>

### Sample non-XML format file

SQL Server support two types of format file: non-XML format and XML format. The non-XML format is the original format that is supported by earlier versions of SQL Server. Please review [Use Non-XML format files (SQL Server)](non-xml-format-files-sql-server.md) for detailed information. The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myWidenative.fmt`, based on the schema of `myWidenative`. 

- To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the `format` argument and use `nul` instead of a data-file path. 
- The format option also requires the `-f` option. 
- `c` is used to specify character data
- `T` is used to specify a trusted connection using integrated security. 

At a command prompt, enter the following commands:

```cmd
bcp TestDatabase.dbo.myWidenative format nul -f D:\BCP\myWidenative.fmt -T -N

REM Review file
Notepad D:\BCP\myWidenative.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed. Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

<a id="examples"></a>

## Examples

The examples below use the database, and format files created above.

<a id="bcp_widenative_export"></a>

<a id="using-bcp-and-unicode-native-format-to-export-data"></a>

### Use bcp and Unicode native format to export data

The `-N` switch and `OUT` command. 

The data file created in this example will be used in all subsequent examples. 

At a command prompt, enter the following commands:

```cmd
bcp TestDatabase.dbo.myWidenative OUT D:\BCP\myWidenative.bcp -T -N

REM Review results
NOTEPAD D:\BCP\myWidenative.bcp
```

<a id="bcp_widenative_import"></a>

<a id="using-bcp-and-unicode-native-format-to-import-data-without-a-format-file"></a>

### Use bcp and Unicode native format to import data without a format file

The `-N` switch and `IN` command. 

At a command prompt, enter the following commands:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidenative;"

REM Import data
bcp TestDatabase.dbo.myWidenative IN D:\BCP\myWidenative.bcp -T -N

REM Review results is SSMS
```

<a id="bcp_widenative_import_fmt"></a>

<a id="using-bcp-and-unicode-native-format-to-import-data-with-a-non-xml-format-file"></a>

### Use bcp and Unicode native format to import data with a non-XML format file

The `-N` and `-f` switches and `IN` command. 

At a command prompt, enter the following commands:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidenative;"

REM Import data
bcp TestDatabase.dbo.myWidenative IN D:\BCP\myWidenative.bcp -f D:\BCP\myWidenative.fmt -T -N

REM Review results is SSMS
```

<a id="bulk_widenative"></a>

<a id="using-bulk-insert-and-unicode-native-format-without-a-format-file"></a>

### Use BULK INSERT and Unicode native format without a format file

The `DATAFILETYPE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myWidenative; -- for testing

BULK INSERT TestDatabase.dbo.myWidenative
    FROM 'D:\BCP\myWidenative.bcp'
    WITH (DATAFILETYPE = 'widenative' );

-- review results
SELECT * FROM TestDatabase.dbo.myWidenative;
```

<a id="bulk_widenative_fmt"></a>

<a id="using-bulk-insert-and-unicode-native-format-with-a-non-xml-format-file"></a>

### Use BULK INSERT and Unicode native format with a non-XML format file

The `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myWidenative; -- for testing

BULK INSERT TestDatabase.dbo.myWidenative
   FROM 'D:\BCP\myWidenative.bcp'
   WITH ( FORMATFILE = 'D:\BCP\myWidenative.fmt'  );

-- review results
SELECT * FROM TestDatabase.dbo.myWidenative;
```

<a id="openrowset_widenative_fmt"></a>

<a id="using-openrowset-and-unicode-native-format-with-a-non-xml-format-file"></a>

### Use OPENROWSET and Unicode native format with a non-XML format file

The `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myWidenative; -- for testing

INSERT INTO TestDatabase.dbo.myWidenative
SELECT * FROM OPENROWSET (
    BULK 'D:\BCP\myWidenative.bcp',
    FORMATFILE = 'D:\BCP\myWidenative.fmt'
) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myWidenative;
```

<a id="RelatedTasks"></a>

## Related tasks

To use data formats for bulk import or bulk export:

- [Import native and character format data from earlier versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)
- [Use character format to import or export data (SQL Server)](use-character-format-to-import-or-export-data-sql-server.md)
- [Use native format to import or export data (SQL Server)](use-native-format-to-import-or-export-data-sql-server.md)
- [Use Unicode character format to import or export data (SQL Server)](use-unicode-character-format-to-import-or-export-data-sql-server.md)

## Related content

- [bcp Utility](../../tools/bcp-utility.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [OPENROWSET BULK (Transact-SQL)](../../t-sql/functions/openrowset-bulk-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
