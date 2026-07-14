---
title: Deploy Availability Groups with DH2i DxOperator on AKS
description: Set up an availability group in SQL Server on Kubernetes using DH2i DxOperator.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 07/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - linux-related-content
  - sfi-ropc-blocked
---
# Deploy availability groups on Kubernetes with DH2i DxOperator on Azure Kubernetes Service

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Always On availability groups (AGs) for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Linux based containers deployed to an Azure Kubernetes Service (AKS) cluster, using DH2i DxOperator. These procedures also apply to Azure Red Hat OpenShift clusters. The primary distinction is the deployment of an [Azure Red Hat OpenShift cluster](/azure/openshift/quickstart-portal), followed by substituting `kubectl` commands with `oc` in the following steps.

Using the steps in this article, you learn how to deploy a StatefulSet and use the DH2i DxOperator to create and configure an AG with three replicas, hosted on AKS.

This tutorial consists of the following steps:

> [!div class="checklist"]
> - Create a `configmap` object on AKS cluster with mssql-conf settings
> - Install DxOperator
> - Create secret objects
> - Deploy a three-replica SQL Server availability group using a YAML file
> - Connect to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]

## Prerequisites

- An Azure Kubernetes Service (AKS) or Kubernetes cluster.

- A valid DxEnterprise license with AG features and tunnels enabled. For more information, see the [developer edition](https://dh2i.com/trial/) for nonproduction usage, or [DxEnterprise software](https://dh2i.com/dxenterprise-high-availability/) for production workloads.

## Create the `configmap` object

1. In AKS, create the `configmap` object, which has **[mssql-conf](../../configure/mssql-conf.md)** settings based on your requirements. In this example, you create the `configmap` by using a file called `mssqlconfig.yaml` with the following parameters.

   ```yaml
   apiVersion: v1
   kind: ConfigMap
   metadata:
     name: mssql-config
   data:
     mssql.conf: |
       [EULA]
       accepteula = Y

       [sqlagent]
       enabled = true
   ```

1. Create the object by running the following command.

   ```bash
   kubectl apply -f ./mssqlconfig.yaml
   ```

## Create secret objects

Create a secret to store the `sa` password for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

```bash
kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="<password>"
```

> [!CAUTION]  
> [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

Create a secret to store the license key for DH2i. Visit [DH2i's website](https://dh2i.com/trial/) to get a developer license. Replace `XXXX-XXXX-XXXX-XXXX` in the following example with your license key.

```bash
kubectl create secret generic dxe --from-literal=DX_PASSKEY="<password>" --from-literal=DX_LICENSE=XXXX-XXXX-XXXX-XXXX
```

## Install DxOperator

To install DxOperator, download the DxOperator YAML file by using the following example, and then apply the YAML file.

1. Deploy the YAML file that describes how to set up an AG by using the following command. Save the file with a custom name, such as `DxOperator.yaml`.

   ```bash
   curl -L https://dxoperator.dh2i.com/dxsqlag/files/v2.yaml -o DxOperator.yaml
   kubectl apply -f DxOperator.yaml
   ```

1. After you install the operator, you can deploy [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] containers, configure the availability group, define replicas, and deploy and configure the DxEnterprise cluster. Change the following sample deployment YAML file, `DxSqlAg.yaml`, to suit your requirements.

   ```yaml
   apiVersion: dh2i.com/v1
   kind: DxSqlAg
   metadata:
     name: contoso-sql
   spec:
    sqlAgConfiguration:
      synchronousReplicas: 3
      asynchronousReplicas: 0
      # ConfigurationOnlyReplicas are only allowed with availabilityGroupClusterType set to EXTERNAL
      configurationOnlyReplicas: 0
      availabilityGroupName: AG1
      # Adds auto-generated load balancers to serviceTemplates for easy cluster access
      # For more customization, use the serviceTemplates section below
      #createLoadBalancers: true
      # Listener port for the availability group (uncomment to apply)
      #availabilityGroupListenerPort: 51433
      # For a contained availability group, add the option CONTAINED
      #availabilityGroupOptions: CONTAINED
      # Disables automatic availability mode switching when scaling down
      #disableModeSwitching: true
     statefulSetSpec:
       podspec:
         dxEnterpriseContainer:
           image: "docker.io/dh2i/dxe:latest"
           imagePullPolicy: Always
           acceptEula: true
           clusterSecret: dxe
           vhostName: VHOST1
           # Configuration options for the required persistent volume claim for DxEnterprise
           #volumeClaimConfiguration:
           # Set custom storage class for DxE PVC
           #storageClassName: example-class
         mssqlServerContainer:
           image: "mcr.microsoft.com/mssql/server:2025-latest"
           imagePullPolicy: Always
           mssqlSecret: mssql
           acceptEula: true
           mssqlPID: Developer
           # Set a non-default SQL Server port. DxOperator will auto-detect
           # the port from other sources too, such as mssqlConfigMap
           #mssqlTcpPort: 51433
           # The MSSQL configMap (mssql.conf file)
           #mssqlConfigMap: mssql-config
           # Configuration options for the required persistent volume claim for SQL Server
           #volumeClaimConfiguration:
           #  resources:
           #    requests:
           #      storage: 2Gi
         # Additional pod containers, such as mssql-tools
         #containers:
         #- name: mssql-tools
           #image: "mcr.microsoft.com/mssql-tools"
           #command: [ "/bin/sh" ]
           #args: [ "-c", "tail -f /dev/null" ]
   ```

1. Deploy the `DxSqlAg.yaml` file.

   ```bash
   kubectl apply -f DxSqlAg.yaml
   ```

## Create an availability group listener

1. Update the `availabilityGroupListenerPort` value in your `DxSqlAg.yaml` file:

   ```yaml
   spec:
     sqlAgConfiguration:
       availabilityGroupListenerPort: 51433
   ```

1. Apply the file:

   ```bash
   kubectl apply -f DxSqlAg.yaml
   ```

1. Apply the following YAML to add a load balancer for the listener.

   ```yaml
   apiVersion: v1
   kind: Service
   metadata:
     name: contoso-cluster-lb
   spec:
     type: LoadBalancer
     selector:
       dh2i.com/entity: contoso-sql
       # This label points to whichever pod is the active Vhost member
       dh2i.com/active-vhost-vhost1: "true"
     ports:
       - name: sql
         protocol: TCP
         port: 1433
         targetPort: 51444
       - name: listener
         protocol: TCP
         port: 51433
         targetPort: 51433
       - name: dxe
         protocol: TCP
         port: 7979
         targetPort: 7979
   ```

1. Verify that the services and load balancers are running.

   ```bash
   kubectl get services
   ```

   The output looks similar to the following example:

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
   ```

1. Verify that all three SQL Server pods are running.

   ```bash
   kubectl get pods
   ```

   The output looks similar to the following example:

   ```output
   NAME            READY   STATUS    RESTARTS   AGE
   contoso-sql-0   2/2     Running   0          74m
   contoso-sql-1   2/2     Running   0          74m
   contoso-sql-2   2/2     Running   0          74m
   ```

## Related content

- [Deploy SQL Server containers and availability group with DH2i DxOperator on Azure Kubernetes Service via Rancher](tutorial-kubernetes-dxoperator-rancher-suse.md)
- [Deploy availability groups with DH2i DxEnterprise on Kubernetes](tutorial-kubernetes-dh2i.md)
- [Quickstart: Deploy a SQL Server container cluster on Azure or Red Hat OpenShift](../../install-upgrade/quickstart-containers-azure.md)
- [Deploy SQL Server Linux containers on Kubernetes with StatefulSets](../../containers/kubernetes-best-practices-statefulsets.md)
