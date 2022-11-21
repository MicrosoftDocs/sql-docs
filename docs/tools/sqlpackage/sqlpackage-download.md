---
title: Download and install SqlPackage
description: 'Download and Install SqlPackage for Windows, macOS, or Linux'
ms.custom:
  - tools|sos
  - intro-installation
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 11/9/2022
---

# Download and install SqlPackage

SqlPackage runs on Windows, macOS, and Linux, and is available to install through `dotnet tool` or as a standalone zip download.

- **Version number:** 161.6374.0
- **Build number:** 16.1.6374.0
- **Release date:** November 9, 2022

For details about the latest release, see the [release notes](release-notes-sqlpackage.md).

> [!NOTE]
> SqlPackage version numbering has been adjusted to better reflect the DacFx build number it is associated with. Previously, SqlPackage had a distinct version number (19) and build number (160.x). Beginning with version 161, the version number of SqlPackage will match the DacFx version number it is associated with (eg 161.6374.0).

## Installation, cross-platform

Installing SqlPackage as a [dotnet tool](/dotnet/core/tools/global-tools) requires the [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet/6.0) v6.0 or later to be installed on your machine.  Installing SqlPackage as a global tool will make it available on your path as `sqlpackage` and is the recommended way to install SqlPackage for Windows, macOS, and Linux.

To install SqlPackage as a global .NET tool, run the following command:

   ```bash
   dotnet tool install -g microsoft.sqlpackage
   ```

More information on the options available with the `dotnet tool install` command can be found in the [dotnet tool install documentation](/dotnet/core/tools/dotnet-tool-install).


To update SqlPackage to the latest version, run the following command:

   ```bash
   dotnet tool update -g microsoft.sqlpackage
   ```


To uninstall SqlPackage, run the following command:

   ```bash
   dotnet tool uninstall -g microsoft.sqlpackage
   ```



## Installation, zip download

|Platform|Download|
|:---|:---|
|[Windows .NET 6](#windows-net-6) |[.zip file](https://go.microsoft.com/fwlink/?linkid=2215400)|
|[Windows](#windows-net-framework)|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2215326)|
|[macOS .NET 6](#macos) |[.zip file](https://go.microsoft.com/fwlink/?linkid=2215401)|
|[Linux .NET 6](#linux) |[.zip file](https://go.microsoft.com/fwlink/?linkid=2215501)|


### Linux

1. Download [SqlPackage for Linux](https://aka.ms/sqlpackage-linux).
2. To extract the file and launch SqlPackage, open a new Terminal window and type the following commands:

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
   sudo apt-get install libicu55      # for 16.x
   sudo apt-get install libicu57      # for 17.x
   sudo apt-get install libicu60      # for 18.x
   sudo apt-get install libicu66      # for 20.x
   ```

### macOS

1. Download [SqlPackage for macOS](https://aka.ms/sqlpackage-macos).
2. To extract the file and launch SqlPackage, open a new Terminal window and type the following commands:

   ```bash
   mkdir sqlpackage
   unzip ~/Downloads/sqlpackage-osx-<version string>.zip -d ~/sqlpackage
   chmod +x ~/sqlpackage/sqlpackage
   echo 'export PATH="$PATH:~/sqlpackage"' >> ~/.bash_profile
   source ~/.bash_profile
   sqlpackage
   ```

   > [!NOTE]
   > Security settings may require modification to run SqlPackage on macOS. Use the following commands to interact with Gatekeeper from the command line.

   **Before executing SqlPackage:**
   ```bash
   sudo spctl --master-disable
   ```

   **After executing SqlPackage:**
   ```bash
   sudo spctl --master-enable
   ```

### Windows (.NET 6)

1. Download [SqlPackage for Windows](https://aka.ms/sqlpackage-windows).
2. To extract the file by right clicking on the file in Windows Explorer, and selecting 'Extract All...', and select the target directory.
3. Open a new Terminal window and cd to the location where SqlPackage was extracted:

   ```cmd
   > sqlpackage
   ```

### Windows (.NET Framework)

This release of SqlPackage includes a standard Windows installer experience, and a .zip: 

1. Download and run the [DacFramework.msi installer for Windows](https://aka.ms/dacfx-msi).
2. Open a new Command Prompt window, and run SqlPackage.exe
    - SqlPackage is installed to the ```C:\Program Files\Microsoft SQL Server\160\DAC\bin``` folder

### Uninstall SqlPackage

If you installed SqlPackage using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed SqlPackage with a .zip or other archive, then delete the files.

### Automated environments

Evergreen links are available for downloading the latest Sqlpackage versions:
- Linux ([https://aka.ms/sqlpackage-linux](https://aka.ms/sqlpackage-linux))
- macOS ([https://aka.ms/sqlpackage-macos](https://aka.ms/sqlpackage-macos))
- Windows ([https://aka.ms/sqlpackage-windows](https://aka.ms/sqlpackage-windows))
- Windows, .NET Framework ([https://aka.ms/dacfx-msi](https://aka.ms/dacfx-msi))

## DacFx

SqlPackage is a command-line interface for the DacFx framework, exposing some of the public DacFx APIs. DacServices ([Microsoft.SqlServer.Dac](/dotnet/api/microsoft.sqlserver.dac.dacservices)) is a related mechanism for integrating database deployment into your application pipeline.  The DacServices API is available in a package through NuGet, [Microsoft.SqlServer.DacFx](https://www.NuGet.org/packages/Microsoft.SqlServer.DacFx).  The current DacFx version is 161.6374.0.

Adding the NuGet package to a .NET project is accomplished via the .NET CLI with this command:

```cmd
dotnet add package Microsoft.SqlServer.DacFx
```

> [!NOTE]
> Additional NuGet packages were published under the DacFx name, "Microsoft.SqlServer.DacFx.x64" and "Microsoft.SqlServer.DacFx.x86". Support for both platforms is covered under the "Microsoft.SqlServer.DacFx" package. New references should be made to this package, not the x64 or x86 variants.


## Supported Operating Systems

SqlPackage runs on Windows, macOS, and Linux and is built using .NET 6.  The [.NET 6 OS requirements](https://github.com/dotnet/core/blob/main/release-notes/6.0/supported-os.md) are minimum requirements for SqlPackage, which has additional requirements due to its dependencies.

### Windows (x64)

- Windows 11
- Windows 10 (1607+)
- Windows 8.1
- Windows 7 SP1
- Windows Server Core
- Windows Server 2012 R2
- Windows Server 2016
- Windows Server 2019
- Windows Server 2022

### macOS

- macOS 13 "Ventura"
- macOS 12 "Monterey"
- macOS 11 "Big Sur"
- macOS 10.15 "Catalina"

### Linux (x64)

- Debian 10, 11
- Red Hat Enterprise Linux 7+
- SUSE Linux Enterprise Server v12 SP2+
- Ubuntu 16.04, 18.04, 20.04, 22.04

## Available Languages

This release of SqlPackage can be installed in the following languages:

SqlPackage .NET 6 Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2215400&clcid=0x40a)

SqlPackage .NET Framework Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2215326&clcid=0x40a)

SqlPackage .NET 6 macOS:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2215401&clcid=0x40a)

SqlPackage .NET 6 Linux:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2215501&clcid=0x40a)


## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
