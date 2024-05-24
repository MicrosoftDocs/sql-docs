---
title: "FOR XML support for the timestamp data type"
description: Learn about support for the timestamp data type when using the FOR XML clause in a SQL query.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/24/2024
ms.service: sql
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "timestamp data type"
---
# FOR XML support for the timestamp data type

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

In the `FOR XML` transformation, **timestamp** type values are treated as **varbinary(8)** data, and is always Base64 encoded. The XML Schema Definition (XSD) or XML-Data Reduced (XDR) schema, if requested, reflects this type.

```sql
DROP TABLE t;
GO

CREATE TABLE t (
    c1 INT,
    c2 TIMESTAMP
);
GO

INSERT t VALUES (1, NULL);
GO

SELECT * FROM t
FOR XML AUTO, XMLDATA;
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```xml
<Schema name="Schema1" xmlns="urn:schemas-microsoft-com:xml-data" xmlns:dt="urn:schemas-microsoft-com:datatypes">
    <ElementType name="t" content="empty" model="closed">
        <AttributeType name="c1" dt:type="i4" />
        <AttributeType name="c2" dt:type="bin.base64" />
        <attribute type="c1" />
        <attribute type="c2" />
    </ElementType>
</Schema>
<t xmlns="x-schema:#Schema1" c1="1" c2="AAAAAAAACGE=" />
```

## Related content

- [FOR XML support for various SQL Server data types](for-xml-support-for-various-sql-server-data-types.md)
