---
title: Download and install Carbon | Microsoft Docs
description: 'Get Carbon for Linux, macOS, or Windows'
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
ms.date: 10/02/2017
ms.author: sstein
---
# Download and install Carbon

[Carbon](index.md) is a lightweight, open source, multi-OS and multi-database tool, designed from the ground-up for DBAs and developers. Carbon simplifies configuration, management, monitoring, and troubleshooting of databases. 

The preview version of Carbon installs by simply copying a zip or tar file to your local computer, extracting the file, and running the executable.

## Supported Operating Systems

Carbon runs on Linux, Mac, and Windows.

| Platform | Supported versions |
|:---|:---|
| Windows (64-bit only) | Windows 10 (recommended), Windows 8.1, Windows 8, Windows 7 (SP1), Windows Server 2016, Windows Server 2012 R2 (64-bit), Windows Server 2012 (64-bit), Windows Server 2008 R2 (64-bit) |
| Mac | macOS Sierra (10.9+) |
| Linux | Linux Mint 17, Linux Mint 18, Elementary OS 0.3 Ubuntu 16.04+, Elementary OS 0.4 Debian 8.2 CentOS 7.1, Oracle Linux 7 Red Hat Enterprise Linux (RHEL) Fedora 23 openSUSE 13.2 SLES |


## Get Carbon for Windows

1. Download [Carbon for Windows](https://github.com/Microsoft/carbon/releases/download/v0.20.0/2017-Sep-27-carbon-windows.zip) to your local computer.
2. Browse to the downloaded file and extract/unzip it.
3. Run *\Carbon\Carbon-windows\Carbon.exe* [VERIFY PATH]

### Uninstalling Carbon on Windows

To uninstall the Carbon preview, simply delete the files.


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

### Uninstalling Carbon on Linux

To uninstall the Carbon preview, simply delete the files.


## Get Carbon for macOS

1. Download [Carbon for macOS](https://github.com/Microsoft/carbon/releases/download/v0.20.0/2017-Sep-27-carbon-macos.zip) to your local computer.
2. Expand the contents by double-clicking on the downloaded archive.
3. Drag *Carbon.app* to the *Applications* folder, making it available in the Launchpad.
4. Add *Carbon* to your Dock by right-clicking the icon and choosing *Options* > *Keep in Dock*.
5. Install openssl by running the following:

   ```$ brew install openssl $ ln -s /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib /usr/local/lib/ $ ln -s /usr/local/opt/openssl/lib/libssl.1.0.0.dylib /usr/local/lib/```

## Uninstalling Carbon

To uninstall any flavor of the Carbon preview, simply delete the files.

## Next Steps

See the following articles to get started:
- [Take a lap around the Carbon environment](overview.md)
- [Connect & Query SQL Server](get-started-sql-server.md)
