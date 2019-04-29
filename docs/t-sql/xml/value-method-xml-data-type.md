---
title: "value() Method (xml Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "value method"
  - "value() method"
ms.assetid: 298a7361-dc9a-4902-9b1e-49a093cd831d
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# value() Method (xml Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Performs an XQuery against the XML and returns a value of SQL type. This method returns a scalar value.  
  
 You typically use this method to extract a value from an XML instance stored in an **xml** type column, parameter, or variable. In this way, you can specify SELECT queries that combine or compare XML data with data in non-XML columns.  
  
## Syntax  
  
```  
  
value (XQuery, SQLType)  
```  
  
## Arguments  
 *XQuery*  
 Is the *XQuery* expression, a string literal, that retrieves data inside the XML instance. The XQuery must return at most one value. Otherwise, an error is returned.  
  
 *SQLType*  
 Is the preferred SQL type, a string literal, to be returned. The return type of this method matches the *SQLType* parameter. *SQLType* cannot be an **xml** data type, a common language runtime (CLR) user-defined type, **image**, **text**, **ntext**, or **sql_variant** data type. *SQLType* can be an SQL, user-defined data type.  
  
 The **value()** method uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] CONVERT operator implicitly and tries to convert the result of the XQuery expression, the serialized string representation, from XSD type to the corresponding SQL type specified by [!INCLUDE[tsql](../../includes/tsql-md.md)] conversion. For more information about type casting rules for CONVERT, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
> [!NOTE]  
>  For performance reasons, instead of using the **value()** method in a predicate to compare with a relational value, use **exist()** with **sql:column()**. This is shown in example D that follows.  
  
## Examples  
  
### A. Using the value() method against an xml type variable  
 In the following example, an XML instance is stored in a variable of `xml` type. The `value()` method retrieves the `ProductID` attribute value from the XML. The value is then assigned to an `int` variable.  
  
```  
DECLARE @myDoc xml  
DECLARE @ProdID int  
SET @myDoc = '<Root>  
<ProductDescription ProductID="1" ProductName="Road Bike">  
<Features>  
  <Warranty>1 year parts and labor</Warranty>  
  <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>  
</Features>  
</ProductDescription>  
</Root>'  
  
SET @ProdID =  @myDoc.value('(/Root/ProductDescription/@ProductID)[1]', 'int' )  
SELECT @ProdID  
```  
  
 Value 1 is returned as a result.  
  
 Although there is only one `ProductID` attribute in the XML instance, the static typing rules require you to explicitly specify that the path expression returns a singleton. Therefore, the additional `[1]` is specified at the end of the path expression. For more information about static typing, see [XQuery and Static Typing](../../xquery/xquery-and-static-typing.md).  
  
### B. Using the value() method to retrieve a value from an xml type column  
 The following query is specified against an **xml** type column (`CatalogDescription`) in the `AdventureWorks` database. The query retrieves `ProductModelID` attribute values from each XML instance stored in the column.  
  
```  
SELECT CatalogDescription.value('             
    declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";             
       (/PD:ProductDescription/@ProductModelID)[1]', 'int') AS Result             
FROM Production.ProductModel             
WHERE CatalogDescription IS NOT NULL             
ORDER BY Result desc             
```  
  
 Note the following from the previous query:  
  
-   The `namespace` keyword is used to define a namespace prefix.  
  
-   Per static typing requirements, `[1]` is added at the end of the path expression in the `value()` method to explicitly indicate that the path expression returns a singleton.  
  
 This is the partial result:  
  
```  
-----------  
35           
34           
...  
```  
  
### C. Using the value() and exist() methods to retrieve values from an xml type column  
 The following example shows using both the `value()` method and the [exist() method](../../t-sql/xml/exist-method-xml-data-type.md) of the **xml** data type. The `value()` method is used to retrieve `ProductModelID` attribute values from the XML. The `exist()` method in the `WHERE` clause is used to filter the rows from the table.  
  
 The query retrieves product model IDs from XML instances that include warranty information (the <`Warranty`> element) as one of the features. The condition in the `WHERE` clause uses the `exist()` method to retrieve only the rows satisfying this condition.  
  
```  
SELECT CatalogDescription.value('  
     declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
           (/PD:ProductDescription/@ProductModelID)[1] ', 'int') as Result  
FROM  Production.ProductModel  
WHERE CatalogDescription.exist('  
     declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
  
     /PD:ProductDescription/PD:Features/wm:Warranty ') = 1  
```  
  
 Note the following from the previous query:  
  
-   The `CatalogDescription` column is a typed XML column. This means that it has a schema collection associated with it. In the [XQuery Prolog](../../xquery/modules-and-prologs-xquery-prolog.md), the namespace declaration is used to define the prefix that is used later in the query body.  
  
-   If the `exist()` method returns a `1` (True), it indicates that the XML instance includes the <`Warranty`> child element as one of the features.  
  
-   The `value()` method in the `SELECT` clause then retrieves the `ProductModelID` attribute values as integers.  
  
 This is the partial result:  
  
```  
Result       
-----------  
19           
23           
...  
```  
  
### D. Using the exist() method instead of the value() method  
 For performance reasons, instead of using the `value()` method in a predicate to compare with a relational value, use `exist()` with `sql:column()`. For example:  
  
```  
CREATE TABLE T (c1 int, c2 varchar(10), c3 xml)  
GO  
  
SELECT c1, c2, c3   
FROM T  
WHERE c3.value( '/root[1]/@a', 'integer') = c1  
GO  
```  
  
 This can be written in the following way:  
  
```  
SELECT c1, c2, c3   
FROM T  
WHERE c3.exist( '/root[@a=sql:column("c1")]') = 1  
GO  
```  
  
## See Also  
 [Add Namespaces to Queries with WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)   
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
