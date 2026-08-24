---
title: "getRowIdLifetime Method (SQLServerDatabaseMetaData)"
description: "getRowIdLifetime Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getRowIdLifetime Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a status indicating whether or not SQL RowId data type is supported. If supported, it returns the lifetime for which a RowId object remains valid.  
  
## Syntax  
  
```  
  
public java.sql.RowIdLifetime getRowIdLifetime()  
```  
  
## Return Value  
 A RowIdLifetime object.  
  
> [!NOTE]  
>  In the JDBC Driver version 2.0 release, this method returns java.sql.RowIdLifetime.ROWID_UNSUPPORTED value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getRowIdLifetime method is specified by the getRowIdLifetime method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
