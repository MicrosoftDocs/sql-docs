---
title: Configure SQL Server settings with environment variables | Microsoft Docs
description: This article describes how to use environment variables to configure specific SQL Server 2017 settings on Linux.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/20/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 
---
# Configure SQL Server settings with environment variables on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

You can use several different environment variables to configure SQL Server 2017 on Linux. These variables are used in two scenarios:

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

You can use several different environment variables to configure SQL Server 2019 preview on Linux. These variables are used in two scenarios:

::: moniker-end

- To configure initial setup with the `mssql-conf setup` command.
- To configure a new [SQL Server container in Docker](quickstart-install-connect-docker.md).

> [!TIP]
> If you need to configure SQL Server after these setup scenarios, see [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

## Environment variables

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

| Environment variable | Description |
|-----|-----|
| **ACCEPT_EULA** | Accept the SQL Server license agreement when set to any value (for example, 'Y'). |
| **MSSQL_SA_PASSWORD** | Configure the SA user password. |
| **MSSQL_PID** | Set the SQL Server edition or product key. Possible values include: </br></br>**Evaluation**</br>**Developer**</br>**Express**</br>**Web**</br>**Standard**</br>**Enterprise**</br>**A product key**</br></br>If specifying a product key, it must be in the form of #####-#####-#####-#####-#####, where '#' is a number or a letter.|
| **MSSQL_LCID** | Sets the language ID to use for SQL Server. For example 1036 is French. |
| **MSSQL_COLLATION** | Sets the default collation for SQL Server. This overrides the default mapping of language id (LCID) to collation. |
| **MSSQL_MEMORY_LIMIT_MB** | Sets the maximum amount of memory (in MB) that SQL Server can use. By default it is 80% of the total physical memory. |
| **MSSQL_TCP_PORT** | Configure the TCP port that SQL Server listens on (default 1433). |
| **MSSQL_IP_ADDRESS** | Set the IP address. Currently, the IP address must be IPv4 style (0.0.0.0). |
| **MSSQL_BACKUP_DIR** | Set the Default backup directory location. |
| **MSSQL_DATA_DIR** | Change the directory where the new SQL Server database data files (.mdf) are created. |
| **MSSQL_LOG_DIR** | Change the directory where the new SQL Server database log (.ldf) files are created. |
| **MSSQL_DUMP_DIR** | Change the directory where SQL Server will deposit the memory dumps and other troubleshooting files by default. |
| **MSSQL_ENABLE_HADR** | Enable Availability Group. For example, '1' is enabled, and '0' is disabled |
| **MSSQL_AGENT_ENABLED** | Enable SQL Server Agent. For example, 'true' is enabled and 'false' is disabled. By default, agent is disabled.  |
| **MSSQL_MASTER_DATA_FILE** | Sets the location of the master database data file. Must be named **master.mdf** until first run of SQL Server. |
| **MSSQL_MASTER_LOG_FILE** | Sets the location of the master database log file. Must be named **mastlog.ldf** until first run of SQL Server. |
| **MSSQL_ERROR_LOG_FILE** | Sets the location of the errorlog files. |

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

| Environment variable | Description |
|-----|-----|
| **ACCEPT_EULA** | Accept the SQL Server license agreement when set to any value (for example, 'Y'). |
| **MSSQL_SA_PASSWORD** | Configure the SA user password. |
| **MSSQL_PID** | Set the SQL Server edition or product key. Possible values include: </br></br>**Evaluation**</br>**Developer**</br>**Express**</br>**Web**</br>**Standard**</br>**Enterprise**</br>**A product key**</br></br>If specifying a product key, it must be in the form of #####-#####-#####-#####-#####, where '#' is a number or a letter.|
| **MSSQL_LCID** | Sets the language ID to use for SQL Server. For example 1036 is French. |
| **MSSQL_COLLATION** | Sets the default collation for SQL Server. This overrides the default mapping of language id (LCID) to collation. |
| **MSSQL_MEMORY_LIMIT_MB** | Sets the maximum amount of memory (in MB) that SQL Server can use. By default it is 80% of the total physical memory. |
| **MSSQL_TCP_PORT** | Configure the TCP port that SQL Server listens on (default 1433). |
| **MSSQL_IP_ADDRESS** | Set the IP address. Currently, the IP address must be IPv4 style (0.0.0.0). |
| **MSSQL_BACKUP_DIR** | Set the Default backup directory location. |
| **MSSQL_DATA_DIR** | Change the directory where the new SQL Server database data files (.mdf) are created. |
| **MSSQL_LOG_DIR** | Change the directory where the new SQL Server database log (.ldf) files are created. |
| **MSSQL_DUMP_DIR** | Change the directory where SQL Server will deposit the memory dumps and other troubleshooting files by default. |
| **MSSQL_ENABLE_HADR** | Enable Availability Group. For example, '1' is enabled, and '0' is disabled |
| **MSSQL_AGENT_ENABLED** | Enable SQL Server Agent. For example, 'true' is enabled and 'false' is disabled. By default, agent is disabled.  |
| **MSSQL_MASTER_DATA_FILE** | Sets the location of the master database data file. Must be named **master.mdf** until first run of SQL Server. |
| **MSSQL_MASTER_LOG_FILE** | Sets the location of the master database log file. Must be named **mastlog.ldf** until first run of SQL Server. |
| **MSSQL_ERROR_LOG_FILE** | Sets the location of the errorlog files. |

::: moniker-end

## Use with initial setup

This example runs `mssql-conf setup` with configured environment variables. The following environment variables are specified:

- **ACCEPT_EULA** accepts the end user license agreement.
- **MSSSQL_PID** specifies the freely licensed Developer Edition of SQL Server for non-production use.
- **MSSQL_SA_PASSWORD** sets a strong password.
- **MSSQL_TCP_PORT** sets the TCP port that SQL Server listens on to 1234.

```bash
sudo ACCEPT_EULA='Y' MSSQL_PID='Developer' MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>' MSSQL_TCP_PORT=1234 /opt/mssql/bin/mssql-conf setup
```

## Use with Docker

This example docker command uses the following environment variables to create a new SQL Server container:

- **ACCEPT_EULA** accepts the end user license agreement.
- **MSSSQL_PID** specifies the freely licensed Developer Edition of SQL Server for non-production use.
- **MSSQL_SA_PASSWORD** sets a strong password.
- **MSSQL_TCP_PORT** sets the TCP port that SQL Server listens on to 1234. This means that instead of mapping port 1433 (default) to a host port, the custom TCP port must be mapped with the `-p 1234:1234` command in this example.

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

If you are running Docker on Linux/macOS, use the following syntax with single quotes:

```bash
docker run -e ACCEPT_EULA=Y -e MSSQL_PID='Developer' -e MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>' -e MSSQL_TCP_PORT=1234 -p 1234:1234 -d mcr.microsoft.com/mssql/server:2017-latest
```

If you are running Docker on Windows, use the following syntax with double quotes:

```bash
docker run -e ACCEPT_EULA=Y -e MSSQL_PID="Developer" -e MSSQL_SA_PASSWORD="<YourStrong!Passw0rd>" -e MSSQL_TCP_PORT=1234 -p 1234:1234 -d mcr.microsoft.com/mssql/server:2017-latest
```

> [!NOTE]
> The process for running production editions in containers is slightly different. For more information, see [Run production container images](sql-server-linux-configure-docker.md#production).

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

If you are running Docker on Linux/macOS, use the following syntax with single quotes:

```bash
docker run -e ACCEPT_EULA=Y -e MSSQL_PID='Developer' -e MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>' -e MSSQL_TCP_PORT=1234 -p 1234:1234 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

If you are running Docker on Windows, use the following syntax with double quotes:

```bash
docker run -e ACCEPT_EULA=Y -e MSSQL_PID="Developer" -e MSSQL_SA_PASSWORD="<YourStrong!Passw0rd>" -e MSSQL_TCP_PORT=1234 -p 1234:1234 -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

::: moniker-end

## Next steps

For other SQL Server settings not listed here, see [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

For more information on how to install and run SQL Server on Linux, see [Install SQL Server on Linux](sql-server-linux-setup.md).
