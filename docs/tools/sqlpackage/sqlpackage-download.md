---
title: Download and install SqlPackage
description: "Download and Install SqlPackage for Windows, macOS, or Linux"
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 06/06/2024
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.custom: tools|sos, intro-installation, linux-related-content
---

# Download and install SqlPackage

SqlPackage runs on Windows, macOS, and Linux, and is available to install through `dotnet tool` or as a standalone zip download. For details about the latest release, see the [release notes](release-notes-sqlpackage.md).

- **Version number:** 162.3.563
- **Build number:** 162.3.563.1
- **Release date:** June 6, 2024

SqlPackage is developed and released for both .NET 8 and .NET Framework. Installing the .NET 8 SqlPackage version is recommended via the [convenient `dotnet tool` method](#installation-cross-platform), which is cross-platform and easy to update, or via the [portable self-contained .zip download](#installation-file-download-alternative). The .NET 8 SqlPackage releases benefit from the continual advances to the performance and scalability of .NET as part of the [focus on for modern applications](/dotnet/core/introduction#net-ecosystem), which contrasts to the maintenance support of .NET Framework for Windows. The .NET Framework version is only available as a [.msi Windows installer](#windows-net-framework).

> [!NOTE]
> Previously, SqlPackage had a distinct version number (19) and build number (160.x). Beginning with version 161, the version number of SqlPackage matches the DacFx version number it is associated with (eg 162.0.52).

## Installation, cross-platform

Installing SqlPackage as a [dotnet tool](/dotnet/core/tools/global-tools) requires the [.NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0) to be installed on your machine. Installing SqlPackage as a global tool makes it available on your path as `sqlpackage` and is the recommended method to install SqlPackage for Windows, macOS, and Linux. SqlPackage is available as a dotnet tool for .NET 6 and .NET 8.

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

### Preview releases

Preview releases of SqlPackage are available with the dotnet tool feed. To access preview releases, use the `--prerelease` option with the `dotnet tool` command. For example, to install the latest preview release, run the following command:

   ```bash
   dotnet tool install -g --prerelease microsoft.sqlpackage
   ```

To update SqlPackage to the latest preview version, run the following command:

   ```bash
   dotnet tool update -g --prerelease microsoft.sqlpackage
   ```

A list of preview releases is available on the [dotnet tool feed for SqlPackage](https://www.nuget.org/packages/microsoft.sqlpackage/).


## Installation, file download (alternative)

SqlPackage is also prepared as a self-contained download for Windows, macOS, and Linux. No .NET install is required, however, the dependencies included in this .zip download are updated more frequently in the [dotnet tool option for SqlPackage](#installation-cross-platform). The following links are for the latest version of SqlPackage:

|Platform|Download|
|:---|:---|
|Windows .NET 8 |[.zip file](https://go.microsoft.com/fwlink/?linkid=2273950)|
|Windows|[.msi file](https://go.microsoft.com/fwlink/?linkid=2274058)|
|macOS .NET 8 |[.zip file](https://go.microsoft.com/fwlink/?linkid=2274060)|
|Linux .NET 8 |[.zip file](https://go.microsoft.com/fwlink/?linkid=2274059)|



### Linux

1. Download [SqlPackage for Linux](https://aka.ms/sqlpackage-linux)
2. Extract the file and launch SqlPackage, open a new Terminal window and type the following commands:

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
   > You may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:

   **Debian:**

   ```bash
   sudo apt-get install libunwind8
   ```

   **Red Hat:**

   ```bash
   yum install libunwind
   yum install libicu
   ```

   **Ubuntu:**

   ```bash
   sudo apt-get install libunwind8
   ```

### macOS

1. Download [SqlPackage for macOS](https://aka.ms/sqlpackage-macos)
2. Extract the file and launch SqlPackage, open a new Terminal window and type the following commands:

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

### Windows (.NET 8)

1. Download [SqlPackage for Windows](https://aka.ms/sqlpackage-windows)
2. Extract the file by right-clicking on the file in Windows Explorer, and selecting 'Extract All...', and select the target directory
3. Open a new Terminal window and cd to the location where SqlPackage was extracted:

   ```cmd
   > sqlpackage
   ```

### Windows (.NET Framework)

This release of SqlPackage includes a standard Windows installer experience, and a .zip: 

1. Download and run the [DacFramework.msi installer for Windows](https://aka.ms/dacfx-msi)
2. Open a new Command Prompt window, and run SqlPackage
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

SqlPackage is a command-line interface for the DacFx framework, exposing some of the public DacFx APIs. DacServices ([Microsoft.SqlServer.Dac](/dotnet/api/microsoft.sqlserver.dac.dacservices)) is a related mechanism for integrating database deployment into your application pipeline. The DacServices API is available in a package through NuGet, [Microsoft.SqlServer.DacFx](https://www.NuGet.org/packages/Microsoft.SqlServer.DacFx).

Adding the NuGet package to a .NET project is accomplished via the .NET CLI with this command:

```cmd
dotnet add package Microsoft.SqlServer.DacFx
```

> [!NOTE]
> Additional NuGet packages were published under the DacFx name, "Microsoft.SqlServer.DacFx.x64" and "Microsoft.SqlServer.DacFx.x86". Support for both platforms is covered under the "Microsoft.SqlServer.DacFx" package. New references should be made to this package, not the x64 or x86 variants.


## Supported Operating Systems

SqlPackage runs on Windows, macOS, and Linux and is built using .NET 8. The [.NET 8 OS requirements](https://github.com/dotnet/core/blob/main/release-notes/8.0/supported-os.md) are minimum requirements for SqlPackage, which has extra requirements due to its dependencies.

### Windows (x64)

- Windows 11
- Windows 10 (1607+)
- Windows Server Core 2012 R2+
- Windows Server 2012 R2+

### macOS

- macOS 12 "Monterey"+

### Linux (x64)

- Debian 11+
- Red Hat Enterprise Linux 8+
- SUSE Linux Enterprise Server v12 SP2+
- Ubuntu 20.04+

## Available Languages

This release of SqlPackage can be installed in the following languages:

SqlPackage .NET 8 Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2273950&clcid=0x40a)

SqlPackage .NET Framework Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2274058&clcid=0x40a)

SqlPackage .NET 8 macOS:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2274060&clcid=0x40a)

SqlPackage .NET 8 Linux:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2274059&clcid=0x40a)


## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)
- Learn more about [SqlPackage in CI/CD pipelines](sqlpackage-pipelines.md)
- Learn more about [troubleshooting issues with SqlPackage](troubleshooting-issues-and-performance-with-sqlpackage.md)
- Share feedback on SqlPackage in the [DacFx GitHub repository](https://github.com/microsoft/DacFx)

[Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
