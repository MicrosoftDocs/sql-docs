---
title: Install SQL Server Integration Services on Linux
description: This article describes how to install SQL Server Integration Services (SSIS) on Linux. You can install SSIS on Ubuntu 16.04 and Red Hat Enterprise Linux.
author: lrtoyou1223
ms.author: lle
ms.reviewer: maghan
ms.date: 01/09/2018
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.custom:
  - intro-installation
---
# Install SQL Server Integration Services (SSIS) on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Follow the steps in this article to install SQL Server Integration Services (**mssql-server-is**) on Linux. For info about the features that are supported in this release of Integration Services for Linux, see the [Release Notes](sql-server-linux-release-notes-2017.md).

You can install SQL Server Integration Services on these platforms:

- [Ubuntu 16.04](#ubuntu)
- [Red Hat Enterprise Linux](#RHEL)

## <a name="ubuntu"></a> Install SSIS on Ubuntu

To install the **mssql-server-is** package on Ubuntu, follow these steps:

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the SQL Server Ubuntu repository.

   ```bash
   sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2017.list)"
   ```

1. Run the following commands to install SQL Server Integration Services.

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server-is
   ```

1. After installing Integration Services, run **ssis-conf**. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. After the configuration is done, set the PATH environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the SQL Server Ubuntu repository.

   ```bash
   sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2019.list)"
   ```

1. Run the following commands to install SQL Server Integration Services.

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server-is
   ```

1. After installing Integration Services, run **ssis-conf**. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. After the configuration is done, set the PATH environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

### Update SSIS

If you already have **mssql-server-is** installed, update to the latest version with the following command:

```bash
sudo apt-get install mssql-server-is
```

### Remove SSIS

To remove **mssql-server-is**, run the following command:

```bash
sudo apt-get remove mssql-server-is
```

## <a name="RHEL"></a> Install SSIS on RHEL
To install the **mssql-server-is** package on RHEL, follow these steps:

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Download the SQL Server Red Hat repository configuration file.

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/7/mssql-server-2017.repo
   ```

1. Run the following command to install SQL Server Integration Services.

   ```bash
   sudo yum install -y mssql-server-is
   ```

1. After installation, run **ssis-conf**. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. After the configuration is done, set the PATH environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

1. Download the SQL Server Red Hat repository configuration file.

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/7/mssql-server-2019.repo
   ```

1. Run the following command to install SQL Server Integration Services.

   ```bash
   sudo yum install -y mssql-server-is
   ```

1. After installation, run **ssis-conf**. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. After the configuration is done, set the PATH environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

### Update SSIS

If you already have **mssql-server-is** installed, update to the latest version by using the following command:

```bash
sudo yum update mssql-server-is
```

### Remove SSIS
To remove **mssql-server-is**, run the following command:

```bash
sudo yum remove mssql-server-is
```

## Unattended installation

To run **ssis-conf setup** as an unattended installation, do the following steps:

1. Specify the **-n** (no prompt) option.
1. Provide required values by setting environment variables.

The following example does these actions:

- Installs SSIS
- Specifies the Developer edition by providing a value for the SSIS_PID environment variable
- Accepts the Microsoft Software License Terms by providing a value for the ACCEPT_EULA environment variable
- Runs an unattended installation by specifying the **-n** (no prompt) option

```
sudo SSIS_PID=Developer ACCEPT_EULA=Y /opt/ssis/bin/ssis-conf -n setup 
```

### Environment variables for unattended installation

| Environment variable | Description |
|---|---|
| ACCEPT_EULA | Accepts the SQL Server license terms when set to any value like "Y".|
| SSIS_PID | Sets the SQL Server edition or product key. Here are the possible values:<ul><li>Evaluation</li><li>Developer</li><li>Express</li><li>Web</li><li>Standard</li><li>Enterprise</li><li>A product key</li></ul>If you specify a product key, it must be in the form *#####*-*#####*-*#####*-*#####*-*#####*, where *#* is a letter or a digit.  |

## Next steps

To run SSIS packages on Linux, see [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md).

To configure additional SSIS settings on Linux, see [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

## Related content about SSIS on Linux

- [Extract, transform, and load data on Linux with SSIS](sql-server-linux-migrate-ssis.md)
- [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md)
- [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md)
- [Schedule SQL Server Integration Services package execution on Linux with cron](sql-server-linux-schedule-ssis-packages.md)
