---
title: "Use XML in Computed Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "computed columns, XML"
  - "XML [SQL Server], computed columns"
ms.assetid: 1313b889-69b4-4018-9868-0496dd83bf44
author: MightyPen
ms.author: genemi
manager: craigg
---
# Use XML in Computed Columns
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  XML instances can appear as a source for a computed column, or as a type of computed column. The examples in this topic show how to use XML with computed columns.  
  
## Creating Computed Columns from XML Columns  
 In the following `CREATE TABLE` statement, an `xml` type column (`col2`) is computed from `col1`:  
  
```  
CREATE TABLE T(col1 varchar(max), col2 AS CAST(col1 AS xml) )    
```  
  
 The `xml` data type can also appear as a source in creating a computed column, as shown in the following `CREATE TABLE` statement:  
  
```  
CREATE TABLE T (col1 xml, col2 as cast(col1 as varchar(1000) ))   
```  
  
 You can create a computed column by extracting a value from an `xml` type column as shown in the following example. Because the **xml** data type methods cannot be used directly in creating computed columns, the example first defines a function (`my_udf`) that returns a value from an XML instance. The function wraps the `value()` method of the `xml` type. The function name is then specified in the `CREATE TABLE` statement for the computed column.  
  
```  
CREATE FUNCTION my_udf(@var xml) returns int  
AS BEGIN   
RETURN @var.value('(/ProductDescription/@ProductModelID)[1]' , 'int')  
END  
GO  
-- Use the function in CREATE TABLE.  
CREATE TABLE T (col1 xml, col2 as dbo.my_udf(col1) )  
GO  
-- Try adding a row.   
INSERT INTO T values('<ProductDescription ProductModelID="1" />')  
GO  
-- Verify results.  
SELECT col2, col1  
FROM T  
  
```  
  
 As in the previous example, the following example defines a function to return an **xml** type instance for a computed column. Inside the function, the `query()` method of the `xml` data type retrieves a value from an `xml` type parameter.  
  
```  
CREATE FUNCTION my_udf(@var xml)   
  RETURNS xml AS   
BEGIN   
   RETURN @var.query('ProductDescription/Features')  
END  
```  
  
 In the following `CREATE TABLE` statement, `Col2` is a computed column that uses the XML data (`<Features>` element) that is returned by the function:  
  
```  
CREATE TABLE T (Col1 xml, Col2 as dbo.my_udf(Col1) )  
-- Insert a row in table T.  
INSERT INTO T VALUES('  
<ProductDescription ProductModelID="1" >  
  <Features>  
    <Feature1>description</Feature1>  
    <Feature2>description</Feature2>  
  </Features>  
</ProductDescription>')  
-- Verify the results.  
SELECT *  
FROM T  
```  
  
### In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Promote Frequently Used XML Values with Computed Columns](../../relational-databases/xml/promote-frequently-used-xml-values-with-computed-columns.md)|Describes how to use property promotion with computed columns and property tables.|  
  
  
