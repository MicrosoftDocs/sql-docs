---
title: "The xs:QName Type"
description: Learn how to use the xs:QName type as an XML Schema restriction element or as the member type of a union.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "xs:QName type"
author: MikeRayMSFT
ms.author: mikeray
---
# The xs:QName type

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support types derived from **xs:QName** by the use of an XML schema restriction element. Also, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] currently doesn't support union types with **QName** as a member type.

## Example

The following `CREATE XML SCHEMA COLLECTION` statements can't load the XML schema, because they specify the `xs:QName` type as a member type of the union:

```sql
CREATE XML SCHEMA COLLECTION QNameLimitation1 AS N'
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
    <xs:simpleType name="myUnion">
        <xs:union memberTypes="xs:int xs:QName"/>
    </xs:simpleType>
</xs:schema>';
GO

CREATE XML SCHEMA COLLECTION QNameLimitation2 AS N'
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
    <xs:simpleType name="myUnion">
        <xs:union memberTypes="xs:integer">
   <xs:simpleType>
    <xs:list itemType="xs:QName"/>
   </xs:simpleType>
  </xs:union>
    </xs:simpleType>
</xs:schema>';
GO
```

Both statements fail with an error.

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
