---
title: "Example: Retrieving Binary Data"
description: View an example of an SQL query that retrieves binary data using the RAW and BINARY BASE64 options with the FOR XML clause.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "RAW mode, retrieving binary data example"
author: MikeRayMSFT
ms.author: mikeray
# monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# Example: Retrieve binary data

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following query returns the product photo stored in a **varbinary(max)** type column. The `BINARY BASE64` option is specified in the query to return the binary data in base64-encoded format.

## Example

```sql
USE AdventureWorks2012;
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

## See also

[Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
