---
title: Configure Windows to receive remote table copies - Parallel Data Warehouse | Microsoft Docs
description: Describes how to purchase and configure a non-appliance Windows system connected using the InfiniBand network for use with the remote table copy feature in Parallel Data Warehouse. The Windows system will host the SQL Server database that receives the remote table copy from a SQL Server PDW database. It is purchased separately from the appliance and connected to the appliance InfiniBand network. 
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---


# Configure an external Windows system to receive remote table copies using InfiniBand - Parallel Data Warehouse
Describes how to purchase and configure a non-appliance Windows system connected using the InfiniBand network for use with the remote table copy feature in SQL Server PDW. The Windows system will host the SQL Server database that receives the remote table copy from a SQL Server PDW database. It is purchased separately from the appliance and connected to the appliance InfiniBand network.  
  
> [!NOTE]  
> Connecting through the InfiniBand network is not required for using remote table copy. Connecting through the Ethernet network can be done if the Ethernet bandwidth meets your needs.  
  
This topic describes one of the configuration steps for configuring remote table copy. For a list of all the configuration steps, see [Remote Table Copy](remote-table-copy.md)  
  
## Before You Begin  
Before you configure the external Windows system, you must:  
  
1.  Purchase or provide a Windows system that will receive the remote copies.  
  
2.  Rack the Windows system in the Control rack (if there is enough space) or close enough to the appliance so that you can connect it to the appliance InfiniBand network.  
  
3.  Purchase InfiniBand cables and an InfiniBand network adapter from your appliance hardware vendor. We recommend purchasing a network adapter with two ports for fault tolerance when receiving the exported data. A two port network adapter is recommended, but is not a requirement.  
  
## <a name="HowToWindows"></a>Configure an External Windows System To Receive Remote Table Copies  
To configure the external Windows system, use the following steps:  
  
1.  Install the InfiniBand network adapter into your Windows system.  
  
2.  Connect the InfiniBand network adapter to the main InfiniBand switch in the Control rack using InfiniBand cables.  
  
3.  Install and configure the appropriate Windows driver for the InfiniBand network adapter.  
  
    InfiniBand drivers for Windows are developed by the OpenFabrics Alliance, an industry consortium of InfiniBand vendors.  The correct driver may have been distributed with your InfiniBand adapter. If not, the driver can be downloaded from www.openfabrics.org.  
  
4.  Configure the IP address for each port on the adapter. SMP systems are required to use static IP addresses from a range of addresses reserved for this purpose. Configure the first port according to the following parameters:  
  
    -   IP network address: 172.16.132.x  
  
    -   IP subnet mask: 255.255.128.0  
  
    -   IP host range: 1-254  
  
    For InfiniBand network adapters with two ports, configure the second port according to the following parameters:  
  
    -   IP network address: 172.16.132.x  
  
    -   IP subnet mask: 255.255.128.0  
  
    -   IP host range: 1-254  
  
5.  If a two port adapter is used, or multiple external Windows systems are connected to an appliance, assign each system a different host number within each IP subnet.  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
