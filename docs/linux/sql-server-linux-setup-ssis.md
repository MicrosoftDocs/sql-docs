---
title: Install SQL Server Integration Services on Linux | Microsoft Docs
description: This article describes how to install SQL Server Integration Services (SSIS) on Linux.
author: leolimsft 
ms.author: lle 
ms.reviewer: douglasl
manager: craigg
ms.date: 10/02/2017
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: sql-linux
ms.suite: "sql"
ms.custom: ""
ms.technology: database-engine
ms.workload: "On Demand"
---
# Install SQL Server Integration Services (SSIS) on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

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
sudo apt-get remove msssql-server-is
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

## Next steps

To run SSIS packages on Linux, see [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md).

To configure additional SSIS settings on Linux, see [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md).
