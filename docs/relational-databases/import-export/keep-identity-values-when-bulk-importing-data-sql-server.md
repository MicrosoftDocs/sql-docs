---
title: "Keep Identity Values When Bulk Importing Data"
description: When you bulk import data that contains identity values to a SQL Server instance, by default, it assigns new values. You can choose to keep the original values.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/19/2025
ms.service: sql
ms.subservice: data-movement
ms.topic: concept-article
helpviewer_keywords:
  - "identity values [SQL Server], bulk imports"
  - "data formats [SQL Server], identity values"
  - "bulk importing [SQL Server], identity values"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Keep identity values when bulk importing data (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Data files that contain identity values can be bulk imported into an instance of Microsoft SQL Server. 

By default, the values for the identity column in the data file that is imported are ignored and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns unique values automatically. The unique values are based on the seed and increment values that are specified during table creation.

If the data file does not contain values for the identifier column in the table, use a format file to specify that the identifier column in the table should be skipped when importing data. See [Use a Format File to Skip a Table Column (SQL Server)](use-a-format-file-to-skip-a-table-column-sql-server.md) for additional information.

<a id="keep_identity"></a>

## Keep identity values

To prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from assigning identity values while bulk importing data rows into a table, use the appropriate keep-identity command qualifier. When you specify a keep-identity qualifier, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the identity values in the data file. 

These qualifiers are as follows:

|Command|Keep-identity qualifier|Qualifier type|  
|-------------|------------------------------|--------------------|  
| `bcp` |`-E`|Switch|  
| `BULK INSERT` |`KEEPIDENTITY`|Argument|  
| `INSERT ... SELECT * FROM OPENROWSET(BULK...)` |KEEPIDENTITY|Table hint|  

 For more information, see [bcp Utility](../../tools/bcp-utility.md), [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md), [OPENROWSET BULK (Transact-SQL)](../../t-sql/functions/openrowset-bulk-transact-sql.md), [INSERT (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md), [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md), and [Table hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-table.md).  

> [!NOTE]
>  To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../sequence-numbers/sequence-numbers.md).

<a id="etc"></a>

## Example Test Conditions

The examples in this topic are based on the table, data file, and format file defined below.

<a id="sample_table"></a>

### Sample Table

The script below creates a test database and a table named `myIdentity`. Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE dbo.myIdentity ( 
   PersonID smallint IDENTITY(1,1) NOT NULL,
   FirstName varchar(25) NOT NULL,
   LastName varchar(30) NOT NULL,
   BirthDate date
   );
```

<a id="sample_data_file"></a>

### Sample Data File

Using Notepad, create an empty file `D:\BCP\myIdentity.bcp` and insert the data below.  
```
3,Anthony,Grosse,1980-02-23
2,Alica,Fatnowna,1963-11-14
1,Stella,Rosenhain,1992-03-02
4,Miller,Dylan,1954-01-05
```

Alternatively, you can execute the following PowerShell script to create and populate the data file:
```powershell
cls
# revise directory as desired
$dir = 'D:\BCP\';

$bcpFile = $dir + 'myIdentity.bcp';

# Confirm directory exists
IF ((Test-Path -Path $dir) -eq 0)
{
    Write-Host "The path $dir does not exist; please create or modify the directory.";
    RETURN;
};

# clear content, will error if file does not exist, can be ignored
Clear-Content -Path $bcpFile -ErrorAction SilentlyContinue;

# Add data
Add-Content -Path $bcpFile -Value '3,Anthony,Grosse,1980-02-23';
Add-Content -Path $bcpFile -Value '2,Alica,Fatnowna,1963-11-14';
Add-Content -Path $bcpFile -Value '1,Stella,Rosenhain,1992-03-02';
Add-Content -Path $bcpFile -Value '4,Miller,Dylan,1954-01-05';

#Review content
Get-Content -Path $bcpFile;
Invoke-Item $bcpFile;
```

<a id="nonxml_format_file"></a>

### Sample Non-XML format file

SQL Server support two types of format file: non-XML format and XML format. The non-XML format is the original format that is supported by earlier versions of SQL Server. For more information, see [Use Non-XML format files (SQL Server)](non-xml-format-files-sql-server.md).

The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myIdentity.fmt`, based on the schema of `myIdentity`. 

- To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the `format` argument and use `nul` instead of a data-file path. 
- The format option also requires the `-f` option. 
- `c` is used to specify character data
- `t,` is used to specify a comma as a [field terminator](specify-field-and-row-terminators-sql-server.md)
- `T` is used to specify a trusted connection using integrated security.

At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myIdentity format nul -c -f D:\BCP\myIdentity.fmt -t, -T

REM Review file
Notepad D:\BCP\myIdentity.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed. Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

<a id="examples"></a>

## Examples

The examples use the database, datafile, and format files created in this article.

<a id="bcp_identity"></a>

<a id="using-bcptoolsbcp-utilitymd-and-keeping-identity-values-without-a-format-file"></a>

### Use bcp and keep Identity Values without a format file

The `-E` switch. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -T -c -t, -E

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```

<a id="bcp_identity_fmt"></a>

<a id="using-bcptoolsbcp-utilitymd-and-keeping-identity-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use bcp and keep Identity values with a non-XML format file

The `-E` and `-f` switches. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -f D:\BCP\myIdentity.fmt -T -E

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```

<a id="bcp_default"></a>

<a id="using-bcptoolsbcp-utilitymd-and-generated-identity-values-without-a-format-file"></a>

### Use bcp and generated Identity values without a format file

Using defaults. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -T -c -t,

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```

<a id="bcp_default_fmt"></a>

<a id="using-bcptoolsbcp-utilitymd-and-generated-identity-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use bcp and generated Identity Values with a non-XML format file

Use defaults and `-f` switch. 

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -f D:\BCP\myIdentity.fmt -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```

<a id="bulk_identity"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-keeping-identity-values-without-a-format-file"></a>

### Use BULK INSERT and keep Identity values without a format file

The `KEEPIDENTITY` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myIdentity; -- for testing
BULK INSERT dbo.myIdentity
    FROM 'D:\BCP\myIdentity.bcp'
    WITH (
        DATAFILETYPE = 'char',  
        FIELDTERMINATOR = ',',  
        KEEPIDENTITY
        );

-- review results
SELECT * FROM TestDatabase.dbo.myIdentity;
```

<a id="bulk_identity_fmt"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-keeping-identity-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use BULK INSERT and keep Identity values with a non-XML format file

The `KEEPIDENTITY` and the `FORMATFILE` arguments. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myIdentity; -- for testing
BULK INSERT dbo.myIdentity
   FROM 'D:\BCP\myIdentity.bcp'
   WITH (
        FORMATFILE = 'D:\BCP\myIdentity.fmt',
        KEEPIDENTITY
        );

-- review results
SELECT * FROM TestDatabase.dbo.myIdentity;
```

<a id="bulk_default"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-generated-identity-values-without-a-format-file"></a>

### Use BULK INSERT and generated Identity values without a format file

Using defaults. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myIdentity;  -- for testing
BULK INSERT dbo.myIdentity
   FROM 'D:\BCP\myIdentity.bcp'
   WITH (
      DATAFILETYPE = 'char',  
      FIELDTERMINATOR = ','
      );

-- review results
SELECT * FROM TestDatabase.dbo.myIdentity;
```

<a id="bulk_default_fmt"></a>

<a id="using-bulk-insert-transact-sqlt-sqlstatementsbulk-insert-transact-sqlmd-and-generated-identity-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use BULK INSERT and generated Identity values with a non-XML format file

Using defaults and `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myIdentity;  -- for testing
BULK INSERT dbo.myIdentity
   FROM 'D:\BCP\myIdentity.bcp'
   WITH (
        FORMATFILE = 'D:\BCP\myIdentity.fmt'
        );

-- review results
SELECT * FROM TestDatabase.dbo.myIdentity;
```

<a id="openrowset_identity_fmt"></a>

<a id="using-openrowset-bulk-transact-sqlt-sqlfunctionsopenrowset-bulk-transact-sqlmd-and-keeping-identity-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use OPENROWSET BULK and keep Identity values with a non-XML format file

The `KEEPIDENTITY` table hint and `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myIdentity;  -- for testing
INSERT INTO dbo.myIdentity
WITH (KEEPIDENTITY) 
(PersonID, FirstName, LastName, BirthDate)
    SELECT *
    FROM OPENROWSET (
        BULK 'D:\BCP\myIdentity.bcp', 
        FORMATFILE = 'D:\BCP\myIdentity.fmt'  
        ) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myIdentity;
```

<a id="openrowset_default_fmt"></a>

<a id="using-openrowset-bulk-transact-sqlt-sqlfunctionsopenrowset-bulk-transact-sqlmd-and-generated-identity-values-with-a-use-non-xml-format-files-sql-servernon-xml-format-files-sql-servermd"></a>

### Use OPENROWSET BULK and generated Identity values with a non-XML format file

Using defaults and the `FORMATFILE` argument. 

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE dbo.myIdentity;  -- for testing
INSERT INTO dbo.myIdentity
(FirstName, LastName, BirthDate)
    SELECT FirstName, LastName, BirthDate
    FROM OPENROWSET (
        BULK 'D:\BCP\myIdentity.bcp', 
        FORMATFILE = 'D:\BCP\myIdentity.fmt'  
        ) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myIdentity;
```

<a id="RelatedTasks"></a>

## Related Tasks

-   [Keep nulls or default values during bulk import (SQL Server)](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  

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

1. [Specify field and row terminators (SQL Server)](specify-field-and-row-terminators-sql-server.md)  

1. [Specify prefix length in data files using bcp (SQL Server)](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  

1. [Specify file storage type using bcp (SQL Server)](specify-file-storage-type-by-using-bcp-sql-server.md)  

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [bcp utility](../../tools/bcp/bcp-utility.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [Table hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-table.md)
- [Format files to import or export data (SQL Server)](format-files-for-importing-or-exporting-data-sql-server.md)
