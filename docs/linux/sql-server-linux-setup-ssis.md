---
# required metadata

title: Install SQL Server Integration Services on Linux | Microsoft Docs
description: This topic describes how to install SQL Server Integration Services on Linux.
author: leolimsft 
ms.author: lle 
manager: craigg
ms.date: 06/22/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: integration-services
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
# Installing SQL Server Integration Services (SSIS) on Linux


Follow the steps in this article to install SQL Server Integration Services (`mssql-server-is`) on Linux. For info about the features supported in this release of Integration Services for Linux, see the [Release Notes](sql-server-linux-release-notes.md).


At this time, Integration Services on Linux is supported only on the Ubuntu platform.


## <a name="ubuntu"></a> Install SSIS on Ubuntu
To install the `mssql-server-is` package on Ubuntu, follow these steps:


1.  Import the public repository GPG keys.

    ```bash
    curl httpspackages.microsoft.comkeysmicrosoft.asc  sudo apt-key add –
    ```


2.  Register the Microsoft SQL Server Ubuntu repository.

    ```bash
    curl httpspackages.microsoft.comconfigubuntu16.04mssql-server.list sudo tee etcaptsources.list.dmssql-server.list
    ```


3.  Run the following commands to install SQL Server Integration Services.

    ```bash
    sudo apt-get update
    sudo apt-get install -y mssql-server-is
    ```


4.  After installing Integration Services, run `ssis-conf`.

    ```bash
    sudo /opt/ssis/bin/ssis-conf
    ```


5.  After the configuration is done, set the path.

    ```bash
    export PATH=/opt/ssis/bin:$PATH
    ```


6.  If you're not in the SSIS group, add the current user to the SSIS group. 

    ```bash
    sudo gpasswd -a “current user” ssis
    ```

      Use the `ID` command to make sure the current user is in the SSIS group.

    ```bash
    id
    ```

## Update SSIS
If you already have `mssql-server-is` installed, you can update to the latest version with the following command:

```bash
sudo apt-get install mssql-server-is
```

## Run a package
Copy the SSIS package to the Linux computer. Then use the following command to run the package.

```bash
dtexec F <package name> DE <protection password>
```

## Next steps

For more information about how to use SSIS on Linux to extract, transform, and load data, see [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md).