---
title: Configure SQL Server container in Kubernetes for high availability | Microsoft Docs
description: This tutorial shows how to deploy a SQL Server high availability soluion with Kubernetes on Azure Container Service.
author: MikeRayMSFT
ms.author: mikeray
manager: jhubbard
ms.date: 01/02/2018
ms.topic: tutorial
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: sql-linux
ms.suite: "sql"
ms.custom: "mvc"
ms.technology: database-engine
ms.workload: "Inactive"
---
# Configure SQL Server container in Kubernetes for high availability

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

In this article, you will configure a SQL Server instance on Kubernetes in Azure Container Service (AKS) with persistent storage for high availability. 

This tutorial demonstrates how to configure a highly available SQL Server instance in containers using AKS. 

> [!div class="checklist"]
> * Install kubectl
> * Set up the cluster
> * Configure storage
> * Create a stateful set
> * Connect to the container with SQL Server Management Studios (SSMS)
> * Verify failure and recovery

### HA solution using Kubernetes running in Azure Container Service

Kubernetes 1.6+ has support for Storage Classes, Persistent Volume Claims, and the Azure disk volume driver. You can create and manage your SQL Server instances natively in Kubernetes. For additional high availability, you can use a StatefulSet. This article includes Kubernetes specs on how to deploy SQL Server on Kubernetes cluster running on Azure Container Service and how to use a StatefulSet to achieve a high availability configuration similar to shared disk failover cluster instance. In this configuration, Kubernetes plays the role of the cluster orchestrator. Upon a failure of SQL Server instance running in a container, the orchestrator bootstraps another instance of the container that attaches to the same persistent storage, which maps to Azure disk.

![Kubernetes SQL Server Cluster](media/tutorial-sql-server-containers-kubernetes/kubernetes-sql.png)

## Prerequisites

* An Azure Container Service (AKS) cluster. 

   If you are not familiar with AKS clusters, you can follow the instructions on [Deploy an Azure Container Service (AKS) cluster](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough) to create an AKS cluster. 

<!--
### Configuration for Azure CLI

If you use Azure CLI on your local machine, verify the Azure CLI version and install `kubectl`. This section links to instructions for these tasks. 

#### Verify Azure CLI version

Install the Azure CLI version for your environment. See the environment specific instructions under [Install Azure CLI 2.0](http://docs.microsoft.com/cli/azure/install-azure-cli).

Verify that you are running Azure CLI version 2.0.21 or later. Run `az --version` to check your version. 

Configure the security context 

```azurecli
az login
```

Azure CLI returns instructions to sign in on your current device. Follow those instructions. 

Set the Azure account for the cluster.
	
```azurecli
az account set -s "<AzureAccount>"
```

* `<AzureAccount>` 
    * Your Azure account.

For example, to set Azure CLI to use an account named *MyAccount*, run the following command:

```azurecli
az account set -s "MyAccount"
```

#### Install kubectl

Install kubectl for your environment. See the environment specific instructions under [Install and Set Up kubectl](http://kubernetes.io/docs/tasks/tools/install-kubectl/).

## Create the resource group

Create the resource group.

```azurecli
az group create -n <ResourceGroup> -l <Location>
```

* `<ResourceGroup>` 
   * The name of the new resource group.
    
* `<Location>` 
   * The name of the Azure region.

For example, to create a resource group named *MyRG* in Central US, run the following command:

```azurecli
az group create -n MyRG -l centralus
```

## Deploy an Azure Container Service Cluster (AKS)

Follow the instructions on [Deploy an Azure Container Service (AKS) cluster](http://docs.microsoft.com/azure/aks/tutorial-kubernetes-deploy-cluster) to deploy an AKS cluster. 

Note that Azure has a SQL Server container image. 

In Azure, the Kubernetes cluster is also called the container service. You can create a container service with Azure CLI.

```azurecli
az aks create --name=<ClusterName> --resource-group=<ResourceGroup> --generate-ssh-key 
```

* `<ClusterName>` 
   * The name of your cluster.

* `<ResourceGroup>` 
   * The name of your resource group.

* `<Location>` 
   * The name of the Azure region.

For parameter information, see [az aks create](https://docs.microsoft.com/en-us/cli/azure/aks?view=azure-cli-latest#az_aks_create).

For example, the following command creates a new Azure managed Kubernetes cluster with the following parameters:
* Name - `MyManagedCluster` 
* Region - same as the resource group 
* Node count - 3
* SSH keys - automatically generated public and private key files.

Run the following command:

```azurecli
az aks create --name=MyManagedCluster --resource-group=MyRG --generate-ssh-key
```

Azure creates the Kubernetes cluster and the Azure CLI returns the status. 

## Authenticate into the cluster.

```azurecli
az aks get-credentials -n <ClusterName> -g <ResourceGroup> 
```

* `<ClusterName>`
   * The name of your cluster.

* `<ResourceGroup>`
   * The name of your resource group.

For example, to authenticate into the cluster you created in the previous step, run:

```azurecli
az aks get-credentials -n MyManagedCluster -g MyRG 
```

After you connect to the cluster, use `kubectl` to interact with the cluster. For example, to verify your connection to the cluster run:

```cmd
kubectl get nodes 
```
    
`kubectl` returns the names, of the nodes and some status information of the kubernetes cluster you created in Azure. When three agent nodes and one master has the status "Ready" 

You now have a Kubernetes cluster installed in Azure. For a detailed introduction of how to set up a Kubernetes cluster, see [Introduction to Azure Container Service (AKS)](http://docs.microsoft.com/azure/aks/intro-kubernetes). 
-->

## Configure storage

Configure a persistent volume, and persistent volume claim in the Kubernetes cluster. For background on Kuberntes storage, see [Persistent Volumes](http://kubernetes.io/docs/concepts/storage/persistent-volumes/).  Complete the following steps: 

1. Create a manifest to define the storage class and the persistent volume claim.  The manifest specifies the storage provisionioner, paramaters and the reclaim policy. The Kubernetes cluster will use this manifest to create the persistent storage. 

   The following yaml example defines a storage class and persistent volume claim. The storage class is named `azure-disk` and the persistent volume claim is named `mssql-data`. The persistent volume claim metadata includes an annotation connecting it back to the the storage class. 

   ```yaml
   kind: StorageClass
   apiVersion: storage.k8s.io/v1beta1
   metadata:
        name: azure-disk
   provisioner: kubernetes.io/azure-disk
   parameters:
     storageaccounttype: Standard_LRS
     kind: Managed
   ---
   kind: PersistentVolumeClaim
   apiVersion: v1
   metadata:
     name: mssql-data
     annotations:
       volume.beta.kubernetes.io/storage-class: azure-disk
   spec:
     accessModes:
     - ReadWriteOnce
     resources:
       requests:
         storage: 8Gi
   ```

   Save the file, for example **pvc.yml**.

1. Create the persistent volume claim in Kubernetes.

   ```azurecli
   kubectl apply -f <Path to PVC.yaml file>
   ```

   * `<Path to PVC.yaml file>`
      * The location where you saved the file.

   The persistent volume is automatically created as an Azure storage account, and bound to the persistent volume claim. 

1. Verify the persistent volume claim.

   ```azurecli
   kubectl describe pvc <PersistentVolumeClaim>
   ```

   * `<PersistentVolumeClaim>`
      * The name of the persistent volume claim.

    In the preceding step, the persistent volume claim is named `mssql-data`. To see the metadata about the persistent volume claim, run the following command.

    ```azurecli
    kubectl describe pvc mssql-data
    ```

    The returned metadata includes a value called `Volume`. This value maps to the name of the blob.

    ![Describe volume](media/tutorial-sql-server-containers-kubernetes/describe-volume.png)

    Note the value for volume in the command prompt image above, matches part of the name of the blob in the Azure portal image below. 

    ![Describe volume portal](media/tutorial-sql-server-containers-kubernetes/describe-volume-portal.png)

1. Verify the persistent volume.

   ```azurecli
   kubectl describe pv
   ```

   `kubectl` returns metadata about the persistent volume that was automatically created and bound to the persistent volume claim. 

## Configure SA password as Kubernetes secret

Create a secret in the Kubernetes cluster to store the SA password for SQL Server. 

1. Create a yaml file for for the secret that includes the SA password.  

   ```yaml
   apiVersion: v1
   kind: Secret
   metadata:
     name: mssql
   type: Opaque
   data:
     SA_PASSWORD: UEBzc3dvcmQxMg==
   ```

   * `<ComplexPassword>`
      * The password you will use for the SA account. 

   Save the file as `secret.yaml`.

1.  Create the secret in the Kubernetes cluster.

   ```azurecli
   kubectl apply -f <Path to secret.yaml file>
   ```
   
For additional information about secrets in Kubernetes, see [Secrets](http://kubernetes.io/docs/concepts/configuration/secret/).

## Create the SQL Server container

In this example, the SQL Server container is described as a [Kubernetes deployment object](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/). In this step, create a manifest to describe the container based on the Microsoft SQL Server mssql-server-linux image. The manifest references the `mssql-server` persistent volume claim, and the `mssql` secret which you already applied to the Kubernetes cluster. 

1. Create a yaml file describing the deployment. The following example describes a stateful set including a container based on the SQL Server container image.

   ```yaml
   ---
   apiVersion: apps/v1beta1
   kind: Deployment
   metadata:
     name: mssql-deployment
   spec:
#     serviceName: mssql-statefulset
     replicas: 1
     template:
       metadata:
         labels:
           app: mssql
       spec:
#         terminationGracePeriodSeconds: 10
         containers:
         - name: mssql
           image: microsoft/mssql-server-linux
#           ports:
#           - containerPort: 1433
        env:
        - name: ACCEPT_EULA
          value: "Y"
        - name: MSSQL_PID
          value: "Developer"
        - name: SA_PASSWORD
          valueFrom:
            secretKeyRef:
              name: mssql
              key: SA_PASSWORD 
           volumeMounts:
           - name: mssqldb
             mountPath: /var/opt/mssql
         volumes:
         - name: mssqldb
           persistentVolumeClaim:
             claimName: mssql-data
   ---
   apiVersion: v1
   kind: Service
   metadata:
     name: mssql-loadbalancer
   spec:
     selector:
       app: mssql
     ports:
       - protocol: TCP
         port: 1433
         targetPort: 1433
     type: LoadBalancer
   ```

   Copy the preceding code into a new file, named `sqldeployment.yaml`. Update the following values. 

   * `value: "Developer"`
     * Sets the container to run SQL Server developer edition. If it is not for production data, you can use `Developer`. If it is for production use, select the appropriate edition. Can be one of `Enterprise`, `Standard`, or `Express`. 

     >[!NOTE]
     >For more information, see [How to license SQL Server](http://www.microsoft.com/sql-server/sql-server-2017-pricing).

   * `persistentVolumeClaim`
     * This value requires an entry for `claimName:` that maps to the name used for the persistent volume claim. This article uses `mssql-data`. 

   >[!NOTE]
   >By using the `LoadBalancer` service type, the SQL Server container is accessible remotely (via the internet) at port 1433.

    Save the file, for example **sqldeployment.yaml**.

1. Create the stateful set.

   ```azurecli
   kubectl apply -f <Path to sqldeployment.yaml file>
   ```

   * `<Path to sqldeployment.yaml file>`
      * `The location where you saved the file.

   The stateful set is created, with SQL Server running as a pod in the kubernetes cluster with connection to persistent storage. 

1. Verify the services are running. Run the following command:

   ```azurecli
   kubectl get services 
   ```

   Note the IP address for the SQL Server container. 

## Connect to the container with SSMS

If you configured the container as described, you can connect with SSMS from outside of the Azure virtual network. To access via SSMS, use the external IP Address. If needed, supply the port of the instance. For example, `1433`.

## Verify failure and recovery

To verify failure and recovery you can delete the pod. Do the following steps:

1. List the pod running SQL Server.

   ```azurecli
   kubectl get pods
   ```

   Note the name of the pod running SQL Server.

1. Delete the pod.

   ```azurecli
   kubectl delete pod mssql-statefulset-0
   ```

   Note that `mssql-statefulset-0` is the value returned from the previous step for pod name. 

Kubernetes will automatically recreate the pod to recover a SQL Server container and connect to the persistent storage.

## Next steps

In this tutorial, you learned how to 
> [!div class="checklist"]
> * Install kubectl
> * Set up the cluster
> * Configure storage
> * Create a stateful set
> * Connect to the container with SSMS
> * Verify failure and recovery


> [!div class="nextstepaction"]
>[Intro - Kubernetes](http://docs.microsoft.com/en-us/azure/aks/intro-kubernetes)
