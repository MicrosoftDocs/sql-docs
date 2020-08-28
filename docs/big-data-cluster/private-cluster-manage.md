---
title: Manage big data clusters (BDC) in Azure Kubernetes Service (AKS) private cluster
titleSuffix: SQL Server Big Data Cluster
description: Learn how to manage a  SQL Server Big Data Clusters in Azure Kubernetes Service (AKS) private cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 08/20/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manage big data cluster in AKS private cluster

This article explains how to manage an Azure Kubernetes Service (AKS) private cluster with SQL Server big data clusters (BDC) deployed in Azure.

To manage the AKS private cluster, you can use the same set of options mentioned in previous section about How to manage  BDC in AKS private cluster.

The easiest option is to:

1. Deploy an Azure VM in the same VNET with your AKS cluster
1. Connect to that VM and [Install SQL Server 2019 Big Data tools](deployment-guidance.md#install-sql-server-2019-big-data-tools)

If you need to manage private BDC in AKS cluster, there are four options available as below upon your specific scenario:

- Deploy an Azure VM for management purposes. This may be called a *jumpbox*. Locate the jumpbox in the same VNet with your AKS cluster. Grant access to BDC controller with your BDC username and password.  
- In Azure, you can also use an Azure VM in a separate network and set up [Virtual network peering](/azure/virtual-network/virtual-network-peering-overview) to the VNet.
- In Azure, if your service runs behind an [Azure Standard Load balancer](/azure/aks/load-balancer-standard) it can be enabled for [Azure Private Link](/azure/private-link/private-link-service-overview#limitations) access so that consumers of your service can access it privately from their own VNets.
- In hybrid scenario, you can also set up [Azure ExpressRoute](/azure/expressroute/expressroute-introduction) or [VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways) connection.

For security purpose, you can use AKS features for API server authorized IP ranges to limit access to API server (on AKS Control Plane) by allowing exclusively specific IP addresses such as allow jumpbox VM or management VM access or an IP addresses range for all your developers on your team and the firewall public frontend IP address.

## Next steps

[Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md)