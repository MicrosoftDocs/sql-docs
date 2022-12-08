---
title: "Use a format file to bulk import data"
description: In SQL Server, you can use a format file in bulk-import operations. A format file maps the fields of the data file to the columns of the table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/29/2022
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "bulk importing [SQL Server], format files"
  - "format files [SQL Server], importing data using"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use a format file to bulk import data (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article illustrates the use of a format file in bulk-import operations.  A format file maps the fields of the data file to the columns of the table.  Review [Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md) for additional information.

## Before you begin

* For a format file to work with a Unicode character data file, all input fields must be Unicode text strings (that is, either fixed-size or character-terminated Unicode strings).
* To bulk export or import [SQLXML](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md) data, use one of the following data types in your format file:
  * SQLCHAR or SQLVARCHAR (the data is sent in the client code page or in the code page implied by the collation)
  * SQLNCHAR or SQLNVARCHAR (the data is sent as Unicode)
  * SQLBINARY or SQLVARBIN (the data is sent without any conversion).
* Azure SQL Database and Azure Synapse Analytics only support [bcp](../../tools/bcp-utility.md).  For additional information, see:
  * [Load data into Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/design-elt-data-loading)
  * [Load data from SQL Server into Azure Synapse Analytics (flat files)](/azure/synapse-analytics/sql-data-warehouse/design-elt-data-loading)
  * [Migrate Your Data](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-develop)
* This syntax, including bulk insert, is not supported in Azure Synapse Analytics. [!INCLUDE[Use ADF or PolyBase instead of Synapse Bulk Insert](../../includes/paragraph-content/bulk-insert-synapse.md)]

## Example test conditions

The examples of format files in this topic are based on the table and data file defined below.

### Sample table

The script below creates a test database and a table named `myFirstImport`.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE dbo.MyFirstImport (
   PersonID smallint,
   FirstName varchar(25),
   LastName varchar(30),
   BirthDate Date
   );
```

### Sample data file

Using Notepad, create an empty file `D:\BCP\myFirstImport.bcp` and insert the following data:

```output
1,Anthony,Grosse,1980-02-23
2,Alica,Fatnowna,1963-11-14
3,Stella,Rosenhain,1992-03-02
```

Alternatively, you can execute the following PowerShell script to create and populate the data file:

```powershell
Clear-Host
# revise directory as desired

$dir = 'D:\BCP\';

$bcpFile = Join-Path -Path $dir -ChildPath 'MyFirstImport.bcp';

# Confirm directory exists

IF ((Test-Path -Path $dir) -eq 0)
{
    Write-Host "The path $dir does not exist; please create or modify the directory.";
    RETURN;
};

# Clear content, will error if file does not exist, can be ignored

Clear-Content -Path $bcpFile -ErrorAction SilentlyContinue;

# Add data

Add-Content -Path $bcpFile -Value '1,Anthony,Grosse,1980-02-23';
Add-Content -Path $bcpFile -Value '2,Alica,Fatnowna,1963-11-14';
Add-Content -Path $bcpFile -Value '3,Stella,Rosenhain,1992-03-02';

# Review content

Get-Content -Path $bcpFile;
Notepad.exe $bcpfile;
```

## Create the format files

SQL Server support two types of format file: non-XML format and XML format.  The non-XML format is the original format that is supported by earlier versions of SQL Server.

### Create a non-XML format file

Review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myFirstImport.fmt`, based on the schema of `myFirstImport`.  To use a bcp command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option also requires the **-f** option.  In addition, for this example, the qualifier **c** is used to specify character data, **t,** is used to specify a comma as a [field terminator](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md), and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myFirstImport format nul -c -f D:\BCP\myFirstImport.fmt -t, -T

REM Review file
Notepad D:\BCP\myFirstImport.fmt
```

Your non-XML format file, `D:\BCP\myFirstImport.fmt` should look as follows:

```output
13.0
4
1       SQLCHAR             0       7       ","      1     PersonID               ""
2       SQLCHAR             0       25      ","      2     FirstName              SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR             0       30      ","      3     LastName               SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR             0       11      "\r\n"   4     BirthDate              ""
```

> [!IMPORTANT]  
> Ensure your non-XML format file ends with a carriage return\line feed.  Otherwise you will likely receive the following error message:
>  
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

### Create an XML format file

Review [XML Format Files (SQL Server)](../../relational-databases/import-export/xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to create an xml format file, `myFirstImport.xml`, based on the schema of `myFirstImport`. To use a bcp command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option always requires the **-f** option, and to create an XML format file, you must also specify the **-x** option.  In addition, for this example, the qualifier **c** is used to specify character data, **t,** is used to specify a comma as a [field terminator](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md), and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myFirstImport format nul -c -x -f D:\BCP\myFirstImport.xml -t, -T

REM Review file
Notepad D:\BCP\myFirstImport.xml
```

Your XML format file, `D:\BCP\myFirstImport.xml` should look as follows:

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="http://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<RECORD>
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="11"/>
</RECORD>
<ROW>
  <COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  <COLUMN SOURCE="2" NAME="FirstName" xsi:type="SQLVARCHAR"/>
  <COLUMN SOURCE="3" NAME="LastName" xsi:type="SQLVARCHAR"/>
  <COLUMN SOURCE="4" NAME="BirthDate" xsi:type="SQLDATE"/>
</ROW>
</BCPFORMAT>
```

## Use a format file to bulk import data

The examples below use the database, datafile, and format files created above.

### Use [bcp](../../tools/bcp-utility.md) and [Non-XML Format File](../../relational-databases/import-export/non-XML-format-files-SQL-server.md)

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.MyFirstImport;"

REM Import data
bcp TestDatabase.dbo.myFirstImport IN D:\BCP\myFirstImport.bcp -f D:\BCP\myFirstImport.fmt -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.MyFirstImport"
```

### Use [bcp](../../tools/bcp-utility.md) and [XML format file](../../relational-databases/import-export/XML-format-files-SQL-server.md)

At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.MyFirstImport;"

REM Import data
bcp TestDatabase.dbo.myFirstImport IN D:\BCP\myFirstImport.bcp -f D:\BCP\myFirstImport.xml -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.MyFirstImport;"
```

### Use [BULK INSERT](../../T-SQL/statements/bulk-insert-Transact-SQL.md) and [Non-XML Format File](../../relational-databases/import-export/non-XML-format-files-SQL-server.md)

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myFirstImport; -- (for testing)
BULK INSERT dbo.myFirstImport  
   FROM 'D:\BCP\myFirstImport.bcp'  
   WITH (FORMATFILE = 'D:\BCP\myFirstImport.fmt');  
GO

-- review results
SELECT * FROM TestDatabase.dbo.myFirstImport;
```

### Use [BULK INSERT](../../T-SQL/statements/bulk-insert-Transact-SQL.md) and [XML Format File](../../relational-databases/import-export/XML-format-files-SQL-server.md)

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myFirstImport; -- (for testing)
BULK INSERT dbo.myFirstImport  
   FROM 'D:\BCP\myFirstImport.bcp'  
   WITH (FORMATFILE = 'D:\BCP\myFirstImport.xml');  
GO

-- review results
SELECT * FROM TestDatabase.dbo.myFirstImport;
```

### Use [OPENROWSET(BULK...)](../../T-SQL/functions/OPENROWSET-Transact-SQL.md) and [Non-XML Format File](../../relational-databases/import-export/non-XML-format-files-SQL-server.md)

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;
GO

TRUNCATE TABLE myFirstImport; -- (for testing)
INSERT INTO dbo.myFirstImport
    SELECT *
    FROM OPENROWSET (
        BULK 'D:\BCP\myFirstImport.bcp',
        FORMATFILE = 'D:\BCP\myFirstImport.fmt'
        ) AS t1;
GO

-- review results
SELECT * FROM TestDatabase.dbo.myFirstImport;
```

### Use [OPENROWSET(BULK...)](../../T-SQL/functions/OPENROWSET-Transact-SQL.md) and [XML Format File](../../relational-databases/import-export/XML-format-files-SQL-server.md)

Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myFirstImport; -- (for testing)
INSERT INTO dbo.myFirstImport  
    SELECT *
    FROM OPENROWSET (
        BULK 'D:\BCP\myFirstImport.bcp',
        FORMATFILE = 'D:\BCP\myFirstImport.xml'  
       ) AS t1;
GO

-- review results
SELECT * FROM TestDatabase.dbo.myFirstImport;
```

## More examples

[Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md)  
[Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)  
[Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)  
[Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

## Next steps

- [bcp Utility](../../tools/bcp-utility.md)
- [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)
- [Non-XML Format Files &#40;SQL Server&#41;](../../relational-databases/import-export/non-xml-format-files-sql-server.md)
- [XML Format Files &#40;SQL Server&#41;](../../relational-databases/import-export/xml-format-files-sql-server.md)
- [Format Files for Importing or Exporting Data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)
