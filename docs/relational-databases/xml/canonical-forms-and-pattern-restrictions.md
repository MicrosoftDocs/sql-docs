---
title: "Canonical Forms and Pattern Restrictions"
description: Learn how to prevent issues that occur when the canonical representation of primitive value types doesn't comply with pattern restrictions from an XSD pattern facet.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "pattern restrictions"
  - "canonical forms"
author: MikeRayMSFT
ms.author: mikeray
---
# Canonical forms and pattern restrictions

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The XSD pattern facet allows for the restriction of the lexical space of simple types. When a pattern restriction is put on a type for which there's more than one possible lexical representation, some values could cause unexpected behavior upon validation.

This behavior occurs because lexical representations of these values aren't stored in the database. Therefore, the values are converted to their canonical representations when serialized as output. If a document contains a value whose canonical form doesn't comply with the pattern restriction for its type, the document is rejected if a user tries to reinsert it.

To prevent this, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects any XML document that contains values that can't be reinserted, because of the violation of pattern restrictions by their canonical forms. For example, the value "33.000" doesn't validate against a type derived from **xs:decimal** with a pattern restriction of "33\\.0+". Although "33.000" complies with this pattern, the canonical form, "33", doesn't.

Therefore, you should be careful when you apply pattern facets to types derived from the following primitive types: **boolean**, **decimal**, **float**, **double**, **dateTime**, **time**, **date**, **hexBinary**, and **base64Binary**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] issues a warning when you add any such components to a schema collection.

Imprecise serialization of floating-point values has a similar problem. Because of the floating-point serialization algorithm used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it's possible for similar values to share the same canonical form. When a floating-point value is serialized and then reinserted, its value may change slightly. In rare cases, this may result in a value that violates any of the following facets for its type on reinsertion: **enumeration**, **minInclusive**, **minExclusive**, **maxInclusive**, or **maxExclusive**. To prevent this, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects any values of types derived from `xs:float` or `xs:double` that can't be serialized and reinserted.

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
