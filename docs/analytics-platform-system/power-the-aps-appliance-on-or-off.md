---
title: Power the appliance on or off - Analytics Platform System | Microsoft Docs
description: Power the appliance on or off for Analytics Platform System 
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Power the appliance on or off for Analytics Platform System
This topic describes how to power on or power off your Analytics Platform Systemappliance that is running Parallel Data Warehouse. Use this topic when a Analytics Platform System appliance is moved, or to power on an appliance after a catastrophic power failure.  
  
Powering the appliance on and off is not the same as starting and stopping the appliance services. For information on that subject, see [PDW Services Status &#40;Analytics Platform System&#41;](pdw-services-status.md). For information about powering on or off a SQL Server 2008 Parallel Data Warehouse, see the SQL Server 2008 Parallel Data Warehouse help file. For information about powering on or off a SQL Server 2012 AU1 or AU2 Parallel Data Warehouse, see the help file for those versions.  
  
When these instructions specify connecting to a SQL Server PDW node, the connection can be local using attached devices (KVM) or remote using Remote Desktop. Some actions must be physical (turning on a power switch), and some (such as shut down) can be physical or by using Windows commands.  
  
Connections to SQL Server PDW nodes can be made using the IP addresses assigned to the nodes, or from the **HST01** computer by using either the **Failover Cluster Manager** (**cluadmin.msc**) or **Hyper-V Manager** (**virtmgmt.msc**) applications and right-clicking the node name.  
  
## <a name="PowerOff"></a>Power Off the Appliance  
  
### Before you begin  
Before powering off the appliance, you should end all activity on the appliance. To end all activity:  
  
-   Use the **Sessions** page of the Admin Console to identify the current users. Contact them and ask them to log off.  
  
-   If necessary you can use the **KILL** statement to force the termination of a client connection. Use caution when killing connections. When interrupted, some transactional processes, such as a long running update, must rollback activity before SQL Server can complete the recovery of the database. Rolling back a partially completed update or delete, can be time consuming.  
  
### To power off the appliance  
  
> [!WARNING]  
> All steps must be performed in the exact order listed and each step must complete before the next step is performed, unless otherwise noted. Performing steps out of order or without waiting for each step to complete can result in errors when the appliance is powered up at a later time.  
  
1.  Connect to the PDW Control node (**_PDW_region_-CTL01** ) and login with the Analytics Platform System appliance domain administrator account.  
  
2.  Run `C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\dwconfig.exe` to open the **Configuration Manager**.  
  
3.  In the Configuration Manager, under the **Parallel Data Warehouse Topology** menu, click the **Services Status** tab, and click **Stop Region** to stop PDW services.   
  
4.  Connect to **_appliance_domain_-HST01** and login with the appliance domain administrator account.  
  
5.  Using the **Failover Cluster Manager** connect to the **_appliance_domain_-WFOHST01** cluster, if not automatically connected, and then in the Navigation pane, click **Roles**. In the **Roles** pane:  
  
    1.  Multi-select all of the virtual machines. Right-click them, and select **Shut Down**.  
  
    2.  Wait for all selected VMs to finish shutting down.  
  
6.  Close the **Failover Cluster Manager** application.  
  
7. Shut down all the servers except **_appliance_domain_-HST01**.  
  
8. Shut down the **_appliance_domain_-HST01** server.  
  
9. Shut down the Power Distribution Units (PDUs).  
  
## <a name="PowerOn"></a>Power On the Appliance  
  
### To power on the appliance  
  
> [!WARNING]  
> All steps must be performed in the exact order listed and each step must complete before the next step is performed, unless otherwise noted. Performing steps out of order or without waiting for each step to complete can result in startup errors.  
  
1.  Power on the Power Distribution Units (PDU's), and wait for the switches to automatically start.  
  
2.  Power on the **_appliance_domain_-HST01** server.  
  
3.  Log in to **_appliance_domain_-HST01** as the appliance domain administrator.  
  
4.  Start the **Hyper-V Manager** program (**virtmgmt.msc**) and connect to **_appliance_domain_-HST01** if not connected by default.  
  
    1.  If you cannot connect by name because the **_PDW_region_-AD01** is not running, try connecting by using the IP address.  
  
    2.  In the **Virtual Machines** pane, locate **_PDW_region_-AD01** and confirm that it is running. If not, start this VM and wait for it to be fully started.  
  
5.  Power on the rest of the servers in the appliance.  
  
6.  While on **HST01** logged on as the appliance domain administrator, from **Hyper-V Manager**:  
  
    1.  Connect to **_appliance_domain_-HST02**.  
  
    2.  In the **Virtual Machines** pane, locate **_PDW_region_-AD02** and confirm that it is running.  If not, start this VM and wait for it to be fully started.  
  
7.  Using the **Failover Cluster Manager** connect to the **_appliance_domain_-WFOHST01** cluster, if not automatically connected, and then in the **Navigation** pane, click **Roles**. In the **Roles** pane:  
  
    1.  Multi-select all of the virtual machines, right-click them, and then click **Start**.  
  
    2.  Wait for all selected VMs to finish starting before proceeding to the next step.  
  
    3.  If necessary for the VMs that failed over, shut them down, move them, and restart them on the proper primary host.  
  
8. Disconnect from **HST01** if you wish.  
  
9. Connect to **_PDW_region_-CTL01** using the appliance domain administrator account.  
  
10. Run `C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\dwconfig.exe` to launch the **Configuration Manager**.  
  
11. In the **Configuration Manager**, in the **Parallel Data Warehouse Topology** menu, click the **Services Status** tab, and click **Start Region** to start PDW services.  
  
### To verify the appliance health  
After the appliance has started, open the **Admin Console** and check the Health page for alerts that might indicate failure conditions. For more information, see [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](monitor-the-appliance-by-using-the-admin-console.md).  
  
## See Also  
[Appliance Management Tasks &#40;Analytics Platform System&#41;](appliance-management-tasks.md)  
  
