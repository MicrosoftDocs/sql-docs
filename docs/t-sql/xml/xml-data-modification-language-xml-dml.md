---
title: "XML Data Modification Language (XML DML) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "modifying data [SQL Server], XML DML"
  - "XML [SQL Server], DML"
  - "XML DML [SQL Server]"
  - "data modification language [XML DML]"
  - "data modifications [XML DML]"
  - "DML [XML in SQL Server]"
  - "XQuery, XML DML"
  - "xml data type [SQL Server], XML DML"
ms.assetid: 20ce50d2-c07b-4e41-93a7-1380d2cd49cb
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# XML Data Modification Language (XML DML)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The XML Data Modification Language (XML DML) is an extension of the XQuery language. As defined by W3C, the XQuery language lacks the Data Manipulation (DML) part. The XML DML introduced in this topic, and also the XQuery language, provides a fully functional query and data-modification language that you can use against the **xml** data type.  
  
 The XML DML adds the following case-sensitive keywords to XQuery:  
  
-   **insert**  
  
-   **delete**  
  
-   **replace value of**  
  
 As described in [XML Data Type and Columns &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-type-and-columns-sql-server.md), you can create variables and columns of the **xml** type and assign XML documents or fragments to them. To modify or update these XML instances, do the following:  
  
-   Use the [modify() Method xml Data Type)](../../t-sql/xml/modify-method-xml-data-type.md) of the **xml** data type.  
  
-   Specify the appropriate XML DML statements inside the **modify()** method.  
  
 Note that there are some attributes that cannot be inserted, deleted, or have their value modified. For example:  
  
-   For typed or untyped **xml,** the attributes are **xmlns**, **xmlns:\***, and **xml:base**.  
  
-   For typed **xml** only, the attributes are **xsi:nil**, and **xsi:type**.  
  
 Other restrictions include the following:  
  
-   For typed or untyped **xml**, inserting the attribute **xml:base** will fail.  
  
-   For typed **xml**, deleting and modifying the **xsi:nil** attribute will fail. For untyped **xml**, you can delete the attribute or modify its value.  
  
-   For typed **xml**, modifying the value of the **xs:type** attribute will fail. For untyped **xml**, you can modify the attribute value.  
  
 When you modify a typed XML instance, the final format must be a valid instance of that type. Otherwise, a validation error is returned.  
  
## See Also  
 [insert &#40;XML DML&#41;](../../t-sql/xml/insert-xml-dml.md)   
 [delete &#40;XML DML&#41;](../../t-sql/xml/delete-xml-dml.md)   
 [replace value of &#40;XML DML&#41;](../../t-sql/xml/replace-value-of-xml-dml.md)   
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)  
  
  
