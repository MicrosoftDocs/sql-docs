---
title: "Use a Format File to Map Table Columns to Data-File Fields (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/19/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping columns to fields during import [SQL Server]"
  - "format files [SQL Server], mapping columns to fields"
ms.assetid: e7ee4f7e-24c4-4eb7-84d2-41e57ccc1ef1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use a Format File to Map Table Columns to Data-File Fields (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
A data file can contain fields arranged in a different order from the corresponding columns in the table. This topic presents both non-XML and XML format files that have been modified to accommodate a data file whose fields are arranged in a different order from the table columns. The modified format file maps the data fields to their corresponding table columns.  Please review [Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md) for additional information.

|Outline|
|---|
|[Example Test Conditions](#etc)<br />&emsp;&#9679;&emsp;[Sample Table](#sample_table)<br />&emsp;&#9679;&emsp;[Sample Data File](#sample_data_file)<br />[Creating the Format Files](#create_format_file)<br />&emsp;&#9679;&emsp;[Creating a Non-XML Format File](#nonxml_format_file)<br />&emsp;&#9679;&emsp;[Modifying the Non-XML Format File](#modify_nonxml_format_file)<br />&emsp;&#9679;&emsp;[Creating an XML Format File](#xml_format_file)<br />&emsp;&#9679;&emsp;[Modifying the XML Format File](#modify_xml_format_file)<br />[Importing Data with a Format File to Map Table Columns to Data-File Field](#import_data)<br />&emsp;&#9679;&emsp;[Using bcp and Non-XML Format File](#bcp_nonxml)<br />&emsp;&#9679;&emsp;[Using bcp and XML Format File](#bcp_xml)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and Non-XML Format File](#bulk_nonxml)<br />&emsp;&#9679;&emsp;[Using BULK INSERT and XML Format File](#bulk_xml)<br />&emsp;&#9679;&emsp;[Using OPENROWSET(BULK...) and Non-XML Format File](#openrowset_nonxml)<br />&emsp;&#9679;&emsp;[Using OPENROWSET(BULK...) and XML Format File](#openrowset_xml)|

> [!NOTE]  
>  Either a non-XML or XML format file can be used to bulk import a data file into the table by using a [bcp utility](../../tools/bcp-utility.md) command, [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, or INSERT ... SELECT * FROM [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement. For more information, see [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).  

## Example Test Conditions<a name="etc"></a>  
The examples of modified format files in this topic are based on the table and data file defined below.

### Sample Table<a name="sample_table"></a>
The script below creates a test database and a table named `myRemap`.  Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;
CREATE TABLE myRemap
   (
   PersonID smallint,
   FirstName varchar(25),
   LastName varchar(30),
   Gender char(1)
   );
```

### Sample Data File<a name="sample_data_file"></a>
The data below presents `FirstName` and `LastName` in the reverse order as presented in the table `myRemap`.  Using Notepad, create an empty file `D:\BCP\myRemap.bcp` and insert the following data:
```
1,Grosse,Anthony,M
2,Fatnowna,Alica,F
3,Rosenhain,Stella,F
```

## Creating the Format Files<a name="create_format_file"></a>
To bulk import data from `myRemap.bcp` into the `myRemap` table, the format file must do the following:
* Map the first data field to the first column, `PersonID`.
* Map the second data field to the third column, `LastName`.
* Map the third data field to the second column, `FirstName`.
* Map the fourth data field to the fourth column, `Gender`.

The simplest method to create the format file is by using the [bcp utility](../../tools/bcp-utility.md).  First, create a base format file from the existing table.  Second, modify the base format file to reflect the actual data file.

### Creating a Non-XML Format File<a name="nonxml_format_file"></a>
Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information. The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myRemap.fmt`, based on the schema of `myRemap`.  In addition, the qualifier `c` is used to specify character data, `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:
```
bcp TestDatabase.dbo.myRemap format nul -c -f D:\BCP\myRemap.fmt -t, -T
```
### Modifying the Non-XML Format File <a name="modify_nonxml_format_file"></a>
See [Structure of Non-XML Format Files](../../relational-databases/import-export/non-xml-format-files-sql-server.md#Structure) for terminology.  Open `D:\BCP\myRemap.fmt` in Notepad and perform the following modifications:
1.  Re-arrange the order of the format-file rows so that the rows are in the same order as the data in `myRemap.bcp`.
2.  Ensure the host file field order values are sequential.
3.  Ensure there is a carriage return after the last format-file row.

Compare the changes:	 
**Before**
```
13.0
4
1       SQLCHAR	0       7       ","      1     PersonID               ""
2       SQLCHAR	0       25      ","      2     FirstName              SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR	0       30      ","      3     LastName               SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR	0       1       "\r\n"   4     Gender                 SQL_Latin1_General_CP1_CI_AS

```
**After**
```
13.0
4
1       SQLCHAR	0       7       ","      1     PersonID               ""
2       SQLCHAR	0       30      ","      3     LastName               SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR	0       25      ","      2     FirstName              SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR	0       1       "\r\n"   4     Gender                 SQL_Latin1_General_CP1_CI_AS

```
The modified format file now reflects:
* The first data field in `myRemap.bcp` is mapped to the first column, ` myRemap.. PersonID`
* The second data field in `myRemap.bcp` is mapped to the third column, `myRemap.. LastName`
* The third data field in `myRemap.bcp` is mapped to the second column, `myRemap.. FirstName`
* The fourth data field in `myRemap.bcp` is mapped to the fourth column, ` myRemap.. Gender`

### Creating an XML Format File <a name="xml_format_file"></a>  
Please review [XML Format Files (SQL Server)](../../relational-databases/import-export/xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to create an xml format file, `myRemap.xml`, based on the schema of `myRemap`.  In addition, the qualifier `c` is used to specify character data, `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  The `x` qualifier must be used to generate an XML-based format file.  At a command prompt, enter the following command:
```
bcp TestDatabase.dbo.myRemap format nul -c -x -f D:\BCP\myRemap.xml -t, -T
```
### Modifying the XML Format File <a name="modify_xml_format_file"></a>
See [Schema Syntax for XML Format Files](../../relational-databases/import-export/xml-format-files-sql-server.md#StructureOfXmlFFs) for terminology.  Open `D:\BCP\myRemap.xml` in Notepad and perform the following modifications:
1. The order in which the \<FIELD> elements are declared in the format file is the order in which those fields appear in the data file, thus reverse the order for the \<FIELD> elements with ID attributes 2 and 3.
2. Ensure the \<FIELD> ID attribute values are sequential.
3. The order of the \<COLUMN> elements in the \<ROW> element defines the order in which they are returned by the bulk operation.  The XML format file assigns each \<COLUMN> element a local name that has no relationship to the column in the target table of a bulk import operation.  The order of the \<COLUMN> elements is independent of the order of \<FIELD> elements in a \<RECORD> definition.  Each \<COLUMN> element corresponds to a \<FIELD> element (whose ID is specified in the SOURCE attribute of the \<COLUMN> element).  Thus, the values for \<COLUMN> SOURCE are the only attributes that require revision.  Reverse the order for \<COLUMN> SOURCE attributes 2 and 3.

Compare the changes  
**Before**
```
\<?xml version="1.0"?>
\<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
 <RECORD>
  \<FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  \<FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="1" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
 </RECORD>
 <ROW>
  \<COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  \<COLUMN SOURCE="2" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  \<COLUMN SOURCE="3" NAME="LastName" xsi:type="SQLVARYCHAR"/>
  \<COLUMN SOURCE="4" NAME="Gender" xsi:type="SQLCHAR"/>
 </ROW>
</BCPFORMAT>
```
**After**
```
\<?xml version="1.0"?>
\<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
 <RECORD>
  \<FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  \<FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  \<FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="1" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
 </RECORD>
 <ROW>
  \<COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  \<COLUMN SOURCE="3" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  \<COLUMN SOURCE="2" NAME="LastName" xsi:type="SQLVARYCHAR"/>
  \<COLUMN SOURCE="4" NAME="Gender" xsi:type="SQLCHAR"/>
 </ROW>
</BCPFORMAT>
```
The modified format file now reflects:
* FIELD 1, which corresponds to COLUMN 1, is mapped to the first table column, `myRemap.. PersonID`
* FIELD 2, which corresponds to COLUMN 2, is re-mapped to the third table column, `myRemap.. LastName`
* FIELD 3, which corresponds to COLUMN 3, is re-mapped to the second table column, `myRemap.. FirstName`
* FIELD 4, which corresponds to COLUMN 4, is mapped to the fourth table column, `myRemap.. Gender`


## Importing Data with a Format File to Map Table Columns to Data-File Field<a name="import_data"></a>
The examples below use the database, datafile, and format files created above.

### Using [bcp](../../tools/bcp-utility.md) and [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)<a name="bcp_nonxml"></a>
At a command prompt, enter the following command:
```
bcp TestDatabase.dbo.myRemap IN D:\BCP\myRemap.bcp -f D:\BCP\myRemap.fmt -T
```

### Using [bcp](../../tools/bcp-utility.md) and [XML Format File](../../relational-databases/import-export/xml-format-files-sql-server.md)<a name="bcp_xml"></a>
At a command prompt, enter the following command:
```
bcp TestDatabase.dbo.myRemap IN D:\BCP\myRemap.bcp -f D:\BCP\myRemap.xml -T
```

### Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)<a name="bulk_nonxml"></a>
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myRemap;
BULK INSERT dbo.myRemap   
   FROM 'D:\BCP\myRemap.bcp'   
   WITH (FORMATFILE = 'D:\BCP\myRemap.fmt');  
GO  

-- review results
SELECT * FROM TestDatabase.dbo.myRemap;
```

### Using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [XML Format File](../../relational-databases/import-export/xml-format-files-sql-server.md)<a name="bulk_xml"></a>
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myRemap;
BULK INSERT dbo.myRemap   
   FROM 'D:\BCP\myRemap.bcp'   
   WITH (FORMATFILE = 'D:\BCP\myRemap.xml');  
GO  

-- review results
SELECT * FROM TestDatabase.dbo.myRemap;
```

### Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and [Non-XML Format File](../../relational-databases/import-export/non-xml-format-files-sql-server.md)<a name="openrowset_nonxml"></a>	
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;
GO

TRUNCATE TABLE myRemap;
INSERT INTO dbo.myRemap
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myRemap.bcp',
		FORMATFILE = 'D:\BCP\myRemap.fmt'
		) AS t1;
GO

-- review results
SELECT * FROM TestDatabase.dbo.myRemap;
```

### Using [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and [XML Format File](../../relational-databases/import-export/xml-format-files-sql-server.md)<a name="openrowset_xml"></a>
Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):
```sql
USE TestDatabase;  
GO

TRUNCATE TABLE myRemap;
INSERT INTO dbo.myRemap 
	SELECT *
	FROM OPENROWSET (
		BULK 'D:\BCP\myRemap.bcp',
		FORMATFILE = 'D:\BCP\myRemap.xml'  
       ) AS t1;
GO

-- review results
SELECT * FROM TestDatabase.dbo.myRemap;
```


  
## See Also  
[bcp Utility](../../tools/bcp-utility.md)   
 [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)   
 [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
  
