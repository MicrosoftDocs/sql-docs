---
title: Processing and storage capacity
description: Your business requirements determine the number of Data Scale Units, and the size of the Compute node disks that you need in your Analytics Platform System (APS) appliance.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Processing and storage capacity in Analytics Platform System
Your business requirements determine the number of Data Scale Units, and the size of the Compute node disks that you need in your Analytics Platform System (APS) appliance. Use these processing and storage calculations to guide your capacity purchasing and planning decisions.  
  
  
## <a name="section1"></a>Planning for processing capacity  
Query performance for SQL Server Parallel Data Warehouse (PDW) depends heavily on the number of CPU cores working on your data in parallel. Within limits, increasing parallelism improves the massively parallel processing (MPP) query performance. Even if your data size is relatively small, the power of the MPP query engine is enhanced by having greater parallelism.  
  
For example, an appliance with 12 Compute nodes has 192 CPU cores that process your data in parallel. That's 192-way parallelism! An appliance with 56 Compute nodes has 896 cores all working in parallel. This magnitude of parallelism is not achievable without MPP computing.  
  
As the number of Compute nodes increases, scaling out the appliance requires adding more than one Compute node at a time to get a noticeable benefit. Hardware vendors support only specific configurations of Data Scale Units to ensure that the benefit of scaling the appliance outweighs the cost of redistributing the data across more Compute nodes.  
  
### Data Scale Unit configuration examples - HPE  
These are examples of the supported HPE configurations for Data Scale Uunits. They might vary from the most current supported configurations, but are provided as an example of how to increase capacity by approximately 20 percent.  
  
Uplift is the percent capacity gain by increasing the Data Scale Uunits from one row to the next. For example, increasing the Data Scale units from 6 to 8 gives a 33% uplift in CPU cores and memory.  It also increases the disk space which isn't shown in this table.  
  
|Data Scale units|Compute nodes|CPU cores|Memory (GB)|Uplift|  
|--------------------|-----------------|-------------|-----------------|----------|  
|1|2|32|512|-|  
|2|4|64|1024|100%|  
|3|6|96|1536|50%|  
|4|8|128|2048|33%|  
|5|10|160|2560|25%|  
|6|12|192|3072|20%|  
|8|16|256|4096|33%|  
|10|20|320|5120|25%|  
|12|24|384|6144|20%|  
|16|32|512|8192|33%|  
|20|40|640|10240|25%|  
|24|48|768|12288|20%|  
|28|56|896|14336|17%|  
  
Explanation:  
  
-   **Data Scale units** per appliance. To learn about Data Scale Units, see [Analytics Platform System Hardware Components](hardware-components.md).  
  
-   **Compute Nodes** per appliance.  
  
-   **CPU cores** per appliance. There are 16 cores per Compute node, one core per each mirrored disk-pair. For Compute node disk structure, see [Analytics Platform System Hardware Components](hardware-components.md).  
  
-   **Memory** per appliance. Each core has 256 GB memory.  
  
### Data Scale unit configuration examples - Dell, Quanta  
These are examples of the supported Dell and Quanta configurations for Data Scale Uunits. They might vary from the most current supported configurations, but are provided as an example of how to increase capacity by approximately 20 percent.  
  
Uplift is the percent capacity gain by increasing the Data Scale Uunits from one row to the next. For example, increasing the Data Scale units from 6 to 8 gives a 33% uplift in CPU cores and memory. It also increases the disk space which isn't shown in this table.  
  
|Data Scale Units|Compute Nodes|CPU Cores|Memory (GB)|Uplift|  
|--------------------|-----------------|-------------|-----------------|----------|  
|1|3|48|768|-|  
|2|6|96|1536|100%|  
|3|9|144|2,304|50%|  
|4|12|192|3,072|33%|  
|5|15|240|3,840|25%|  
|6|18|288|4,608|20%|  
|7|21|336|5,376|17%|  
|8|24|384|6,144|14%|  
|9|27|432|6,912|13%|  
|12|36|576|9,216|33%|  
|15|45|720|11,520|25%|  
|18|54|864|13,824|20%|  
  
## <a name="section2"></a>Planning for storage capacity  
This table estimates that you could load and store up to 6 petabytes of uncompressed data onto a fully built Analytics Platform System appliance. 
  
|Vendor|Drive size|Physical data storage Per Compute node|Maximum Compute nodes per rack|Physical maximum data storage per rack|Estimated maximum user data storage per rack|Maximum racks|Estimated maximum user data storage per appliance|  
|----------|--------------|------------------------------------------|----------------------------------|------------------------------------------|------------------------------------------------|-----------------|-----------------------------------------------------|  
|HPE|1 TB|16 TB|8|128 TB|320 TB|7|2,240 TB|  
|HPE|2 TB|32 TB|8|256 TB|640 TB|7|4,480 TB|  
|HPE|4 TB|64 TB|8|512 TB|1280 TB|7|8,960 TB|  
|DELL|1 TB|16 TB|9|144 TB|360 TB|6|2,160 TB|  
|DELL|2 TB|32 TB|9|288 TB|720 TB|6|4,320 TB|  
|DELL|4 TB|64 TB|9|576 TB|1440 TB|6|8,640 TB|   
  
Explanation:  
  
-   **Drive size** is 1, 2, or 4 TB for each Hardware vendor.  
  
-   **Physical data storage per Compute node** = (Drive size) * (16 disks per Compute node). The mirrored disks are not included since they are for redundancy.  
  
-   **Maximum compute nodes per rack** is specific to the hardware vendor.  
  
-   **Physical maximum data storage per rack** = (Physical data storage per Compute node) * (Maximum Compute nodes per rack).  
  
-   **Estimated maximum user data storage per rack** = (Physical maximum data storage per rack) * (5 for a 5:1 compression ratio) \* (50% for logs and tempDB). This is a conservative estimate for the uncompressed user data that can be loaded and stored onto the appliance. This is an estimate and is not enforced by software. The actual user data storage depends on your data and your configuration.  
  
-   **Maximum racks** is specific for each Hardware vendor.  
  
-   **Estimated maximum data storage per appliance** = (Estimated maximum data storage per rack) * (Maximum racks). This is a conservative estimate of the grand total size of user data that you could load and store on a fully built appliance.  
  
