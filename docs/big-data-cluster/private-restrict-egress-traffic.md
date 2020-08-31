---
title: Restrict egress traffic of Big Data Clusters (BDC) clusters in Azure Kubernetes Service (AKS) private cluster
titleSuffix: SQL Server Big Data Clusters
description: Learn how to restrict egress traffic of Big Data Clusters (BDC) clusters in Azure Kubernetes Service (AKS) private cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 08/20/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Restrict egress traffic of Big Data Clusters (BDC) clusters in Azure Kubernetes Service (AKS) private cluster

AKS provisions a standard SKU Load Balancer to be set up and used for egress by default. However, the default setup may not meet the requirements of all scenarios if public IPs are disallowed or additional hops are required for egress. Define a user user-defined route table if the cluster disallows public IPs and sits behind a network virtual appliance (NVA).

By default, AKS clusters have unrestricted outbound (egress) internet access for the sake of management and operational purposes. Worker nodes in an AKS cluster need to access certain ports and fully qualified domain names (FQDNs) for instance:

* Worker node OS security updates, the cluster needs to pull base system container images from Microsoft Container Registry (MCR)
* GPU enabled AKS worker nodes need to access some endpoints from Nvidia to install driver
* In the scenario where customers use AKS work in conjunction with Azure services such as Azure policy for enterprise-grade compliance, Azure Monitoring (with container insights) 
* Dev Space enabled and more scenarios of similar nature

> [!NOTE]
> Currently, when you deploy BDC in Azure Kubernetes Service (AKS) private cluster, there is no inbound dependencies in the scenario besides what we mentioned in this article, you can find all outbound dependencies at [control egress traffic for cluster nodes in Azure Kubernetes Service (AKS)](/azure/aks/limit-egress-traffic) .

This article covers how to deploy BDC in AKS private cluster with advanced networking and UDR (user-defined route) as well as its further integration with enterprise-grade networking environment.

## Use Azure firewall to restrict egress traffic

Azure Firewall provides an Azure Kubernetes Service `(AzureKubernetesService)` FQDN tag to simplify this configuration.

See [Restrict egress traffic using Azure firewall](/azure/aks/limit-egress-traffic#restrict-egress-traffic-using-azure-firewall) for complete information about this tag.

The following image shows how traffic is restricted on an AKS private cluster. 

:::image type="content" source="media/private-cluster-restrict-egress-traffic/aks-azure-firewall-egress.png" alt-text="AKS private cluster firewall egress traffic":::

To set up a basic architecture with Azure Firewall:

1. Create the resource group & VNet
2. Create & set up Azure firewall
3. Create user-defined route table
4. Set up firewall rules
5. Create service principal (SP)
6. Create AKS private cluster
7. Create BDC deployment profile
8. Deploy BDC

The following steps provide details.

## Create the resource group and VNet

1. Define a set of environment variables for creating resources.

   ```console
   export REGION_NAME=<region>
   export RESOURCE_GROUP=private-bdc-aksudr-rg
   export SUBNET_NAME=aks-subnet
   export VNET_NAME=bdc-vnet
   export AKS_NAME=bdcaksprivatecluster
   ```

1. Create the resource group

   ```azurecli
   az group create -n $RESOURCE_GROUP -l $REGION_NAME
   ```

1. Create the VNET

   ```azurecli
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

1. Define a set of environment variables for creating resources.

   ```console
   export FWNAME=bdcaksazfw
   export FWPUBIP=$FWNAME-ip
   export FWIPCONFIG_NAME=$FWNAME-config

   az extension add --name azure-firewall
   ```

1. Create a dedicated subnet for the firewall

   > [!NOTE]
   > You cannot change the firewall name after creation

   ```azurecli
   az network vnet subnet create \
     --resource-group $RESOURCE_GROUP \
     --vnet-name $VNET_NAME \
     --name AzureFirewallSubnet \
     --address-prefix 10.3.0.0/24

    az network firewall create -g $RESOURCE_GROUP -n $FWNAME -l $REGION_NAME --enable-dns-proxy true

    az network public-ip create -g $RESOURCE_GROUP -n $FWPUBIP -l $REGION_NAME --sku "Standard"

    az network firewall ip-config create -g $RESOURCE_GROUP -f $FWNAME -n $FWIPCONFIG_NAME --public-ip-address $FWPUBIP --vnet-name $VNET_NAME
   ```

By default, Azure automatically routes traffic between Azure subnets, virtual networks, and on-premises networks. 

## Create user-defined route table

Create a user-defined route (UDR) table with a hop to Azure Firewall.

```azurecli

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
```

## Set up firewall rules 

```azurecli
# Add FW Network Rules

az network firewall network-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwnr' -n 'apiudp' --protocols 'UDP' --source-addresses '*' --destination-addresses "AzureCloud.$REGION_NAME" --destination-ports 1194 --action allow --priority 100
az network firewall network-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwnr' -n 'apitcp' --protocols 'TCP' --source-addresses '*' --destination-addresses "AzureCloud.$REGION_NAME" --destination-ports 9000
az network firewall network-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwnr' -n 'time' --protocols 'UDP' --source-addresses '*' --destination-fqdns 'ntp.ubuntu.com' --destination-ports 123

# Add FW Application Rules

az network firewall application-rule create -g $RESOURCE_GROUP -f $FWNAME --collection-name 'aksfwar' -n 'fqdn' --source-addresses '*' --protocols 'http=80' 'https=443' --fqdn-tags "AzureKubernetesService" --action allow --priority 100
```

You can associate user-defined route table (UDR) to AKS cluster where deployed BDC previously using the following command:

```azurecli
az network vnet subnet update -g $RESOURCE_GROUP --vnet-name $VNET_NAME --name $SUBNET_NAME --route-table $FWROUTE_TABLE_NAME
```
## Create service principal (SP) and assign permissions to the VNet

```azurecli
# Create SP and Assign Permission to Virtual Network

az ad sp create-for-rbac -n "bdcaks-sp" --skip-assignment

APPID=<your service principal ID >
PASSWORD=< your service principal password >
VNETID=$(az network vnet show -g $RESOURCE_GROUP --name $VNET_NAME --query id -o tsv)

# Assign SP Permission to VNET

az role assignment create --assignee $APPID --scope $VNETID --role "Network Contributor"


RTID=$(az network route-table show -g $RESOURCE_GROUP -n $FWROUTE_TABLE_NAME --query id -o tsv)
az role assignment create --assignee $APPID --scope $RTID --role "Network Contributor"
```

## Create AKS cluster

Then create the AKS cluster with `userDefinedRouting` as outbound type.

```azcli 
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
```

## Build Big Data Cluster (BDC) deployment profile

You can create BDC clusters with custom profile: 

```console
azdata bdc config init --source aks-dev-test --target private-bdc-aks --force
```console

## Generate and config BDC custom deployment profile: 
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

## Deploy BDC in AKS private cluster

```console
export AZDATA_USERNAME=<your bdcadmin username>
export AZDATA_PASSWORD=< your bdcadmin password>

azdata bdc create --config-profile private-bdc-aks --accept-eula yes
```

## Use third party firewall instead of Azure Firewall

Use third party firewall to restrict egress traffic when deployed BDC with AKS private cluster. For example, [Palo Alto Networks](https://www.paloaltonetworks.com/) provides firewall products in the Azure Marketplace. These firewalls can be used in private deployment solutions with more compliant configurations. Any firewall should provide the following network rules:

* All the required outbound network rules and FQDNs for AKS clusters and all Wildcard HTTP/HTTPS endpoints and dependencies that can vary with your AKS cluster based on a number of qualifiers and your actual requirements. 
* Azure Global required network rules / FQDN/application rules mentioned here. 
* Optional recommended FQDN / application rules for AKS clusters mentioned here. 

Please check how to [manage BDC in AKS private cluster](private-cluster-manage.md)  and then the next step is to [connect to BDC cluster](connect-to-big-data-cluster.md).

See automation scripts for this scenario at [SQL Server Samples repository on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/private-aks).

## Next steps

[Manage big data cluster in AKS private cluster](private-manage.md)

[Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md)