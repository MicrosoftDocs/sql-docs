---
title: SSMS setup failed or requires restart
description: "Troubleshooting SSMS installation problems"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 05/10/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.custom: intro-installation
---


# SQL Server Management Studio (SSMS) setup failed or requires restarting the machine

Infrequently, users encounter SSMS installation problems, and common error messages include:

- "Failed to install MSI package"
- "Microsoft ODBC Driver 17 for SQL Server: A previous installation required a reboot of the machine for changes to take effect"
- "Fatal error during installation (0x80070643)"

## Suggested resolution

Following these steps to uninstall the "Microsoft ODBC Driver 17 for SQL Server" before beginning the SSMS installation commonly allows the setup to succeed if it fails with one of the aforementioned or similar error messages.

1. Close any related applications, including SSMS, Visual Studio, or SQL Server Profiler.
1. Go to Control Panel > Add/Remove Programs.
1. Locate the entry for "Microsoft ODBC Driver 17 for SQL Server" and uninstall. This step may require a restart.
1. Begin the SSMS installation. The latest version is available [here](../download-sql-server-management-studio-ssms.md).

[!INCLUDE[get-help-sql-tools](../../includes/paragraph-content/get-help-sql-tools.md)]

## Related content

- [Download SSMS](../download-sql-server-management-studio-ssms.md)
