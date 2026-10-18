---
title: Prevent Automatic Startup of an Instance (SQL Server Configuration Manager)
description: Find out how to prevent an instance of SQL Server from starting automatically. See how to set the start mode to manual to accomplish this task.
author: adbadram
ms.author: adbadram
ms.date: 03/16/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "automatic SQL Server startup"
  - "SQL Server, stopping"
  - "SQL Server, automatic startup"
  - "canceling automatic startup"
  - "stopping SQL Server"
  - "preventing automatic startups [SQL Server]"
---
# SQL Server Configuration Manager: Prevent automatic startup of an instance

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to prevent an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from starting automatically in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is normally configured to start automatically. You can change that by setting the start mode for the instance to manual.

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

Open SQL Server Configuration Manager from the Windows Start menu.

[!INCLUDE [open-sql-server-configuration-manager](../../includes/paragraph-content/open-sql-server-configuration-manager.md)]

### Prevent automatic startup of an instance of SQL Server

1. In SQL Server Configuration Manager, expand **Services**, and then select **SQL Server**.

1. In the details pane, right-click **MSSQLServer**, and then select **Properties.**

1. In the **SQL Server \<**_instancename_**> Properties** dialog box, on the **Service** tab, in the **General** box, set the value of **Start Mode** to **Manual**.

1. Select **OK** to close the **SQL Server \<**_instancename_**> Properties** dialog box, and then close [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

## Related content

- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)
