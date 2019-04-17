---
title: "xml Data Type Methods | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xml data type [SQL Server], methods"
  - "methods [XML in SQL Server]"
ms.assetid: d112b9c9-be9f-435c-a9e6-d21b65778fb7
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# xml Data Type Methods
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  You can use the **xml** data type methods to query an XML instance stored in a variable or column of **xml** type. The topics in this section describe how to use the **xml** data type methods.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[query&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/query-method-xml-data-type.md)|Describes how to use the query() method to query over an XML instance.|  
|[value&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/value-method-xml-data-type.md)|Describes how to use the value() method to retrieve a value of SQL type from an XML instance.|  
|[exist&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/exist-method-xml-data-type.md)|Describes how to use the exist() method to determine whether a query returns a nonempty result.|  
|[modify&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/modify-method-xml-data-type.md)|Describes how to use the modify() method to specify [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)statements to perform updates.|  
|[nodes&#40;&#41; Method &#40;xml Data Type&#41;](../../t-sql/xml/nodes-method-xml-data-type.md)|Describes how to use the nodes() method to shred XML into multiple rows, which propagates parts of XML documents into rowsets.|  
|[Binding Relational Data Inside XML Data](../../t-sql/xml/binding-relational-data-inside-xml-data.md)|Describes how to bind non-XML data inside XML.|  
|[Guidelines for Using xml Data Type Methods](../../t-sql/xml/guidelines-for-using-xml-data-type-methods.md)|Describes guidelines for using the **xml** data type methods.|  
  
 You call these methods by using the user-defined type method invocation syntax. For example:  
  
```  
SELECT XmlCol.query(' ... ')  
FROM   Table  
```  
  
> [!NOTE]  
>  The **xml** data type methods **query()**, **value()**, and **exist()** return NULL if executed against a NULL XML instance. Also, **modify()** does not return anything, but **nodes()** returns rowsets and an empty rowset with a NULL input.  
  
## See Also  
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)  
  
  
