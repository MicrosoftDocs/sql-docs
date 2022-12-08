---
title: "AD mode deployment becomes unresponsive: sparkhead pods"
titleSuffix: SQL Server Big Data Cluster
description: Troubleshooting unresponsive deployment of a SQL Server Big Data Cluster in an Active Directory domain with unhealthy sparkhead pods.
author: macarv-ms
ms.author: macarv
ms.reviewer: wiassaf
ms.date: 06/17/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: troubleshooting
ms.custom: kr2b-contr-experiment
---

# Active Directory mode deployment becomes unresponsive: sparkhead pods

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Deployment in Active Directory (AD) mode freezes. Check symptoms to see if the cause is a missing reverse lookup zone entry for the domain controller on the different networks of the cluster nodes.

## Symptom

You started deploying SQL Server Big Data Clusters with AD mode. The deployment is stuck and not moving forward.

The following example shows the deployment results in a bash shell.

```output
Starting cluster deployment.
Waiting for cluster controller to start.
Waiting for cluster controller to start.
Waiting for cluster controller to start.
Waiting for cluster controller to start.
Waiting for cluster controller to start.
Cluster controller endpoint is available at bdc-control.corpnet.contoso.com:30080, 10.166.6.77:30080.
Waiting for control plane to be ready after 5 minutes.
Cluster control plane is ready.
Cluster is not ready after 15 minutes. Check controller logs for more details.
Data pool is ready.
Storage pool is ready.
Compute pool is ready.
Master pool is ready.
Cluster is not ready after 30 minutes. Check controller logs for more details.
...
...
```

Check the current deployed pods.

```bash
kubectl get pods -n mssql-cluster
```

The results indicate that all the pods have been deployed, but the deployment isn't reporting success.

```output
NAME              READY   STATUS    RESTARTS   AGE 
appproxy-c7f2l    2/2     Running   0          3d13h 
compute-0-0       3/3     Running   0          3d13h 
control-88dgt     3/3     Running   0          3d13h 
controldb-0       2/2     Running   0          3d13h 
controlwd-zzkxz   1/1     Running   0          3d13h 
data-0-0          3/3     Running   0          3d13h 
data-0-1          3/3     Running   0          3d13h 
dns-xkdhh         2/2     Running   0          3d13h 
gateway-0         2/2     Running   0          3d13h 
logsdb-0          1/1     Running   0          3d13h 
logsui-qz8qq      1/1     Running   0          3d13h 
master-0          4/4     Running   0          3d13h 
master-1          4/4     Running   0          3d13h 
master-2          4/4     Running   0          3d13h 
metricsdb-0       1/1     Running   0          3d13h 
metricsdc-xezf7   1/1     Running   0          3d13h 
metricsdc-qdjkh   1/1     Running   0          3d13h 
metricsui-mr34w   1/1     Running   0          3d13h 
mgmtproxy-kz5gg   2/2     Running   0          3d13h 
nmnode-0-0        2/2     Running   1          3d13h 
nmnode-0-1        2/2     Running   0          3d13h 
operator-42ffv    1/1     Running   0          3d13h 
sparkhead-0       4/4     Running   0          3d13h 
sparkhead-1       4/4     Running   0          3d13h 
storage-0-0       4/4     Running   0          3d13h 
storage-0-1       4/4     Running   0          3d13h 
storage-0-2       4/4     Running   0          3d13h 
zookeeper-0       2/2     Running   0          3d13h 
zookeeper-1       2/2     Running   0          3d13h 
zookeeper-2       2/2     Running   0          3d13h 
```

Inspect the health of the HDFS and Spark services. Look for `sparkhead` pod errors.

## Check the HDFS and Spark services

From Azure Data Studio, connect to the controller and view the Big Data Cluster dashboard. Confirm that the HDFS and Spark services have unhealthy `sparkhead` pods.

:::image type="content" source="./media/troubleshoot-ad-hung-deployment-unhealthy-sparkhead-pods/hdfs_spark_unhealthy_sparkhead_pods.png" alt-text="Screenshot shows the Big Data Cluster overview with two sparkhead entries as unhealthy.":::

Extract the logs and locate this file: `\mssql-cluster\control-<identifier>\controller\control-<identifier>-controller-stdout.log`.

> [!TIP]
> There are multiple ways to collect the logs. Instead of copying the logs with [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], you can use a notebook in Azure Data Studio.
>
> In Azure Data Studio, connect to the Kubernetes cluster, and run an appropriate troubleshooting notebook. The following are examples of notebooks.
>
> - TSG027 - Observe cluster deployment
> - TSG061 - Get tail of all container logs for pods in BDC namespace
> - TSG001 - Run `azdata copy-logs`
>
  
## Inspect the logs

Locate the log. The following example points to a controller deployment log:

`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-YYYYMMDD-HHMMSS\<namespace>\control-<identifier>\controller\control-<identifier>-controller-stdout.log`

```output
StatefulSet sparkhead is not healthy: 
{{Pod sparkhead-0 is unhealthy: 
{Container hadoop-yarn-jobhistory is unhealthy: 
{Found error properties: 
{Property: jobhistoryserver.readiness, Details: 'Health module returned error state. error: Head https://sparkhead-0.corpnet.contoso.com:19888/ws/v1/history: dial tcp 10.244.2.33:19888: connect: connection refused'}}} 
{Container hadoop-livy-sparkhistory is unhealthy: 
{Found error properties: 
{Property: sparkhistory.readiness, Details: 'Health module returned error state. error: Head https://sparkhead-0.corpnet.contoso.com:18480: dial tcp 10.244.2.33:18480: connect: connection refused'}}}, 
{Container hadoop-hivemetastore is unhealthy: 
{Found error properties: 
{Property: hivemetastorehttp.readiness, Details: 'Health module returned error state. error: Post https://sparkhead-0.corpnet.contoso.com:9084/api/hms: dial tcp 10.244.2.33:9084: connect: connection refused'}}}}}, 
  
{{Pod sparkhead-1 is unhealthy: 
{Container hadoop-yarn-jobhistory is unhealthy: 
{Found error properties: 
{Property: jobhistoryserver.readiness, Details: 'Health module returned error state. error: Head https://sparkhead-1.corpnet.contoso.com:19888/ws/v1/history: dial tcp 10.244.1.24:19888: connect: connection refused'}}}, 
{Container hadoop-livy-sparkhistory is unhealthy: 
{Found error properties: 
{Property: sparkhistory.readiness, Details: 'Health module returned error state. error: Head https://sparkhead-1.corpnet.contoso.com:18480: dial tcp 10.244.1.24:18480: connect: connection refused'}}}, 
{Container hadoop-hivemetastore is unhealthy: 
{Found error properties: 
{Property: hivemetastorehttp.readiness, Details: 'Health module returned error state. error: Post https://sparkhead-1.corpnet.contoso.com:9084/api/hms: dial tcp 10.244.1.24:9084: connect: connection refused'}}}}} 
```

Inspect the `sparkhead` pods, paying attention to the container logs. This example looks at `sparkhead-0`:

```output
sparkhead-0\hadoop-hivemetastore\supervisor\log\hivemetastorehttp-stderr---supervisor-pZ1gdb 
  
YYYY-MM-DD HH:MM:SS.ms INFO retry.RetryInvocationHandler: org.apache.hadoop.ipc.RemoteException(org.apache.hadoop.ipc.StandbyException): Operation category READ is not supported in state standby. Visit https://s.apache.org/sbnn-error 
at org.apache.hadoop.hdfs.server.namenode.ha.StandbyState.checkOperation(StandbyState.java:98) 
at org.apache.hadoop.hdfs.server.namenode.NameNode$NameNodeHAContext.checkOperation(NameNode.java:1998) 
at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.checkOperation(FSNamesystem.java:1502) 
at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.getFileInfo(FSNamesystem.java:3227) 
at org.apache.hadoop.hdfs.server.namenode.NameNodeRpcServer.getFileInfo(NameNodeRpcServer.java:1158) 
at org.apache.hadoop.hdfs.protocolPB.ClientNamenodeProtocolServerSideTranslatorPB.getFileInfo(ClientNamenodeProtocolServerSideTranslatorPB.java:983) 
at org.apache.hadoop.hdfs.protocol.proto.ClientNamenodeProtocolProtos$ClientNamenodeProtocol$2.callBlockingMethod(ClientNamenodeProtocolProtos.java) 
at org.apache.hadoop.ipc.ProtobufRpcEngine$Server$ProtoBufRpcInvoker.call(ProtobufRpcEngine.java:527) 
at org.apache.hadoop.ipc.RPC$Server.call(RPC.java:1036) 
at org.apache.hadoop.ipc.Server$RpcCall.run(Server.java:978) 
at org.apache.hadoop.ipc.Server$RpcCall.run(Server.java:906) 
at java.security.AccessController.doPrivileged(Native Method) 
at javax.security.auth.Subject.doAs(Subject.java:422) 
at org.apache.hadoop.security.UserGroupInformation.doAs(UserGroupInformation.java:1729) 
at org.apache.hadoop.ipc.Server$Handler.run(Server.java:2876) 
, while invoking ClientNamenodeProtocolTranslatorPB.getFileInfo over nmnode-0-0.corpnet.contoso.com/10.244.2.36:9000 after 8 failover attempts. Trying to failover after sleeping for 13518ms. 
  
sparkhead-0\hadoop-yarn-jobhistory\supervisor\log\jobhistoryserver-stderr---supervisor-GvebR8 
  
YYYY-MM-DD HH:MM:SS.ms INFO retry.RetryInvocationHandler: org.apache.hadoop.ipc.RemoteException(org.apache.hadoop.ipc.StandbyException): Operation category READ is not supported in state standby. Visit https://s.apache.org/sbnn-error 
at org.apache.hadoop.hdfs.server.namenode.ha.StandbyState.checkOperation(StandbyState.java:98) 
at org.apache.hadoop.hdfs.server.namenode.NameNode$NameNodeHAContext.checkOperation(NameNode.java:1998) 
at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.checkOperation(FSNamesystem.java:1502) 
at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.getFileInfo(FSNamesystem.java:3227) 
at org.apache.hadoop.hdfs.server.namenode.NameNodeRpcServer.getFileInfo(NameNodeRpcServer.java:1158) 
at org.apache.hadoop.hdfs.protocolPB.ClientNamenodeProtocolServerSideTranslatorPB.getFileInfo(ClientNamenodeProtocolServerSideTranslatorPB.java:983) 
at org.apache.hadoop.hdfs.protocol.proto.ClientNamenodeProtocolProtos$ClientNamenodeProtocol$2.callBlockingMethod(ClientNamenodeProtocolProtos.java) 
at org.apache.hadoop.ipc.ProtobufRpcEngine$Server$ProtoBufRpcInvoker.call(ProtobufRpcEngine.java:527) 
at org.apache.hadoop.ipc.RPC$Server.call(RPC.java:1036) 
at org.apache.hadoop.ipc.Server$RpcCall.run(Server.java:978) 
at org.apache.hadoop.ipc.Server$RpcCall.run(Server.java:906) 
at java.security.AccessController.doPrivileged(Native Method) 
at javax.security.auth.Subject.doAs(Subject.java:422) 
at org.apache.hadoop.security.UserGroupInformation.doAs(UserGroupInformation.java:1729) 
at org.apache.hadoop.ipc.Server$Handler.run(Server.java:2876) 
, while invoking ClientNamenodeProtocolTranslatorPB.getFileInfo over nmnode-0-1.corpnet.contoso.com/10.244.1.30:9000 after 5 failover attempts. Trying to failover after sleeping for 11416ms. 
  
sparkhead-0\hadoop-livy-sparkhistory\supervisor\log\livy-stderr---supervisor-XiHB1w 
  
YYYY-MM-DD HH:MM:SS.ms INFO retry.RetryInvocationHandler: org.apache.hadoop.ipc.RemoteException(org.apache.hadoop.ipc.StandbyException): Operation category READ is not supported in state standby. Visit https://s.apache.org/sbnn-error 
at org.apache.hadoop.hdfs.server.namenode.ha.StandbyState.checkOperation(StandbyState.java:98) 
at org.apache.hadoop.hdfs.server.namenode.NameNode$NameNodeHAContext.checkOperation(NameNode.java:1998) 
at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.checkOperation(FSNamesystem.java:1502) 
at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.getFileInfo(FSNamesystem.java:3227) 
at org.apache.hadoop.hdfs.server.namenode.NameNodeRpcServer.getFileInfo(NameNodeRpcServer.java:1158) 
at org.apache.hadoop.hdfs.protocolPB.ClientNamenodeProtocolServerSideTranslatorPB.getFileInfo(ClientNamenodeProtocolServerSideTranslatorPB.java:983) 
at org.apache.hadoop.hdfs.protocol.proto.ClientNamenodeProtocolProtos$ClientNamenodeProtocol$2.callBlockingMethod(ClientNamenodeProtocolProtos.java) 
at org.apache.hadoop.ipc.ProtobufRpcEngine$Server$ProtoBufRpcInvoker.call(ProtobufRpcEngine.java:527) 
at org.apache.hadoop.ipc.RPC$Server.call(RPC.java:1036) 
at org.apache.hadoop.ipc.Server$RpcCall.run(Server.java:978) 
at org.apache.hadoop.ipc.Server$RpcCall.run(Server.java:906) 
at java.security.AccessController.doPrivileged(Native Method) 
at javax.security.auth.Subject.doAs(Subject.java:422) 
at org.apache.hadoop.security.UserGroupInformation.doAs(UserGroupInformation.java:1729) 
at org.apache.hadoop.ipc.Server$Handler.run(Server.java:2876) 
, while invoking ClientNamenodeProtocolTranslatorPB.getFileInfo over nmnode-0-1.corpnet.contoso.com/10.244.1.30:9000 after 1 failover attempts. Trying to failover after sleeping for 1401ms. 
```

## Cause

The reverse lookup zone entry for the domain controller in the domain controller's DNS server for the Kubernetes network is missing. For this example, the missing entry was `cni0 10.244`. The `sparkhead` pod containers were trying to use the IP address 10.244.1.30:9000 to reach `nnnode-0-1`, but the DNS wasn't able to resolve it.

:::image type="content" source="media/troubleshoot-ad-hung-deployment-unhealthy-sparkhead-pods/missing_reverse_lookup_zone_entry_for_domain_controller.png" alt-text="Screenshot shows a console window with the I P address for c n i 0 highlighted.":::

## Resolution

Add the missing reverse DNS entry (PTR record) for the zone referred in the logs. For this example, we added 244.10.

:::image type="content" source="media/troubleshoot-ad-hung-deployment-unhealthy-sparkhead-pods/missing_reverse_lookup_zone_entry_for_domain_controller_add.png" alt-text="Screenshot shows adding the missing reverse lookup zone entry for the domain controller.":::

> [!NOTE]
> Make sure that there is a reverse DNS entry (PTR record) for the domain controller itself, registered in the DNS server for all different networks of your cluster nodes.

## Next steps

- [Verify reverse DNS entry for domain controller](active-directory-deploy.md#verify-reverse-dns-entry-for-domain-controller)
