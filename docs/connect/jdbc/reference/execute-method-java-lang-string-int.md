---
title: "execute Method (java.lang.String, int[])"
description: "execute Method (java.lang.String, int[])"
author: David-Engel
ms.author: v-davidengel
ms.date: "02/07/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.execute (javal.lang.String.int[])"
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

A **String** that contains an SQL statement.

*columnIndexes*

An array of **int**s that indicates the column indexes of the auto-generated keys that should be made available.

## Return Value
**true** if the first result is a result set. Otherwise, **false**.
  
## Exceptions
[SQLServerException](./sqlserverexception-class.md)

## Remarks
This execute method is specified by the execute method in the java.sql.Statement interface.

## See Also

[execute Method &#40;SQLServerStatement&#41;](./execute-method-sqlserverstatement.md)

[SQLServerStatement members](./sqlserverstatement-members.md)

[SQLServerStatement class](./sqlserverstatement-class.md)
