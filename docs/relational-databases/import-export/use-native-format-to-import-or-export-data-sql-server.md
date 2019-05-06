---
title: "Use Native Format to Import or Export Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/30/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "native data format [SQL Server]"
  - "data formats [SQL Server], native"
ms.assetid: eb279b2f-0f1f-428f-9b8f-2a7fc495b79f
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use Native Format to Import or Export Data (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Native format is recommended when you bulk transfer data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using a data file that does not contain any extended/double-byte character set (DBCS) characters.  

> [!NOTE]
>  To bulk transfer data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended or DBCS characters, you should use the Unicode native format. For more information, see [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md).

Native format maintains the native data types of a database. Native format is intended for high-speed data transfer of data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables. If you use a format file, the source and target tables do not need to be identical. The data transfer involves two steps:  
  
1.  Bulk exporting the data from a source table into a data file  
  
2.  Bulk importing the data from the data file into the target table  
  
The use of native format between identical tables avoids unnecessary conversion of data types to and from character format, saving time and space. To achieve the optimum transfer rate, however, few checks are performed regarding data formatting. To prevent problems with the loaded data, see the following restrictions list.  

|In this Topic:|
|---|
|[Restrictions](#restrictions)|
|[How bcp Handles Data in Native Format](#considerations)|
|[Command Options for Native Format](#command_options)|
|[Example Test Conditions](#etc)<br /><br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Non-XML Format File](#nonxml_format_file)|
|[Examples](#examples)<br />&emsp;&#9679;&emsp;[Using bcp and Native Format to Export Data](#bcp_native_export)<br />&emsp;&#9679;&emsp;[Using bcp and Native Format to Import Data without a Format File](#bcp_native_import)<br />&emsp;&#9679;&emsp;[Using bcp and Native Format to Import Data with a Non-XML Format File](#bcp_native_import_fmt)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Native Format without a Format File](#bulk_native)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Native Format with a Non-XML Format File](#bulk_native_fmt)<br />&emsp;&#9679;&emsp;[Using OPENROWSET and Native Format with a Non-XML Format File](#openrowset_native_fmt)|
|[Related Tasks](#RelatedTasks)<p>                                                                                                                                                                                                                  </p>|

## Restrictions<a name="restrictions"></a>  
To import data in native format successfully, ensure that:  
  
-   The data file is in native format.  
  
-   Either the target table must be compatible with the data file (having the correct number of columns, data type, length, NULL status, and so forth), or you must use a format file to map each field to its corresponding columns.  
  
    > [!NOTE]
    >  If you import data from a file that is mismatched with the target table, the import operation might succeed but the data values inserted into the target table are likely to be incorrect. This is because the data from the file is interpreted by using the format of the target table. Therefore, any mismatch results in the insertion of incorrect values. However, under no circumstances can such a mismatch cause logical or physical inconsistencies in the database.  
  
     For information on using format files, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).  
  
 A successful import will not corrupt the target table.  
  
## How bcp Handles Data in Native Format<a name="considerations"></a>
 This section discusses special considerations for how the **bcp** utility exports and imports data in native format.  
  
-   Noncharacter data  
  
     The [bcp utility](../../tools/bcp-utility.md) uses the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal binary data format to write noncharacter data from a table to a data file.  
  
-   [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) or [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) data  
  
     At the beginning of each [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) or [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) field, [bcp](../../tools/bcp-utility.md) adds the prefix length.  
  
    > [!IMPORTANT]
    >  When native mode is used, by default, the [bcp utility](../../tools/bcp-utility.md) converts characters from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to OEM characters before it copies them to a data file. The [bcp utility](../../tools/bcp-utility.md) converts characters from a data file to ANSI characters before it bulk imports them into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. During these conversions, extended character data can be lost. For extended characters, either use Unicode native format or specify a code page.
  
-   [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) data  
  
     If [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) data is stored as a SQLVARIANT in a native-format data file, the data maintains all of its characteristics. The metadata that records the data type of each data value is stored along with the data value. This metadata is used to re-create the data value with the same data type in a destination [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) column.  
  
     If the data type of the destination column is not [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md), each data value is converted to the data type of the destination column, following the normal rules of implicit data conversion. If an error occurs during data conversion, the current batch is rolled back. Any [char](../../t-sql/data-types/char-and-varchar-transact-sql.md) and [varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) values that are transferred between [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md) columns may have code page conversion issues.  
  
     For more information about data conversion, see [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md).  
  
## Command Options for Native Format<a name="command_options"></a>  
You can import native format data into a table using [bcp](../../tools/bcp-utility.md), [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md).  For a [bcp](../../tools/bcp-utility.md) command or [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, you can specify the data format in the statement.  For an [INSERT ... SELECT * FROM OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement, you must specify the data format in a format file.  

Native format is supported by the following command options:  

|Command|Option|Description|  
|-------------|------------|-----------------|  
|bcp|**-n**|Causes the bcp utility to use the native data types of the data.*|  
|BULK INSERT|DATAFILETYPE **='native'**|Uses the native or wide native data types of the data. Note that DATAFILETYPE is not needed if a format file specifies the data types.|  
|OPENROWSET|N/A|Must use a format file|

  
 \*To load native (**-n**) data to a format compatible with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients, use the **-V** switch. For more information, see [Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md).  
  
> [!NOTE]
>  Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).
  

## Example Test Conditions<a name="etc"></a>  
The examples in this topic are based on the table, and format file defined below.

### **Sample Table**<a name="sample_table"></a>
The script below creates a test database, a table named `myNative` and populates the table with some initial values.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE dbo.myNative ( 
   PersonID smallint NOT NULL,
   FirstName varchar(25) NOT NULL,
   LastName varchar(30) NOT NULL,
   BirthDate date,
   AnnualSalary money
   );

-- Populate table
INSERT TestDatabase.dbo.myNative
VALUES 
(1, 'Anthony', 'Grosse', '1980-02-23', 65000.00),
(2, 'Alica', 'Fatnowna', '1963-11-14', 45000.00),
(3, 'Stella', 'Rossenhain', '1992-03-02', 120000.00);

-- Review Data
SELECT * FROM TestDatabase.dbo.myNative;
```

### **Sample Non-XML Format File**<a name="nonxml_format_file"></a>
SQL Server support two types of format file: non-XML format and XML format.  The non-XML format is the original format that is supported by earlier versions of SQL Server.  Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myNative.fmt`, based on the schema of `myNative`.  To use a [bcp](../../tools/bcp-utility.md) command to create a format file, specify the **format** argument and use **nul** instead of a data-file path.  The format option also requires the **-f** option.  In addition, for this example, the qualifier **c** is used to specify character data, and **T** is used to specify a trusted connection using integrated security.  At a command prompt, enter the following commands:

```cmd
bcp TestDatabase.dbo.myNative format nul -f D:\BCP\myNative.fmt -T -n 

REM Review file
Notepad D:\BCP\myNative.fmt
```

> [!IMPORTANT]
> Ensure your non-XML format file ends with a carriage return\line feed.  Otherwise you will likely receive the following error message:
> 
> `SQLState = S1000, NativeError = 0`  
> `Error = [Microsoft][ODBC Driver 13 for SQL Server]I/O error while reading BCP format file`

## Examples<a name="examples"></a>
The examples below use the database, and format files created above.

### **Using bcp and Native Format to Export Data**<a name="bcp_native_export"></a>
**-n** switch and **OUT** command.  Note: the data file created in this example will be used in all subsequent examples.  At a command prompt, enter the following commands:

```cmd
bcp TestDatabase.dbo.myNative OUT D:\BCP\myNative.bcp -T -n

REM Review results
NOTEPAD D:\BCP\myNative.bcp
```

### **Using bcp and Native Format to Import Data without a Format File**<a name="bcp_native_import"></a>
**-n** switch and **IN** command.  At a command prompt, enter the following commands:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNative;"

REM Import data
bcp TestDatabase.dbo.myNative IN D:\BCP\myNative.bcp -T -n

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNative;"
```

### **Using bcp and Native Format to Import Data with a Non-XML Format File**<a name="bcp_native_import_fmt"></a>
**-n** and **-f** switches and **IN** command.  At a command prompt, enter the following commands:

```cmd
REM Truncate table (for testing)
SQLCMD -Q "TRUNCATE TABLE TestDatabase.dbo.myNative;"

REM Import data
bcp TestDatabase.dbo.myNative IN D:\BCP\myNative.bcp -f D:\BCP\myNative.fmt -T

REM Review results
SQLCMD -Q "SELECT * FROM TestDatabase.dbo.myNative;"
```

### **Using BULK INSERT and Native Format without a Format File**<a name="bulk_native"></a>
**DATAFILETYPE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myNative; -- for testing
BULK INSERT TestDatabase.dbo.myNative
	FROM 'D:\BCP\myNative.bcp'
	WITH (
		DATAFILETYPE = 'native'
		);

-- review results
SELECT * FROM TestDatabase.dbo.myNative;
```

### **Using BULK INSERT and Native Format with a Non-XML Format File**<a name="bulk_native_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myNative; -- for testing
BULK INSERT TestDatabase.dbo.myNative
   FROM 'D:\BCP\myNative.bcp'
   WITH (
		FORMATFILE = 'D:\BCP\myNative.fmt'
		);

-- review results
SELECT * FROM TestDatabase.dbo.myNative;
```

### **Using OPENROWSET and Native Format with a Non-XML Format File**<a name="openrowset_native_fmt"></a>
**FORMATFILE** argument.  Execute the following Transact-SQL in Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

```sql
TRUNCATE TABLE TestDatabase.dbo.myNative;  -- for testing
INSERT INTO TestDatabase.dbo.myNative
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myNative.bcp', 
		FORMATFILE = 'D:\BCP\myNative.fmt'  
		) AS t1;

-- review results
SELECT * FROM TestDatabase.dbo.myNative;
```
  
## Related Tasks<a name="RelatedTasks"></a>
To use data formats for bulk import or bulk export 
  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [sql_variant &#40;Transact-SQL&#41;](../../t-sql/data-types/sql-variant-transact-sql.md)   
 [Import Native and Character Format Data from Earlier Versions of SQL Server](../../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
  
