---
title: "exist() Method (xml Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "exist() method"
  - "exist method"
ms.assetid: a55b75e0-0a17-4787-a525-9b095410f7af
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# exist() Method (xml Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a **bit** that represents one of the following conditions:  
  
-   1, representing True, if the XQuery expression in a query returns a nonempty result. That is, it returns at least one XML node.  
  
-   0, representing False, if it returns an empty result.  
  
-   NULL if the **xml** data type instance against which the query was executed contains NULL.  
  
## Syntax  
  
```  
  
exist (XQuery)   
```  
  
## Arguments  
 XQuery  
 Is an XQuery expression, a string literal.  
  
## Remarks  
  
> [!NOTE]  
>  The **exist()** method returns 1 for the XQuery expression that returns a nonempty result. If you specify the **true()** or **false()** functions inside the **exist()** method, the **exist()** method will return 1, because the functions **true()** and **false()** return Boolean True and False, respectively. That is, they return a nonempty result). Therefore, **exist()** will return 1 (True), as shown in the following example:  
  
```  
declare @x xml;  
set @x='';  
select @x.exist('true()');   
```  
  
## Examples  
 The following examples show how to specify the **exist()** method.  
  
### Example: Specifying the exist() method against an xml type variable  
 In the following example, @x is an **xml** type variable (untyped xml) and @f is an integer type variable that stores the value returned by the **exist()** method. The **exist()** method returns True (1) if the date value stored in the XML instance is `2002-01-01`.  
  
```  
declare @x xml;  
declare @f bit;  
set @x = '<root Somedate = "2002-01-01Z"/>';  
set @f = @x.exist('/root[(@Somedate cast as xs:date?) eq xs:date("2002-01-01Z")]');  
select @f;  
```  
  
 In comparing dates in the **exist()** method, note the following:  
  
-   The code `cast as xs:date?` is used to cast the value to **xs:date** type for purposes of comparison.  
  
-   The value of the **@Somedate** attribute is untyped. In comparing this value, it is implicitly cast to the type on the right side of the comparison, the **xs:date** type.  
  
-   Instead of **cast as xs:date()**, you can use the **xs:date()** constructor function. For more information, see [Constructor Functions &#40;XQuery&#41;](../../xquery/constructor-functions-xquery.md).  
  
 The following example is similar to the previous one, except it has a <`Somedate`> element.  
  
```  
DECLARE @x xml;  
DECLARE @f bit;  
SET @x = '<Somedate>2002-01-01Z</Somedate>';  
SET @f = @x.exist('/Somedate[(text()[1] cast as xs:date ?) = xs:date("2002-01-01Z") ]')  
SELECT @f;  
```  
  
 Note the following from the previous query:  
  
-   The **text()** method returns a text node that contains the untyped value `2002-01-01`. (The XQuery type is **xdt:untypedAtomic**.) You must explicitly cast this typed value from **x** to **xsd:date**, because implicit casting is not supported in this case.  
  
### Example: Specifying the exist() method against a typed xml variable  
 The following example illustrates the use of the **exist()** method against an **xml** type variable. It is a typed XML variable, because it specifies the schema namespace collection name, `ManuInstructionsSchemaCollection`.  
  
 In the example, a manufacturing instructions document is first assigned to this variable and then the **exist()** method is used to find whether the document includes a <`Location`> element whose **LocationID** attribute value is 50.  
  
 The **exist()** method specified against the @x variable returns 1 (True) if the manufacturing instructions document includes a <`Location`> element that has `LocationID=50`. Otherwise, the method returns 0 (False).  
  
```  
DECLARE @x xml (Production.ManuInstructionsSchemaCollection);  
SELECT @x=Instructions  
FROM Production.ProductModel  
WHERE ProductModelID=67;  
--SELECT @x  
DECLARE @f int;  
SET @f = @x.exist(' declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
    /AWMI:root/AWMI:Location[@LocationID=50]  
');  
SELECT @f;  
```  
  
### Example: Specifying the exist() method against an xml type column  
 The following query retrieves product model IDs whose catalog descriptions do not include the specifications, <`Specifications`> element:  
  
```  
SELECT ProductModelID, CatalogDescription.query('  
declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
    <Product   
        ProductModelID= "{ sql:column("ProductModelID") }"   
        />  
') AS Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('  
    declare namespace  pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     /pd:ProductDescription[not(pd:Specifications)]'  
    ) = 1;  
```  
  
 Note the following from the previous query:  
  
-   The WHERE clause selects only those rows from the **ProductDescription** table that satisfy the condition specified against the **CatalogDescription xml** type column.  
  
-   The **exist()** method in the WHERE clause returns 1 (True) if the XML does not include any <`Specifications`> element. Note the use of the [not() function (XQuery)](../../xquery/functions-on-boolean-values-not-function.md).  
  
-   The [sql:column() function (XQuery)](../../xquery/xquery-extension-functions-sql-column.md) function is used to bring in the value from a non-XML column.  
  
-   This query returns an empty rowset.  
  
 The query specifies **query()** and **exist()** methods of the xml data type and both these methods declare the same namespaces in the query prolog. In this case, you may want to use WITH XMLNAMESPACES to declare the prefix and use it in the query.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)  
SELECT ProductModelID, CatalogDescription.query('  
    <Product   
        ProductModelID= "{ sql:column("ProductModelID") }"   
        />  
') AS Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('  
     /pd:ProductDescription[not(pd:Specifications)]'  
    ) = 1;  
```  
  
## See Also  
 [Add Namespaces to Queries with WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)   
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
