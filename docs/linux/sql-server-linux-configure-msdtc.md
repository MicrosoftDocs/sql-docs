---
title: How to configure MSDTC on Linux | Microsoft Docs
description: This article provides a walk-through for configuring MSDTC on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article describes how to configure the Microsoft Distributed Transaction Coordinator (MSTDC) on Linux. MSDTC support on Linux was introduced in SQL Server 2019 CTP 2.0.

## Overview

Distributed transactions are enabled on SQL Server on Linux by introducing MSDTC and RPC endpoint mapper functionality within SQL Server. By default, an RPC endpoint mapping process listens on port 135 for incoming RPC requests and routes that to appropriate components (such as the MSDTC service). A process requires super user privilege to bind to system ports (port numbers less than 1024) on Linux. To avoid starting SQL Server with root privileges for the RPC endpoint mapper process, system administrators must use iptables to create NAT translation to route traffic on port 135 to SQL Server's RPC endpoint mapping process.

SQL Server 2019 introduces two configuration parameters for the mssql-conf utility.

| mssql-conf setting | Description |
|---|---|
| **network.rpcport** | The RPC port that the RPC endpoint manager process binds to. |
| **network.servertcpport** | The port that the MSDTC server listens to. |

For more information about these settings and other related MSDTC settings, see [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md#msdtc).

## Supported MSDTC configurations

The following MSDTC configurations are supported:

- OLE-TX Distributed transactions against SQL Server on Linux for JDBC providers.
- XA Distributed transactions against SQL Server on Linux using JDBC providers.
- Distributed transactions on Linked server.

For limitations and known issues for MSDTC in CTP 2.0, see [Release notes for SQL Server 2019 CTP on Linux](sql-server-linux-release-notes-2019.md#msdtc).

## MSDTC configuration steps

There are three steps to configure MSDTC communication and functionality. If the necessary configuration steps are not done, SQL Server will not enable MSDTC functionality.

- Configure **network.rpcport** and **distributedtransaction.servertcpport** using mssql-conf.
- Configure Linux server routing so that RPC communication on port 135 is redirected to SQL Server's **network.rpcport**.
- Configure the firewall to allow communication on both **rpcport** and **servertcpport** so the RPC endpoint mapping process and MSDTC process can communicate externally to other transaction managers and coordinators.

The following sections provide detailed instructions for each step.

## Configure RPC and MSDTC ports

First, configure **network.rpcport** and **distributedtransaction.servertcpport** using mssql-conf.

1. Use mssql-conf to set the **network.rpcport** value. The following example sets it to 13500.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set network.rpcport 13500
   ```

2. Set the **distributedtransaction.servertcpport** value. The following example sets it to 51999.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set distributedtransaction.servertcpport 51999
   ```

3. Restart SQL Server.

   ```bash
   sudo systemctl restart mssql-server
   ```

## Configure port routing

Configure the Linux server routing table so that RPC communication on port 135 is redirected to SQL Server's **network.rpcport**. The iptable rules may not persist during reboots, so the following commands also provide instructions for restoring the rules after a reboot.

1. Create routing rules for port 135. In the following example, port 135 is directed to the RPC port, 13500, defined in the previous section. Replace `<ipaddress>` with the IP address of your server.

   ```bash
   iptables -t nat -A PREROUTING -d <ip> -p tcp --dport 135 -m addrtype --dst-type LOCAL  \
      -j DNAT --to-destination <ip>:13500 -m comment --comment RpcEndPointMapper
   iptables -t nat -A OUTPUT -d <ip> -p tcp --dport 135 -m addrtype --dst-type LOCAL \
      -j DNAT --to-destination <ip>:13500 -m comment --comment RpcEndPointMapper
   ```

   The `--comment RpcEndPointMapper` parameter in the previous commands assists with managing these rules in later commands.

1. View the routing rules you created with the following command:

   ```bash
   iptables -S -t nat | grep "RpcEndPointMapper"
   ```

1. Save the routing rules to a file on your machine.

   ```bash
   iptables-save > /etc/iptables.conf
   ```

1. Add the following command to `/etc/rc.local` to reload the rules after a reboot.

   ```bash
   iptables-restore < /etc/iptables.conf
   ```

> [!IMPORTANT]
> The previous steps assume a fixed IP address. If the IP address for your SQL Server instance changes (due to manual intervention or DHCP), you must remove and recreate the routing rules. If you need to recreate or delete existing routing rules, you can use the following command to remove old `RpcEndPointMapper` rules:
> 
> ```bash
> iptables -S -t nat | grep "RpcEndPointMapper" | sed 's/^-A //' | while read rule; do iptables -t nat -D $rule; done
> ```

## Configure the firewall

The final step is to configure the firewall to allow communication on the **rpcport**, the **servertcpport**, and port 135. The actual steps for this will vary depending on your Linux distribution and firewall. The following example shows how to create these rules on Ubuntu.

```bash
sudo ufw allow from any to any port 13500 proto tcp
sudo ufw allow from any to any port 51999 proto tcp
sudo ufw allow from any to any port 135 proto tcp
```

The following example shows how this could be done on Red Hat Enterprise Linux (RHEL):

```bash
sudo firewall-cmd --zone=public --add-port=51999/tcp --permanent
sudo firewall-cmd --zone=public --add-port=13500/tcp --permanent
sudo firewall-cmd --zone=public --add-port=135/tcp --permanent
sudo firewall-cmd --reload
```

## Verify

At this point, SQL Server should be able to participate in distributed transactions. To verify that SQL Server is listening, run the **netstat** command (if you are using RHEL, you might have to first install the **net-tools** package):

```bash
sudo netstat -tulpn | grep sqlservr
```

You should see output similar to the following:

```bash
tcp 0 0 0.0.0.0:1433 0.0.0.0:* LISTEN 13911/sqlservr
tcp 0 0 127.0.0.1:1434 0.0.0.0:* LISTEN 13911/sqlservr
tcp 0 0 0.0.0.0:13500 0.0.0.0:* LISTEN 13911/sqlservr
tcp 0 0 0.0.0.0:51999 0.0.0.0:* LISTEN 13911/sqlservr
tcp6 0 0 :::1433 :::* LISTEN 13911/sqlservr
tcp6 0 0 ::1:1434 :::* LISTEN 13911/sqlservr
tcp6 0 0 :::13500 :::* LISTEN 13911/sqlservr
tcp6 0 0 :::51999 :::* LISTEN 13911/sqlservr
```

However, after a restart, SQL Server does not start listening on the **servertcpport** until the first distributed transaction. In this case, you would not see SQL Server listening on port 51999 in this example until the first distributed transaction.

## Next steps

For more information about SQL Server on Linux, see [SQL Server on Linux](sql-server-linux-overview.md).
