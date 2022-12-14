---
title: "string-length Function (XQuery) | Microsoft Docs"
description: Learn how to use the XQuery function string-length().
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "string-length function"
  - "fn:string-length function"
ms.assetid: 7cd69c8b-cf2c-478c-b9a3-e0e14e1aa8aa
author: "rothja"
ms.author: "jroth"
---
# Functions on String Values - string-length
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns the length of the string in characters.  
  
## Syntax  
  
```  
  
fn:string-length() as xs:integer  
fn:string-length($arg as xs:string?) as xs:integer  
```  
  
## Arguments  
 *$arg*  
 Source string whose length is to be computed.  
  
## Remarks  
 If the value of *$arg* is an empty sequence, an **xs:integer** value of 0 is returned.  
  
 The behavior of surrogate pairs in XQuery functions depends on the database compatibility level. If the compatibility level is 110 or later, each surrogate pair is counted as a single character. For earlier compatibility levels, they are counted as two characters. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) and [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
 If the value contains a 4-byte Unicode character that is represented by two surrogate characters, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will count the surrogate characters individually.  
  
 The **string-length()** without a parameter can only be used inside a predicate. For example, the following query returns the <`ROOT`> element:  
  
```  
DECLARE @x xml;  
SET @x='<ROOT>Hello</ROOT>';  
SELECT @x.query('/ROOT[string-length()=5]');  
```  
  
## Supplementary Characters (Surrogate Pairs)  
 The behavior of surrogate pairs in XQuery functions depends on the database compatibility level and, in some cases, on the default namespace URI for functions. For more information, see the section "XQuery Functions Are Surrogate-Aware" in the topic [Breaking Changes to Database Engine Features in SQL Server 2016](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md). Also see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) and [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
 This topic provides XQuery examples against XML instances stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the string-length() XQuery function to retrieve products with long summary descriptions  
 For products whose summary description is greater than 50 characters, the following query retrieves the product ID, the length of the summary description, and the summary itself, the <`Summary`> element.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' as pd)  
SELECT CatalogDescription.query('  
      <Prod ProductID= "{ /pd:ProductDescription[1]/@ProductModelID }" >  
       <LongSummary SummaryLength =   
           "{string-length(string( (/pd:ProductDescription/pd:Summary)[1] )) }" >  
           { string( (/pd:ProductDescription/pd:Summary)[1] ) }  
       </LongSummary>  
      </Prod>  
 ') as Result  
FROM Production.ProductModel  
WHERE CatalogDescription.value('string-length( string( (/pd:ProductDescription/pd:Summary)[1]))', 'decimal') > 200;  
```  
  
 Note the following from the previous query:  
  
-   The condition in the WHERE clause retrieves only the rows where the summary description stored in the XML document is longer than 200 characters. It uses the [value() method (XML data type)](../t-sql/xml/value-method-xml-data-type.md).  
  
-   The SELECT clause just constructs the XML that you want. It uses the [query() method (XML data type)](../t-sql/xml/query-method-xml-data-type.md) to construct the XML and specify the necessary XQuery expression to retrieve data from the XML document.  
  
 This is a partial result:  
  
```  
Result  
-------------------  
<Prod ProductID="19">  
      <LongSummary SummaryLength="214">Our top-of-the-line competition   
             mountain bike. Performance-enhancing options include the  
             innovative HL Frame, super-smooth front suspension, and   
             traction for all terrain.  
      </LongSummary>  
</Prod>  
...  
```  
  
### B. Using the string-length() XQuery function to retrieve products whose warranty descriptions are short  
 For products whose warranty descriptions are less than 20 characters long, the following query retrieves XML that includes the product ID, length, warranty description, and the <`Warranty`> element itself.  
  
 Warranty is one of the product features. An optional <`Warranty`> child element follows after the <`Features`> element.  
  
```  
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)  
  
SELECT CatalogDescription.query('  
      for   $ProdDesc in /pd:ProductDescription,  
            $pf in $ProdDesc/pd:Features/wm:Warranty  
      where string-length( string(($pf/wm:Description)[1]) ) < 20  
      return   
          <Prod >  
             { $ProdDesc/@ProductModelID }  
             <ShortFeature FeatureDescLength =   
                             "{string-length( string(($pf/wm:Description)[1]) ) }" >  
                 { $pf }  
             </ShortFeature>  
          </Prod>  
     ') as Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('/pd:ProductDescription')=1;  
```  
  
 Note the following from the previous query:  
  
-   **pd** and **wm** are the namespace prefixes used in this query. They identify the same namespaces used in the document that is being queried.  
  
-   The XQuery specifies a nested FOR loop. The outer FOR loop is required, because you want to retrieve the **ProductModelID** attributes of the <`ProductDescription`> element. The inner FOR loop is required, because you want only those products that have warranty feature descriptions that are less than 20 characters long.  
  
 This is the partial result:  
  
```  
Result  
-------------------------  
<Prod ProductModelID="19">  
  <ShortFeature FeatureDescLength="15">  
    <wm:Warranty   
       xmlns:wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
      <wm:WarrantyPeriod>3 years</wm:WarrantyPeriod>  
      <wm:Description>parts and labor</wm:Description>  
    </wm:Warranty>  
   </ShortFeature>  
</Prod>  
...  
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
