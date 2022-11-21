---
description: "Remove a Server Group (SQL Server Management Studio)"
title: Remove a Server Group
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "removing server groups"
  - "Registered Servers [SQL Server], server groups"
  - "server groups [SQL Server]"
  - "deleting server groups"
  - "groups [SQL Server], server"
ms.assetid: 1f3ea9ee-67c0-46ed-bf02-ceca92d3b8fe
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/07/2017
---

# Remove a Server Group (SQL Server Management Studio)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This topic describes how to remove a server group in Registered Servers in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio. You can delete a server group at any time. If the server group is not empty, any servers or server groups contained in the deleted server group will also be deleted. Before deleting a server group, move any servers or server groups that you want to retain to a new server group.  
  
##  <a name="SSMSProcedure"></a>  
  
#### To remove a server group  
  
1.  In Registered Servers, right-click a server group, and then click **Delete**.  
  
2.  In the **Delete Confirmation** dialog box, click **Yes**.  
  
## See Also  
 [Move a Registered Server or Registered Server Group &#40;SQL Server Management Studio&#41;](./move-a-registered-server-or-registered-server-group.md)  
  
