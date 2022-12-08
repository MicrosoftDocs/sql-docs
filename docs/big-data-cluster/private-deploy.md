---
title: Deploy BDC in Azure Kubernetes Service (AKS) private cluster
titleSuffix: SQL Server Big Data Cluster
description: Learn how to deploy a SQL Server Big Data Clusters with Azure Kubernetes Service (AKS) private cluster with advanced networking (CNI).
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 08/20/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Deploy BDC in Azure Kubernetes Service (AKS) private cluster

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article explains how to deploy SQL Server Big Data Clusters on Azure Kubernetes Service (AKS) private cluster. This configuration supports restricted use of public IP addresses in enterprise networking environment.

A private deployment provides the following benefits:

* Restricted use of public IP addresses
* Network traffic between application servers and cluster node pools remains only on the private network
* Custom configuration for mandatory egress traffics to fit specific requirements

This article demonstrates how to use an AKS private cluster to restrict the use of Public IP address while respective security strings have applied.

## Deploy private big data cluster with AKS private cluster

To get started, create a [AKS private cluster](/azure/aks/private-clusters) to make sure the network traffic between API server and node pools remains on the private network only. The control plane or API server has internal IP addresses in an AKS private cluster.

This section shows you deploy a big data cluster in Azure Kubernetes Service (AKS) private cluster with advanced networking (CNI).

## Create a private AKS cluster with advanced networking

```azurecli

export REGION_NAME=<your Azure region >
export RESOURCE_GROUP=< your resource group name >
export SUBNET_NAME=aks-subnet
export VNET_NAME=bdc-vnet
export AKS_NAME=< your aks private cluster name >
 
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

To be able to get to next step, you need to provision an AKS cluster with Standard Load Balancer with private cluster feature enabled. Your command will look like as follows: 

```azurecli
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

After a successful deployment, you can go to `<MC_yourakscluster>` resource group and you'll find the `kube-apiserver` is a private endpoint. For example, see the following section.

## Connect to an AKS cluster

```azurecli
az aks get-credentials -n $AKS_NAME -g $RESOURCE_GROUP
```

## Build Big Data Cluster deployment profile

After connecting to an AKS cluster, you can start to deploy BDC, and you can prepare the environment variable and initiate a deployment: 

```azurecli
azdata bdc config init --source aks-dev-test --target private-bdc-aks --force
```

Generate and config BDC custom deployment profile:

```azurecli
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.docker.imageTag=2019-CU6-ubuntu-16.04"
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.storage.data.className=default"
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.storage.logs.className=default"

azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -c private-bdc-aks/control.json -j "$.spec.endpoints[1].serviceType=NodePort"

azdata bdc config replace -c private-bdc-aks/bdc.json -j "$.spec.resources.master.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -c private-bdc-aks/bdc.json -j "$.spec.resources.gateway.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -c private-bdc-aks/bdc.json -j "$.spec.resources.appproxy.spec.endpoints[0].serviceType=NodePort"
```

## Deploy private SQL Server Big Data Cluster with HA

In case you are [deploying a SQL Server Big Data Cluster (SQL-BDC) with high availability (HA)](deployment-high-availability.md), you'll be using deploy `aks-dev-test-ha` deployment profile. After a successful deployment, you can use the same `kubectl get svc` command and you'll see an additional `master-secondary-svc` service is created. You need to configure `ServiceType` as `NodePort`. Other steps will be similar to what mentioned in previous section.

The following example sets the `ServiceType` as `NodePort`:

```azurecli
azdata bdc config replace -c private-bdc-aks /bdc.json -j "$.spec.resources.master.spec.endpoints[1].serviceType=NodePort"
```

## Deploy BDC in AKS private cluster

```azurecli
export AZDATA_USERNAME=<your bdcadmin username>
export AZDATA_PASSWORD=< your bdcadmin password>

azdata bdc create --config-profile private-bdc-aks --accept-eula yes
```

## Check deployment status

The deployment will take a few minutes and you can use the following command to check the deployment status: 

```console
kubectl get pods -n mssql-cluster -w
```

## Check the service status

Use the following command to check the services. Verify that they are all healthy without any external IPs:

```console
kubectl get services -n mssql-cluster
```

See how to [manage big data cluster in AKS private cluster](private-manage.md) and then the next step is to [connect to a SQL Server big data cluster](connect-to-big-data-cluster.md).

See automation scripts for this scenario at [SQL Server Samples repository on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/private-aks).

## Next steps

[Manage a private cluster](private-manage.md)

[Restrict egress traffic of Private big data cluster](private-restrict-egress-traffic.md)

[Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md)
