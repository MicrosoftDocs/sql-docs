---
title: supportsANSI92IntermediateSQL Method (SQLServerDatabaseMetaData)
description: "API reference for the mssql-jdbc library. Method: supportsANSI92IntermediateSQL (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: 01/11/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsANSI92IntermediateSQL"
apitype: "Assembly"
---
# supportsANSI92IntermediateSQL Method (SQLServerDatabaseMetaData)

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

Retrieves whether this database supports the ANSI92 intermediate SQL grammar.

## Syntax

```java
public boolean supportsANSI92IntermediateSQL()
```

## Return value

 **true** if supported. Otherwise, **false**.

## Exceptions

[SQLServerException](sqlserverexception-class.md)

## Remarks

This supportsANSI92IntermediateSQL method is specified by the supportsANSI92IntermediateSQL method in the java.sql.DatabaseMetaData interface.

## See also

[SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)  
[SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)  
[SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
