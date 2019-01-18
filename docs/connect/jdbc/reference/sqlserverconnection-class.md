---
title: "SQLServerConnection Class | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 937292a6-1525-423e-a2b2-a18fd34c2893
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLServerConnection Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents a JDBC connection to a [!INCLUDE[msCoName](../../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Implements:** [ISQLServerConnection](../../../connect/jdbc/reference/isqlserverconnection-interface.md), java.io.Serializable  
  
## Syntax  
  
```  
  
public class SQLServerConnection  
```  
  
## Remarks  
 SQLServerConnection supports JDBC connection pooling and can be either a physical JDBC connection or a logical JDBC connection. SQLServerConnection manages transaction control for all statements that were created from it, and it can participate in XA distributed transactions managed via a XAResource adapter.  
  
 SQLServerConnection manages a pool of prepared statement handles. Prepared statements are prepared once and are typically run many times with different data values for their parameters. Prepared statements are also maintained across logical (pooled) connection closes.  
  
> [!NOTE]  
>  SQLServerConnection is not thread safe. However, multiple statements that are created from a single connection can be processed simultaneously in concurrent threads.  
  
 This class supports unwrapping to SQLServerConnection class, java.sql.connection interface, and ISQLServerConnection interface. For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
