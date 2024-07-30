---
title: Download ODBC Driver for SQL Server
description: Download the Microsoft ODBC Driver for SQL Server to develop native-code applications that connect to SQL Server and Azure SQL Database.
author: David-Engel
ms.author: davidengel
ms.reviewer: v-chojas
ms.date: 07/31/2024
ms.service: sql
ms.subservice: connectivity
ms.custom: linux-related-content
ms.topic: conceptual
---

# Download ODBC Driver for SQL Server

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Microsoft ODBC Driver for SQL Server is a single dynamic-link library (DLL) containing run-time support for applications using native-code APIs to connect to SQL Server. Use Microsoft ODBC Driver 18 for SQL Server to create new applications or enhance existing applications that need to take advantage of newer SQL Server features.

## Download for Windows

The redistributable installer for Microsoft ODBC Driver 18 for SQL Server installs the client components, which are required during run time to take advantage of newer SQL Server features. It optionally installs the header files needed to develop an application that uses the ODBC API. Starting with version 17.4.2, the installer also includes and installs the Microsoft Active Directory Authentication Library (ADAL.dll).

Version 18.4.1.1 is the latest general availability (GA) version. If you have a previous version of Microsoft ODBC Driver 18 for SQL Server installed, installing 18.4.1.1 upgrades it to 18.4.1.1. The Microsoft ODBC Driver 18 for SQL Server can be installed side by side with Microsoft ODBC Driver 17 for SQL Server.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2280794)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2281260)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (ARM64)](https://go.microsoft.com/fwlink/?linkid=2281322)**  

> [!NOTE]
> Use the x86 installer for 32-bit machines, or the x64 installer to install both 64-bit and 32-bit drivers on a 64-bit machine.

### Version information

- Release number: 18.4.1.1
- Released: July 31, 2024

> [!NOTE]
> If you are accessing this page from a non-English language version, and want to see the most up-to-date content, please select **Read in English** at the top of this page. You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

This release of Microsoft ODBC Driver for SQL Server can be installed in the following languages:

Microsoft ODBC Driver 18.4.1.1 for SQL Server (x64):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2280794&clcid=0x40a)

Microsoft ODBC Driver 18.4.1.1 for SQL Server (x86):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2281260&clcid=0x40a)

Microsoft ODBC Driver 18.4.1.1 for SQL Server (ARM64):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2281322&clcid=0x40a)

## Version 17

Version 17.10.6 is the latest general availability (GA) version of the 17.x driver. If you have a previous version of Microsoft ODBC Driver 17 for SQL Server installed, installing 17.10.6 upgrades it to 17.10.6.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 17 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2266337)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 17 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2266446)**  

- Release number: 17.10.6.1
- Released: April 9, 2024

This release of Microsoft ODBC Driver for SQL Server can be installed in the following languages:

Microsoft ODBC Driver 17.10.6.1 for SQL Server (x64):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2266337&clcid=0x40a)

Microsoft ODBC Driver 17.10.6.1 for SQL Server (x86):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2266446&clcid=0x40a)

### Release notes for Windows

For details about this release on Windows, see [the Windows release notes](windows\release-notes-odbc-sql-server-windows.md).

### Previous releases for Windows

To download previous releases for Windows, see [previous Microsoft ODBC Driver for SQL Server releases](windows\release-notes-odbc-sql-server-windows.md#previous-releases).

## Download for Linux and macOS

The Microsoft ODBC Driver for SQL Server can be downloaded and installed using package managers for Linux and macOS using the relevant installation instructions:  
[Install ODBC for SQL Server (Linux)](linux-mac\installing-the-microsoft-odbc-driver-for-sql-server.md)  
[Install ODBC for SQL Server (macOS)](linux-mac\install-microsoft-odbc-driver-sql-server-macos.md)  

If you need to download the packages for offline installation, all versions are available via the below links.

> [!Note]
> Packages named `msodbcsql18-*` are the latest version. Packages named `msodbcsql-*` are version 13 of the driver.

### Alpine

- [18.4.1.1 Alpine ARM package](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_arm64.sig))
- [18.4.1.1 Alpine package](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_amd64.sig))
- [18.3.3.1 Alpine ARM package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_arm64.sig))
- [18.3.3.1 Alpine package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_amd64.sig))
- [18.3.2.1 Alpine package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_amd64.sig))
- [18.3.2.1 Alpine package (ARM)](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_arm64.sig))
- [18.3.1.1 Alpine package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_amd64.sig))
- [18.3.1.1 Alpine package (ARM)](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_arm64.sig))
- [18.2.1.1 Alpine package](https://download.microsoft.com/download/1/f/f/1fffb537-26ab-4947-a46a-7a45c27f6f77/msodbcsql18_18.2.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/1/f/f/1fffb537-26ab-4947-a46a-7a45c27f6f77/msodbcsql18_18.2.1.1-1_amd64.sig))
- [18.1.2.1 Alpine package](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.2.1-1_amd64.sig))
- [18.1.1.1 Alpine package](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.1.1-1_amd64.sig))
- [18.0.1.1 Alpine package](https://download.microsoft.com/download/b/9/f/b9f3cce4-3925-46d4-9f46-da08869c6486/msodbcsql18_18.0.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/b/9/f/b9f3cce4-3925-46d4-9f46-da08869c6486/msodbcsql18_18.0.1.1-1_amd64.sig))
- [17.9.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.9.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.9.1.1-1_amd64.sig))
- [17.8.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.8.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.8.1.1-1_amd64.sig))
- [17.7.2.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.2.1-1_amd64.sig))
- [17.7.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.1.1-1_amd64.sig))
- [17.6.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.6.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.6.1.1-1_amd64.sig))
- [17.5.2.2 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.2-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.2-1_amd64.sig))
- [17.5.2.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.1-1_amd64.sig))
- [17.5.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.1.1-1_amd64.sig))

### Debian

- Debian 12 .deb packages: [v17](https://packages.microsoft.com/debian/12/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/12/prod/pool/main/m/msodbcsql18/)
- Debian 11 .deb packages: [v17](https://packages.microsoft.com/debian/11/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/11/prod/pool/main/m/msodbcsql18/)
- Debian 10 .deb packages: [v17](https://packages.microsoft.com/debian/10/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/10/prod/pool/main/m/msodbcsql18/)
- Debian 9 .deb packages: [v17](https://packages.microsoft.com/debian/9/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/9/prod/pool/main/m/msodbcsql18/)
- Debian 8 .deb packages: [v13](https://packages.microsoft.com/debian/8/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/debian/8/prod/pool/main/m/msodbcsql17/)

### Red Hat

- [Red Hat 8 .rpm packages](https://packages.microsoft.com/rhel/8/prod/)
- [Red Hat 7 .rpm packages](https://packages.microsoft.com/rhel/7/prod/)
- [Red Hat 6 .rpm packages](https://packages.microsoft.com/rhel/6/prod/)

### SUSE

- [SUSE 15 .rpm packages](https://packages.microsoft.com/sles/15/prod/)
- [SUSE 12 .rpm packages](https://packages.microsoft.com/sles/12/prod/)
- [SUSE 11 .rpm packages](https://packages.microsoft.com/sles/11/prod/)

### Ubuntu
- Ubuntu 24.04 .deb packages: [v18](https://packages.microsoft.com/ubuntu/24.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 23.04 .deb packages: [v18](https://packages.microsoft.com/ubuntu/23.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 22.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/22.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/22.04/prod/pool/main/m/msodbcsql18/)
[v18](https://packages.microsoft.com/ubuntu/21.10/prod/pool/main/m/msodbcsql18/)
- Ubuntu 20.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 18.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 16.04 .deb packages: [v13](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql17/)
- Ubuntu 14.04 .deb packages: [v13](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql17/)

See also [Installing the Linux driver](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

### macOS

- See the [Homebrew formulae](https://github.com/Microsoft/homebrew-mssql-release) for details.

See also [Installing the macOS driver](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).

### Older Linux releases

- **Red Hat Enterprise Linux 5 and 6 (64-bit)** - [Download Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321)  
- **SUSE Linux Enterprise 11 Service Pack 2 (64-bit)** - [Download Microsoft ODBC Driver 11 Preview for SQL Server - SUSE Linux](https://go.microsoft.com/fwlink/?LinkId=264916)

### Release notes for Linux and macOS

For details about releases for Linux and macOS, see [the Linux and macOS release notes](linux-mac\release-notes-odbc-sql-server-linux-mac.md).
