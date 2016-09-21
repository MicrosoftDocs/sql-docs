---
title: "PDW Processing and Storage Capacity (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2c32fec4-e97c-4797-b7f8-7c8d4301b7b6
caps.latest.revision: 7
author: BarbKess
---
# PDW Processing and Storage Capacity (Analytics Platform System)
Your business requirements determine the number of Data Scale Units, and the size of the Compute node disks that you need in your SQL Server PDW appliance. Use these processing and storage calculations to guide your capacity purchasing and planning decisions.  
  
## Contents  
  
-   [Planning for Processing Capacity](#section1)  
  
-   [Planning for Storage Capacity](#section2)  
  
## <a name="section1"></a>Planning for Processing Capacity  
PDW’s MPP query performance depends heavily on the number of CPU cores working on your data in parallel. Within limits, increasing parallelism improves MPP query performance. Even if your data size is relatively small, the power of the MPP query engine is enhanced by having greater parallelism.  
  
For example, an appliance with 12 Compute nodes has 192 CPU cores that process your data in parallel. That’s 192-way parallelism! An appliance with 56 Compute nodes has 896 cores all working in parallel. This magnitude of parallelism is not achievable with SMP computing.  
  
As the number of Compute nodes increases, scaling out the appliance requires adding more than one Compute node at a time to get a noticeable benefit. Hardware vendors support only specific configurations of Data Scale Units to ensure that the benefit of scaling the appliance outweighs the cost of redistributing the data across more Compute nodes.  
  
### Data Scale Unit Configuration Examples - HPE  
These are examples of the supported HPE configurations for Data Scale Units. They might vary from the most current supported configurations, but are provided as an example of how to increase capacity by approximately 20 percent.  
  
Uplift is the percent capacity gain by increasing the Data Scale Units from one row to the next. For example, increasing the Data Scale Units from 6 to 8 gives a 33% uplift in CPU cores and memory.  It also increases the disk space which isn’t shown in this table.  
  
|Data Scale Units|Compute Nodes|CPU Cores|Memory (GB)|Uplift|  
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
  
-   **Data Scale Units** per appliance. To learn about Data Scale Units, see [Hardware Components &#40;Analytics Platform System&#41;](../architecture/hardware-components-analytics-platform-system.md)  
  
-   **Compute Nodes** per appliance.  
  
-   **CPU cores** per appliance. There are 16 cores per Compute node, one core per each mirrored disk-pair. For Compute node disk structure, see [Hardware Components &#40;Analytics Platform System&#41;](../architecture/hardware-components-analytics-platform-system.md).  
  
-   **Memory** per appliance. Each core has 256 GB memory.  
  
### Data Scale Unit Configuration Examples – Dell, Quanta  
These are examples of the supported Dell and Quanta configurations for Data Scale Units. They might vary from the most current supported configurations, but are provided as an example of how to increase capacity by approximately 20 percent.  
  
Uplift is the percent capacity gain by increasing the Data Scale Units from one row to the next. For example, increasing the Data Scale Units from 6 to 8 gives a 33% uplift in CPU cores and memory. It also increases the disk space which isn’t shown in this table.  
  
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
  
## <a name="section2"></a>Planning for Storage Capacity  
This table estimates that you could load and store up to 6 petabytes of uncompressed user SQL Server PDW data onto a fully built Analytics Platform System appliance. If the appliance has an HDInsight region, the maximum SQL Server PDW storage will be reduced according to the size of the HDInsight region.  
  
|Vendor|Drive Size|Physical Data Storage Per Compute Node|Maximum Compute Nodes Per Rack|Physical Maximum Data Storage Per Rack|Estimated Maximum User Data Storage Per Rack|Maximum Racks|Estimated Maximum User Data Storage Per Appliance|  
|----------|--------------|------------------------------------------|----------------------------------|------------------------------------------|------------------------------------------------|-----------------|-----------------------------------------------------|  
|HPE|1 TB|16 TB|8|128 TB|320 TB|7|2,240 TB|  
|HPE|2 TB|32 TB|8|256 TB|640 TB|7|4,480 TB|  
|HPE|3 TB|48 TB|8|384 TB|960 TB|7|6,720 TB|  
|DELL|1 TB|16 TB|9|144 TB|360 TB|6|2,160 TB|  
|DELL|2 TB|32 TB|9|288 TB|720 TB|6|4,320 TB|  
|DELL|3 TB|48 TB|9|432 TB|1080 TB|6|6,480 TB|  
  
Explanation:  
  
-   **Drive Size** is 1, 2, or 3 TB for each Hardware vendor.  
  
-   **Physical Data Storage Per Compute Node** = (Drive Size) * (16 disks per Compute node). The mirrored disks are not included since they are for redundancy.  
  
-   **Maximum Compute Nodes Per Rack** is specific to the hardware vendor.  
  
-   **Physical Maximum Data Storage Per Rack** = (Physical data Storage Per Compute Node) * (Maximum Compute Nodes Per Rack).  
  
-   **Estimated Maximum User Data Storage Per Rack** = (Physical Maximum Data Storage Per Rack) * (5 for a 5:1 compression ratio) \* (50% for logs and tempDB). This is a conservative estimate for the uncompressed user data that can be loaded and stored onto the appliance. This is an estimate and is not enforced by software. The actual user data storage depends on your data and your configuration.  
  
-   **Maximum Racks** is specific for each Hardware vendor.  
  
-   **Estimated Maximum Data Storage Per Appliance** = (Estimated Maximum Data Storage Per Rack) * (Maximum Racks). This is a conservative estimate of the grand total size of user data that you could load and store on a fully built appliance.  
  
