---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/23/2024
ms.service: sql-managed-instance
ms.topic: include
---

The Next-gen General Purpose service tier is an architectural upgrade of the existing General Purpose service tier that offers the following key characteristics: 

- Designed for businesses with higher performance requirements while offering the same baseline cost as the General Purpose service tier 
- Significant upgrades to performance, scalability, and resource flexibility over the General Purpose service tier
- Uses managed disks instead of page blobs, which drastically improve storage performance metrics 
- 3 free IOPS for every GB of reserved storage
- Support of up to 500 databases per instance, and a max storage size of 32 TB

Since the Next-gen General Purpose service tier is an upgrade to the existing General Purpose service tier, regardless of which service tier your instance uses, your billing statement reflects the *General Purpose* service tier. 

#### Architectural model

The Next-gen General Purpose service tier is an upgrade to the existing General Purpose service tier that uses an upgraded remote storage layer to store instance data and log files on managed disks instead of page blobs. This means the Next-gen General Purpose service tier upgrade offers faster storage latency, IOPS, and throughput than the existing General Purpose service tier, with increased limits to storage, the number of vCores, and the max number of databases. Additionally, since the performance quotas are shared by the whole instance, you no longer have to resize individual files to improve their performance. The baseline cost of the Next-gen General Purpose service tier is the same as the General Purpose service tier, but you can use sliders to increase your IO performance, which is then billed separately. 

The Next-gen General Purpose service tier helps reduce cost by offering free IOPS at three IOPS for every GB of reserved storage. The price of the storage includes the minimum IOPS. If you go above the minimum, you're charged as follows: 1 IOPS = storage price (by region) divided by three. 

For example: 
- If 1 GB of storage costs 0.115, then 1 IOPS = 0.115/3 = 0.038 per IOPS. 
- A 1,024-GB instance receives 3072 IOPS for free. You can choose to increase your IOPS up to the [VM limit](../../managed-instance/resource-limits.md#iops) for an additional cost. 

#### When to choose this service tier

Choose this service tier if your business is budget-oriented but the performance metrics and limits of the General Purpose service tier are insufficient. 

The key reasons why you should choose the Next-gen General Purpose service tier instead of the General Purpose tier are:

- Better performance for the same baseline cost
- Improved latency, throughput, and IOPS
- Greater storage capacity
- More flexibility for your compute 
- You need over 100 databases for a single instance 
- You need more than 16 TB of reserved storage