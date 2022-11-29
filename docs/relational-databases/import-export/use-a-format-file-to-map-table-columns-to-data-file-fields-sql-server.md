---
title: "Map table columns to data-file fields with a format file"
description: In SQL Server, non-XML and XML format files can accommodate a data file whose fields are arranged in a different order from the table columns.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/29/2022
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "mapping columns to fields during import [SQL Server]"
  - "format files [SQL Server], mapping columns to fields"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use a format file to map table columns to data-file fields (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A data file can contain fields arranged in a different order from the corresponding columns in the table. This topic presents both non-XML and XML format files that have been modified to accommodate a data file whose fields are arranged in a different order from the table columns. The modified format file maps the data fields to their corresponding table columns.  Please review [Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md) for additional information.

> [!NOTE]  
> Either a non-XML or XML format file can be used to bulk import a data file into the table by using a [bcp utility](../../tools/bcp-utility.md) command, [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, or INSERT ... SELECT * FROM [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement. For more information, see [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).

> [!NOTE]  
> This syntax, including bulk insert, is not supported in Azure Synapse Analytics. [!INCLUDE[Use ADF or PolyBase instead of Synapse Bulk Insert](../../includes/paragraph-content/bulk-insert-synapse.md)]

## <a id="etc"></a> Example test conditions

The examples of modified format files in this topic are based on the table and data file defined below.

### <a id="sample_table"></a> Sample table

The script below creates a test database and a table named `myRemap`. Execute the following Transact-SQL in Microsoft SQL Server Management Studio (SSMS):

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

### <a id="sample_data_file"></a> Sample data file

The data below presents `FirstName` and `LastName` in the reverse order as presented in the table `myRemap`.  Using Notepad, create an empty file `D:\BCP\myRemap.bcp` and insert the following data:

```output
1,Grosse,Anthony,M
2,Fatnowna,Alica,F
3,Rosenhain,Stella,F
```

## <a id="create_format_file"></a> Create the format files

To bulk import data from `myRemap.bcp` into the `myRemap` table, the format file must do the following:

* Map the first data field to the first column, `PersonID`.
* Map the second data field to the third column, `LastName`.
* Map the third data field to the second column, `FirstName`.
* Map the fourth data field to the fourth column, `Gender`.

The simplest method to create the format file is by using the [bcp utility](../../tools/bcp-utility.md).  First, create a base format file from the existing table.  Second, modify the base format file to reflect the actual data file.

### <a id="nonxml_format_file"></a> Create a non-XML format file

Please review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information. The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myRemap.fmt`, based on the schema of `myRemap`.  In addition, the qualifier `c` is used to specify character data, `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myRemap format nul -c -f D:\BCP\myRemap.fmt -t, -T
```

### <a id="modify_nonxml_format_file"></a> Modify the non-XML format file

See [Structure of Non-XML Format Files](../../relational-databases/import-export/non-xml-format-files-sql-server.md#Structure) for terminology.  Open `D:\BCP\myRemap.fmt` in Notepad and perform the following modifications:

1.  Re-arrange the order of the format-file rows so that the rows are in the same order as the data in `myRemap.bcp`.
1.  Ensure the host file field order values are sequential.
1.  Ensure there is a carriage return after the last format-file row.

Compare the changes:

**Before**

```output
13.0
4
1       SQLCHAR    0       7       ","      1     PersonID               ""
2       SQLCHAR    0       25      ","      2     FirstName              SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR    0       30      ","      3     LastName               SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR    0       1       "\r\n"   4     Gender                 SQL_Latin1_General_CP1_CI_AS
```

**After**

```output
13.0
4
1       SQLCHAR    0       7       ","      1     PersonID               ""
2       SQLCHAR    0       30      ","      3     LastName               SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR    0       25      ","      2     FirstName              SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR    0       1       "\r\n"   4     Gender                 SQL_Latin1_General_CP1_CI_AS
```

The modified format file now reflects:

* The first data field in `myRemap.bcp` is mapped to the first column, `myRemap.. PersonID`
* The second data field in `myRemap.bcp` is mapped to the third column, `myRemap.. LastName`
* The third data field in `myRemap.bcp` is mapped to the second column, `myRemap.. FirstName`
* The fourth data field in `myRemap.bcp` is mapped to the fourth column, `myRemap.. Gender`

### <a id="xml_format_file"></a> Create an XML format file

Please review [XML Format Files (SQL Server)](../../relational-databases/import-export/xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to create an xml format file, `myRemap.xml`, based on the schema of `myRemap`.  In addition, the qualifier `c` is used to specify character data, `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  The `x` qualifier must be used to generate an XML-based format file.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myRemap format nul -c -x -f D:\BCP\myRemap.xml -t, -T
```

### <a id="modify_xml_format_file"></a> Modify the XML format file

Review [Schema syntax for XML format files](../../relational-databases/import-export/xml-format-files-sql-server.md#StructureOfXmlFFs) for terminology.  Open `D:\BCP\myRemap.xml` in Notepad and perform the following modifications:

1. The order in which the \<FIELD> elements are declared in the format file is the order in which those fields appear in the data file, thus reverse the order for the \<FIELD> elements with ID attributes 2 and 3.
1. Ensure the \<FIELD> ID attribute values are sequential.
1. The order of the \<COLUMN> elements in the \<ROW> element defines the order in which the bulk operation sends them to the target.  The XML format file assigns each \<COLUMN> element a local name that has no relationship to the column in the target table of a bulk import operation.  The order of the \<COLUMN> elements is independent of the order of \<FIELD> elements in a \<RECORD> definition.  Each \<COLUMN> element corresponds to a \<FIELD> element (whose ID is specified in the SOURCE attribute of the \<COLUMN> element).  Thus, the values for \<COLUMN> SOURCE are the only attributes that require revision.  Reverse the order for \<COLUMN> SOURCE attributes 2 and 3.

Compare the changes:

**Before**

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<RECORD>
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="1" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
</RECORD>
<ROW>
  <COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  <COLUMN SOURCE="2" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  <COLUMN SOURCE="3" NAME="LastName" xsi:type="SQLVARYCHAR"/>
  <COLUMN SOURCE="4" NAME="Gender" xsi:type="SQLCHAR"/>
</ROW>
</BCPFORMAT>
```

**After**

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<RECORD>
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="1" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
</RECORD>
<ROW>
  <COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  <COLUMN SOURCE="3" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  <COLUMN SOURCE="2" NAME="LastName" xsi:type="SQLVARYCHAR"/>
  <COLUMN SOURCE="4" NAME="Gender" xsi:type="SQLCHAR"/>
</ROW>
</BCPFORMAT>
```

The modified format file now reflects:
* FIELD 1, which corresponds to COLUMN 1, is mapped to the first table column, `myRemap.. PersonID`
* FIELD 2, which corresponds to COLUMN 2, is re-mapped to the third table column, `myRemap.. LastName`
* FIELD 3, which corresponds to COLUMN 3, is re-mapped to the second table column, `myRemap.. FirstName`
* FIELD 4, which corresponds to COLUMN 4, is mapped to the fourth table column, `myRemap.. Gender`

## <a id="import_data"></a> Import data with a format file to map table columns to data-file field

The examples below use the database, datafile, and format files created above.

### <a id="bcp_nonxml"></a> Use [bcp](../../tools/bcp-utility.md) and [non-XML format file](../../relational-databases/import-export/non-xml-format-files-sql-server.md)

At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myRemap IN D:\BCP\myRemap.bcp -f D:\BCP\myRemap.fmt -T
```

### <a id="bcp_xml"></a> Use [bcp](../../tools/bcp-utility.md) and [XML format file](../../relational-databases/import-export/xml-format-files-sql-server.md)

At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myRemap IN D:\BCP\myRemap.bcp -f D:\BCP\myRemap.xml -T
```

### <a id="bulk_nonxml"></a> Use [BULK INSERT](../../T-SQL/statements/bulk-insert-Transact-SQL.md) and [non-XML format file](../../relational-databases/import-export/non-xml-format-files-sql-server.md)

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

### <a id="bulk_xml"></a> Use [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [XML format file](../../relational-databases/import-export/xml-format-files-sql-server.md)

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

### <a id="openrowset_nonxml"></a> Use [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and [non-XML format file](../../relational-databases/import-export/non-xml-format-files-sql-server.md)

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

### <a id="openrowset_xml"></a> Use [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) and [XML format file](../../relational-databases/import-export/xml-format-files-sql-server.md)

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

## Next steps

- [bcp Utility](../../tools/bcp-utility.md)   
- [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)   
- [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
