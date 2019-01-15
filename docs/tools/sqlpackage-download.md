---
title: Download and install sqlpackage | Microsoft Docs
description: 'Download and Install sqlpackage for Windows, macOS, or Linux'
ms.custom: "tools|sos"
ms.date: "06/18/2018"
ms.prod: sql
ms.reviewer: "alayu; sstein"
ms.prod_service: sql-tools
ms.topic: conceptual
author: "pensivebrian"
ms.author: "broneill"
manager: craigg
---
# Download and install sqlpackage

sqlpackage runs on Windows, macOS, and Linux.

Download and install the latest .NET Framework release and macOS and Linux previews:

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2033947)|October 24, 2018|18.0|15.0.4200.1|
|macOS .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2044514)|November 15, 2018 | - |15.0.4240.1|
|Linux .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2044263)|November 15, 2018 | - |15.0.4240.1|

For details about the latest release, see the [release notes](sqlpackage-release-notes.md).

## Get sqlpackage for Windows

This release of sqlpackage includes a standard Windows installer experience, and a .zip: 

1. Download and run the [DacFramework.msi installer for Windows](https://go.microsoft.com/fwlink/?linkid=2033947).
2. Open a new Command Prompt window, and run sqlpackage.exe
    - sqlpackage is installed to the ```C:\Program Files\Microsoft SQL Server\150\DAC\bin``` folder
    - Installing the x86 version on a x64 machine, sqlpackage is installed to the ```C:\Program Files (x86)\Microsoft SQL Server\150\DAC\bin``` folder

## Get sqlpackage (preview) for macOS

1. Download [sqlpackage for macOS](https://go.microsoft.com/fwlink/?linkid=2044514).
2. To extract the file and launch sqlpackage, open a new Terminal window and type the following commands:

   **.zip Installation:**

   ```bash
   mkdir sqlpackage
   unzip ~/Downloads/sqlpackage-osx-<version string>.zip ~/sqlpackage 
   echo 'export PATH="$PATH:~/sqlpackage"' >> ~/.bash_profile
   source ~/.bash_profile
   sqlpackage
   ```

## Get sqlpackage (preview) for Linux

1. Download [sqlpackage for Linux](https://go.microsoft.com/fwlink/?linkid=2044263) by using one of the installers or the tar.gz archive:
2. To extract the file and launch sqlpackage, open a new Terminal window and type the following commands:

   **.zip Installation:**

   ```bash
   cd ~
   mkdir sqlpackage
   unzip ~/Downloads/sqlpackage-linux-<version string>.zip -d ~/sqlpackage 
   echo "export PATH=\"\$PATH:$HOME/sqlpackage\"" >> ~/.bashrc
   chmod a+x ~/sqlpackage/sqlpackage
   source ~/.bashrc
   sqlpackage
   ```

   > [!NOTE]
   > On Debian, Redhat, and Ubuntu, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:

   **Debian:**

   ```bash
   sudo apt-get install libunwind8
   ```

   **Redhat:**

   ```bash
   yum install libunwind
   yum install libicu
   ```

   **Ubuntu:**

   ```bash
   sudo apt-get install libunwind8

   # install the libicu library based on the Ubuntu version
   sudo apt-get install libicu52      # for 14.x
   sudo apt-get install libicu55      # for 16.x
   sudo apt-get install libicu57      # for 17.x
   sudo apt-get install libicu60      # for 18.x
   ```

## Uninstall sqlpackage (preview)

If you installed sqlpackage using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed sqlpackage with a .zip or other archive, then simply delete the files.

## Supported Operating Systems

sqlpackage runs on Windows, macOS, and Linux, and is supported on the following platforms:

### Windows

- Windows 10
- Windows 8.1
- Windows 8
- Windows 7 SP1
- Windows Server 2016
- Windows Server 2012 R2
- Windows Server 2012
- Windows Server 2008 R2

### macOS

- macOS 10.13 High Sierra
- macOS 10.12 Sierra

### Linux (x64)

- Red Hat Enterprise Linux 7.4
- Red Hat Enterprise Linux 7.3
- SUSE Linux Enterprise Server v12 SP2
- Ubuntu 16.04

## Next Steps

- Learn more about [sqlpackage](sqlpackage.md)

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
