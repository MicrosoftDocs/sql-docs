---
title: "Antivirus Software (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 60ab9a88-d339-4917-a38b-f9481aef38fd
caps.latest.revision: 29
author: BarbKess
---
# Antivirus Software
If your data center requires antivirus software, use these guidelines to install antivirus software on Analytics Platform System. We recommend not installing antivirus software unless it is a firm requirement of your data center.  
  
> [!WARNING]  
> We strongly recommend that you individually assess the security risk for each computer and for Analytics Platform System as a whole, and that you select the tools that are appropriate for the security risk level of each computer. Additionally, we recommend that before you roll out any virus-protection project, you test the whole system under a full load to measure any changes in stability and performance.  
>   
> Virus protection software requires some system resources to execute. You must perform testing before and after you install your antivirus software to determine whether there is any performance effect on the Analytics Platform System.  
  
This topic is based on the guidance in [How to choose antivirus software to run on computers that are running SQL Server](http://support.microsoft.com/kb/309422) and [KB Article 961804](http://support.microsoft.com/kb/961804/en-us).  
  
## Exclusion List for Physical Hosts  
To install the antivirus software on the physical hosts, exclude the following list of directories and processes. These should not be scanned by the antivirus software.  
  
**Exclude these directories:**  
  
-   C:\ProgramData\Microsoft\Windows\Hyper-V - Virtual machine configuration directory  
  
-   C:\Users\Public\Documents\Hyper-V\Virtual Hard Disks - Default virtual hard disk drive directory  
  
-   C:\clusterStorage - Virtual hard disk drive directories  
  
**Exclude these processes:**  
  
-   Virtual machine management (Vmms.exe)  
  
-   Virtual machine work processes (Vmwp.exe)  
  
## Exclusion List for Virtual Machines (VMs)  
To install the antivirus software on the VMs, exclude the following list of directories and files. These should not be scanned by the antivirus software.  
  
***PDW_region*-CTL01**  
  
-   C:\windows\cluster\  
  
-   G:\  
  
***appliance_domain*-AD01** and ***appliance_domain*-AD02**  
  
-   No restrictions  
  
**Compute node VMs**  
  
-   C:\windows\cluster\  
  
-   G:\  
  
***appliance_domain*-VMM**  
  
-   No restrictions  
  
***appliance_domain*-WDS**  
  
-   No restrictions  
  
***appliance_domain*-ISCSI01**  
  
-   C:\iscsitarget  
  
## See Also  
[Appliance Management Tasks &#40;Analytics Platform System&#41;](appliance-management-tasks.md)  
  
