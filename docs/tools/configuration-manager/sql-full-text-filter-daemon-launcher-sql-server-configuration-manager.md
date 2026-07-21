---
title: "SQL Full-Text Filter Daemon Launcher (SQL Server Configuration Manager)"
description: Learn about the SQL Full-text Filter Daemon Launcher, a service that SQL Server uses to start a process that it requires for full-text search.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# SQL Full-text Filter Daemon Launcher (SQL Server Configuration Manager)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The SQL Full-text Filter Daemon Launcher (FDHOST Launcher) service is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] full-text search to start the filter daemon host process, which handles full-text search filtering and word breaking.

You must run this service to use full-text search. The FDHOST Launcher service is an instance-aware service that associates with a specific instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The FDHOST Launcher service propagates the service account information to each filter daemon host process it starts.

For more information about the filter daemon host processes, see [Full-Text Search architecture](../../relational-databases/search/full-text-search.md#architecture).

## Configuration tabs

This article provides details about the available options when configuring the SQL Full-text Filter Daemon Launcher service. To access these property tabs, open **SQL Server Configuration Manager**, go to **SQL Server Services**, and open **SQL Full-Text Filter Daemon Launcher**.

- [Log On](#log-on-tab)
- [Service](#service-tab)
- [Advanced](#advanced-tab)

## Log On tab

Use the **Log On** tab of the **SQL Full-text Filter Daemon Launcher Properties** dialog box to specify the account used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] full-text service, to change the password of an account, and to start and stop the service. Changing the password of an account takes effect after restarting the service.

When you change the account name used by a service on a clustered instance, the new account must be a member of the domain group specified during setup for the service, or you must have permission to add members to that group. If you don't have permission to modify group membership, contact the domain administrator.

For more information about selecting an account to run the service, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

## Remarks

By default, only members of the local administrators group can start, stop, pause, resume or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services](/troubleshoot/windows-server/windows-security/grant-users-rights-manage-services).

### Options

#### Built-in account

- **Local System**: Specify the local system account. This account doesn't require a password. However, the local system account might prevent the service from interacting with other servers, depending on the privileges granted to the account.

- **Local Service**: Specify the local service account. This account doesn't require a password. However, the local service account might prevent the service from interacting with other servers, depending on the privileges granted to the account.

- **Network Service**: Don't use the Network Service account for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services or the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent services. Local User or Domain User accounts are more appropriate for these services.

#### This account

Specify a local or domain user account that uses Microsoft Windows Authentication. Use a domain user account that has minimal rights for services.

- **Account Name**: Specify the local or domain user account name.
- **Password**: Type the password of the account.
- **Confirm password**: Type the password of the account again.

#### Service status

- **Start**: Select this button to start the service.
- **Stop**: Select this button to stop the service.
- **Pause**: Select this button to pause the service. This option isn't available for the SQL Full-text Filter Daemon Launcher service.
- **Resume**: Resume a paused service.

## Advanced tab

The **Advanced** tab doesn't show any properties by default. If you define custom properties, they appear on this tab with their associated values.

## Service tab

Use the **Service** tab to view or specify the following options.

### Options

[!INCLUDE [service-tab-1](../includes/configuration-manager/service-tab-1.md)]

#### Host Name

Displays the name of the computer or cluster running the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Full-Text service.

[!INCLUDE [service-tab-2](../includes/configuration-manager/service-tab-2.md)]
