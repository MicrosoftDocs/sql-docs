---
title: "Keep Identity Values When Bulk Importing Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/21/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "identity values [SQL Server], bulk imports"
  - "data formats [SQL Server], identity values"
  - "bulk importing [SQL Server], identity values"
ms.assetid: 45894a3f-2d8a-4edd-9568-afa7d0d3061f
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Keep Identity Values When Bulk Importing Data (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Data files that contain identity values can be bulk imported into an instance of Microsoft SQL Server.  By default, the values for the identity column in the data file that is imported are ignored and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns unique values automatically.  The unique values are based on the seed and increment values that are specified during table creation.

If the data file does not contain values for the identifier column in the table, use a format file to specify that the identifier column in the table should be skipped when importing data.  See [Use a Format File to Skip a Table Column (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md) for additional information.

|Outline|
|---|
|[Keep Identity Values](#keep_identity)<br />[Example Test Conditions](#etc)<br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Data File](#sample_data_file)<br />&emsp;&#9679;&emsp;[Sample Non-XML Format File](#nonxml_format_file)<br />[Examples](#examples)<br />&emsp;&#9679;&emsp;[Using bcp and Keeping Identity Values without a Format File](#bcp_identity)<br />&emsp;&#9679;&emsp;[Using bcp and Keeping Identity Values with a Non-XML Format File](#bcp_identity_fmt)<br />&emsp;&#9679;&emsp;[Using bcp and Generated Identity Values without a Format File](#bcp_default)<br />&emsp;&#9679;&emsp;[Using bcp and Generated Identity Values with a Non-XML Format File](#bcp_default_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Keeping Identity Values without a Format File](#bulk_identity)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Keeping Identity Values with a Non-XML Format File](#bulk_identity_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Generated Identity Values without a Format File](#bulk_default)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Generated Identity Values with a Non-XML Format File](#bulk_default_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET and Keeping Identity Values with a Non-XML Format File](#openrowset_identity_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET and Generated Identity Values with a Non-XML Format File](#openrowset_default_fmt)<br /><p>                                                                                                                                                                                                                  </p>|

## Keep Identity Values <a name="keep_identity"></a>  
To prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from assigning identity values while bulk importing data rows into a table, use the appropriate keep-identity command qualifier.  When you specify a keep-identity qualifier, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the identity values in the data file.  These qualifiers are as follows:

|Command|Keep-identity qualifier|Qualifier type|  
|-------------|------------------------------|--------------------|  
|bcp|-E|Switch|  
|BULK INSERT|KEEPIDENTITY|Argument|  
|INSERT ... SELECT * FROM OPENROWSET(BULK...)|KEEPIDENTITY|Table hint|  
   
 For more information, see [bcp Utility](../../tools/bcp-utility.md), [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md), [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md), [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md), [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md), and [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  

> [!NOTE]
>  To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).
 
## Example Test Conditions<a name="etc"></a>  
The examples in this topic are based on the table, data file, and format file defined below.

### **Sample Table**<a name="sample_table"></a>
The script below creates a test database and a table named `myIdentity`.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
 
### **Sample Data File**<a name="sample_data_file"></a>
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

### **Sample Non-XML Format File**<a name="nonxml_format_file"></a>
SQL Server support two types of format file: non-XML format and XML format.  The non-XML format is the original format that is supported by earlier versions of SQL Server.  Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myIdentity.fmt`, based on the schema of `myIdentity`.  To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option also requires the **-f** option.  In addition, for this example, the qualifier **c** is used to specify character data, **t,** is used to specify a comma as a [field terminator](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md), and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:
  
```
bcp TestDatabase.dbo.myIdentity format nul -c -f D:\BCP\myIdentity.fmt -t, -T

REM Review file
Notepad D:\BCP\myIdentity.fmt
```
 
> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed.  Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

## Examples<a name="examples"></a>
The examples below use the database, datafile, and format files created above.
  
### **Using [bcp](../../tools/bcp-utility.md) and Keeping Identity Values without a Format File**<a name="bcp_identity"></a>
**-E** switch.  At a command prompt, enter the following command:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -T -c -t, -E

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```

### **Using [bcp](../../tools/bcp-utility.md) and Keeping Identity Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bcp_identity_fmt"></a>
**-E** and **-f** switches.  At a command prompt, enter the following command:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -f D:\BCP\myIdentity.fmt -T -E

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```
 
### **Using [bcp](../../tools/bcp-utility.md) and Generated Identity Values without a Format File**<a name="bcp_default"></a>
Using defaults.  At a command prompt, enter the following command:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -T -c -t,

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```
  
### **Using [bcp](../../tools/bcp-utility.md) and Generated Identity Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bcp_default_fmt"></a>
Using defaults and **-f** switch.  At a command prompt, enter the following command:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myIdentity;"

REM Import data
bcp TestDatabase.dbo.myIdentity IN D:\BCP\myIdentity.bcp -f D:\BCP\myIdentity.fmt -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myIdentity;"
```
  
### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Keeping Identity Values without a Format File**<a name="bulk_identity"></a>
**KEEPIDENTITY** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
  
### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Keeping Identity Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bulk_identity_fmt"></a>
**KEEPIDENTITY** and the **FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
  
### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Generated Identity Values without a Format File**<a name="bulk_default"></a>
Using defaults.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
  
### **Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and Generated Identity Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="bulk_default_fmt"></a>
Using defaults and **FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
  
### **Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and Keeping Identity Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="openrowset_identity_fmt"></a>
**KEEPIDENTITY** table hint and **FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
 
### **Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and Generated Identity Values with a [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)**<a name="openrowset_default_fmt"></a>
Using defaults and **FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  
  
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
  
1.  [Specify Field and Row Terminators &#40;SQL Server&#41;](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)  
  
2.  [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  
  
3.  [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-file-storage-type-by-using-bcp-sql-server.md)  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md)  
[Format Files for Importing or Exporting Data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)  
  
