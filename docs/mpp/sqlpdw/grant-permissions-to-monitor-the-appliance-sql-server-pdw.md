---
title: "Grant Permissions to Monitor the Appliance (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d4333189-f093-42fa-9003-e25ee3388cb8
caps.latest.revision: 11
author: BarbKess
---
# Grant Permissions to Monitor the Appliance (SQL Server PDW)
The SQL Server PDW appliance can be monitored by using either the Admin Console or SQL Server PDW system views. Logins require the server level **VIEW SERVER STATE** permission to monitor the appliance. Logins require the **ALTER ANY CONNECTION** permission to terminate connections by using the Admin Console or the **KILL** command. For information on permissions required to use the Admin Console, see [Grant Permissions to Use the Admin Console &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/grant-permissions-to-use-the-admin-console-sql-server-pdw.md).  
  
## <a name="PermsAdminConsole"></a>Grant Permission to Monitor the Appliance by Using System Views  
The following SQL statements create a login named `monitor_login` and grants the **VIEW SERVER STATE** permission to the `monitor_login` login.  
  
```  
USE master;  
GO  
CREATE LOGIN monitor_login WITH PASSWORD='Password4321';  
GRANT VIEW SERVER STATE TO monitor_login;  
GO  
```  
  
## Grant Permission to Monitor the Appliance by Using System Views and to Terminate Connections  
The following SQL statements create a login named `monitor_and_terminate_login` and grants the **VIEW SERVER STATE** and **ALTER ANY CONNECTION** permissions to the `monitor_and_terminate_login` login.  
  
```  
USE master;  
GO  
CREATE LOGIN monitor_and_terminate_login WITH PASSWORD='Password1234';   
GRANT VIEW SERVER STATE TO monitor_and_terminate_login;   
GRANT ALTER ANY CONNECTION TO monitor_and_terminate_login;  
GO  
```  
  
To create admin logins, see [Fixed Server Roles &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/fixed-server-roles-sql-server-pdw.md).  
  
## See Also  
[Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
