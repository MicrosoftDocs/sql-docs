---
title: "Canonical Forms and Pattern Restrictions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "pattern restrictions"
  - "canonical forms"
ms.assetid: 088314ec-7d0b-4a05-8a33-f35da5bfe59c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Canonical Forms and Pattern Restrictions
  The XSD pattern facet allows for the restriction of the lexical space of simple types. When a pattern restriction is put on a type for which there is more than one possible lexical representation, some values could cause unexpected behavior upon validation.  
  
 This behavior occurs because lexical representations of these values are not stored in the database. Therefore, the values are converted to their canonical representations when serialized as output. If a document contains a value whose canonical form does not comply with the pattern restriction for its type, the document is rejected if a user tries to reinsert it.  
  
 To prevent this, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects any XML document that contains values that cannot be reinserted, because of the violation of pattern restrictions by their canonical forms. For example, the value "33.000" does not validate against a type derived from **xs:decimal** with a pattern restriction of "33\\.0+". Although "33.000" complies with this pattern, the canonical form, "33", does not.  
  
 Therefore, you should be careful when you apply pattern facets to types derived from the following primitive types: `boolean`, `decimal`, `float`, `double`, `dateTime`, `time`, `date`, `hexBinary`, and `base64Binary`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] issues a warning when you add any such components to a schema collection.  
  
 Imprecise serialization of floating-point values has a similar problem. Because of the floating-point serialization algorithm used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it is possible for similar values to share the same canonical form. When a floating-point value is serialized and then reinserted, its value may change slightly. In rare cases, this may result in a value that violates any of the following facets for its type on reinsertion: **enumeration**, **minInclusive**, **minExclusive**, **maxInclusive**, or **maxExclusive**. To prevent this, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects any values of types derived from `xs:float` or `xs:double` that cannot be serialized and reinserted.  
  
## See Also  
 [Requirements and Limitations for XML Schema Collections on the Server](requirements-and-limitations-for-xml-schema-collections-on-the-server.md)  
  
  
