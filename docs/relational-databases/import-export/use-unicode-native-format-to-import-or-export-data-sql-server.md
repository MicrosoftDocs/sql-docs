---
title: "Use Unicode Native Format to Import or Export Data (SQL Server)"
description: Use Unicode native format for bulk transfer of data between instances of SQL Server, which eliminates conversion of data types to and from character format.
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/30/2016"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
helpviewer_keywords:
  - "Unicode [SQL Server], bulk importing and exporting"
  - "data formats [SQL Server], Unicode native"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use Unicode Native Format to Import or Export Data (SQL Server)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Unicode native format is helpful when information must be copied from one [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation to another. The use of native format for noncharacter data saves time, eliminating unnecessary conversion of data types to and from character format. The use of Unicode character format for all character data prevents loss of any extended characters during bulk transfer of data between servers using different code pages. A data file in Unicode native format can be read by any bulk-import method.  
  
 Unicode native format is recommended for the bulk transfer of data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended or DBCS characters. For noncharacter data, Unicode native format uses native (database) data types. For character data, such as [char](../../t-sql/data-types/char-and-varchar-transact-sql.md), [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md), [nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), [text](../../t-sql/data-types/ntext-text-and-image-transact-sql.md), [varchar(max)](../../t-sql/data-types/char-and-varchar-transact-sql.md), [nvarchar(max)](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), and [ntext](../../t-sql/data-types/ntext-text-and-image-transact-sql.md), the Unicode native format uses Unicode character data format.  
  
 The [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) data that is stored as a SQLVARIANT in a Unicode native-format data file operates in the same manner as it does in a native-format data file, except that [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) and [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) values are converted to [nchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md) and [nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md), which doubles the amount of storage required for the affected columns. The original metadata is preserved, and the values are converted back to their original [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) and [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) data type when bulk imported into a table column.  
 
 |In this Topic:|
|---|
|[Command Options for Unicode Native Format](#command_options)|
|[Example Test Conditions](#etc)<br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Non-XML Format File](#nonxml_format_file)|
|[Examples](#examples)<br />&emsp;&#9679;&emsp;[Using bcp and Unicode Native Format to Export Data](#bcp_widenative_export)<br />&emsp;&#9679;&emsp;[Using bcp and Unicode Native Format to Import Data without a Format File](#bcp_widenative_import)<br />&emsp;&#9679;&emsp;[Using bcp and Unicode Native Format to Import Data with a Non-XML Format File](#bcp_widenative_import_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Unicode Native Format without a Format File](#bulk_widenative)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Unicode Native Format with a Non-XML Format File](#bulk_widenative_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET and Unicode Native Format with a Non-XML Format File](#openrowset_widenative_fmt)|
|[Related Tasks](#RelatedTasks)<p>                                                                                                                                                                                                                  </p>|
  
## Command Options for Unicode Native Format<a name="command_options"></a>  
You can import Unicode native format data into a table using [bcp](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md).  For a [bcp](../../tools/bcp-utility.md) command or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, you can specify the data format in the statement.  For an [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement, you must specify the data format in a format file.  
  
Unicode native format is supported by the following command options:  
  
|Command|Option|Description|  
|-------------|------------|-----------------|  
|bcp|**-N**|Causes the **bcp** utility to use the Unicode native format, which uses native (database) data types for all noncharacter data and Unicode character data format for all character (**char**, **nchar**, **varchar**, **nvarchar**, **text**, and **ntext**) data.|  
|BULK INSERT|DATAFILETYPE **='widenative'**|Uses Unicode native format when bulk importing data.|  
|OPENROWSET|N/A|Must use a format file|
    
> [!NOTE]
>  Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).
  
## Example Test Conditions<a name="etc"></a>  
The examples in this topic are based on the table, and format file defined below.

### **Sample Table**<a name="sample_table"></a>
The script below creates a test database, a table named `myWidenative` and populates the table with some initial values.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
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
VALUES 
(1, N'ϴAnthony', N'Grosse', '02-23-1980', 65000.00),
(2, N'❤Alica', N'Fatnowna', '11-14-1963', 45000.00),
(3, N'☎Stella', N'Rossenhain', '03-02-1992', 120000.00);

-- Review Data
SELECT * FROM TestDatabase.dbo.myWidenative;
```

### **Sample Non-XML Format File**<a name="nonxml_format_file"></a>
SQL Server support two types of format file: non-XML format and XML format.  The non-XML format is the original format that is supported by earlier versions of SQL Server.  Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myWidenative.fmt`, based on the schema of `myWidenative`.  To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option also requires the **-f** option.  In addition, for this example, the qualifier **c** is used to specify character data, and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following commands:

```
bcp TestDatabase.dbo.myWidenative format nul -f D:\BCP\myWidenative.fmt -T -N

REM Review file
Notepad D:\BCP\myWidenative.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed.  Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

## Examples<a name="examples"></a>
The examples below use the database, and format files created above.

### **Using bcp and Unicode Native Format to Export Data**<a name="bcp_widenative_export"></a>
**-N** switch and **OUT** command.  Note: the data file created in this example will be used in all subsequent examples.  At a command prompt, enter the following commands:
```
bcp TestDatabase.dbo.myWidenative OUT D:\BCP\myWidenative.bcp -T -N

REM Review results
NOTEPAD D:\BCP\myWidenative.bcp
```

### **Using bcp and Unicode Native Format to Import Data without a Format File**<a name="bcp_widenative_import"></a>
**-N** switch and **IN** command.  At a command prompt, enter the following commands:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidenative;"

REM Import data
bcp TestDatabase.dbo.myWidenative IN D:\BCP\myWidenative.bcp -T -N

REM Review results is SSMS
```

### **Using bcp and Unicode Native Format to Import Data with a Non-XML Format File**<a name="bcp_widenative_import_fmt"></a>
**-N** and **-f** switches and **IN** command.  At a command prompt, enter the following commands:
```
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myWidenative;"

REM Import data
bcp TestDatabase.dbo.myWidenative IN D:\BCP\myWidenative.bcp -f D:\BCP\myWidenative.fmt -T -N

REM Review results is SSMS
```

### **Using BULK INSERT and Unicode Native Format without a Format File**<a name="bulk_widenative"></a>
**DATAFILETYPE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
TRUNCATE TABLE TestDatabase.dbo.myWidenative; -- for testing
BULK INSERT TestDatabase.dbo.myWidenative
	FROM 'D:\BCP\myWidenative.bcp'
	WITH (
		DATAFILETYPE = 'widenative'
		);

-- review results
SELECT * FROM TestDatabase.dbo.myWidenative;
```

### **Using BULK INSERT and Unicode Native Format with a Non-XML Format File**<a name="bulk_widenative_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
TRUNCATE TABLE TestDatabase.dbo.myWidenative; -- for testing
BULK INSERT TestDatabase.dbo.myWidenative
   FROM 'D:\BCP\myWidenative.bcp'
   WITH (
		FORMATFILE = 'D:\BCP\myWidenative.fmt'
		);

-- review results
SELECT * FROM TestDatabase.dbo.myWidenative;
```

### **Using OPENROWSET and Unicode Native Format with a Non-XML Format File**<a name="openrowset_widenative_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):
```sql
TRUNCATE TABLE TestDatabase.dbo.myWidenative;  -- for testing
INSERT INTO TestDatabase.dbo.myWidenative
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myWidenative.bcp', 
		FORMATFILE = 'D:\BCP\myWidenative.fmt'  
		) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myWidenative;
```

## Related Tasks<a name="RelatedTasks"></a>
To use data formats for bulk import or bulk export  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
  
  
