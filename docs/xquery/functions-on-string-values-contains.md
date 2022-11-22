---
title: "contains Function (XQuery) | Microsoft Docs"
description: Learn about using the contains function in an XQuery to determine whether a specified string value contains the specified substring value.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "contains function (XQuery)"
  - "fn:contains function"
ms.assetid: 2c88c015-04fc-429b-84b2-835596a28b65
author: "rothja"
ms.author: "jroth"
---
# Functions on String Values - contains
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns a value of type xs:boolean indicating whether the value of *$arg1* contains a string value specified by *$arg2*.  
  
## Syntax  
  
```  
  
fn:contains ($arg1 as xs:string?, $arg2 as xs:string?) as xs:boolean?  
```  
  
## Arguments  
 *$arg1*  
 String value to test.  
  
 *$arg2*  
 Substring to look for.  
  
## Remarks  
 If the value of *$arg2* is a zero-length string, the function returns **True**. If the value of *$arg1* is a zero-length string and the value of *$arg2* is not a zero-length string, the function returns **False**.  
  
 If the value of *$arg1* or *$arg2* is the empty sequence, the argument is treated as the zero-length string.  
  
 The contains() function uses XQuery's default Unicode code point collation for the string comparison.  
  
 The substring value specified for *$arg2* has to be less than or equal to 4000 characters. If the value specified is greater than 4000 characters, a dynamic error condition occurs and the contains() function returns an empty sequence instead of a Boolean value of **True** or **False**. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not raise dynamic errors on XQuery expressions.  
  
 In order to get case-insensitive comparisons, the [upper-case](../xquery/functions-on-string-values-upper-case.md) or lower-case functions can be used.  
  
## Supplementary Characters (Surrogate Pairs)  
 The behavior of surrogate pairs in XQuery functions depends on the database compatibility level and, in some cases, on the default namespace URI for functions. For more information, see the section "XQuery Functions Are Surrogate-Aware" in the topic [Breaking Changes to Database Engine Features in SQL Server 2016](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md). Also see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) and [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
 This topic provides XQuery examples against XML instances stored in various xml-type columns in the AdventureWorks database.  
  
### A. Using the contains() XQuery function to search for a specific character string  
 The following query finds products that contain the word Aerodynamic in the summary descriptions. The query returns the ProductID and the <`Summary`> element for such products.  
  
```  
--The product model description document uses  
--namespaces. The WHERE clause uses the exit()  
--method of the xml data type. Inside the exit method,  
--the XQuery contains()function is used to  
--determine whether the <Summary> text contains the word  
--Aerodynamic.   
  
USE AdventureWorks  
GO  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)  
SELECT ProductModelID, CatalogDescription.query('  
      <Prod>  
         { /pd:ProductDescription/@ProductModelID }  
         { /pd:ProductDescription/pd:Summary }  
      </Prod>  
 ') as Result  
FROM Production.ProductModel  
where CatalogDescription.exist('  
   /pd:ProductDescription/pd:Summary//text()  
    [contains(., "Aerodynamic")]') = 1  
```  
  
 Results  
  
 `ProductModelID Result`  
  
 `-------------- ---------`  
  
 `28     <Prod ProductModelID="28">`  
  
 `<pd:Summary xmlns:pd=`  
  
 `"https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">`  
  
 `<p1:p xmlns:p1="http://www.w3.org/1999/xhtml">`  
  
 `A TRUE multi-sport bike that offers streamlined riding and`  
  
 `a revolutionary design. Aerodynamic design lets you ride with`  
  
 `the pros, and the gearing will conquer hilly roads.</p1:p>`  
  
 `</pd:Summary>`  
  
 `</Prod>`  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
