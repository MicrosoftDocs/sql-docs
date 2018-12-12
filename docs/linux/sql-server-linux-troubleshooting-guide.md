---
title: Troubleshoot SQL Server on Linux | Microsoft Docs
description: Provides troubleshooting tips for using SQL Server on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 05/01/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 99636ee8-2ba6-4316-88e0-121988eebcf9S
---
# Troubleshoot SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This document describes how to troubleshoot Microsoft SQL Server running on Linux or in a Docker container. When troubleshooting SQL Server on Linux, remember to review the supported features and known limitations in the [SQL Server on Linux Release Notes](sql-server-linux-release-notes.md).

> [!TIP]
> For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.md).

## <a id="connection"></a> Troubleshoot connection failures
If you are having difficulty connecting to your Linux SQL Server, there are a few things to check. 

- Verify that the server name or IP address is reachable from your client machine.

   > [!TIP]
   > To find the IP address of your Ubuntu machine, you can run the ifconfig command as in the following example:
   >
   >   ```bash
   >   sudo ifconfig eth0 | grep 'inet addr'
   >   ```
   > For Red Hat, you can use the ip addr as in the following example:
   >
   >   ```bash
   >   sudo ip addr show eth0 | grep "inet"
   >   ```
   > One exception to this technique relates to Azure VMs. For Azure VMs, [find the public IP for the VM in the Azure portal](https://docs.microsoft.com/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine#connect).

- If applicable, check that you have opened the SQL Server port (default 1433) on the firewall.

- For Azure VMs, check that you have a [network security group rule for the default SQL Server port](https://docs.microsoft.com/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine#remote).

- Verify that the user name and password do not contain any typos or extra spaces or incorrect casing.

- Try to explicitly set the protocol and port number with the server name like the following example: **tcp:servername,1433**.

- Network connectivity issues can also cause connection errors and timeouts. After verifying your connection information and network connectivity, try the connection again.

## Manage the SQL Server service

The following sections show how to start, stop, restart, and check the status of the SQL Server service. 

### Manage the mssql-server service in Red Hat Enterprise Linux (RHEL) and Ubuntu 

Check the status of the SQL Server service using this command:

   ```bash
   sudo systemctl status mssql-server
   ```

You can stop, start, or restart the SQL Server service as needed using the following commands:

   ```bash
   sudo systemctl stop mssql-server
   sudo systemctl start mssql-server
   sudo systemctl restart mssql-server
   ```

### Manage the execution of the mssql Docker container

You can get the status and container ID of the latest created SQL Server Docker container by running the following command (The ID is under the **CONTAINER ID** column):

   ```bash
   sudo docker ps -l
   ```
   
You can stop or restart the SQL Server service as needed using the following commands:
   
   ```bash
   sudo docker stop <container ID>
   sudo docker restart <container ID>
   ```

> [!TIP]
> For more troubleshooting tips for Docker, see [Troubleshooting SQL Server Docker containers](sql-server-linux-configure-docker.md#troubleshooting).

## Access the log files
   
The SQL Server engine logs to the /var/opt/mssql/log/errorlog file in both the Linux and Docker installations. You need to be in 'superuser' mode to browse this directory.

The installer logs here: /var/opt/mssql/setup-< time stamp representing time of install>
You can browse the errorlog files with any UTF-16 compatible tool like 'vim' or 'cat' like this: 

   ```bash
   sudo cat errorlog
   ```

If you prefer, you can also convert the files to UTF-8 to read them with 'more' or 'less' with the following command:
   
   ```bash
   sudo iconv -f UTF-16LE -t UTF-8 <errorlog> -o <output errorlog file>
   ```
## Extended events

Extended events can be queried via a SQL command.  More information about extended events can be found [here](https://technet.microsoft.com/library/bb630282.aspx):

## Crash dumps 

Look for dumps in the log directory in Linux. Check under the /var/opt/mssql/log directory for Linux Core dumps (.tar.gz2 extension) or SQL minidumps (.mdmp extension)

For Core dumps 
   ```bash
   sudo ls /var/opt/mssql/log | grep .tar.gz2 
   ```

For SQL dumps 
   ```bash
   sudo ls /var/opt/mssql/log | grep .mdmp 
   ```
   
## Start SQL Server in Minimal Configuration or in Single User Mode

### Start SQL Server in Minimal Configuration Mode
This is useful if the setting of a configuration value (for example, over-committing memory) has prevented the server from starting.
  
   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr -f
   ```

### Start SQL Server in Single User Mode
Under certain circumstances, you may have to start an instance of SQL Server in single-user mode by using the startup option -m. For example, you may want to change server configuration options or recover a damaged master database or other system database. For example, you may want to change server configuration options or recover a damaged master database or other system database   

Start SQL Server in Single User Mode
   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr -m
   ```

Start SQL Server in Single User Mode with SQLCMD
   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr -m SQLCMD
   ```
  
> [!WARNING]  
>  Start SQL Server on Linux with the "mssql" user to prevent future startup issues. Example "sudo -u mssql /opt/mssql/bin/sqlservr [STARTUP OPTIONS]" 

If you have accidentally started SQL Server with another user, you must change ownership of SQL Server database files back to the 'mssql' user prior to starting SQL Server with systemd. For example, to change ownership of all database files under /var/opt/mssql to the 'mssql' user, run the following command

   ```bash
   chown -R mssql:mssql /var/opt/mssql/
   ```

## Rebuild system databases
As a last resort, you can choose to rebuild the master and model databases back to default versions.

> [!WARNING]
> These steps will **DELETE all SQL Server system data** that you have configured! This includes information about your user databases (but not the user databases themselves). It will also delete other information stored in the system databases, including the following: master key information, any certs loaded in master, the SA Login password, job-related information from msdb, DB Mail information from msdb, and sp_configure options. Only use if you understand the implications!

1. Stop SQL Server.

   ```bash
   sudo systemctl stop mssql-server
   ```

1. Run **sqlservr** with the **force-setup** parameter. 

   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr --force-setup
   ```
   
   > [!WARNING]
   > See the previous warning! Also, you must run this as the **mssql** user as shown here.

1. After you see the message "Recovery is complete", press CTRL+C. This will shut down SQL Server

1. Reconfigure the SA password.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set-sa-password
   ```
   
1. Start SQL Server and reconfigure the server. This includes restoring or re-attaching any user databases.

   ```bash
   sudo systemctl start mssql-server
   ```

## Improve performance

There are many factors that affect performance, including database design, hardware, and workload demands. If you are looking to improve performance, start by reviewing the best practices in the article, [Performance best practices and configuration guidelines for SQL Server on Linux](sql-server-linux-performance-best-practices.md). Then explore some of the avilable tools for troubleshooting performance problems.

- [Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [System dynamic management views (DMVs)](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [Performance Dashboard in SQL Server Management Studio](https://blogs.msdn.microsoft.com/sql_server_team/new-in-ssms-performance-dashboard-built-in/)

## Common issues

1. You cannot connect to your remote SQL Server instance.

   See the troubleshooting section of the article, [Connect to SQL Server on Linux](#connection).

2. ERROR: Hostname must be 15 characters or less.

   This is a known-issue that happens whenever the name of the machine that is trying to install the SQL Server Debian package is longer than 15 characters. There are currently no workarounds other than changing the name of the machine. One way to achieve this is by editing the hostname file and rebooting the machine. The following [website guide](https://www.cyberciti.biz/faq/ubuntu-change-hostname-command/) explains this in detail.

3. Resetting the system administration (SA) password.

   If you have forgotten the system administrator (SA) password or need to reset it for some other reason, follow these steps.

   > [!NOTE]
   > The following steps stop the SQL Server service temporarily.

   Log into the host terminal, run the following commands and follow the prompts to reset the SA password:

   ```bash
   sudo systemctl stop mssql-server
   sudo /opt/mssql/bin/mssql-conf setup
   ```

4. Using special characters in password.

   If you use some characters in the SQL Server login password, you might need to escape them with a backslash when you use them in a Linux command in the terminal. For example, you must escape the dollar sign ($) anytime you use it in a terminal command/shell script:

   Does not work:

   ```bash
   sudo sqlcmd -S myserver -U sa -P Test$$
   ```

   Works:

   ```bash
   sqlcmd -S myserver -U sa -P Test\$\$
   ```

   Resources:
   [Special characters](https://tldp.org/LDP/abs/html/special-chars.html)
   [Escaping](https://tldp.org/LDP/abs/html/escapingsection.html)

[!INCLUDE[Get Help Options](../includes/paragraph-content/get-help-options.md)]
