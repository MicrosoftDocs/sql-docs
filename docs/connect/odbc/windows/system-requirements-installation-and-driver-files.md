---
title: System Requirements, Installation, and Driver Files
description: This article describes the system requirements for the Microsoft ODBC Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl, vanto
ms.date: 11/12/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - ignite-2024
---

# System requirements, installation, and driver files

[!INCLUDE [Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article describes the Microsoft ODBC Driver for SQL Server and its compatibility with Microsoft SQL products.

<a id="sql-version-compatibility"></a>

## Microsoft SQL version compatibility

Compatibility indicates that you can use a driver with the listed database version. SQL Server releases generally maintain backward compatibility with existing client drivers, but older drivers might not expose features introduced in later database versions.

| Database version&nbsp;&#8594;<br />&#8595; Driver Version | SQL database in Microsoft Fabric | Azure SQL Database | Azure Synapse Analytics | Azure SQL Managed Instance | SQL Server 2025 | SQL Server 2022 | SQL Server 2019 | SQL Server 2017 |
| ---| --- | --- | --- | --- | --- | --- | --- | --- |
| 18.7 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.6 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.5 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.4 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.3 | | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.2 | | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.1 | | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.0 | | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 17.11 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 17.10 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 17.9 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.8 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.7 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.6 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.5 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.4 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.3 | | Yes | Yes | Yes | | | Yes | Yes |
| 17.2 | | Yes | Yes | Yes | | | | Yes |
| 17.1 | | Yes | Yes | Yes | | | | Yes |
| 17.0 | | Yes | Yes | Yes | | | | Yes |
| 13.1 | | | | | | | | Yes |
| 13 | | | | | | | | |
| 11 | | | | | | | | |

### Connection string details

Specify one of the following driver names in a connection string:

- Use `ODBC Driver 18 for SQL Server` for Driver 18.
- Use `ODBC Driver 17 for SQL Server` for Driver 17.
- Use `ODBC Driver 13 for SQL Server` for Driver 13 and 13.1.
- Use `ODBC Driver 11 for SQL Server` for Driver 11.

## Supported operating systems

The following table shows which Windows operating system versions each driver release supports:

| Operating system&nbsp;&#8594;<br />&#8595; Driver version | Windows Server 2025 | Windows Server 2022 | Windows Server 2019 | Windows Server 2016 | Windows 11 |
| --- | --- | --- | --- | --- | --- |
| 18.7 | Yes | Yes | Yes | Yes | Yes |
| 18.6 | Yes | Yes | Yes | Yes | Yes |
| 18.5 | Yes | Yes | Yes | Yes | Yes |
| 18.4 | | Yes | Yes | Yes | Yes |
| 18.3 | | Yes | Yes | Yes | Yes |
| 18.2 | | Yes | Yes | Yes | Yes |
| 18.1 | | Yes | Yes | Yes | Yes |
| 18.0 | | Yes | Yes | Yes | Yes |
| 17.10 | Yes | Yes | Yes | Yes | Yes |
| 17.9 | | Yes | Yes | Yes | Yes |
| 17.8 | | | Yes | Yes | |
| 17.7 | | | Yes | Yes | |
| 17.6 | | | Yes | Yes | |
| 17.5 | | | Yes | Yes | |
| 17.4 | | | Yes | Yes | |
| 17.3 | | | Yes | Yes | |
| 17.2 | | | | Yes | |
| 17.1 | | | | Yes | |
| 17.0 | | | | Yes | |
| 13.1 | | | | Yes | |
| 13 | | | | | |
| 11 | | | | | |

<a id="installing-microsoft-odbc-driver-for-sql-server"></a>

## Install Microsoft ODBC Driver for SQL Server

Use Driver 18 for new applications. Driver 17 remains supported for applications that require version 17 behavior. Before you upgrade an application from Driver 17 to Driver 18, review the [major version differences](../major-version-differences.md), including the change to encryption defaults. For support dates, see the [support lifecycle](../support-lifecycle.md).

Both major versions use an installer named `msodbcsql.msi`. Select the Driver 18 or Driver 17 installer from [Downloads for Windows](../download-odbc-driver-for-sql-server.md#download-for-windows). The installed driver name and software development kit (SDK) files remain specific to the major version.

1. Select the installer for your Windows operating-system architecture: x64, x86, or ARM64, as available for the selected release. Use the x64 installer on x64 Windows, even for 32-bit applications; don't use the x86 installer on 64-bit Windows.
1. Download the installer into a separate directory for the selected major version. Driver 17 and Driver 18 installers have the same file name.
1. Run the installer with administrator permission, review the license terms, and complete the installation. For unattended installation or SDK components, use the options in the following sections.
1. Repeat [Verify the installation](#verify-the-installation) and check the registration and file version for the major version you installed.

To update an installed driver, download and run the installer for the same major version. To move from Driver 17 to Driver 18, install Driver 18 and update your application to request `ODBC Driver 18 for SQL Server`.

On Windows ARM64, remove any x64 Driver 18 installation through [Remove or reinstall Driver 17 or Driver 18](#remove-or-reinstall-driver-17-or-driver-18) before you run the ARM64 installer. The Driver 18.7.1.1 ARM64 installer reports an architecture conflict and stops only for x64 Driver 18 versions through 18.5.1.1. It doesn't detect later x64 versions, so both products remain installed. After you remove the x64 installation, run the ARM64 installer and verify the installation.

> [!NOTE]  
> Users with Driver version 17.1.0.1 or earlier are advised to manually uninstall the existing driver before proceeding with the installation of the updated version.

### Verify the installation

Run the following commands in PowerShell to check the Windows processor architecture, registered driver names, ODBC platforms, driver paths, and file versions:

```powershell
$ErrorActionPreference = 'Stop'
Get-CimInstance -ClassName Win32_ComputerSystem |
    Select-Object -ExpandProperty SystemType

$driverNames = @(
    'ODBC Driver 17 for SQL Server',
    'ODBC Driver 18 for SQL Server'
)

$drivers = @(
    Get-OdbcDriver -Platform All |
        Where-Object { $_.Name -in $driverNames }
)

if ($drivers.Count -eq 0) {
    throw 'Neither ODBC Driver 17 nor ODBC Driver 18 for SQL Server is registered.'
}

foreach ($driver in $drivers) {
    $driverPath = $driver.Attribute['Driver']
    [pscustomobject]@{
        Name = $driver.Name
        Platform = $driver.Platform
        DriverPath = $driverPath
        FileVersion = (Get-Item -LiteralPath $driverPath).VersionInfo.FileVersion
    }
}
```

`SystemType` reports the Windows processor architecture. The `Platform` property shows whether a driver is registered for 32-bit or 64-bit applications. Neither value establishes your application's target architecture or whether its driver binary runs natively or under emulation. The file version varies by installed release. The block fails if neither major version is registered; a single-major installation is valid. Check that the results include the major version you installed, then run the query again after an update.

### Remove or reinstall Driver 17 or Driver 18

Close applications that use the driver before removing it. On Windows with a desktop interface, open **Control Panel** > **Programs** > **Programs and Features**. Select **Microsoft ODBC Driver 17 for SQL Server** or **Microsoft ODBC Driver 18 for SQL Server**, and then select **Uninstall**. On Windows Server Core, use [Silent uninstall](#silent-uninstall) with the matching installer. You need administrator permission.

After removal, run the following registration query. Replace `18` with `17` to check Driver 17:

```powershell
Get-OdbcDriver -Platform All |
    Where-Object { $_.Name -eq 'ODBC Driver 18 for SQL Server' }
```

The query should return no matching registrations. To reinstall the driver, run the matching major-version installer from [Downloads for Windows](../download-odbc-driver-for-sql-server.md#download-for-windows), then repeat [Verify the installation](#verify-the-installation). Removing a driver can prevent applications that request its driver name from connecting. Don't delete driver files manually.

### C runtime compatibility

Driver 18.6 and earlier versions, including all Driver 17 releases, require the Visual C++ v14 Redistributable on Windows. Starting with Driver 18.7, the driver installer includes the runtime. When you install the Redistributable separately, use the latest supported version from [Microsoft Visual C++ Redistributable latest supported downloads](/cpp/windows/latest-supported-vc-redist). The latest supported version meets every minimum in the following table.

| Driver version | Architecture | Separate Visual C++ install required | Minimum Visual C++ version |
| --- | --- | --- | --- |
| 18.7.1.1 | x86, x64, ARM64 | No. The installer includes version 14.44.35211.0. | Not applicable |
| 17.11.1.1 | x86, x64 | Yes. Download [x86](https://aka.ms/vc14/vc_redist.x86.exe) or [x64](https://aka.ms/vc14/vc_redist.x64.exe). | 14.15 |
| 18.6.2.1 | x86, x64, ARM64 | Yes. Download [x86](https://aka.ms/vc14/vc_redist.x86.exe), [x64](https://aka.ms/vc14/vc_redist.x64.exe), or [ARM64](https://aka.ms/vc14/vc_redist.arm64.exe). | 14.34 |

The minimum is the major and minor version. It follows the [Visual C++ Redistributable compatibility rule](/cpp/windows/determining-which-dlls-to-redistribute), which requires the Redistributable's major version to match the toolset and its minor version to be the same or higher. For more information, see the [Windows prerequisites](../download-odbc-driver-for-sql-server.md#prerequisites).

> [!IMPORTANT]
> A native application that uses local deployment for an older Microsoft Visual C++ runtime can force a newer ODBC driver to load that runtime instead of the version required by the driver. For example, this issue can occur when an application built with the Visual Studio 2019 (`v142`) platform toolset includes an older runtime in the application directory. The version mismatch can cause failures when the application loads or uses the ODBC driver.
>
> Don't use local deployment for the Visual C++ runtime. If your application requires local deployment, ensure its runtime is at least as new as the latest build tools used to build any application component, including the ODBC driver. For more information, see [C++ binary compatibility between Visual Studio versions: Restrictions](/cpp/porting/binary-compat-2015-2017#restrictions).

### Side-by-side with Native Client

The driver can be installed side-by-side with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. Major versions of the driver (11, 13, 17, 18) can all be installed side-by-side with each other, as well.

Starting with Driver 17.11.1.1 and Driver 18.5.1.1, the Windows installer installs both Core and SDK features by default. Earlier Driver 17 and Driver 18 releases install only Core by default. To install all features in an earlier release, specify `ADDLOCAL=ALL` on the command line. Here's an example.

```console
msiexec /i msodbcsql.msi ADDLOCAL=ALL
```

### End-user license

To accept the end-user license terms when you install with `/quiet`, `/passive`, `/qn`, `/qb`, or `/qr`, specify `IACCEPTMSODBCSQLLICENSETERMS=YES`. Write the property in uppercase letters. Here's an example.

```console
msiexec /quiet /i msodbcsql.msi IACCEPTMSODBCSQLLICENSETERMS=YES ADDLOCAL=ALL
```

### Silent uninstall

The following example shows how to perform a silent uninstall.

```console
msiexec /quiet /uninstall msodbcsql.msi
```

A silent uninstall can return exit code `0` without removing the driver. For example, the driver stays installed when an installed application registered a dependency on it. After a silent uninstall, run the registration query in [Remove or reinstall Driver 17 or Driver 18](#remove-or-reinstall-driver-17-or-driver-18) to confirm the driver was removed.

### Indicate dependency

When you bundle an application setup program with Microsoft Installer, assign it a product code. When you silently install the ODBC driver, set `APPGUID` to that product code. While the product with that code is installed, the driver installer stops any uninstall of the driver. Here's an example.

```console
msiexec /quiet /i msodbcsql.msi IACCEPTMSODBCSQLLICENSETERMS=YES APPGUID={<product-code-guid>}
```

Replace `<product-code-guid>` with the product code GUID assigned to your application. Don't enclose the `APPGUID=` property in quotation marks. When the whole property is quoted, `msiexec` treats it as an invalid argument and displays its usage message instead of installing the driver.

## Command-line tools: sqlcmd.exe and bcp.exe

The current Microsoft Command Line Utilities package contains `bcp.exe` and the ODBC-based `sqlcmd.exe`. The package is separate from the ODBC Driver and requires the latest Microsoft ODBC Driver for SQL Server. Download the package for [Windows x64](https://go.microsoft.com/fwlink/?linkid=2370127) or [Windows x86](https://go.microsoft.com/fwlink/?linkid=2370024).

Earlier packages are available from [Microsoft Command Line Utilities 11](https://www.microsoft.com/download/details.aspx?id=36433), [Microsoft Command Line Utilities 13](https://www.microsoft.com/download/details.aspx?id=52680), and [Microsoft Command Line Utilities 13.1](https://www.microsoft.com/download/details.aspx?id=53591). The older version 11 tools are installed in the `110\Tools` subfolder of `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC`; versions 13 and 13.1 use the `130\Tools` subfolder.

An application that uses BCP functions must use the driver version that matches the header and library used to compile it.

For example, compile with the Driver 18 `msodbcsql.h` and `msodbcsql18.lib`, then use `DRIVER={ODBC Driver 18 for SQL Server}` in the connection string. For Driver 17, use `msodbcsql17.lib` and `DRIVER={ODBC Driver 17 for SQL Server}`.

<a id="components-of-the-microsoft-odbc-driver-for-sql-server-on-windows"></a>

## Key components of the Microsoft ODBC Driver for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows

The Windows installer includes the following key components:

| Component | Description |
| :--- | :--- |
| `msodbcsql18.dll` or<br />`msodbcsql17.dll` or<br />`msodbcsql13.dll` or<br />`msodbcsql11.dll` | The main driver dynamic-link library (DLL). Windows installs the file in the system directory for its architecture. |
| `msodbcdiag18.dll` or<br />`msodbcdiag17.dll` or<br />`msodbcdiag13.dll` or<br />`msodbcdiag11.dll` | The dynamic-link library (DLL) file that contains the driver's diagnostics (tracing) interface. Windows installs the file in the system directory for its architecture. |
| `msodbcsqlr18.rll` or<br />`msodbcsqlr17.rll` or<br />`msodbcsqlr13.rll` or<br />`msodbcsqlr11.rll` | The accompanying resource file for the driver library. Windows installs the file in the `1033` subdirectory of the system directory for its architecture. |
| `s13ch_msodbcsql.chm` or<br />`s11ch_msodbcsql.chm` | The Data Source Wizard help file that documents how to create a data source for the driver. Windows installs the file in the `1033` subdirectory of the system directory for its architecture.<br /><br />**NOTE:** There's no chm file for ODBC Driver 17 and later versions. |
| `msodbcsql.h` | The header file that contains all of the new definitions needed to use the driver.<br /><br />**Note:** You can't reference `msodbcsql.h` and `odbcss.h` in the same program.<br />`msodbcsql.h` for ODBC Driver 18 is installed in `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\180\SDK\Include`.<br />`msodbcsql.h` for ODBC Driver 17 is installed in `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\170\SDK\Include`.<br />`msodbcsql.h` for ODBC Driver 13 is installed in `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK`.<br />`msodbcsql.h` for ODBC Driver 11 is installed in `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK`. |
| `msodbcsql18.lib` or<br />`msodbcsql17.lib` or<br />`msodbcsql13.lib` or<br />`msodbcsql11.lib` | The library file needed to call the **bcp** utility functions that are part of the driver.<br /><br />`msodbcsql18.lib` is installed under `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\180\SDK\Lib`.<br />`msodbcsql17.lib` is installed under `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\170\SDK\Lib`.<br />For Driver 17 and Driver 18, select the `x86`, `x64`, or `arm64` subdirectory for the application target, as available in the selected installer, and add it to your project's linker library search path.<br />`msodbcsql13.lib` is installed in `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK`.<br />`msodbcsql11.lib` is installed in `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK`. |

## Related content

- [Microsoft ODBC Driver for SQL Server on Windows](microsoft-odbc-driver-for-sql-server-on-windows.md)
- [Major version differences](../major-version-differences.md)
- [Support lifecycle](../support-lifecycle.md)
