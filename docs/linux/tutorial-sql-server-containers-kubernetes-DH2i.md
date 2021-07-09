---
title: Deploy Always on availability group using DH2i cluster solution for SQL Server Containers deployed on Azure Kubernetes Services (AKS)
description: This tutorial shows how to deploy a SQL Server Always On availability group with DH2i Clustering solution for SQL Server containers on Azure Kubernetes Service.
ms.custom: seo-lt-2019
author: amvin87
ms.author: amvin87
ms.reviewer: amvin87
ms.date: 09/07/2021
ms.topic: tutorial
ms.prod: sql
ms.technology: linux
---

# Deploy Always on availability group using DH2i cluster solution for SQL Server containers deployed on Azure Kubernetes Services (AKS)

This tutorial explains how to configure SQL Server Always On availability group for SQL Server Linux based containers deployed in Kubernetes cluster. In this case, Azure Kubernetes Service(AKS) is used as the kubernetes cluster and the tutorial consists of the following tasks:

1. Deploy Azure Kubernetes Service. 
2. Prepare the SQL Server & DhH2i container image. 
3. Deploy Containers on Azure Kubernetes Service. 
4. Configure the DxEnterprise cluster. 
5. Configure Read_Write_Routing_URL for listener functionality - Optional. 

## Prerequisites
1. You would need an Azure account to deploy Azure Kubernetes Service. For this tutorial a two node cluster is sufficient. 
2. Create Azure Container Registry this will be used in our deployment scripts to pull the custom image and deploy the containers to Azure Kubernetes service. You could use your choice of container registry instead of ACR to push the custom container images.

## Deploy Azure Kubernetes Service

To setup a two-node Kubernetes cluster using the Azure Kubernetes Service, please follow this [quickstart tutorial](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal#create-an-aks-cluster). Once you create the cluster you can connect to the cluster by following the steps documented in the ["connect to the cluster"](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal#connect-to-the-cluster) section of the article. 

You should now a two-node kubernetes cluster, and running a command like : kubectl get nodes from your client machine should show results similar to this:

```bash
C:\>kubectl get nodes
NAME                                STATUS   ROLES   AGE   VERSION
aks-nodepool1-75119571-vmss000000   Ready    agent   61d   v1.19.9
aks-nodepool1-75119571-vmss000001   Ready    agent   61d   v1.19.9
```

## Prepare the SQL Server & DH2i DxEnterprise custom container image

Next, we are going to prepare the custom container image which will be used in our deployment scripts to deploy SQL Server, .Net and DxEnterprise inside a container deployed using this custom image. The sample dockerfile for the deployment is shared below, you can change the SQL Server version in the below sample dockerfile. 

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

On a Linux machine, you can build the image using the commands shown below:

```bash
$mkdir dockefiles 
$cd dockerfiles 
$nano Dockerfile  
# paste the sample dockerfile content shared above 
# now build the image using the command: 
$docker build -t <tagname> . 
# you should now be able to see the new image sqlimage when you run the docker images command 
```

Now tag the image and push it to ACR using the following commands, you need to ensure that you have already logged in to the ACR using the docker login command, for more details see [login to ACR](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal#log-in-to-registry). 

```bash
$docker tag sqlimage/latest amvinacr.azurecr.io/sqlimage:latest 
#now push to the ACR repo: 
$docker push amvinacr.azurecr.io/sqlimage:latest 
#you can browse your ACR through the portal and should see the repo and the tag listed in the ACR. 
```
This ensures that you now have the custom image pushed to Azure Container Registry (ACR) and now to integrate your Azure Kubernetes Service with Azure Container Registry, please run the below command, for more details refer this (article)[https://docs.microsoft.com/en-us/azure/aks/cluster-container-registry-integration]

```bash
az aks update -n myAKSCluster -g amvindomain --attach-acr amvinacr
```

## Deploy containers on Azure Kubernetes Service

We will be deploying SQL Server containers as statefulset deployments, a sample deployment file is shared below for reference which deploys the containers on the Azure Kubernetes Service. Please note the below points: 

1. We are going to deploy three SQL Server instances, 1-Primary replica and 2-Secondary replicas. You can also optinally added labels to the node, to ensure that primary always runs on a specific node and the secondary replicas run on another node. Here are the steps to label the nodes: 
    1. Get the node names of the cluster using the command: 
        ```bash
        kubectl get nodes
        ```
    2. Now label the nodes using the commands:
        ```bash
        kubectl label node aks-nodepool1-75119571-vmss000000 <role=ags-primary> 
        kubectl label node aks-nodepool1-75119571-vmss000001 <role=ags-secondary> 
        ```
2. Before you deploy the SQL Server containers, please create the SA password secret on kubernetes using the command:

```bash
kubectl create secret generic mssql --from-literal=SA_PASSWORD="MyC0m9l&xP@ssw0rd"
```
Replace MyC0m9l&xP@ssw0rd with a complex password

3. Create a manifest (a YAML file) to describe the deployment. The following example describes our current deployment which uses the custom container image created in the preceding steps

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
        image: amvinacr.azurecr.io/sqldh2i:latest 
        env: 
        - name: ACCEPT_EULA 
          value: "Y" 
        - name: MSSQL_AGENT_ENABLED 
          value: "Y" 
        - name: MSSQL_ENABLE_HADR 
          value: "1" 
        - name: SA_PASSWORD 
          valueFrom: 
            secretKeyRef: 
              name: mssql 
              key: SA_PASSWORD 
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
        image: amvinacr.azurecr.io/sqldh2i:latest 
        env: 
        - name: ACCEPT_EULA 
          value: "Y" 
        - name: MSSQL_AGENT_ENABLED 
          value: "Y" 
        - name: MSSQL_ENABLE_HADR 
          value: "1" 
        - name: SA_PASSWORD 
          valueFrom: 
            secretKeyRef: 
              name: mssql 
              key: SA_PASSWORD 
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

Copy the preceding code into a new file, named sqldeployment.yaml, update the values like port, image and storage details as per your configuration. Create the deployment using the command below:

```bash
kubectl apply -f <Path to sqldeployment.yaml file>
```
Once the deployment completes when you run the kubectl get all command you should see result as shown below:

```bash
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
As you can see, we have three SQL Server instances each using its own storage with three loadbalancer services exposing port 1433(SQL) and 7979 (DxEnterprise Cluster) so you can connect to each of the SQL Server instance using the External-IP address and the SA_PASSWORD is the same password that you provided when creating the mssql secret in the preceding steps.

## Configure the DxEnterprise Cluster on the Containers deployed 

DxEnterprise is high availability clustering software from DH2i that supports SQL Server availability groups, including in containers. A fully featured [developer](https://dh2i.com/dxenterprise-dxodyssey-developer-edition) edition is available for non-production use. To configure the DxEnterprise cluster in containers, follow the steps in the sections "Configure the Primary and Create the Availability Group", "Join the Remaining Containers to the DxEnterprise Cluster" and "Configure the Availability Group Database(s)" respectively from this [DH2i guide](https://dh2i.com/wp-content/uploads/DxEnterprise-v21.0-SQL-Server-for-Kubernetes-StatefulSet-on-Azure-Quick-Start-Guide.pdf)

With this, you should have an Always On availability group created and database(s) added to the group supporting high availability.

## Steps to configure Read/write connection redirection: (Optional) 

Once, the availability group is created you can enable the Read/write connection redirection from secondary to primary using the below steps. Refer, Read_write_routing_URL to know more. This fulfils the listener requirements  

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

## Next Steps

1. [Deploy SQL Server containers on Azure Kubernetes Service](https://docs.microsoft.com/en-us/sql/linux/tutorial-sql-server-containers-kubernetes?view=sql-server-ver15)
2. [Deploy SQL Server Read Scale AG on SQL Server Linux based containers deployed on kubernetes](https://techcommunity.microsoft.com/t5/sql-server/configure-sql-server-ag-read-scale-for-sql-containers-deployed/ba-p/2224742)