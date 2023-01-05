---
title: "sql:variable() Function (XQuery) | Microsoft Docs"
description: Learn how to use the XQuery Extension function sql:variable() to expose a variable that contains a SQL relational value inside an XQuery expression.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sql:variable() function"
  - "sql:variable function"
ms.assetid: 6e2e5063-c1cf-4b5a-b642-234921e3f4f7
author: "rothja"
ms.author: "jroth"
---
# XQuery Extension Functions - sql:variable()
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Exposes a variable that contains a SQL relational value inside an XQuery expression.  
  
## Syntax  
  
```  
  
sql:variable("variableName") as xdt:anyAtomicType?  
```  
  
## Remarks  
 As described in the topic [Binding Relational Data Inside XML](../t-sql/xml/binding-relational-data-inside-xml-data.md), you can use this function when you use [XML data type methods](../t-sql/xml/xml-data-type-methods.md) to expose a relational value inside XQuery.  
  
 For example, the [query() method](../t-sql/xml/query-method-xml-data-type.md) is used to specify a query against an XML instance that is stored in an **xml** data type variable or column. Sometimes, you might also want your query to use values from a [!INCLUDE[tsql](../includes/tsql-md.md)] variable, or parameter, to bring relational and XML data together. To do this, you use the **sql:variable** function.  
  
 The SQL value will be mapped to a corresponding XQuery value and its type will be an XQuery base type that is equivalent to the corresponding SQL type.  
  
 You can only refer to an **xml** instance in the context of the source expression of an XML-DML insert statement; otherwise you cannot refer to values that are of type **xml** or a common language runtime (CLR) user-defined type.  
  
## Examples  
  
### A. Using the sql:variable() function to bring a Transact-SQL variable value into XML  
 The following example constructs an XML instance that made up of the following:  
  
-   A value (`ProductID`) from a non-XML column. The [sql:column() function](../xquery/xquery-extension-functions-sql-column.md) is used to bind this value in the XML.  
  
-   A value (`ListPrice`) from a non-XML column from another table. Again, `sql:column()` is used to bind this value in the XML.  
  
-   A value (`DiscountPrice`) from a [!INCLUDE[tsql](../includes/tsql-md.md)] variable. The `sql:variable()` method is used to bind this value into the XML.  
  
-   A value (`ProductModelName`) from an **xml** type column, to make the query more interesting.  
  
 This is the query:  
  
```sql
DECLARE @price money  
  
SET @price=2500.00  
SELECT ProductID, Production.ProductModel.ProductModelID,CatalogDescription.query('  
declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
  
       <Product   
           ProductID="{ sql:column("Production.Product.ProductID") }"  
           ProductModelID= "{ sql:column("Production.Product.ProductModelID") }"  
           ProductModelName="{/pd:ProductDescription[1]/@ProductModelName }"  
           ListPrice="{ sql:column("Production.Product.ListPrice") }"  
           DiscountPrice="{ sql:variable("@price") }"  
        />')   
FROM Production.Product   
JOIN Production.ProductModel  
ON Production.Product.ProductModelID = Production.ProductModel.ProductModelID  
WHERE ProductID=771  
```  
  
 Note the following from the previous query:  
  
-   The XQuery inside the `query()` method constructs the XML.  
  
-   The `namespace` keyword is used to define a namespace prefix in the [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md). This is done because the `ProductModelName` attribute value is retrieved from the `CatalogDescription xml` type column, which has a schema associated with it.  
  
 This is the result:  
  
```xml
<Product ProductID="771" ProductModelID="19"   
         ProductModelName="Mountain 100"   
         ListPrice="3399.99" DiscountPrice="2500" />  
```  
  
## See Also  
 [SQL Server XQuery Extension Functions](./xquery-extension-functions-sql-column.md)   
 [Compare Typed XML to Untyped XML](../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)   
 [Create Instances of XML Data](../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
