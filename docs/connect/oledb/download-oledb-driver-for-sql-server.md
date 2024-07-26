---
title: Download Microsoft OLE DB Driver for SQL Server
description: Download the Microsoft OLE DB Driver for SQL Server to develop native Windows applications that connect to SQL Server and Azure SQL Database.
author: David-Engel
ms.author: davidengel
ms.date: 07/09/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Download Microsoft OLE DB Driver for SQL Server

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

The OLE DB Driver for SQL Server is a stand-alone data access application programming interface (API), used for OLE DB. OLE DB Driver for SQL Server is available on Windows and delivers the SQL OLE DB driver in one dynamic-link library (DLL).

## Download

The redistributable installer for Microsoft OLE DB Driver for SQL Server installs the client components required during run time to take advantage of newer SQL Server features. Starting with version 18.3, the installer also includes and installs the Microsoft Active Directory Authentication Library (ADAL.dll).

Microsoft OLE DB Driver 19.3.5 for SQL Server is the latest general availability (GA) version. The Microsoft OLE DB Driver 19 for SQL Server will install side by side with Microsoft OLE DB Driver 18 for SQL Server.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft OLE DB Driver 19 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2278038)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft OLE DB Driver 19 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2278037)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft OLE DB Driver 19 for SQL Server (Arm64)](https://go.microsoft.com/fwlink/?linkid=2277846)**  

> [!NOTE]
> Installation of the [Microsoft Visual C++ Redistributable](/cpp/windows/latest-supported-vc-redist) is a prerequisite,
> and while the x64 installer for Microsoft OLE DB Driver installs both the 64-bit and 32-bit driver, the x64 installer for the Microsoft Visual C++ Redistributable
> doesn't install 32-bit binaries. You must install both the x86 and x64 versions of the C++ redistributable package to install the x64 OLE DB driver package.

### Version information

- Release number: 19.3.5
- Released: July 09, 2024

> [!Note]
> If you are accessing this page from a non-English language version, and want to see the most up-to-date content, please select **Read in English** at the top of this page. You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

This release of Microsoft OLE DB Driver for SQL Server can be installed in the following languages:

Microsoft OLE DB Driver 19.3.5 for SQL Server (x64):  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2278038&clcid=0x40a)

Microsoft OLE DB Driver 19.3.5 for SQL Server (x86):  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2278037&clcid=0x40a)

Microsoft OLE DB Driver 19.3.5 for SQL Server (Arm64):  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2277846&clcid=0x40a)

## Release notes

For details about this release, see [the release notes](release-notes-for-oledb-driver-for-sql-server.md).

## Previous releases

[Previous Microsoft OLE DB Driver for SQL Server Releases](release-notes-for-oledb-driver-for-sql-server.md#previous-releases)

## Related content

- [Release notes for the Microsoft OLE DB Driver for SQL Server](release-notes-for-oledb-driver-for-sql-server.md)  
- [System requirements for OLE DB Driver for SQL Server](system-requirements-for-oledb-driver-for-sql-server.md)  
- [Support policies for OLE DB Driver for SQL Server](applications\support-policies-for-oledb-driver-for-sql-server.md)  
- [When to use OLE DB Driver for SQL Server](when-to-use-oledb-driver-for-sql-server.md)  
- [Installing OLE DB Driver for SQL Server](applications/installing-oledb-driver-for-sql-server.md)  
- [MSOLEDBSQL major version differences](major-version-differences.md)