---
title: "prepareStatement Method (java.lang.String)"
description: "prepareStatement Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "02/07/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.prepareStatement (java.lang.String)"
apitype: "Assembly"
---
# prepareStatement Method (java.lang.String)

Creates a [SQLServerPreparedStatement](./sqlserverpreparedstatement-class.md) object for sending parameterized SQL statements to the database.

## Syntax

```
public java.sql.PreparedStatement prepareStatement(java.lang.String sql)
```

#### Parameters
*sql*

A **String** containing an SQL statement.

## Return Value
A PreparedStatement object.

## Exceptions  
[SQLServerException](./sqlserverexception-class.md)

## Remarks
This prepareStatement method is specified by the prepareStatement method in the java.sql.Connection interface.

## See Also

[prepareStatement Method &#40;SQLServerConnection&#41;](./preparestatement-method-sqlserverconnection.md)

[SQLServerConnection Members](./sqlserverconnection-members.md)

[SQLServerConnection Class](./sqlserverconnection-class.md)
