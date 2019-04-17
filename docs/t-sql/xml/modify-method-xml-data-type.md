---
title: "modify() Method (xml Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "modify() method"
  - "modify method"
ms.assetid: 52430735-51f4-46d1-a308-9aecf8648fda
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# modify() Method (xml Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Modifies the contents of an XML document. Use this method to modify the content of an **xml** type variable or column. This method takes an XML DML statement to insert, update, or delete nodes from XML data. The **modify()** method of the **xml** data type can only be used in the SET clause of an UPDATE statement.  
  
## Syntax  
  
```  
  
modify (XML_DML)  
```  
  
## Arguments  
 XML_DML  
 Is a string in XML Data Manipulation Language (DML). The XML document is updated according to this expression.  
  
> [!NOTE]  
>  An error is returned if the **modify()** method is called on a null value or results in a null value.  
  
## Examples  
 Because the **modify()** method requires a string in the XML Data Manipulation Language (DML), the samples for **modify()** are contained in the topics that describe the XML DML statements. For these examples, see [insert &#40;XML DML&#41;](../../t-sql/xml/insert-xml-dml.md), [delete &#40;XML DML&#41;](../../t-sql/xml/delete-xml-dml.md) and [replace value of &#40;XML DML&#41;](../../t-sql/xml/replace-value-of-xml-dml.md).  
  
## See Also  
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)  
  
  
