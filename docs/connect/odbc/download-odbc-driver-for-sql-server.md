---
title: Download ODBC Driver for SQL Server
ms.date: 03/10/2020
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 53b09784-bb9d-4fd4-99d3-0492b3308ac4
author: rothja
ms.author: jroth
---

# Download ODBC Driver for SQL Server

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

Microsoft ODBC Driver for SQL Server is a single dynamic-link library (DLL) containing run-time support for applications using native-code APIs to connect to SQL Server. Use Microsoft ODBC Driver 17 for SQL Server to create new applications or enhance existing applications that need to take advantage of newer SQL Server features.

## Download for Windows

The redistributable installer for Microsoft ODBC Driver 17 for SQL Server installs the client components, which are required during run time to take advantage of newer SQL Server features. It optionally installs the header files needed to develop an application that uses the ODBC API. Starting with version 17.4.2, the installer also includes and installs the Microsoft Active Directory Authentication Library (ADAL.dll).

Version 17.5.2 is the latest general availability (GA) version. If you have a previous version of Microsoft ODBC Driver 17 for SQL Server installed, installing 17.5.2 upgrades it to 17.5.2.

**[![Download](../../ssms/media/download-icon.png) Download Microsoft ODBC Driver 17 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2120137)**  
**[![Download](../../ssms/media/download-icon.png) Download Microsoft ODBC Driver 17 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2120140)**  

### Version information

- Release number: 17.5.2.1
- Released: March 6, 2020

> [!Note]
> If you are accessing this page from a non-English language version, and want to see the most up-to-date content, please visit the [US-English version of the site](https://aka.ms/downloadmsodbcsqlenglish). You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

This release of Microsoft ODBC Driver for SQL Server can be installed in the following languages:

Microsoft ODBC Driver 17.5.2 for SQL Server (x64):  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x40a)

Microsoft ODBC Driver 17.5.2 for SQL Server (x86):  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x40a)

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
> Packages named `msodbcsql17-*` are the latest version. Packages named `msodbcsql-*` are version 13 of the driver.

### Debian

- [Debian 10 .deb packages](https://packages.microsoft.com/debian/10/prod/pool/main/m/msodbcsql17/)
- [Debian 9 .deb packages](https://packages.microsoft.com/debian/9/prod/pool/main/m/msodbcsql17/)
- [Debian 8 .deb packages](https://packages.microsoft.com/debian/8/prod/pool/main/m/msodbcsql17/)
- [Debian 8 .deb packages (msodbcsql 13.x)](https://packages.microsoft.com/debian/8/prod/pool/main/m/msodbcsql/)

### RedHat

- [RedHat 8 .rpm packages](https://packages.microsoft.com/rhel/8/prod/)
- [RedHat 7 .rpm packages](https://packages.microsoft.com/rhel/7/prod/)
- [RedHat 6 .rpm packages](https://packages.microsoft.com/rhel/6/prod/)

### Suse

- [SuSE 15 .rpm packages](https://packages.microsoft.com/sles/15/prod/)
- [SuSE 12 .rpm packages](https://packages.microsoft.com/sles/12/prod/)
- [SuSE 11 .rpm packages](https://packages.microsoft.com/sles/11/prod/)

### Ubuntu

- [Ubuntu 19.04 .deb packages](https://packages.microsoft.com/ubuntu/19.04/prod/pool/main/m/msodbcsql17/)
- [Ubuntu 18.10 .deb packages](https://packages.microsoft.com/ubuntu/18.10/prod/pool/main/m/msodbcsql17/)
- [Ubuntu 18.04 .deb packages](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql17/)
- [Ubuntu 16.04 .deb packages](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql17/)
- [Ubuntu 14.04 .deb packages](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql17/)
- [Ubuntu 16.04 .deb packages (msodbcsql 13.x)](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/)
- [Ubuntu 14.04 .deb packages (msodbcsql 13.x)](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql/)

See also [Installing the Linux driver](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

### macOS

- See the [Homebrew formulae](https://github.com/Microsoft/homebrew-mssql-release) for details.

See also [Installing the macOS driver](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).

### Older Linux releases

- **Red Hat Enterprise Linux 5 and 6 (64-bit)** - [Download Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321)  
- **SUSE Linux Enterprise 11 Service Pack 2 (64-bit)** - [Download Microsoft ODBC Driver 11 Preview for SQL Server - SUSE Linux](https://go.microsoft.com/fwlink/?LinkId=264916)

### Release notes for Linux and macOS

For details about releases for Linux and macOS, see [the Linux and macOS release notes](linux-mac\release-notes-odbc-sql-server-linux-mac.md).
