---
title: "lower-case  Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery function lower-case() that converts each character in a specified string to its lower case equivalent.
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "lower-case Function (XQuery)"
  - "lower-case"
ms.assetid: 5222c4ff-890c-4d57-8506-c065a5ebfd3e
author: "rothja"
ms.author: "jroth"
---
# Functions on String Values - lower-case
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  The lower-case function converts each character in *$arg* to its lower case equivalent. The Microsoft Windows binary case conversion for Unicode code points specifies how characters are converted to lower case. This standard is not identical to the mapping for Unicode code point standard.  
  
## Syntax  
  
```  
  
fn:lower-case($arg as xs:string?) as xs:string  
```  
  
## Arguments  
  
|Term|Definition|  
|-|-|
|*$arg*|The string value to be converted to lower case.|  
  
## Remarks  
 If the value of *$arg* is empty, a zero length string is returned.  
  
## Examples  
  
### A. Changing a string to lower case  
 The following example changes the input string 'abcDEF!@4' to lower case.  
  
```  
DECLARE @x xml = N'abcDEF!@4';  
SELECT @x.value('fn:lower-case(/text()[1])', 'nvarchar(10)');  
```  
  
 [!INCLUDE[ssResult](../includes/ssresult-md.md)]  
  
 `abcdef!@4`  
  
### B. Search for a specific character string  
 This example shows you how to use the lower-case function to perform a case insensitive search.  
  
```  
USE AdventureWorks  
GO  
--WITH XMLNAMESPACES clause specifies the namespace prefix  
--to use.   
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)  
--The XQuery contains() function is used to determine whether  
--any of the text nodes below the <Summary> element contain  
--the word 'frame'. The lower-case() function makes the   
--case insensitive.  
  
SELECT ProductModelID, CatalogDescription.query('  
      <Prod>  
         { /pd:ProductDescription/@ProductModelID }  
         { /pd:ProductDescription/pd:Summary }  
      </Prod>  
 ') as Result  
FROM Production.ProductModel  
where CatalogDescription.exist('  
/pd:ProductDescription/pd:Summary//text()[  
          contains(lower-case(.), "FRAME")]')  = 1  
```  
  
 [!INCLUDE[ssResult](../includes/ssresult-md.md)]  
  
 `ProductModelID Result`  
  
 `-------------- ---------`  
  
 `19     <Prod ProductModelID="19">`  
  
 `<pd:Summary xmlns:pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">`  
  
 `<p1:p xmlns:p1="http://www.w3.org/1999/xhtml">Our top-of-the-line competition mountain bike.`  
  
 `Performance-enhancing options include the innovative HL Frame,`  
  
 `super-smooth front suspension, and traction for all terrain.`  
  
 `</p1:p>`  
  
 `</pd:Summary>`  
  
 `</Prod>`  
  
 `25     <Prod ProductModelID="25">`  
  
 `<pd:Summary xmlns:pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">`  
  
 `<p1:p xmlns:p1="http://www.w3.org/1999/xhtml">This bike is ridden by race winners. Developed with the`  
  
 `Adventure Works Cycles professional race team, it has a extremely light`  
  
 `heat-treated aluminum frame, and steering that allows precision control.`  
  
 `</p1:p>`  
  
 `</pd:Summary>`  
  
 `</Prod>`  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
