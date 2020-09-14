---
title: "Example: Retrieving Binary Data | Microsoft Docs"
description: View an example of an SQL query that retrieves binary data using the RAW and BINARY BASE64 options with the FOR XML clause.
ms.custom: ""
ms.date: 04/03/2020
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "RAW mode, retrieving binary data example"
ms.assetid: 5cea5d49-58ac-403a-a933-c4fd91de400b
author: RothJa
ms.author: jroth
# monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"
---
# Example: Retrieving Binary Data

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

The following query returns the product photo stored in a **varbinary(max)** type column. The `BINARY BASE64` option is specified in the query to return the binary data in base64-encoded format.

## Example

```sql
USE AdventureWorks2012;
GO
SELECT ProductPhotoID, ThumbNailPhoto
FROM Production.ProductPhoto
WHERE ProductPhotoID=1
FOR XML RAW, BINARY BASE64 ;
GO
```

Expect the following result:

```console
<row ProductModelID="1" ThumbNailPhoto="base64 encoded binary data"/>
```

## See Also

[Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
