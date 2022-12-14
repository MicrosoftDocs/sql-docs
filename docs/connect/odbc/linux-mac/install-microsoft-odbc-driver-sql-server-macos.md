---
title: Install the Microsoft ODBC driver for SQL Server (macOS)
description: Learn how to install the Microsoft ODBC Driver for SQL Server on macOS clients to enable database connectivity.
author: David-Engel
ms.author: v-davidengel
ms.date: 09/20/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
ms.custom: intro-installation
helpviewer_keywords:
  - "driver, installing"
---

# Install the Microsoft ODBC driver for SQL Server (macOS)

This article explains how to install the Microsoft ODBC Driver for SQL Server on macOS. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

> [!Note]
> The Microsoft ODBC driver for SQL Server on macOS is only supported on the x64 architecture through version 17.7. Apple M1 (ARM64) support was added starting with version 17.8. The architecture will be detected and the correct package will be automatically installed by the Homebrew formula. If your command prompt is running in x64 emulation mode on the M1, the x64 package will be installed. If you're not running in emulation mode in your command prompt, the ARM64 package will be installed.
> Additionally, the Homebrew default directory changed with the M1, to `/opt/homebrew`. The paths below use the x64 Homebrew paths, which default to `/usr/local`, so your file paths will vary accordingly.

## Microsoft ODBC 18

To install Microsoft ODBC driver 18 for SQL Server on macOS, run the following commands:

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql18 mssql-tools18
```

## Previous versions

The following sections provide instructions for installing previous versions of the Microsoft ODBC driver on macOS.

## <a id="17"></a> Microsoft ODBC 17

To install Microsoft ODBC driver 17 for SQL Server on macOS, run the following commands:

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql17 mssql-tools
```

> [!IMPORTANT]
> If you installed the v17 `msodbcsql` package that was briefly available, you should remove it before installing the `msodbcsql17` package. This will avoid conflicts. The `msodbcsql17` package can be installed side by side with the `msodbcsql` v13 package.


## <a id="13.1"></a> ODBC 13.1

Use the following commands to install the Microsoft ODBC driver 13.1 for SQL Server on OS X 10.11 (El Capitan) and macOS 10.12 (Sierra):

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install.sh)"
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
brew install msodbcsql@13.1.9.2 mssql-tools@14.0.6.0
```

## Driver files

The ODBC driver on macOS consists of the following components:

|Component|Description|  
|---------------|-----------------|  
|libmsodbcsql.18.dylib or libmsodbcsql.17.dylib or libmsodbcsql.13.dylib|The dynamic library (`dylib`) file that contains all of the driver's functionality. This file is installed in `/usr/local/lib/`.|
|`msodbcsqlr18.rll` or `msodbcsqlr17.rll` or `msodbcsqlr13.rll`|The accompanying resource file for the driver library. This file is installed in `[driver .dylib directory]../share/msodbcsql18/resources/en_US/` for Driver 18, `[driver .dylib directory]../share/msodbcsql17/resources/en_US/` for Driver 17, and in `[driver .dylib directory]../share/msodbcsql/resources/en_US/` for Driver 13. |
|msodbcsql.h|The header file that contains all of the new definitions needed to use the driver.<br /><br /> **Note:**  You can't reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h is installed in `/usr/local/include/msodbcsql18/` for Driver 18, `/usr/local/include/msodbcsql17/` for Driver 17, and in `/usr/local/include/msodbcsql/` for Driver 13. |
|LICENSE.txt|The text file that contains the terms of the End-User License Agreement. This file is placed in `/usr/local/share/doc/msodbcsql18/` for Driver 18, `/usr/local/share/doc/msodbcsql17/` for Driver 17, and in `/usr/local/share/doc/msodbcsql/` for Driver 13. |
|RELEASE_NOTES|The text file that contains release notes. This file is placed in `/usr/local/share/doc/msodbcsql18/` for Driver 18, `/usr/local/share/doc/msodbcsql17/` for Driver 17, and in `/usr/local/share/doc/msodbcsql/` for Driver 13. |

## Resource file loading

The driver needs to load the resource file in order to function. This file is called `msodbcsqlr18.rll`, `msodbcsqlr17.rll`, or `msodbcsqlr13.rll` depending on the driver version. The location of the `.rll` file is relative to the location of the driver itself (`so` or `dylib`), as noted in the component table. As of version 17.1 the driver will also attempt to load the `.rll` from the default directory if loading from the relative path fails. The default resource file path on macOS is `/usr/local/share/msodbcsql18/resources/en_US/`

## Troubleshooting

Some users encounter an issue when trying to connect after installing the ODBC driver and receive an error like: `"[01000] [unixODBC][Driver Manager]Can't open lib 'ODBC Driver 18 for SQL Server' : file not found (0) (SQLDriverConnect)"`. It may be the case that unixODBC isn't configured correctly to find registered drivers. In these cases, creating symbolic links can resolve the issue.

```bash
sudo ln -s /usr/local/etc/odbcinst.ini /etc/odbcinst.ini
sudo ln -s /usr/local/etc/odbc.ini /etc/odbc.ini
```

For additional cases where you're unable to make a connection to SQL Server using the ODBC driver, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

If brew is having trouble finding the formulas, make sure you didn't skip the install step: `brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release`

## Next steps

After installing the driver, you can try the [C++ ODBC example application](../cpp-code-example-app-connect-access-sql-db.md). For more information about developing ODBC applications, see [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md).

For more information, see the ODBC driver [release notes](release-notes-odbc-sql-server-linux-mac.md) and [system requirements](system-requirements.md).
