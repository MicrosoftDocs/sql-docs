---
title: "ownUpdatesAreVisible Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.ownUpdatesAreVisible"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: eacbb1a8-ac9a-4f44-832e-ae0af476522e
author: MightyPen
ms.author: genemi
manager: craigg
---
# ownUpdatesAreVisible Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the result set's own updates are visible.  
  
## Syntax  
  
```  
  
public boolean othersUpdatesAreVisible(int type)  
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
 **true** if the updates are visible. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This ownUpdatesAreVisible method is specified by the ownUpdatesAreVisible method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
