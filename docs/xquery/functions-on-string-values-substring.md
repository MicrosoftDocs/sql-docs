---
title: "substring Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery function substring() that returns the specified portion of a source string.
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "substring function [XQuery]"
  - "fn:substring function"
ms.assetid: 2b3b8651-de51-46dc-af82-c86c45eac871
author: "rothja"
ms.author: "jroth"
---
# Functions on String Values - substring
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns part of the value of *$sourceString*, starting at the position indicated by the value of *$startingLoc,* and continues for the number of characters indicated by the value of *$length*.  
  
## Syntax  
  
```  
  
fn:substring($sourceString as xs:string?,  
                          $startingLoc as xs:decimal?) as xs:string?  
  
fn:substring($sourceString as xs:string?,  
                          $startingLoc as xs:decimal?,  
                          $length as xs:decimal?) as xs:string?  
```  
  
## Arguments  
 *$sourceString*  
 Source string.  
  
 *$startingLoc*  
 Starting point in the source string from which the substring starts. If this value is negative or 0, only those characters in positions greater than zero are returned. If it is greater than the length of the *$sourceString*,  the zero-length string is returned.  
  
 *$length*  
 [optional] Number of characters to retrieve. If not specified, it returns all the characters from the location specified in *$startingLoc* up to the end of string.  
  
## Remarks  
 The three-argument version of the function returns the characters in `$sourceString` whose position `$p` obeys:  
  
 `fn:round($startingLoc) <= $p < fn:round($startingLoc) + fn:round($length)`  
  
 The value of *$length* can be greater than the number of characters in the value of *$sourceString* following the start position. In this case, the substring returns the characters up to the end of *$sourceString*.  
  
 The first character of a string is located at position 1.  
  
 If the value of *$sourceString* is the empty sequence, it is handled as the zero-length string. Otherwise, if either *$startingLoc* or *$length* is the empty sequence, the empty sequence is returned.  
  
## Supplementary Characters (Surrogate Pairs)  
 The behavior of surrogate pairs in XQuery functions depends on the database compatibility level and, in some cases, on the default namespace URI for functions. For more information, see the section "XQuery Functions Are Surrogate-Aware" in the topic [Breaking Changes to Database Engine Features in SQL Server 2016](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md). Also see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) and [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
## Implementation Limitations  
 SQL Server requires the *$startingLoc* and *$length parameters* to be of type xs:decimal instead of xs:double.  
  
 SQL Server allows *$startingLoc* and *$length* to be the empty sequence, because the empty sequence is a possible value as a result of dynamic errors being mapped to ().  
  
## Examples  
 This topic provides XQuery examples against XML instances stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
### A. Using the substring() XQuery function to retrieve partial summary product-model descriptions  
 The query retrieves the first 50 characters of the text that describes the product model, the <`Summary`> element in the document.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)  
SELECT ProductModelID, CatalogDescription.query('  
    <Prod>{ substring(string((/pd:ProductDescription/pd:Summary)[1]), 1, 50) }</Prod>  
 ') as Result  
FROM Production.ProductModel  
where CatalogDescription.exist('/pd:ProductDescription')  = 1;  
```  
  
 Note the following from the previous query:  
  
-   The **string()** function returns the string value of the<`Summary`> element. This function is used, because the <`Summary`> element contains both the text and subelements (html formatting elements), and because you will skip these elements and retrieve all the text.  
  
-   The **substring()** function retrieves the first 50 characters from the string value retrieved by the **string()**.  
  
 This is a partial result:  
  
```  
ProductModelID Result  
-------------- ----------------------------------------------------  
19      <Prod>Our top-of-the-line competition mountain bike.</Prod>   
23      <Prod>Suitable for any type of riding, on or off-roa</Prod>  
...  
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
