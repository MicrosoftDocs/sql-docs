---
title: Download and install Microsoft SQL Operations Studio (preview) | Microsoft Docs
description: 'Download and Install Microsoft SQL Operations Studio (preview) for Windows, macOS, or Linux'
keywords: Microsoft SQL Operations Studio (preview), install Microsoft SQL Operations Studio (preview), download Microsoft SQL Operations Studio (preview)
ms.custom: "tools|sos"
ms.date: "11/08/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Download and install Microsoft SQL Operations Studio (preview)

[!INCLUDE[name-sos-short](../includes/name-sos-short.md)] is a free, lightweight, modern database development and management tool that runs on Windows, macOS, and Linux. Use [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] to manage SQL Server running anywhere, Azure SQL Database, and Azure SQL Data Warehouse.   

> [!NOTE]
> This preview version of [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] installs by copying a zip (tar) file to your local computer, extracting (expanding) the file, and running the executable.


## Get SQL Operations Studio (preview) for Windows

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for Windows](https://go.microsoft.com/fwlink/?linkid=862648) to your local computer.
1. Right-click the **.zip** file, and select **Properties**.
1. On the **General** tab, select **Unblock**, and click **Apply**.
1. Browse to the downloaded file and extract it.
1. Run *\sqlops-windows\sqlops.exe*


## Get SQL Operations Studio (preview) for macOS

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for macOS](https://go.microsoft.com/fwlink/?linkid=862647) to your local computer.
2. To expand the contents of the zip, double-click it.
3. Drag *sqlops.app* to the *Applications* folder, making it available in the *Launchpad*.

## Get SQL Operations Studio (preview) for Linux

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for Linux](https://go.microsoft.com/fwlink/?linkid=862646)
1. Open a new Terminal window.
1. Type the following commands to extract the file and launch [!INCLUDE[name-sos](../includes/name-sos-short.md)]:

   ```bash
   cd ~
   cp ~/Downloads/sqlops-linux-<version string>-x64.tar.gz ~
   tar -xvf ~/pgi3-sqlops-linux-<version string>-x64.tar.gz
   echo 'export PATH="$PATH:~/Downloads/sqlops-linux-x64"' >> ~/.bashrc
   source ~/.bashrc
   sqlops .
   ```

## Uninstall SQL Operations Studio (preview)

To uninstall [!INCLUDE[name-sos-short](../includes/name-sos-short.md)], delete the files.

## Supported Operating Systems

[!INCLUDE[name-sos](../includes/name-sos-short.md)] runs on Windows, macOS, and Linux, and is supported on the following platforms:

### Windows
- Windows 10 (64-bit)
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1) (64-bit) - Requires [KB2533623](https://www.microsoft.com/en-us/download/details.aspx?id=26767)
- Windows Server 2016
- Windows Server 2012 R2 (64-bit)
- Windows Server 2012 (64-bit)
- Windows Server 2008 R2 (64-bit)

### macOS
- macOS 10.13 High Sierra
- macOS 10.12 Sierra

### Linux
- Red Hat Enterprise Linux 7.4
- Red Hat Enterprise Linux 7.3
- SUSE Linux Enterprise Server v12 SP2
- Ubuntu 16.04


## Next Steps

See one of the following quickstarts to get started:
- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Data Warehouse](quickstart-sql-dw.md)
