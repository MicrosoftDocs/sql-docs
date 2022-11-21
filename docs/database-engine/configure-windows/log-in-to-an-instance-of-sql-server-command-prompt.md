---
title: "Log In to an Instance of SQL Server (Command Prompt)"
description: "Learn about the sqlcmd utility. See how to use it in a command prompt to test connectivity to an instance of SQL Server."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "logins [SQL Server], named instance of SQL Server"
  - "log ins [SQL Server]"
  - "logins [SQL Server], default instance of SQL Server"
  - "command prompt [SQL Server], logins"
  - "logging in [SQL Server]"
---
# Log In to an Instance of SQL Server (Command Prompt)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to test connectivity to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the **sqlcmd** utility.  
  
##  <a name="SSMSProcedure"></a>  
  
#### To log in to the default instance of SQL Server  
  
1.  From a command prompt, enter the following command to connect by using Windows Authentication:  
  
    ```  
    sqlcmd [ /E ] [ /S servername ]  
  
    ```  
  
#### To log in to a named instance of SQL Server  
  
1.  From a command prompt, enter the following command to connect by using Windows Authentication:  
  
    ```  
    sqlcmd [ /E ] /S servername\instancename  
  
    ```  
  
## See Also  
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [osql Utility](../../tools/osql-utility.md)  
  
  
