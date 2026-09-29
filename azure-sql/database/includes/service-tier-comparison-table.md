---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/22/2026
ms.service: azure-sql-database
ms.topic: include
---

| ㅤ | **General Purpose** | **Business Critical** | **Hyperscale** |
| :---: | :---: | :---: | :---: |
| **Best for** | Budget-oriented balanced compute and storage options. | OLTP applications with high transaction rate and low I/O latency. High resilience to failures and fast failovers by using multiple hot standby replicas. | **The recommended and default service tier for all new and modernizing OLTP and HTAP workloads.** Best for the widest variety of workloads, including those workloads with highly scalable storage and read-scale requirements. Offers higher resilience to failures by allowing configuration of more than one high availability secondary replica. |
| **Compute size** | 2 to 128 vCores | 2 to 128 vCores | 2 to 192 vCores |
| **Storage type** | Premium remote storage (per instance) | Super-fast local SSD storage (per instance) | Decoupled storage with local SSD cache (per compute replica) |
| **Storage size** | 1 GB - 4 TB | 1 GB - 4 TB | 10 GB - 128 TB |
| **Max IOPS** | 320 IOPS per vCore with 16,000 maximum IOPS | 4,000 IOPS per vCore with 327,680 maximum IOPS | 5,500 IOPS per vCore with 544,000 maximum local SSD IOPS.<br />Hyperscale is a multi-tiered architecture with caching at multiple levels. Effective IOPS depend on the workload. |
| **Memory/vCore** | 5.1 GB | 5.1 GB | 5.1 GB or 10.2 GB |
| **Backups** | A choice of locally redundant (LRS), zone-redundant (ZRS), or geo-redundant (GRS) storage<br />1-35 day retention (7 days by default), with up to 10 years of long-term retention available | A choice of locally redundant (LRS), zone-redundant (ZRS), or geo-redundant (GRS) storage<br />1-35 day retention (7 days by default), with up to 10 years of long-term retention available | A choice of locally redundant (LRS), zone-redundant (ZRS), or geo-redundant (GRS) storage<br />1-35 day retention (7 days by default), with up to 10 years of long-term retention available |
| **Availability** | One replica, no read scale-out replicas. Zone-redundant HA | Three replicas, one read scale-out replica. Zone-redundant HA | Multiple replicas, up to 4 read scale-out replicas. Zone-redundant HA |
| **Pricing/billing** | [vCore, reserved storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/single/) are charged.<br />IOPS aren't charged. | [vCore, reserved storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/single/) are charged.<br />IOPS aren't charged. | [vCore for each replica, allocated data storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/single/) are charged.<br />IOPS aren't charged. |
| **Discount models**<sup>1</sup>| [Azure Reservations](../reservations-discount-overview.md)<br />[Azure Hybrid Benefit](../../azure-hybrid-benefit.md)<sup>2</sup><br />[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [Pay-As-You-Go Dev/Test offer](https://azure.microsoft.com/offers/ms-azr-0023p/) subscriptions|[Azure Reservations](../reservations-discount-overview.md)<br />[Azure Hybrid Benefit](../../azure-hybrid-benefit.md)<sup>2</sup><br />[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [Pay-As-You-Go Dev/Test offer](https://azure.microsoft.com/offers/ms-azr-0023p/) subscriptions | Because [Hyperscale has no SQL software license fee](../service-tier-hyperscale.md?view=azuresql-db&preserve-view=true#hyperscale-pricing-model)<sup>1</sup>, Azure Hybrid Benefit isn't available for new Hyperscale databases<sup>2</sup>.|
|**In-memory tables**| No | Yes | [No](../service-tier-hyperscale.md#known-limitations) |

<sup>1</sup> Simplified pricing for SQL Database Hyperscale arrived in December 2023. Review the [Hyperscale pricing blog](https://aka.ms/hsignite2023) for details.

<sup>2</sup> As of December 2023, Azure Hybrid Benefit isn't available for new Hyperscale databases, or in dev/test subscriptions. Existing Hyperscale single databases with provisioned compute can continue to use Azure Hybrid Benefit to save on compute costs until December 2026. For more information, review the [Hyperscale pricing blog](https://aka.ms/hsignite2023).
