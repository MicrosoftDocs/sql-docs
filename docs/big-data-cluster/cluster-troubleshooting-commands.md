---
title: Monitor and troubleshoot
titleSuffix: SQL Server big data clusters
description: This article provides useful commands for monitoring and troubleshooting a SQL Server 2019 big data cluster (preview).
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/23/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Monitoring and troubleshoot SQL Server big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes several useful Kubernetes commands that you can use to monitor and troubleshoot a SQL Server 2019 big data cluster (preview). It shows how to view in-depth details of a pod or other Kubernetes artifacts that are located in the big data cluster. This article also covers common tasks, such as copying files to or from a container running one of the SQL Server big data cluster services.

> [!TIP]
> Run the following **kubectl** commands on either a Windows (cmd or PS) or Linux (bash) client machine. They require previous authentication in the cluster and a cluster context to run against. For example, for a previously created AKS cluster you can run `az aks get-credentials --name <aks_cluster_name> --resource-group <azure_resource_group_name>` to download the Kubernetes cluster configuration file and set the cluster context.

## Get status of pods

You can use the `kubectl get pods` command to get the status of pods in the cluster for either all namespaces or the big data cluster namespace. The following sections show examples of both.

### Show status of all pods in the Kubernetes cluster

Run the following command to get all the pods and their statuses, including the pods that are part of the namespace that SQL Server big data cluster pods are created in:

```bash
kubectl get pods --all-namespaces
```

### Show status of all pods in the SQL Server big data cluster

Use the `-n` parameter to specify a specific namespace. Note that SQL Server big data cluster pods are created in a new namespace created at cluster bootstrap time based on the cluster name specified in the deployment configuration file. The default name, `mssql-cluster`, is used here.

```bash
kubectl get pods -n mssql-cluster
```

You should see output similar to the following list for a running big data cluster:

```output
PS C:\> kubectl get pods -n mssql-cluster
NAME              READY   STATUS    RESTARTS   AGE
appproxy-f2qqt    2/2     Running   0          110m
compute-0-0       3/3     Running   0          110m
control-zlncl     4/4     Running   0          118m
data-0-0          3/3     Running   0          110m
data-0-1          3/3     Running   0          110m
gateway-0         2/2     Running   0          109m
logsdb-0          1/1     Running   0          112m
logsui-jtdnv      1/1     Running   0          112m
master-0          7/7     Running   0          110m
metricsdb-0       1/1     Running   0          112m
metricsdc-shv2f   1/1     Running   0          112m
metricsui-9bcj7   1/1     Running   0          112m
mgmtproxy-x6gcs   2/2     Running   0          112m
nmnode-0-0        1/1     Running   0          110m
storage-0-0       7/7     Running   0          110m
storage-0-1       7/7     Running   0          110m
```

> [!NOTE]
> During deployment, pods with a **STATUS** of **ContainerCreating** are still coming up. If the deployment hangs for any reason, this can give you an idea where the problem might be. Also look at the **READY** column. This tells you how many containers have started in the pod. Note that deployments can take 30 minutes or more depending on your configuration and network. Much of this time is spent downloading the container images for different components.

## Get pod details

Run the following command to get a detailed description of a specific pod in JSON format output. It includes details, such as the current Kubernetes node that the pod is placed on, the containers running within the pod, and the image used to bootstrap the containers. It also shows other details, such as labels, status, and persisted volumes claims that are associated with the pod.

```bash
kubectl describe pod  <pod_name> -n <namespace_name>
```

The following example shows the details for the `master-0` pod in a big data cluster named `mssql-cluster`:

```bash
kubectl describe pod  master-0 -n mssql-cluster
```

If any errors have occurred, you can sometimes see the error in the recent events for the pod.

## Get pod logs

You can retrieve the logs for containers running in a pod. The following command retrieves the logs for all containers running in the pod named `master-0` and outputs them to a file name `master-0-pod-logs.txt`:

```bash
kubectl logs master-0 --all-containers=true -n mssql-cluser > master-0-pod-logs.txt
```

## <a id="services"></a> Get status of services

Run the following command to get details for the big data cluster services. These details include their type and the IPs associated with respective services and ports. Note that SQL Server big data cluster services are created in a new namespace created at cluster bootstrap time based on the cluster name specified in deployment configuration file.

```bash
kubectl get svc -n <namespace_name>
```

The following example shows the status for services in a big data cluster named `mssql-cluster`:

```bash
kubectl get svc -n mssql-cluster
```

The following services support external connections to the big data cluster:

| Service | Description |
|---|---|
| **master-svc-external** | Provides access to the master instance.<br/>(**EXTERNAL-IP,31433** and the **SA** user) |
| **controller-svc-external** | Supports tools and clients that manage the cluster. |
| **mgmtproxy-svc-external** | Provides access to the [Cluster Administration Portal](cluster-admin-portal.md).<br/>(https://**EXTERNAL-IP**:30777/portal) |
| **gateway-svc-external** | Provides access to the HDFS/Spark gateway.<br/>(**EXTERNAL-IP** and the **root** user) |
| **appproxy-svc-external** | Support application deployment scenarios. |

> [!TIP]
> This is a way of viewing the services with **kubectl**, but it is also possible to use `mssqlctl cluster endpoints list` command to view these endpoints. For more information, see [Get big data cluster endpoints](deployment-guidance.md#endpoints).

## Get service details

Run this command to get a detailed description of a service in JSON format output. It will include details like labels, selector, IP, external-IP (if the service is of LoadBalancer type), port, etc.

```bash
kubectl describe service <service_name> -n <namespace_name>
```

The following example retrieves details for the **master-svc-external** service:

```bash
kubectl describe service master-svc-external -n mssql-cluster
```

## Run commands in a container

If existing tools or the infrastructure does not enable you to perform a certain task without actually being in the context of the container, you can log in to the container using `kubectl exec` command. For example, you might need to check if a specific file exists, or you might need to restart services in the container. 

To use the `kubectl exec` command, use the following syntax:

```bash
kubectl exec -it <pod_name>  -c <container_name> -n <namespace_name> -- /bin/bash <command name> 
```

The following two sections provide two examples of running a command in a specific container.

### <a id="restartsql"></a> Log in to a specific container and restart SQL Server process

The following example shows how to restart the SQL Server process in the `mssql-server` container in the `master-0` pod:

```bash
kubectl exec -it master-0  -c mssql-server -n mssql-cluster -- /bin/bash 
supervisorctl restart mssql
```

### <a id="restartservices"></a> Log in to a specific container and restart services in a container
 
The following example shows how to restart all services managed by **supervisord**: 

```bash
kubectl exec -it master-0  -c mssql-server -n mssql-cluster -- /bin/bash 
supervisorctl -c /opt/supervisor/supervisord.conf reload
```

## <a id="copy"></a> Copy files

If you need to copy files from the container to your local machine, use `kubectl cp` command with the following syntax:

```bash
kubectl cp <pod_name>:<source_file_path> -c <container_name> -n <namespace_name> <target_local_file_path>
```

Similarly, you can use `kubectl cp` to copy files from the local machine into a specific container.

```bash
kubectl cp <source_local_file_path> <pod_name>:<target_container_path> -c <container_name>  -n <namespace_name>
```

### <a id="copyfrom"></a> Copy files from a container

The following example copies the SQL Server log files from the container to the `~/temp/sqlserverlogs` path on the local machine (in this example the local machine is a Linux client):

```bash
kubectl cp master-0:/var/opt/mssql/log -c mssql-server -n mssql-cluster ~/tmp/sqlserverlogs
```

### <a id="copyinto"></a> Copy files into container

The following example copies the **AdventureWorks2016CTP3.bak** file from the local machine to the SQL Server master instance container (`mssql-server`) in the `master-0` pod. The file is copied to the `/tmp` directory in the container. 

```bash
kubectl cp ~/Downloads/AdventureWorks2016CTP3.bak master-0:/tmp -c mssql-server -n mssql-cluster
```

## <a id="forcedelete"></a> Force delete a pod
 
It is not recommended to force-delete a pod. But for testing availability, resiliency, or data persistence, you can delete a pod to simulate a pod failure with the `kubectl delete pods` command.

```bash
kubectl delete pods <pod_name> -n <namespace_name> --grace-period=0 --force
```

The following example deletes the storage pool pod, `storage-0-0`:

```bash
kubectl delete pods storage-0-0 -n mssql-cluster --grace-period=0 --force
```

## <a id="getip"></a> Get pod IP
 
For troubleshooting purposes, you might need to get the IP of the node a pod is currently running on. To get the IP address, use the `kubectl get pods` command with the following syntax:

```bash
kubectl get pods <pod_name> -o yaml -n <namespace_name> | grep hostIP
```

The following example gets the IP address of the node that the `master-0` pod is running on:

```bash
kubectl get pods master-0 -o yaml -n mssql-cluster | grep hostIP
```

## Cluster administration portal

Use the [cluster administration portal](cluster-admin-portal.md) to monitor the status of your big data cluster. For example, during deployments, you can use the **Deployment** tab. You have to wait for the **mgmtproxy-svc-external** service to start before accessing this portal, so it won't be available at the beginning of a deployment.

## Kubernetes dashboard

You can launch the Kubernetes dashboard for additional information about the cluster. The following sections explain how to launch the dashboard for Kubernetes on AKS and for Kubernetes bootstrapped using kubeadm.
 
### Start dashboard when cluster is running in AKS

To launch the Kubernetes dashboard run:

```bash
az aks browse --resource-group <azure_resource_group> --name <aks_cluster_name>
```

> [!Note]
> If you get the following error: *Unable to listen on port 8001: All listeners failed to create with the following errors: Unable to create listener: Error listen tcp4 127.0.0.1:8001: >bind: Only one usage of each socket address (protocol/network address/port) is normally permitted. Unable to create listener: Error listen tcp6: address [[::1]]:8001: missing port in >address error: Unable to listen on any of the requested ports: [{8001 9090}]*, make sure you did not start the dashboard already from another window.

When you launch the dashboard on your browser, you might get permission warnings due to RBAC being enabled by default in AKS clusters, and the service account used by the dashboard does not have enough permissions to access all resources (for example, *pods is forbidden: User "system:serviceaccount:kube-system:kubernetes-dashboard" cannot list pods in the namespace "default"*). Run the following command to give the necessary permissions to `kubernetes-dashboard`, and then restart the dashboard:

```bash
kubectl create clusterrolebinding kubernetes-dashboard -n kube-system --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard
```

### Start dashboard when Kubernetes cluster is bootstrapped  using kubeadm

For detailed instructions how to deploy and configure the dashboard in your Kubernetes cluster, see [the Kubernetes documentation](https://kubernetes.io/docs/tasks/access-application-cluster/web-ui-dashboard/). To launch the Kubernetes dashboard, run this command:

```bash
kubectl proxy
```

## Next steps

For more information about big data clusters, see [What are SQL Server big data clusters](big-data-cluster-overview.md).
