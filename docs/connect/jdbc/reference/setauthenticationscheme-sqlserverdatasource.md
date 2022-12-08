---
title: "setAuthenticationScheme (SQLServerDataSource)"
description: "setAuthenticationScheme (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setAuthenticationScheme (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates the kind of integrated security you want your application to use.  
  
## Syntax  
  
```vb  
public void setAuthenticationScheme(String authenticationScheme);  
```  
  
#### Parameters  
 *authenticationScheme*  
  
 Values are **"JavaKerberos"** and the default **"NativeAuthentication"**. For more information, see [Using Kerberos Integrated Authentication to Connect to SQL Server](../../../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md).  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
