---
title: "Keep Nulls or Default Values During Bulk Import"
description: For bulk import in SQL Server, both bcp and BULK INSERT load default values to replace null values. For both, you can choose to retain null values.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/19/2025
ms.service: sql
ms.subservice: data-movement
ms.topic: how-to
helpviewer_keywords:
  - "bulk importing [SQL Server], null values"
  - "bulk importing [SQL Server], default values"
  - "data formats [SQL Server], null values"
  - "bulk rowset providers [SQL Server]"
  - "bcp utility [SQL Server], null values"
  - "BULK INSERT statement"
  - "default values"
  - "OPENROWSET function, bulk importing"
  - "data formats [SQL Server], default values"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Keep nulls or default values during bulk import (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

By default, when data is imported into a table, the [bcp](../../tools/bcp-utility.md) command and [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement observe any defaults that are defined for the columns in the table. For example, if there is a null field in a data file, the default value for the column is loaded instead. The [bcp](../../tools/bcp-utility.md) command and [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement both allow you to specify that nulls values be retained.

In contrast, a regular `INSERT` statement retains the null value instead of inserting a default value. The [INSERT ... SELECT * FROM OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md) statement provides the same basic behavior as regular INSERT but additionally supports a [table hint](../../t-sql/queries/hints-transact-sql-table.md) for inserting the default values.

<a id="keep_nulls"></a>

<a id="keeping-null-values"></a>

## Keep null values

The following qualifiers specify that an empty field in the data file retains its null value during the bulk-import operation, rather than inheriting a default value (if any) for the table columns. For [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md), by default, any columns that are not specified in the bulk-load operation are set to `NULL`.

|Command|Qualifier|Qualifier type|  
|-------------|---------------|--------------------|  
| `bcp` |`-k`|Switch|  
| `BULK INSERT` |`KEEPNULLS`\*|Argument|  
| `INSERT ... SELECT * FROM OPENROWSET(BULK...)` |N/A|N/A|  

\* For [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md), if default values are not available, the table column must be defined to allow null values. 

> [!NOTE]
> These qualifiers disable checking of `DEFAULT` definitions on a table by these bulk-import commands. However, for any concurrent `INSERT` statements, `DEFAULT` definitions are expected.

<a id="keep_default"></a>

<a id="using-default-values-with-insert--select--from-openrowset-bulk-transact-sqlt-sqlfunctionsopenrowset-bulk-transact-sqlmd"></a>

<a id="using-default-values-with-insert--select--from-openrowsetbulk"></a>

## Use Default Values with INSERT ... SELECT * FROM OPENROWSET BULK

You can specify that for an empty field in the data file, the corresponding table column uses its default value (if any). To use default values, use [table hints](../../t-sql/queries/hints-transact-sql-table.md).

For more information, see [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md).

<a id="etc"></a>

## Example test conditions

The examples use the database and format files created in this article.

Change the local file location of the code sample to a file location on your machine. 

<a id="sample_table"></a>

### Sample Table

The script creates a test database and a table named `myNulls`. The fourth table column, `Kids`, has a default value. Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE dbo.myNulls ( 
   PersonID smallint not null,
   FirstName varchar(25),
   LastName varchar(30),
   Kids varchar(13) DEFAULT 'Default Value',
   BirthDate date
   );
```

<a id="sample_data_file"></a>

### Sample data file

Using Notepad, create an empty file `D:\BCP\myNulls.bcp` and insert the following sample data. There is no value in the third record, fourth column.

```text
1,Anthony,Grosse,Yes,1980-02-23
2,Alica,Fatnowna,No,1963-11-14
3,Stella,Rosenhain,,1992-03-02
```

Alternatively, you can execute the following PowerShell script to create and populate the data file:

```powershell
cls
# revise directory as desired
$dir = 'D:\BCP\';

$bcpFile = $dir + 'MyNulls.bcp';

# Confirm directory exists
IF ((Test-Path -Path $dir) -eq 0)
{
    Write-Host "The path $dir does not exist; please create or modify the directory.";
    RETURN;
};

# clear content, will error if file does not exist, can be ignored
Clear-Content -Path $bcpFile -ErrorAction SilentlyContinue;

# Add data
Add-Content -Path $bcpFile -Value '1,Anthony,Grosse,Yes,1980-02-23';
Add-Content -Path $bcpFile -Value '2,Alica,Fatnowna,No,1963-11-14';
Add-Content -Path $bcpFile -Value '3,Stella,Rosenhain,,1992-03-02';

#Review content
Get-Content -Path $bcpFile;
Invoke-Item $bcpFile;
```

<a id="nonxml_format_file"></a>

### Sample non-XML format file

SQL Server support two types of format file: non-XML format and XML format. The non-XML format is the original format that is supported by earlier versions of SQL Server. For more information, see [Use Non-XML format files (SQL Server)](non-xml-format-files-sql-server.md). 

The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-XML format file, `myNulls.fmt`, based on the schema of `myNulls`. 

- To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the `format` argument and use `nul` instead of a data-file path. 
- The format option also requires the `-f` option. 
- `c` is used to specify character data
- `t,` is used to specify a comma as a [field terminator](specify-field-and-row-terminators-sql-server.md)
- `T` is used to specify a trusted connection using integrated security. 

At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myNulls format nul -c -f D:\BCP\myNulls.fmt -t, -T

REM Review file
Notepad D:\BCP\myNulls.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed. Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

 For more information about creating format files, see [Create a format file with bcp (SQL Server)](create-a-format-file-sql-server.md).  

<a id="import_data"></a>

## Keep nulls or use default values during bulk import

The examples use the database, datafile, and format files created in this article.

<a id="bcp_null"></a>

<a id="using-bcptoolsbcp-utilitymd-and-keeping-null-values-without-a-format-file"></a>

### Use bcp and keep null values without a format file

The `-k` switch. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -c -t, -T -k

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```

<a id="bcp_null_fmt"></a>

<a id="using-bcptoolsbcp-utilitymd-and-keeping-null-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use bcp and keep null values with a non-XML format file 

The `-k` and `-f` switches. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -f D:\BCP\myNulls.fmt -T -k

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```

<a id="bcp_default"></a>

<a id="using-bcptoolsbcp-utilitymd-and-using-default-values-without-a-format-file"></a>

### Use bcp and default values without a format file

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -c -t, -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```

<a id="bcp_default_fmt"></a>

<a id="using-bcptoolsbcp-utilitymd-and-using-default-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use bcp and default values with a non-XML format file

The `-f` switch. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -f D:\BCP\myNulls.fmt -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```

<a id="bulk_null"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-keeping-null-values-without-a-format-file"></a>

### Use BULK INSERT and keep null values without a format file

The `KEEPNULLS` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO
TRUNCATE TABLE dbo.myNulls; -- for testing
BULK INSERT dbo.myNulls
    FROM 'D:\BCP\myNulls.bcp'
    WITH (
        DATAFILETYPE = 'char',  
        FIELDTERMINATOR = ',',  
        KEEPNULLS
        );

-- review results
SELECT * FROM TestDatabase.dbo.myNulls;
```

<a id="bulk_null_fmt"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-keeping-null-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use BULK INSERT and keep null values with a non-XML format file

The `KEEPNULLS` and the `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myNulls; -- for testing
BULK INSERT dbo.myNulls
   FROM 'D:\BCP\myNulls.bcp'
   WITH (
        FORMATFILE = 'D:\BCP\myNulls.fmt',
        KEEPNULLS
        );

-- review results
SELECT * FROM TestDatabase.dbo.myNulls;
```

<a id="bulk_default"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-using-default-values-without-a-format-file"></a>

### Use BULK INSERT and use default values without a format file

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myNulls;  -- for testing
BULK INSERT dbo.myNulls
   FROM 'D:\BCP\myNulls.bcp'
   WITH (
      DATAFILETYPE = 'char',  
      FIELDTERMINATOR = ','
      );

-- review results
SELECT * FROM TestDatabase.dbo.myNulls;
```

<a id="bulk_default_fmt"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-using-default-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use BULK INSERT and default values with a non-XML format file

The `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myNulls;  -- for testing
BULK INSERT dbo.myNulls
   FROM 'D:\BCP\myNulls.bcp'
   WITH (
        FORMATFILE = 'D:\BCP\myNulls.fmt'
        );

-- review results
SELECT * FROM TestDatabase.dbo.myNulls;
```

<a id="openrowset__null_fmt"></a>

<a id="using-openrowset-bulk-transact-sqlt-sqlfunctionsopenrowset-bulk-transact-sqlmd-and-keeping-null-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use OPENROWSET BULK and keep null values with a non-XML format file

The `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myNulls;  -- for testing
INSERT INTO dbo.myNulls
    SELECT *
    FROM OPENROWSET (
        BULK 'D:\BCP\myNulls.bcp', 
        FORMATFILE = 'D:\BCP\myNulls.fmt'  
        ) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myNulls;
```

<a id="openrowset__default_fmt"></a>

<a id="using-openrowset-bulk-transact-sqlt-sqlfunctionsopenrowset-bulk-transact-sqlmd-and-using-default-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use OPENROWSET BULK and keep default values with a non-XML format file

The `KEEPDEFAULTS` table hint and `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myNulls;  -- for testing
INSERT INTO dbo.myNulls
WITH (KEEPDEFAULTS) 
    SELECT *
    FROM OPENROWSET (
        BULK 'D:\BCP\myNulls.bcp', 
        FORMATFILE = 'D:\BCP\myNulls.fmt'  
        ) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myNulls;
```

<a id="RelatedTasks"></a>

## Related Tasks

-   [Keep identity values when bulk importing data (SQL Server)](keep-identity-values-when-bulk-importing-data-sql-server.md)  

-   [Prepare data for bulk export or import](prepare-data-for-bulk-export-or-import-sql-server.md)  

 **To use a format file**  

-   [Create a format file with bcp (SQL Server)](create-a-format-file-sql-server.md)  

-   [Use a format file to bulk import data (SQL Server)](use-a-format-file-to-bulk-import-data-sql-server.md)  

-   [Use a format file to map table columns to data-file fields (SQL Server)](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  

-   [Use a format file to skip a data field (SQL Server)](use-a-format-file-to-skip-a-data-field-sql-server.md)  

-   [Use a Format File to Skip a Table Column (SQL Server)](use-a-format-file-to-skip-a-table-column-sql-server.md)  

 **To use data formats for bulk import or bulk export**  

-   [Import native and character format data from earlier versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  

-   [Use character format to import or export data (SQL Server)](use-character-format-to-import-or-export-data-sql-server.md)  

-   [Use native format to import or export data (SQL Server)](use-native-format-to-import-or-export-data-sql-server.md)  

-   [Use unicode character format to import or export data (SQL Server)](use-unicode-character-format-to-import-or-export-data-sql-server.md)  

-   [Use Unicode Native Format to Import or Export Data (SQL Server)](use-unicode-native-format-to-import-or-export-data-sql-server.md)  

 **To specify data formats for compatibility when using bcp**  

-   [Specify field and row terminators (SQL Server)](specify-field-and-row-terminators-sql-server.md)  

-   [Specify prefix length in data files using bcp (SQL Server)](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  

-   [Specify file storage type using bcp (SQL Server)](specify-file-storage-type-by-using-bcp-sql-server.md)  

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [OPENROWSET BULK (Transact-SQL)](../../t-sql/functions/openrowset-bulk-transact-sql.md)
- [bcp utility](../../tools/bcp/bcp-utility.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [Table hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-table.md)
