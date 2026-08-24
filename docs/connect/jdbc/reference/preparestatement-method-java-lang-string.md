---
title: "prepareStatement Method (java.lang.String)"
description: "prepareStatement Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/22/2026"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.prepareStatement (java.lang.String)"
apitype: "Assembly"
ai-usage: ai-assisted
---
# prepareStatement Method (java.lang.String)

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

Creates a [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object for sending parameterized SQL statements to the database.

## Syntax

```java
public java.sql.PreparedStatement prepareStatement(java.lang.String sql)
```

#### Parameters

`sql`

A **String** that contains a SQL statement.

## Return Value

A PreparedStatement object.

## Exceptions

[SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)

## Remarks

This prepareStatement method is specified by the prepareStatement method in the java.sql.Connection interface.

## Related content

- [prepareStatement Method (SQLServerConnection)](preparestatement-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
