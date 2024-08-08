---
title: Configure login auditing
description: Configure login auditing for SQL Server on Windows, using SSMS.
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: randolphwest
ms.date: 07/12/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "auditing [SQL Server]"
  - "audits [SQL Server], logins"
  - "logins [SQL Server], auditing"
---

# Configure login auditing (SQL Server Management Studio)

[!INCLUDE [sql-windows-only](../includes/applies-to-version/sql-windows-only.md)]

This article describes how to configure login auditing in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows, to monitor [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] login activity. Login auditing can be configured to write to the error log on the following events.

- Failed logins
- Successful logins
- Both failed and successful logins

You must restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] before this option will take effect.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### Configure login auditing

1. In SQL Server Management Studio, connect to an instance of the [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] with Object Explorer.

1. In Object Explorer, right-click the server name, and then select **Properties**.

1. On the **Security** page, under **Login** auditing, select the desired option and close the **Server Properties** page.

1. In Object Explorer, right-click the server name, and then select **Restart**.

    :::image type="content" source="media/configure-login-auditing-sql-server-management-studio/configure-login-auditing-window.png" alt-text="Screenshot of the configure login audit window.":::

## Related content

- [SQL Server Management Studio components and configuration](tutorials/ssms-configuration.md)
- [Configure SQL Server Agent](agent/configure-sql-server-agent.md)
- [Create a server audit and database audit specification](../relational-databases/security/auditing/create-a-server-audit-and-database-audit-specification.md)
