---
title: Install SQL Server Integration Services on Linux | Microsoft Docs
description: This topic describes how to install SQL Server Integration Services on Linux.
author: leolimsft 
ms.author: lle 
manager: craigg
ms.date: 07/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: integration-services
ms.assetid: 
---
# Install SQL Server Integration Services (SSIS) on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../../docs/includes/tsql-appliesto-sslinux-only.md)]

Follow the steps in this article to install SQL Server Integration Services (`mssql-server-is`) on Linux. For info about the features supported in this release of Integration Services for Linux, see the [Release Notes](sql-server-linux-release-notes.md).

Install SQL Server Integration Servers for your platform:

- [Ubuntu](#ubuntu)
- [Red Hat Enterprise Linux](#RHEL)



## <a name="ubuntu"></a> Install SSIS on Ubuntu
To install the `mssql-server-is` package on Ubuntu, follow these steps:


1.  Import the public repository GPG keys.

    ```bash
    curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add –
    ```


2.  Register the Microsoft SQL Server Ubuntu repository.

    ```bash
    curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server.list | sudo tee /etc/apt/sources.list.d/mssql-server.list
    ```


3.  Run the following commands to install SQL Server Integration Services.

    ```bash
    sudo apt-get update
    sudo apt-get install -y mssql-server-is
    ```


4.  After installing Integration Services, run `ssis-conf`.

    ```bash
    sudo /opt/ssis/bin/ssis-conf setup
    ```


5.  After the configuration is done, set the path.

    ```bash
    export PATH=/opt/ssis/bin:$PATH
    ```


If you already have `mssql-server-is` installed, you can update to the latest version with the following command:

```bash
sudo apt-get install mssql-server-is
```


To remove `mssql-server-is`, you can run following command:
```bash
sudo apt-get remove msssql-server-is
```



## <a name="RHEL"></a> Install SSIS on RHEL
To install the `mssql-server-is` package on RHEL, follow these steps:


1.  Enter superuser mode.

    ```bash
    sudo su
    ```


2.  Download the Microsoft SQL Server Red Hat repository configuration file.

    ```bash
    curl https://packages.microsoft.com/config/rhel/7/mssql-server.repo > /etc/yum.repos.d/mssql-server.repo
    ```


3.  Exit superuser mode.

    ```bash
    exit
    ```


4.  Run the following commands to install SQL Server Integration Services.

    ```bash
    sudo yum install -y mssql-server-is
    ```


5.  After installation, please run `ssis-conf`.

    ```bash
    sudo /opt/ssis/bin/ssis-conf setup
    ```


6.  Once the configuration is done, set path.

    ```bash
    export PATH=/opt/ssis/bin:$PATH
    ```


If you already have `mssql-server-is` installed, you can update to the latest version with the following command:

```bash
sudo yum update mssql-server-is
```


To remove `mssql-server-is`, you can run following command:
```bash
sudo yum remove msssql-server-is
```




## Run a package
Copy the SSIS package to the Linux computer. Then use the following command to run the package.

```bash
dtexec /F <package name> /DE <protection password>
```



## Next steps

For more information about how to use SSIS on Linux to extract, transform, and load data, see [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md).