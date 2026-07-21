---
title: "SQL Server Properties (Log On Tab)"
description: Learn about the Log On tab of the SQL Server Properties dialog box. Use this tab to specify the account that SQL Server uses and to start or stop the service.
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
# SQL Server Properties (Log On tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use the **Log On** tab of the **SQL Server Properties** dialog box to specify the account used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service, to change the password of an account, and to start and stop the service. Changing the password of an account takes effect immediately.

When you change the account name used by a service on a clustered instance, the new account must be a member of the domain group specified during setup for the service, or you must have permission to add members to that group. If you don't have permission to modify group membership, contact the domain administrator.

For more information about selecting an account to run the service, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

## Remarks

By default, only members of the local administrators group can start, stop, pause, resume or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services](/troubleshoot/windows-server/windows-security/grant-users-rights-manage-services).

## Options

#### Built-in account

- **Local System**: Specify the local system account. This account doesn't require a password. However, the local system account might prevent the service from interacting with other servers, depending on the privileges granted to the account.

- **Local Service**: Specify the local service account. This account doesn't require a password. However, the local service account might prevent the service from interacting with other servers, depending on the privileges granted to the account.

- **Network Service**: Don't use the Network Service account for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent services. Local User or Domain User accounts are more appropriate for these services.

#### This account

Specify a local or domain user account that uses Windows Authentication. Use a domain user account that has minimal rights for services.

#### Account Name

Specify the local or domain user account name.

#### Password

Type the password of the account.

#### Confirm password

Type the password of the account again.

#### Start

Start the service.

> [!NOTE]  
> When starting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], a WMI error containing the phrase "not implemented [0x80004001]" might indicate that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] isn't installed on the target computer.

#### Stop

Stop the service.

#### Pause

Pause the service.

#### Resume

Resume a paused service.
