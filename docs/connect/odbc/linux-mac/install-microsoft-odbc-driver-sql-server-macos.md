---
title: Install the Microsoft ODBC Driver for SQL Server (macOS)
description: Learn how to install the Microsoft ODBC Driver for SQL Server on macOS clients to enable database connectivity.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, sunilbs, mcimfl
ms.date: 11/12/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: install-set-up-deploy
ai-usage: ai-assisted
ms.custom:
  - intro-installation
  - linux-related-content
helpviewer_keywords:
  - "driver, installing"
---

# Install the Microsoft ODBC driver for SQL Server (macOS)

This article explains how to install the Microsoft ODBC Driver for SQL Server on macOS. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

## Choose a driver version

Use Driver 18 for new applications. Driver 17 remains supported for applications that require version 17 behavior. Before you upgrade an application from Driver 17 to Driver 18, review the [major version differences](../major-version-differences.md), including the change to encryption defaults. For support dates, see the [support lifecycle](../support-lifecycle.md).

The following table shows the driver and command-line tools formulas used in the commands in this article:

| Version | Driver formula | Optional tools formula |
| --- | --- | --- |
| 18 | `msodbcsql18` | `mssql-tools18` |
| 17 | `msodbcsql17` | `mssql-tools` |

> [!NOTE]
> Driver versions through 17.7 on macOS are x64-only. Starting with Driver 17.8, the macOS packages support both x64 and Apple ARM64. The Homebrew formula detects the architecture and installs the matching package. If your command prompt is running in x64 emulation mode on ARM64, Homebrew installs the x64 package.
>
> Use **unixODBC** with Homebrew installs. The formulas for Driver 17 and Driver 18 install unixODBC as a dependency. If another setup uses **iODBC**, make sure the driver library and driver manager target the same architecture. An architecture mismatch can produce an error such as `incompatible architecture (have 'arm64', need 'x86_64')`.

## Prepare Homebrew

If Homebrew isn't installed, follow [Homebrew's installation instructions](https://brew.sh/). After installation, follow the shell-configuration instructions printed by the installer, then run `brew --version` to confirm that `brew` is available. Homebrew's default prefix on Apple silicon is `/opt/homebrew`.

## Microsoft ODBC 18

To install Microsoft ODBC driver 18 for SQL Server on macOS, run the following commands:

```bash
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql18 mssql-tools18
```

If Homebrew refuses to load a formula from an untrusted tap, verify the formula's canonical name and tap before you grant trust. To trust only the Driver 18 formulas, run:

```bash
brew trust --formula microsoft/mssql-release/msodbcsql18
brew trust --formula microsoft/mssql-release/mssql-tools18
```

Then retry the installation.

## Update or verify a driver installation

To update a driver, refresh Homebrew and upgrade the formula for the same major version. Replace `msodbcsql18` with `msodbcsql17` to update Driver 17. To move between major versions, install the other formula and configure your application to request its driver name. Both formulas can be installed at the same time.

```bash
brew update && HOMEBREW_ACCEPT_EULA=Y brew upgrade msodbcsql18
```

Set `driver_version` to `17` or `18`, then check the installed formula version, unixODBC registration, macOS architecture, and driver library architecture:

| Driver version | `driver_formula` | `driver_name` | Driver library |
| --- | --- | --- | --- |
| 18 | `msodbcsql18` | `ODBC Driver 18 for SQL Server` | `libmsodbcsql.18.dylib` |
| 17 | `msodbcsql17` | `ODBC Driver 17 for SQL Server` | `libmsodbcsql.17.dylib` |

```bash
set -e
driver_version=18
driver_formula="msodbcsql${driver_version}"
driver_name="ODBC Driver ${driver_version} for SQL Server"
driver_library="$(brew --prefix "$driver_formula")/lib/libmsodbcsql.${driver_version}.dylib"

brew list --versions "$driver_formula"
odbcinst -q -d -n "$driver_name"
uname -m
file "$driver_library"
```

The commands report the installed formula version, registered driver name, macOS architecture, and driver library architecture. By default, the Homebrew formula registers the driver. If you installed the formula with the `--without-registration` option, the registration query fails and the block stops before the architecture checks.

## Remove or reinstall Driver 17 or Driver 18

Close applications that use the driver before removing it. If you installed the optional tools, uninstall them first. For Driver 17, replace `mssql-tools18` with `mssql-tools`:

```bash
HOMEBREW_NO_AUTOREMOVE=1 brew uninstall mssql-tools18
```

Homebrew can automatically remove unused dependencies during `brew uninstall`. Set `HOMEBREW_NO_AUTOREMOVE=1` for both uninstall commands to keep `unixODBC` available until you unregister and verify the driver. If you run `brew autoremove`, wait until after that verification.

Then uninstall the driver. Replace `msodbcsql18` with `msodbcsql17` to remove Driver 17:

```bash
HOMEBREW_NO_AUTOREMOVE=1 brew uninstall msodbcsql18
```

If Homebrew reports that another formula depends on the driver, review that dependency before proceeding. Don't force removal of a driver needed by installed tools.

After uninstalling a formula that registered the driver, remove its registration. For Driver 17, replace `18` with `17`:

```bash
odbcinst -u -d -n "ODBC Driver 18 for SQL Server"
```

After removing the registration, run `odbcinst -q -d`. If it lists driver names, confirm that the removed driver name isn't listed. If it reports `SQLGetPrivateProfileString failed`, run `odbcinst -j` and check the `DRIVERS` path it reports. If the driver configuration file exists, is readable, and contains no driver registrations, no drivers remain registered. If the file is missing, unreadable, or still contains the removed driver's entry, resolve that configuration issue before treating removal as complete. To reinstall, repeat the installation commands for the same major version, then repeat verification. Removing a driver can prevent applications that request its driver name from connecting. Don't delete driver libraries or driver-manager configuration files manually.

## <a id="17"></a> Microsoft ODBC 17

To install Microsoft ODBC driver 17 for SQL Server on macOS, run the following commands:

```bash
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql17 mssql-tools
```

If Homebrew refuses to load a formula from an untrusted tap, verify the formula's canonical name and tap before you grant trust. To trust only the Driver 17 formulas, run:

```bash
brew trust --formula microsoft/mssql-release/msodbcsql17
brew trust --formula microsoft/mssql-release/mssql-tools
```

Then retry the installation.

You can install the `msodbcsql17` and `msodbcsql18` driver formulas side by side. The `mssql-tools` and `mssql-tools18` formulas both provide `sqlcmd` and `bcp`, so Homebrew can't link both. If `mssql-tools18` is installed, the preceding command installs `mssql-tools` but reports that the `brew link` step didn't complete, and `sqlcmd` and `bcp` continue to run the version 18 tools. To add Driver 17 while Driver 18 tools are installed, install only the driver:

```bash
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql17
```

> [!IMPORTANT]  
> If you installed the briefly available v17 `msodbcsql` package, remove it before installing `msodbcsql17` to avoid conflicts. You can install `msodbcsql17` side by side with the v13 `msodbcsql` package.

## <a id="previous-versions"></a> Earlier versions

The following section provides installation instructions for Driver 13.1.

## <a id="13.1"></a> ODBC 13.1

These commands document the historical Driver 13.1 path for OS X 10.11 (El Capitan) and macOS 10.12 (Sierra). Homebrew's [current supported-install requirements](https://docs.brew.sh/Installation#macos-requirements) list macOS Sequoia (15) or later; they don't include these older systems.

```bash
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
brew install msodbcsql@13.1.9.2 mssql-tools@14.0.6.0
```

## Driver files

Key ODBC driver files on macOS include:

| Component | Description |
| --- | --- |
| `libmsodbcsql.18.dylib`, `libmsodbcsql.17.dylib`, or `libmsodbcsql.13.dylib` | The main driver library. The Driver 18 library is under `$(brew --prefix msodbcsql18)/lib/`; the Driver 17 library is under `$(brew --prefix msodbcsql17)/lib/`. The historical Driver 13.1 library is under `/usr/local/lib/`. |
| `msodbcsqlr18.rll`, `msodbcsqlr17.rll`, or `msodbcsqlr13.rll` | The resource file for the driver library. For Driver 18, it's under `$(brew --prefix msodbcsql18)/share/msodbcsql18/resources/en_US/`; for Driver 17, it's under `$(brew --prefix msodbcsql17)/share/msodbcsql17/resources/en_US/`. The historical Driver 13.1 file is under `/usr/local/share/msodbcsql/resources/en_US/`. |
| `msodbcsql.h` | The header file that contains definitions for the driver. You can't reference `msodbcsql.h` and `odbcss.h` in the same program. The Driver 18 header is under `$(brew --prefix msodbcsql18)/include/msodbcsql18/`; the Driver 17 header is under `$(brew --prefix msodbcsql17)/include/msodbcsql17/`. The historical Driver 13.1 header is under `/usr/local/include/msodbcsql/`. |
| `LICENSE.txt` | The End-User License Agreement. It's under `$(brew --prefix msodbcsql18)/share/doc/msodbcsql18/` for Driver 18 and `$(brew --prefix msodbcsql17)/share/doc/msodbcsql17/` for Driver 17. The historical Driver 13.1 file is under `/usr/local/share/doc/msodbcsql/`. |
| `RELEASE_NOTES` | The release notes. They're under `$(brew --prefix msodbcsql18)/share/doc/msodbcsql18/` for Driver 18 and `$(brew --prefix msodbcsql17)/share/doc/msodbcsql17/` for Driver 17. The historical Driver 13.1 file is under `/usr/local/share/doc/msodbcsql/`. |

<a id="resource-file-loading"></a>

## Load the resource file

The driver needs the resource file to function. Its location is relative to the driver library, as shown in the previous table. Starting with version 17.1, the driver also checks its default resource directory if the relative lookup fails.

## Troubleshooting

If a connection fails with a `Can't open lib` error, check which configuration files unixODBC reads:

```bash
odbcinst -j
```

If the reported directory isn't the Homebrew configuration directory, set `ODBCSYSINI` for the shell that starts your application, then check the paths again:

```bash
export ODBCSYSINI="$(brew --prefix)/etc"
odbcinst -j
```

For more information about connection problems, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

If Homebrew can't find the formulas, add the Microsoft tap:

```bash
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
```

## Related content

- [Connect and query with C++ and ODBC](../cpp-code-example-app-connect-access-sql-db.md)
- [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md)
- [Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS](release-notes-odbc-sql-server-linux-mac.md)
- [System Requirements (Linux and macOS)](system-requirements.md)
