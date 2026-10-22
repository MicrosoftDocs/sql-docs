---
title: "IsSparseColumnSet Method (SQLServerResultSetMetaData)"
description: Indicates if a column in a result set is a sparse column set.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 07/24/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# isSparseColumnSet Method (SQLServerResultSetMetaData)

[!INCLUDE [Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

Indicates if a column in a result set is a sparse column set.

## Syntax

```java
public boolean isSparseColumnSet(int column)
```

#### Parameters

*column*

The (one-based) index of the column.

## Return value

**true** if a column in a result set is a sparse column set, otherwise **false**.

## Remarks

This method doesn't retrieve information from the database.

## Related content

- [SQLServerResultSetMetaData Methods](sqlserverresultsetmetadata-methods.md)
- [SQLServerResultSetMetaData Members](sqlserverresultsetmetadata-members.md)
- [SQLServerResultSetMetaData Class](sqlserverresultsetmetadata-class.md)
