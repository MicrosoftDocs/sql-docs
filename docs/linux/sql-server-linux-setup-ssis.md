---
title: Install SQL Server Integration Services on Linux | Microsoft Docs
description: This article describes how to install SQL Server Integration Services (SSIS) on Linux.
author: leolimsft 
ms.author: lle 
ms.reviewer: douglasl
manager: craigg
ms.date: 01/09/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
---
# Install SQL Server Integration Services (SSIS) on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

Follow the steps in this article to install SQL Server Integration Services (`mssql-server-is`) on Linux. For info about the features supported in this release of Integration Services for Linux, see the [Release Notes](sql-server-linux-release-notes.md).

Install SQL Server Integration Servers for your platform:

- [Ubuntu](#ubuntu)
- [Red Hat Enterprise Linux](#RHEL)

## <a name="ubuntu"></a> Install SSIS on Ubuntu
To install the `mssql-server-is` package on Ubuntu, follow these steps:

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

2. Register the Microsoft SQL Server Ubuntu repository.

   ```bash
   sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2017.list)"
   ```

3. Run the following commands to install SQL Server Integration Services.

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server-is
   ```

4. After installing Integration Services, run `ssis-conf`. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

5. After the configuration is done, set the path.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

### Update SSIS
If you already have `mssql-server-is` installed, you can update to the latest version with the following command:

```bash
sudo apt-get install mssql-server-is
```

### Remove SSIS
To remove `mssql-server-is`, you can run following command:
```bash
sudo apt-get remove mssql-server-is
```

## <a name="RHEL"></a> Install SSIS on RHEL
To install the `mssql-server-is` package on RHEL, follow these steps:

1. Download the Microsoft SQL Server Red Hat repository configuration file.

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/7/mssql-server-2017.repo
   ```

1. Run the following commands to install SQL Server Integration Services.

   ```bash
   sudo yum install -y mssql-server-is
   ```


1. After installation, run `ssis-conf`. For more info, see [Configure SSIS on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

   ```bash
   sudo /opt/ssis/bin/ssis-conf setup
   ```

1. Once the configuration is done, set path.

   ```bash
   export PATH=/opt/ssis/bin:$PATH
   ```

### Update SSIS
If you already have `mssql-server-is` installed, you can update to the latest version with the following command:

```bash
sudo yum update mssql-server-is
```

### Remove SSIS
To remove `mssql-server-is`, you can run following command:
```bash
sudo yum remove mssql-server-is
```

## Unattended installation
To run an unattended installation when you run `ssis-conf setup`, do the following things:
1.  Specify the `-n` (no prompt) option.
2.  Provide required values by setting environment variables.

The following example does the following things:
-   Installs SSIS.
-   Specifies the Developer edition by providing a value for the `SSIS_PID` environment variable.
-   Accepts the EULA by providing a value for the `ACCEPT_EULA` environment variable.
-   Runs an unattended installation by specifying the `-n` (no prompt) option.

```
sudo SSIS_PID=Developer ACCEPT_EULA=Y /opt/ssis/bin/ssis-conf -n setup 
```

### Environment variables for unattended installation

| Environment variable | Description |
|---|---|
| **ACCEPT_EULA** | Accepts the SQL Server license agreement when set to any value (for example, `Y`).|
| **SSIS_PID** | Sets the SQL Server edition or product key. Here are the possible values:<br/>Evaluation<br/>Developer<br/>Express <br/>Web <br/>Standard<br/>Enterprise <br/>A product key<br/><br/>If you specify a product key, the product key must be in the form `#####-#####-#####-#####-#####`, where `#` is a letter or a number.  |
| | |

## Next steps

To run SSIS packages on Linux, see [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md).

To configure additional SSIS settings on Linux, see [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

## Related content about SSIS on Linux
-   [Extract, transform, and load data on Linux with SSIS](sql-server-linux-migrate-ssis.md)
-   [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md)
-   [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md)
-   [Schedule SQL Server Integration Services package execution on Linux with cron](sql-server-linux-schedule-ssis-packages.md)
