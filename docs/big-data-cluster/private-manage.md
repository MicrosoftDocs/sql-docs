---
title: Manage SQL Server Big Data Clusters in Azure Kubernetes Service (AKS) private cluster
titleSuffix: SQL Server Big Data Cluster
description: Learn how to manage a SQL Server Big Data Clusters in Azure Kubernetes Service (AKS) private cluster.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Manage SQL Server Big Data Clusters in AKS private cluster

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article explains how to manage an Azure Kubernetes Service (AKS) private cluster with big data clusters deployed in Azure.

As described in [Create a private cluster](/azure/aks/private-clusters/), the AKS private cluster API server endpoint has no public IP address. To manage, the API server, use a VM that has access to the AKS clusters's Azure Virtual Network (VNet).

## Azure VM - same VNet

The simplest method is to deploy an Azure VM in the same VNet as the AKS cluster.

1. Deploy an Azure VM in the same VNET with your AKS cluster. This is sometimes called a *jumpbox*.
1. Connect to that VM and [Install SQL Server 2019 Big Data tools](deployment-guidance.md#install-sql-server-2019-big-data-tools).

For security purpose, you can use AKS features for the API server authorized IP ranges to limit access to the API server (on AKS Control Plane). The limited access allows specific IP addresses - such as a jumpbox VM or management VM, or an IP address range for a group of developers, and the firewall public frontend IP address.

## Other options

Alternatives to using a jumpbox include:

* Use a VM in a separate network and setup [Virtual network peering](/azure/virtual-network/virtual-network-peering-overview) to the VNet.

* [Azure ExpressRoute](/azure/expressroute/expressroute-introduction) or [VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways) connection.

   [Options for connecting to the private cluster](/azure/aks/private-clusters#options-for-connecting-to-the-private-cluster) discusses each of these methods above.

* If your service runs behind an [Azure Standard Load balancer](/azure/aks/load-balancer-standard) it can be enabled for [Azure Private Link](/azure/private-link/private-link-service-overview#limitations). With Azure Private Link, you can enable private access from other Azure VNets.

* In hybrid scenario, you can also set up [Azure ExpressRoute](/azure/expressroute/expressroute-introduction) or [VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways) connection.

## Next steps

[Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md)