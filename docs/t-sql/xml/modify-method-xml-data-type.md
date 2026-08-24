---
title: modify() Method (xml Data Type)
description: "modify() Method (xml Data Type)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "modify() method"
  - "modify method"
dev_langs:
  - "TSQL"
---
# modify() Method (xml Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Modifies the contents of an XML document. Use this method to modify the content of an **xml** type variable or column. This method takes an XML DML statement to insert, update, or delete nodes from XML data. The **modify()** method of the **xml** data type can only be used in the SET clause of an UPDATE statement.  
  
## Syntax  
  
```syntaxsql
modify (XML_DML)  
```  
  
## Arguments
 XML_DML  
 Is a string in XML Data Manipulation Language (DML). The XML document is updated according to this expression.  
  
> [!NOTE]  
>  An error is returned if the **modify()** method is called on a null value or results in a null value.  
  
## Examples  
 Because the **modify()** method requires a string in the XML Data Manipulation Language (DML), the samples for **modify()** are contained in the topics that describe the XML DML statements. For these examples, see [insert &#40;XML DML&#41;](../../t-sql/xml/insert-xml-dml.md), [delete &#40;XML DML&#41;](../../t-sql/xml/delete-xml-dml.md) and [replace value of &#40;XML DML&#41;](../../t-sql/xml/replace-value-of-xml-dml.md).  
  
## Related content

- [Create instances of XML data](../../relational-databases/xml/create-instances-of-xml-data.md)
- [xml Data Type Methods](xml-data-type-methods.md)
- [XML Data Modification Language (XML DML)](xml-data-modification-language-xml-dml.md)
