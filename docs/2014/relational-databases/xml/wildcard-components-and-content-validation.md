---
title: "Wildcard Components and Content Validation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "wildcard components [XML]"
  - "content validation [XML]"
ms.assetid: ffa7d974-3645-446c-8425-f0b22b6b060a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Wildcard Components and Content Validation
  Wildcard components are used to increase flexibility in what is allowed to appear in a content model. These components are supported in the XSD language in the following ways:  
  
-   Element wildcard components. These are represented by the **\<xsd:any>** element.  
  
-   Attribute wildcard components. These are represented by the **\<xsd:anyAttribute>** element.  
  
 Both wildcard character elements, **\<xsd:any>** and **\<xsd:anyAttribute>**, support the use of a **processContents** attribute. This lets you specify a value that indicates how XML applications handle the validation of document content associated with these wildcard character elements. These are the different values and their effect:  
  
-   The **strict** value specifies that the contents are fully validated.  
  
-   The **skip** value specifies that the contents are not validated.  
  
-   The **lax** value specifies that only elements and attributes for which schema definitions are available are validated.  
  
## Lax Validation and xs:anyType Elements  
 The XML Schema specification uses **lax** validation for elements of the **anyType** type. Because [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] did not support lax validation, strict validation was applied for elements of the **anyType**. Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], lax validation is supported. Content of elements of type **anyType** will be validated using lax validation.  
  
 The following example illustrates the lax validation. The schema element `e` is of the **anyType** type. The example creates typed **xml** variables and illustrates the lax validation of the element of the **anyType** type.  
  
```  
CREATE XML SCHEMA COLLECTION SC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema"   
        targetNamespace="http://ns">  
   <element name="e" type="anyType"/>  
   <element name="a" type="byte"/>  
   <element name="b" type="string"/>  
 </schema>'  
GO  
```  
  
 The following example succeeds, because the validation of `<e>` is successful:  
  
```  
DECLARE @var XML(SC)  
SET @var = '<e xmlns="http://ns"><a>1</a><b>data</b></e>'  
GO  
```  
  
 The following example succeeds. The instance is accepted, even though no element `<c>` is defined in the schema:  
  
```  
DECLARE @var XML(SC)  
SET @var = '<e xmlns="http://ns"><a>1</a><c>Wrong</c><b>data</b></e>'  
GO  
```  
  
 The XML instance in the following example is rejected, because the definition of the `<a>` element does not allow a string value.  
  
```  
DECLARE @var XML(SC)  
SET @var = '<e xmlns="http://ns"><a>Wrong</a><b>data</b></e>'  
SELECT @var  
GO  
```  
  
## See Also  
 [Requirements and Limitations for XML Schema Collections on the Server](requirements-and-limitations-for-xml-schema-collections-on-the-server.md)  
  
  
