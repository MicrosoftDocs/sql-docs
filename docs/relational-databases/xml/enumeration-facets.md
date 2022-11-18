---
title: "Enumeration Facets"
description: Learn how SQL Server uses enumeration facets to validate XML schemas.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "enumeration facets"
author: MikeRayMSFT
ms.author: mikeray
---
# Enumeration facets

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects XML schemas with types that have pattern facets or enumerations that violate those facets.

## Example

The following schema would be rejected, because the featured enumeration value includes a mixed-case value. It would also be rejected because this value violates the pattern value that limits values to only lowercase letters.

```sql
CREATE XML SCHEMA COLLECTION MySampleCollection AS '
<schema xmlns="http://www.w3.org/2001/XMLSchema" targetNamespace="http://ns" xmlns:ns="http://ns">
    <simpleType name="MyST">
       <restriction base="string">
          <pattern value="[a-z]*"/>
       </restriction>
    </simpleType>

    <simpleType name="MyST2">
       <restriction base="ns:MyST">
           <enumeration value="mYstring"/>
       </restriction>
    </simpleType>
</schema>';
GO
```

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
