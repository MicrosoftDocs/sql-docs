---
title: "local-name-from-QName (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:local-name-from-QName function"
  - "local-name-from-QName function"
ms.assetid: fafed718-8c3c-403f-93ee-ec51fc157a6e
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions Related to QNames - local-name-from-QName
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns an xs:NCNAME that represents the local part of QName specified by *$arg*. The result is an empty sequence if *$arg* is the empty sequence.  
  
## Syntax  
  
```  
fn:local-name-from-QName($arg as xs:QName?) as xs:NCName?  
```  
  
## Arguments  
 *$arg*  
 Is the QName that the local name should be extracted from.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
 The following example uses the **local-name-from-QName()** function to retrieve the local name and namespace URI parts from a QName type value. The example performs the following:  
  
-   Creates an XML schema collection.  
  
-   Creates a table with an xml type column. The xml type is typed using the XML schema collection.  
  
-   Stores a sample XML instance in the table. Using the **query()** method of the xml data type, the query expression is executed to retrieve the local name part of the QName type value from the instance.  
  
```sql
DROP TABLE T  
go  
DROP XML SCHEMA COLLECTION SC  
go  
CREATE XML SCHEMA COLLECTION SC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema"  
targetNamespace="QNameXSD" >  
      <element name="root" type="QName" nillable="true"/>  
</schema>'  
go  
  
CREATE TABLE T (xmlCol XML(SC))  
go  
-- following OK  
insert into T values ('<root xmlns="QNameXSD" xmlns:a="https://someURI">a:someLocalName</root>')  
 go  
-- Retrieve the local name.   
SELECT xmlCol.query('declare default element namespace "QNameXSD"; local-name-from-QName(/root[1])')  
FROM T  
-- Result = someLocalName  
-- You can retrieve namespace URI part from the QName using the namespace-uri-from-QName() function  
SELECT xmlCol.query('declare default element namespace "QNameXSD"; namespace-uri-from-QName(/root[1])')  
FROM T  
-- Result = https://someURI  
```  
  
## See Also  
 [Functions Related to QNames &#40;XQuery&#41;](https://msdn.microsoft.com/library/7e07eb26-f551-4b63-ab77-861684faff71)  
  
  
