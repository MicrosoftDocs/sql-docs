---
description: "modify() Method (xml Data Type)"
title: modify() Method (xml Data Type)
ms.custom: ""
ms.date: "07/26/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "modify() method"
  - "modify method"
ms.assetid: 52430735-51f4-46d1-a308-9aecf8648fda
author: MikeRayMSFT
ms.author: mikeray
---
# modify() Method (xml Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Modifies the contents of an XML document. Use this method to modify the content of an **xml** type variable or column. This method takes an XML DML statement to insert, update, or delete nodes from XML data. The **modify()** method of the **xml** data type can only be used in the SET clause of an UPDATE statement.  
  
## Syntax  
  
```syntaxsql
modify (XML_DML)  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  
