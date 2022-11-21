---
title: "true Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery function true() that returns the Boolean value True.
ms.custom: ""
ms.date: "08/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:true function"
  - "true function"
ms.assetid: 318e370d-0444-4812-afe4-307df7ef9f3b
author: "rothja"
ms.author: "jroth"
---
# Boolean Constructor Functions - true (XQuery)
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  Returns the xs:boolean value True. This is equivalent to `xs:boolean("1")`.  
  
## Syntax  
  
```  
fn:true() as xs:boolean  
```  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the true() XQuery Boolean function  
 The following example queries an untyped **xml** variable. The expression in the **value()** method returns Boolean **true()** if "aaa" is the attribute value. The **value()** method of the **xml** data type converts the Boolean value into a bit and returns it.  
  
```  
DECLARE @x XML  
SET @x= '<ROOT><elem attr="aaa">bbb</elem></ROOT>'  
select @x.value(' if ( (/ROOT/elem/@attr)[1] eq "aaa" ) then fn:true() else fn:false() ', 'bit')  
go  
-- result = 1  
```  
  
 In the following example, the query is specified against a typed **xml** column. The `if` expression checks the typed Boolean value of the <`ROOT`> element and returns the constructed XML, accordingly. The example performs the following:  
  
-   Creates an XML schema collection that defines the <`ROOT`> element of the xs:boolean type.  
  
-   Creates a table with a typed **xml** column by using the XML schema collection.  
  
-   Saves an XML instance in the column and queries it.  
  
```  
-- Drop table if exist  
--DROP TABLE T  
--go  
DROP XML SCHEMA COLLECTION SC  
go  
CREATE XML SCHEMA COLLECTION SC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
targetNamespace="QNameXSD" >  
      <element name="ROOT" type="boolean" nillable="true"/>  
</schema>'  
go  
CREATE TABLE T (xmlCol XML(SC))  
go  
-- following OK  
insert into T values ('<ROOT xmlns="QNameXSD">true</ROOT>')  
 go  
-- Retrieve the local name.   
SELECT xmlCol.query('declare namespace a="QNameXSD";   
   if (/a:ROOT[1] eq true()) then  
       <result>Found boolean true</result>  
   else  
       <result>Found boolean false</result>')  
  
FROM T  
-- result = <result>Found boolean true</result>  
-- Clean up  
DROP TABLE T  
go  
DROP XML SCHEMA COLLECTION SC  
go  
```  
  
## See Also  
 [Boolean Constructor Functions &#40;XQuery&#41;](./xquery-functions-against-the-xml-data-type.md)  
  
