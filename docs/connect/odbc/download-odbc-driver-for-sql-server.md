---
title: Download ODBC Driver for SQL Server
description: Download the Microsoft ODBC Driver for SQL Server to develop native-code applications that connect to Microsoft SQL products.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl, vanto
ms.date: 11/25/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - linux-related-content
  - ignite-2024
---

# Download ODBC Driver for SQL Server

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The Microsoft ODBC Driver for SQL Server provides run-time support for native-code applications that connect to Microsoft SQL products. Use Driver 18 to create new applications or to access features in newer database versions.

## Prerequisites

Driver 17.11 and earlier versions and Driver 18.0 through 18.6 require a separate installation of the Visual C++ v14 Redistributable on Windows. Starting with Driver 17.12 and Driver 18.7, the driver installer includes the runtime. When you install the Redistributable separately, use the latest supported version from [Microsoft Visual C++ Redistributable latest supported downloads](/cpp/windows/latest-supported-vc-redist). The latest supported version meets every minimum in the following table.

| Driver version | Architecture | Separate Visual C++ install required | Minimum Visual C++ version |
| --- | --- | --- | --- |
| 18.7.1.1 | x86, x64, ARM64 | No. The installer includes version 14.44.35211.0. | Not applicable |
| 17.12.1.1 | x86, x64 | No. The installer includes version 14.44.35211.0. | Not applicable |
| 17.11.1.1 | x86, x64 | Yes. Download [x86](https://aka.ms/vc14/vc_redist.x86.exe) or [x64](https://aka.ms/vc14/vc_redist.x64.exe). | 14.15 |
| 18.6.2.1 | x86, x64, ARM64 | Yes. Download [x86](https://aka.ms/vc14/vc_redist.x86.exe), [x64](https://aka.ms/vc14/vc_redist.x64.exe), or [ARM64](https://aka.ms/vc14/vc_redist.arm64.exe). | 14.34 |

The minimum is the major and minor version. It follows the [Visual C++ Redistributable compatibility rule](/cpp/windows/determining-which-dlls-to-redistribute), which requires the Redistributable's major version to match the toolset and its minor version to be the same or higher.

Choose an installer that matches the Windows operating-system architecture. For Driver 18, the x64 installer installs x64 and 32-bit drivers, and the ARM64 installer installs ARM64 and 32-bit drivers. Driver 17.11.1.1 has x86 and x64 installers only; use Driver 18 for native ARM64 applications. Use the x86 installer only on 32-bit Windows. The Visual C++ v14 Redistributable you install separately must match the architecture of each application that uses the driver. Its version must be at least as recent as the newest Microsoft C++ (MSVC) build tools used to build any application component. For more information, see [C++ binary compatibility between Visual Studio versions: Restrictions](/cpp/porting/binary-compat-2015-2017#restrictions).

Linux and macOS don't require this component. For Linux and macOS prerequisites, see the installation guides linked in the [Download for Linux and macOS](#download-for-linux-and-macos) section.

## Download for Windows

The redistributable installer for Microsoft ODBC Driver 18 for SQL Server installs the client components required to run an application that uses the driver. It can also install the header files needed to develop an application that uses the ODBC application programming interface (API).

Version 18.7.1.1 is the latest general availability (GA) version. If you have a previous version of Microsoft ODBC Driver 18 for SQL Server installed, installing 18.7.1.1 upgrades it to 18.7.1.1. You can install the Microsoft ODBC Driver 18 for SQL Server side by side with Microsoft ODBC Driver 17 for SQL Server.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2378279)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2378646)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 18 for SQL Server (ARM64)](https://go.microsoft.com/fwlink/?linkid=2378647)**  

> [!NOTE]
> Use the x86 installer on 32-bit Windows, the x64 installer on x64 Windows, or the ARM64 installer on ARM64 Windows. The x64 and ARM64 installers each install drivers for native 64-bit and 32-bit applications.

### Version information    

- Release number: 18.7.1.1
- Released: September 7, 2026

> [!NOTE]
> If you're accessing this page from a non-English language version, and want to see the most up-to-date content, select **Read in English** at the top of this page. You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

Select a language to download a localized Driver 18.7.1.1 package:

**Microsoft ODBC Driver 18.7.1.1 for SQL Server (x64):**  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x404) | [Czech](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x405) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x412) | [Polish](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x415) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x40a) | [Turkish](https://go.microsoft.com/fwlink/?linkid=2378279&clcid=0x41f)

**Microsoft ODBC Driver 18.7.1.1 for SQL Server (x86):**  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x404) | [Czech](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x405) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x412) | [Polish](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x415) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x40a) | [Turkish](https://go.microsoft.com/fwlink/?linkid=2378646&clcid=0x41f)

**Microsoft ODBC Driver 18.7.1.1 for SQL Server (ARM64):**  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x404) | [Czech](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x405) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x412) | [Polish](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x415) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x40a) | [Turkish](https://go.microsoft.com/fwlink/?linkid=2378647&clcid=0x41f)

## Version 17

Version 17.12.1.1 is the latest general availability (GA) version of the 17.x driver. If you have a previous version of Microsoft ODBC Driver 17 for SQL Server installed, installing 17.12.1.1 upgrades it to 17.12.1.1.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 17 for SQL Server (x64)](https://go.microsoft.com/fwlink/?linkid=2388792)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft ODBC Driver 17 for SQL Server (x86)](https://go.microsoft.com/fwlink/?linkid=2388986)**  

The Windows downloads for Driver 17.12.1.1 are x86 and x64 installers. For native ARM64 applications on Windows, use Driver 18.

- Release number: 17.12.1.1
- Released: November 25, 2026

Select a language to download a localized Driver 17.12.1.1 package:

Microsoft ODBC Driver 17.12.1.1 for SQL Server (x64):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2388792&clcid=0x40a)

Microsoft ODBC Driver 17.12.1.1 for SQL Server (x86):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2388986&clcid=0x40a)

### Release notes for Windows

For more information about this release on Windows, see [the Windows release notes](windows\release-notes-odbc-sql-server-windows.md).

### Previous releases for Windows

To download previous releases for Windows, see [previous Microsoft ODBC Driver for SQL Server releases](windows\release-notes-odbc-sql-server-windows.md#previous-releases).

## Download for Linux and macOS

Use a package manager on Linux or macOS to download and install the Microsoft ODBC Driver for SQL Server. For instructions, see:
[Install ODBC for SQL Server (Linux)](linux-mac\installing-the-microsoft-odbc-driver-for-sql-server.md)  
[Install ODBC for SQL Server (macOS)](linux-mac\install-microsoft-odbc-driver-sql-server-macos.md)  

For offline installation, select the link for your distribution and driver version.

> [!NOTE]
> Packages named `msodbcsql18-*` are for Driver 18 and include multiple releases. For the latest release, see the [Linux and macOS release notes](linux-mac/release-notes-odbc-sql-server-linux-mac.md). Packages named `msodbcsql-*` are for Driver 13.

### Alpine

Verify package integrity and authenticity by using the published Pretty Good Privacy (PGP) signatures.

- [18.7.2.1 Alpine driver ARM package](https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/msodbcsql18_18.7.2.1-1_arm64.apk) ([PGP Signature](https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/msodbcsql18_18.7.2.1-1_arm64.sig))
- [18.7.2.1 Alpine driver package](https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/msodbcsql18_18.7.2.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/5b705670-6f53-4df8-a084-d6c6098d95d7/msodbcsql18_18.7.2.1-1_amd64.sig))
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
- [17.12.1.1 Alpine package](https://download.microsoft.com/download/01432586-9668-4062-a363-adc7810ae83e/msodbcsql17_17.12.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/01432586-9668-4062-a363-adc7810ae83e/msodbcsql17_17.12.1.1-1_amd64.sig))
- [17.12.1.1 Alpine tools package](https://download.microsoft.com/download/2409b65e-ebd8-429d-bc9c-8821f7526d02/mssql-tools_17.12.1.1-1_amd64.apk) ([PGP Signature](https://download.microsoft.com/download/2409b65e-ebd8-429d-bc9c-8821f7526d02/mssql-tools_17.12.1.1-1_amd64.sig))
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

- Ubuntu 26.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/26.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/26.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 25.10 .deb packages: [v17](https://packages.microsoft.com/ubuntu/25.10/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/25.10/prod/pool/main/m/msodbcsql18/)
- Ubuntu 24.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/24.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/24.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 22.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/22.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/22.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 20.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 18.04 .deb packages: [v17](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql17/) [v18](https://packages.microsoft.com/ubuntu/18.04/prod/pool/main/m/msodbcsql18/)
- Ubuntu 16.04 .deb packages: [v13](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql17/)
- Ubuntu 14.04 .deb packages: [v13](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql/) [v17](https://packages.microsoft.com/ubuntu/14.04/prod/pool/main/m/msodbcsql17/)

For more information about installing the Linux driver, see [Install ODBC for SQL Server (Linux)](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

### macOS

- For more information about macOS packages, see the [Homebrew formulae](https://github.com/Microsoft/homebrew-mssql-release).

For more information about installing the macOS driver, see [Install ODBC for SQL Server (macOS)](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).

### Release notes for Linux and macOS

For more information about releases for Linux and macOS, see [the Linux and macOS release notes](linux-mac\release-notes-odbc-sql-server-linux-mac.md).
