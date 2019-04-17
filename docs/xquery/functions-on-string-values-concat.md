---
title: "concat Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:concat function"
  - "concat function [XQuery]"
ms.assetid: d50afd20-a297-445e-be9e-13b48017e7ca
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions on String Values - concat
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Accepts zero or more strings as arguments and returns a string created by concatenating the values of each of these arguments.  
  
## Syntax  
  
```  
  
fn:concat ($string as xs:string?  
           ,$string as xs:string?  
           [, ...]) as xs:string  
```  
  
## Arguments  
 *$string*  
 Optional string to concatenate.  
  
## Remarks  
 The function requires at least two arguments. If an argument is an empty sequence, it is treated as the zero-length string.  
  
## Supplementary Characters (Surrogate Pairs)  
 The behavior of surrogate pairs in XQuery functions depends on the database compatibility level and, in some cases, on the default namespace URI for functions. For more information, see the section "XQuery Functions Are Surrogate-Aware" in the topic [Breaking Changes to Database Engine Features in SQL Server 2016](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md). Also see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) and [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks sample database.  
  
### A. Using the concat() XQuery function to concatenate strings  
 For a specific product model, this query returns a string created by concatenating the warranty period and warranty description. In the catalog description document, the <`Warranty`> element is made up of <`WarrantyPeriod`> and <`Description`> child elements.  
  
```  
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)  
SELECT CatalogDescription.query('  
    <Product   
        ProductModelID= "{ (/pd:ProductDescription/@ProductModelID)[1] }"  
        ProductModelName = "{ sql:column("PD.Name") }" >  
        {   
          concat( string((/pd:ProductDescription/pd:Features/wm:Warranty/wm:WarrantyPeriod)[1]), "-",  
                  string((/pd:ProductDescription/pd:Features/wm:Warranty/wm:Description)[1]))   
         }   
     </Product>  
 ') as Result  
FROM Production.ProductModel PD  
WHERE  PD.ProductModelID=28  
  
```  
  
 Note the following from the previous query:  
  
-   In the SELECT clause, CatalogDescription is an **xml** type column. Therefore, the [query() method (XML data type)](../t-sql/xml/query-method-xml-data-type.md), Instructions.query(), is used. The XQuery statement is specified as the argument to the query method.  
  
-   The document against which the query is executed uses namespaces. Therefore, the **namespace** keyword is used to define the prefix for the namespace. For more information, see [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md).  
  
 This is the result:  
  
```  
<Product ProductModelID="28" ProductModelName="Road-450">1 year-parts and labor</Product>  
```  
  
 The previous query retrieves information for a specific product. The following query retrieves the same information for all the products for which XML catalog descriptions are stored. The **exist()** method of the **xml** data type in the WHERE clause returns True if the XML document in the rows has a <`ProductDescription`> element.  
  
```  
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)  
  
SELECT CatalogDescription.query('  
    <Product   
        ProductModelID= "{ (/pd:ProductDescription/@ProductModelID)[1] }"   
        ProductName = "{ sql:column("PD.Name") }" >  
        {   
          concat( string((/pd:ProductDescription/pd:Features/wm:Warranty/wm:WarrantyPeriod)[1]), "-",  
                  string((/pd:ProductDescription/pd:Features/wm:Warranty/wm:Description)[1]))   
         }   
     </Product>  
 ') as Result  
FROM Production.ProductModel PD  
WHERE CatalogDescription.exist('//pd:ProductDescription ') = 1  
  
```  
  
 Note that the Boolean value returned by the **exist()** method of the **xml** type is compared with 1.  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **concat()** function in SQL Server only accepts values of type xs:string. Other values have to be explicitly cast to xs:string or xdt:untypedAtomic.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
