---
title: Customer Feedback for SQL Server on Linux | Microsoft Docs
description: Describes how SQL Server customer feedback is collected and configured on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 06/22/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
---
# Customer Feedback for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

By default, Microsoft SQL Server collects information about how its customers are using the application. Specifically, SQL Server collects information about the installation experience, usage, and performance. This information helps Microsoft improve the product to better meet customer needs. For example, Microsoft collects information about what kinds of error codes customers encounter so that we can fix related bugs, improve our documentation about how to use SQL Server, and determine whether features should be added to the product to better serve customers.

This document provides details about what kinds of information are collected and about how to configure Microsoft SQL Server on Linux to send that collected information to Microsoft. SQL Server 2017 includes a privacy statement that explains what information we do and do not collect from users. For more information, see the [privacy statement](https://go.microsoft.com/fwlink/?LinkID=868444).

Specifically, Microsoft does not send any of the following types of information through this mechanism:

- Any values from inside user tables
- Any logon credentials or other authentication information
- Personally Identifiable Information (PII)

SQL Server 2017 always collects and sends information about the installation experience from the setup process so that we can quickly find and fix any installation problems that the customer is experiencing. SQL Server 2017 can be configured not to send information (on a per-server instance basis) to Microsoft through **mssql-conf**. mssql-conf is a configuration script that installs with SQL Server 2017 for Red Hat Enterprise Linux, SUSE Linux Enterprise Server, and Ubuntu.

> [!NOTE]
> You can disable the sending of information to Microsoft only in paid versions of SQL Server.

## Disable Customer Feedback

This option lets you change if SQL Server sends feedback to Microsoft or not. By default, this value is set to true. To change the value, run the following commands:

> [!IMPORTANT]
> You can not turn off customer feedback for free editions of SQL Server, Express and Developer.

### On Red Hat, SUSE, and Ubuntu

1. Run the mssql-conf script as root with the **set** command for **telemetry.customerfeedback**. The following example turns off customer feedback by specifying **false**.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set telemetry.customerfeedback false
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```
   
### On Docker
To disable Customer Feedback on docker, you must have Docker [persist your data](sql-server-linux-configure-docker.md). 

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Add an `mssql.conf` file with the lines `[telemetry]` and `customerfeedback = false` in the host directory:
 
   ```bash
   echo '[telemetry]' >> <host directory>/mssql.conf
   ```

   ```bash
   echo 'customerfeedback = false' >> <host directory>/mssql.conf
   ```

2. Run the container image

   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
   ```

   ```PowerShell
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
   ```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

1. Add an `mssql.conf` file with the lines `[telemetry]` and `customerfeedback = false` in the host directory:

   ```bash
   echo '[telemetry]' >> <host directory>/mssql.conf
   ```

   ```bash
   echo 'customerfeedback = false' >> <host directory>/mssql.conf
   ```

2. Run the container image

   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:vNext-CTP2.0-ubuntu
   ```

   ```PowerShell
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:vNext-CTP2.0-ubuntu
   ```

::: moniker-end

## Local Audit for SQL Server on Linux Usage Feedback Collection

Microsoft SQL Server 2017 contains Internet-enabled features that can collect and send information about your computer or device ("standard computer information") to Microsoft. The Local Audit component of SQL Server Usage Feedback collection can write data collected by the service to a designated folder, representing the data (logs) that will be sent to Microsoft. The purpose of the Local Audit is to allow customers to see all data Microsoft collects with this feature, for compliance, regulatory or privacy validation reasons.

In SQL Server on Linux, Local Audit is configurable at instance level for SQL Server Database Engine. Other SQL Server components and SQL Server Tools do not have Local Audit capability for usage feedback collection.

### Enable Local Audit

This option enables Local Audit and lets you set the directory where the Local Audit logs are created.

1. Create a target directory for new Local Audit logs. The following example creates a new **/tmp/audit** directory:

   ```bash
   sudo mkdir /tmp/audit
   ```

2. Change the owner and group of the directory to the **mssql** user:

   ```bash
   sudo chown mssql /tmp/audit
   sudo chgrp mssql /tmp/audit
   ```

3. Run the mssql-conf script as root with the **set** command for **telemetry.userrequestedlocalauditdirectory**:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set telemetry.userrequestedlocalauditdirectory /tmp/audit
   ```

4. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```
   
### On Docker
To enable Local Audit on docker, you must have Docker [persist your data](sql-server-linux-configure-docker.md). 

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. The target directory for new Local Audit logs will be in the container. Create a target directory for new Local Audit logs in the host directory on your machine. The following example creates a new **/audit** directory:

   ```bash
   sudo mkdir <host directory>/audit
   ```

1. Add an `mssql.conf` file with the lines `[telemetry]` and `userrequestedlocalauditdirectory = <host directory>/audit` in the host directory:
 
   ```bash
   echo '[telemetry]' >> <host directory>/mssql.conf
   ```

   ```bash
   echo 'userrequestedlocalauditdirectory = <host directory>/audit' >> <host directory>/mssql.conf
   ```

1. Run the container image

   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
   ```

   ```PowerShell
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
   ```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

1. The target directory for new Local Audit logs will be in the container. Create a target directory for new Local Audit logs in the host directory on your machine. The following example creates a new **/audit** directory:

   ```bash
   sudo mkdir <host directory>/audit
   ```

1. Add an `mssql.conf` file with the lines `[telemetry]` and `userrequestedlocalauditdirectory = <host directory>/audit` in the host directory:
 
   ```bash
   echo '[telemetry]' >> <host directory>/mssql.conf
   ```

   ```bash
   echo 'userrequestedlocalauditdirectory = <host directory>/audit' >> <host directory>/mssql.conf
   ```

1. Run the container image

   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:vNext-CTP2.0-ubuntu
   ```

   ```PowerShell
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>" -p 1433:1433 -v <host directory>:/var/opt/mssql -d mcr.microsoft.com/mssql/server:vNext-CTP2.0-ubuntu
   ```

::: moniker-end

## Next steps

For more information about SQL Server on Linux, see the [Overview of SQL Server on Linux](sql-server-linux-overview.md).
