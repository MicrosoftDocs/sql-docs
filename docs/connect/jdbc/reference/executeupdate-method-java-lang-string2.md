---
title: "executeUpdate Method (java.lang.String)"
description: "executeUpdate Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "02/07/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.executeUpdate (java.lang.String)"
apitype: "Assembly"
---
# executeUpdate Method (java.lang.String)

Runs the given SQL statement, which can be an INSERT, UPDATE, MERGE, or DELETE statement; or an SQL statement that returns nothing, such as an SQL DDL statement.

## Syntax

```
public final int executeUpdate(java.lang.String sql)
```

#### Parameters
*sql*

A **String** that contains the SQL statement.

## Return Value
An **int** that indicates the number of rows affected, or 0 if using a DDL statement.

## Exceptions
[SQLServerException](./sqlserverexception-class.md)

## Remarks
This executeUpdate method is specified by the executeUpdate method in the java.sql.PreparedStatement interface.

Calling this method will result in an exception since the SQL statement for the SQLServerPreparedStatement object is specified when the object is created.

## See Also

[executeUpdate Method &#40;SQLServerPreparedStatement&#41;](./executeupdate-method-sqlserverpreparedstatement.md)

[SQLServerPreparedStatement Members](./sqlserverpreparedstatement-members.md)

[SQLServerPreparedStatement Class](./sqlserverpreparedstatement-class.md)
