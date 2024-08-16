---
title: "Change the password of the accounts used (SQL Server Configuration Manager)"
description: Find out how to change the password of the accounts that the Database Engine and the SQL Server Agent use. Learn when it is important to change the password.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
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

This article describes how to change the password of the accounts used by the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager.

The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent run on a computer as a service using credentials that are initially provided during setup. If the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running under a domain account and the password for that account is changed, the password used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be updated to the new password. If the password isn't updated, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might lose access to some domain resources and if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stops, the service don't restart until the password is updated.

To change [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication passwords, see [Choose an authentication mode](../../relational-databases/security/choose-an-authentication-mode.md).

## Before you begin

SQL Server Configuration Manager is the tool designed and authorized to change the settings of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. Changing a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service by using the Windows Service Control Manager (**services.msc**) application doesn't always change all of the necessary settings and might prevent the service from functioning properly. However, in a clustered environment, after changing the password on the active node by using SQL Server Configuration Manager, you must change the password on the passive node by using the Service Control Manager.

## Permissions

You must be an administrator of the computer to change the password used by a service.

## <a id="SSMSProcedure"></a> Use SQL Server Configuration Manager

#### Change the password used by the SQL Server (Database Engine) service

1. Select the **Start** button, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

   Because SQL Server Configuration Manager is a snap-in for the [!INCLUDE [msconame-md](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, SQL Server Configuration Manager doesn't appear as an application in newer versions of Windows.

   | Operating system | Details |
   | --- | --- |
   | **Windows 10 and Windows 11** | To open SQL Server Configuration Manager, on the **Start Page**, type `SQLServerManager16.msc` (for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]). For other versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], replace `16` with the appropriate number. Selecting `SQLServerManager16.msc` opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click `SQLServerManager16.msc`, and then select **Open file location**. In the Windows File Explorer, right-click `SQLServerManager16.msc`, and then select **Pin to Start** or **Pin to taskbar**. |
   | **Windows 8** | To open SQL Server Configuration Manager, in the **Search** charm, under **Apps**, type `SQLServerManager<version>.msc`, such as `SQLServerManager16.msc`, and then press **Enter**. |

1. In SQL Server Configuration Manager, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server (**\<instancename>**)**, and then select **Properties**.

1. In the **SQL Server (**\<instancename>**) Properties** dialog box, on the Log On tab, for the account listed in the **Account Name** box, type the new password in the **Password** and **Confirm Password** boxes, and then select **OK**.

   The password takes effect immediately, without restarting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### Change the password used by the SQL Server Agent service

1. Select the **Start** button, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

1. In SQL Server Configuration Manager, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server Agent (**\<instancename>**)**, and then select **Properties**.

1. In the **SQL Server Agent (**\<instancename>**) Properties** dialog box, on the Log On tab, for the account listed in the **Account Name** box, type the new password in the **Password** and **Confirm Password** boxes, and then select **OK**.

     On a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the password takes effect immediately, without restarting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. On a clustered instance, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might take the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource offline, and require a restart.

## Related content

- [SQL Server Configuration Manager: Connect to another computer](scm-services-connect-to-another-computer.md)
