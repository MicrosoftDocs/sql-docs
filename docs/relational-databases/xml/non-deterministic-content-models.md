---
title: "Non-Deterministic Content Models"
description: View an example of using an XML schema with a non-deterministic content model.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "non-deterministic content models"
  - "content models [XML in SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Non-Deterministic content models

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Non-deterministic content models are accepted in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if the occurrence constraints are 0, 1, or unbounded.

Before [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 1 (SP1), [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejected XML schemas that had non-deterministic content models.

## Example: Non-deterministic content model rejected

The following example attempts to create an XML schema with a non-deterministic content model. The code fails because it isn't clear whether the `<root>` element should have a sequence of two `<a>` elements or if the `<root>` element should have two sequences, each with an `<a>` element.

```sql
CREATE XML SCHEMA COLLECTION MyCollection AS '
<schema xmlns="http://www.w3.org/2001/XMLSchema">
    <element name="root">
        <complexType>
            <sequence minOccurs="1" maxOccurs="2">
                <element name="a" type="string" minOccurs="1" maxOccurs="2"/>
            </sequence>
        </complexType>
    </element>
</schema>
';
GO
```

The schema can be fixed by moving the occurrence constraint to a unique location. For example, the constraint can be moved to the containing sequence particle:

```xml
<sequence minOccurs="1" maxOccurs="4">
    <element name="a" type="string" minOccurs="1" maxOccurs="1"/>
</sequence>
```

Or the constraint can be moved to the contained element:

```xml
<sequence minOccurs="1" maxOccurs="1">
     <element name="a" type="string" minOccurs="1" maxOccurs="4"/>
</sequence>
```

## Example: Non-deterministic content model accepted

The following schema would be rejected in versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP1.

```sql
CREATE XML SCHEMA COLLECTION MyCollection AS '
<schema xmlns="http://www.w3.org/2001/XMLSchema">
    <element name="root">
        <complexType>
            <sequence minOccurs="0" maxOccurs="unbounded">
                <element name="a" type="string" minOccurs="0" maxOccurs="1"/>
                <element name="b" type="string" minOccurs="1" maxOccurs="unbounded"/>
            </sequence>
        </complexType>
    </element>
</schema>
';
GO
```

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
