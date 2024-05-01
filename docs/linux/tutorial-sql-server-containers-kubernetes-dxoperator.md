---
title: Deploy availability groups with DH2i DxOperator on AKS
description: Set up an availability group in SQL Server on Kubernetes using DH2i DxOperator.
author: aravindmahadevan-ms
ms.author: armaha
ms.reviewer: amitkh, randolphwest
ms.date: 04/17/2024
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - linux-related-content
---
# Deploy availability groups on Kubernetes with DH2i DxOperator on Azure Kubernetes Service

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Always On availability groups (AGs) for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux based containers deployed to an Azure Kubernetes Service (AKS) Kubernetes cluster, using DH2i DxOperator.

> [!NOTE]  
> Microsoft supports data movement, AG, and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] components. DH2i is responsible for support of the DxEnterprise product, which includes cluster and quorum management. DxOperator is a software extension to Kubernetes that uses custom resource definitions to automate the deployment of DxEnterprise clusters. DxEnterprise then provides all of the instrumentation to create, configure, manage and provide automatic failover for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] AG workloads in Kubernetes.
>
> You can register for a [free DxEnterprise software license](https://dh2i.com/dxoperator-for-sql-server-kubernetes-deployments/). For more information, see the [DxOperator Quick Start Guide](https://support.dh2i.com/docs/guides/dxoperator/dxoperator-qsg/).

Using the steps mentioned in this article, learn how to deploy a StatefulSet and use the DH2i DxOperator to create and configure an AG with three replicas, hosted on AKS.

This tutorial consists of the following steps:

> [!div class="checklist"]
> - Create a `configmap` object on AKS cluster with mssql-conf settings
> - Install DxOperator
> - Create a secret objects
> - Deploy 3 replica SQL AG using YAML file
> - Connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]

## Prerequisites

- An Azure Kubernetes Service (AKS) or Kubernetes cluster.

- A valid DxEnterprise license with AG features and tunnels enabled. For more information, see the [developer edition](https://dh2i.com/trial/) for nonproduction usage, or [DxEnterprise software](https://dh2i.com/dxenterprise-high-availability/) for production workloads.

## Create the `configmap` object

1. In AKS, create the `configmap` object, which has specific **mssql-conf** settings. Create a file called `mssqlconfig.yaml`, with your specific **mssql-conf** settings added to it, using the following example.

   ```yaml
   apiVersion: v1
   kind: ConfigMap
   metadata:
     name: mssql-config
   data:
     mssql.conf: |
       [EULA]
       accepteula = Y
   
       [network]
       tcpport = 51433
   
       [sqlagent]
       enabled = true
   ```

1. Create the object by executing the following command.

   ```bash
   kubectl apply -f ./mssqlconfig.yaml
   ```

## Create secret objects

Create a secret to store the `sa` password for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

```bash
kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="Password123"
```

Create a secret to store the license key for DH2i. Visit [DH2i's website](https://dh2i.com/trial/) to get a developer license. Replace `XXXX-XXXX-XXXX-XXXX` in the following example with your license key.

```bash
kubectl create secret generic dxe --from-literal=DX_PASSKEY="Password123" --from-literal=DX_LICENSE=XXXX-XXXX-XXXX-XXXX
```

## Install DxOperator

To install DxOperator, you must download the DxOperator YAML file using the following example, and then apply the YAML file.

1. Deploy the YAML describing how to set up an AG, using the following command. Save the file with a custom name, such as `DxEnterpriseSqlAg.yaml`.

   ```bash
   curl -L https://dxoperator.dh2i.com/dxesqlag/files/v1.yaml -o DxEnterpriseSqlAg.yaml
   kubectl apply –f DxEnterpriseSqlAg.yaml
   ```

   The YAML file looks similar to the following example.

   ```yaml
   apiVersion: dh2i.com/v1
   kind: DxEnterpriseSqlAg
   metadata:
     name: contoso-sql
   spec:
     synchronousReplicas: 3
     asynchronousReplicas: 0
     # ConfigurationOnlyReplicas are only allowed with availabilityGroupClusterType set to EXTERNAL
     configurationOnlyReplicas: 0
     availabilityGroupName: AG1
     # Listener port for the availability group (uncomment to apply)
     availabilityGroupListenerPort: 51433
     # For a contained availability group, add the option CONTAINED
     availabilityGroupOptions: null
     # Valid options are EXTERNAL (automatic failover) and NONE (no automatic failover)
     availabilityGroupClusterType: EXTERNAL
     createLoadBalancers: true
     template:
       metadata:
         labels:
           label: example
         annotations:
           annotation: example
       spec:
         dxEnterpriseContainer:
           image: "docker.io/dh2i/dxe:latest"
           imagePullPolicy: Always
           acceptEula: true
           clusterSecret: dxe
           vhostName: VHOST1
           joinExistingCluster: false
           # QoS – guaranteed (uncomment to apply)
           #resources:
             #limits:
               #memory: 1Gi
               #cpu: '1'
           # Configuration options for the required persistent volume claim for DxEnterprise
           volumeClaimConfiguration:
             storageClassName: null
             resources:
               requests:
                 storage: 1Gi
         mssqlServerContainer:
           image: "mcr.microsoft.com/mssql/server:latest"
           imagePullPolicy: Always
           mssqlSecret: mssql
           acceptEula: true
           mssqlPID: Developer
           mssqlConfigMap: mssql-config
           # QoS – guaranteed (uncomment to apply)
           #resources:
             #limits:
               #memory: 2Gi
               #cpu: '2'
           # Configuration options for the required persistent volume claim for SQL Server
           volumeClaimConfiguration:
             storageClassName: null
             resources:
               requests:
                 storage: 2Gi
         # Additional side-car containers, such as mssql-tools (uncomment to apply)
         #containers:
         #- name: mssql-tools
             #image: "mcr.microsoft.com/mssql-tools"
             #command: [ "/bin/sh" ]
             #args: [ "-c", "tail -f /dev/null" ]
   ```

Deploy the `DxEnterpriseSqlAg.yaml` file.

   ```bash
   kubectl apply -f DxEnterpriseSqlAg.yaml
   ```

## Create an availability group listener

Apply the following YAML to add a load balancer, by setting the selector to the name of the YAML file used to deploy an AG in the previous step. In this example, it's `DxEnterpriseSqlAg`.

```yaml
apiVersion: v1
kind: Service
metadata:
  name: contoso-cluster-lb
spec:
  type: LoadBalancer
  selector:
    dh2i.com/entity: contoso-sql
  ports:
    - name: sql
      protocol: TCP
      port: 1433
      targetPort: 1433
    - name: listener
      protocol: TCP
      port: 51433
      targetPort: 51433
    - name: dxe
      protocol: TCP
      port: 7979
      targetPort: 7979
```

Verify the deployment and load balancer assignments.

```bash
kubectl get pods
kubectl get services
```

You should see output similar to the following example.

```output
NAME                     TYPE           CLUSTER-IP   EXTERNAL-IP     PORT(S)                                         AGE
contoso-cluster-lb       LoadBalancer   10.1.0.21    172.212.20.29   1433:30484/TCP,14033:30694/TCP,7979:30385/TCP   3m18s
contoso-sql-0            ClusterIP      None         <none>          7979/TCP,7980/TCP,7981/UDP,5022/TCP,1433/TCP    79m
contoso-sql-0-lb         LoadBalancer   10.1.0.210   4.255.19.171    7979:32374/TCP,1433:32444/TCP                   79m
contoso-sql-1            ClusterIP      None         <none>          7979/TCP,7980/TCP,7981/UDP,5022/TCP,1433/TCP    79m
contoso-sql-1-lb         LoadBalancer   10.1.0.158   4.255.19.201    7979:30152/TCP,1433:30868/TCP                   79m
contoso-sql-2            ClusterIP      None         <none>          7979/TCP,7980/TCP,7981/UDP,5022/TCP,1433/TCP    79m
contoso-sql-2-lb         LoadBalancer   10.1.0.159   4.255.19.218    7979:30566/TCP,1433:31463/TCP                   79m
kubernetes               ClusterIP      10.1.0.1     <none>          443/TCP                                         87m

PS /home/aravind> kubectl get pods
NAME         READY   STATUS    RESTARTS   AGE
contoso-sql-0   2/2     Running   0          74m
contoso-sql-1   2/2     Running   0          74m
contoso-sql-2   2/2     Running   0          74m
```

## Related content

- [Deploy availability groups with DH2i DxEnterprise on Kubernetes](tutorial-sql-server-containers-kubernetes-dh2i.md)
- [Deploy SQL Server containers on Azure Kubernetes Service](quickstart-sql-server-containers-kubernetes.md)
- [Deploy SQL Server Linux containers on Kubernetes with StatefulSets](sql-server-linux-kubernetes-best-practices-statefulsets.md)
- [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
