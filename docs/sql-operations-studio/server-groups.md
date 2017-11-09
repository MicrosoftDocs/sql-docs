---
title: Server groups in SQL Operations Studio (preview) | Microsoft Docs
description: Learn about server groups in SQL Operations Studio (preview).
keywords:
ms.custom: "tools|sos"
ms.date: "11/08/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Server groups in [!INCLUDE[name-sos](../includes/name-sos-short.md)]

Server groups provide a way to organize your connections to the servers and databases you work with. When you create server groups, the configuration details are saved into *User Settings*. Because *User Settings* are JSON, you can easily copy and share settings with other users who need to connect and work with the same collection of servers. 


## Sharing server groups with other users

As stated in the previous section, server group configuration details are stored in User Settings. To share settings with team mates, perform the following steps:

1. Open *User Settings* by pressing **Ctrl+Shift+P** to open the *Command Palette*.
1. Type *settings* in the search box and from the available settings files, select **Preferences: Open User Settings**.

   ![Open user settings command](./media/server-groups/open-user-settings.png)

1. In the right side list of settings, locate and copy the following two sections:
   - datasource.connectionGroups
   - datasource.connections

   ![server groups in settings](./media/server-groups/server-groups-settings.png)

1. Share these configuration details with users who need to connect to these servers and databases.



## Additional resources
- [Workspace and User settings](settings.md)