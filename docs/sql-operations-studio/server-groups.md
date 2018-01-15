---
title: Server groups in SQL Operations Studio (preview) | Microsoft Docs
description: Learn about server groups in SQL Operations Studio (preview).
ms.custom: "tools|sos"
ms.date: "11/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Server groups in [!INCLUDE[name-sos](../includes/name-sos-short.md)]

Server groups provide a way to organize your connections to the servers and databases you work with. When you create server groups, the configuration details are saved into *User Settings*.

## Create and edit server groups

1. Click **New Server Group** at the top of the *SERVERS* sidebar.
2. Enter a group name and select a color for the group. Optionally, add a description.

   ![add server group](./media/server-groups/add-server-group.png)

To edit an existing server group, right-click the group, and select **Edit Server Group**.

To edit available server group colors, edit the *Server Groups* values in [User Settings](settings.md).

> [!TIP]
> You can drag and drop servers between different Server Groups.



## Additional resources
- [Workspace and User settings](settings.md)