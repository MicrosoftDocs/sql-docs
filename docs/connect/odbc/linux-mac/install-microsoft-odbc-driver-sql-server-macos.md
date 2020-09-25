---
title: Install the Microsoft ODBC driver for SQL Server (macOS)
description: "Learn how to install the Microsoft ODBC Driver for SQL Server on macOS clients to enable database connectivity."
ms.date: 09/08/2020
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver, installing"
author: David-Engel
ms.author: v-daenge
---

# Install the Microsoft ODBC driver for SQL Server (macOS)

This article explains how to install the Microsoft ODBC Driver for SQL Server on macOS. It also includes instructions for the optional command-line tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC development headers.

This article provides commands for installing the ODBC driver from the bash shell. If you want to download the packages directly, see [Download ODBC Driver for SQL Server](../download-odbc-driver-for-sql-server.md).

## Microsoft ODBC 17

To install Microsoft ODBC driver 17 for SQL Server on macOS, run the following commands:

```bash
/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
HOMEBREW_NO_ENV_FILTERING=1 ACCEPT_EULA=Y brew install msodbcsql17 mssql-tools
```

> [!IMPORTANT]
> If you installed the v17 `msodbcsql` package that was briefly available, you should remove it before installing the `msodbcsql17` package. This will avoid conflicts. The `msodbcsql17` package can be installed side by side with the `msodbcsql` v13 package.

## Previous versions

The following sections provide instructions for installing previous versions of the Microsoft ODBC driver on macOS.

## <a id="13.1"></a> ODBC 13.1

Use the following commands to install the Microsoft ODBC driver 13.1 for SQL Server on OS X 10.11 (El Capitan) and macOS 10.12 (Sierra):

```bash
/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
brew install msodbcsql@13.1.9.2 mssql-tools@14.0.6.0
```

## Driver files

The ODBC driver on macOS consists of the following components:

|Component|Description|  
|---------------|-----------------|  
|libmsodbcsql.17.dylib or libmsodbcsql.13.dylib|The dynamic library (`dylib`) file that contains all of the driver's functionality. This file is installed in `/usr/local/lib/`.|  
|`msodbcsqlr17.rll` or `msodbcsqlr13.rll`|The accompanying resource file for the driver library. This file is installed in `[driver .dylib directory]../share/msodbcsql17/resources/en_US/` for Driver 17 and in `[driver .dylib directory]../share/msodbcsql/resources/en_US/` for Driver 13. | 
|msodbcsql.h|The header file that contains all of the new definitions needed to use the driver.<br /><br /> **Note:**  You cannot reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h is installed in `/usr/local/include/msodbcsql17/` for Driver 17 and in `/usr/local/include/msodbcsql/` for Driver 13. |
|LICENSE.txt|The text file that contains the terms of the End-User License Agreement. This file is placed in `/usr/local/share/doc/msodbcsql17/` for Driver 17 and in `/usr/local/share/doc/msodbcsql/` for Driver 13. |
|RELEASE_NOTES|The text file that contains release notes. This file is placed in `/usr/local/share/doc/msodbcsql17/` for Driver 17 and in `/usr/local/share/doc/msodbcsql/` for Driver 13. |

## Resource file loading

The driver needs to load the resource file in order to function. This file is called `msodbcsqlr17.rll` or `msodbcsqlr13.rll` depending on the driver version. The location of the `.rll` file is relative to the location of the driver itself (`so` or `dylib`), as noted in the table above. As of version 17.1 the driver will also attempt to load the `.rll` from the default directory if loading from the relative path fails. The default resource file path on macOS is `/usr/local/share/msodbcsql17/resources/en_US/`

## Troubleshooting

Some users encounter an issue when trying to connect after installing the ODBC driver and receive an error like: `"[01000] [unixODBC][Driver Manager]Can't open lib 'ODBC Driver 17 for SQL Server' : file not found (0) (SQLDriverConnect)"`. It may be the case that unixODBC is not configured correctly to find registered drivers. In these cases, creating a couple symbolic links can resolve the issue.

```bash
sudo ln -s /usr/local/etc/odbcinst.ini /etc/odbcinst.ini
sudo ln -s /usr/local/etc/odbc.ini /etc/odbc.ini
```

For additional cases where you are unable to make a connection to SQL Server using the ODBC driver, see the known issues article on [troubleshooting connection problems](known-issues-in-this-version-of-the-driver.md#connectivity).

## Next steps

After installing the driver, you can try the [C++ ODBC example application](../../odbc/cpp-code-example-app-connect-access-sql-db.md). For more information about developing ODBC applications, see [Developing Applications](../../../odbc/reference/develop-app/developing-applications.md).

For more information, see the ODBC driver [release notes](release-notes-odbc-sql-server-linux-mac.md) and [system requirements](system-requirements.md).
