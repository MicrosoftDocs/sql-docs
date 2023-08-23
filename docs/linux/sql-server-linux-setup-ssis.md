---
title: Install SQL Server Integration Services on Linux
description: This article describes how to install SQL Server Integration Services (SSIS) on Linux. You can install SSIS on Ubuntu and Red Hat Enterprise Linux.
author: lrtoyou1223
ms.author: lle
ms.reviewer: maghan, randolphwest
ms.date: 03/01/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: intro-installation
---
# Install SQL Server Integration Services (SSIS) on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Follow the steps in this article to install SQL Server Integration Services (**mssql-server-is**) on Linux. For more information about the features that are supported in Integration Services for Linux, see:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md)
- [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md)
- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md)

You can install SQL Server Integration Services (SSIS) on Red Hat Enterprise Linux (RHEL) and Ubuntu. SUSE Linux Enterprise Server (SLES) isn't supported. Installing SSIS on containers is also not supported.

# [Red Hat Enterprise Linux](#tab/rhel)

## <a id="RHEL"></a> Install SSIS on RHEL

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

1. After the configuration is done, set the `PATH` environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

1. Download the SQL Server Red Hat repository configuration file.

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo
   ```

1. Run the following command to install SQL Server Integration Services.

   ```bash
   sudo yum install -y mssql-server-is
   ```

1. After installation, run **ssis-conf**. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. After the configuration is done, set the `PATH` environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

1. Download the SQL Server Red Hat repository configuration file.

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2022.repo
   ```

1. Run the following command to install SQL Server Integration Services.

   ```bash
   sudo yum install -y mssql-server-is
   ```

1. After installation, run **ssis-conf**. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. After the configuration is done, set the `PATH` environment variable.

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

# [SUSE Linux Enterprise Server](#tab/sles)

## <a id="SLES"></a> Install SSIS on SLES

The SQL Server Integration Services package (**mssql-server-is**) is not supported on SUSE Linux Enterprise Server.

# [Ubuntu](#tab/ubuntu)

## <a id="ubuntu"></a> Install SSIS on Ubuntu

To install the **mssql-server-is** package on Ubuntu, follow these steps:

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the SQL Server Ubuntu repository.

   ```bash
   sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list)"
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

1. After the configuration is done, set the `PATH` environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the SQL Server Ubuntu repository.

   ```bash
   sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"
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

1. After the configuration is done, set the `PATH` environment variable.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the SQL Server Ubuntu repository.

   ```bash
   sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list)"
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

1. After the configuration is done, set the `PATH` environment variable.

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

---

## Unattended setup

To run **ssis-conf setup** unattended (RHEL and Ubuntu only), do the following steps:

1. Specify the `-n` (no prompt) option.
1. Provide required values by setting environment variables.

The following example does these actions:

- Installs SSIS
- Specifies the Developer edition by providing a value for the `SSIS_PID` environment variable
- Accepts the Microsoft Software License Terms by providing a value for the `ACCEPT_EULA` environment variable
- Runs an unattended setup by specifying the `-n` (no prompt) option

```bash
sudo SSIS_PID=Developer ACCEPT_EULA=Y /opt/ssis/bin/ssis-conf -n setup
```

### Environment variables for unattended setup

| Environment variable | Description |
| --- | --- |
| `ACCEPT_EULA` | Accepts the SQL Server license terms when set to any value like `Y`. |
| `SSIS_PID` | Sets the SQL Server edition or product key. Here are the possible values:<br /><br />- Evaluation<br />- Developer<br />- Express<br />- Web<br />- Standard<br />- Enterprise<br />- A product key<br /><br />If you specify a product key, it must be in the form `#####-#####-#####-#####-#####`, where `#` is a letter or a digit. |

## Next steps

- [Extract, transform, and load data on Linux with SSIS](sql-server-linux-migrate-ssis.md)
- [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md)
- [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md)
- [Schedule SQL Server Integration Services package execution on Linux with cron](sql-server-linux-schedule-ssis-packages.md)
