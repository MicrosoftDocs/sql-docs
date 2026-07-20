---
title: "Check Parameters System Configuration Checker"
description: During SQL Server Setup, the System Configuration Checker for conditions that prevent a successful SQL Server installation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: concept-article
helpviewer_keywords:
  - "installing SQL Server, system configuration checks"
  - "failed system configuration checks [SQL Server]"
  - "verifying configuration before installation"
  - "SCC [SQL Server]"
  - "system configuration checker"
  - "scanning computer before installation [SQL Server]"
  - "checking configuration before installation"
  - "status information [SQL Server], system configuration checker"
  - "parameters [SQL Server], system configuration checker"
  - "configuration checkers [SQL Server]"
  - "Setup [SQL Server], system configuration checker"
monikerRange: ">=sql-server-2017"
---
# Check parameters for the System Configuration Checker

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

During [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup, the System Configuration Checker (SCC) scans the computer where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will be installed.

## Remarks

The SCC checks for conditions that prevent a successful [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation. Before Setup starts the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard, the SCC retrieves the status of each item. It then compares the result with required conditions and provides guidance for removal of blocking issues.

The system configuration checker generates a report which contains a short description for each executed rule, and the execution status. The system configuration check report is located at `%programfiles%\Microsoft SQL Server\140\Setup Bootstrap\Log\<yyyyMMdd_HHmm>`.

## Related content

- [Hardware and software requirements for SQL Server 2022](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [Security considerations for a SQL Server installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)
- [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md)
