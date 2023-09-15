---
title: Configure automated patching for Azure Arc-enabled SQL Server
description: This article explains how to configure Azure Arc-enabled SQL Server automated patching.
author: twright-msft
ms.author: twright
ms.reviewer: mikeray
ms.date: 03/13/2023
ms.subservice: 
ms.topic: conceptual 
---

# Configure automated patching for Arc-enabled SQL Servers preview

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

You can configure automated patching for Azure Arc-enabled SQL Servers.

Automated patching:

- Establishes a maintenance window for an Arc-enabled SQL Server instance
- Works at the host operating system level and applies to all installed SQL Server instances
- Only installs automated updates during this maintenance window 

   For SQL Server, this restriction ensures that system updates and any associated restarts occur at the best possible time for the SQL Server instances and their hosted databases. 

- Currently only works on Windows hosts

   It configures Windows Update/Microsoft Update which is the service that ultimately does the patching. 

- Applies Windows and SQL Server updates marked as **Important** or **Critical**

> [!IMPORTANT]
> Only Windows and SQL Server updates marked as **Important** or **Critical** are installed. Other SQL Server updates, such as service packs and cumulative updates that are not marked as **Important** or **Critical**, must be installed manually.

## Settings

The following table describes the options that you may configure for automated patching. Automated patching is currently only configurable through the Azure portal, ARM API, and Azure Policy.

| Setting | Possible values | Description |
| --- | --- | --- |
| **Automated Patching** |Enable \| **Disable** | Enables or disables automated patching. |
| **Maintenance schedule** | Daily \| Sunday \| Monday \| Tuesday \| Wednesday \| Thursday \| Friday \| Saturday |The weekly schedule for downloading and installing Windows, SQL Server, and Microsoft updates. |
| **Maintenance start hour** |0:00-23:00 |The local start time to apply updates. |

## Configure in the Azure portal

Use the Azure portal to configure automated patching for existing Arc-enabled SQL Server instances.

1. In the portal, locate the **Server - Azure Arc**.
1. Under **Operations**, select **SQL Server Configuration**.
1. Under **Patching (Preview)**, you can enable or disable patching and set a maintenance schedule.

When you enable or configure automated patching, Azure configures the Azure Extension for SQL Server in the background.

> [!NOTE]
> Patching only applies to servers covered with Software Assurance or pay-as-you-go license type. If the server license type is license only, the option to select patching is disabled.
>
> To change the license type to license only, follow these steps:
>
> 1. Unsubscribe from patching and ESU if it is enabled.
> 1. Save the change.
> 1. Wait approximately 5 minutes for the saved change to complete.
> 1. Set the new license type.
>

## Next steps

[Manage SQL Server license and billing options](manage-configuration.md)