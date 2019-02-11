---
title: "query() Method (xml Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "query method"
  - "query() method"
ms.assetid: f48f6f7b-219f-463a-bf36-bc10f21afaeb
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# query() Method (xml Data Type)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Specifies an XQuery against an instance of the **xml** data type. The result is of **xml** type. The method returns an instance of untyped XML.  
  
## Syntax  
  
```  
  
query ('XQuery')  
```  
  
## Arguments  
 XQuery  
 Is a string , an XQuery expression, that queries for XML nodes such as elements, attributes, in an XML instance.  
  
## Examples  
 This section provides examples of using the query() method of the **xml** data type.  
  
### A. Using the query() method against an xml type variable  
 The following example declares a variable **@myDoc** of **xml** type and assigns an XML instance to it. The **query()** method is then used to specify an XQuery against the document.  
  
 The query retrieves the <`Features`> child element of the <`ProductDescription`> element:  
  
```  
declare @myDoc xml  
set @myDoc = '<Root>  
<ProductDescription ProductID="1" ProductName="Road Bike">  
<Features>  
  <Warranty>1 year parts and labor</Warranty>  
  <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>  
</Features>  
</ProductDescription>  
</Root>'  
SELECT @myDoc.query('/Root/ProductDescription/Features')  
```  
  
 This is the result:  
  
```  
<Features>  
  <Warranty>1 year parts and labor</Warranty>  
  <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>  
</Features>        
```  
  
### B. Using the query() method against an XML type column  
 In the following example, the **query()** method is used to specify an XQuery against the **CatalogDescription** column of **xml** type in the **AdventureWorks** database:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
<Product ProductModelID="{ /PD:ProductDescription[1]/@ProductModelID }" />  
') as Result  
FROM Production.ProductModel  
where CatalogDescription.exist('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
     /PD:ProductDescription/PD:Features/wm:Warranty ') = 1  
```  
  
 Note the following from the previous query:  
  
-   The CatalogDescription column is a typed **xml** column . This means it has a schema collection associated with it. In the [XQuery Prolog](../../xquery/modules-and-prologs-xquery-prolog.md), the **namespace** keyword is used to define the prefix that is later used in the query body.  
  
-   The **query()** method constructs XML, a <`Product`> element that has a **ProductModelID** attribute, in which the **ProductModelID** attribute value is retrieved from the database. For more information about XML construction, see [XML Construction &#40;XQuery&#41;](../../xquery/xml-construction-xquery.md).  
  
-   The [exist() method (XML data type)](../../t-sql/xml/exist-method-xml-data-type.md) in the WHERE clause is used to find only rows that contains the <`Warranty`> element in the XML. Again, the **namespace** keyword is used to define two namespace prefixes.  
  
 This is the partial result:  
  
```  
<Product ProductModelID="19"/>   
<Product ProductModelID="23"/>   
...  
```  
  
 Note that the query() and exist() methods both declare the PD prefix. In these cases, you can use WITH XMLNAMESPACES to first define the prefixes and use it in the query.  
  
```  
WITH XMLNAMESPACES 
(  
   'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS PD,  
   'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS WM
)  
SELECT CatalogDescription.query('<Product ProductModelID="{ /PD:ProductDescription[1]/@ProductModelID }" />')
       AS Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('/PD:ProductDescription/PD:Features/WM:Warranty ') = 1;

```  
  
## See Also  
 [Add Namespaces to Queries with WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)   
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
