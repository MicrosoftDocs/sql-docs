---
title: "Mixed Type and Simple Content"
description: View an example showing that SQL Server doesn't support creating an XML schema that restricts a mixed type to a simple content.
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
  - "mixed types [SQL Server]"
---
# Mixed type and simple content

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support restricting a mixed type to a simple content.

## Example

In the following XML schema collection, `myComplexTypeA` is a complex type that can be emptied. That is, both its elements have `minOccurs` set to 0. The attempt to restrict this to a simple content, as in the `myComplexTypeB` declaration, isn't supported. Therefore, the following XML schema collection creation fails:

```sql
CREATE XML SCHEMA COLLECTION SC AS '
<schema xmlns="http://www.w3.org/2001/XMLSchema" targetNamespace="http://ns" xmlns:ns="http://ns"
xmlns:ns1="http://ns1">
    <complexType name="myComplexTypeA" mixed="true">
        <sequence>
            <element name="a" type="string" minOccurs="0"/>
            <element name="b" type="string" minOccurs="0" maxOccurs="23"/>
        </sequence>
    </complexType>
    <complexType name="myComplexTypeB">
        <simpleContent>
            <restriction base="ns:myComplexTypeA">
                <simpleType>
                    <restriction base="int">
                        <minExclusive value="25"/>
                    </restriction>
                </simpleType>
            </restriction>
        </simpleContent>
    </complexType>
</schema>
';
GO
```

## Related content

- [Requirements and limitations for XML schema collections on the server](requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
