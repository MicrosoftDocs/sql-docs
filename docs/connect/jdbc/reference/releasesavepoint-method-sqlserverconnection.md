---
title: "releaseSavepoint Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.releaseSavepoint"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b6b625ea-c7ce-4a32-a9e0-6d2b4321bfd8
author: MightyPen
ms.author: genemi
manager: craigg
---
# releaseSavepoint Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Removes the given [SQLServerSavepoint](../../../connect/jdbc/reference/sqlserversavepoint-class.md) object from the current transaction.  
  
> [!NOTE]  
>  This method is not currently supported by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)].  
  
## Syntax  
  
```  
  
public void releaseSavepoint(java.sql.Savepoint savepoint)  
```  
  
#### Parameters  
 *savepoint*  
  
 The SavePoint object to remove.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This releaseSavepoint method is specified by the releaseSavepoint method in the java.sql.Connection interface.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
