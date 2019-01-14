---
title: "Use a Format File to Skip a Data Field (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/19/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "format files [SQL Server], skipping data fields"
  - "skipping data fields when importing"
ms.assetid: 6a76517e-983b-47a1-8f02-661b99859a8b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use a Format File to Skip a Data Field (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
A data file can contain more fields than the number of columns in the table. This topic describes modifying both non-XML and XML format files to accommodate a data file with more fields by mapping the table columns to the corresponding data fields and ignoring the extra fields.  Please review [Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md) for additional information.

|Outline|
|---|
|[Example Test Conditions](#etc)<br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Data File](#sample_data_file)<br />[Creating the Format Files](#create_format_file)<br />&emsp;&#9679;&emsp;[Creating a Non-XML Format File](#nonxml_format_file)<br />&emsp;&#9679;&emsp;[Modifying a Non-XML Format File](#modify_nonxml_format_file)<br />&emsp;&#9679;&emsp;[Creating an XML Format File](#xml_format_file)<br />&emsp;&#9679;&emsp;[Modifying an XML Format File](#modify_xml_format_file)<br />[Importing Data with a Format File to skip a Data Field](#import_data)<br />&emsp;&#9679;&emsp;[Using bcp and Non-XML Format File](#bcp_nonxml)<br />&emsp;&#9679;&emsp;[Using bcp and XML Format File](#bcp_xml)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Non-XML Format File](#bulk_nonxml)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and XML Format File](#bulk_xml)<br />&emsp;&#9679;&emsp;[Using OPENROWSET(BULK...) and Non-XML Format File](#openrowset_nonxml)<br />&emsp;&#9679;&emsp;[Using OPENROWSET(BULK...) and XML Format File](#openrowset_xml)<p>                                                                                                                                                                                                                  </p>|
  
> [!NOTE]
>  Either a non-XML or XML format file can be used to bulk import a data file into the table by using a [bcp utility](../../tools/bcp-utility.md) command, [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, or INSERT ... SELECT * FROM [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement. For more information, see [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).

## Example Test Conditions<a name="etc"></a>  
The examples of modified format files in this topic are based on the table and data file defined below.
  
### Sample Table<a name="sample_table"></a>
The script below creates a test database and a table named `myTestSkipField`.  Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
 
```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE myTestSkipField
   (
   PersonID smallint,
   FirstName varchar(25),
   LastName varchar(30)
   );
```
  
### Sample Data File<a name="sample_data_file"></a>
Create an empty file `D:\BCP\myTestSkipField.bcp` and insert the following data: 
```
1,SkipMe,Anthony,Grosse
2,SkipMe,Alica,Fatnowna
3,SkipMe,Stella,Rosenhain
```
  
## Creating the Format Files<a name="create_format_file"></a>
To bulk import data from `myTestSkipField.bcp` into the `myTestSkipField` table, the format file must do the following:
* Map the first data field to the first column, `PersonID`.
* Skip the second data field.
* Map the third data field to the second column, `FirstName`.
* Map the fourth data field to the third column, `LastName`.  

The simplest method to create the format file is by using the [bcp utility](../../tools/bcp-utility.md).  First, create a base format file from the existing table.  Second, modify the base format file to reflect the actual data file.
  
### <a name="nonxml_format_file"></a>Creating a Non-XML Format File 
Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information. The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myTestSkipField.fmt`, based on the schema of `myTestSkipField`.  In addition, the qualifier `c` is used to specify character data , `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:

```
bcp TestDatabase.dbo.myTestSkipField format nul -c -f D:\BCP\myTestSkipField.fmt -t, -T
```

### Modifying the Non-XML Format File <a name="modify_nonxml_format_file"></a>
See [Structure of Non-XML Format Files](../../relational-databases/import-export/non-xml-format-files-sql-server.md#Structure) for terminology. Open `D:\BCP\myTestSkipField.fmt` in Notepad and perform the following modifications:
1) Copy the entire format-file row for `FirstName` and paste it directly after `FirstName` on the next line.
2) Increase the host file field order value by one for the new row and all subsequent rows.
3) Increase the number of columns value to reflect the actual number of fields in the data file.
3) Modify the server column order from `2` to `0` for the second format-file row. 

Compare the changes made:  
**Before**
```
13.0
3
1       SQLCHAR	0       7       ","      1     PersonID	    ""
2       SQLCHAR	0       25      ","      2     FirstName	SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR	0       30      "\r\n"   3     LastName 	SQL_Latin1_General_CP1_CI_AS
```
**After**
```
13.0
4
1       SQLCHAR	0       7       ","      1     PersonID 	""
2       SQLCHAR	0       25      ","      0     FirstName	SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR	0       25      ","      2     FirstName	SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR	0       50      "\r\n"   3     LastName 	SQL_Latin1_General_CP1_CI_AS

```

The modified format file now reflects:
* 4 data fields
* The first data field in `myTestSkipField.bcp` is mapped to the first column, ` myTestSkipField.. PersonID`
* The second data field in `myTestSkipField.bcp` is not mapped to any column.
* The third data field in `myTestSkipField.bcp` is mapped to the second column, `myTestSkipField.. FirstName`
* The fourth data field in `myTestSkipField.bcp` is mapped to the third column, `myTestSkipField.. LastName`

### Creating an XML Format File <a name="xml_format_file"></a>  
Please review [XML Format Files (SQL Server)](../../relational-databases/import-export/xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to create an xml format file, `myTestSkipField.xml`, based on the schema of `myTestSkipField`.  In addition, the qualifier `c` is used to specify character data , `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  The `x` qualifier must be used to generate an XML-based format file.  At a command prompt, enter the following command:

```
bcp TestDatabase.dbo.myTestSkipField format nul -c -x -f D:\BCP\myTestSkipField.xml -t, -T
```

### Modifying the XML Format File <a name="modify_xml_format_file"></a>
See [Schema Syntax for XML Format Files](../../relational-databases/import-export/xml-format-files-sql-server.md#StructureOfXmlFFs) for terminology.  Open `D:\BCP\myTestSkipField.xml` in Notepad and perform the following modifications:
1) Copy the entire second field and paste it directly after the second field on the next line.
2) Increase the "FIELD ID" value by 1 for the new FIELD and for each subsequent FIELD.
3) Increase the "COLUMN SOURCE" value by 1 for `FirstName`, and `LastName` to reflect the revised mapping.

Compare the changes made:  
**Before**
```
\<?xml version="1.0"?>
\<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
 <RECORD>
  \<FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  \<FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="3" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
 </RECORD>
 <ROW>
  \<COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  \<COLUMN SOURCE="2" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  \<COLUMN SOURCE="3" NAME="LastName" xsi:type="SQLVARYCHAR"/>
 </ROW>
</BCPFORMAT>
```

**After**
```
\<?xml version="1.0"?>
\<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
 <RECORD>
  \<FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  \<FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
 </RECORD>
 <ROW>
  \<COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  \<COLUMN SOURCE="3" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  \<COLUMN SOURCE="4" NAME="LastName" xsi:type="SQLVARYCHAR"/>
 </ROW>
</BCPFORMAT>
```

The modified format file now reflects:
* 4 data fields
* FIELD 1 which corresponds to COLUMN 1 is mapped to the first table column, `myTestSkipField.. PersonID`
* FIELD 2 does not correspond to any COLUMN and thus, is not mapped to any table column.
* FIELD 3 which corresponds to COLUMN 3 is mapped to the second table column, `myTestSkipField.. FirstName`
* FIELD 4 which corresponds to COLUMN 4 is mapped to the third table column, `myTestSkipField.. LastName`

## Importing Data with a Format File to skip a Data Field<a name="import_data"></a>
The examples below use the database, datafile, and format files created above.

### Using [bcp](../../tools/bcp-utility.md) and [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)<a name="bcp_nonxml"></a>
At a command prompt, enter the following command:
```
bcp TestDatabase.dbo.myTestSkipField IN D:\BCP\myTestSkipField.bcp -f D:\BCP\myTestSkipField.fmt -T
```

### Using [bcp](../../tools/bcp-utility.md) and [XML Format File](../../relational-databases/import-export/xml-format-files-sql-server.md)<a name="bcp_xml"></a>
At a command prompt, enter the following command:
```
bcp TestDatabase.dbo.myTestSkipField IN D:\BCP\myTestSkipField.bcp -f D:\BCP\myTestSkipField.xml -T
```

### Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)<a name="bulk_nonxml"></a>
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myTestSkipField;
BULK INSERT dbo.myTestSkipField   
   FROM 'D:\BCP\myTestSkipField.bcp'   
   WITH (FORMATFILE = 'D:\BCP\myTestSkipField.fmt');  
GO  

-- review results
SELECT * FROM TestDatabase.dbo.myTestSkipField;
```

### Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [XML Format File](../../relational-databases/import-export/xml-format-files-sql-server.md)<a name="bulk_xml"></a>
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myTestSkipField;
BULK INSERT dbo.myTestSkipField   
   FROM 'D:\BCP\myTestSkipField.bcp'   
   WITH (FORMATFILE = 'D:\BCP\myTestSkipField.xml');  
GO  

-- review results
SELECT * FROM TestDatabase.dbo.myTestSkipField;
```

### Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)<a name="openrowset_nonxml"></a>	
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;
GO

TRUNCATE TABLE myTestSkipField;
INSERT INTO dbo.myTestSkipField
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myTestSkipField.bcp',
		FORMATFILE = 'D:\BCP\myTestSkipField.fmt'
		) AS t1;
GO

-- review results
SELECT * FROM TestDatabase.dbo.myTestSkipField;
```

### Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and [XML Format File](../../relational-databases/import-export/xml-format-files-sql-server.md)<a name="openrowset_xml"></a>
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myTestSkipField;
INSERT INTO dbo.myTestSkipField 
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myTestSkipField.bcp',
		FORMATFILE = 'D:\BCP\myTestSkipField.xml'  
       ) AS t1;
GO

-- review results
SELECT * FROM TestDatabase.dbo.myTestSkipField;
```

  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)   
 [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
  
