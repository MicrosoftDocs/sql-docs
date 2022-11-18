---
title: "Maintenance Plan (Manage Connections)"
description: Maintenance Plan (Manage Connections)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.maint.connections.f1"
ms.assetid: 95ad9375-6584-423e-b9de-0e86782f8017
---
# Maintenance Plan (Manage Connections)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Manage Connections** dialog box to specify the properties of connections used by maintenance plans. When you create a maintenance plan, a local connection to the server where you created the plan is created. Use this connection to create tasks that perform work on this local connection. When needed, use the **Manage Connections** dialog box to add them. When additional connections are configured they appear in the connections box for each task.  
  
## Options  
 **Server**  
 The server instance.  
  
 **Authentication**  
 Indicates whether the connection is made with Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  

> [!IMPORTANT]  
> The package is stored in **msdb** database with its **ProtectionLevel** set to **ServerStorage**, so when *SQL Server Authentication* is used, the password will not be encrypted in **msdb**. You can use *SQL Server Authentication* as long as **msdb** is secured, but using *Windows Authentication* is recommended

## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)  
  
  
