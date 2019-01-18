---
title: "setAuthenticationScheme (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b942f78e-7ce1-44ef-923d-a7c3d7c76b83
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
