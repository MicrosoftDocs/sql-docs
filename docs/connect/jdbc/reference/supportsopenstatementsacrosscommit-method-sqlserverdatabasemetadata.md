---
title: "supportsOpenStatementsAcrossCommit Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsOpenStatementsAcrossCommit"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e733586c-9222-43cb-92ea-ba474f442a43
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsOpenStatementsAcrossCommit Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports keeping statements open across commits.  
  
## Syntax  
  
```  
  
public boolean supportsOpenStatementsAcrossCommit()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsOpenStatementsAcrossCommit method is specified by the supportsOpenStatementsAcrossCommit method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
