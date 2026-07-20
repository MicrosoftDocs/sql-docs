---
title: "Unknown Service (Log On Tab)"
description: Learn about the Log On tab of the Unknown Service Properties dialog box in SQL Server. See how to use it to specify an account and to start or stop the service.
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
# Unknown Service (Log On tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is unable to identify this service.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager receives service information from the WMI provider on the computer running the service. Either there was an error while reading the service properties, or the service properties aren't complete. To resolve the problem, try closing and reopening [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, or check the WMI provider on the computer running the service.

The WMI provider is a Windows component. For information on how to check permissions on the WMI provider, see [Configure WMI to show server status in SQL Server tools](/ssms/configure-wmi-to-show-server-status-in-sql-server-tools).

If you believe you're viewing the correct service, use the **Log On** tab of the **Unknown Service Properties** dialog box to specify the account used by this service, and to start and stop the service.

## Remarks

By default, only members of the local administrators group can start, stop, pause, resume or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services](/troubleshoot/windows-server/windows-security/grant-users-rights-manage-services).

## Options

#### Local System account

Specify a local system account, which doesn't require a password. However, the local system account might prevent the service from interacting with other servers, depending on the privileges granted to the account.

#### This account

Specify a local or domain user account that uses Windows Authentication. Use a domain user account with minimal rights for services. For information about selecting an account, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

#### Account Name

Specify the local or domain user account name.

#### Password

Type the password of the account.

#### Confirm password

Type the password of the account again.

#### Start

Start the service.

#### Stop

Stop the service.

#### Pause

Pause the service.

#### Resume

Resume a paused service.
