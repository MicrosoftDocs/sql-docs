---
title: "FOR XML Support for the timestamp Data Type"
description: Learn about support for the timestamp data type when using the FOR XML clause in a SQL query.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "timestamp data type"
author: MikeRayMSFT
ms.author: mikeray
---
# FOR XML support for the timestamp data type

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

In the FOR XML transformation, **timestamp** type values are treated as **varbinary(8)** data and will always be base 64 encoded. The XSD or XDR schema, if requested, reflects this type.

```sql
DROP TABLE t;
GO
CREATE TABLE t
(c1 int,
c2 timestamp);
GO

INSERT t values(1, null);
GO
SELECT * FROM t
for XML AUTO, XMLDATA;
GO
```

This is the result:

```xml
<Schema name="Schema1"
        xmlns="urn:schemas-microsoft-com:xml-data"
        xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <ElementType name="t" content="empty" model="closed">
    <AttributeType name="c1" dt:type="i4" />
    <AttributeType name="c2" dt:type="bin.base64" />
    <attribute type="c1" />
    <attribute type="c2" />
  </ElementType>
</Schema>
<t xmlns="x-schema:#Schema1" c1="1" c2="AAAAAAAAH04=" />
```

## See also

- [FOR XML Support for Various SQL Server Data Types](../../relational-databases/xml/for-xml-support-for-various-sql-server-data-types.md)
