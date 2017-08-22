---
title: Troubleshoot SQL Server on Linux | Microsoft Docs
description: Provides troubleshooting tips for using SQL Server 2017 on Linux.
author: annashres 
ms.author: anshrest 
manager: jhubbard
ms.date: 05/08/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 99636ee8-2ba6-4316-88e0-121988eebcf9S
---
# Troubleshoot SQL Server on Linux

[!INCLUDE[tsql-appliesto-sslinx-only_md](../../docs/includes/tsql-appliesto-sslinx-only_md.md)]

This document describes how to troubleshoot Microsoft SQL Server running on Linux or in a Docker container. When troubleshooting SQL Server on Linux, please make remember the limitations of this private preview release. You can find a list of these in the [Release Notes](sql-server-linux-release-notes.md).

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
   > One exception to this technique relates to Azure VMs. For Azure VMs, [find the public IP for the VM in the Azure portal](sql-server-linux-azure-virtual-machine.md#connect).

- If applicable, check that you have opened the SQL Server port (default 1433) on the firewall.

- For Azure VMs, check that you have a [network security group rule for the default SQL Server port](sql-server-linux-azure-virtual-machine.md#remote).

- Verify that the user name and password do not contain any typos or extra spaces or incorrect casing.

- Try to explicitly set the protocol and port number with the server name like the following: **tcp:servername,1433**.

- Network connectivity issues can also cause connection errors and timeouts. After verifying your connection information and network connectivity, try the connection again.

## Manage the SQL Server service

The following sections show how to start, stop, restart, and check the status of the SQL Server service. 

### Manage the mssql-server service in Red Hat Enterprise Linux (RHEL) and Ubuntu 

Check the status of the status of the SQL Server service using this command:

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

You can get the status and container ID of the latest created SQL Server Docker container by running the following command (The ID will be under the “CONTAINER ID” column):

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
   
The SQL Server engine logs to the /var/opt/mssql/log/errorlog file in both the Linux and Docker installations. You need to be in ‘superuser’ mode to browse this directory.

The installer logs here: /var/opt/mssql/setup-< time stamp representing time of install>
You can browse the errorlog files with any UTF-16 compatible tool like ‘vim’ or ‘cat’ like this: 

   ```bash
   sudo cat errorlog
   ```

If you prefer, you can also convert the files to UTF-8 to read them with ‘more’ or ‘less’ with the following command:
   
   ```bash
   sudo iconv –f UTF-16LE –t UTF-8 <errorlog> -o <output errorlog file>
   ```
## Extended events

Extended events can be queried via a SQL command.  More information about extended events can be found [here](https://technet.microsoft.com/en-us/library/bb630282.aspx):

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

## Common issues

1. You can not connect to your remote SQL Server instance.

   See the troubleshooting section of the topic, [Connect to SQL Server on Linux](#connection).

2. ERROR: Hostname must be 15 characters or less.

   This is a known-issue that happens whenever the name of the machine that is trying to install the SQL Server Debian package is longer than 15 characters. There are currently no workarounds other than changing the name of the machine. One way to achieve this is by editing the hostname file and rebooting the machine. The following [website guide](http://www.cyberciti.biz/faq/ubuntu-change-hostname-command/) explains this in detail.

3. Resetting the system administration (SA) password.

   If you have forgotten the system administrator (SA) password or need to reset it for some other reason please follow these steps.

   > [!NOTE]
   > Following these steps will stop the SQL Server service temporarily.

   Log into the host terminal, run the following commands and follow the prompts to reset the SA password:

   ```bash
   sudo systemctl stop mssql-server
   sudo /opt/mssql/bin/mssql-conf setup
   ```

4. Using special characters in password.

   If you use some characters in the SQL Server login password you may need to escape them when using them in the Linux terminal. You will need to escape the $ anytime using the backslash character you are using it in a terminal command/shell script:

   Does not work:

   ```bash
   sudo sqlcmd -S myserver -U sa -P Test$$
   ```

   Works:

   ```bash
   sqlcmd -S myserver -U sa -P Test\$\$
   ```

   Resources:
   [Special characters](http://tldp.org/LDP/abs/html/special-chars.html)
   [Escaping](http://tldp.org/LDP/abs/html/escapingsection.html)

## Support

Support is available through the community and monitored by the engineering team. For specific questions, use the following resources:

- [DBA Stack Exchange](https://dba.stackexchange.com/questions/tagged/sql-server): Ask database administration questions
- [Stack Overflow](http://stackoverflow.com/questions/tagged/sql-server): Ask development questions
- [MSDN Forums](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver): Ask technical questions
- [Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback): Report bugs and request feature
- [Reddit](https://www.reddit.com/r/SQLServer/): Discuss SQL Server