---
title: Configure time zone - Analytics Platform System | Microsoft Docs
description: The Time Zone page enables you to set the time zone for all nodes on your Analytics Platform System (APS) appliance. 
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Appliance time zone configuration - Analytics Platform System
The **Time Zone** page enables you to set the time zone for all nodes on your Analytics Platform System (APS) appliance.  
  
## To set the time zone  
  
1.  Launch the Configuration Manager. For more information, see [Launch the Configuration Manager &#40;Analytics Platform System&#41;](launch-the-configuration-manager.md).  
  
2.  Stop the appliance services by using the **Services Status** page in the Configuration Manager. See [PDW Services Status &#40;Analytics Platform System&#41;](pdw-services-status.md) for instructions.  
  
3.  In the left pane of the Configuration Manager, click **Time Zone**. Select the desired time zone from the **Time Zone** drop-down menu. Depending on your location, you may also choose to select the box next to **Automatically adjust clock for Daylight Saving Time**.  
  
4.  Click **Apply** to save your changes.  
  
5.  Restart the appliance services by using the **Services Status** page in the Configuration Manager. If you are also planning to change the privileges, you can do that before restarting the appliance.  
  
![DWConfig Appliance Time](./media/appliance-time-zone-configuration/SQL_Server_PDW_DWConfig_ApplTopTime.png "SQL_Server_PDW_DWConfig_ApplTopTime")  
  
## See Also  
[Launch the Configuration Manager &#40;Analytics Platform System&#41;](launch-the-configuration-manager.md)  
  
