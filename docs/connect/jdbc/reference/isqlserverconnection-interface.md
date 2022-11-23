---
title: "ISQLServerConnection Interface"
description: "ISQLServerConnection Interface"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# ISQLServerConnection Interface
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents a JDBC connection to a [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. This interface was added in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** java.sql.Connection  
  
## Syntax  
  
```  
  
public interface ISQLServerConnection  
```  
  
## Remarks  
 This interface is implemented by [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md).  
  
 This interface exposes the following [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific field:  
  
|Field|For more information, see|  
|-----------|-------------------------------|  
|public final static int TRANSACTION_SNAPSHOT|[TRANSACTION_SNAPSHOT](../../../connect/jdbc/reference/transaction-snapshot-field-sqlserverconnection.md)|  
|public UUID getClientConnectionId()|[getClientConnectionID()](../../../connect/jdbc/reference/getclientconnectionid-method-sqlserverconnection.md)|  
  
## See Also  
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
