---
title: "commit Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.commit"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: c7346165-51bf-4844-b64c-29833c147236
author: MightyPen
ms.author: genemi
manager: craigg
---
# commit Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Makes all changes made since the previous commit or rollback permanent, and releases any database locks currently held by this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public void commit()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This commit method is specified by the commit method in the java.sql.Connection interface.  
  
 This method should be used only when auto-commit mode has been disabled.  
  
 Note that this method will fail and throw an exception if the client starts a manual transaction and then for some reason SQL Server rolls back the manual transaction. For example, an exception is thrown if the client calls a stored procedure that explicitly calls ROLLBACK TRANSACTION, and then the client calls the commit method. In addition, if SQL Server raises an error of sufficient severity (16 or higher) to roll back the client initiated manual transaction; a subsequent call to the commit method will throw an exception.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
