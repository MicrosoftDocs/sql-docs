---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/10/2026
ms.service: azure-sql-managed-instance
ms.topic: include
ms.custom:
  - ignite-2025
---

The Next-gen General Purpose service tier is an architectural upgrade of the existing General Purpose service tier that offers the following key characteristics: 

- Designed for businesses with higher performance requirements while offering the same baseline cost as the General Purpose service tier 
- Support of up to **500 databases per instance**, and a max storage size of 32 TB
- Significant upgrades to performance, scalability and resource flexibility over the General Purpose service tier
- Uses [Elastic SAN](/azure/storage/elastic-san/elastic-san-introduction) instead of page blobs, which drastically improve storage performance metrics 
- 3 free IOPS for every GB of reserved storage
- Scale your instance resources independently by manually adjusting vCores, memory, storage, and IOPS, such as by using the [Create Or Update REST API](/rest/api/sql/managed-instances/create-or-update) or sliders in the Azure portal: 

   :::image type="content" source="media/azure-sql-managed-instance-next-gen-general-purpose-upgrade/scale-next-gen-resources.png" alt-text="Screenshot of scaling your SQL managed instance resources independently in the Azure portal.":::

Because the Next-gen General Purpose service tier is an upgrade to the existing General Purpose service tier, your billing statement always reflects the *General Purpose* service tier. 

#### Architectural model

The Next-gen General Purpose service tier is an upgrade to the existing General Purpose service tier that uses an upgraded remote storage layer to store instance data and log files on Elastic SAN instead of page blobs. This upgrade offers faster storage latency, IOPS, and throughput than the existing General Purpose service tier, with increased limits to storage, the number of vCores, and the max number of databases. Additionally, since the performance quotas are shared by the whole instance, you no longer have to resize individual files to improve their performance. The baseline cost of the Next-gen General Purpose service tier is the same as the General Purpose service tier, but you can use sliders to increase your IO performance and memory to vCore ratio, which is then billed separately. 

The Next-gen General Purpose service tier supports [flexible memory](../../managed-instance/resource-limits.md#flexible-memory), which allows you to choose the amount of memory you want to allocate to your instance. This feature is a significant improvement over the General Purpose service tier, which has a fixed memory allocation based on the number of selected vCores. Flexible memory is generally available for locally redundant instances on Premium-series hardware. Flexible memory for zone-redundant Next-gen General Purpose instances on Premium-series hardware is currently in preview.

The Next-gen General Purpose service tier helps reduce cost by offering free IOPS at three IOPS for every GB of reserved storage. The price of the storage includes the minimum IOPS. If you go above the minimum, you're charged as follows: 1 IOPS = storage price (by region) divided by three. 

For example: 
- If 1 GB of storage costs 0.115, then 1 IOPS = 0.115/3 = 0.038 per IOPS. 
- A 1,024-GB instance receives 3,072 IOPS for free. You can choose to increase your IOPS up to the [VM limit](../../managed-instance/resource-limits.md#iops-and-throughput) for an additional cost. 

#### When to choose this service tier

Choose this service tier if your business is budget-oriented but the performance metrics and limits of the General Purpose service tier are insufficient. 

The key reasons why you should choose the Next-gen General Purpose service tier instead of the General Purpose tier are:

- Better performance for the same baseline cost
- Improved latency, throughput, and IOPS
- Greater storage capacity
- More flexibility for your compute 
- You need over 100 databases for a single instance 
- You need more than 16 TB of reserved storage
