---
title: Change the Password of the Accounts Used (SQL Server Configuration Manager)
description: "Find out how to change the password of the accounts that the Database Engine and the SQL Server Agent use. Learn when it's important to change the password."
author: adbadram
ms.author: adbadram
ms.date: 03/16/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "expired password [SQL Server], SQL Server Agent"
  - "passwords [SQL Server], SQL Server Agent service"
  - "passwords [SQL Server], changing"
  - "expired password [SQL Server], Database Engine"
  - "passwords [SQL Server], SQL Server service"
  - "Database Engine [SQL Server], passwords"
  - "changing passwords used by SQL Server"
  - "modifying passwords"
---
# SQL Server Configuration Manager: Change the password of the accounts used

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to change the password of the accounts used by the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent by using SQL Server Configuration Manager.

The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent run on a computer as a service using credentials that are initially provided during setup. If the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running under a domain account and the password for that account is changed, the password used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be updated to the new password. If the password isn't updated, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might lose access to some domain resources and if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stops, the service don't restart until the password is updated.

To change [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication passwords, see [Choose an authentication mode](../../relational-databases/security/choose-an-authentication-mode.md).

## Before you begin

SQL Server Configuration Manager is the tool designed and authorized to change the settings of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. Changing a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service by using the Windows Service Control Manager (**services.msc**) application doesn't always change all of the necessary settings and might prevent the service from functioning properly. However, in a clustered environment, after changing the password on the active node by using SQL Server Configuration Manager, you must change the password on the passive node by using the Service Control Manager.

It's also possible to automate password management through [group-managed service accounts](configure-windows-service-accounts-and-permissions.md#GMSA).

## Permissions

You must be an administrator of the computer to change the password used by a service.

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

Open SQL Server Configuration Manager from the Windows Start menu.

[!INCLUDE [open-sql-server-configuration-manager](../../includes/paragraph-content/open-sql-server-configuration-manager.md)]

### Change the password used by the SQL Server (Database Engine) service

1. In SQL Server Configuration Manager, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server (**\<instancename>**)**, and then select **Properties**.

1. In the **SQL Server (**\<instancename>**) Properties** dialog box, on the Log On tab, for the account listed in the **Account Name** box, type the new password in the **Password** and **Confirm Password** boxes, and then select **OK**.

   The password change made in SQL Server Configuration Manager takes effect immediately, without the need to restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. If you use the Windows Services app (`services.msc`) to change the account password, a service restart is required.

### Change the password used by the SQL Server Agent service

1. In SQL Server Configuration Manager, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server Agent (**\<instancename>**)**, and then select **Properties**.

1. In the **SQL Server Agent (**\<instancename>**) Properties** dialog box, on the Log On tab, for the account listed in the **Account Name** box, type the new password in the **Password** and **Confirm Password** boxes, and then select **OK**.

   On a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the password takes effect immediately, without restarting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. On a clustered instance, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might take the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource offline, and require a restart.

## Related content

- [SQL Server Configuration Manager: Connect to another computer](scm-services-connect-to-another-computer.md)
