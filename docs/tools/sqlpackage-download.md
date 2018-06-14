---
title: Download and install sqlpackage | Microsoft Docs
description: 'Download and Install sqlpackage for Windows, macOS, or Linux'
ms.custom: "tools|sos"
ms.date: "06/04/2018"
ms.prod: sql
ms.reviewer: "alayu; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: "pensivebrian"
ms.author: "broneill"
manager: craigg
---
# Download and install sqlpackage

sqlpackage runs on Windows, macOS, and Linux.

Download and install the latest .NET Framework release and macOS and Linux previews:

|Platform|Download|Release date| Version |
|:---|:---|:---|:---|
|Windows|[Installer](https://go.microsoft.com/fwlink/?linkid=873386)|Jan 25, 2018 |17.4.1|
|macOS|[.zip](https://go.microsoft.com/fwlink/?linkid=873927)|May 9, 2018 |0.0.1|
|Linux|[.zip](https://go.microsoft.com/fwlink/?linkid=873926)|May 9, 2018 |0.0.1|

## Get sqlpackage for Windows

This release of sqlpackage includes a standard Windows installer experience, and a .zip: 

**Installer**

1. Download and run the [DacFramework.msi installer for Windows](https://go.microsoft.com/fwlink/?linkid=873386).
2. sqlpackage will be installed to the ```C:\Program Files\Microsoft SQL Server\140\DAC\bin``` folder.

## Get sqlpackage (preview) for macOS

1. Download [sqlpackage for macOS](https://go.microsoft.com/fwlink/?linkid=873927).
2. To expand the contents of the zip, double-click it.


## Get sqlpackage (preview) for Linux

1. Download sqlpackage for Linux by using one of the installers or the tar.gz archive:
    - [.tar.gz](https://go.microsoft.com/fwlink/?linkid=873389)
1. To extract the file and launch sqlpackage, open a new Terminal window and type the following commands:

   **tar.gz Installation:**
   ```bash 
   cd ~ 
   cp ~/Downloads/sqlops-linux-<version string>.tar.gz ~ 
   tar -xvf ~/sqlops-linux-<version string>.tar.gz 
   echo 'export PATH="$PATH:~/sqlops-linux-x64"' >> ~/.bashrc
   source ~/.bashrc 
   sqlops 
   ``` 

   > [!NOTE]
   > On Debian, Redhat, and Ubuntu, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:
   

   **Debian:** 
   ```bash
   sudo apt-get install libuwind8
   ```

   **Redhat:** 
   ```bash
   yum install libunwind 
   ```

   **Ubuntu:** 
   ```bash
   sudo apt-get install libunwind8
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

- Learn more about [sqlpackage](/sqlpackage.md)

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
