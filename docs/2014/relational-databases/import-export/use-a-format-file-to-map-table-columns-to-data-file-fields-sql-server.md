---
title: "Use a Format File to Map Table Columns to Data-File Fields (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping columns to fields during import [SQL Server]"
  - "format files [SQL Server], mapping columns to fields"
ms.assetid: e7ee4f7e-24c4-4eb7-84d2-41e57ccc1ef1
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Use a Format File to Map Table Columns to Data-File Fields (SQL Server)
  A data file can contain fields arranged in a different order from the corresponding columns in the table. This topic presents both non-XML and XML format files that have been modified to accommodate a data file whose fields are arranged in a different order from the table columns. The modified format file maps the data fields to their corresponding table columns.  
  
> [!NOTE]  
>  Either a non-XML format file or an XML format file can be used to bulk import a data file into the table by using a **bcp** command, BULK INSERT statement, or INSERT ... SELECT * FROM OPENROWSET(BULK...) statement. For more information, see [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](use-a-format-file-to-bulk-import-data-sql-server.md).  
  
## Sample Table and Data File  
 The examples of modified format files in this topic are based on the following table and data file.  
  
### Sample Table  
 The examples in this topic require that a table named `myTestOrder` be created in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database under the `dbo` schema. To create this table, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Editor, execute the following code:  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE myTestOrder   
   (  
   Col1 smallint,  
   Col2 nvarchar(50) ,  
   Col3 nvarchar(50) ,   
   Col4 nvarchar(50)   
   );  
GO  
  
```  
  
### Data File  
 The data file, `myTestOrder-c.txt`, contains the following records:  
  
```  
DataField3,DataField2,1,DataField4  
DataField3,DataField2,1,DataField4  
DataField3,DataField2,1,DataField4  
  
```  
  
 To bulk import data from `myTestSkipCol2-c.dat` into the `myTestSkipCol` table, the format file must map the first data field to `Col3`, the second data field to `Col2`, the third data field to `Col1`, and the fourth data field to `Col4`.  
  
## Using a Non-XML Format File  
 You can change the order of a column mapping by changing the order value for the column to indicate the position of the corresponding data field.  
  
 The following sample non-XML format file presents a format file, `myTestOrder.fmt`, that maps the fields in `myTestOrder-c.txt` to the columns of the `myTestOrder` table. For information about how to create the data file and table, see "Sample Table and Data File," earlier in this topic. The format file uses character data format.  
  
 The format file contains the following information:  
  
```  
9.0  
4  
1       SQLCHAR       0       100     ","     3     Col3               SQL_Latin1_General_CP1_CI_AS  
2       SQLCHAR       0       100     ","     2     Col2               SQL_Latin1_General_CP1_CI_AS  
3       SQLCHAR       0       7       ","     1     Col1               ""  
4       SQLCHAR       0       100     "\r\n"  4     Col4               SQL_Latin1_General_CP1_CI_AS  
  
```  
  
> [!NOTE]  
>  For more information about the layout of non-XML format files, see [Non-XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md).  
  
### Example  
 The following example uses a `BULK INSERT` statement to bulk import data from the `myTestOrder-c.txt` data file into the `myTestOrder` sample table by using the `myTestOrder.fmt` non-XML format file.  
  
 In the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks2012;  
GO  
BULK INSERT myTestOrder  
FROM 'C:\myTestOrder-c.txt'   
WITH (formatfile='C:\myTestOrder.fmt');  
GO  
  
```  
  
## Using an XML Format File  
 The following sample non-XML format file presents a format file, `myTestOrder.xml`, that maps the fields in `myTestOrder-c.txt` to the columns of the `myTestOrder` table For information about how to create the data file and table, see "Sample Table and Data File," earlier in this topic.  
  
 The `myTestOrder.xml` format file contains the following information:  
  
```  
<?xml version="1.0"?>  
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"   
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
 <RECORD>  
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>  
  <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
 </RECORD>  
 <ROW>  
  <COLUMN SOURCE="3" NAME="Col1" xsi:type="SQLSMALLINT"/>  
  <COLUMN SOURCE="2" NAME="Col2" xsi:type="SQLNVARCHAR"/>  
  <COLUMN SOURCE="1" NAME="Col3" xsi:type="SQLNVARCHAR"/>  
  <COLUMN SOURCE="4" NAME="Col4" xsi:type="SQLNVARCHAR"/>  
 </ROW>  
</BCPFORMAT>  
  
```  
  
> [!NOTE]  
>  For information about the syntax of the XML Schema and additional samples of XML format files, see [XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md).  
  
### Example  
 The following example uses the `OPENROWSET` bulk rowset provider to import data from the `myTestOrder-c.txt` data file into the `myTestOrder` sample table by using the `myTestOrder.xml` XML format file. The `INSERT... SELECT` statement specifies the column list in the select list.  
  
 In the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Editor, execute the following code:  
  
```  
USE AdventureWorks2012;  
GO  
INSERT INTO myTestOrder   
  SELECT Col1, Col2, Col3, Col4  
      FROM  OPENROWSET(BULK  'C:\myTestOrder-c.txt',  
      FORMATFILE='C:\myTestOrder.Xml'    
       ) AS t1;  
GO  
  
```  
  
## See Also  
 [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](use-a-format-file-to-skip-a-table-column-sql-server.md)   
 [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
  
