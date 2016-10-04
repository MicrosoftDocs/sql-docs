---
title: "Hardware Configurations (Analytics Platform System)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f95945b7-97ae-4ab9-bae5-c792a516acea
caps.latest.revision: 9
author: BarbKess
---
# Hardware Configurations (Analytics Platform System)
SQL Server PDW hardware is architected with scalable units so that you buy the right amount of processing and storage according to your business requirements. The appliance scales storage for the SQL Server PDW Region from a few Terabytes to over 6 Petabytes of data.  
  
## Contents  
  
-   [PDW One-Rack Configurations](#section1)  
  
-   [PDW Multi-Rack Configurations](#section2)  
  
-   [HDI Minimum Configuration](#section3)  
  
## <a name="section1"></a>PDW One-Rack Configurations  
The first rack in the appliance contains the components required to run the PDW Region and the HDI Region, if there is an HDI Region. The PDW Region is required for all appliances, and so the minimum appliance configuration is a Rack and Network plus a PDW Base Scale Unit.  
  
These diagrams show ways that the first rack of the appliance can be configured for the PDW Region. You can have between 2 and 9 Compute nodes in the first rack, depending on the hardware vendor.  
  
### First Rack Configurations - DELL  
The minimum configuration for a DELL appliance has 3 Compute nodes. You can add up to 2 Data Scale Units to the first rack for a total of 9 Compute nodes.  
  
![Base Scale Unit](../architecture/media/SQL_Server_PDW_HW_DELL_BaseScaleUnit.png "SQL_Server_PDW_HW_DELL_BaseScaleUnit")  
  
### First Rack Configurations - HPE  
The minimum configuration for an HPE appliance has 2 Compute nodes. You can add up to 3 Data Scale Units to the first rack for a total of 8 Compute nodes.  
  
![PDW first rack configurations for HP](../architecture/media/SQL_Server_PDW_HW_HP_BaseScaleUnit.png "SQL_Server_PDW_HW_HP_BaseScaleUnit")  
  
## <a name="section2"></a>PDW Multi-Rack Configurations  
You add capacity to the PDW Region by adding Data Scale Units, along with additional Rack & Network components as necessary to provide the proper power, networking, and rack infrastructure. Each additional Rack & Network requires a PDW passive host.  
  
Each hardware vendor specifies the number of Data Scale Units you can add given the capacity of your appliance. We recommend adding enough Data Scale units to see at least a 20 percent uplift in performance. For example, adding one Data Scale Unit to an appliance that already has 20 Data Scale Units might result in a negligible performance gain. The net gain wouldn’t be worth the cost and effort.  
  
### Scale Out Example - HPE  
This diagram shows a 3 rack HP appliance that contains 20 Compute nodes.  
  
![PDW Region with 20 Compute Nodes (HP)](../architecture/media/APS_HW_PDWRegionGrowth_HP.png "APS_HW_PDWRegionGrowth_HP")  
  
### Scale Out Example – DELL, Quanta  
This diagram shows a 3 rack DELL or Quanta appliance that contains 21 Compute nodes.  
  
![Dell or Quanta PDW Region](../architecture/media/APS_HW_PDW_RegionGrowth_DellQuanta.png "APS_HW_PDW_RegionGrowth_DellQuanta")  
  
## <a name="section3"></a>HDI Minimum Configuration  
To configure APS with an HDI Region, at a minimum you need the PDW Region plus the HDI Base Scale Unit. Data scale units in additional racks can also be dedicated to the HDI Region.  
  
> [!NOTE]  
> When you determine the capacity of the HDI Region, please take into consideration that you cannot expand the HDI Region by simply adding more hardware. If you need to expand with this version, you can do this by adding hardware and then rebuilding the HDI Region.  
  
![Minimal HDI Region](../architecture/media/APS_HW_MultiRegion_MinConfig.png "APS_HW_MultiRegion_MinConfig")  
  
