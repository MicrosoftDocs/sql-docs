---
title: "Grant Permissions to Use the Admin Console (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fbab2c2e-fa6d-4236-a69c-d75099d4cbfa
caps.latest.revision: 12
author: BarbKess
---
# Grant Permissions to Use the Admin Console (SQL Server PDW)
This topic describes how to grant permissions to logins to use the Admin Console.  
  
## <a name="PermsAdminConsole"></a>Grant Permissions to Monitor Appliance Health and Use the Admin Console  
**Use the Admin Console**  
  
To use the Admin Console a login requires the server level **VIEW SERVER STATE** permission. The following SQL statement grants the **VIEW SERVER STATE** permission to the login `KimAbercrombie` so that Kim can use the Admin Console to monitor the SQL Server PDW appliance.  
  
```  
USE master;  
GO  
GRANT VIEW SERVER STATE TO KimAbercrombie;  
GO  
```  
  
**Kill Sessions**  
  
To grant a login the permission to kill sessions, grant the **ALTER ANY CONNECTION** permission as follows:  
  
```  
GRANT ALTER ANY CONNECTION TO KimAbercrombie;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
