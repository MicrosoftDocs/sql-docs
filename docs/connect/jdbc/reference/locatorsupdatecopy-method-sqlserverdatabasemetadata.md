---
title: "locatorsUpdateCopy Method (SQLServerDatabaseMetaData)"
description: "locatorsUpdateCopy Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.locatorsUpdateCopy"
apitype: "Assembly"
---
# locatorsUpdateCopy Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether updates made to a LOB are made on a copy or directly to the LOB.  
  
## Syntax  
  
```  
  
public boolean locatorsUpdateCopy()  
```  
  
## Return Value  
 **true** if updates are made to a copy. **false** if updates are made directly.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This locatorsUpdateCopy method is specified by the locatorsUpdateCopy method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
