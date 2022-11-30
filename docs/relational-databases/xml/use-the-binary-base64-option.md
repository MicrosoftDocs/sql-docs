---
title: "Use the BINARY BASE64 Option"
description: Learn how to use the BINARY BASE64 option in an SQL query to return binary data in the base64 encoding format.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "AUTO FOR XML mode, BINARY BASE64 option"
author: MikeRayMSFT
ms.author: mikeray
# monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# Use the BINARY BASE64 option

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

If the BINARY BASE64 option is specified in the query, the binary data is returned in base64 encoding format.

If the BINARY BASE64 option isn't specified in the query, then by default, AUTO mode supports URL encoding of binary data. A reference to a relative URL to the virtual root of the database is returned. This reference is to the database where the query was executed. The returned reference can be used to access the actual binary data in subsequent operations. This access is achieved by using the SQLXML ISAPI `dbobject` query. The query must provide enough information to identify the image. Such information might include the columns of the primary key.

## Column alias

Don't use an alias for a binary column when you query a view and using the FOR XML AUTO mode. If you use an alias, the alias is returned in the URL encoding of the binary data. In subsequent operations, the alias is meaningless. The meaningless alias and the URL encoding can't be used to retrieve the image.

### Cast to a BLOB

In a SELECT query, casting any column to a binary large object (BLOB) makes the column a temporary entity. Being temporary, the BLOB loses its associated table name and column name. This cast causes AUTO mode queries to generate an error, because the system doesn't know where to put this value in the XML hierarchy.

For example, consider the following table with its one row.

```sql
CREATE TABLE MyTable (Col1 int PRIMARY KEY, Col2 binary)
INSERT INTO MyTable VALUES (1, 0x7);
```

The following query produces an error, which is caused by the casting to a binary large object (BLOB):

```sql
SELECT Col1,
CAST(Col2 as image) as Col2
FROM MyTable
FOR XML AUTO;
```

The solution is to add the BINARY BASE64 option to the FOR XML clause. If you remove the casting, the query produces good results.

```sql
SELECT Col1,
CAST(Col2 as image) as Col2
FROM MyTable
FOR XML AUTO, BINARY BASE64;
```

Expect the following good result:

```xml
<MyTable Col1="1" Col2="Bw==" />
```

## See also

[Use AUTO Mode with FOR XML](../../relational-databases/xml/use-auto-mode-with-for-xml.md)
