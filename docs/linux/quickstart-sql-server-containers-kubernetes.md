---
title: "Quickstart: Deploy a SQL Server container with Azure Kubernetes Services (AKS)"
description: This tutorial shows how to deploy a SQL Server high availability solution with Kubernetes on Azure Kubernetes Service.
author: rwestMSFT
ms.author: randolphwest
ms.topic: quickstart
ms.service: sql
ms.subservice: linux
ms.date: 08/18/2022
ms.custom:
  - template-quickstart
  - seo-lt-2019
  - intro-deployment
---
# Quickstart: Deploy a SQL Server container with Azure Kubernetes Services (AKS)

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This quickstart demonstrates how to configure a highly available SQL Server instance in a container on Azure Kubernetes Service (AKS), with persistent storage. If the SQL Server instance fails, Kubernetes automatically re-creates it in a new pod. Kubernetes also provides resiliency against a node failure.

This quickstart uses [kubectl](https://kubernetes.io/docs/user-guide/kubectl/) to manage the cluster.

## Prerequisites

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/?WT.mc_id=A261C142F).

- A Kubernetes cluster. See [Deploy an Azure Kubernetes Service (AKS) cluster](/azure/aks/tutorial-kubernetes-deploy-cluster) to create and connect to a single-node Kubernetes cluster in AKS with `kubectl`.

  > [!NOTE]  
  > To protect against node failure, a Kubernetes cluster requires more than one node.

- Azure CLI. See [How to install the Azure CLI](/cli/azure/install-azure-cli) to install the latest version.

## Create an SA password

1. Create an SA password in the Kubernetes cluster. Kubernetes can manage sensitive configuration information, like passwords as [secrets](https://kubernetes.io/docs/concepts/configuration/secret/).

2. To create a secret in Kubernetes named `mssql` that holds the value `MyC0m9l&xP@ssw0rd` for the `MSSQL_SA_PASSWORD`, run the following command. Remember to pick your own complex password:

   ```console
   kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="MyC0m9l&xP@ssw0rd"
   ```

## Create storage

For a database in a Kubernetes cluster, you must use persisted storage. You can configure a [persistent volume](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) and [persistent volume claim](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#persistent-volume-claim-protection) in the Kubernetes cluster using the following steps:

1. Create a manifest to define the storage class and the persistent volume claim.  The manifest specifies the storage provisioner, parameters, and [reclaim policy](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#reclaiming). The Kubernetes cluster uses this manifest to create the persistent storage.

1. The following YAML example defines a storage class and persistent volume claim. The storage class provisioner is `azure-disk`, because this Kubernetes cluster is in Azure. The storage account type is `Standard_LRS`. The persistent volume claim is named `mssql-data`. The persistent volume claim metadata includes an annotation connecting it back to the storage class.

   ```yaml
   kind: StorageClass
   apiVersion: storage.k8s.io/v1
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

   Save the file (for example, `pvc.yaml`).

1. Create the persistent volume claim in Kubernetes, where `<path to pvc.yaml file>` is the location where you saved the file:

   ```console
   kubectl apply -f <path to pvc.yaml file>
   ```

   The persistent volume is automatically created as an Azure storage account, and bound to the persistent volume claim.

   ```output
   storageclass "azure-disk" created
   persistentvolumeclaim "mssql-data" created
   ```

1. Verify the persistent volume claim, where `<persistentVolumeClaim>` is the name of the persistent volume claim:

   ```console
   kubectl describe pvc <persistentVolumeClaim>
   ```

   In the preceding step, the persistent volume claim is named `mssql-data`. To see the metadata about the persistent volume claim, run the following command:

   ```console
   kubectl describe pvc mssql-data
   ```

   The returned metadata includes a value called `Volume`. This value maps to the name of the blob.

   :::image type="content" source="media/quickstart-sql-server-containers-kubernetes/describe-volume.png" alt-text="Screenshot of returned metadata, including Volume, which is highlighted.":::

   The value for volume matches part of the name of the blob in the following image from the Azure portal:

   :::image type="content" source="media/quickstart-sql-server-containers-kubernetes/describe-volume-portal.png" alt-text="Screenshot of the Azure portal blob name.":::

1. Verify the persistent volume.

   ```console
   kubectl describe pv
   ```

   `kubectl` returns metadata about the persistent volume that was automatically created and bound to the persistent volume claim.

## Create the deployment

The container hosting the SQL Server instance is described as a Kubernetes *deployment object*. The deployment creates a *replica set*. The replica set creates the *pod*.

You will create a manifest to describe the container, based on the SQL Server [mssql-server-linux](https://hub.docker.com/_/microsoft-mssql-server) Docker image.

- The manifest references the `mssql-server` persistent volume claim, and the `mssql` secret that you already applied to the Kubernetes cluster.
- The manifest also describes a [service](https://kubernetes.io/docs/concepts/services-networking/service/). This service is a load balancer. The load balancer guarantees that the IP address persists after SQL Server instance is recovered.
- The manifest describes resource *requests* and *limits*. These are based on the minimum [system requirements](sql-server-linux-setup.md#system).

1. Create a manifest (a YAML file) to describe the deployment. The following example describes a deployment, including a container based on the SQL Server container image.

   ```yaml
   apiVersion: apps/v1
   kind: Deployment
   metadata:
     name: mssql-deployment
   spec:
     replicas: 1
     selector:
        matchLabels:
          app: mssql
     template:
       metadata:
         labels:
           app: mssql
       spec:
         terminationGracePeriodSeconds: 30
         hostname: mssqlinst
         securityContext:
           fsGroup: 10001
         containers:
         - name: mssql
           image: mcr.microsoft.com/mssql/server:2019-latest
           resources:
             requests:
               memory: "2G"
               cpu: "2000m"
             limits:
               memory: "2G"
               cpu: "2000m"
           ports:
           - containerPort: 1433
           env:
           - name: MSSQL_PID
             value: "Developer"
           - name: ACCEPT_EULA
             value: "Y"
           - name: MSSQL_SA_PASSWORD
             valueFrom:
               secretKeyRef:
                 name: mssql
                 key: MSSQL_SA_PASSWORD
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
     name: mssql-deployment
   spec:
     selector:
       app: mssql
     ports:
       - protocol: TCP
         port: 1433
         targetPort: 1433
     type: LoadBalancer
   ```

   Copy the preceding code into a new file, named `sqldeployment.yaml`. Update the following values:

   - MSSQL_PID `value: "Developer"`: Sets the container to run SQL Server Developer edition. Developer edition isn't licensed for production data. If the deployment is for production use, set the appropriate edition (`Enterprise`, `Standard`, or `Express`). For more information, see [How to license SQL Server](https://www.microsoft.com/sql-server/sql-server-2019-pricing).

   - `persistentVolumeClaim`: This value requires an entry for `claimName:` that maps to the name used for the persistent volume claim. This tutorial uses `mssql-data`.

   - `name: MSSQL_SA_PASSWORD`: Configures the container image to set the SA password, as defined in this section.

     ```yaml
     valueFrom:
       secretKeyRef:
         name: mssql
         key: MSSQL_SA_PASSWORD
     ```

     When Kubernetes deploys the container, it refers to the secret named `mssql` to get the value for the password.

   - `securityContext`: Defines privilege and access control settings for a pod or container. In this case it's specified at the pod level, so all containers adhere to that security context. In the security context, we define the `fsGroup` with the value `10001`, which is the Group ID (GID) for the `mssql` group. This value means that all processes of the container are also part of the supplementary GID `10001` (`mssql`). The owner for volume `/var/opt/mssql` and any files created in that volume will be GID `10001` (the `mssql` group).

   > [!WARNING]  
   > By using the `LoadBalancer` service type, the SQL Server instance is accessible remotely (via the Internet) at port 1433.

   Save the file. For example, `sqldeployment.yaml`.

1. Create the deployment, where `<path to sqldeployment.yaml file>` is the location where you saved the file:

   ```console
   kubectl apply -f <path to sqldeployment.yaml file>
   ```

   The deployment and service are created. The SQL Server instance is in a container, connected to persistent storage.

   ```output
   deployment "mssql-deployment" created
   service "mssql-deployment" created
   ```

   The deployment and service are created. The SQL Server instance is in a container, connected to persistent storage.

   To view the status of the pod, type `kubectl get pod`.

   ```output
   NAME                                READY    STATUS    RESTARTS   AGE
   mssql-deployment-3813464711-h312s   1/1      Running   0          17m
   ```

   The pod has a status of `Running`. This status indicates that the container is ready. After the deployment is created, it can take a few minutes before the pod is visible. The delay is because the cluster pulls the [mssql-server-linux](https://hub.docker.com/_/microsoft-mssql-server) image from Docker hub. After the image is pulled the first time, subsequent deployments might be faster if the deployment is to a node that already has the image cached on it.

1. Verify the services are running. Run the following command:

   ```console
   kubectl get services
   ```

   This command returns services that are running, and the internal and external IP addresses for the services. Note the external IP address for the `mssql-deployment` service. Use this IP address to connect to SQL Server.

   ```output
   NAME               TYPE           CLUSTER-IP    EXTERNAL-IP     PORT(S)          AGE
   kubernetes         ClusterIP      10.0.0.1      <none>          443/TCP          52m
   mssql-deployment   LoadBalancer   10.0.113.96   52.168.26.254   1433:30619/TCP   2m
   ```

   For more information about the status of the objects in the Kubernetes cluster, run the following command. Remember to replace `<MyResourceGroup>` and `<MyKubernetesClustername>` with your resource group and Kubernetes cluster name:

   ```azurecli
   az aks browse --resource-group <MyResourceGroup> --name <MyKubernetesClustername>
   ```

1. You can also verify the container is running as non-root by running the following command, where `<nameOfSqlPod>` is the name of your SQL Server pod:

   ```console
   kubectl.exe exec <nameOfSqlPod> -it -- /bin/bash
   ```

   You will be able to see the username as `mssql` if you run `whoami`. `mssql` is a non-root user.

    ```console
    whoami
    ```

## Connect to the SQL Server instance

You can connect with an application from outside the Azure virtual network, using the `sa` account and the external IP address for the service. Use the password that you configured as the Kubernetes secret.

You can use the following applications to connect to the SQL Server instance.

- [SQL Server Managed Studio (SSMS)](./sql-server-linux-manage-ssms.md)
- [SQL Server Data Tools (SSDT)](./sql-server-linux-develop-use-ssdt.md)
- [Azure Data Studio](../azure-data-studio/quickstart-sql-server.md)

### Connect with sqlcmd

To connect with `sqlcmd`, run the following command:

```cmd
sqlcmd -S <External IP Address> -U sa -P "MyC0m9l&xP@ssw0rd"
```

Replace the following values:

- `<External IP Address>` with the IP address for the `mssql-deployment` service
- `MyC0m9l&xP@ssw0rd` with your complex password

## Verify failure and recovery

To verify failure and recovery, you can delete the pod with the following steps:

1. List the pod running SQL Server.

   ```console
   kubectl get pods
   ```

   Note the name of the pod running SQL Server.

1. Delete the pod.

   ```console
   kubectl delete pod mssql-deployment-0
   ```

   `mssql-deployment-0` is the value returned from the previous step for the pod name.

Kubernetes automatically recreates the pod to recover a SQL Server instance, and connects to the persistent storage. Use `kubectl get pods` to verify that a new pod is deployed. Use `kubectl get services` to verify that the IP address for the new container is the same.

## Clean up resources

If you don't plan on going through the tutorials that follow, clean up your unnecessary resources. Use the `az group delete` command to remove the resource group, container service, and all related resources. Replace `<MyResourceGroup>` with the name of the resource group containing your AKS cluster.

```azurecli
az group delete --name <MyResourceGroup> --yes --no-wait
```

## Next steps

- [Introduction to Kubernetes](/azure/aks/intro-kubernetes)
- [Quickstart: Run SQL Server container images with Docker](quickstart-install-connect-docker.md)
- [Deploy availability group with DH2i for SQL Server containers on AKS](tutorial-sql-server-containers-kubernetes-dh2i.md)
