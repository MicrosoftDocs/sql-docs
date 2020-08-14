---
title: Restrict egress traffic
titleSuffix: SQL Server Big Data Cluster
description: Learn how to restrict egress traffic.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 08/20/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Restrict egress traffic of Private BDC cluster

Since AKS provisions a standard SKU Load Balancer to be set up and used for egress by default. However, the default setup may not meet the requirements of all scenarios if public IPs are disallowed or additional hops are required for egress. You can customize cluster egress with a User-Defined Route if you have those disallows public IPs and  your cluster sit behind a network virtual appliance (NVA).  

By default, AKS clusters have unrestricted outbound (egress) internet access for the sake of management and operational purposes. Worker nodes in an AKS cluster need to access certain ports and fully qualified domain names (FQDNs) for instance:

* Worker node OS security updates, the cluster needs to pull base system container images from Microsoft Container Registry (MCR)
* GPU enabled AKS worker nodes need to access some endpoints from Nvidia to install driver.
* In the scenario where customers use AKS work in conjunction with Azure services such as Azure policy for enterprise-grade compliance, Azure Monitoring (with container insights) 
* Dev Space enabled and more scenarios of similar nature.

> [!NOTE]
> Currently, when you deploy BDC in Azure Kubernetes Service ( AKS ), there is no inbound dependencies in the scenario besides what we mentioned in this article, you can find all outbound dependencies at : [control egress traffic for cluster nodes in Azure Kubernetes Service (AKS)]( /azure/aks/limit-egress-traffic) . 
This article covers how to deploy BDC in AKS private cluster with advanced networking and UDR (User Defined Route) as well as its further integration with enterprise-grade networking environment. 

## Use Azure firewall to restrict egress traffic

All those egress traffics can be restricted using Azure firewall which provides an Azure Kubernetes Service (AzureKubernetesService) FQDN Tag to simplify this configuration. The FQDN tag contains all the FQDNs listed in AKS official documentation and is kept automatically up to date.

The following is a reference architecture to build a completely private environment in AKS, you can check here to see how to restrict egress traffic using Azure firewall step by step.

Here are steps to set up basic architecture using Azure Firewall : 

## Define a set of environment variables to be used in resource creations.

```console
export REGION_NAME=northeurope
export RESOURCE_GROUP=private-bdc-aksudr-rg
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
```

## Create and set up Azure Firewall

### Define a set of environment variables to be used in resource creations

```console
export FWNAME=bdcaksazfw
export FWPUBIP=$FWNAME-ip
export FWIPCONFIG_NAME=$FWNAME-config

az extension add --name azure-firewall
```

### Dedicated subnet for Azure Firewall (Firewall name cannot be changed)

az network vnet subnet create \
    --resource-group $RESOURCE_GROUP \
    --vnet-name $VNET_NAME \
    --name AzureFirewallSubnet \
    --address-prefix 10.3.0.0/24

az network firewall create -g $RESOURCE_GROUP -n $FWNAME -l $REGION_NAME --enable-dns-proxy true

az network public-ip create -g $RESOURCE_GROUP -n $FWPUBIP -l $REGION_NAME --sku "Standard"

az network firewall ip-config create -g $RESOURCE_GROUP -f $FWNAME -n $FWIPCONFIG_NAME --public-ip-address $FWPUBIP --vnet-name $VNET_NAME



By default, Azure automatically routes traffic between Azure subnets, virtual networks, and on-premises networks. In this scenario, we’re creating a user-defined route ( UDR ) table with a hop to Azure Firewall.

export SUBID= <your Azure subscription ID>
export FWROUTE_TABLE_NAME=bdcaks-rt
export FWROUTE_NAME=bdcaksroute
export FWROUTE_NAME_INTERNET=bdcaksrouteinet

export FWPUBLIC_IP=$(az network public-ip show -g $RESOURCE_GROUP -n $FWPUBIP --query "ipAddress" -o tsv)
export FWPRIVATE_IP=$(az network firewall show -g $RESOURCE_GROUP -n $FWNAME --query "ipConfigurations[0].privateIpAddress" -o tsv)

# Create UDR and add a route for Azure Firewall

az network route-table create -g $RESOURCE_GROUP --name $FWROUTE_TABLE_NAME

az network route-table route create -g $RESOURCE_GROUP --name $FWROUTE_NAME --route-table-name $FWROUTE_TABLE_NAME --address-prefix 0.0.0.0/0 --next-hop-type VirtualAppliance --next-hop-ip-address $FWPRIVATE_IP --subscription $SUBID

az network route-table route create -g $RESOURCE_GROUP --name $FWROUTE_NAME_INTERNET --route-table-name $FWROUTE_TABLE_NAME --address-prefix $FWPUBLIC_IP/32 --next-hop-type Internet




Set up firewall rules based on the document : 

# Add FW Network Rules

az network firewall network-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwnr' -n 'apiudp' --protocols 'UDP' --source-addresses '*' --destination-addresses "AzureCloud.$REGION_NAME" --destination-ports 1194 --action allow --priority 100
az network firewall network-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwnr' -n 'apitcp' --protocols 'TCP' --source-addresses '*' --destination-addresses "AzureCloud.$REGION_NAME" --destination-ports 9000
az network firewall network-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwnr' -n 'time' --protocols 'UDP' --source-addresses '*' --destination-fqdns 'ntp.ubuntu.com' --destination-ports 123

# Add FW Application Rules

az network firewall application-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwar' -n 'fqdn' --source-addresses '*' --protocols 'http=80' 'https=443' --fqdn-tags "AzureKubernetesService" --action allow --priority 100

You can associate User defined route table (UDR) to AKS cluster where deployed BDC previsouly using the following command:

az network vnet subnet update -g $RESOURCE_GROUP --vnet-name $VNET_NAME --name $SUBNET_NAME --route-table $FWROUTE_TABLE_NAME



# Create SP and Assign Permission to Virtual Network

az ad sp create-for-rbac -n "bdcaks-sp" --skip-assignment

APPID=<your service principle ID >
PASSWORD=< your service principle password >
VNETID=$(az network vnet show -g $RESOURCE_GROUP --name $VNET_NAME --query id -o tsv)

# Assign SP Permission to VNET

az role assignment create --assignee $APPID --scope $VNETID --role "Network Contributor"


RTID=$(az network route-table show -g $RESOURCE_GROUP -n $FWROUTE_TABLE_NAME --query id -o tsv)
az role assignment create --assignee $APPID --scope $RTID --role "Network Contributor"




Then create AKS cluster with userDefinedRouting ( UDR ) as outbound type.

az aks create \
    --resource-group $RESOURCE_GROUP \
    --location $REGION_NAME \
    --name $AKS_NAME \
    --load-balancer-sku standard \
    --outbound-type userDefinedRouting \
    --enable-private-cluster \
    --network-plugin azure \
    --vnet-subnet-id $SUBNET_ID \
    --docker-bridge-address 172.17.0.1/16 \
    --dns-service-ip 10.2.0.10 \
    --service-cidr 10.2.0.0/24 \
    --service-principal $APPID \
    --client-secret $PASSWORD \
    --node-vm-size Standard_D13_v2 \
    --node-count 2 \
    --generate-ssh-keys


Then you can create BDC private cluster with custom profile. You can find a template to set up this architecture here in sql server sample repository on Github. 

Use 3rd Party Firewall to restrict egress traffic
Popular 3rd party firewall such as palo alto is chosen by customers, it can be used in private deployment solution with more complicity on configuration, but basically, it has to consider the following network rules : 
•	 All the required outbound network rules and FQDNs for AKS clusters and all Wildcard HTTP/HTTPS endpoints and dependencies that can vary with your AKS cluster based on a number of qualifiers and your actual requirements. 
•	Azure Global required network rules / FQDN/application rules mentioned here. 
•	Optional recommended FQDN / application rules for AKS clusters mentioned here. 


## Next steps

