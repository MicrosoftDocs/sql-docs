---
title: How to Configure MSDTC on Linux
description: In this article, learn how to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
---
# How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article describes how to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux.

MSDTC on Linux is supported on [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update 16 and later versions.

## Overview

You enable distributed transactions on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux by introducing MSDTC and remote procedure call (RPC) endpoint mapper functionality within [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. By default, an RPC endpoint-mapping process listens on port 135 for incoming RPC requests and provides registered components information to remote requests. Remote requests can use the information returned by endpoint mapper to communicate with registered RPC components, such as MSDTC services.

A process requires superuser privileges to bind to well-known ports (port numbers less than 1024) on Linux. To avoid starting [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with root privileges for the RPC endpoint mapper process, system administrators must use **iptables** to create Network Address Translation to route traffic on port 135 to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance's RPC endpoint-mapping process.

MSDTC uses two configuration parameters for the **`mssql-conf`** utility:

| mssql-conf setting | Description |
| --- | --- |
| `network.rpcport` | The TCP port that the RPC endpoint mapper process binds to. |
| `distributedtransaction.servertcpport` | The port that the MSDTC server listens to. If not set, the MSDTC service uses a random ephemeral port on service restarts, and firewall exceptions need to be reconfigured to ensure that MSDTC service can continue communication. |

For more information about these settings and other related MSDTC settings, see [Configure SQL Server on Linux with the mssql-conf tool](mssql-conf.md).

## Supported transaction standards

The following MSDTC configurations are supported:

| Transaction standard | Data sources | ODBC driver | JDBC driver |
| --- | --- | --- | --- |
| **OLE-TX transactions** | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux | Yes | No |
| **XA distributed transactions** | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], other ODBC, and JDBC data sources that support XA | Yes (requires 17.3 or later versions) | Yes |
| **Distributed transactions on linked server** | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] | Yes | No |

For more information, see [Understanding XA Transactions](../../connect/jdbc/understanding-xa-transactions.md#configuration-instructions).

## MSDTC configuration steps

Complete the following three steps to configure MSDTC communication and functionality for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

- Use **`mssql-conf`** to configure `network.rpcport` and `distributedtransaction.servertcpport`.
- Configure the firewall to allow communication on `distributedtransaction.servertcpport` and port 135.
- Configure Linux server routing so that RPC communication on port 135 is redirected to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance's `network.rpcport`.

The following sections provide detailed instructions for each step.

## Configure RPC and MSDTC ports

Use **`mssql-conf`** to configure `network.rpcport` and `distributedtransaction.servertcpport`. This step applies to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and is common across all supported distributions.

1. Use **`mssql-conf`** to set the `network.rpcport` value. The following example sets it to 13500.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set network.rpcport 13500
   ```

1. Set the `distributedtransaction.servertcpport` value. The following example sets it to 51999.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set distributedtransaction.servertcpport 51999
   ```

1. Restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   ```bash
   sudo systemctl restart mssql-server
   ```

## Configure the firewall

Configure the firewall to allow communication on `distributedtransaction.servertcpport` and port 135. This step enables the RPC endpoint-mapping process and MSDTC process to communicate externally to other transaction managers and coordinators. The actual procedure varies depending on your Linux distribution and firewall.

### [Configure Ubuntu](#tab/ubuntu)

The following example shows how to create these rules on **Ubuntu**.

```bash
sudo ufw allow from any to any port 51999 proto tcp
sudo ufw allow from any to any port 135 proto tcp
sudo ufw allow from any to any port 13500 proto tcp
```

### [Configure Red Hat Enterprise Linux (RHEL)](#tab/rhel)

The following example shows how to create these rules on **RHEL**.

```bash
sudo firewall-cmd --zone=public --add-port=51999/tcp --permanent
sudo firewall-cmd --zone=public --add-port=135/tcp --permanent
sudo firewall-cmd --reload
```

---

It's important to configure the firewall before configuring port routing in the next section. Refreshing the firewall can clear the port routing rules in some cases.

## Configure port routing

Configure the Linux server routing table so that RPC communication on port 135 is redirected to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance's `network.rpcport`. The configuration mechanism for port forwarding might differ on different distributions. The following sections provide guidance for Ubuntu, SUSE Linux Enterprise Server (SLES), and Red Hat Enterprise Linux (RHEL).

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

### [Port routing in Ubuntu and SLES](#tab/ubuntu)

Ubuntu and SLES don't use the **firewalld** service, so **iptables** rules are an efficient mechanism to achieve port routing. The **iptables** rules might not persist during restarts, so the following commands also provide instructions for restoring the rules after a restart.

1. Create routing rules for port 135. In the following example, port 135 is directed to the RPC port, 13500, defined in the previous section. Replace `<ip>` with the IP address of your server.

   ```bash
   sudo iptables -t nat -A PREROUTING -d <ip> -p tcp --dport 135 -m addrtype --dst-type LOCAL  \
      -j DNAT --to-destination <ip>:13500 -m comment --comment RpcEndPointMapper
   sudo iptables -t nat -A OUTPUT -d <ip> -p tcp --dport 135 -m addrtype --dst-type LOCAL \
      -j DNAT --to-destination <ip>:13500 -m comment --comment RpcEndPointMapper
   ```

   The `--comment RpcEndPointMapper` parameter in the previous commands helps you manage these rules in later commands.

1. View the routing rules you created with the following command:

   ```bash
   sudo iptables -S -t nat | grep "RpcEndPointMapper"
   ```

1. Save the routing rules to a file on your machine.

   ```bash
   sudo iptables-save > /etc/iptables.conf
   ```

1. To reload the rules after a restart, add the following command to `/etc/rc.local` (for Ubuntu) or to `/etc/init.d/after.local` (for SLES):

   ```bash
   iptables-restore < /etc/iptables.conf
   ```

   > [!NOTE]  
   > You must have superuser (sudo) privileges to edit the `rc.local` or `after.local` files.

The `iptables-save` and `iptables-restore` commands, along with `rc.local`/`after.local` startup configuration, provide a basic mechanism to save and restore **iptables** entries. Depending on your Linux distribution, you might have more advanced or automated options available. For example, an Ubuntu alternative is the `iptables-persistent` package to make entries persistent.

The previous steps assume a fixed IP address. If the IP address for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance changes (due to manual intervention or DHCP), you must remove and recreate the routing rules if you created them with **iptables**. To recreate or delete existing routing rules, use the following command to remove old `RpcEndPointMapper` rules:

```bash
sudo iptables -S -t nat | grep "RpcEndPointMapper" | sed 's/^-A //' | while read rule; do sudo iptables -t nat -D $rule; done
```

### [Port routing in RHEL](#tab/rhel)

On distributions that use the **firewalld** service, such as Red Hat Enterprise Linux, you can use the same service for both opening the port on the server and internal port forwarding. For example, on Red Hat Enterprise Linux, use the **firewalld** service (via the **firewall-cmd** configuration utility with `--add-forward-port` or similar options) to create and manage persistent port forwarding rules instead of using **iptables**.

```bash
sudo firewall-cmd --permanent --add-forward-port=port=135:proto=tcp:toport=13500
sudo firewall-cmd --reload
```

---

## Verify

At this point, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] should be able to participate in distributed transactions. To verify that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is listening, run the **netstat** command. If you're using RHEL, you might have to first install the **net-tools** package:

```bash
sudo netstat -tulpn | grep sqlservr
```

The output looks similar to the following example:

```output
tcp 0 0 0.0.0.0:1433 0.0.0.0:* LISTEN 13911/sqlservr
tcp 0 0 127.0.0.1:1434 0.0.0.0:* LISTEN 13911/sqlservr
tcp 0 0 0.0.0.0:13500 0.0.0.0:* LISTEN 13911/sqlservr
tcp 0 0 0.0.0.0:51999 0.0.0.0:* LISTEN 13911/sqlservr
tcp6 0 0 :::1433 :::* LISTEN 13911/sqlservr
tcp6 0 0 ::1:1434 :::* LISTEN 13911/sqlservr
tcp6 0 0 :::13500 :::* LISTEN 13911/sqlservr
tcp6 0 0 :::51999 :::* LISTEN 13911/sqlservr
```

However, after a restart, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't start listening on the `distributedtransaction.servertcpport` until the first distributed transaction. In this case, you wouldn't see [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] listening on port 51999 in this example until the first distributed transaction.

## Configure authentication on RPC communication for MSDTC

MSDTC for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux doesn't use authentication on RPC communication by default. However, when the host machine is joined to an Active Directory domain, you can configure MSDTC to use authenticated RPC communication by using the following **`mssql-conf`** settings:

| Setting | Description |
| --- | --- |
| `distributedtransaction.allowonlysecurerpccalls` | Configure secure only RPC calls for distributed transactions. The default value is 0. |
| `distributedtransaction.fallbacktounsecurerpcifnecessary` | Configure security only RPC calls for distributed transactions. The default value is 0. |
| `distributedtransaction.turnoffrpcsecurity` | Enable or disable RPC security for distributed transactions. The default value is 0. |

## Supportability and compatibility

### Active Directory

Microsoft recommends using MSDTC with RPC enabled if [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is enrolled in an Active Directory configuration. If you configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use Active Directory authentication, MSDTC uses mutual authentication RPC security by default.

### Windows and Linux

If a client on a Windows operating system needs to enlist into distributed transaction with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, it must have the following minimum version of Windows operating system:

| Operating system | Minimum version | OS build |
| --- | --- | --- |
| [Windows Server](/windows-server/get-started/windows-server-release-info) | 1903 | 18362.30.190401-1528 |
| [Windows 10](/windows/release-information/) | 1903 | 18362.267 |

### Limitations

- Distributed transactions support only standalone SQL Server instances. They don't support high-availability clustering.

- MSDTC isn't supported with Always On availability groups on Linux, regardless of cluster type (`CLUSTER_TYPE = NONE` or `CLUSTER_TYPE = EXTERNAL`).

- Coordinating distributed transactions across availability group replicas isn't supported. Handling distributed transactions during availability group failover scenarios isn't supported.

- Clustered MSDTC configurations aren't supported on Linux.

## Related content

- [What is SQL Server on Linux?](../sql-server-linux-overview.md)
- [How to use distributed transactions with SQL Server Linux containers](../containers/configure-distributed-transactions.md)
