---
title: "PolyBase: How to Change SQL Server Driver Version"
description: Learn how to change the SQL Server driver version for PolyBase.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, hudequei
ms.date: 06/11/2025
ms.service: sql
ms.subservice: polybase
ms.topic: how-to
monikerRange: ">=sql-server-2017 || =azuresqldb-mi-current"
---

# Change the SQL Server driver version for PolyBase

[!INCLUDE [sqlserver2025-asmi](../../includes/applies-to-version/sqlserver2025-asmi.md)]

This article describes how to change the SQL Server driver version for PolyBase.

When using `sqlserver` as a provider, PolyBase uses the Microsoft ODBC Driver for SQL Server installed with the product. Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], two versions of the driver are installed: ODBC version 18 (the default), and ODBC version 17.

You should always use the latest driver. However, to ensure compatibility with previous editions, older ODBC versions can also be used.

For more information about which driver version supports which SQL Server version, see [System requirements, installation, and driver files](../../connect/odbc/windows/system-requirements-installation-and-driver-files.md).

## Change the version of the ODBC driver

To use Microsoft ODBC Driver version 17 with PolyBase, you must update the `PolyBase ODBC Driver for SQL Server.ini` file to specify which version of ODBC you want to use.

| Operating system | Location |
| --- | --- |
| Windows | *\<SQL Server installation Folder>\binn\PolyBase\ODBC Drivers* |
| Linux | `/var/opt/mssql/binn/Polybase/ODBC Drivers` |

Change the following setup lines from:

```ini
Driver=PolyBase ODBC Driver for SQL Server\18.5.1.1\msodbcsql18.dll
Setup=PolyBase ODBC Driver for SQL Server\18.5.1.1\msodbcsql18.dll
```

To:

```ini
Driver=PolyBase ODBC Driver for SQL Server\17.10.6.1\msodbcsql17.dll
Setup=PolyBase ODBC Driver for SQL Server\17.10.6.1\msodbcsql17.dll
```

## Restart SQL Server

After changing the INI file, you need to restart both PolyBase services:

- SQL Server PolyBase Data Movement
- SQL Server PolyBase Engine

On Linux, you can run the following command:

```bash
sudo systemctl restart mssql-server
```

## Related content

- [Get started with PolyBase in SQL Server 2022](polybase-get-started.md)
- [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md)
- [PolyBase configuration and security for Hadoop](polybase-configuration.md)
