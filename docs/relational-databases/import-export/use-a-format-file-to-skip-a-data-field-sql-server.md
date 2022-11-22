---
title: "Use a format file to skip a data field"
description: You can use a format file with a data file that has more fields than table columns. It maps table columns to corresponding data fields and ignores extra fields.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/29/2022
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "format files [SQL Server], skipping data fields"
  - "skipping data fields when importing"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use a format file to skip a data field (SQL Server)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A data file can contain more fields than the number of columns in the table. This topic describes modifying both non-XML and XML format files to accommodate a data file with more fields by mapping the table columns to the corresponding data fields and ignoring the extra fields.  Please review [Create a Format File (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md) for additional information.

> [!NOTE]  
> Either a non-XML or XML format file can be used to bulk import a data file into the table by using a [bcp utility](../../tools/bcp-utility.md) command, [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement, or INSERT ... SELECT * FROM [OPENROWSET(BULK...)](../../t-sql/functions/openrowset-transact-sql.md) statement. For more information, see [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).

> [!NOTE]  
> This syntax, including bulk insert, is not supported in Azure Synapse Analytics. [!INCLUDE[Use ADF or PolyBase instead of Synapse Bulk Insert](../../includes/paragraph-content/bulk-insert-synapse.md)]

## <a id="etc"></a> Example test conditions

The examples of modified format files in this topic are based on the table and data file defined below.

### <a id="sample_table"></a> Sample table

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

### <a id="sample_data_file"></a> Sample data file

Create an empty file `D:\BCP\myTestSkipField.bcp` and insert the following data:

```output
1,SkipMe,Anthony,Grosse
2,SkipMe,Alica,Fatnowna
3,SkipMe,Stella,Rosenhain
```

## <a id="create_format_file"></a> Create the format files

To bulk import data from `myTestSkipField.bcp` into the `myTestSkipField` table, the format file must do the following:

* Map the first data field to the first column, `PersonID`.
* Skip the second data field.
* Map the third data field to the second column, `FirstName`.
* Map the fourth data field to the third column, `LastName`.

The simplest method to create the format file is by using the [bcp utility](../../tools/bcp-utility.md).  First, create a base format file from the existing table.  Second, modify the base format file to reflect the actual data file.

### <a id="nonxml_format_file"></a> Create a non-XML format file

Review [Non-XML Format Files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) for detailed information. The following command will use the [bcp utility](../../tools/bcp-utility.md) to generate a non-xml format file, `myTestSkipField.fmt`, based on the schema of `myTestSkipField`.  In addition, the qualifier `c` is used to specify character data , `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myTestSkipField format nul -c -f D:\BCP\myTestSkipField.fmt -t, -T
```

### <a id="modify_nonxml_format_file"></a> Modify the non-XML format file

Review the [structure of non-XML format files](../../relational-databases/import-export/non-xml-format-files-sql-server.md#Structure) for terminology. Open `D:\BCP\myTestSkipField.fmt` in Notepad and perform the following modifications:

1) Copy the entire format-file row for `FirstName` and paste it directly after `FirstName` on the next line.
2) Increase the host file field order value by one for the new row and all subsequent rows.
3) Increase the number of columns value to reflect the actual number of fields in the data file.
4) Modify the server column order from `2` to `0` for the second format-file row.

Compare the changes made:

**Before**

```output
13.0
3
1       SQLCHAR    0       7       ","      1     PersonID        ""
2       SQLCHAR    0       25      ","      2     FirstName    SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR    0       30      "\r\n"   3     LastName     SQL_Latin1_General_CP1_CI_AS
```

**After**

```output
13.0
4
1       SQLCHAR    0       7       ","      1     PersonID     ""
2       SQLCHAR    0       25      ","      0     FirstName    SQL_Latin1_General_CP1_CI_AS
3       SQLCHAR    0       25      ","      2     FirstName    SQL_Latin1_General_CP1_CI_AS
4       SQLCHAR    0       50      "\r\n"   3     LastName     SQL_Latin1_General_CP1_CI_AS
```

The modified format file now reflects:

* 4 data fields
* The first data field in `myTestSkipField.bcp` is mapped to the first column, `myTestSkipField.. PersonID`
* The second data field in `myTestSkipField.bcp` is not mapped to any column.
* The third data field in `myTestSkipField.bcp` is mapped to the second column, `myTestSkipField.. FirstName`
* The fourth data field in `myTestSkipField.bcp` is mapped to the third column, `myTestSkipField.. LastName`

### <a id="xml_format_file"></a> Create an XML format file

Review [XML Format Files (SQL Server)](../../relational-databases/import-export/xml-format-files-sql-server.md) for detailed information.  The following command will use the [bcp utility](../../tools/bcp-utility.md) to create an xml format file, `myTestSkipField.xml`, based on the schema of `myTestSkipField`.  In addition, the qualifier `c` is used to specify character data , `t,` is used to specify a comma as a field terminator, and `T` is used to specify a trusted connection using integrated security.  The `x` qualifier must be used to generate an XML-based format file.  At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myTestSkipField format nul -c -x -f D:\BCP\myTestSkipField.xml -t, -T
```

### <a id="modify_xml_format_file"></a> Modify the XML format file

Review [schema syntax for XML format files](../../relational-databases/import-export/xml-format-files-sql-server.md#StructureOfXmlFFs) for terminology.  Open `D:\BCP\myTestSkipField.xml` in Notepad and perform the following modifications:

1) Copy the entire second field and paste it directly after the second field on the next line.
2) Increase the "FIELD ID" value by 1 for the new FIELD and for each subsequent FIELD.
3) Increase the "COLUMN SOURCE" value by 1 for `FirstName`, and `LastName` to reflect the revised mapping.

Compare the changes made:

**Before**

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<RECORD>
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
</RECORD>
<ROW>
  <COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  <COLUMN SOURCE="2" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  <COLUMN SOURCE="3" NAME="LastName" xsi:type="SQLVARYCHAR"/>
</ROW>
</BCPFORMAT>
```

**After**

```xml
<?xml version="1.0"?>
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<RECORD>
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="25" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
  <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>
</RECORD>
<ROW>
  <COLUMN SOURCE="1" NAME="PersonID" xsi:type="SQLSMALLINT"/>
  <COLUMN SOURCE="3" NAME="FirstName" xsi:type="SQLVARYCHAR"/>
  <COLUMN SOURCE="4" NAME="LastName" xsi:type="SQLVARYCHAR"/>
</ROW>
</BCPFORMAT>
```

The modified format file now reflects:

* 4 data fields
* FIELD 1 which corresponds to COLUMN 1 is mapped to the first table column, `myTestSkipField.. PersonID`
* FIELD 2 does not correspond to any COLUMN and thus, is not mapped to any table column.
* FIELD 3 which corresponds to COLUMN 3 is mapped to the second table column, `myTestSkipField.. FirstName`
* FIELD 4 which corresponds to COLUMN 4 is mapped to the third table column, `myTestSkipField.. LastName`

## <a id="import_data"></a> Import data with a format file to skip a data field

The examples below use the database, datafile, and format files created above.

### <a id="bcp_nonxml"></a> Use [bcp](../../tools/bcp-utility.md) and [non-XML format file](../../relational-databases/import-export/non-XML-format-files-SQL-server.md)

At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myTestSkipField IN D:\BCP\myTestSkipField.bcp -f D:\BCP\myTestSkipField.fmt -T
```

### <a id="bcp_xml"></a> Use [bcp](../../tools/bcp-utility.md) and [XML format file](../../relational-databases/import-export/XML-format-files-SQL-server.md)

At a command prompt, enter the following command:

```cmd
bcp TestDatabase.dbo.myTestSkipField IN D:\BCP\myTestSkipField.bcp -f D:\BCP\myTestSkipField.xml -T
```

### <a id="bulk_nonxml"></a> Use [BULK INSERT](../../T-SQL/statements/bulk-insert-Transact-SQL.md) and [non-XML format file](../../relational-databases/import-export/non-XML-format-files-SQL-server.md)

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

### <a id="bulk_xml"></a> Use [BULK INSERT](../../T-SQL/statements/bulk-insert-Transact-SQL.md) and [XML format file](../../relational-databases/import-export/XML-format-files-SQL-server.md)

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

### <a id="openrowset_nonxml"></a> Use [OPENROWSET(BULK...)](../../T-SQL/functions/OPENROWSET-Transact-SQL.md) and [non-XML format file](../../relational-databases/import-export/non-XML-format-files-SQL-server.md)

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

### <a id="openrowset_xml"></a> Use [OPENROWSET(BULK...)](../../T-SQL/functions/OPENROWSET-Transact-SQL.md) and [XML format file](../../relational-databases/import-export/XML-format-files-SQL-server.md)

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

## Next steps

- [bcp Utility](../../tools/bcp-utility.md)
- [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)
- [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)
