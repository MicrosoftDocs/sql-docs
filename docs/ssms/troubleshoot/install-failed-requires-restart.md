---
description: "Troubleshooting SSMS installation problems"
title: SSMS setup failed or requires restart
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: c28ffa44-7b8b-4efa-b755-c7a3b1c11ce4
author: dzsquared
ms.author: drskwier
ms.reviewer: matteot, maghan
ms.custom:
  - seo-lt-2019
  - intro-installation
ms.date: 06/18/2021
---


# SQL Server Management Studio (SSMS) setup failed or requires restart
When facing SSMS installation problems, common error messages include:
- "Failed to install MSI package"
- "Microsoft ODBC Driver 17 for SQL Server: A previous installation required a reboot of the machine for changes to take effect."
- "Fatal error during installation (0x80070643)"

## Suggested resolution
Following these steps to uninstall the "Microsoft ODBC Driver 17 for SQL Server" before beginning the SSMS installation commonly allows the setup to succeed if it has previously failed with one of the above or similar error messages.

1. Close any related applications, including SSMS, Visual Studio, or SQL Profiler.
2. Go to Control Panel > Add/Remove Programs.
3. Locate the entry for "Microsoft ODBC Driver 17 for SQL Server" and uninstall.  This step may require a reboot.
4. Begin the SSMS installation.  The latest version is available [here](../download-sql-server-management-studio-ssms.md).

## Next Steps
- [Download SSMS](../download-sql-server-management-studio-ssms.md)
- [SQL Server user feedback](https://aka.ms/sqlfeedback)
