---
title: Deploy in Active Directory on Azure Kubernetes Services (AKS) - tutorial
titleSuffix: SQL Server Big Data Cluster
description: Learn how to deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS).
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 10/23/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Tutorial: Deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS)

This article explains how to deploy a SQL Server big data cluster (BDC) in the Active Directory authentication mode with a reference architecture which extends your on-premises Active Directory domain Service (AD DS) to Azure. You can deploy it from [Azure Architecture Center](https://github.com/mspnp/identity-reference-architectures/tree/master/adds-extend-domain) with [Azure building blocks](https://github.com/mspnp/template-building-blocks/wiki/Install-Azure-Building-Blocks).

## Prerequisites

Before deploying a SQL Server 2019 big data cluster, you need to:

* [Install the big data tools](deploy-big-data-tools.md) on the management Azure VM which resides on the same VNet or peered to the VNET where you deploy the new AKS cluster.
* Prepare to deploy a SQL Server big data cluster (BDC) in the [Active Directory authentication mode](active-directory-prerequisites.md) in your on-prem AD controller.

## Create AKS subnet

1. Set environment variables

   ```console
   export REGION_NAME=< your Azure Region > 
   export RESOURCE_GROUP=<your resource group >
   export SUBNET_NAME=aks-subnet
   export VNET_NAME= adds-vnet
   export AKS_NAME= <your aks cluster name>
   ```

1. Create the AKS subnet

   ```console
   SUBNET_ID=$(az network vnet subnet show \
    --resource-group $RESOURCE_GROUP \
    --vnet-name $VNET_NAME \
    --name $SUBNET_NAME \
    --query id -o tsv)
   ```

The following screenshot shows how we plan the subnets resides in the VNET in the architecture.

## Create an AKS private cluster

You can use the following command to deploy AKS private cluster, in case no need to use private AKS cluster , remove `--enable-private-cluster` parameter in the command, any other requirements about creating AKS cluster, see [how to deploy an Azure Kubernetes Cluster (AKS)](/azure/aks/tutorial-kubernetes-deploy-cluster).

```azurecli
az aks create \
    --resource-group $RESOURCE_GROUP \
    --name $AKS_NAME \
    --load-balancer-sku standard \
    --enable-private-cluster \
    --network-plugin azure \
    --vnet-subnet-id $SUBNET_ID \
    --docker-bridge-address 172.17.0.1/16 \
    --dns-service-ip 10.3.0.10 \
    --service-cidr 10.3.0.0/24 \
    --node-vm-size Standard_D13_v2 \
    --node-count 2 \
    --generate-ssh-keys
```

After deploying an AKS cluster successfully, you need to [connect to the AKS cluster](/azure/aks/tutorial-kubernetes-deploy-cluster#connect-to-cluster-using-kubectl). 

## Verify reverse DNS entry for domain controller

Before starting the BDC deployment in AD mode in AKS cluster, you need to make sure that the domain controller itself has both **A record** and **PTR record** ( reverse DNS entry ), registered in the DNS server.

You can verify this by running **nslookup** command or run [the PowerShell script](troubleshoot-ad-reverse-lookup-zone.md) to confirm if you have reverse DNS entry (PTR record) configured.

## Create BDC Deployment Profile

The following command creates a deployment profile:

```console
azdata bdc config init --source kubeadm-prod  --target bdc-ad-aks
```

The following commands are utilized to set parameters for a deployment profile.

### control.json 

```console
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.spec.storage.data.className=default"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.spec.storage.logs.className=default"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.spec.endpoints[1].serviceType=NodePort"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.spec.endpoints[0].dnsName=controller.contoso.com"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.spec.endpoints[1].dnsName=proxys.contoso.com"

# security settings 
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.security.activeDirectory.ouDistinguishedName=OU\=bdc\,DC\=contoso\,DC\=com"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.security.activeDirectory.dnsIpAddresses=[\"192.168.0.4\"]"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.security.activeDirectory.domainControllerFullyQualifiedDns=[\"ad1.contoso.com\"]"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.security.activeDirectory.domainDnsName=contoso.com"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.security.activeDirectory.clusterAdmins=[\"bdcadminsgroup\"]"
azdata bdc config replace -p bdc-ad-aks/control.json -j "$.security.activeDirectory.clusterUsers=[\"bdcusersgroup\"]"
```

### bdc.json

```console
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.master.spec.endpoints[0].dnsName=master.contoso.com"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.master.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.master.spec.endpoints[1].dnsName=mastersec.contoso.com"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.master.spec.endpoints[1].serviceType=NodePort"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.gateway.spec.endpoints[0].dnsName=gateway.contoso.com"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.gateway.spec.endpoints[0].serviceType=NodePort"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.appproxy.spec.endpoints[0].dnsName=approxy.contoso.com"
azdata bdc config replace -p bdc-ad-aks/bdc.json -j "$.spec.resources.appproxy.spec.endpoints[0].serviceType=NodePort"
```

## Initiate deployment

The following command initiates a BDC deployment:

```console
azdata bdc create --config-profile bdc-ad-aks --accept-eula yes
```

## Next steps

