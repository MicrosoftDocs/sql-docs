---
title: "SQL Server Browser Properties (Log on Tab)"
description: Learn about the Log On tab of the SQL Server Browser Properties dialog box. See how to use this tab to specify an account and to start or stop the service.
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
# SQL Server Browser Properties (Log On tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser program runs as a service on the server. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens for incoming requests for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens on a UDP port and accepts unauthenticated requests using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Resolution Protocol (SSRP).

Changing the password of an account takes effect immediately without restarting the service.

## Options

#### Local System account

Run the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service in the security context of the Local System account. When possible, use a low-permission account instead.

#### This account

Specify a local or domain user account that uses Windows Authentication. Use a domain user account with minimal rights for services. For information about selecting an account, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

#### Browse

Browse for a user or built-in security principal.

> [!IMPORTANT]  
> Use a low-permission account. For information about the permissions required for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service, see the Security section of [SQL Server Browser service](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md).

#### Password

Enter the password of the security principal.

#### Confirm password

Confirm the password of the security principal.

#### Service status

Indicates whether this service is running, stopped, or disabled. "**...**" indicates a state change is pending.

#### Start

Start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.

#### Stop

Stop the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.

#### Pause

Pause the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.

#### Resume

Resume a paused [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.

## Related content

- [SQL Server Browser service (Database Engine and SSAS)](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md)
