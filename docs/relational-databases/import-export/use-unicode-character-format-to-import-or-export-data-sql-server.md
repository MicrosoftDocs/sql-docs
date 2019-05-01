---
title: "Use Unicode Character Format to Import or Export Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/30/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "data formats [SQL Server], Unicode character"
  - "Unicode [SQL Server], bulk importing and exporting"
ms.assetid: 74342a11-c1c0-4746-b482-7f3537744a70
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use Unicode Character Format to Import or Export Data (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Unicode character format is recommended for bulk transfer of data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended/DBCS characters. The Unicode character data format allows data to be exported from a server by using a code page that differs from the code page used by the client that is performing the operation. In such cases, use of Unicode character format has the following advantages:  
  
* If the source and destination data are Unicode data types, use of Unicode character format preserves all of the character data.  
  
* If the source and destination data are not Unicode data types, use of Unicode character format minimizes the loss of extended characters in the source data that cannot be represented at the destination.

|In this Topic:|
|---|
|[Considerations for Using Unicode Character Format](#considerations)|
|[Special Considerations for Using Unicode Character Format, bcp, and a Format File](#special_considerations)|
|[Command Options for Unicode Character Format](#command_options)|
|[Example Test Conditions](#etc)<br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Non-XML Format File](#nonxml_format_file)|
|[Examples](#examples)<br />&emsp;&#9679;&emsp;[Using bcp and Unicode Character Format to Export Data](#bcp_widechar_export)<br />&emsp;&#9679;&emsp;[Using bcp and Unicode Character Format to Import Data without a Format File](#bcp_widechar_import)<br />&emsp;&#9679;&emsp;[Using bcp and Unicode Character Format to Import Data with a Non-XML Format File](#bcp_widechar_import_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Unicode Character Format without a Format File](#bulk_widechar)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Unicode Character Format with a Non-XML Format File](#bulk_widechar_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET and Unicode Character Format with a Non-XML Format File](#openrowset_widechar_fmt)|
|[Related Tasks](#RelatedTasks)<p>                                                                                                                                                                                                                  </p>|
 
## Considerations for Using Unicode Character Format<a name="considerations"></a>
When using Unicode character format, consider the following:  

* By default, the [bcp utility](../../tools/bcp-utility.md) separates the character-data fields with the tab character and terminates the records with the newline character.  For information about how to specify alternative terminators, see [Specify Field and Row Terminators &#40;SQL Server&#41;](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).

* The [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) data that is stored in a Unicode character-format data file operates in the same way it operates in a character-format data file, except that the data is stored as [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md) instead of [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) data. For more information about character format, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  

## Special Considerations for Using Unicode Character Format, bcp, and a Format File<a name="special_considerations"></a>
Unicode character format data files follow the conventions for Unicode files.  The first two bytes of the file are hexadecimal numbers, 0xFFFE.  These bytes serve as byte-order marks (BOM), specifying whether the high-order byte is stored first or last in the file.  The [bcp Utility](../../tools/bcp-utility.md) may misinterpret the BOM and cause part of your import process to fail; you may receive an error message similar as follows:
```
Starting copy...
SQLState = 22005, NativeError = 0
Error = [Microsoft][ODBC Driver 13 for SQL Server]Invalid character value for cast specification
```

The BOM may be misinterpreted under the following conditions:
* The [bcp Utility](../../tools/bcp-utility.md) is used and the **-w** switch is used to indicate Unicode character

* A format file is used

* The first field in the data file is non-character

Consider whether any of the following workarounds may be available for your *specific* situation:
* Don't use a format file.  An example of this workaround is provided below, see [Using bcp and Unicode Character Format to Import Data without a Format File](#bcp_widechar_import),

* Use the **-c** switch instead of **-w**,

* Re-export the data using a native format,

* Use [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md).  Examples of these workaround are provided below, see [Using BULK INSERT and Unicode Character Format with a Non-XML Format File](#bulk_widechar_fmt) and [Using OPENROWSET and Unicode Character Format with a Non-XML Format File](#openrowset_widechar_fmt),
 
* Manually insert first record in destination table and then use **-F 2** switch to have import start on second record,

* Manually insert dummy first record in data file and then use **-F 2** switch to have import start on second record.  An example of this workaround is provided below, see [Using bcp and Unicode Character Format to Import Data with a Non-XML Format File](#bcp_widechar_import_fmt),

* Use a staging table where the first column is a character data type, or

* Re-export the data and change the data field order so that the first data field will be character.  Then use a format file to remap the data field to the actual order in the table.  For an example, see [Use a Format File to Map Table Columns to Data-File Fields (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md).
  
## Command Options for Unicode Character Format<a name="command_options"></a>  
You can import Unicode character format data into a table using [bcp](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md).  For a [bcp](../../tools/bcp-utility.md) command or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, you can specify the data format in the statement.  For an [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement, you must specify the data format in a format file.  
  
Unicode character format is supported by the following command options:  
  
|Command|Option|Description|  
|-------------|------------|-----------------|  
|bcp|**-w**|Uses the Unicode character format.|  
|BULK INSERT|DATAFILETYPE **='widechar'**|Uses Unicode character format when bulk importing data.|  
|OPENROWSET|N/A|Must use a format file|
  
> [!NOTE]
>  Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).
  
## Example Test Conditions<a name="etc"></a>  
The examples in this topic are based on the table, and format file defined below.

### **Sample Table**<a name="sample_table"></a>
The script below creates a test database, a table named `myWidechar` and populates the table with some initial values.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE dbo.myWidechar ( 
	PersonID smallint NOT NULL,
	FirstName nvarchar(25) NOT NULL,
	LastName nvarchar(30) NOT NULL,
	BirthDate date,
	AnnualSalary money
);

-- Populate table
INSERT TestDatabase.dbo.myWidechar
VALUES 
(1, N'ϴAnthony', N'Grosse', '02-23-1980', 65000.00),
(2, N'❤Alica', N'Fatnowna', '11-14-1963', 45000.00),
(3, N'☎Stella', N'Rossenhain', '03-02-1992', 120000.00);

-- Review Data
SELECT * FROM TestDatabase.dbo.myWidechar;
```

### **Sample Non-XML Format File**<a name="nonxml_format_file"></a>
SQL Server support two types of format file: non-XML format and XML format.  The non-XML format is the original format that is supported by earlier versions of SQL Server.  Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myWidechar.fmt`, based on the schema of `myWidechar`.  To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option also requires the **-f** option.  In addition, for this example, the qualifier **c** is used to specify character data, and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following commands:

```
bcp TestDatabase.dbo.myWidechar format nul -f D:\BCP\myWidechar.fmt -T -w

REM Review file
Notepad D:\BCP\myWidechar.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed.  Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`


## Examples<a name="examples"></a>
The examples below use the database, and format files created above.

### **Using bcp and Unicode Character Format to Export Data**<a name="bcp_widechar_export"></a>
**-w** switch and **OUT** command.  Note: the data file created in this example will be used in all subsequent examples.  At a command prompt, enter the following commands:
```
bcp TestDatabase.dbo.myWidechar OUT D:\BCP\myWidechar.bcp -T -w

REM Review results
NOTEPAD D:\BCP\myWidechar.bcp
```

### **Using bcp and Unicode Character Format to Import Data without a Format File**<a name="bcp_widechar_import"></a>
**-w** switch and **IN** command.  At a command prompt, enter the following commands:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidechar;"

REM Import data
bcp TestDatabase.dbo.myWidechar IN D:\BCP\myWidechar.bcp -T -w

REM Review results is SSMS
```

### **Using bcp and Unicode Character Format to Import Data with a Non-XML Format File**<a name="bcp_widechar_import_fmt"></a>
**-w** and **-f** switches and **IN** command.  A workaround will need to be used since this example involves bcp, a format file, Unicode character, and the first data field in the data file is non-character.  See [Special Considerations for Using Unicode Character Format, bcp, and a Format File](#special_considerations), above.  The data file `myWidechar.bcp` will be altered by adding an additional record as a "dummy" record which will  then be skipped with the `-F 2` switch.

At a command prompt, enter the following commands and follow the modification steps:
```
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
  
### **Using BULK INSERT and Unicode Character Format without a Format File**<a name="bulk_widechar"></a>
**DATAFILETYPE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
TRUNCATE TABLE TestDatabase.dbo.myWidechar; -- for testing
BULK INSERT TestDatabase.dbo.myWidechar
	FROM 'D:\BCP\myWidechar.bcp'
	WITH (
		DATAFILETYPE = 'widechar'
		);

-- review results
SELECT * FROM TestDatabase.dbo.myWidechar;
```
  
### **Using BULK INSERT and Unicode Character Format with a Non-XML Format File**<a name="bulk_widechar_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
TRUNCATE TABLE TestDatabase.dbo.myWidechar; -- for testing
BULK INSERT TestDatabase.dbo.myWidechar
   FROM 'D:\BCP\myWidechar.bcp'
   WITH (
		FORMATFILE = 'D:\BCP\myWidechar.fmt'
		);

-- review results
SELECT * FROM TestDatabase.dbo.myWidechar;
```
  
### **Using OPENROWSET and Unicode Character Format with a Non-XML Format File**<a name="openrowset_widechar_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
TRUNCATE TABLE TestDatabase.dbo.myWidechar;  -- for testing
INSERT INTO TestDatabase.dbo.myWidechar
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myWidechar.bcp', 
		FORMATFILE = 'D:\BCP\myWidechar.fmt'  
		) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myWidechar;
```
 
  
## Related Tasks<a name="RelatedTasks"></a>
To use data formats for bulk import or bulk export  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)  
  
  
