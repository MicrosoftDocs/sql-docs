---
title: "Use a Format File to Bulk Import Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bulk importing [SQL Server], format files"
  - "format files [SQL Server], importing data using"
ms.assetid: 2956df78-833f-45fa-8a10-41d6522562b9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Use a Format File to Bulk Import Data (SQL Server)
  This topic illustrates the use of a format file in bulk-import operations. The format file maps the fields of the data file to the columns of the table.  You can use a non-XML or XML format file to bulk import data when using a **bcp** command or a BULK INSERT or INSERT ... SELECT * FROM OPENROWSET(BULK...) [!INCLUDE[tsql](../../includes/tsql-md.md)] command.  
  
> [!IMPORTANT]  
>  For a format file to work with a Unicode character data file, all the input fields must be Unicode text strings (that is, either fixed-size or character-terminated Unicode strings).  
  
> [!NOTE]  
>  If you are unfamiliar with format files, see [Non-XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md) and [XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md).  
  
## Format File Options for Bulk-Import Commands  
 The following table summarizes the format-file option of for each of the bulk-import commands.  
  
|Bulk-load Command|Using the Format-File Option|  
|------------------------|-----------------------------------|  
|BULK INSERT|FORMATFILE = '*format_file_path*'|  
|INSERT ... SELECT * FROM OPENROWSET(BULK...)|FORMATFILE = '*format_file_path*'|  
|**bcp** ... **in**|**-f** *format_file*|  
  
 For more information, see [bcp Utility](../../tools/bcp-utility.md), [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql), or [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql).  
  
> [!NOTE]  
>  To bulk export or import SQLXML data, use one of the following data types in your format file: SQLCHAR or SQLVARYCHAR (the data is sent in the client code page or in the code page implied by the collation), SQLNCHAR or SQLNVARCHAR (the data is sent as Unicode), or SQLBINARY or SQLVARYBIN (the data is sent without any conversion).  
  
## Examples  
 The examples in this section illustrate how to use format files to bulk-import data by using the **bcp** command and the BULK INSERT, and INSERT ... SELECT * FROM OPENROWSET(BULK...) statements. Before you can run one of the bulk-import examples, you need to create a sample table, data file, and a format file.  
  
### Sample Table  
 The examples require that a table named **myTestFormatFiles** table be created in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] sample database under the **dbo** schema. To create this table, in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE myTestFormatFiles (  
   Col1 smallint,  
   Col2 nvarchar(50),  
   Col3 nvarchar(50),  
   Col4 nvarchar(50)  
   );  
GO  
```  
  
### Sample Data File  
 The examples use a sample data file, `myTestFormatFiles-c.Dat`, which contains the following records. To create the data file, at the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows command prompt, enter:  
  
```  
10,Field2,Field3,Field4  
15,Field2,Field3,Field4  
46,Field2,Field3,Field4  
58,Field2,Field3,Field4  
```  
  
### Sample Format Files  
 Some of the examples in this section use an XML format file, `myTestFormatFiles-f-x-c.Xml`, and other examples use a non-XML format file. Both format files use character data formats and a non-default field terminator (,).  
  
#### The Sample Non-XML Format File  
 The following example uses **bcp** to generate an XML format file from the `myTestFormatFiles` table. The `myTestFormatFiles.Fmt` file contains the following information:  
  
```  
9.0  
4  
1       SQLCHAR       0       7       ","      1     Col1         ""  
2       SQLCHAR       0       100     ","      2     Col2         SQL_Latin1_General_CP1_CI_AS  
3       SQLCHAR       0       100     ","      3     Col3         SQL_Latin1_General_CP1_CI_AS  
4       SQLCHAR       0       100     "\r\n"   4     Col4         SQL_Latin1_General_CP1_CI_AS  
```  
  
 To use **bcp** with the **format** option to create this format file, at the Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012..MyTestFormatFiles format nul -c -t, -f myTestFormatFiles.Fmt -T  
  
```  
  
 For more information about creating a format file, see [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md).  
  
#### The Sample XML Format File  
 The following example uses **bcp** to create to generate an XML format file from the `myTestFormatFiles` table. The `myTestFormatFiles.Xml` file contains the following information:  
  
```  
<?xml version="1.0"?>  
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
 <RECORD>  
  <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="7"/>  
  <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="," MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n" MAX_LENGTH="100" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
 </RECORD>  
 <ROW>  
  <COLUMN SOURCE="1" NAME="Col1" xsi:type="SQLSMALLINT"/>  
  <COLUMN SOURCE="2" NAME="Col2" xsi:type="SQLNVARCHAR"/>  
  <COLUMN SOURCE="3" NAME="Col3" xsi:type="SQLNVARCHAR"/>  
  <COLUMN SOURCE="4" NAME="Col4" xsi:type="SQLNVARCHAR"/>  
 </ROW>  
</BCPFORMAT>  
```  
  
 To use **bcp** with the **format** option to create this format file, at the Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012..MyTestFormatFiles format nul -c -t, -x -f myTestFormatFiles.Xml -T  
```  
  
### Using bcp  
 The following example uses **bcp** to bulk import data from the `myTestFormatFiles-c.Dat` data file into `HumanResources.myTestFormatFiles` table in the  sample database. This example uses an XML format file, `MyTestFormatFiles.Xml`. The example deletes any existing table rows before importing the data file.  
  
 At the Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012..myTestFormatFiles in C:\myTestFormatFiles-c.Dat -f C:\myTestFormatFiles.Xml -T  
```  
  
> [!NOTE]  
>  For more information about this command, see [bcp Utility](../../tools/bcp-utility.md).  
  
### Using BULK INSERT  
 The following example uses BULK INSERT to bulk import data from the `myTestFormatFiles-c.Dat` data file into `HumanResources.myTestFormatFiles` table in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] sample database. This example uses a non-XML format file, `MyTestFormatFiles.Fmt`. The example deletes any existing table rows before importing the data file.  
  
 In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks2012;  
GO  
DELETE myTestFormatFiles;  
GO  
BULK INSERT myTestFormatFiles   
   FROM 'C:\myTestFormatFiles-c.Dat'   
   WITH (FORMATFILE = 'C:\myTestFormatFiles.Fmt');  
GO  
SELECT * FROM myTestFormatFiles;  
GO  
```  
  
> [!NOTE]  
>  For more information about this statement, see [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql).  
  
### Using the OPENROWSET Bulk Rowset Provider  
 The following example uses `INSERT ... SELECT * FROM OPENROWSET(BULK...)` to bulk import data from the `myTestFormatFiles-c.Dat` data file into `HumanResources.myTestFormatFiles` table in the `AdventureWorks` sample database. This example uses an XML format file, `MyTestFormatFiles.Xml`. The example deletes any existing table rows before importing the data file.  
  
 In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks2012;  
DELETE myTestFormatFiles;  
GO  
INSERT INTO myTestFormatFiles  
    SELECT *  
      FROM  OPENROWSET(BULK  'C:\myTestFormatFiles-c.Dat',  
      FORMATFILE='C:\myTestFormatFiles.Xml'       
      ) as t1 ;  
GO  
SELECT * FROM myTestFormatFiles;  
GO  
```  
  
 When you finish using the sample table, you can drop it using the following statement:  
  
```  
DROP TABLE myTestFormatFiles  
```  
  
> [!NOTE]  
>  For more information about the OPENROWSET BULK clause, see [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql).  
  
## Additional Examples  
 [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md)  
  
 [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](use-a-format-file-to-skip-a-table-column-sql-server.md)  
  
 [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
 [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [Non-XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md)   
 [XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md)  
  
  
