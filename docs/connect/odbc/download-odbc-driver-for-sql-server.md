---
title: Download ODBC Driver for SQL Server
description: Download the Microsoft ODBC Driver for SQL Server to develop native-code applications that connect to SQL Server and Azure SQL Database.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl, vanto
ms.date: 09/07/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom:
  - linux-related-content
  - ignite-2024
---

# Download ODBC Driver for SQL Server

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Microsoft ODBC Driver for SQL Server is a single dynamic-link library (DLL) containing run-time support for applications using native-code APIs to connect to SQL Server. Use Microsoft ODBC Driver 18 for SQL Server to create new applications or enhance existing applications that need to take advantage of newer SQL Server features.

## Prerequisites

ODBC Driver 18.6 and earlier versions require the Microsoft Visual C++ Redistributable to be installed on Windows. If this component isn't already installed, download and install the version that matches your system architecture (x64, x86, or ARM64) from [Microsoft Visual C++ Redistributable latest supported downloads](/cpp/windows/latest-supported-vc-redist).

Starting with ODBC Driver 18.7, the Visual C++ Redistributable doesn't need to be preinstalled.

Linux and macOS don't require this component. For Linux and macOS prerequisites, see the installation guides linked in the [Download for Linux and macOS](#download-for-linux-and-macos) section.

## Download for Windows

The redistributable installer for Microsoft ODBC Driver 18 for SQL Server installs the client components, which are required during run time to take advantage of newer SQL Server features. It optionally installs the header files needed to develop an application that uses the ODBC API. Starting with version 17.4.2, the installer also includes and installs the Microsoft Active Directory Authentication Library (ADAL.dll).

Version 18.7.1.1 is the latest general availability (GA) version. If you have a previous version of Microsoft ODBC Driver 18 for SQL Server installed, installing 18.7.1.1 upgrades it to 18.7.1.1. You can install the Microsoft ODBC Driver 18 for SQL Server side by side with Microsoft ODBC Driver 17 for SQL Server.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2378279)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2378646)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (ARM64)](https://go.microsoft.com/fwlink/?linkid=2378647)**  

> [!NOTE]
> Use the x86 installer on 32-bit machines, the x64 installer on x64 machines, or the arm64 installer on ARM64 machines. The x64 and arm64 installers install both 64-bit and 32-bit drivers.

### Version information    

- Release number: 18.7.1.1
- Released: September 7, 2026

> [!NOTE]
> If you're accessing this page from a non-English language version, and want to see the most up-to-date content, select **Read in English** at the top of this page. You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

This release of Microsoft ODBC Driver for SQL Server can be installed in the following languages:

**Microsoft ODBC Driver 18.7.1.1 for SQL Server (x64):**  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x404) | [Czech](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x405) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x412) | [Polish](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x415) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x40a) | [Turkish](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x41f)

**Microsoft ODBC Driver 18.7.1.1 for SQL Server (x86):**  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x404) | [Czech](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x405) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x412) | [Polish](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x415) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x40a) | [Turkish](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x41f)

**Microsoft ODBC Driver 18.7.1.1 for SQL Server (ARM64):**  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x404) | [Czech](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x405) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x412) | [Polish](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x415) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x40a) | [Turkish](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x41f)

## Version 17

Version 17.11.1 is the latest general availability (GA) version of the 17.x driver. If you have a previous version of Microsoft ODBC Driver 17 for SQL Server installed, installing 17.11.1 upgrades it to 17.11.1.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 17 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2361646)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 17 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2361647)**  

- Release number: 17.11.1.1
- Released: April 30, 2026

This release of Microsoft ODBC Driver for SQL Server can be installed in the following languages:

Microsoft ODBC Driver 17.11.1.1 for SQL Server (x64):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2361646&clcid=0x40a)

Microsoft ODBC Driver 17.11.1.1 for SQL Server (x86):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2361647&clcid=0x40a)

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

Packages are signed using Pretty Good Privacy (PGP) signatures to verify integrity and authenticity.

- [18.7.1.1 Alpine driver ARM package](https://download.microsoft.com/download/ade174b7-8cea-4543-91a6-c33ae320c2f0/msodbcsql18_18.7.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/ade174b7-8cea-4543-91a6-c33ae320c2f0/msodbcsql18_18.7.1.1-1_arm64.sig))
- [18.7.1.1 Alpine driver package](https://download.microsoft.com/download/ade174b7-8cea-4543-91a6-c33ae320c2f0/msodbcsql18_18.7.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/ade174b7-8cea-4543-91a6-c33ae320c2f0/msodbcsql18_18.7.1.1-1_amd64.sig))
- [18.7.1.1 Alpine tools ARM package](https://download.microsoft.com/download/a5dcc5e7-6124-49d3-8df4-48d738a0e784/mssql-tools18_18.7.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/a5dcc5e7-6124-49d3-8df4-48d738a0e784/mssql-tools18_18.7.1.1-1_arm64.sig))
- [18.7.1.1 Alpine tools package](https://download.microsoft.com/download/a5dcc5e7-6124-49d3-8df4-48d738a0e784/mssql-tools18_18.7.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/a5dcc5e7-6124-49d3-8df4-48d738a0e784/mssql-tools18_18.7.1.1-1_amd64.sig))
- [18.6.2.1 Alpine driver ARM package](https://download.microsoft.com/download/0b3d5518-b4a7-4a2b-afc7-7ee9e967f93c/msodbcsql18_18.6.2.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/0b3d5518-b4a7-4a2b-afc7-7ee9e967f93c/msodbcsql18_18.6.2.1-1_arm64.sig))
- [18.6.2.1 Alpine driver package](https://download.microsoft.com/download/0b3d5518-b4a7-4a2b-afc7-7ee9e967f93c/msodbcsql18_18.6.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/0b3d5518-b4a7-4a2b-afc7-7ee9e967f93c/msodbcsql18_18.6.2.1-1_amd64.sig))
- [18.6.2.1 Alpine tools ARM package](https://download.microsoft.com/download/cad0d30f-b9b1-4765-a011-81d8a66c8b8d/mssql-tools18_18.6.2.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/cad0d30f-b9b1-4765-a011-81d8a66c8b8d/mssql-tools18_18.6.2.1-1_arm64.sig))
- [18.6.2.1 Alpine tools package](https://download.microsoft.com/download/cad0d30f-b9b1-4765-a011-81d8a66c8b8d/mssql-tools18_18.6.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/cad0d30f-b9b1-4765-a011-81d8a66c8b8d/mssql-tools18_18.6.2.1-1_amd64.sig))
- [18.6.1.1 Alpine driver ARM package](https://download.microsoft.com/download/9dcab408-e0d4-4571-a81a-5a0951e3445f/msodbcsql18_18.6.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/9dcab408-e0d4-4571-a81a-5a0951e3445f/msodbcsql18_18.6.1.1-1_arm64.sig))
- [18.6.1.1 Alpine driver package](https://download.microsoft.com/download/9dcab408-e0d4-4571-a81a-5a0951e3445f/msodbcsql18_18.6.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/9dcab408-e0d4-4571-a81a-5a0951e3445f/msodbcsql18_18.6.1.1-1_amd64.sig))
- [18.6.1.1 Alpine tools ARM package](https://download.microsoft.com/download/b60bb8b6-d398-4819-9950-2e30cf725fb0/mssql-tools18_18.6.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/b60bb8b6-d398-4819-9950-2e30cf725fb0/mssql-tools18_18.6.1.1-1_arm64.sig))
- [18.6.1.1 Alpine tools package](https://download.microsoft.com/download/b60bb8b6-d398-4819-9950-2e30cf725fb0/mssql-tools18_18.6.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/b60bb8b6-d398-4819-9950-2e30cf725fb0/mssql-tools18_18.6.1.1-1_amd64.sig))
- [18.5.1.1 Alpine ARM package](https://download.microsoft.com/download/fae28b9a-d880-42fd-9b98-d779f0fdd77f/msodbcsql18_18.5.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/fae28b9a-d880-42fd-9b98-d779f0fdd77f/msodbcsql18_18.5.1.1-1_arm64.sig))
- [18.5.1.1 Alpine package](https://download.microsoft.com/download/fae28b9a-d880-42fd-9b98-d779f0fdd77f/msodbcsql18_18.5.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/fae28b9a-d880-42fd-9b98-d779f0fdd77f/msodbcsql18_18.5.1.1-1_amd64.sig))
- [18.4.1.1 Alpine ARM package](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_arm64.sig))
- [18.4.1.1 Alpine package](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/7/6/d/76de322a-d860-4894-9945-f0cc5d6a45f8/msodbcsql18_18.4.1.1-1_amd64.sig))
- [18.3.3.1 Alpine ARM package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_arm64.sig))
- [18.3.3.1 Alpine package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.3.1-1_amd64.sig))
- [18.3.2.1 Alpine ARM package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_arm64.sig))
- [18.3.2.1 Alpine package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.2.1-1_amd64.sig))
- [18.3.1.1 Alpine ARM package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_arm64.sig))
- [18.3.1.1 Alpine package](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/3/5/5/355d7943-a338-41a7-858d-53b259ea33f5/msodbcsql18_18.3.1.1-1_amd64.sig))
- [18.2.1.1 Alpine package](https://download.microsoft.com/download/1/f/f/1fffb537-26ab-4947-a46a-7a45c27f6f77/msodbcsql18_18.2.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/1/f/f/1fffb537-26ab-4947-a46a-7a45c27f6f77/msodbcsql18_18.2.1.1-1_amd64.sig))
- [18.1.2.1 Alpine package](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.2.1-1_amd64.sig))
- [18.1.1.1 Alpine package](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/8/6/8/868e5fc4-7bfe-494d-8f9d-115cbcdb52ae/msodbcsql18_18.1.1.1-1_amd64.sig))
- [18.0.1.1 Alpine package](https://download.microsoft.com/download/b/9/f/b9f3cce4-3925-46d4-9f46-da08869c6486/msodbcsql18_18.0.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/b/9/f/b9f3cce4-3925-46d4-9f46-da08869c6486/msodbcsql18_18.0.1.1-1_amd64.sig))
- [17.11.1.1 Alpine package](https://download.microsoft.com/download/607ebe2c-e17c-4c34-b367-10a75b83bef9/msodbcsql17_17.11.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/607ebe2c-e17c-4c34-b367-10a75b83bef9/msodbcsql17_17.11.1.1-1_amd64.sig))
- [17.11.1.1 Alpine tools package](https://download.microsoft.com/download/aca0282f-6a67-49c0-ae08-887d59d16d1a/mssql-tools_17.11.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/aca0282f-6a67-49c0-ae08-887d59d16d1a/mssql-tools_17.11.1.1-1_amd64.sig))
- [17.9.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.9.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.9.1.1-1_amd64.sig))
- [17.8.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.8.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.8.1.1-1_amd64.sig))
- [17.7.2.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.2.1-1_amd64.sig))
- [17.7.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.7.1.1-1_amd64.sig))
- [17.6.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.6.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.6.1.1-1_amd64.sig))
- [17.5.2.2 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.2-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.2-1_amd64.sig))
- [17.5.2.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.1-1_amd64.sig))
- [17.5.1.1 Alpine package](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.1.1-1_amd64.sig))

### Debian

- Debian 13 .deb packages: [v17](https://packages.microsoft.com/debian/13/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/13/prod/pool/main/m/msodbcsql18/)
- Debian 12 .deb packages: [v17](https://packages.microsoft.com/debian/12/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/12/prod/pool/main/m/msodbcsql18/)
- Debian 11 .deb packages: [v17](https://packages.microsoft.com/debian/11/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/11/prod/pool/main/m/msodbcsql18/)
- Debian 10 .deb packages: [v17](https://packages.microsoft.com/debian/10/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/10/prod/pool/main/m/msodbcsql18/)
- Debian 9 .deb packages: [v17](https://packages.microsoft.com/debian/9/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/debian/9/prod/pool/main/m/msodbcsql18/)
- Debian 8 .deb packages: [v13](https://packages.microsoft.com/debian/8/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/debian/8/prod/pool/main/m/msodbcsql17/)

### Azure Linux

- [Azure Linux 3.0 .rpm packages](https://packages.microsoft.com/azurelinux/3.0/prod/ms-non-oss/)

### Red Hat

- [Red Hat 10 .rpm packages](https://packages.microsoft.com/rhel/10/prod/Packages/m/)
- [Red Hat 9 .rpm packages](https://packages.microsoft.com/rhel/9/prod/Packages/m/)
- [Red Hat 8 .rpm packages](https://packages.microsoft.com/rhel/8/prod/Packages/m/)
- [Red Hat 7 .rpm packages](https://packages.microsoft.com/rhel/7/prod/Packages/m/)
- [Red Hat 6 .rpm packages](https://packages.microsoft.com/rhel/6/prod/Packages/m/)

### SUSE

- [SUSE 16 .rpm packages](https://packages.microsoft.com/sles/16/prod/Packages/m/)
- [SUSE 15 .rpm packages](https://packages.microsoft.com/sles/15/prod/Packages/m/)
- [SUSE 12 .rpm packages](https://packages.microsoft.com/sles/12/prod/Packages/m/)
- [SUSE 11 .rpm packages](https://packages.microsoft.com/sles/11/prod/Packages/m/)

### Ubuntu

- Ubuntu 25.10 .deb packages: [v17](https://packages.microsoft.com/ubuntu/25.10/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/25.10/prod/pool/main/m/msodbcsql18/)
- Ubuntu 24.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/24.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/24.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 22.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/22.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/22.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 20.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 18.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 16.04 .deb packages: [v13](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql17/)
- Ubuntu 14.04 .deb packages: [v13](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql17/)

See also [Installing the Linux driver](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

### macOS

- See the [Homebrew formulae](https://github.com/Microsoft/homebrew-mssql-release) for details.

See also [Installing the macOS driver](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).

### Release notes for Linux and macOS

For details about releases for Linux and macOS, see [the Linux and macOS release notes](linux-mac\release-notes-odbc-sql-server-linux-mac.md).
