---
title: "Understand SQL Server Management Studio Windows Management"
description: Understand how to manage the windows for SQL Server Management Studio tools.
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: randolphwest
ms.date: 06/03/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "autohide [SQL Server Management Studio]"
  - "SQL Server Management Studio [SQL Server], tool windows"
  - "push pin [SQL Server Management Studio]"
  - "tool windows [SQL Server Management Studio]"
---
# Understand SQL Server Management Studio Windows management

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The tool windows in [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)] are part of a highly functional, flexible, and efficient system that allows you to:

- Maximize the user workspace for development and management.
- Reduce the number of unused windows displayed at one time.
- Easily customize the user environment.

Manipulating windows is central to the [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)] environment. Users can easily access the tools and windows they use frequently. Users can control how much space they want to allocate to different information, and the environment should maximize the space available for editing queries accordingly. Windows can be moved to different locations on the screen. Many windows can be undocked and dragged out of the [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)] frame, which is useful when using more than one monitor.

You can increase your editing space while maintaining functionality with the Auto Hide feature. This feature displays the window as a tab within a bar along the border of the main [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)] environment. When the pointer is placed over one of these tabs, the underlying window reveals itself. Auto Hide for a window can be toggled by selecting the **Auto Hide** button, represented by a pushpin in the upper-right corner of the window. There's also an **Auto Hide All** option on the **Window** menu.

Some components can be configured in either tabbed mode where components appear as tabs in the same docking location, or in multiple document interface (MDI) mode where each document has its own window. To configure this feature, navigate to **Tools** > **Options** > **Environment**, and select **General**.

## Cached identity information

When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For Windows Authentication, information about membership in Windows groups is included. The identity remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the account must sign out from the authentication authority (Windows or [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]), and sign in again. A member of the **sysadmin** fixed server role, or any login with the `ALTER ANY CONNECTION` permission, can use the `KILL` command to end a connection and force a login to reconnect. [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. To force reconnection, close all connections.

## Related content

- [Use SQL Server Management Studio](sql-server-management-studio-ssms.md)
- [The SQL Server Management Studio Environment](the-sql-server-management-studio-environment.md)
