---
title: Download and install
titleSuffix: Azure Data Studio
description: Download and Install Azure Data Studio (Release Candidate) for Windows, macOS, or Linux
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "markingmyname"
ms.author: "maghan"
ms.custom: "seodec18"
ms.date: "08/15/2019"
ms.reviewer: "alayu; sstein"
---

# Download and install Azure Data Studio (Big Data Cluster release candidate)

This article explains how to download and install Azure Data Studio (Big Data Cluster release candidate). This release of Azure Data Studio highlights features related to SQL Server Big Data Cluster release candidate.

For normal, production releases of Azure Data Studio, follow the instructions at [Download and install Azure Data Studio](../azure-data-studio/download.md).

|Platform|Download|Release date| Version |
|:---|:---|:---|:---|
|Windows|[User Installer (recommended)](https://go.microsoft.com/fwlink/?linkid=2100710)<br>[System Installer](https://go.microsoft.com/fwlink/?linkid=2100711)<br>[.zip](https://go.microsoft.com/fwlink/?linkid=2100712)|August 15, 2019 |1.10.0|
|macOS|[.zip](https://go.microsoft.com/fwlink/?linkid=2100809)|August 15, 2019 |1.10.0|
|Linux|[.deb](https://go.microsoft.com/fwlink/?linkid=2100672)<br>[.rpm](https://go.microsoft.com/fwlink/?linkid=2100810)<br>[.tar.gz](https://go.microsoft.com/fwlink/?linkid=2100714)|August 15, 2019 |1.10.0|

For details about the latest release, see the [release notes](../big-data-clusters/release-notes-big-data-custer.md).

## Get Azure Data Studio for Windows

This release of [!INCLUDE[name-sos](../includes/name-sos-short.md)] includes a standard Windows installer experience, and a .zip file.

The *user installer* is recommended because it does not require administrator privileges, which simplifies both installs and upgrades. The user installer does not require Administrator privileges as the location is under your user Local AppData (LOCALAPPDATA) folder. The user installer also provides a smoother background update experience. For more information, see [User setup for Windows](https://code.visualstudio.com/updates/v1_26#_user-setup-for-windows).

**User Installer** (recommended)

1. Download and run the [[!INCLUDE[name-sos](../includes/name-sos-short.md)] *user* installer for Windows](https://go.microsoft.com/fwlink/?linkid=2100710).
2. Start the [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] app.

**System Installer**

1. Download and run the [[!INCLUDE[name-sos](../includes/name-sos-short.md)] *system* installer for Windows](https://go.microsoft.com/fwlink/?linkid=2100711).
2. Start the [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] app.

**.zip file**

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] .zip for Windows](https://go.microsoft.com/fwlink/?linkid=2100712).
2. Browse to the downloaded file and extract it.
3. Run `\azuredatastudio-windows\azuredatastudio.exe`


## Get Azure Data Studio for macOS

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for macOS](https://go.microsoft.com/fwlink/?linkid=2100809).
2. To expand the contents of the zip, double-click it.
3. To make [!INCLUDE[name-sos](../includes/name-sos-short.md)] available in the *Launchpad*, drag *Azure Data Studio.app* to the *Applications* folder.


## Get Azure Data Studio for Linux

1. Download [!INCLUDE[name-sos](../includes/name-sos-short.md)] for Linux by using one of the installers or the tar.gz archive:
    - [.deb](https://go.microsoft.com/fwlink/?linkid=2100672)
    - [.rpm](https://go.microsoft.com/fwlink/?linkid=2100810)
    - [.tar.gz](https://go.microsoft.com/fwlink/?linkid=2100714)
1. To extract the file and launch [!INCLUDE[name-sos](../includes/name-sos-short.md)], open a new Terminal window and type the following commands:

   **Debian Installation:**
   ```bash
   cd ~
   sudo dpkg -i ./Downloads/azuredatastudio-linux-<version string>.deb

   azuredatastudio
   ```

   **rpm Installation:**
   ```bash
   cd ~
   yum install ./Downloads/azuredatastudio-linux-<version string>.rpm

   azuredatastudio
   ```

   **tar.gz Installation:**
   ```bash 
   cd ~ 
   cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~ 
   tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz 
   echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
   source ~/.bashrc 
   azuredatastudio 
   ``` 

   > [!NOTE]
   > On Debian, Redhat, and Ubuntu, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:
   

   **Debian:** 
   ```bash
   sudo apt-get install libunwind8
   ```

   **Redhat:** 
   ```bash
   yum install libXScrnSaver
   ```

   **Ubuntu:** 
   ```bash
   sudo apt-get install libxss1

   sudo apt-get install libgconf-2-4

   sudo apt-get install libunwind8
   ```
## Download Insiders build of Azure Data Studio
In general, users should download the stable release of Azure Data Studio above. However, if you want to try out our beta features and give us feedback, you can download an [Insiders build of Azure Data Studio.](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-master)

## Uninstall Azure Data Studio

If you installed [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] with a .zip or other archive, then simply delete the files.

## Supported Operating Systems

[!INCLUDE[name-sos](../includes/name-sos-short.md)] runs on Windows, macOS, and Linux, and is supported on the following platforms:

### Windows
- Windows 10 (64-bit)
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1) (64-bit) - Requires [KB2533623](https://www.microsoft.com/download/details.aspx?id=26767)
- Windows Server 2019
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

## Recommended System Requirements

|             | CPU Cores | Memory/RAM |
|:-----------|:---------|:----------|
| Recommended |     4     |      8 GB    |
|   Minimum   |     2     |      4 GB     |
|             |           |            |


## Next Steps

See one of the following quickstarts to get started:
- [Deploy Big Data Cluster](quickstart-big-data-cluster-deploy.md)
- [Connect to Big Data Cluster](connect-to-big-data-cluster.md)

[!INCLUDE[get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839) and [usage data collection](usage-data-collection.md).
