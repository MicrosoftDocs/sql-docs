---
title: Distributed transactions (MSDTC) with SQL Server Containers
description: Learn to use the Microsoft Distributed Transaction Coordinator (MSDTC) for distributed transactions in a SQL Server container.
ms.custom: seo-lt-2019
author: VanMSFT 
ms.author: vanto
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
---

# How to use distributed transactions with SQL Server Containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to set up SQL Server Linux containers for distributed transactions.

SQL Server container images can use the Microsoft Distributed Transaction Coordinator (MSDTC), which is required for distributed transactions. To understand the communications requirements for MSDTC, see [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md). This article explains the special requirements and scenarios related to SQL Server containers.

> [!NOTE]
> SQL Server 2017 run in root containers by default, whereas SQL Server 2019 containers run as a non-root user.

## Configuration

To enable MSDTC transaction in SQL Server containers, you must set two new environment variables:

- **MSSQL_RPC_PORT**: the TCP port that RPC endpoint mapper service binds to and listens on.  
- **MSSQL_DTC_TCP_PORT**: the port that MSDTC service is configured to listen on.

### Pull and run

<!--SQL Server 2017 on Linux -->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

The following example shows how to use these environment variables to pull and run a single SQL Server 2017 container configured for MSDTC. This allows it to communicate with any application on any hosts.

```bash
docker run \
   -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' \
   -e 'MSSQL_RPC_PORT=135' -e 'MSSQL_DTC_TCP_PORT=51000' \
   -p 51433:1433 -p 135:135 -p 51000:51000  \
   -d mcr.microsoft.com/mssql/server:2017-latest
```

```PowerShell
docker run `
   -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong!Passw0rd>" `
   -e "MSSQL_RPC_PORT=135" -e "MSSQL_DTC_TCP_PORT=51000" `
   -p 51433:1433 -p 135:135 -p 51000:51000  `
   -d mcr.microsoft.com/mssql/server:2017-latest
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

The following example shows how to use these environment variables to pull and run a single SQL Server 2019 container configured for MSDTC. This allows it to communicate with any application on any hosts.

```bash
docker run \
   -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' \
   -e 'MSSQL_RPC_PORT=135' -e 'MSSQL_DTC_TCP_PORT=51000' \
   -p 51433:1433 -p 135:135 -p 51000:51000  \
   -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

```PowerShell
docker run `
   -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong!Passw0rd>" `
   -e "MSSQL_RPC_PORT=135" -e "MSSQL_DTC_TCP_PORT=51000" `
   -p 51433:1433 -p 135:135 -p 51000:51000  `
   -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

::: moniker-end

> [!IMPORTANT]

In this command, the **RPC Endpoint Mapper** service has been bound to port 135, and the **MSDTC** service has been bound to port 51000 within docker virtual network. SQL Server TDS communication occurs on port 1433 within docker virtual network. These ports have been externally exposed to host as TDS port 51433, RPC endpoint mapper port 135, and MSDTC port 51000.

> [!NOTE]
> The RPC Endpoint Mapper and MSDTC port do not have to be the same on the host and the container. So while RPC Endpoint Mapper port was configured to be 135 on container, it could potentially be mapped to port 13501 or any other available port on the host server.

## Configure the firewall

In order to communicate with and through the host, you must also configure the firewall on the host server for the containers. Open the firewall for all ports that SQL Server container exposes for external communication. In the previous example, this would be ports 135, 51433, and 51000. These are the ports on the host itself and not the ports they map to in the container. So, if RPC Endpoint mapper port 51000 of container was mapped to host port 51001, then port 51001 (not 51000) should be opened in the firewall for communication with the host.  

The following example shows how to create these rules on Ubuntu.

```bash
sudo ufw allow from any to any port 51433 proto tcp
sudo ufw allow from any to any port 51000 proto tcp
sudo ufw allow from any to any port 135 proto tcp
```

The following example shows how this could be done on Red Hat Enterprise Linux (RHEL):

```bash
sudo firewall-cmd --zone=public --add-port=51433/tcp --permanent
sudo firewall-cmd --zone=public --add-port=51000/tcp --permanent
sudo firewall-cmd --zone=public --add-port=135/tcp --permanent
sudo firewall-cmd --reload
```

## Configure port routing on the host

In the previous example, because a single SQL Server container maps RPC port 135 to port 135 on the host, distributed transactions with the host should now work with no further configuration. Note that it is possible to directly use port 135 in containers running as root, because SQL Server runs with elevated privileges in those containers. For SQL Server outside of a container or for non-root containers, a different ephemeral port, such as 13500, must be used in the container and traffic to port 135 must then be routed to that port. You would also need to configure port routing rules within the container from the container port 135 to the ephemeral port.

Also, if you decide to map the container's port 135 to a different port on the host, such as 13500, then you have to configure port routing on the host. This enables the SQL Server container to participate in distributed transactions with the host and with other external servers.

For more information about routing ports, see [Configure port routing](sql-server-linux-configure-msdtc.md#configure-port-routing).

## Deploy SQL Server containers with MSDTC configured on Kubernetes platform

If you are deploying SQL Server containers on a Kubernetes platform, please see the example yaml deployment manifest below. In this example, the kubernetes platform is Azure Kubernetes Service (AKS). Also, before running the sample deployment yaml script, please create the necessary secret to store the sa password. A typical command is as shown below

```bash
kubectl create secret generic mssql --from-literal=SA_PASSWORD="MyC0m9l&xP@ssw0rd"
```
You will notice the following points in the manifest file:

1) In the cluster, we create the following objects: StorageClass, two SQL Server pods deployed as statefulset deployments, and two load balancer services to connect to the respective SQL Server instances.
2) You will also notice that we have deployed the load balancer services with static IP addresses, which can be configured on Azure Kubernetes Service as documented [here](https://docs.microsoft.com/en-us/azure/aks/static-ip). Creating the load balancer services with static IP addresses ensures that the external IP address does not change if the load balancer service is deleted and recreated.
3) In the script below, you can see that we are using port 13500 for the MSSQL RPC PORT environment variable and port 51000 for the MSSQL DTC TCP PORT environment variable, both of which are required for MSDTC.
4) The port routing, i.e. routing port 135 to 13500, is configured in the load balancer script by appropriately configuring the port and targetPort as shown below:

```bash
kind: StorageClass
apiVersion: storage.k8s.io/v1
metadata:
     name: azure-disk
provisioner: kubernetes.io/azure-disk
parameters:
  storageaccounttype: Standard_LRS
  kind: Managed
---
apiVersion: apps/v1
kind: StatefulSet
metadata:
 name: mssql
 labels:
  app: mssql
spec:
 serviceName: "mssql"
 replicas: 2
 selector:
  matchLabels:
   app: mssql
 template:
  metadata:
   labels:
    app: mssql
  spec:
   securityContext:
     fsGroup: 10001
   containers:
   - name: mssql
     image: mcr.microsoft.com/mssql/server:2019-latest
     ports:
     - containerPort: 1433
       name: tcpsql
     - containerPort: 13500
       name: dtcport
     - containerPort: 51000
       name: dtctcpport
     env:
     - name: ACCEPT_EULA
       value: "Y"
     - name: MSSQL_ENABLE_HADR
       value: "1"
     - name: MSSQL_AGENT_ENABLED
       value: "1"
     - name: MSSQL_RPC_PORT
       value: "13500"
     - name: MSSQL_DTC_TCP_PORT
       value: "51000"
     - name: SA_PASSWORD
       valueFrom:
         secretKeyRef:
          name: mssql
          key: SA_PASSWORD
     volumeMounts:
     - name: mssql
       mountPath: "/var/opt/mssql"
 volumeClaimTemplates:
   - metadata:
      name: mssql
     spec:
      accessModes:
      - ReadWriteOnce
      resources:
       requests:
        storage: 8Gi
---
apiVersion: v1
kind: Service
metadata:
  name: mssql-0
spec:
  type: LoadBalancer
  loadBalancerIP: 40.88.213.209
  selector:
    statefulset.kubernetes.io/pod-name: mssql-0
  ports:
  - protocol: TCP
    port: 1433
    targetPort: 1433
    name: tcpsql
  - protocol: TCP
    port: 51000
    targetPort: 51000
    name: dtctcpport
  - protocol: TCP
    port: 135 
    targetPort: 13500
    name: nonrootport
---
apiVersion: v1
kind: Service
metadata:
  name: mssql-1
spec:
  type: LoadBalancer
  loadBalancerIP: 20.72.137.129
  selector:
    statefulset.kubernetes.io/pod-name: mssql-1
  ports:
  - protocol: TCP
    port: 1433
    targetPort: 1433
    name: tcpsql
  - protocol: TCP
    port: 51000
    targetPort: 51000
    name: dtctcpport
  - protocol: TCP
    port: 135
    targetPort: 13500
    name: nonrootport
```

When you run the ```kubectl get all``` command after the above deployment to see all the resources created, assuming you created the resource in the default namespace, you should see the output shown below.

```bash
C:\>kubectl get all
NAME          READY   STATUS    RESTARTS   AGE
pod/mssql-0   1/1     Running   0          4d22h
pod/mssql-1   1/1     Running   0          4d22h

NAME                 TYPE           CLUSTER-IP    EXTERNAL-IP     PORT(S)                                        AGE
service/kubernetes   ClusterIP      10.0.0.1      <none>          443/TCP                                        6d6h
service/mssql-0      LoadBalancer   10.0.18.186   40.88.213.209   1433:31875/TCP,51000:31219/TCP,135:30044/TCP   2d6h
service/mssql-1      LoadBalancer   10.0.16.180   20.72.137.129   1433:30353/TCP,51000:32734/TCP,135:31239/TCP   2d6h

NAME                     READY   AGE
statefulset.apps/mssql   2/2     5d1h
```
Now you can use tools like SSMS to connect to either of the above two SQL Servers and run a sample DTC transaction. In this case, I am connecting to mssql-1 (20.72.137.129) and creating the linked server to mssql-0 (40.88.213.209) to run the distributed transaction, as shown below. 

```t-sql
USE [master]
GO
EXEC master.dbo.sp_addlinkedserver @server = N'40.88.213.209', @srvproduct=N'SQL Server' ;
GO

EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname = N'40.88.213.209', @rmtuser = 'sa', @rmtpassword = 'xxxx', @useself = N'False';
GO

# start the distributed transaction and this should show you the sysprocesses from the mssql-1 instance
set xact_abort on
begin distributed transaction
select * from [40.88.213.209].master.dbo.sysprocesses
commit
Go
```

## Next steps

For more information about MSDTC on Linux, see [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md).
