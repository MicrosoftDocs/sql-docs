---
title: Download and install sqlpackage
description: 'Download and Install sqlpackage for Windows, macOS, or Linux'
ms.custom: "tools|sos"
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan; sstein"
ms.date: 06/02/2021
---

# Download and install sqlpackage

sqlpackage runs on Windows, macOS, and Linux.

Download and install the latest .NET Framework release and macOS and Linux previews:

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|[Windows](#get-sqlpackage-for-windows)|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2165211)|June 2, 2021| 18.7.1 | 15.0.5164.1 |
|[macOS .NET Core](#get-sqlpackage-net-core-for-macos) |[.zip file](https://go.microsoft.com/fwlink/?linkid=2165132)|June 2, 2021| 18.7.1| 15.0.5164.1 |
|[Linux .NET Core](#get-sqlpackage-net-core-for-linux) |[.zip file](https://go.microsoft.com/fwlink/?linkid=2165213)|June 2, 2021| 18.7.1| 15.0.5164.1 |
|[Windows .NET Core](#get-sqlpackage-net-core-for-windows) |[.zip file](https://go.microsoft.com/fwlink/?linkid=2165212)|June 2, 2021| 18.7.1| 15.0.5164.1 |

For details about the latest release, see the [release notes](release-notes-sqlpackage.md). To download additional languages, see the [Available Languages](#available-languages) section.


An evergreen link ([https://aka.ms/sqlpackage-linux](https://aka.ms/sqlpackage-linux)) is available that points to the current version of sqlpackage for [Linux .NET Core](#get-sqlpackage-net-core-for-linux), which can be used in automating environments with the latest sqlpackage.

## DacFx
SqlPackage is a command-line interface for the DacFx framework, exposing some of the public DacFx APIs. DacServices ([Microsoft.SqlServer.Dac](/dotnet/api/microsoft.sqlserver.dac.dacservices)) is a related mechanism for integrating database deployment into your application pipeline.  The DacServices API is available in a package through NuGet, [Microsoft.SqlServer.DACFx](https://www.NuGet.org/packages/Microsoft.SqlServer.DACFx).  The current DacFx version is 150.5164.1.

Installing the NuGet package via the .NET CLI is accomplished with this command:

```cmd
dotnet add package Microsoft.SqlServer.DACFx
```

>[!NOTE]
> Additional NuGet packages were published under the DacFx name, "Microsoft.SqlServer.DacFx.x64" and "Microsoft.SqlServer.DacFx.x86". Support for both platforms is covered under the "Microsoft.SqlServer.DACFx" package. New references should be made to this package, not the x64 or x86 variants.

## Get sqlpackage for Windows

This release of sqlpackage includes a standard Windows installer experience, and a .zip: 

1. Download and run the [DacFramework.msi installer for Windows](https://go.microsoft.com/fwlink/?linkid=2157201).
2. Open a new Command Prompt window, and run sqlpackage.exe
    - sqlpackage is installed to the ```C:\Program Files\Microsoft SQL Server\150\DAC\bin``` folder

## Get sqlpackage .NET Core for Windows

1. Download [sqlpackage for Windows](https://go.microsoft.com/fwlink/?linkid=2157302).
2. To extract the file by right clicking on the file in Windows Explorer, and selecting 'Extract All...', and select the target directory.
3. Open a new Terminal window and cd to the location where sqlpackage was extracted:

   ```cmd
   > sqlpackage
   ```

## Get sqlpackage .NET Core for macOS

1. Download [sqlpackage for macOS](https://go.microsoft.com/fwlink/?linkid=2157203).
2. To extract the file and launch sqlpackage, open a new Terminal window and type the following commands:

   ```bash
   $ mkdir sqlpackage
   $ unzip ~/Downloads/sqlpackage-osx-<version string>.zip -d ~/sqlpackage 
   $ echo 'export PATH="$PATH:~/sqlpackage"' >> ~/.bash_profile
   $ source ~/.bash_profile
   $ sqlpackage
   ```

   > [!NOTE]
   > Security settings may require modification to run sqlpackage on macOS. Use the following commands to interact with Gatekeeper from the command line.

   **Before executing sqlpackage:**
   ```bash
   $ sudo spctl --master-disable
   ```

   **After executing sqlpackage:**
   ```bash
   $ sudo spctl --master-enable
   ```

## Get sqlpackage .NET Core for Linux

1. Download [sqlpackage for Linux](https://go.microsoft.com/fwlink/?linkid=2157202) by using one of the installers or the tar.gz archive.
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
   $ sudo apt-get install libicu66      # for 20.x
   ```

## Uninstall sqlpackage

If you installed sqlpackage using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed sqlpackage with a .zip or other archive, then delete the files.

## Supported Operating Systems

sqlpackage runs on Windows, macOS, and Linux and is built using .NET Core 3.1.  The [.NET Core 3.1 OS requirements](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md) apply to sqlpackage.

### Windows (x64)

- Windows 10 (1607+)
- Windows 8.1
- Windows 7 SP1
- Windows Server Core
- Windows Server 2012 R2
- Windows Server 2016
- Windows Server 2019

### macOS

- macOS 10.15 "Catalina"
- macOS 10.14 "Mojave"
- macOS 10.13 "High Sierra"

### Linux (x64)

- Red Hat Enterprise Linux 7+
- SUSE Linux Enterprise Server v12 SP2+
- Ubuntu 16.04, 18.04, 20.04

## Available Languages

This release of sqlpackage can be installed in the following languages:

sqlpackage Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2157201&clcid=0x40a)

sqlpackage .NET Core Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2157302&clcid=0x40a)

sqlpackage .NET Core macOS:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2157203&clcid=0x40a)

sqlpackage .NET Core Linux:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2157202&clcid=0x40a)


## Next Steps

- Learn more about [sqlpackage](sqlpackage.md)

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
