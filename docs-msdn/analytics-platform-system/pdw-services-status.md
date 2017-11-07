---
title: "PDW Services Status (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3fc9bee2-c372-4c4a-956c-fb54215d8918
caps.latest.revision: 14
author: BarbKess
---
# PDW Services Status
The Parallel Data Warehouse **Services Status** page in the Microsoft Analytics Platform System Configuration Manager shows the current status of all SQL Server PDW services, and provides the ability to stop and start the PDW services. This is the only supported method for starting and stopping the PDW services. Note that individual components or services cannot be started independently.  
  
#### To start or stop the appliance services  
  
1.  To start the appliance services, click **Start Appliance**.  
  
2.  To stop the appliance services, click **Stop Appliance**.  
  
It is not necessary to click **Apply** when starting and stopping the appliance services by using **Start Appliance** and **Stop Appliance**.  
  
![DWConfig Appliance PDW Services](./media/pdw-services-status/SQL_Server_PDW_DWConfig_ApplPDWServices.png "SQL_Server_PDW_DWConfig_ApplPDWServices")  
  
> [!NOTE]  
> Stopping the PDW Region also stops the PDW agent (sqldwagent) on the nodes of the HDInsight Region. The HDInsight Region is still functional, however health monitoring will not be available. (The PDW agent requires the PDW control node to report health monitoring.)  
  
## See Also  
[Power the APS Appliance On or Off &#40;Analytics Platform System&#41;](power-the-aps-appliance-on-or-off.md)  
  
