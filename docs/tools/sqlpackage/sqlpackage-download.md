---
title: Download and Install SqlPackage
description: Download and Install SqlPackage for Windows, macOS, or Linux
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 06/03/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: install-set-up-deploy
ms.collection:
  - data-tools
ms.custom:
  - tools|sos
  - intro-installation
  - linux-related-content
---

# Download and install SqlPackage

SqlPackage runs on Windows, macOS, and Linux, and is available to install through `dotnet tool` or as a standalone zip download. For details about the latest release, see the [release notes](release-notes-sqlpackage.md).

- **Version number:** 170.4.83
- **Build number:** 170.4.83.3
- **Release date:** June 3, 2026

SqlPackage is developed and released for both .NET and .NET Framework. Installing the .NET 10 SqlPackage version is recommended via the [convenient `dotnet tool` method](#installation-cross-platform), which is cross-platform and easy to update, or via the [portable self-contained .zip download](#installation-file-download-alternative). The .NET 10 SqlPackage releases benefit from the continual advances to the performance and scalability of .NET as part of the [focus on for modern applications](/dotnet/core/introduction#net-ecosystem), which contrasts to the maintenance support of .NET Framework for Windows. The .NET Framework version is only available as a [.msi Windows installer](#windows-net-framework).

> [!NOTE]  
> Previously, SqlPackage had a distinct version number (19) and build number (160.x). Beginning with version 161, the version number of SqlPackage matches the DacFx version number it's associated with (for example, 162.0.52).

## Installation, cross-platform

Installing SqlPackage as a [dotnet tool](/dotnet/core/tools/global-tools) requires the [.NET SDK](https://dotnet.microsoft.com/download/dotnet/10.0) to be installed on your machine. Installing SqlPackage as a global tool makes it available on your path as `sqlpackage` and is the recommended method to install SqlPackage for Windows, macOS, and Linux. SqlPackage is available as a dotnet tool for .NET 8 and later versions.

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

### Install SqlPackage with future releases of .NET

To install SqlPackage with a newer version of the .NET SDK, add `--allow-roll-forward` to the install command:

   ```bash
   dotnet tool install -g microsoft.sqlpackage --allow-roll-forward
   ```

This option allows SqlPackage to use a newer version of the .NET runtime if .NET 8 or .NET 10 isn't installed.

### Troubleshoot installation

If you encounter issues during installation, follow these steps:

1. **Ensure .NET SDK is installed**: Verify that the .NET SDK is installed on your machine by running the following command:

   ```bash
   dotnet --list-sdks
   ```

   If the .NET SDK isn't listed, download and install it from the [.NET SDK download page](https://dotnet.microsoft.com/download/dotnet/10.0).

1. **Verify NuGet source configuration**: SqlPackage is published to `nuget.org`, a public NuGet feed. You might encounter an error indicating that `microsoft.sqlpackage` can't be found:

   ```output
   microsoft.sqlpackage is not found in NuGet feeds C:\Program Files(x86)\Microsoft SDKs\NuGetPackages\
   ```

   Ensure that `nuget.org` is a configured NuGet source for dotnet. List the configured NuGet sources by running:

   ```bash
   dotnet nuget list source
   ```

   This command should display a list of NuGet sources. Look for `nuget.org` in the output, which is typically listed as `https://api.nuget.org/v3/index.json`. If `nuget.org` isn't listed, add it as a NuGet source using the following command:

   ```bash
   dotnet nuget add source https://api.nuget.org/v3/index.json
   ```

1. **Retry installation**: After verifying the .NET SDK installation and NuGet source configuration, retry installing SqlPackage using the appropriate command:

   ```bash
   dotnet tool install -g microsoft.sqlpackage
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

SqlPackage is also prepared as a self-contained download for Windows, macOS, and Linux. No .NET install is required, however, the [operating system requirements](#supported-operating-systems) are the same as the [dotnet tool install](#installation-cross-platform). The dependencies included in this .zip download are updated more frequently in the [dotnet tool option for SqlPackage](#installation-cross-platform). The following links are for the latest version of SqlPackage:

| Platform | Download |
| --- | --- |
| Windows .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2366026) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2366025) |
| macOS .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2365929) |
| Linux .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2365831) |

### Linux

1. Download [SqlPackage for Linux](https://aka.ms/sqlpackage-linux)
1. Extract the file and launch SqlPackage. Open a new Terminal window and type the following commands:

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
   > You might have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:

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
1. Extract the file and launch SqlPackage. Open a new Terminal window and type the following commands:

   ```bash
   mkdir sqlpackage
   unzip ~/Downloads/sqlpackage-osx-<version string>.zip -d ~/sqlpackage
   chmod +x ~/sqlpackage/sqlpackage
   echo 'export PATH="$PATH:~/sqlpackage"' >> ~/.bash_profile
   source ~/.bash_profile
   sqlpackage
   ```

   > [!NOTE]  
   > Security settings might require modification to run SqlPackage on macOS. Use the following commands to interact with Gatekeeper from the command line.

   **Before executing SqlPackage:**

   ```bash
   sudo spctl --master-disable
   ```

   **After executing SqlPackage:**

   ```bash
   sudo spctl --master-enable
   ```

### Windows (.NET 10)

1. Download [SqlPackage for Windows](https://aka.ms/sqlpackage-windows)
1. Extract the file by right-clicking on the file in Windows Explorer, and selecting 'Extract All...', and select the target directory
1. Open a new Terminal window and cd to the location where SqlPackage was extracted:

   ```cmd
   > sqlpackage
   ```

### Windows (.NET Framework)

This release of SqlPackage includes a standard Windows installer experience, and a .zip:

1. Download and run the [DacFramework.msi installer for Windows](https://aka.ms/dacfx-msi)
1. Open a new Command Prompt window, and run SqlPackage
   - SqlPackage is installed to the ```C:\Program Files\Microsoft SQL Server\170\DAC\bin``` folder

### Uninstall SqlPackage

If you installed SqlPackage using the Windows installer, then uninstall the same way you remove any Windows application.

If you installed SqlPackage with a .zip or other archive, then delete the files.

### Automated environments

Installing the dotnet tool version of SqlPackage is recommended for automated environments, such as CI/CD pipelines, due to its ease of installation and update. However, the file download option can be used in automated environments as well.

   ```bash
   dotnet tool install -g microsoft.sqlpackage
   ```

Evergreen links are available for downloading the latest SqlPackage versions:

- Linux (<https://aka.ms/sqlpackage-linux>)
- macOS (<https://aka.ms/sqlpackage-macos>)
- Windows (<https://aka.ms/sqlpackage-windows>)
- Windows, .NET Framework (<https://aka.ms/dacfx-msi>)

## DacFx

SqlPackage is a command-line interface for the DacFx framework, exposing some of the public DacFx APIs. DacServices ([Microsoft.SqlServer.Dac](/dotnet/api/microsoft.sqlserver.dac.dacservices)) is a related mechanism for integrating database deployment into your application pipeline. The DacServices API is available in a package through NuGet, [Microsoft.SqlServer.DacFx](https://www.NuGet.org/packages/Microsoft.SqlServer.DacFx).

Adding the NuGet package to a .NET project is accomplished via the .NET CLI with this command:

```cmd
dotnet add package Microsoft.SqlServer.DacFx
```

## Supported Operating Systems

SqlPackage runs on Windows, macOS, and Linux and is built using .NET 10. The [.NET 10 OS requirements](https://github.com/dotnet/core/blob/main/release-notes/10.0/supported-os.md) are minimum requirements for SqlPackage, which has extra requirements due to its dependencies.

### Windows (x64)

- Windows 11
- Windows 10 (1607+)
- Windows Server Core 2012 R2+
- Windows Server 2012 R2+

### macOS

- macOS 14 "Sonoma"+

### Linux (x64)

- Debian 12+
- Red Hat Enterprise Linux 8+
- SUSE Linux Enterprise Server 15 SP6+
- Ubuntu 22.04+

## Available Languages

This release of SqlPackage can be installed in the following languages:

SqlPackage .NET 10 Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2366026&clcid=0x40a)

SqlPackage .NET Framework Windows:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2366025&clcid=0x40a)

SqlPackage .NET 10 macOS:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2365929&clcid=0x40a)

SqlPackage .NET 10 Linux:  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2365831&clcid=0x40a)

## Related content

- [SqlPackage](sqlpackage.md)
- [SqlPackage in development pipelines](sqlpackage-pipelines.md)
- [Troubleshoot issues and performance with SqlPackage](troubleshooting-issues-and-performance-with-sqlpackage.md)
- [What are SQL database projects?](../sql-database-projects/sql-database-projects.md)
- [DacFx GitHub repository](https://github.com/microsoft/DacFx)
- [Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839)
