---
# required metadata

title: Configure SQL Server settings with environment variables | Microsoft Docs
description: This topic describes how to use environment variables to configure specific SQL Server 2017 settings on Linux.
author: rothja
ms.author: jroth
manager: jhubbard
ms.date: 05/18/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""
---
# Configure SQL Server settings with environment variables on Linux

You can use several different environment variables to configure SQL Server 2017 CTP 2.1 on Linux. This topic provides a list of the settings that are configurable using environment variables.

> [!TIP]
> Configuring SQL Server with environment variables is useful for SQL Server running on Docker. For more information, see [Run the SQL Server 2017 container image on Docker on Linux, Mac, or Windows](sql-server-linux-setup-docker.md). 

## How SQL Server prioritizes configuration settings

There are two main ways to configure SQL Server on Linux. You can use the [mssql-conf](sql-server-linux-configure-mssql-conf.md) tool, or you can use the environment variables covered in this topic. When SQL Server starts, the following prioritizaion is used:

1. Environment variables.
2. mssql-conf setting.
3. Default value.

## Environment variables

| Environment variable | Description |
|-----|-----|
| **ACCEPT_EULA** | Accept the SQL Server license agreement when set to any value (for example, 'Y'). Used on first install only. |
| **SA_PASSWORD** | Configure the SA user password. Used on first install only. |
| **MSSQL_TCP_PORT** | Configure the TCP port that SQL Server listens on (default 1433). |
| **MSSQL_IP_ADDRESS** | Set the IP address. Currently, the IP address must be IPv4 style (0.0.0.0). |
| **MSSQL_BACKUP_DIR** | Sets the Default backup directory location. |
| **MSSQL_DATA_DIR** | Change the directory where the new SQL Server database data files (.mdf) are created. |
| **MSSQL_LOG_DIR** | Changes the directory where the new SQL Server database log (.ldf) files are created. |
| **MSSQL_DUMP_DIR** | Change the directory where SQL Server will deposit the memory dumps and other troubleshooting files by default. |
| **MSSQL_ENABLE_HADR** | Enable Availability Groups. |

## Examples

### SQL Server setup

The following example uses a scenario where you are setting up SQL Server for the first time. **MSSQL_TCP_PORT** sets the TCP port that SQL Server listens on. **SA_PASSWORD** and **ACCEPT_EULA** are required for installation. SQL Server is then restarted after setup to apply the new port setting.

```bash
MSSQL_TCP_PORT=1234
SA_PASSWORD=<YourStrong!Passw0rd>
ACCEPT_EULA=Y

sudo /opt/mssql/bin/mssql-conf setup

sudo systemctl restart mssql-server
```

### Docker

The following docker run command also uses **MSSQL_TCP_PORT** to set the TCP port that SQL Server listens on. In this case, the host port mapping must that this new port into consideration. Instead of mapping port 1433 (default) to a host port, the custom TCP port must be mapped (1234 in this example).

```bash
docker run -e ACCEPT_EULA=Y -e SA_PASSWORD='<YourStrong!Passw0rd>' -e MSSQL_TCP_PORT=1234 -p 1234:1234 -d microsoft/mssql-server-linux
```

## Next steps

For other SQL Server settings not listed here, see [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

For more information on how to install and run SQL Server on Linux, see [Install SQL Server on Linux](sql-server-linux-setup.md).
