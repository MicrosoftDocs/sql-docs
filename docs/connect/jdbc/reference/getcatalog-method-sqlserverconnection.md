---
title: "getCatalog Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.getCatalog"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e87ef65f-4b5a-4e1c-8db5-7f0932390bb0
author: MightyPen
ms.author: genemi
manager: craigg
---
# getCatalog Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the current catalog name of this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public java.lang.String getCatalog()  
```  
  
## Return Value  
 A **String** that contains the catalog name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCatalog method is specified by the getCatalog method in the java.sql.Connection interface.  
  
 Returns the current catalog property of the SQLServerConnection object, or null if it is not set. The catalog property is set explicitly with the [setCatalog](../../../connect/jdbc/reference/setcatalog-method-sqlserverconnection.md) method, or is implicitly updated by reading the environment change on TDS for the current catalog.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
