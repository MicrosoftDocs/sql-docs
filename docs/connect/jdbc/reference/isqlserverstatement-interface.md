---
title: "ISQLServerStatement Interface"
description: "ISQLServerStatement Interface"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# ISQLServerStatement Interface
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents the basic implementation of JDBC statement functionality. This interface was added in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** java.sql.Statement  
  
## Syntax  
  
```  
  
public interface ISQLServerStatement  
```  
  
## Remarks  
 This interface is implemented by [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md).  
  
 This interface exposes the following [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific methods:  
  
|Method|For more information, see|  
|------------|-------------------------------|  
|public String getResponseBuffering|[getResponseBuffering](../../../connect/jdbc/reference/getresponsebuffering-method-sqlserverstatement.md)|  
|public void setResponseBuffering|[setResponseBuffering](../../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md)|  
  
## See Also  
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
