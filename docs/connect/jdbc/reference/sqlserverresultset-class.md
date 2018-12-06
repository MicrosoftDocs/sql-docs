---
title: "SQLServerResultSet Class | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: eaffcff1-286c-459f-83da-3150778480c9
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLServerResultSet Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents a JDBC result set.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Implements:** [ISQLServerResultSet](../../../connect/jdbc/reference/isqlserverresultset-interface.md)  
  
## Syntax  
  
```  
  
public final class SQLServerResultSet  
```  
  
## Remarks  
 There are two types of result sets: client-side and server-side.  
  
 Client-side result sets are used when the results can fit in the client process memory. These results provide the fastest performance and are read by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] in their entirety from the database. These result sets do not impose additional load on the database by incurring the overhead of creating server-side cursors. However, these types of result sets are not updatable.  
  
 Server-side result sets can be used when the results do not fit in the client process memory or when the result set is to be updatable. With this type of result set, the JDBC driver creates a server-side cursor and fetches rows of the result set transparently as the user scrolls through it.  
  
 The SQLServerResultSet class provides many methods to let you update the result set with any native Java data type and many Java object types.  
  
 This class supports unwrapping to SQLServerResultSet class, ISQLServerResultSet interface, and java.sql.ResultSet interface. For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
