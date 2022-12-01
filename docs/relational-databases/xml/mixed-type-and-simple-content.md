---
title: "Mixed Type and Simple Content"
description: View an example showing that SQL Server doesn't support creating an XML schema that restricts a mixed type to a simple content.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "mixed types [SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Mixed type and simple content

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

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

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
