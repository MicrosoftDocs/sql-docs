---
title: Deploy in Active Directory on Azure Kubernetes Services (AKS)
titleSuffix: SQL Server Big Data Cluster
description: Explains concepts and planning information for how to deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS).
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 11/12/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS)

SQL Server Big Data Clusters support [Active Directory (AD) deployment mode](./active-directory-prerequisites.md) for **Identity and Access Management (IAM)**. IAM for **Azure Kubernetes Service (AKS)** has been challenging because industry-standard protocols such as OAuth 2.0 and OpenID Connect which is widely supported by Microsoft identity platform is not supported by SQL Server.  

This article explains how to deploy a big data cluster in AD mode while deploying in [Azure Kubernetes Service (AKS)](/azure/aks/intro-kubernetes). 

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Architecture topologies

**Active Directory Domain Services (AD DS)** runs on an Azure virtual machine (VM) [in the same way](/windows-server/identity/ad-ds/deploy/virtual-dc/adds-on-azure-vm) it runs in many on-premises instances.  After promoting the new domain controllers in Azure, set the primary and secondary DNS Servers for the virtual network, demote any on-premises DNS Servers would be demoted to tertiary or later. AD authentication enables domain-joined clients on [Linux to authenticate to SQL Server](../linux/sql-server-linux-active-directory-auth-overview.md) using their domain credentials and the Kerberos protocol.

There are a few ways to deploy a big data cluster in AD mode in AKS.  This article introduces two methods, which are easier to implement and integrate with existing enterprise-grade architectures:

* **Extend your on-premises Active Directory domain to Azure.** This method [enables your Active Directory environment](/azure/architecture/reference-architectures/identity/adds-extend-domain) to provide distributed authentication services using Active Directory Domain Services (AD DS) on Azure. You replicate your on-premises Active Directory Domain Services (AD DS) to reduce the latency caused by sending authentication requests from the cloud back to on-premises AD DS. A typical use-case for this solution is when your application is hosted partly on-premises and partly in Azure and your authentication requests need to travel back and forth.

   See how to deploy this solution step by step [in this reference architecture](https://github.com/mspnp/identity-reference-architectures/tree/master/adds-extend-domain).

* **Extend the Active Directory Domain Services (AD DS) resource forest to Azure.** In this architecture, you create a new domain in Azure that is trusted by your on-premises AD forest. This architecture shows a [one-way trust from the domain in Azure to the on-premises forest](/azure/architecture/reference-architectures/identity/adds-forest).

   The trust allows on-premises users access resources in the domain in Azure. See how to deploy this solution step by step [in this reference architecture](https://github.com/mspnp/identity-reference-architectures/tree/master/adds-forest).

The reference architectures described above allow you to create a landing zone, which has all resources to be deployed from scratch or any additional workaround based on existing architecture. In addition to those reference architectures, you should deploy the big data cluster in an AKS cluster on a separate subnet that stays in your target VNet or a peered VNet.

The following image represents a typical architecture:

:::image type="content" source="media/active-directory-deployment-aks/ad-in-aks-diagram.png" alt-text="AKS cluster with AD and SQL Server Big Data Cluster":::

## Recommendations

The following recommendations apply for most big data cluster deployments in AD mode on AKS. Available options will be mentioned in each component to provide guidance for better integration with enterprise-grade architecture.

### Networking recommendations

A few key components can be used to connect your on-premises environment to Azure:

* **Azure VPN Gateway**: A VPN gateway is a specific type of virtual network gateway that is used to send encrypted traffic between an Azure virtual network and an on-premises location over the public internet. You'll use both Azure Virtual Network Gateway and local Virtual Network Gateway. For information about how to configure them, see [What is VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways).
* **Azure ExpressRoute**: ExpressRoute connections do not go over the public internet, and offer higher security, reliability, and speeds with lower latencies than typical connections over the internet. The choice of your connectivity option will affect the latency, performance, and SLA level of your solution depending on the SKUs. For specific information, see [About ExpressRoute virtual network gateways](/azure/expressroute/expressroute-about-virtual-network-gateways).

Most customers use a jump-box or [Azure Bastion](/azure/bastion/bastion-overview) to access other Azure infrastructure. **Azure Private Link** enables you to securely access Azure PaaS Services, including AKS in this scenario as well as and other Azure hosted services over a private endpoint in your virtual network. Traffic between your virtual network and the service traverses over the Microsoft backbone network, eliminating exposure to the public internet. You can also create your own private link service in your virtual network and deliver it privately to your customers.

### Active Directory and Azure recommendation

On-premises AD DS stores information about user accounts, and enables other authorized users on the same network to access this information by authenticating identities associated with users, computers, applications, or other resources that are included in a security boundary. In most hybrid scenarios, user authentication runs over a VPN Gateway or ExpressRoute connection to the on-premises AD DS environment.  

For a big data cluster deployment in AD mode, the solution to [integrate on-premises Active Directory with Azure](/azure/architecture/reference-architectures/identity/), must have the following prerequisites:

* An [AD account has specific permission](active-directory-prerequisites.md) to create users, groups, and machine accounts inside the provided organizational unit (OU) in your on-premises Active directory.
* A DNS server to [resolve internal DNS](active-directory-dns-reconciliation.md). It must contain both **A (forward lookup)** and **PTR (reverse lookup) records** in the DNS server with names in this domain. Specify the Domain DNS settings in the big data cluster deployment profile.  

## Next steps

[Tutorial: Deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS)](active-directory-deployment-aks-tutorial.md)
