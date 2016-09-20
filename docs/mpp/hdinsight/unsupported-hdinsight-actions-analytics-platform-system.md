---
title: "Unsupported HDInsight Actions (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 06a610ed-fd99-4b32-b219-f39f1f8261e1
caps.latest.revision: 6
author: BarbKess
---
# Unsupported HDInsight Actions (Analytics Platform System)
When Analytics Platform System tools are available, always use Analytics Platform System tools to perform actions. The Analytics Platform System tools are engineered to perform actions in methods that keep Analytics Platform System running reliably.  
  
## Unsupported Actions  
  
|Unsupported Action|Recommended Action|  
|----------------------|----------------------|  
|Do not start or stop services from the Windows local services tool.|Use the **Services Status** page of the Configuration Manager tool. For more information, see [HDInsight Services Status &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-services-status-analytics-platform-system.md).|  
|Do not manage user accounts in Active Directory.|Use the **User Management** page of the Configuration Manager tool. For more information, see . [HDInsight User Management &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-user-management-analytics-platform-system.md)|  
|Do not enable remote desktop access to regular users.|Regular users should access HDInsight and Hadoop services through the Developer Dashboard tool. For more information, see . [HDInsight Region Developer Dashboard &#40;Analytics Platform System&#41;](../../mpp/hdinsight/hdinsight-region-developer-dashboard-analytics-platform-system.md).|  
|Do not manage IIS hosted apps or change IIS configuration.|The current configuration is required for Analytics Platform System.|  
|Do not change SQL Server configuration on HMN01.|The current configuration is required for Analytics Platform System.|  
|Do not use Hadoop command line options to run Hadoop services.|Use the DW Configuration Manager to start and stop HDInsight services. For more information, see [HDInsight Services Status &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-services-status-analytics-platform-system.md).|  
|Do not change Hadoop configurations that are not specified in the topic [Supported Hadoop Configuration Changes &#40;Analytics Platform System&#41;](../../mpp/hdinsight/supported-hadoop-configuration-changes-analytics-platform-system.md).|Not applicable.|  
|Do not install additional tools on APS. For example installing HBase, Flume, Mahout, or ZooKeeper in the HDInsight region is not supported.|Not applicable.|  
  
## See Also  
[Changing Hadoop Configuration Settings &#40;Analytic Platform System&#41;](../../mpp/hdinsight/changing-hadoop-configuration-settings-analytic-platform-system.md)  
[Supported Hadoop Configuration Changes &#40;Analytics Platform System&#41;](../../mpp/hdinsight/supported-hadoop-configuration-changes-analytics-platform-system.md)  
  
