---
title: Download and install Microsoft SQL Operations Studio | Microsoft Docs
description: 'Download and Install Microsoft SQL Operations Studio for Windows, macOS, or Linux'
keywords: Microsoft SQL Operations Studio, install Microsoft SQL Operations Studio, download Microsoft SQL Operations Studio
ms.custom: "tools|sos"
ms.date: "11/01/2017"
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

[!INCLUDE[name-sos](../includes/name-sos-short.md)] runs on Windows, macOS, and Linux.

[!INCLUDE[name-sos](../includes/name-sos-short.md)] is supported on the following platforms:

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



## Get SQL Operations Studio for Windows

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for Windows](https://github.com/Microsoft/carbon/releases/download/v0.23.3/2017-Nov-03-sqlops-windows.zip) to your local computer.

   > [!IMPORTANT]
   > Executable files may be blocked by Windows when zip files are downloaded from an external source and extracted. The following steps unblock the .zip file before extracting. 

1. Right-click the **.zip** file, and select **Properties**.
1. On the **General** tab, select **Unblock**, and click **Apply**.
1. Browse to the downloaded file and extract it.
2. Run *\sqlops-windows\sqlops.exe*

If [!INCLUDE[name-sos](../includes/name-sos-short.md)] is blocked when you run it, click **More info** > **Run Anyway**.

## Get SQL Operations Studio for macOS

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for macOS](https://github.com/Microsoft/carbon/releases/download/v0.23.3/2017-Nov-03-sqlops-macos-2.zip) to your local computer.
2. To expand the contents of the zip, double-click it.
3. Drag *sqlops.app* to the *Applications* folder, making it available in the *Launchpad*.

### Start [!INCLUDE[name-sos](../includes/name-sos-short.md)] from the terminal:
1. Add [!INCLUDE[name-sos](../includes/name-sos-short.md)] to the PATH by running the command:  
      ```ln -s /user/local/bin/sqlops/Applications/sqlops.app/Contents/Resources/app/bin/sqlops```
1. Start [!INCLUDE[name-sos](../includes/name-sos-short.md)] by running the following command:  
      ```sqlops```


## Get SQL Operations Studio for Linux

1. Open a new Terminal window.
2. Type the following commands to extract the file and launch [!INCLUDE[name-sos](../includes/name-sos-short.md)]:

```bash
cd ~
cp ~/Downloads/pgi3-sqlops-linux-x64.tar.gz ~
tar -xvf ~/pgi3-sqlops-linux-x64.tar.gz
echo 'export PATH="$PATH:~/sqlops-linux-x64"' >> ~/.bashrc
source .bashrc
sqlops .
```


## Uninstall SQL Operations Studio

To uninstall [!INCLUDE[name-sos-short](../includes/name-sos-short.md)], delete the files.

## Next Steps

See one of the following articles to get started:
- [Connect & Query SQL Server](get-started-sql-server.md)
- [Connect & Query Azure SQL Database](get-started-sql-database.md)
- [Connect & Query Azure Data Warehouse](get-started-sql-dw.md)
