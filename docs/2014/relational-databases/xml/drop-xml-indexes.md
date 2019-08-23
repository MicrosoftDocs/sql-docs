---
title: "Drop XML Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "removing indexes"
  - "dropping indexes"
  - "XML indexes [SQL Server], dropping"
ms.assetid: 7591ebea-34af-4925-8553-b2adb5b487c2
author: MightyPen
ms.author: genemi
manager: craigg
---
# Drop XML Indexes
  The [DROP INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-index-transact-sql)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement can be used to drop existing primary or secondary XML and non-XML indexes. However, no options of DROP INDEX apply to XML indexes. If you drop the primary XML index, any secondary indexes that are present are also deleted.  
  
 The DROP syntax with *TableName.IndexName* is being phased out and is not supported for XML indexes.  
  
## Example: Creating and Dropping a Primary XML Index  
 In the following example, an XML index is created on an `xml` type column.  
  
```  
DROP TABLE T  
GO  
CREATE TABLE T (Col1 INT PRIMARY KEY, XmlCol XML)  
GO  
-- Create Primary XML index   
CREATE PRIMARY XML INDEX PIdx_T_XmlCol   
ON T(XmlCol)  
GO  
-- Verify the index creation.   
-- Note index type is 3 for xml indexes.  
-- Note the type 3 is index on XML type.  
SELECT *  
FROM sys.xml_indexes  
WHERE object_id = object_id('T')  
AND name='PIdx_T_XmlCol'   
-- Drop the index.  
DROP INDEX PIdx_T_XmlCol ON T  
```  
  
 When a table is dropped, all the XML indexes on it are also automatically dropped. However, an XML column cannot be dropped from a table if an XML index exists on the column.  
  
 In the following example, an XML index is created on an `xml` type column. For more information, see [Compare Typed XML to Untyped XML](../xml/compare-typed-xml-to-untyped-xml.md).  
  
```  
CREATE TABLE TestTable(  
 Col1 int primary key,   
 Col2 xml (Production.ProductDescriptionSchemaCollection))   
GO  
```  
  
 Now, you can create a primary XML index on `Co12`.  
  
```  
CREATE PRIMARY XML INDEX PIdx_TestTable_Col2   
ON TestTable(Col2)  
GO  
```  
  
## Example: Creating an XML Index by Using the DROP_EXISTING Index Option  
 In the following example, an XML index is created on a column (`XmlColx`). Then, another XML index with the same name is created on a different column, (`XmlColy`). Because the `DROP_EXISTING` option is specified, the existing XML index on (`XmlColx)` is dropped and a new XML index on (`XmlColy`) is created.  
  
```  
DROP TABLE T  
GO  
CREATE TABLE T(Col1 int primary key, XmlColx xml, XmlColy xml)  
GO  
-- Create XML index on XmlColx.  
CREATE PRIMARY XML INDEX PIdx_T_XmlCol   
ON T(XmlColx)  
GO  
-- Create same name XML index on XmlColy.  
CREATE PRIMARY XML INDEX PIdx_T_XmlCol   
ON T(XmlColy)   
WITH (DROP_EXISTING = ON)  
-- Verify the index is created on XmlColy.d.  
SELECT sc.name   
FROM   sys.xml_indexes si inner join sys.index_columns sic   
ON     sic.object_id=si.object_id and sic.index_id=si.index_id  
INNER  join sys.columns sc on sc.object_id=sic.object_id   
AND    sc.column_id=sic.column_id  
WHERE  si.name='PIdx_T_XmlCol'   
AND    si.object_id=object_id('T')  
```  
  
 This query returns the column name on which the specified XML index is created.  
  
## See Also  
 [XML Indexes &#40;SQL Server&#41;](xml-indexes-sql-server.md)  
  
  
