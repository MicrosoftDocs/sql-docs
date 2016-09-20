---
title: "HDInsight Firewall Configuration (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3196fdeb-023a-41ec-b03c-3a7cb18ce5ab
caps.latest.revision: 8
author: BarbKess
---
# HDInsight Firewall Configuration (Analytics Platform System)
The **Firewall** page of the SQL Server PDW Configuration Manager enables you to enable or disable firewall rules that allow or prevent access to specific ports on the HDInsight region of the Microsoft Analytics Platform System.  
  
## To manage ports and firewall rules for appliance nodes  
  
1.  Launch the Configuration Manager. For more information, see [Launch the Configuration Manager &#40;Analytics Platform System&#41;](../../mpp/management/launch-the-configuration-manager-analytics-platform-system.md).  
  
2.  In the left pane of the Configuration Manager, expand **HDInsight Topology**, and then click **Firewall**.  
  
3.  Locate the port or firewall rule to update in the configuration list, and then select or clear the box next to that item. Only administrator configurable options are shown in this list, including opening and closing ports on externally facing nodes.  
  
4.  Click **Apply** to save your changes.  
  
![DWConfig Appliance HDI Firewall](../../mpp/management/media/SQL_Server_PDW_DWConfig_ApplHDIFirewall.png "SQL_Server_PDW_DWConfig_ApplHDIFirewall")  
  
## External Ports  
The following ports are opened for client connections coming from outside of HDInsight.  
  
|Purpose|Port #|Nodes|  
|-----------|-----------|---------|  
|SSL encrypted connections (For internal communications and to access HDInsight cluster services)|443|All nodes|  
|Client access to HDInsight Developer Dashboard|81|HSN01|  
|HDInsight remote desktop access|3389|HSN01|  
|HDInsight cluster outbound connectivity|Any|HHN01, HDN*xyz* (all data nodes)|  
  
> [!NOTE]  
> Creating external tables or external data sources uses TCP port 8020 by default. These statements can be configured to use other ports instead. The Hortonworks JOB_TRACKER_LOCATION default port is 50300. Integrating with other systems and tools may require additional ports.  
  
## See Also  
[PDW Firewall Configuration &#40;Analytics Platform System&#41;](../../mpp/management/pdw-firewall-configuration-analytics-platform-system.md)  
  
