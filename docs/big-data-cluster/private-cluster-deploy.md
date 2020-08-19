---
title: Deploy private cluster
titleSuffix: SQL Server Big Data Cluster
description: Learn how to deploy a SQL Server Big Data Clusters with AKS private cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 08/20/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy BDC in Azure Kubernetes Service (AKS) private cluster

This article explains how to deploy SQL Server Big Data Clusters on Azure Kubernetes Service (AKS) private cluster. This configuration supports restricted access to public IP addresses.

A private deployment provides the following benefits:

* Restricted use of public IP addresses
* Network traffic between application servers and cluster node pools remains only on the private network
* Custom configuration for mandatory egress traffics to fit specific requirements

This article demonstrates how to use an AKS private cluster to restrict the use of Public IP address while respective security strings have applied.

## Deploy private BDC cluster with AKS private cluster

To get started, create a [AKS private cluster](/azure/aks/private-clusters) to restrict the network traffic between API server and node pools. The traffic remains on the private network only.  Because the solution uses an AKS private cluster, the control plane or API server has internal IP addresses.  
This section shows you deploy a private BDC cluster in Azure Kubernetes Service (AKS) with advanced networking (CNI).

## Create a private AKS cluster with advanced networking

```console

export REGION_NAME=northeurope
export RESOURCE_GROUP=private-bdc-aks-rg
export SUBNET_NAME=aks-subnet
export VNET_NAME=bdc-vnet
export AKS_NAME=bdcaksprivatecluster
 
az group create -n $RESOURCE_GROUP -l $REGION_NAME
 
az network vnet create \
    --resource-group $RESOURCE_GROUP \
    --location $REGION_NAME \
    --name $VNET_NAME \
    --address-prefixes 10.0.0.0/8 \
    --subnet-name $SUBNET_NAME \
    --subnet-prefix 10.1.0.0/16
 

SUBNET_ID=$(az network vnet subnet show \
    --resource-group $RESOURCE_GROUP \
    --vnet-name $VNET_NAME \
    --name $SUBNET_NAME \
    --query id -o tsv)
 
echo $SUBNET_ID
## will be displayed something similar as the following: 
/subscriptions/xxxx-xxxx-xxx-xxxx-xxxxxxxx/resourceGroups/your-bdc-rg/providers/Microsoft.Network/virtualNetworks/your-aks-vnet/subnets/your-aks-subnet
```

### Create AKS private cluster with advanced networking (CNI)

To be able to get to next step, you need to provision an AKS cluster with Standard Load Balancer with private cluster feature enabled.  Your command will look like as follows : 

```console
az aks create \
    --resource-group $RESOURCE_GROUP \
    --name $AKS_NAME \
    --load-balancer-sku standard \
    --enable-private-cluster \
    --network-plugin azure \
    --vnet-subnet-id $SUBNET_ID \
    --docker-bridge-address 172.17.0.1/16 \
    --dns-service-ip 10.2.0.10 \
    --service-cidr 10.2.0.0/24 \
    --node-vm-size Standard_D13_v2 \
    --node-count 2 \
    --generate-ssh-keys
```

After a successful deployment, you can go to `<MC_yourakscluster>` resource group and you’ll find the kube-apiserver is a private endpoint (as showed in the following).  
 
## Connect to an AKS cluster

```console
az aks get-credentials -n $AKS_NAME -g $RESOURCE_GROUP
```

After connecting to an AKS cluster, you can start to deploy BDC, and you can prepare the environment variable and initiate a deployment : 

```console
azdata bdc config init --source aks-dev-test --target private-bdc-aks --force
```console

## Generate and config BDC custom deployment profile 
```console
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.docker.imageTag=2019-CU6-ubuntu-16.04"
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.storage.data.className=default"
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.storage.logs.className=default"

azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.endpoints[1].serviceType=NodePort"

azdata bdc config replace -c private-bdc-aks /bdc.json -j "$.spec.resources.master.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -c private-bdc-aks /bdc.json -j "$.spec.resources.gateway.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -c private-bdc-aks /bdc.json -j "$.spec.resources.appproxy.spec.endpoints[0].serviceType=NodePort"
```

## Deploy private SQL Server Big Data Cluster with HA 

In case you are  [deploying a SQL Server Big Data Cluster ( SQL-BDC ) with high availability ( HA )]( deployment-high-availability.md),  you’ll be using deploy aks-dev-test-ha deployment profile. After a successful deployment, you can use the same `kubectl get svc` command and you’ll see an additional ‘master-secondary-svc’ service is created which  you need to configure ServiceType as NodePort ( as the following ) . Other steps will be similar to what mentioned in previous section. 

```console
azdata bdc config replace -c private-bdc-aks /bdc.json -j "$.spec.resources.master.spec.endpoints[1].serviceType= NodePort"
```

## Deploy BDC in AKS private cluster

```console
export AZDATA_USERNAME=<your bdcadmin username>
export AZDATA_PASSWORD=< your bdcadmin password>
export ACCEPT_EULA=yes  #accept agreement

azdata bdc create --config-profile private-bdc-aks --accept-eula yes
```

## Check deployment status

The deployment will take a few minutes and you can use the following command to check the deployment status: 

```console
 Kubectl get pods -n mssql-cluster -w
```

## Check the service status

Make sure using the following command to check the services and they are all in healthy without any external IPs:

```console
kubectl get services -n mssql-cluster
```

Please check manage BDC private cluster to know more about how you can manage BDC private cluster and then the next step is to connect to BDC cluster.

## Next steps

[Manage a private cluster](private-cluster-manage.md)

[Restrict egress traffic of Private BDC cluster](private-cluster-restrict-egress-traffic.md)