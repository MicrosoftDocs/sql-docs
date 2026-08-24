---
title: "The xs:QName Type"
description: "Learn how to use the xs:QName type as an XML Schema restriction element or as the member type of a union."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/05/2022
ms.service: sql
ms.subservice: xml
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "xs:QName type"
---
# The xs:QName type

[!INCLUDE [SQL Server Azure SQL Database FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

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

## Related content

- [Requirements and limitations for XML schema collections on the server](requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
