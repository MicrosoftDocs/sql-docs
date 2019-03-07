---
title: "supportsTransactionIsolationLevel Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsTransactionIsolationLevel"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b716ed6c-6ec3-47a7-8e6d-16cbf2469d6d
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsTransactionIsolationLevel Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the given transaction isolation level.  
  
## Syntax  
  
```  
  
public boolean supportsTransactionIsolationLevel(int level)  
```  
  
#### Parameters  
 *level*  
  
 An **int** that indicates the transaction isolation level.  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsTransactionIsolationLevel method is specified by the supportsTransactionIsolationLevel method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
