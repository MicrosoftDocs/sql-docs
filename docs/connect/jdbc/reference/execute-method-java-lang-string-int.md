---
title: "execute Method (java.lang.String, int[])"
description: "execute Method (java.lang.String, int[])"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "02/07/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.execute (java.lang.String.int[])"
apitype: "Assembly"
---
# execute Method (java.lang.String, int[])

  Runs the given SQL statement, which can return multiple results, and signals [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] that the auto-generated keys that are indicated in the given array should be made available for retrieval.

## Syntax

```Java
public final boolean execute(
    java.lang.String sql,
    int[] columnIndexes)
```

#### Parameters
*sql*

A **String** that contains a SQL statement.

*columnIndexes*

An array of **int** that indicates the column indexes of the auto-generated keys that should be made available.

## Return Value
**true** if the first result is a result set. Otherwise, **false**.
  
## Exceptions
[SQLServerException](./sqlserverexception-class.md)

## Remarks
This execute method is specified by the execute method in the java.sql.Statement interface.

## Related content

- [execute Method (SQLServerStatement)](execute-method-sqlserverstatement.md)
- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
