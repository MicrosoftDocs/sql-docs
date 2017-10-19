---
title: Download and install Carbon | Microsoft Docs
description: 'Download and Install Carbon for Linux, macOS, or Windows'
keywords: Carbon, install Carbon, download Carbon
services: 
documentationcenter: ''
author: stevestein
manager: craigg
editor: 

ms.assetid: 
ms.service:
ms.custom: 
ms.devlang: na
ms.topic: article
ms.tgt_pltfrm: na
ms.prod: NEEDED
ms.workload: data-management
ms.date: 10/19/2017
ms.author: sstein
---
# Download and install Carbon

[Carbon](index.md) is a lightweight, open source, multi-OS and multi-database tool, designed from the ground-up for DBAs and developers. Carbon simplifies configuration, management, monitoring, and troubleshooting of databases. 

> [!NOTE]
> This preview version of Carbon installs by copying a zip (tar) file to your local computer, extracting (expanding) the file, and running the executable.

## Supported Operating Systems

Carbon runs on Linux, Mac, and Windows.

Carbon is supported on the following platforms:

| Platform | Supported versions |
|:---|:---|
| Windows (64-bit only) | Windows 10 (recommended), Windows Server 2016, Windows Server 2012 R2 (64-bit) |
| Mac | macOS Sierra (10.12), macOS High Sierra (10.13) |
| Linux | Ubuntu 16.04 LTS, Red Hat Enterprise Linux 7.3 (RHEL)|

Other Potential Platforms
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1) (64-bit)
- Windows Server 2012 (64-bit)
- Windows Server 2008 R2 (64-bit)
- Ubuntu 17.04
- Ubuntu 17.10
- RHEL 7.2
- SLES 12
- CentOS, Debian, openSUSE



## Get Carbon for Windows

1. Download [Carbon for Windows](https://github.com/Microsoft/carbon/releases/download/v0.20.0/2017-Sep-27-carbon-windows.zip) to your local computer.
2. Browse to the downloaded file and extract it.
3. Run *\Carbon\Carbon-windows\Carbon.exe* [VERIFY PATH]


## Get Carbon for Linux

### Red Hat Enterprise

- yum install: `$ sudo yum install -y mssql-Carbon`
- Offline installation: `$ sudo yum localinstall mssql-server_versionnumber.x86_64.rpm`

### SUSE

- zypper install: `$ sudo zypper install mssql-Carbon`
- Offline installation: `$ sudo zypper install mssql-server_versionnumber.x86_64.rpm`

### Ubuntu

- apt-get install: `$ sudo apt-get update sudo apt-get install -y mssql-server`
- Offline installation: `$ sudo dpkg -i mssql-server_versionnumber_amd64.deb`


## Get Carbon for macOS

1. Download [Carbon for macOS](https://github.com/Microsoft/carbon/releases/download/v0.20.0/2017-Sep-27-carbon-macos.zip) to your local computer.
2. To expand the contents of the zip, double-click it.
3. Drag *Carbon.app* to the *Applications* folder, making it available in the *Launchpad*.

### Start Carbon from the terminal:
1. Add Carbon to the PATH by running the command:  
      ```ln -s /user/local/bin/carbon /Applications/carbon.app/Contents/Resources/app/bin/carbon```
1. Start Carbon by running the following command:  
      ```carbon .```


## Uninstalling Carbon

To uninstall any flavor of the Carbon preview, just delete the files.

## Next Steps

See the following articles to get started:
- [Explore Carbon](tutorial-modern-code-flow-sql-server.md)
- [Connect & Query SQL Server](get-started-sql-server.md)
