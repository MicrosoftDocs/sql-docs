---
title: "doesMaxRowSizeIncludeBlobs Method (SQLServerDatabaseMetaData)"
description: "doesMaxRowSizeIncludeBlobs Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.doesMaxRowSizeIncludeBlobs"
apitype: "Assembly"
---
# doesMaxRowSizeIncludeBlobs Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the return value for the [getMaxRowSize](../../../connect/jdbc/reference/getmaxrowsize-method-sqlserverdatabasemetadata.md) method includes the SQL data types LONGVARCHAR and LONGVARBINARY.  
  
## Syntax  
  
```  
  
public boolean doesMaxRowSizeIncludeBlobs()  
```  
  
## Return Value  
 **true** if the return value includes the data types. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This doesMoxRowSizeIncludeBlobs method is specified by the doesMoxRowSizeIncludeBlobs method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
