---
title: "Keep nulls or default values during bulk import"
description: For bulk import in SQL Server, both bcp and BULK INSERT load default values to replace null values. For both, you can choose to retain null values.
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/20/2016"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019
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
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Keep nulls or default values during bulk import (SQL Server)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

By default, when data is imported into a table, the [bcp](../../tools/bcp-utility.md) command and [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement observe any defaults that are defined for the columns in the table.  For example, if there is a null field in a data file, the default value for the column is loaded instead.  The [bcp](../../tools/bcp-utility.md) command and [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement both allow you to specify that nulls values be retained.

In contrast, a regular INSERT statement retains the null value instead of inserting a default value. The INSERT ... SELECT * FROM [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement provides the same basic behavior as regular INSERT but additionally supports a [table hint](../../t-sql/queries/hints-transact-sql-table.md) for inserting the default values.

|Outline|
|---|
|[Keeping Null Values](#keep_nulls)<br />[Using Default Values with INSERT ... SELECT * FROM OPENROWSET(BULK...)](#keep_default)<br />[Example Test Conditions](#etc)<br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Data File](#sample_data_file)<br />&emsp;&#9679;&emsp;[Sample Non-XML Format File](#nonxml_format_file)<br />[Keep Nulls or Use Default Values During Bulk Import](#import_data)<br />&emsp;&#9679;&emsp;[Using bcp and Keeping Null Values without a Format File](#bcp_null)<br />&emsp;&#9679;&emsp;[Using bcp and Keeping Null Values with a Non-XML Format File](#bcp_null_fmt)<br />&emsp;&#9679;&emsp;[Using bcp and Using Default Values without a Format File](#bcp_default)<br />&emsp;&#9679;&emsp;[Using bcp and Using Default Values with a Non-XML Format File](#bcp_default_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Keeping Null Values without a Format File](#bulk_null)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Keeping Null Values with a Non-XML Format File](#bulk_null_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Using Default Values without a Format File](#bulk_default)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Using Default Values with a Non-XML Format File](#bulk_default_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET(BULK...) and Keeping Null Values with a Non-XML Format File](#openrowset__null_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET(BULK...) and Using Default Values with a Non-XML Format File](#openrowset__default_fmt)

## Keeping Null Values<a name="keep_nulls"></a>  
The following qualifiers specify that an empty field in the data file retains its null value during the bulk-import operation, rather than inheriting a default value (if any) for the table columns.  For [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md), by default, any columns that are not specified in the bulk-load operation are set to NULL.
  
|Command|Qualifier|Qualifier type|  
|-------------|---------------|--------------------|  
|bcp|-k|Switch|  
|BULK INSERT|KEEPNULLS\*|Argument|  
|INSERT ... SELECT * FROM OPENROWSET(BULK...)|N/A|N/A|  
  
\* For [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md), if default values are not available, the table column must be defined to allow null values. 
  
> [!NOTE]
> These qualifiers disable checking of DEFAULT definitions on a table by these bulk-import commands.  However, for any concurrent INSERT statements, DEFAULT definitions are expected.
 
## Using Default Values with INSERT ... SELECT * FROM [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md)<a name="keep_default"></a>  
You can specify that for an empty field in the data file, the corresponding table column uses its default value (if any).  To use default values, use the table hint [KEEPDEFAULTS](../../t-sql/queries/hints-transact-sql-table.md).
 
> [!NOTE]
>  For more information, see [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md), [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md), [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md), and [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md)

## Example Test Conditions<a name="etc"></a>  
The examples in this topic are based on the table, data file, and format file defined below.

### **Sample Table**<a name="sample_table"></a>
The script below creates a test database and a table named `myNulls`.  Notice that the fourth table column, `Kids`, has a default value.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

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

### **Sample Data File**<a name="sample_data_file"></a>
Using Notepad, create an empty file `D:\BCP\myNulls.bcp` and insert the data below.  Note that there is no value in the third record, fourth column.

```
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
  
### **Sample Non-XML Format File**<a name="nonxml_format_file"></a>
SQL Server support two types of format file: non-XML format and XML format.  The non-XML format is the original format that is supported by earlier versions of SQL Server.  Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myNulls.fmt`, based on the schema of `myNulls`.  To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option also requires the **-f** option.  In addition, for this example, the qualifier **c** is used to specify character data, **t,** is used to specify a comma as a [field terminator](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md), and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myNulls format nul -c -f D:\BCP\myNulls.fmt -t, -T

REM Review file
Notepad D:\BCP\myNulls.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed.  Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

 For more information about creating format files, see [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md).  
  
## Keep Nulls or Use Default Values During Bulk Import<a name="import_data"></a>
The examples below use the database, datafile, and format files created above.

### **Using [bcp](../../tools/bcp-utility.md) and Keeping Null Values without a Format File**<a name="bcp_null"></a>

**-k** switch.  At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -c -t, -T -k

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```
  
### **Using [bcp](../../tools/bcp-utility.md) and Keeping Null Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bcp_null_fmt"></a>
**-k** and **-f** switches. At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -f D:\BCP\myNulls.fmt -T -k

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```

### **Using [bcp](../../tools/bcp-utility.md) and Using Default Values without a Format File**<a name="bcp_default"></a>
At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -c -t, -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```
  
### **Using [bcp](../../tools/bcp-utility.md) and Using Default Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bcp_default_fmt"></a>
**-f** switch.  At a command prompt, enter the following command:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNulls;"

REM Import data
bcp TestDatabase.dbo.myNulls IN D:\BCP\myNulls.bcp -f D:\BCP\myNulls.fmt -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNulls;"
```

### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Keeping Null Values without a Format File**<a name="bulk_null"></a>
**KEEPNULLS** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

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

### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Keeping Null Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bulk_null_fmt"></a>
**KEEPNULLS** and the **FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

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

### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Using Default Values without a Format File**<a name="bulk_default"></a>
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

### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Using Default Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bulk_default_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

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

### **Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and Keeping Null Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="openrowset__null_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

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

### **Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and Using Default Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="openrowset__default_fmt"></a>
**KEEPDEFAULTS** table hint and **FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

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

  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)  
  
-   [Prepare Data for Bulk Export or Import &#40;SQL Server&#41;](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)  
  
 **To use a format file**  
  
-   [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md)  
  
-   [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)  
  
-   [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
-   [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
-   [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)  
  
 **To use data formats for bulk import or bulk export**  
  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
 **To specify data formats for compatibility when using bcp**  
  
-   [Specify Field and Row Terminators &#40;SQL Server&#41;](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)  
  
-   [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  
  
-   [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-file-storage-type-by-using-bcp-sql-server.md)  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md)  
  
  
