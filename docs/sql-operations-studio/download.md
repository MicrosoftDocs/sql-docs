---
title: Download and install Microsoft SQL Operations Studio | Microsoft Docs
description: 'Download and Install Microsoft SQL Operations Studio for Windows, macOS, or Linux'
keywords: Microsoft SQL Operations Studio, install Microsoft SQL Operations Studio, download Microsoft SQL Operations Studio
ms.custom: "tools|sos"
ms.date: "10/31/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sanagama; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Download and install Microsoft SQL Operations Studio

[!INCLUDE[name-sos-short](../includes/name-sos-short.md)] is a free, lightweight, modern database development and management tool that runs on Windows, macOS, and Linux. Use [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] to manage SQL Server running anywhere, Azure SQL Database, and Azure SQL Data Warehouse.   

> [!NOTE]
> This preview version of [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] installs by copying a zip (tar) file to your local computer, extracting (expanding) the file, and running the executable.

## Supported Operating Systems

Carbon runs on Windows, macOS, and Linux.

Carbon is supported on the following platforms:

| Platform | Supported versions |
|:---|:---|
| Windows (64-bit only) | Windows 10 (recommended), Windows Server 2016, Windows Server 2012 R2 (64-bit) |
| Mac | macOS Sierra (10.12), macOS High Sierra (10.13) |
| Linux | Ubuntu 16.04 LTS, Red Hat Enterprise Linux 7.3 (RHEL)|

??NEED TO FINALIZE THE ABOVE LIST- I grabbed this info from the mail thread about supported OSs

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

   > [!IMPORTANT]
   > Executable files may be blocked by Windows when zip files are downloaded from an external source and extracted. Follow the steps below to unblock the .zip file before extracting.

1. Right-click the **.zip** file, and select **Properties**.
1. On the **General** tab, select **Unblock**, and click **Apply**.
1. Browse to the downloaded file and extract it.
2. Run *\Carbon\Carbon-windows\Carbon.exe* [VERIFY PATH]


## Get Carbon for macOS

1. Download [Carbon for macOS](https://github.com/Microsoft/carbon/releases/download/v0.20.0/2017-Sep-27-carbon-macos.zip) to your local computer.
2. To expand the contents of the zip, double-click it.
3. Drag *Carbon.app* to the *Applications* folder, making it available in the *Launchpad*.

### Start Carbon from the terminal:
1. Add Carbon to the PATH by running the command:  
      ```ln -s /user/local/bin/carbon /Applications/carbon.app/Contents/Resources/app/bin/carbon```
1. Start Carbon by running the following command:  
      ```carbon .```


## Get Carbon for Linux

1. Open a new Terminal window.
2. Type the following commands to extract the file and launch Carbon:

```bash
cd ~
cp ~/Downloads/pgi3-carbon-linux-x64.tar.gz ~
tar -xvf ~/pgi3-carbon-linux-x64.tar.gz
echo 'export PATH="$PATH:~/carbon-linux-x64"' >> ~/.bashrc
source .bashrc
carbon .
```


## Uninstall Carbon

To uninstall the [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] preview, delete the files.

## Next Steps

See the following articles to get started:
- [Explore Carbon](tutorial-modern-code-flow-sql-server.md)
- [Connect & Query SQL Server](get-started-sql-server.md)
