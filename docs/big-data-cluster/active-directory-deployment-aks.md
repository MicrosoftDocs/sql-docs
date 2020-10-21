---
title: Deploy in Active Directory on Azure Kubernetes Services (AKS)
titleSuffix: SQL Server Big Data Cluster
description: Explains concepts and planning information for how to deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS).
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 10/23/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS)

SQL Server Big data clusters support [AD deployment mode](deploy-active-directory.md) for **Identity and Access Management (IAM)**. IAM for **Azure Kubernetes Service (AKS)** has been challenging because industry-standard protocols such as OAuth 2.0 and OpenID Connect which is widely supported by Microsoft identity platform is not supported by SQL Server.  

This article explains how to enable BDC deployment in AD mode while deploying in [Azure Kubernetes Service (AKS)](/azure/aks/intro-kubernetes). 

## Architecture topologies

**Active Directory Domain Services (AD DS)** can run on an Azure virtual machine (VM) [in the same way](/windows-server/identity/ad-ds/deploy/virtual-dc/adds-on-azure-vm) it runs in many on-premises instances.  After promoting the new domain controllers in Azure, they will need to be set to the primary and secondary DNS Servers for the virtual network, and any on-premises DNS Servers would be demoted to tertiary and beyond. AD authentication enables domain-joined clients on [Linux to authenticate to SQL Server](../linux/sql-server-linux-active-directory-auth-overview.md) using their domain credentials and the Kerberos protocol.

There are a few variations about how to enable BDC deployment in AD mode in AKS.  Here we introduce two types which are easier to implement and integrate with existing enterprise-grade architectures:

* **Extend your on-premises Active Directory domain to Azure** which [extends your Active Directory environment to Azure](/azure/architecture/reference-architectures/identity/adds-extend-domain) to provide distributed authentication services using Active Directory Domain Services (AD DS). This architecture replicates your on-prem Active Directory Domain Services (AD DS) in Azure for the sake of reducing the latency caused by sending authentication requests from the cloud back to on-premises AD DS. A typical use-cas for this solution is when your application is hosted partly on-premises and partly in Azure and your authentication requests need to travel back and forth. See how to deploy this solution step by step [here](https://github.com/mspnp/identity-reference-architectures/tree/master/adds-extend-domain).
* **Active Directory Domain Services (AD DS) resource forest** in Azure how to create a separate Active Directory domain in Azure that is trusted by domains in your on-premises AD forest. This architecture shows a [one-way trust from the domain in Azure to the on-premises domain](/azure/architecture/reference-architectures/identity/adds-forest) so on-premises users to access resources in the domain in Azure. See how to deploy this solution step by step [here]( https://github.com/mspnp/identity-reference-architectures/tree/master/adds-forest).

In addition to those architectural solutions which allow to create a landing zone which has all resources to be deployed from scratch or any additional workaround based on existing architecture, we recommend to deploy BDC in AKS cluster on a separate subnet which resides in your target VNet or peered VNet. A typical architecture would look like the following:

:::image type="content" source="media/active-directory-deployment-aks/bdc-deployment-ad-aks.png" alt-text="AKS cluster with ad and SQL Server Big Data Cluster":::

## Recommendations

The following recommendations apply for most scenarios related to BDC deployment in AD mode on Azure Kubernetes Service (AKS), available options will be mentioned in each component to provide guidance for better integration with enterprise-grade architecture. 

### Networking recommendations

A few key components can be used to connect your on-premises environment to Azure :

* **Azure VPN Gateway**: A VPN gateway is a specific type of virtual network gateway that is used to send encrypted traffic between an Azure virtual network and an on-premises location over the public Internet. You’ll use both Azure Virtual Network Gateway and local Virtual Network Gateway and see how to configure them [here](/azure/vpn-gateway/vpn-gateway-about-vpngateways).
* **ExpressRoute** connections do not go over the public Internet, and offer higher security, reliability, and speeds with lower latencies than typical connections over the Internet.

The choice of your connectivity option will affect the latency, performance, and SLA level of your solution depending on the SKUs.  For specific information, see Here is an article [About ExpressRoute virtual network gateways](/azure/expressroute/expressroute-about-virtual-network-gateways).

Most customers use a jump-box or [Azure Bastion](/azure/bastion/bastion-overview) to access other Azure infrastructure, **Azure Private Link** enables you to access Azure PaaS Services to AKS in this scenario and other Azure hosted customer-owned/partner services over a Private Endpoint in your virtual network securely. Traffic between your virtual network and the service traverses over the Microsoft backbone network, eliminating exposure from the public Internet. You can also create your own Private Link Service in your virtual network and deliver it privately to your customers. 

### Recommendations on integrating on-premises Active Directory with Azure

On-premises AD DS stores information about user accounts, such as names, passwords, and so on, and enables other authorized users on the same network to access this information by authenticating identities associated with users, computers, applications, or other resources that are included in a security boundary. In most hybrid scenario, user authentication runs over a VPN Gateway or ExpressRoute connection to the on-premises AD DS environment.  

For a BDC deployment in AD mode, the solution to [integrate on-premises Active Directory with Azure](/azure/architecture/reference-architectures/identity/), must have the following prerequisites :

* An [AD account has specific permission](active-directory-prerequisites.md) to create users, groups, and machine accounts inside the provided organizational unit (OU) in your on-premises Active directory. 
* A DNS server needs to be configured to [resolve internal DNS](active-directory-dns-reconciliation.md), it must contain both **A (forward lookup)** and **PTR (reverse lookup) records** in the DNS server with names in this domain. You need to specify the Domain DNS settings in BDC deployment profile.  

## Next steps

[Tutorial: Deploy SQL Server Big Data Clusters in AD mode on Azure Kubernetes Services (AKS)](active-directory-deployment-aks-tutorial.md)