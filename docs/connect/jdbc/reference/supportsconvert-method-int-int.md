---
title: "supportsConvert Method (int, int)"
description: "supportsConvert Method (int, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsConvert (int, int)"
apitype: "Assembly"
---
# supportsConvert Method (int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the CONVERT for two given SQL types.  
  
## Syntax  
  
```  
  
public boolean supportsConvert(int fromType,  
                               int toType)  
```  
  
#### Parameters  
 *fromType*  
  
 The JDBC type to convert from.  
  
 *toType*  
  
 The JDBC type to convert to.  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsConvert method is specified by the supportsConvert method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [supportsConvert Method &#40;SQLServerDatabaseMetaData&#41;](../../../connect/jdbc/reference/supportsconvert-method-sqlserverdatabasemetadata.md)   
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
