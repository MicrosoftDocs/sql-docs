---
title: How to use distributed transactions with SQL Server on Docker | Microsoft Docs
description: This article explains how to use Dprovides a walk-through for configuring MSDTC on Linux.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 09/25/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---

# How to use distributed transactions with SQL Server on Docker

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article explains how to set up SQL Server Linux containers on Docker for distributed transactions.

Starting with SQL Server 2019 preview, container images support the Microsoft Distributed Transaction Coordinator (MSDTC) required for distributed transactions. To understand the communications requirements for MSDTC, see [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md). This article explains the special requirements and scenarios related to SQL Server Docker containers.

## Configuration

To enable MSDTC transaction in containers for docker, you must set two new environment variables:

- **MSSQL_RPC_PORT**: the TCP port that RPC endpoint mapper service binds to and listens on.  
- **MSSQL_DTC_TCP_PORT**: the port that MSDTC service is configured to listen on.

### Pull and run

The following example shows how to use these environment variables to pull and run a single SQL Server container configured for MSDTC. This allows it to communicate with any application on any hosts.

```bash
docker run \
   -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' \
   -e 'MSSQL_RPC_PORT=135' -e 'MSSQL_DTC_TCP_PORT=51000' \
   -p 51433:1433 -p 135:135 -p 51000:51000  \
   -d mcr.microsoft.com/mssql/server:2019-CTP2.4-ubuntu
```

> [!IMPORTANT]
> The previous command only works for Docker running on Linux. For Docker on Windows, the Windows host already listens on port 135. You can remove the `-p 135:135` parameter for Docker on Windows, but it has a few limitations. The resulting container can then not be used for distributed transactions that involve the host; it can participate only in distributed transactions among docker containers on the host.

In this command, the **RPC Endpoint Mapper** service has been bound to port 135, and the **MSDTC** service has been bound to port 51000 within docker virtual network. SQL Server TDS communication occurs on port 1433 within docker virtual network. These ports have been externally exposed to host as TDS port 51433, RPC endpoint mapper port 135, and MSDTC port 51000.

> [!NOTE]
> The RPC Endpoint Mapper and MSDTC port do not have to be the same on the host and the container. So while RPC Endpoint Mapper port was configured to be 135 on container, it could potentially be mapped to port 13501 or any other available port on the host server.

## Configure the firewall

In order to communicate with and through the host, you must also configure the firewall on the host server for the containers. Open the firewall for all ports that docker container exposes for external communication. In the previous example, this would be ports 135, 51433, and 51000. These are the ports on the host itself and not the ports they map to in the container. So, if RPC Endpoint mapper port 51000 of container was mapped to host port 51001, then port 51001 (not 51000) should be opened in the firewall for communication with the host.  

The following example shows how to create these rules on Ubuntu.

```bash
sudo ufw allow from any to any port 51433 proto tcp
sudo ufw allow from any to any port 51000 proto tcp
sudo ufw allow from any to any port 135 proto tcp
```

The following example shows how this could be done on Red Hat Enterprise Linux (RHEL):

```bash
sudo firewall-cmd --zone=public --add-port=51999/tcp --permanent
sudo firewall-cmd --zone=public --add-port=51433/tcp --permanent
sudo firewall-cmd --zone=public --add-port=135/tcp --permanent
sudo firewall-cmd --reload
```

## Configure port routing on the host

In the previous example, because a single Docker container maps RPC port 135 to port 135 on the host, distributed transactions with the host should now work with no further configuration. Note that it is possible to directly use port 135 in the container, because SQL Server runs with elevated privileges in a container. For SQL Server outside of a container, a different ephemeral port must be used and traffic to port 135 must then be routed to that port.

However, if you did decide to map the container's port 135 to a different port on the host, such as 13500, then you have to configure port routing on the host. This enables the docker container to participate in distributed transactions with the host and with other external servers. For more information, see [Configure port routing](sql-server-linux-configure-msdtc.md#configure-port-routing).

## Next steps

For more information about MSDTC on Linux, see [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md).
