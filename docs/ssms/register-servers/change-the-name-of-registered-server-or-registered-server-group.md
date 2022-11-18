---
description: "Change the Name of Registered Server or Registered Server Group"
title: Change the Name of Registered Server or Server Group
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: 10e1546b-9edb-400c-8676-2ea1192d6134
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 08/02/2016
---

# Change the Name of Registered Server or Registered Server Group

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This topic describes how to change the name of a registered server or server group in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio. The name can be changed at any time. Changing the name of a server in Registered Servers only changes how the name is displayed. To connect to a different server, you must edit the connection properties of the registered server.  
  
## <a name="SSMSProcedure"></a> Using SQL Server Management Studio

From the menu navigate to **View\\Registered Servers** to open the **Registered Servers** pane.

### To change the name of a server

1. In **Registered Servers**, expand **Database Engine** and then **Local Server Groups**.  

2. Right-click a server and select **Properties** to open the **Edit Server Registration Properties** dialog window.

3. In the **Registered server name** text box, type the new name for the server registration, and then click **Save**.  

### To change the name of a server group  

1. In **Registered Servers**, expand **Database Engine** and then **Local Server Groups**.  

2. Right-click a server group and select **Properties** to open the **Edit Server Group Properties** dialog window. 

3. In the **Group name** text box, type the new name for the server group, and then click **Save**.  

## See Also

[Change a Server's Registration &#40;SQL Server Management Studio&#41;](./change-a-server-s-registration-sql-server-management-studio.md)