---
title: Move a Registered Server or Server Group
description: "Move a Registered Server or Registered Server Group"
author: markingmyname
ms.author: maghan
ms.date: 07/11/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "moving registered server or server group"
  - "Registered Servers [SQL Server], server groups"
  - "server groups [SQL Server]"
  - "Registered Servers [SQL Server], moving server or server group"
  - "groups [SQL Server], server"
---

# Move a Registered Server or Registered Server Group

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to organize the servers in Registered Servers by moving a registered server or server groups in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. Server groups can contain registered servers, or other server groups. Both servers and server groups can be moved from one server group to another.

## <a id="SSMSProcedure"></a>

### To move a registered server or server group

1. In Registered Servers, right-click a server or server group, and then select **Tasks** > **Move To**.

1. In the **Move Server Registration** dialog box, expand the list of server groups, select the node where you want the server or server group to appear, and then select **OK**.

## Related content

- [Register Servers](./register-servers.md)
- [Create or Edit a Server Group (SQL Server Management Studio)](./create-or-edit-a-server-group-sql-server-management-studio.md)
