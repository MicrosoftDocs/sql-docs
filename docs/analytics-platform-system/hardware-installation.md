---
title: Hardware installation - Analytics Platform System | Microsoft Docs
description: This article describes how to move, unpack, and install the hardware for your SQL Server PDW appliance. This article is informational only and is intended to help you understand the process. Your appliance should be unpacked, installed, and verified before it is turned over to you. Customer participation is required for items such as data center access, electrical power, and Ethernet connections.  
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Hardware installation for Analytics Platform System appliance
This article describes how to move, unpack, and install the hardware for your SQL Server PDW appliance. This article is informational only and is intended to help you understand the process. Your appliance should be unpacked, installed, and verified before it is turned over to you. Customer participation is required for items such as data center access, electrical power, and Ethernet connections.  
  
## <a name="BeforeMoving"></a>Before You Move Any Components from the Loading Dock  
Perform the following tasks before you move, unpack, or rack any of the appliance components.  
  
|Task|Description|  
|--------|---------------|  
|Verify that all components have arrived|Use the Bill of Materials (BOM) to verify that all components have arrived and are on their pallets at the receiving dock for your data center.|  
|Verify that the data center meets all requirements for the appliance|Start this task by reviewing the hardware specifications and cabling diagrams providing by your IHV. The next steps provide specifics about rack space and connectivity requirements.|  
|Verify that the data center has proper rack space|Verify that the data center has enough space for all of the appliance racks.<br /><br />Verify that the rack space is empty and ready to receive the appliance racks.|  
|Verify that the data center meets connectivity requirements|Verify that the data center meets the cabling requirements in the cabling diagrams.<br /><br />Verify that there will be room for all cables and power cords after the appliance nodes are racked.|  
|Verify that the floors between the dock and the racks meet weight requirements|Verify that all flooring between the pallets and the racks can support the weight of the appliance nodes, especially in data centers with raised floors.<br /><br />Contact your IHV for information on the weight of each component.|  
|Secure the data center rack|Secure the data center rack in place using additional equipment as required for your data center location, such as earthquake straps in geographic areas prone to earthquakes.|  
|Prepare for assistance with transporting the components|Determine in advance what assistance, equipment, and tools you will need to handle each component safely and without causing damage.|  
  
## <a name="Moving"></a>Move the Racks from the Loading Dock into the Data Center  
Each pallet contains all components for one appliance rack, including nodes, cables, cords, etc.  
  
Use the following checklist to move each appliance rack from the pallet at the loading dock to its rack location in the data center. Move the control rack first, and then move the other appliance data racks.  
  
> [!WARNING]  
> Failure to perform these steps exactly as described could result in bodily harm, damage to your SQL Server PDW appliance, or other problems.  
>   
> Never attempt to lift or move an appliance node or other heavy component without assistance or proper equipment. Contact your IHV for information on the weight of each component so that you can determine in advance what assistance, equipment, and tools you will need to handle each component safely and without causing damage.  
  
|Task|Description|  
|--------|---------------|  
|Verify that the pallet is level|Before you begin to move or unpack the pallet, be sure it is on level ground.|  
|Unbolt a node from the pallet|Starting at the top of the pallet, unbolt the top node from the pallet.|  
|Move the node to a dolly or cart that can support the weight|Use ramps and proper lifting/moving techniques to move the node to a dolly or cart that can support the weight.|  
|Transport the node into the data center|Use proper lifting/moving techniques to move the node into position in the data center rack.|  
|Secure the node in the data center rack|Secure the node in place in the data center rack.|  
|Repeat these steps for the next node or component|Repeat these steps to move the next node or other appliance component into the data center.|  
  
## <a name="AfterMoving"></a>Install Additional Components  
Use the following checklist to install the additional components.  
  
|Task|Description||  
|--------|---------------|-|  
|Unpack and rack network switches and PDUs|Use the rack diagrams to place the network switches and PDUs in the proper location in the rack.||  
|Connect the Infiniband and Ethernet cables according to the cable labels|See the cabling diagram. Each cable has a label on each end that specifies where it needs to be connected.||  
|Connect all power cables|See the cabling diagram.||  
|Turn on the power supply to the racks and the PDUs|Connect the power supply to the racks and from the racks to the PDUs. **Do not power on any of the other appliance components at this time.**||  
  
<!-- MISSING LINKS ## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
  
