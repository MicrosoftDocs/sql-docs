---
title: "insertsAreDetected Method (SQLServerDatabaseMetaData)"
description: "insertsAreDetected Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.insertsAreDetected"
apitype: "Assembly"
---
# insertsAreDetected Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether or not a visible row insert can be detected by calling the method [rowInserted](../../../connect/jdbc/reference/rowinserted-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) class.  
  
## Syntax  
  
```  
  
public boolean insertsAreDetected(int type)  
```  
  
#### Parameters  
 *type*  
  
 An integer that indicates the result set type, which can be one of the following values as defined in java.sql.ResultSet or SQLServerResultSet:  
  
## java.sql.ResultSet Types  
 TYPE_FORWARD_ONLY  
  
 TYPE_SCROLL_SENSITIVE  
  
 TYPE_SCROLL_INSENSITIVE  
  
## SQLServerResultSet Types  
 TYPE_SS_SCROLL_STATIC  
  
 TYPE_SS_SCROLL_KEYSET  
  
 TYPE_SS_DIRECT_FORWARD_ONLY  
  
 TYPE_SS_SERVER_CURSOR_FORWARD_ONLY  
  
 TYPE_SS_SCROLL_DYNAMIC  
  
## Return Value  
 **true** if the row insert can be detected. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This insertsAreDetected method is specified by the insertsAreDetected method in the java.sql.DatabaseMetaData interface.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not detect inserted rows for any cursor type.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
