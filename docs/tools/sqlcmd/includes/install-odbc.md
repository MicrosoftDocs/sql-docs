---
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/15/2023
ms.service: sql
ms.topic: include
---
**sqlcmd** (ODBC) can be installed cross-platform, on Microsoft Windows, macOS, and Linux.

### Windows

:::image type="icon" source="../../../includes/media/download.svg" border="false"::: **[Download Microsoft Command Line Utilities 15 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2230791)**

:::image type="icon" source="../../../includes/media/download.svg" border="false"::: **[Download Microsoft Command Line Utilities 15 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2231320)**

The command line tools are General Availability (GA), however they're being released with the installer package for [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)].

#### Version information

- Release number: 15.0.4298.1
- Build number: 15.0.4298.1
- Release date: April 7, 2023

**sqlcmd** (ODBC) supports Azure Active Directory (Azure AD) authentication, including Multi-Factor Authentication (MFA) support for Azure SQL Database, Azure Synapse Analytics, and Always Encrypted features.

#### System requirements

- Windows 7 through Windows 11
- Windows Server 2008 through Windows Server 2022

This component requires both the built-in [Windows Installer 5](/windows/win32/msi/what-s-new-in-windows-installer-5-0) and the [Microsoft ODBC Driver 17 for SQL Server](../../../connect/odbc/download-odbc-driver-for-sql-server.md).

### Linux and macOS

See [Install sqlcmd and bcp on Linux](../../../linux/sql-server-linux-setup-tools.md) for instructions to install **sqlcmd** on Linux and macOS.
