---
title: Download and install sqlpackage
description: 'Download and Install sqlpackage for Windows, macOS, or Linux'
ms.custom: "tools|sos"
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
author: "pensivebrian"
ms.author: "broneill"
ms.reviewer: "alayu; sstein"
ms.date: 06/20/2018
---

# Download and install sqlpackage

sqlpackage runs on Windows, macOS, and Linux.

Download and install the latest .NET Framework release and macOS and Linux previews:

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2134206)|June 24, 2020|18.5.1|15.0.4826.1|
|macOS .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2134312)|June 24, 2020| 18.5.1|15.0.4826.1|
|Linux .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2134311)|June 24, 2020| 18.5.1|15.0.4826.1|
|Windows .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2134310)|June 24, 2020| 18.5.1|15.0.4826.1|

For details about the latest release, see the [release notes](release-notes-sqlpackage.md). To download additional languages, see the [Available Languages](#available-languages) section.

[!INCLUDE[Freshness](../includes/paragraph-content/fresh-note-steps-feedback.md)]

## Get sqlpackage for Windows

This release of sqlpackage includes a standard Windows installer experience, and a .zip: 

1. Download and run the [DacFramework.msi installer for Windows](https://go.microsoft.com/fwlink/?linkid=2134206).
2. Open a new Command Prompt window, and run sqlpackage.exe
    - sqlpackage is installed to the ```C:\Program Files\Microsoft SQL Server\150\DAC\bin``` folder

## Get sqlpackage .NET Core for Windows

1. Download [sqlpackage for Windows](https://go.microsoft.com/fwlink/?linkid=2134310).
2. To extract the file by right clicking on the file in Windows Explorer, and selecting 'Extract All...', and select the target directory.
3. Open a new Terminal window and cd to the location where sqlpackage was extracted:

   ```cmd
   > sqlpackage
   ```

## Get sqlpackage .NET Core for macOS

1. Download [sqlpackage for macOS](https://go.microsoft.com/fwlink/?linkid=2134312).
2. To extract the file and launch sqlpackage, open a new Terminal window and type the following commands:

   ```bash
   $ mkdir sqlpackage
   $ unzip ~/Downloads/sqlpackage-osx-<version string>.zip -d ~/sqlpackage 
   $ echo 'export PATH="$PATH:~/sqlpackage"' >> ~/.bash_profile
   $ source ~/.bash_profile
   $ sqlpackage
   ```

## Get sqlpackage .NET Core for Linux

1. Download [sqlpackage for Linux](https://go.microsoft.com/fwlink/?linkid=2134311) by using one of the installers or the tar.gz archive:
2. To extract the file and launch sqlpackage, open a new Terminal window and type the following commands:

   ```bash
   $ cd ~
   $ mkdir sqlpackage
   $ unzip ~/Downloads/sqlpackage-linux-<version string>.zip -d ~/sqlpackage 
   $ echo "export PATH=\"\$PATH:$HOME/sqlpackage\"" >> ~/.bashrc
   $ chmod a+x ~/sqlpackage/sqlpackage
   $ source ~/.bashrc
   $ sqlpackage
   ```

   > [!NOTE]
   > On Debian, Redhat, and Ubuntu, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:

   **Debian:**

   ```bash
   $ sudo apt-get install libunwind8
   ```

   **Redhat:**

   ```bash
   $ yum install libunwind
   $ yum install libicu
   ```

   **Ubuntu:**

   ```bash
   $ sudo apt-get install libunwind8

   # install the libicu library based on the Ubuntu version
   $ sudo apt-get install libicu52      # for 14.x
   $ sudo apt-get install libicu55      # for 16.x
   $ sudo apt-get install libicu57      # for 17.x
   $ sudo apt-get install libicu60      # for 18.x
   ```

## Uninstall sqlpackage (preview)

If you installed sqlpackage using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed sqlpackage with a .zip or other archive, then delete the files.

## Supported Operating Systems

sqlpackage runs on Windows, macOS, and Linux, and is supported on the following platforms:

### Windows

- Windows 10
- Windows 8.1
- Windows 7 SP1
- Windows Server Core
- Windows Server 2008 R2
- Windows Server 2012 R2
- Windows Server 2016
- Windows Server 2019

### macOS

- macOS 10.13 High Sierra
- macOS 10.12 Sierra

### Linux (x64)

- Red Hat Enterprise Linux 7.4
- Red Hat Enterprise Linux 7.3
- SUSE Linux Enterprise Server v12 SP2
- Ubuntu 16.04

## Available Languages

This release of sqlpackage can be installed in the following languages:

sqlpackage Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2134206&clcid=0x40a)

sqlpackage .NET Core Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2134310&clcid=0x40a)

sqlpackage .NET Core macOS:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2134312&clcid=0x40a)

sqlpackage .NET Core Linux:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2134311&clcid=0x40a)

## Next Steps

- Learn more about [sqlpackage](sqlpackage.md)

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
