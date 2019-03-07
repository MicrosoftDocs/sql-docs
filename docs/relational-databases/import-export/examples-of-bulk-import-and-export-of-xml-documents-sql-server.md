---
title: "Examples of Bulk Import and Export of XML Documents (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "10/24/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "field terminators [SQL Server]"
  - "bulk importing [SQL Server], data formats"
  - "row terminators [SQL Server]"
  - "OPENROWSET function, XML bulk load"
  - "terminators [SQL Server]"
  - "bulk exporting [SQL Server], data formats"
  - "XML bulk load [SQL Server]"
ms.assetid: dff99404-a002-48ee-910e-f37f013d946d
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Examples of Bulk Import and Export of XML Documents (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

    
##  <a name="top"></a>
 You can bulk import XML documents into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database or bulk export them from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. This topic provides examples of both.  
  
 To bulk import data from a data file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or non-partitioned view, you can use the following:  
  
-   **bcp** utility  
    You can also use the **bcp** utility to export data from anywhere in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that a SELECT statement works, including partitioned views.  
  
-   BULK INSERT  
  
-   INSERT ... SELECT * FROM OPENROWSET(BULK...)  

For more information, see the following topics.
- [Import and Export Bulk Data by Using the bcp Utility (SQL Server).](../../relational-databases/import-export/import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md)
- [Import Bulk Data by Using BULK INSERT or OPENROWSET (BULK...)(SQL Server).](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md) 
- [How to import XML into SQL Server with the XML Bulk Load component.](https://support.microsoft.com/kb/316005)
- [XML Schema Collections (SQL Server)](../xml/xml-schema-collections-sql-server.md)
  
## Examples  
 The examples are the following:  
  
-  [A. Bulk importing XML data as a binary byte stream](#binary_byte_stream)  
  
-  [B. Bulk importing XML data in an existing row](#existing_row)  
  
-  [C. Bulk importing XML data from a file that contains a DTD](#file_contains_dtd)  
  
- [D. Specifying the field terminator explicitly using a format file](#field_terminator_in_format_file)  
  
-  [E. Bulk exporting XML data](#bulk_export_xml_data)  
  
## <a name="binary_byte_stream"></a>Bulk importing XML data as a binary byte stream  
 When you bulk import XML data from a file that contains an encoding declaration that you want to apply, specify the SINGLE_BLOB option in the OPENROWSET(BULK...) clause. The SINGLE_BLOB option ensures that the XML parser in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] imports the data according to the encoding scheme specified in the XML declaration.  
  
#### Sample Table  
 To test example A below, create sample table `T`.  
  
```sql
USE tempdb  
CREATE TABLE T (IntCol int, XmlCol xml);  
GO  
```  
  
#### Sample Data File  
 Before you can run example A, you must create a UTF-8 encoded file (`C:\SampleFolder\SampleData3.txt`) that contains the following sample instance that specifies the `UTF-8` encoding scheme.  
  
```xml  
<?xml version="1.0" encoding="UTF-8"?>  
<Root>  
          <ProductDescription ProductModelID="5">  
             <Summary>Some Text</Summary>  
          </ProductDescription>  
</Root>  
```  
  
#### Example A  
 This example uses the `SINGLE_BLOB` option in an `INSERT ... SELECT * FROM OPENROWSET(BULK...)` statement to import data from a file named `SampleData3.txt` and insert an XML instance in the single-column table, sample table `T`.  
  
```sql
INSERT INTO T(XmlCol)  
SELECT * FROM OPENROWSET(  
   BULK 'c:\SampleFolder\SampleData3.txt',  
   SINGLE_BLOB) AS x;  
```  
  
#### Remarks  
 By using SINGLE_BLOB in this case, you can avoid a mismatch between the encoding of the XML document (as specified by the XML encoding declaration) and the string codepage implied by the server.  
  
 If you use NCLOB or CLOB data types and run into a codepage or encoding conflict, you must do one of the following:  
  
-   Remove the XML declaration to successfully import the contents of the XML data file.  
  
-   Specify a code page in the CODEPAGE option of the query that matches the encoding scheme that is used in the XML declaration.  
  
-   Match, or resolve, the database collation settings with a non-Unicode XML encoding scheme.  
  
 [&#91;Top&#93;](#top)  
  
##  <a name="existing_row"></a> Bulk importing XML data in an existing row  
 This example uses the `OPENROWSET` bulk rowset provider to add an XML instance to an existing row or rows in sample table `T`.  
  
> [!NOTE]  
>  To run this example, you must first complete the test script provided in example A. That example creates the `tempdb.dbo.T` table and bulk imports data from `SampleData3.txt`.  
  
#### Sample Data File  
 Example B uses a modified version of the `SampleData3.txt` sample data file from the preceding example. To run this example, modify the content of this file as follows:  
  
```xml
<Root>  
          <ProductDescription ProductModelID="10">  
             <Summary>Some New Text</Summary>  
          </ProductDescription>  
</Root>  
```  
  
#### Example B  
  
```sql  
-- Query before update shows initial state of XmlCol values.  
SELECT * FROM T  
UPDATE T  
SET XmlCol =(  
SELECT * FROM OPENROWSET(  
   BULK 'C:\SampleFolder\SampleData3.txt',  
           SINGLE_BLOB  
) AS x  
)  
WHERE IntCol = 1;  
GO  
```  
  
 [&#91;Top&#93;](#top)  
  
## <a name="file_contains_dtd"></a> Bulk importing XML data from a file that contains a DTD  
  
> [!IMPORTANT]  
>  We recommended that you not enable support for Document Type Definitions (DTDs) if it is not required in your XML environment. Turning on DTD support increases the attackable surface area of your server, and may expose it to a denial-of-service attack. If you must enable DTD support, you can reduce this security risk by processing only trusted XML documents.  
  
 During an attempt to use a [bcp](../../tools/bcp-utility.md) command to import XML data from a file that contains a DTD, an error similar to the following can occur:  
  
 "SQLState = 42000, NativeError = 6359"  
  
 "Error = [Microsoft][SQL Server Native Client][ SQL Server]Parsing XML with internal subset DTDs not allowed. Use CONVERT with style option 2 to enable limited internal subset DTD support."  
  
 "BCP copy %s failed"  
  
 To work around this problem, you can import XML data from a data file that contains a DTD by using the `OPENROWSET(BULK...)` function and then specifying the `CONVERT` option in the `SELECT` clause of the command. The basic syntax for the command is:  
  
 `INSERT ... SELECT CONVERT(...) FROM OPENROWSET(BULK...)`  
  
#### Sample Data File  
 Before you can test this bulk import example, create a file (`C:\temp\Dtdfile.xml`) that contains the following sample instance:  
  
```xml 
<!DOCTYPE DOC [<!ATTLIST elem1 attr1 CDATA "defVal1">]><elem1>January</elem1>  
```  
  
#### Sample Table  
 Example C uses the `T1` sample table that is created by the following `CREATE TABLE` statement:  
  
```sql  
USE tempdb;  
CREATE TABLE T1(XmlCol xml);  
GO  
```  
  
#### Example C  
 This example uses `OPENROWSET(BULK...)` and specifies the `CONVERT` option in the `SELECT` clause to import the XML data from `Dtdfile.xml` into sample table `T1`.  
  
```sql
INSERT T1  
  SELECT CONVERT(xml, BulkColumn, 2) FROM   
    OPENROWSET(Bulk 'c:\temp\Dtdfile.xml', SINGLE_BLOB) [rowsetresults];  
```  
  
 After the `INSERT` statement executes, the DTD is stripped from the XML and stored in the `T1` table.  
  
 [&#91;Top&#93;](#top)  
  
## <a name="field_terminator_in_format_file"></a> Specifying the field terminator explicitly using a format file  
 The following example shows how to bulk import the following XML document, `Xmltable.dat`.  
  
#### Sample Data File  
 The document in `Xmltable.dat` contains two XML values, one for each row. The first XML value is encoded with UTF-16, and the second value is encoded with UTF-8.  
  
 The contents of this data file are shown in the following Hex dump:  
  
```  
FF FE 3C 00 3F 00 78 00-6D 00 6C 00 20 00 76 00  *..\<.?.x.m.l. .v.*  
65 00 72 00 73 00 69 00-6F 00 6E 00 3D 00 22 00  *e.r.s.i.o.n.=.".*  
31 00 2E 00 30 00 22 00-20 00 65 00 6E 00 63 00  *1...0.". .e.n.c.*  
6F 00 64 00 69 00 6E 00-67 00 3D 00 22 00 75 00  *o.d.i.n.g.=.".u.*  
74 00 66 00 2D 00 31 00-36 00 22 00 3F 00 3E 00  *t.f.-.1.6.".?.>.*  
3C 00 72 00 6F 00 6F 00-74 00 3E 00 A2 4F 9C 76  *\<.r.o.o.t.>..O.v*  
0C FA 77 E4 80 00 89 00-00 06 90 06 91 2E 9B 2E  *..w.............*  
99 34 A2 34 86 00 83 02-92 20 7F 02 4E C5 E4 A3  *.4.4..... ..N...*  
34 B2 B7 B3 B7 FE F8 FF-F8 00 3C 00 2F 00 72 00  *4.........\<./.r.*  
6F 00 6F 00 74 00 3E 00-00 00 00 00 7A EF BB BF  *o.o.t.>.....z...*  
3C 3F 78 6D 6C 20 76 65-72 73 69 6F 6E 3D 22 31  *\<?xml version="1*  
2E 30 22 20 65 6E 63 6F-64 69 6E 67 3D 22 75 74  *.0" encoding="ut*  
66 2D 38 22 3F 3E 3C 72-6F 6F 74 3E E4 BE A2 E7  *f-8"?><root>....*  
9A 9C EF A8 8C EE 91 B7-C2 80 C2 89 D8 80 DA 90  *................*  
E2 BA 91 E2 BA 9B E3 92-99 E3 92 A2 C2 86 CA 83  *................*  
E2 82 92 C9 BF EC 95 8E-EA 8F A4 EB 88 B4 EB 8E  *................*  
B7 EF BA B7 EF BF B8 C3-B8 3C 2F 72 6F 6F 74 3E  *.........</root>*  
00 00 00 00 7A                                   *....z*  
```  
  
#### Sample Table  
 When you bulk import or export an XML document, you should use a [field terminator](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md) that cannot possibly appear in any of the documents; for example, a series of four nulls (`\0`) followed by the letter `z`: `\0\0\0\0z`.  
  
 This example shows how to use this field terminator for the `xTable` sample table. To create this sample table, use the following `CREATE TABLE` statement:  
  
```sql
USE tempdb;  
CREATE TABLE xTable (xCol xml);  
GO  
```  
  
#### Sample Format File  
 The field terminator must be specified in the format file. Example D uses a non-XML format file named `Xmltable.fmt` that contains the following:  
  
```  
9.0  
1  
1       SQLBINARY     0       0       "\0\0\0\0z"    1     xCol         ""  
```  
  
 You can use this format file to bulk import XML documents into the `xTable` table by using a `bcp` command or a `BULK INSERT` or `INSERT ... SELECT * FROM OPENROWSET(BULK...)` statement.  
  
#### Example D  
 This example uses the `Xmltable.fmt` format file in a `BULK INSERT` statement to import the contents of an XML data file named `Xmltable.dat`.  
  
```sql
BULK INSERT xTable   
FROM 'C:\Xmltable.dat'  
WITH (FORMATFILE = 'C:\Xmltable.fmt');  
GO  
```  
  
 [&#91;Top&#93;](#top)  
  
## <a name="bulk_export_xml_data"></a> Bulk exporting XML data  
 The following example uses `bcp` to bulk export XML data from the table that is created in the preceding example by using the same XML format file. In the following `bcp` command, `<server_name>` and `<instance_name>` represent placeholders that must be replaced with appropriate values:  
  
```cmd
bcp bulktest..xTable out a-wn.out -N -T -S<server_name>\<instance_name>  
```  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not save the XML encoding when XML data is persisted in the database. Therefore, the original encoding of XML fields is not available when XML data is exported. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses UTF-16 encoding when exporting XML data.  
  

## See Also  
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [SELECT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-clause-transact-sql.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)  
  
  
