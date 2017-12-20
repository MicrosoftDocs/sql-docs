---
title: Download and install Microsoft SQL Operations Studio (preview) | Microsoft Docs
description: 'Download and Install Microsoft SQL Operations Studio (preview) for Windows, macOS, or Linux'
ms.custom: "tools|sos"
ms.date: "12/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Download and install SQL Operations Studio (preview)

[!INCLUDE[name-sos](../includes/name-sos.md)] runs on Windows, macOS, and Linux.

Download and install the latest release, the *December Public Preview*:

|Platform|Download|Release date|
|:---|:---|:---|
|Windows|[Installer](https://go.microsoft.com/fwlink/?linkid=865305)<br>[.zip](https://go.microsoft.com/fwlink/?linkid=865304)|December 19, 2017 |
|macOS|[.zip](https://go.microsoft.com/fwlink/?linkid=865306)|December 19, 2017 |
|Linux|[.deb](https://go.microsoft.com/fwlink/?linkid=865308)<br>[.rpm](https://go.microsoft.com/fwlink/?linkid=865309)<br>[.tar.gz](https://go.microsoft.com/fwlink/?linkid=865307)|December 19, 2017|

For details about the latest release, see the [release notes](release-notes.md).

## Get SQL Operations Studio (preview) for Windows

This release of [!INCLUDE[name-sos](../includes/name-sos-short.md)] includes a standard Windows installer experience, and a .zip: 

**Installer**

1. Download and run the [[!INCLUDE[name-sos](../includes/name-sos-short.md)] installer for Windows](https://go.microsoft.com/fwlink/?linkid=865305).
1. Start the [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] app.


**.zip file**

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] .zip for Windows](https://go.microsoft.com/fwlink/?linkid=865304).
2. Browse to the downloaded file and extract it.
3. Run `\sqlops-windows\sqlops.exe`


## Get SQL Operations Studio (preview) for macOS

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for macOS](https://go.microsoft.com/fwlink/?linkid=865306).
2. To expand the contents of the zip, double-click it.
3. To make [!INCLUDE[name-sos](../includes/name-sos-short.md)] available in the *Launchpad*, drag *sqlops.app* to the *Applications* folder.


## Get SQL Operations Studio (preview) for Linux

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for Linux](https://go.microsoft.com/fwlink/?linkid=865307).
1. To extract the file and launch [!INCLUDE[name-sos](../includes/name-sos-short.md)], open a new Terminal window and type the following commands:

   ```bash
   cd ~
   cp ~/Downloads/sqlops-linux-<version string>.tar.gz ~
   tar -xvf ~/sqlops-linux-<version string>.tar.gz
   echo 'export PATH="$PATH:~/sqlops-linux-x64"' >> ~/.bashrc
   source ~/.bashrc
   sqlops
   ```

   > [!NOTE]
   > On Ubuntu and Redhat, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:
   
   **Ubuntu:** 
   ```bash
   sudo apt-get install libxss1

   sudo apt-get install libgconf-2-4

   sudo apt-get install libunwind8
   ```

   **Redhat:** 
   ```bash
   yum install libXScrnSaver
   ```

## Uninstall SQL Operations Studio (preview)

If you installed [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] with a .zip or other archive, then simply delete the files.

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

Contribute to [!INCLUDE[name-sos](../includes/name-sos-short.md)]:
- [https://github.com/Microsoft/sqlopsstudio](https://github.com/Microsoft/sqlopsstudio) 

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839) and [usage data collection](usage-data-collection.md).
