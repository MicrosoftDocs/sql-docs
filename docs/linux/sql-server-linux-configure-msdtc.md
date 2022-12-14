---
title: How to configure MSDTC on Linux
description: In this article, learn how to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux.
author: VanMSFT 
ms.author: vanto
ms.date: 08/12/2020
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
---
# How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux.

> [!NOTE]
> MSDTC on Linux is supported on SQL Server 2017 starting with cumulative update 16.

## Overview

Distributed transactions are enabled on SQL Server on Linux by introducing MSDTC and RPC endpoint mapper functionality within SQL Server. By default, an RPC endpoint-mapping process listens on port 135 for incoming RPC requests and provides registered components information to remote requests. Remote requests can use the information returned by endpoint mapper to communicate with registered RPC components, such as MSDTC services. A process requires super user privileges to bind to well-known ports (port numbers less than 1024) on Linux. To avoid starting SQL Server with root privileges for the RPC endpoint mapper process, system administrators must use iptables to create Network Address Translation to route traffic on port 135 to SQL Server's RPC endpoint-mapping process.

MSDTC uses two configuration parameters for the mssql-conf utility:

| mssql-conf setting | Description |
|---|---|
| **network.rpcport** | The TCP port that the RPC endpoint mapper process binds to. |
| **distributedtransaction.servertcpport** | The port that the MSDTC server listens to. If not set, the MSDTC service uses a random ephemeral port on service restarts, and firewall exceptions will need to be reconfigured to ensure that MSDTC service can continue communication. |

For more information about these settings and other related MSDTC settings, see [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

## Supported transaction standards

The following MSDTC configurations are supported:

| Transaction standard | Data sources | ODBC driver | JDBC driver|
|---|---|---|---|
| OLE-TX Transactions | SQL Server on Linux | Yes | No|
| XA Distributed transactions | SQL Server, other ODBC, and JDBC data sources that support XA | Yes (requires version 17.3 or higher) | Yes |
| Distributed transactions on Linked server | SQL Server | Yes | No

For more information, see [Understanding XA Transactions](../connect/jdbc/understanding-xa-transactions.md#configuration-instructions).

## MSDTC configuration steps

There are three steps to configure MSDTC communication and functionality. If the necessary configuration steps aren't done, SQL Server will not enable MSDTC functionality.

- Configure **network.rpcport** and **distributedtransaction.servertcpport** using mssql-conf.
- Configure the firewall to allow communication on **distributedtransaction.servertcpport** and port 135.
- Configure Linux server routing so that RPC communication on port 135 is redirected to SQL Server's **network.rpcport**.

The following sections provide detailed instructions for each step.

## Configure RPC and MSDTC ports

First, configure **network.rpcport** and **distributedtransaction.servertcpport** using mssql-conf. This step if specific to SQL Server and common across all supported distributions.

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

## Configure the firewall

The second step is to configure the firewall to allow communication on **servertcpport** and port 135.  This enables the RPC endpoint-mapping process and MSDTC process to communicate externally to other transaction managers and coordinators. The actual steps for this will vary depending on your Linux distribution and firewall. 

The following example shows how to create these rules on **Ubuntu**.

```bash
sudo ufw allow from any to any port 51999 proto tcp
sudo ufw allow from any to any port 135 proto tcp
sudo ufw allow from any to any port 13500 proto tcp
```

The following example shows how this could be done on **Red Hat Enterprise Linux (RHEL)**:

```bash
sudo firewall-cmd --zone=public --add-port=51999/tcp --permanent
sudo firewall-cmd --zone=public --add-port=135/tcp --permanent
sudo firewall-cmd --reload
```

It is important to configure the firewall before configuring port routing in the next section. Refreshing the firewall can clear the port routing rules in some cases.

## Configure port routing

Configure the Linux server routing table so that RPC communication on port 135 is redirected to SQL Server's **network.rpcport**. Configuration mechanism for port forwarding on different distribution may differ. The following sections provide guidance for Ubuntu, SUS Enterprise Linux (SLES), and Red Hat Enterprise Linux (RHEL).

### Port routing in Ubuntu and SLES

Ubuntu and SLES do not use the **firewalld** service, so **iptable** rules are an efficient mechanism to achieve port routing. The **iptable** rules may not persist during reboots, so the following commands also provide instructions for restoring the rules after a reboot.

1. Create routing rules for port 135. In the following example, port 135 is directed to the RPC port, 13500, defined in the previous section. Replace `<ipaddress>` with the IP address of your server.

   ```bash
   sudo iptables -t nat -A PREROUTING -d <ip> -p tcp --dport 135 -m addrtype --dst-type LOCAL  \
      -j DNAT --to-destination <ip>:13500 -m comment --comment RpcEndPointMapper
   sudo iptables -t nat -A OUTPUT -d <ip> -p tcp --dport 135 -m addrtype --dst-type LOCAL \
      -j DNAT --to-destination <ip>:13500 -m comment --comment RpcEndPointMapper
   ```

   The `--comment RpcEndPointMapper` parameter in the previous commands assists with managing these rules in later commands.

2. View the routing rules you created with the following command:

   ```bash
   sudo iptables -S -t nat | grep "RpcEndPointMapper"
   ```

3. Save the routing rules to a file on your machine.

   ```bash
   sudo iptables-save > /etc/iptables.conf
   ```

4. To reload the rules after a reboot, add the following command to `/etc/rc.local` (for Ubuntu) or to `/etc/init.d/after.local` (for SLES):

   ```bash
   iptables-restore < /etc/iptables.conf
   ```

   > [!NOTE]
   > You must have super user (sudo) privileges to edit the **rc.local** or **after.local** files.

The **iptables-save** and **iptables-restore** commands, along with `rc.local`/`after.local` startup configuration, provide a basic mechanism to save and restore iptables entries. Depending on your Linux distribution, there might be more advanced or automated options available. For example, an Ubuntu alternative is the **iptables-persistent** package to make entries persistent.

> [!IMPORTANT]
> The previous steps assume a fixed IP address. If the IP address for your SQL Server instance changes (due to manual intervention or DHCP), you must remove and recreate the routing rules if they were created with iptables. If you need to recreate or delete existing routing rules, you can use the following command to remove old `RpcEndPointMapper` rules:
> 
> ```bash
> sudo iptables -S -t nat | grep "RpcEndPointMapper" | sed 's/^-A //' | while read rule; do iptables -t nat -D $rule; done
> ```

### Port routing in RHEL

On distributions that use **firewalld** service, such as Red Hat Enterprise Linux, the same service can be used for both opening the port on the server and internal port forwarding. For example, on Red Hat Enterprise Linux, you should use **firewalld** service (via **firewall-cmd** configuration utility with `-add-forward-port` or similar options) to create and manage persistent port forwarding rules instead of using iptables.

```bash
sudo firewall-cmd --permanent --add-forward-port=port=135:proto=tcp:toport=13500
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

## Configure authentication on RPC communication for MSDTC

MSDTC for SQL Server on Linux does not use authentication on RPC communication by default. However, when the host machine is joined to an Active Directory domain, it is possible to configure MSDTC to use authenticated RPC communication using following **mssql-conf** settings:

| Setting | Description |
|---|---|
| **distributedtransaction.allowonlysecurerpccalls**          | Configure secure only RPC calls for distributed transactions. Default value is 0. |
| **distributedtransaction.fallbacktounsecurerpcifnecessary** | Configure security only RPC calls for distributed transactions. Default value is 0. |
| **distributedtransaction.turnoffrpcsecurity**               | Enable or disable RPC security for distributed transactions. Default value is 0. |

## Additional guidance

### Active directory

Microsoft recommends using MSDTC with RPC enabled if SQL Server is enrolled into an Active Directory configuration. If SQL Server is configured to use Active Directory authentication, MSDTC uses mutual authentication RPC security by default.

### Windows and Linux

If a client on a Windows operating system needs to enlist into distributed transaction with SQL Server on Linux, it must have the following minimum version of Windows operating system:

| Operating system | Minimum version | OS Build |
|---|---|---|
| [Windows Server](/windows-server/get-started/windows-server-release-info) | 1903 | 18362.30.190401-1528 |
| [Windows 10](/windows/release-information/) | 1903 | 18362.267 |

## Next steps

For more information about SQL Server on Linux, see [SQL Server on Linux](sql-server-linux-overview.md).
