---
title: "supportsOpenStatementsAcrossRollback Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsOpenStatementsAcrossRollback"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4e38b938-f39f-4c5d-9b32-4ba489535c45
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsOpenStatementsAcrossRollback Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports keeping statements open across rollbacks.  
  
## Syntax  
  
```  
  
public boolean supportsOpenStatementsAcrossRollback()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsOpenStatementsAcrossRollback method is specified by the supportsOpenStatementsAcrossRollback method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
