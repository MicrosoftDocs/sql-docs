---
title: "deletesAreDetected Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.deletesAreDetected"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 73f3d994-bbd7-43d2-8b64-50057e278983
author: MightyPen
ms.author: genemi
manager: craigg
---
# deletesAreDetected Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether or not a visible row delete can be detected by calling the [rowDeleted](../../../connect/jdbc/reference/rowdeleted-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) class.  
  
## Syntax  
  
```  
  
public boolean deletesAreDetected(int type)  
```  
  
#### Parameters  
 *type*  
  
 An **int** that indicates the result set type, which can be one of the following values as defined in java.sql.ResultSet or SQLServerResultSet:  
  
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
 **true** if a hole replaces the deleted row. **false** if the deleted row is removed.  
  
 When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method returns **true** for TYPE_SS_SCROLL_KEYSET cursors and **false** for all other result set types.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This deletesAreDetected method is specified by the deletesAreDetected method in the java.sql.DatabaseMetaData interface.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] detects deleted rows for all updatable cursor types, although the detection is transient for forward and dynamic cursors.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
