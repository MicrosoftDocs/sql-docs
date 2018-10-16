---
title: "Change the Name of Registered Server or Registered Server Group | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying registered server or server group names"
  - "server groups [SQL Server]"
  - "Registered Servers [SQL Server], names"
  - "renaming registered server or server group"
  - "names [SQL Server], registered server or server group"
ms.assetid: 10e1546b-9edb-400c-8676-2ea1192d6134
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Change the Name of Registered Server or Registered Server Group
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  This topic describes how to change the name of a registered server or server group in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. The name can be changed at any time. Changing the name of a server in Registered Servers only changes how the name is displayed. To connect to a different server, you must edit the connection properties of the registered server.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
From the menu navigate to **View\\Registered Servers** to open the **Registered Servers** pane.  
#### To change the name of a server  
  
1.  In **Registered Servers**, expand **Database Engine** and then **Local Server Groups**.  

2.  Right-click a server and select **Properties** to open the **Edit Server Registration Properties** dialog window.
  
3.  In the **Registered server name** text box, type the new name for the server registration, and then click **Save**.  
  
#### To change the name of a server group  
  
1.  In **Registered Servers**, expand **Database Engine** and then **Local Server Groups**.  

2.  Right-click a server group and select **Properties** to open the **Edit Server Group Properties** dialog window. 
  
3.  In the **Group name** text box, type the new name for the server group, and then click **Save**.  
  
## See Also  
 [Change a Server's Registration &#40;SQL Server Management Studio&#41;](../../tools/sql-server-management-studio/change-a-server-s-registration-sql-server-management-studio.md)  
  
  
