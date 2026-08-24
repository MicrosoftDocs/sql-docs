---
title: "Example: Retrieving Binary Data"
description: View an example of a SQL query that retrieves binary data using the RAW and BINARY BASE64 options with the FOR XML clause.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/05/2022
ms.service: sql
ms.subservice: xml
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "RAW mode, retrieving binary data example"
---
# Example: Retrieve binary data

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The following query returns the product photo stored in a **varbinary(max)** type column. The `BINARY BASE64` option is specified in the query to return the binary data in base64-encoded format.

## Example

```sql
USE AdventureWorks2022;
GO
SELECT ProductPhotoID, ThumbNailPhoto
FROM Production.ProductPhoto
WHERE ProductPhotoID = 1
FOR XML RAW, BINARY BASE64;
GO
```

Expect the following result:

```xml
<row ProductModelID="1" ThumbNailPhoto="base64 encoded binary data"/>
```

## Related content

- [Use RAW mode with FOR XML](use-raw-mode-with-for-xml.md)
