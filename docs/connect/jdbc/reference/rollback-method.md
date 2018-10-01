---
title: "rollback Method () | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.rollback ()"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7adb6772-4047-4d8e-931d-b3d20eec44b5
author: MightyPen
ms.author: genemi
manager: craigg
---
# rollback Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Undoes all changes made in the current transaction and releases any database locks currently held by this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public void rollback()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rollBack method is specified by the rollBack method in the java.sql.Connection interface.  
  
 This method should be used only when auto-commit mode has been disabled.  
  
## See Also  
 [rollback Method &#40;SQLServerConnection&#41;](../../../connect/jdbc/reference/rollback-method-sqlserverconnection.md)   
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
