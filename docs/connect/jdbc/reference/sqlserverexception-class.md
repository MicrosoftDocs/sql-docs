---
title: "SQLServerException Class | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: af5ef257-7cf6-4db3-b1ee-07d22d82bef1
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLServerException Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents an unsuccessful or incomplete running of an SQL statement.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** java.sql.SQLException  
  
 **Implements:** java.io.Serializable  
  
## Syntax  
  
```  
  
public final class SQLServerException  
```  
  
## Remarks  
 The SQLServerException class handles both SQL 92 and XOPEN state codes. They are switchable by using a user-specified connection property. Exceptions are written to any open log files that have been specified.  
  
## See Also  
 [SQLServerException Members](../../../connect/jdbc/reference/sqlserverexception-members.md)   
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
