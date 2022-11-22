---
title: Active Directory and Kubernetes DNS Reconciliation in Big Data Clusters deployments
description: Configure DNS reconciliation for SQL Server Big Data Cluster in Active Directory mode
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 09/30/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Active Directory and Kubernetes DNS reconciliation in Big Data Clusters deployments

This article describes some of the challenges and the solutions to accommodate Active Directory integration when deploying Big Data Clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Overview

When the big data cluster is not deployed with Active Directory integration, we rely on [Kubernetes CoreDNS](https://kubernetes.io/docs/tasks/administer-cluster/coredns/) service for internal DNS resolutions. Kubernetes uses an internal domain such as `<namespace>.svc.cluster.local`. It creates A (forward lookup) and PTR (reverse lookup) records in the DNS server with names in this domain.

However, when Active Directory mode is enabled, a new domain comes into the picture with its own set of DNS servers. During internal name resolution, this can result in confusion over which set of DNS servers to go to for forward and reverse lookups.

## Challenges

* When new Kubernetes pods are deployed, DNS entries will need to be added in both sets of DNS servers. Kubernetes takes care of recording the entries in its CoreDNS, however, BDC  deployment workflow is responsible for adding the required entries in Active Directory Domain Controller DNS servers. Similarly, when a big data cluster is deleted, the workflow must ensure these entries are removed.
* Active Directory DNS servers are external to Kubernetes cluster. But BDC has its own IP space inside Kubernetes and cannot create records for this IP space in an externally situated DNS server since this IP space is not visible outside of the cluster boundaries.
* When failover events occur within the Kubernetes cluster, records in AD DNS servers must be updated as well.
* In addition to pod names, Kubernetes service names must also be addressable through the AD domain name lookups. This creates an additional challenge in Active Directory DNS since one service name can map to multiple pod IP addresses.
* Record update propagation and replication delays in organizational Active Directory DNS servers can be significant and beyond the control of BDC management workflows. This can impact BDC functionality immediately upon deployment and failover. On the contrary, Kubernetes CoreDNS is faster and efficient due to its locality.

## Solution

To get around aforementioned challenges, the solution implemented in BDC involves a new internal CoreDNS service that is managed inside BDC namespace. This  is the only DNS service the pods in the BDC namespace will reach out to for name resolutions. The complexity of multiple domains is hidden behind the new CoreDNS service.

For example, in the following diagram the pods use the BDC CoreDNS server to resolve names. The pods do not interact directly with the Kubernetes CoreDNS server or the AD DNS Server. 

:::image type="content" source="media/active-directory-dns-reconciliation/bdc-ad-dns-reconciliation.png" alt-text="Pods connect to CoreDNS server in their own namespace":::

Here are some of the implementation details that clarify how this design works in BDC:

### Centralized management of multiple domains

The complexity of what happens on name lookups remains hidden behind the internal DNS service in a centralized fashion. This avoids putting the burden of managing multiple domains onto individual pods and simplifies the design.

### No records for internal pods in external DNS servers

As result of this design principle, BDC will not have to create and manage A and PTR records for pods in Kubernetes IP space in external DNS servers.

### No duplication of records

Internal DNS records in multiple places. The only storage for these records is Kubernetes CoreDNS. The BDC internal CoreDNS will do a computational rewriting and forwarding of DNS queries to Kubernetes CoreDNS.

### Computational rewriting

Since BDC does not store any records, BDC is  responsible for the translation of incoming forward lookup queries with names having AD domain to the names with Kubernetes domain, and forward this query to Kubernetes CoreDNS.
As an example, an incoming query for `compute-0-0.contoso.local` would be rewritten to `compute-0-0.compute-0-svc.contoso.svc.cluster.local` and this request would be forwarded to Kubernetes CoreDNS.
In case of reverse lookups, the request is forwarded with internal IPs as they are to Kubernetes CoreDNS, and rewrite the response from there to AD domain name before responding to the client.

### Simplicity in pod configurations

Since only the internal BDC CoreDNS is referenced in /etc/resolv.conf of all BDC pods, this simplifies the network view from the pods. The complexity will be hidden in the internal CoreDNS instead.

### Static and Reliable IP address for DNS Service

The CoreDNS service that BDC deploys, will have registered static internal IP that can be accessed from all pods. This will ensure that values in /etc/resolv.conf will not have to be updated.

### Service load balance management is retained by Kubernetes

When lookups happen for services instead of individual pods, they will still go to Kubernetes CoreDNS, so BDC is not responsible for implementing load balancing specially for AD domain.

As an example, if a forward lookup request comes for `compute-0-svc.contoso.local`, it will be rewritten to` compute-0-svc.contoso.svc.cluster`.local. This request will be forwarded to Kubernetes CoreDNS and the load balancing will happen there. A response will be an IP address for one of the multiple compute pool instances (pod replicas).

### Scalability

Since BDC does not store any records, the internal BDC CoreDNS can be scaled without state retention and record replication across multiple replicas. If the DNS records will be stored within BDC, replicating the state across all pods will have to be taken care of BDC as well.

### Externally visible service entries stay in AD DNS

For service endpoints that need to be accessible to clients outside Kubernetes cluster, DNS entries will be created in AD DNS server as BDC is being deployed. User will input the DNS names to register from through the deployment configuration profiles.

### Self-deprovisioning

Once BDC is deleted, there is no additional dynamic work to delete DNS entries when cluster is being deprovisioned. The only entries in remote Active Directory DNS that need to be cleaned up are for external services and they are static in number. The internal DNS entries will automatically be removed with the cluster.

## Next steps

- [Deploy SQL Server Big Data Clusters in Active Directory mode](active-directory-deploy.md)
- [Manage big data cluster access in Active Directory mode](active-directory-objects.md)
- [Deploy multiple SQL Server Big Data Clusters in the same Active Directory domain](active-directory-deployment-background.md)
