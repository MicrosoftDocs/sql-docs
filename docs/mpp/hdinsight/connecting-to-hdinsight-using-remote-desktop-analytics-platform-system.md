---
title: "Connecting to HDInsight Using Remote Desktop (Analytics Platform System)"
ms.custom: na
ms.date: 08/09/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: df3b03eb-f0c1-4c25-a305-958fdb48f3e0
caps.latest.revision: 10
author: BarbKess
---
# Connecting to HDInsight Using Remote Desktop (Analytics Platform System)
For perform advanced cluster administration, remotely connect to virtual machines in the HDI region.  
  
> [!NOTE]  
> Connecting to the physical machines (from the appliance domain) is not covered by this instruction.  
  
## Scenarios  
Connect to the virtual machines for advanced cluster administration.  
  
-   Troubleshooting (log collection and inspection)  
  
-   Making allowed configuration changes  
  
-   Installing supported helper modules (serialization/encryption libraries…)  
  
## How to Connect  
Connecting by using **Remote Desktop** is a two-step operation.  
  
1.  Use **Remote Desktop** to connect to the secure node (HSN01).  
  
2.  Use **Remote Desktop** on the secure node, to connect to any HDI node in the cluster.  
  
## Connection Parameters  
When connecting to HSN01 connect by using the Ethernet IP address. If the Ethernet IP is mapped in the corporate DNS, you can use the HSN01 host alias.  
  
When connecting to internal cluster nodes, start Remote Desktop on the HSN01 node. Use the host name or InfiniBand IP address to connect to virtual machines in the cluster.  
  
Specify the host name is in the following format: ***HDI_region*-*node_name***, for example: `H12345-HHN01` or `H12345-HDN001`.  
  
Specify the login credentials from the appliance domain. (A user name and password were specified when the accounts were created.)  
  
For both **Remote Desktop** connections, specify the username in the format ***appliance_domain*\john**.  
  
Using local machine accounts for **Remote Desktop** is not recommended.  
  
## Permissions  
Only members of HDInsight **Cluster Administrators** group can connect by using **Remote Desktop**. Regular users are not allowed to remotely connect to region virtual machines.  
  
## See Also  
[Product Documentation &#40;Analytics Platform System&#41;](.././/product-documentation-analytics-platform-system.md)  
[Understanding the Security Model of the HDInsight Region &#40;Analytics Platform System&#41;](../hdinsight/understanding-the-security-model-of-the-hdinsight-region-analytics-platform-system.md)  
  
