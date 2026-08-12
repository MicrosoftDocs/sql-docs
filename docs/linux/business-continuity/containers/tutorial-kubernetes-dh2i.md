---
title: Deploy Availability Groups with DH2i DxEnterprise on Kubernetes
description: Set up an availability group in SQL Server on Kubernetes using DH2i DxEnterprise.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - linux-related-content
  - sfi-ropc-blocked
---
# Deploy availability groups with DH2i DxEnterprise on Kubernetes

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Always On availability groups (AGs) with DH2i DxEnterprise for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Linux based containers deployed to an Azure Kubernetes Service (AKS) Kubernetes cluster. You can choose between a [sidecar configuration](/azure/architecture/patterns/sidecar) (preferred), or build your own custom container image.

> [!NOTE]  
> Microsoft supports data movement, AG, and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] components. DH2i supports the DxEnterprise product, which includes cluster and quorum management.

# [Sidecar configuration](#tab/sidecar)

Learn how to deploy a StatefulSet and use DH2i DxEnterprise to create and configure an AG. This tutorial consists of the following steps.

> [!div class="checklist"]
> - Create a headless service configuration
> - Create a StatefulSet configuration with [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and DxEnterprise in the same pod as a sidecar container
> - Create and configure a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] AG, adding the secondary replicas
> - Create a database in the AG, and test failover

## Prerequisites

This tutorial shows an example of an AG with three replicas. You need:

- An Azure Kubernetes Service (AKS) or Kubernetes cluster.
- A valid DxEnterprise license with AG features and tunnels enabled. For more information, see the [developer edition](https://dh2i.com/trial/) for nonproduction usage, or [DxEnterprise software](https://dh2i.com/dxenterprise-high-availability/) for production workloads.

## Create the headless service

1. In a Kubernetes cluster, headless services allow your pods to connect to one another using hostnames.

   To create the headless service, create a YAML file called `headless_services.yaml` with the following sample content.

   ```yaml
   #Headless services for local connections/resolution
   apiVersion: v1
   kind: Service
   metadata:
     name: dxemssql-0
   spec:
     clusterIP: None
     selector:
       statefulset.kubernetes.io/pod-name: dxemssql-0
     ports:
       - name: dxl
         protocol: TCP
         port: 7979
       - name: dxc-tcp
         protocol: TCP
         port: 7980
       - name: dxc-udp
         protocol: UDP
         port: 7981
       - name: sql
         protocol: TCP
         port: 1433
       - name: listener
         protocol: TCP
         port: 14033
   ---
   apiVersion: v1
   kind: Service
   metadata:
     name: dxemssql-1
   spec:
     clusterIP: None
     selector:
       statefulset.kubernetes.io/pod-name: dxemssql-1
     ports:
       - name: dxl
         protocol: TCP
         port: 7979
       - name: dxc-tcp
         protocol: TCP
         port: 7980
       - name: dxc-udp
         protocol: UDP
         port: 7981
       - name: sql
         protocol: TCP
         port: 1433
       - name: listener
         protocol: TCP
         port: 14033
   ---
   apiVersion: v1
   kind: Service
   metadata:
     name: dxemssql-2
   spec:
     clusterIP: None
     selector:
       statefulset.kubernetes.io/pod-name: dxemssql-2
     ports:
       - name: dxl
         protocol: TCP
         port: 7979
       - name: dxc-tcp
         protocol: TCP
         port: 7980
       - name: dxc-udp
         protocol: UDP
         port: 7981
       - name: sql
         protocol: TCP
         port: 1433
       - name: listener
         protocol: TCP
         port: 14033
   ```

1. Run the following command to apply the configuration.

   ```bash
   kubectl apply -f headless_services.yaml
   ```

## Create the StatefulSet

1. Create a StatefulSet YAML file with following sample content, and name it `dxemssql.yaml`.

   This StatefulSet configuration creates three DxEMSSQL replicas that use persistent volume claims to store their data. Each pod in this StatefulSet comprises two containers: a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] container and a DxEnterprise container. These containers start separately in a *sidecar* configuration, but DxEnterprise manages the AG replica in the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] container.

   ```yaml
   #DxEnterprise + MSSQL StatefulSet
   apiVersion: apps/v1
   kind: StatefulSet
   metadata:
     name: dxemssql
   spec:
     serviceName: "dxemssql"
     replicas: 3
     selector:
       matchLabels:
         app: dxemssql
     template:
       metadata:
         labels:
           app: dxemssql
       spec:
         securityContext:
           fsGroup: 10001
         containers:
           - name: sql
             image: mcr.microsoft.com/mssql/server:2022-latest
             env:
               - name: ACCEPT_EULA
                 value: "Y"
               - name: MSSQL_ENABLE_HADR
                 value: "1"
               - name: MSSQL_SA_PASSWORD
                 valueFrom:
                   secretKeyRef:
                     name: mssql
                     key: MSSQL_SA_PASSWORD
             volumeMounts:
               - name: mssql
                 mountPath: "/var/opt/mssql"
           - name: dxe
             image: docker.io/dh2i/dxe
             env:
               - name: MSSQL_SA_PASSWORD
                 valueFrom:
                   secretKeyRef:
                     name: mssql
                     key: MSSQL_SA_PASSWORD
             volumeMounts:
               - name: dxe
                 mountPath: "/etc/dh2i"
     volumeClaimTemplates:
       - metadata:
           name: dxe
         spec:
           accessModes:
             - ReadWriteOnce
           resources:
             requests:
               storage: 1Gi
       - metadata:
           name: mssql
         spec:
           accessModes:
             - ReadWriteOnce
           resources:
             requests:
               storage: 1Gi
   ```

1. Create a credential for the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance.

   ```bash
   kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="<password>"
   ```

   [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

1. Apply the StatefulSet configuration.

   ```bash
   kubectl apply -f dxemssql.yaml
   ```

1. Verify the status of the pods, and proceed to the next step when the pod's status becomes `running`.

   ```bash
   kubectl get pods
   kubectl describe pods
   ```

## Create availability group and test failover

For details on creating and configuring AG, adding replicas, and testing failover, see [SQL Server Availability Groups in Kubernetes](https://support.dh2i.com/docs/guides/dxenterprise/containers/kubernetes/mssql-ag-k8s-statefulset-qsg/#create-the-availability-group).

# [Custom image](#tab/custom)

This tutorial uses Azure Kubernetes Service (AKS) as the Kubernetes cluster and consists of the following tasks.

> [!div class="checklist"]
> - Deploy Azure Kubernetes Service
> - Prepare the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and DH2i custom container image
> - Deploy containers on Azure Kubernetes Service
> - Configure the DxEnterprise cluster
> - Configure `Read_Write_Routing_URL` for listener functionality (optional)

For more information about DxEnterprise, see [DH2i DxEnterprise](https://dh2i.com/dxenterprise-high-availability/).

## Prerequisites

- To deploy Azure Kubernetes Service, you must have an [Azure account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn). A two-node cluster is a good starting point for this tutorial.

- [Create an Azure container registry using the Azure portal](/azure/container-registry/container-registry-get-started-portal). The deployment scripts use this registry to retrieve the custom image and deploy the containers to Azure Kubernetes. Instead of Azure Container Registry (ACR), you can use your preferred container registry to push the custom container images.

## Deploy Azure Kubernetes Service

Follow this [quickstart tutorial](/azure/aks/kubernetes-walkthrough-portal#create-an-aks-cluster) to set up a two-node Kubernetes cluster by using the Azure Kubernetes Service. After you create the cluster, connect to it by following the steps in the [Connect to the cluster](/azure/aks/kubernetes-walkthrough-portal#connect-to-the-cluster) section.

You should now have a two-node Kubernetes cluster. Run the following command from your client machine.

```bash
kubectl get nodes
```

You should see results similar to the following output.

```output
NAME                                STATUS   ROLES   AGE   VERSION
aks-nodepool1-75119571-vmss000000   Ready    agent   61d   v1.19.9
aks-nodepool1-75119571-vmss000001   Ready    agent   61d   v1.19.9
```

## Prepare the SQL Server and DH2i DxEnterprise custom container image

Create the custom container image for the deployment manifest. The custom container image deploys [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], .NET, and DxEnterprise in a container. This section provides a sample Dockerfile. You can modify it to meet your needs, such as changing the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] version.

For more information about Docker and Dockerfiles, see the [Docker documentation](https://docs.docker.com/get-started/).

```dockerfile
FROM mcr.microsoft.com/mssql/server:2019-latest
USER root

#Install dotnet
RUN apt-get update \
   && ACCEPT_EULA=Y apt-get upgrade -y \
   && apt-get install -y wget \
   && wget --no-dns-cache https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
   && dpkg -i packages-microsoft-prod.deb \
   && apt-get update \
   && apt-get install -y dotnet-runtime-6.0 zip \
   && dpkg --purge packages-microsoft-prod \
   && apt-get purge -y wget \
   && apt-get clean \
   && rm packages-microsoft-prod.deb \
   && rm -rf /var/lib/apt/lists/*

#Download and unpack DxE, setup permissions
ADD https://repos.dh2i.com/container/ ./dxe.tgz
RUN tar zxvf dxe.tgz && rm dxe.tgz \
   && chown -R mssql /var/opt/mssql \
   && chmod -R 777 /opt/dh2i /etc/dh2i

#Finish setup
EXPOSE 7979 7985
ENV DX_HAS_MSSQLSERVER=1
USER mssql
ENTRYPOINT ["/opt/dh2i/sbin/dxstart.sh"]
```

1. On a Linux machine, run the following commands to build the image.

   ```bash
   mkdir dockerfiles
   cd dockerfiles
   nano Dockerfile
   ```

1. Paste the preceding sample Dockerfile content into this new file, and then save it.

1. Build the image with the following command. Replace `<tagname>` with `sqldh2i/latest`.

   ```bash
   docker build -t <tagname> .
   ```

   You can now see the new image, `sqldh2i`, when you run the `docker images` command.

Tag the image and push it to Azure Container Registry (ACR) with the following commands. Make sure you're already signed in to ACR with the `docker login` command. For more information, see [Sign in to registry](/azure/container-registry/container-registry-get-started-portal#sign-in-to-registry).

1. Tag the image with the following command.

   ```bash
   docker tag sqldh2i/latest <registry-name>.azurecr.io/sqldh2i:latest
   ```

1. Push the image to the ACR repo.

   ```bash
   docker push <registry-name>.azurecr.io/sqldh2i:latest
   ```

   You can browse your ACR in the portal to see the repo and tag listed.

These steps ensure that the custom image is pushed to Azure Container Registry (ACR), and that you can now integrate your Azure Kubernetes Service (AKS) with ACR by running the following command. For more information, see [Authenticate with Azure Container Registry (ACR) from Azure Kubernetes Service (AKS)](/azure/aks/cluster-container-registry-integration).

Use the short name for `<registry-name>`. The following command doesn't accept the fully qualified name of the registry.

```azurecli
az aks update -n myAKSCluster -g <myResourceGroup> --attach-acr <registry-name>
```

## Deploy containers on Azure Kubernetes Service

This process deploys [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] containers as StatefulSet deployments. For reference, this article provides a sample deployment file later that deploys the containers on Azure Kubernetes Service.

1. Set up three [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances: one as a primary replica, and two as secondary replicas. Optionally, add labels to the nodes to ensure that the primary replica always runs on one node and the secondary replicas run on another. The following steps show how to label the nodes.

   1. Get the node names of the cluster with the following command.

      ```bash
      kubectl get nodes
      ```

   1. Label the nodes with the following commands.

      ```bash
      kubectl label node aks-nodepool1-75119571-vmss000000 role=ags-primary
      ```

      ```bash
      kubectl label node aks-nodepool1-75119571-vmss000001 role=ags-secondary
      ```

1. Before you deploy the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] containers, create the `sa` password secret on Kubernetes with the following command. Replace `<password>` with your own complex password.

   ```bash
   kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="<password>"
   ```

1. Create a manifest (a YAML file) to describe the deployment. The following example shows the deployment, which uses the custom container image you created in the preceding steps.

   > [!NOTE]  
   > Modify this example to fit your environment. For example, replace port, image, and storage details.

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
   apiVersion: apps/v1
   kind: StatefulSet
   metadata:
     name: mssql-pri
     labels:
       app: mssql
   spec:
     serviceName: "mssql-pri"
     replicas: 1
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
           image: <registry-name>.azurecr.io/sqldh2i:latest
           env:
           - name: ACCEPT_EULA
             value: "Y"
           - name: MSSQL_AGENT_ENABLED
             value: "Y"
           - name: MSSQL_ENABLE_HADR
             value: "1"
           - name: MSSQL_SA_PASSWORD
             valueFrom:
               secretKeyRef:
                 name: mssql
                 key: MSSQL_SA_PASSWORD
           volumeMounts:
           - name: dxe
             mountPath: "/etc/dh2i"
           - name: mssql
             mountPath: "/var/opt/mssql"
         nodeSelector:
          role: ags-primary
     volumeClaimTemplates:
     - metadata:
         name: dxe
       spec:
         accessModes:
           - ReadWriteOnce
         resources:
           requests:
             storage: 1Gi
     - metadata:
         name: mssql
       spec:
         accessModes:
           - ReadWriteOnce
         resources:
           requests:
             storage: 8Gi
   ---
   apiVersion: apps/v1
   kind: StatefulSet
   metadata:
     name: mssql-sec
     labels:
       app: mssql
   spec:
     serviceName: "mssql-sec"
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
           image: <registry-name>.azurecr.io/sqldh2i:latest
           env:
           - name: ACCEPT_EULA
             value: "Y"
           - name: MSSQL_AGENT_ENABLED
             value: "Y"
           - name: MSSQL_ENABLE_HADR
             value: "1"
           - name: MSSQL_SA_PASSWORD
             valueFrom:
               secretKeyRef:
                 name: mssql
                 key: MSSQL_SA_PASSWORD
           volumeMounts:
           - name: dxe
             mountPath: "/etc/dh2i"
           - name: mssql
             mountPath: "/var/opt/mssql"
         nodeSelector:
          role: ags-secondary
     volumeClaimTemplates:
     - metadata:
         name: dxe
       spec:
         accessModes:
           - ReadWriteOnce
         resources:
           requests:
             storage: 1Gi
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
     name: mssql-pri-0
   spec:
     type: LoadBalancer
     selector:
       statefulset.kubernetes.io/pod-name: mssql-pri-0
     ports:
     - name: sql
       protocol: TCP
       port: 1433
       targetPort: 1433
     - name: dxe
       protocol: TCP
       port: 7979
       targetPort: 7979
   ---
   apiVersion: v1
   kind: Service
   metadata:
     name: mssql-sec-0
   spec:
     type: LoadBalancer
     selector:
       statefulset.kubernetes.io/pod-name: mssql-sec-0
     ports:
     - name: sql
       protocol: TCP
       port: 1433
       targetPort: 1433
     - name: dxe
       protocol: TCP
       port: 7979
       targetPort: 7979
   ---
   apiVersion: v1
   kind: Service
   metadata:
     name: mssql-sec-1
   spec:
     type: LoadBalancer
     selector:
       statefulset.kubernetes.io/pod-name: mssql-sec-1
     ports:
     - name: sql
       protocol: TCP
       port: 1433
       targetPort: 1433
     - name: dxe
       protocol: TCP
       port: 7979
       targetPort: 7979
   ```

   1. Copy the preceding code into a new file called `sqldeployment.yaml`.

   1. Create the deployment with the following command.

      ```bash
      kubectl apply -f <Path to sqldeployment.yaml file>
      ```

   1. Once the deployment completes, run the `kubectl get all` command. You should see the following results.

      ```output
      C:\>kubectl get all
      NAME              READY   STATUS    RESTARTS   AGE
      pod/mssql-pri-0   1/1     Running   0          33h
      pod/mssql-sec-0   1/1     Running   0          33h
      pod/mssql-sec-1   1/1     Running   0          33h

      NAME                  TYPE           CLUSTER-IP     EXTERNAL-IP     PORT(S)                         AGE
      service/kubernetes    ClusterIP      10.0.0.1       <none>          443/TCP                         33h
      service/mssql-pri-0   LoadBalancer   10.0.134.183   20.204.22.235   1433:30678/TCP,7979:31136/TCP   33h
      service/mssql-sec-0   LoadBalancer   10.0.74.50     20.204.23.32    1433:31009/TCP,7979:30114/TCP   33h
      service/mssql-sec-1   LoadBalancer   10.0.63.62     20.204.74.9     1433:31616/TCP,7979:32190/TCP   33h

      NAME                         READY   AGE
      statefulset.apps/mssql-pri   1/1     33h
      statefulset.apps/mssql-sec   2/2     33h
      ```

You should now have three [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances, each with its own storage and services, exposing ports 1433 ([!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]) and 7979 (DxEnterprise Cluster). You can connect to each [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance at its External-IP address. The `sa` password is the same password you provided when creating the `mssql` secret in the preceding steps.

## Configure the DxEnterprise cluster on the deployed containers

DxEnterprise is high availability clustering software from DH2i that supports AGs, including in containers. A fully featured [developer](https://dh2i.com/trial) edition is available for nonproduction use. To configure the DxEnterprise cluster in containers, follow the steps in this [DH2i guide](https://support.dh2i.com/docs/guides/dxenterprise/azure/ms-k8s-supplemental-guide/#configure-the-primary-and-create-the-availability-group).

These steps create an AG and add databases to the group to support high availability.

> [!NOTE]  
> You can deploy a [basic Always On availability group](../../../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md) with [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Standard edition, but basic AGs support only two replicas plus one configuration-only replica required for successful automatic failover. For more information about failover with a configuration-only replica, see [Configuration-only replica and quorum](../availability-groups/overview.md#configuration-only-replica-and-quorum). To add a configuration-only replica for containers, see the [DH2i documentation](https://support.dh2i.com/docs/guides/dxenterprise/azure/ms-k8s-supplemental-guide/#configure-the-primary-and-create-the-availability-group) and pass the availability mode in the `dxcli add-ags-node` command as `configuration_only`.

---

## Steps to configure availability group listener (optional)

You can also configure an AG listener, with the following steps.

1. Ensure you created the AG listener with DxEnterprise following the optional step near the end of the [DH2i documentation](https://support.dh2i.com/docs/guides/dxenterprise/azure/ms-k8s-supplemental-guide/#add-availability-group-databases-and-a-listener).

1. In Kubernetes, you can optionally create *static* IP addresses. A static IP address ensures that if you delete and recreate the listener service, the external IP address assigned to your listener service doesn't change. Follow the steps to [create a static IP address](/azure/aks/static-ip#create-a-static-ip-address) in Azure Kubernetes Service (AKS).

1. After you create an IP address, assign it and create the load balancer service with the following YAML sample.

   ```yaml
   apiVersion: v1
   kind: Service
   metadata:
     name: agslistener
   spec:
     type: LoadBalancer
     loadBalancerIP: <static-IP-address>
     selector:
       app: mssql
     ports:
     - protocol: TCP
       port: 44444
       targetPort: 44444
   ```

## Steps to configure read/write connection redirection (optional)

After you create the AG, enable read/write connection redirection from the secondary to primary. For more information, see [Secondary to primary replica read/write connection redirection (Always On Availability Groups)](../../../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).

```sql
USE master;
GO

ALTER AVAILABILITY GROUP [AGS1]
MODIFY REPLICA ON N'<name of the primary replica>'
WITH (SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL));
GO

ALTER AVAILABILITY GROUP [AGS1]
MODIFY REPLICA ON N'<name of the secondary-0 replica>'
WITH (SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL));
GO

ALTER AVAILABILITY GROUP [AGS1]
MODIFY REPLICA ON N'<name of the secondary-1 replica>'
WITH (SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL));
GO

ALTER AVAILABILITY GROUP [AGS1]
MODIFY REPLICA ON N'<name of the primary replica>'
WITH (PRIMARY_ROLE(
    READ_WRITE_ROUTING_URL = 'TCP://<External IP address of primary -0>:1433'
));
GO

ALTER AVAILABILITY GROUP [AGS1]
MODIFY REPLICA ON N'<name of the secondary-0 replica>'
WITH (PRIMARY_ROLE(
    READ_WRITE_ROUTING_URL = 'TCP://<External IP address of secondary -0>:1433'
));
GO

ALTER AVAILABILITY GROUP [AGS1]
MODIFY REPLICA ON N'<name of the secondary-1 replica>'
WITH (PRIMARY_ROLE(
    READ_WRITE_ROUTING_URL = 'TCP://<External IP address of secondary -1>:1433'
));
GO
```

## Related content

- [Deploy availability groups on Kubernetes with DH2i DxOperator on Azure Kubernetes Service](tutorial-kubernetes-dxoperator.md)
- [Quickstart: Deploy a SQL Server container cluster on Azure or Red Hat OpenShift](../../install-upgrade/quickstart-containers-azure.md)
- [Deploy SQL Server Linux containers on Kubernetes with StatefulSets](../../containers/kubernetes-best-practices-statefulsets.md)
- [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](../../containers/tutorial-adutil.md)
