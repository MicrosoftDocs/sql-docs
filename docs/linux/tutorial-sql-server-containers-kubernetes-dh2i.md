---
title: Deploy availability group with DH2i cluster on Azure Kubernetes Services (AKS)
description: This tutorial shows how to deploy a SQL Server Always On availability group with DH2i Clustering solution for SQL Server containers on Azure Kubernetes Service (AKS).
author: amitkh-msft
ms.author: amitkh
ms.reviewer: amitkh, vanto
ms.date: 04/01/2022
ms.topic: tutorial
ms.service: sql
ms.subservice: linux
ms.custom:
  - intro-deployment
---

# Deploy availability group with DH2i for SQL Server containers on AKS

This tutorial explains how to configure SQL Server Always On availability group for SQL Server Linux based containers deployed in a Kubernetes cluster. In this tutorial, Azure Kubernetes Service (AKS) is used as the kubernetes cluster and the tutorial consists of the following tasks:

> [!div class="checklist"]
> - Deploy Azure Kubernetes Service
> - Prepare the SQL Server & DH2i custom container image
> - Deploy containers on Azure Kubernetes Service
> - Configure the DxEnterprise cluster
> - Configure Read_Write_Routing_URL for listener functionality - Optional

For more information about DxEnterprise, see [DH2i DxEnterprise](https://dh2i.com/dxenterpriseha_ms/).

> [!NOTE]
> Microsoft supports data movement, availability group, and SQL Server components. DH2i is responsible for support of the DxEnterprise product, which includes cluster and quorum management.

## Prerequisites

- To deploy Azure Kubernetes Service, you must have an [Azure account](https://azure.microsoft.com/free/). A two-node cluster is a good starting point for this tutorial.
- Create [Azure Container Registry](/azure/container-registry/container-registry-get-started-portal). This will be used in our deployment scripts to retrieve the custom image and deploy the containers to Azure Kubernetes. Instead of Azure Container Registry (ACR), you could use your preferred container registry to push the custom container images.

## Deploy Azure Kubernetes Service

Follow this [quickstart tutorial](/azure/aks/kubernetes-walkthrough-portal#create-an-aks-cluster) to set up a two-node Kubernetes cluster using the Azure Kubernetes Service. After you've created the cluster, you can connect to it by following the steps outlined in the [Connect to the cluster](/azure/aks/kubernetes-walkthrough-portal#connect-to-the-cluster) section.

You should now have a two-node kubernetes cluster. Running `kubectl get nodes` from your client machine using a console app should yield results similar to the following:

```bash
C:\>kubectl get nodes
NAME                                STATUS   ROLES   AGE   VERSION
aks-nodepool1-75119571-vmss000000   Ready    agent   61d   v1.19.9
aks-nodepool1-75119571-vmss000001   Ready    agent   61d   v1.19.9
```

## Prepare the SQL Server & DH2i DxEnterprise custom container image

Create the custom container image that will be used in our deployment manifests. The custom container image will deploy SQL Server, .NET, and DxEnterprise in a container. The deployment sample dockerfile is provided below. You can modify it to meet your needs, such as changing the SQL Server version.

For more information on Docker and using dockerfiles, see the [docker documentation](https://docs.docker.com/get-started/).

```bash
FROM mcr.microsoft.com/mssql/server:2019-latest
USER root

#Install dotnet
RUN apt-get update \
   && ACCEPT_EULA=Y apt-get upgrade -y \
   && apt-get install -y wget \
   && wget --no-dns-cache https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
   && dpkg -i packages-microsoft-prod.deb \
   && apt-get update \
   && apt-get install -y dotnet-runtime-3.1 zip \
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

On a Linux machine, run the following commands to build the image:

```bash
$mkdir dockerfiles 
$cd dockerfiles 
$nano Dockerfile  
# paste the sample dockerfile content shared above 
# now build the image using the command:
# <tagname> should be sqldh2i/latest
$docker build -t <tagname> .
# you should now be able to see the new image, sqldh2i when you run the docker images command 
```

Tag the image and push it to Azure Container Registry (ACR) using the commands below. Make sure you've already logged into Azure Container Registry (ACR) using the docker login command. For more information, see [login to ACR](/azure/container-registry/container-registry-get-started-portal#log-in-to-registry).

```bash
$docker tag sqldh2i/latest <registry-name>.azurecr.io/sqldh2i:latest 
#now push to the ACR repo: 
$docker push <registry-name>.azurecr.io/sqldh2i:latest 
#you can browse your ACR through the portal and should see the repo and the tag listed in the ACR. 
```

This ensures that the custom image has been pushed to Azure Container Registry (ACR) and that you can now integrate your Azure Kubernetes Service (AKS) with ACR by running the following command. For more information, see this [Integrate ACR with an AKS cluster](/azure/aks/cluster-container-registry-integration).

Use the short name for `<registry-name>`. The full qualified name of the registry is not accepted in the below command.

```bash
az aks update -n myAKSCluster -g <myResourceGroup> --attach-acr <registry-name>
```

## Deploy containers on Azure Kubernetes Service

We'll deploy SQL Server containers as StatefulSet deployments; a sample deployment file that deploys the containers on the Azure Kubernetes Service is provided below for reference.

1. We'll set up three SQL Server instances. One as a primary replica and two as secondary replicas. You can optionally add labels to the node to ensure that the primary replica always runs on one node and the secondary replicas run on another. The following are the steps for labeling the nodes:

    1. Get the node names of the cluster using the command:

       ```bash
       kubectl get nodes
       ```

    1. Label the nodes using the following commands:

       ```bash
       kubectl label node aks-nodepool1-75119571-vmss000000 <role=ags-primary>
       ```

       ```bash
       kubectl label node aks-nodepool1-75119571-vmss000001 <role=ags-secondary> 
       ```

1. Create the SA password secret on kubernetes before deploying the SQL Server containers using the following command:

    ```bash
    kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="MyC0m9l&xP@ssw0rd"
    ```

    Replace `MyC0m9l&xP@ssw0rd` with your own complex password.

1. Create a manifest (a YAML file) to describe the deployment. The example below shows our current deployment, which makes use of the custom container image created in the preceding steps.

    > [!NOTE]
    > The below is an example and will need to be modified to fit your environment, such as replacing port, image, and storage details.

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

    Copy the preceding code into a new file called **sqldeployment.yaml**.

    Create the deployment using the command below:

    ```bash
    kubectl apply -f <Path to sqldeployment.yaml file>
    ```

    Once the deployment completes, run the **kubectl get all** command. You should see result as shown below:

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

As you can see, we have three SQL Server instances, each with its own storage and services exposing ports 1433 (SQL) and 7979 (DxEnterprise Cluster). You can connect to each SQL Server instance using the External-IP address. The SA PASSWORD is the same password you provided when creating the mssql secret in the preceding steps.

## Configure the DxEnterprise cluster on the containers deployed

DxEnterprise is high availability clustering software from DH2i that supports SQL Server availability groups, including in containers. A fully featured [developer](https://dh2i.com/dxenterprise-dxodyssey-developer-edition) edition is available for non-production use. To configure the DxEnterprise cluster in containers, follow the steps in this [DH2i guide](https://support.dh2i.com/docs/guides/dxenterprise/azure/ms-k8s-supplemental-guide/#configure-the-primary-and-create-the-availability-group).

With this, you should have an Always On availability group created and database(s) added to the group supporting high availability.

> [!NOTE]
> You can deploy [basic Always On availability group](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md) with SQL Server standard edition, but as you may be aware, one of the limitations of basic availability groups is that you are limited to only having two replicas and one additional configuration only replica required for successful automatic failover. Refer to the [documentation](./sql-server-linux-availability-group-overview.md#configuration-only-replica-and-quorum) for more information on failover with configuration only replica. You can add configuration only replica for containers as well, and to do so, please refer to the [DH2i documentation](https://support.dh2i.com/docs/guides/dxenterprise/azure/ms-k8s-supplemental-guide/#configure-the-primary-and-create-the-availability-group), making sure to pass the availability mode in the 'dxcli add-ags-node' command as 'configuration_only'.


## Steps to configure Always On availability group listener: (Optional)

You can also configure an Always On availability group listener; to do so, follow the steps below:

1. Ensure you've created the AG listener using DxEnterprise as outlined in the optional step near the end of the [DH2i documentation](https://support.dh2i.com/docs/guides/dxenterprise/azure/ms-k8s-supplemental-guide/#add-availability-group-databases-and-a-listener).

2. In Kubernetes, you can optionally create static IP addresses. Creating static IP addresses ensures that if the listener service is deleted and recreated, the external IP address assigned to your listener service does not change and thus remains static. Follow the steps outlined [here](/azure/aks/static-ip#create-a-static-ip-address) to create a static IP address in Azure Kubernetes Service (AKS).
   
3. After you have created an IP address, you assign that IP address and create the load balancer service, as shown in the sample yaml below:

   ```bash
   apiVersion: v1
   kind: Service
   metadata:
     name: agslistener
   spec:
     type: LoadBalancer
     loadBalancerIP: 52.140.117.62
     selector:
       app: mssql
     ports:
     - protocol: TCP
       port: 44444
       targetPort: 44444
   ```

## Steps to configure read/write connection redirection: (Optional)

After you've created the availability group, you can enable read/write connection redirection from the secondary to primary by following the steps below. For more information, see [Secondary to primary replica read/write connection redirection](../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).

```bash
USE [master] 
GO 
ALTER AVAILABILITY GROUP [ag_name] 
MODIFY REPLICA ON N'<name of the primary replica>' WITH (SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL)) 
GO 
USE [master] 
GO 
ALTER AVAILABILITY GROUP [AGS1] 
MODIFY REPLICA ON N'<name of the secondary-0 replica>' WITH (SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL)) 
GO 
USE [master] 
GO 
ALTER AVAILABILITY GROUP [AGS1] 
MODIFY REPLICA ON N'<name of the secondary-1 replica>' WITH (SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL)) 
GO 
USE [master] 
GO 
ALTER AVAILABILITY GROUP AGS1 MODIFY REPLICA ON N'<name of the primary replica>' WITH    
(PRIMARY_ROLE (READ_WRITE_ROUTING_URL = 'TCP://<External IP address of primary -0>:1433')) 
GO 
USE [master] 
GO 
ALTER AVAILABILITY GROUP AGS1 MODIFY REPLICA ON N'<name of the secondary-0 replica>' WITH    
(PRIMARY_ROLE (READ_WRITE_ROUTING_URL = 'TCP://<External IP address of secondary -0>:1433')) 
GO 
USE [master] 
GO 
ALTER AVAILABILITY GROUP AGS1 MODIFY REPLICA ON N'<name of the secondary-1 replica>' WITH    
(PRIMARY_ROLE (READ_WRITE_ROUTING_URL = 'TCP://<External IP address of secondary -1>:1433')) 
GO 
```

## Next steps

- [Deploy SQL Server containers on Azure Kubernetes Service](quickstart-sql-server-containers-kubernetes.md)
- [Deploy SQL Server Read Scale AG on SQL Server Linux based containers deployed on kubernetes](https://techcommunity.microsoft.com/t5/sql-server/configure-sql-server-ag-read-scale-for-sql-containers-deployed/ba-p/2224742)
