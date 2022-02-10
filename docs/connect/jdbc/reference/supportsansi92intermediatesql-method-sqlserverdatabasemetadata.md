---
title: supportsANSI92IntermediateSQL Method (SQLServerDatabaseMetaData)
description: "API reference for the mssql-jdbc library. Method: supportsANSI92IntermediateSQL (SQLServerDatabaseMetaData)"
ms.custom: ""
ms.date: 01/11/2022
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: reference
apiname:
  - "SQLServerDatabaseMetaData.supportsANSI92IntermediateSQL"
apilocation:
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4d6e8301-0633-4565-91c6-a80910954461
author: David-Engel
ms.author: v-davidengel
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
