---
title: "prepareStatement Method (java.lang.String) | Microsoft Docs"
ms.custom: ""
ms.date: "02/07/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.prepareStatement (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e825765c-eb55-4800-951b-f3495da36641
author: MightyPen
ms.author: genemi
manager: craigg
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
