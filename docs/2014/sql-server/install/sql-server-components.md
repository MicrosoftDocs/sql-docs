---
title: "SQL Server Components | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Upgrade Advisor, components"
  - "listing components to analyze"
  - "Upgrade Advisor [SQL Server], components"
  - "component analysis [Upgrade Advisor]"
  - "finding components to analyze"
  - "locating components to analyze"
  - "detecting components to analyze"
  - "server names [Upgrade Advisor]"
  - "analyzing system [Upgrade Advisor], component list"
  - "identifying components to analyze"
ms.assetid: 539b9525-ce3f-4950-9146-5527a5a297ee
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Components
  You can run the Upgrade Advisor Analysis Wizard against a local or remote computer that has [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] installed. The first step in the pre-upgrade analysis is to identify the computer and components for analysis.  
  
## Options  
 **Computer name**  
 Specifies the name of the computer to analyze. Upgrade Advisor populates the **Server name** box with the local computer name. You can also use "." and "localhost" to connect to the local computer.  
  
 To analyze a different computer, use the following guidelines:  
  
-   To scan non-clustered instances, enter the computer name.  
  
-   To scan clustered instances, enter the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance.  
  
-   To scan non-clustered components that are installed on a node of a cluster, enter the computer name of the failover cluster node.  
  
    > [!IMPORTANT]  
    >  Do not include the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name.  
  
 Instead of specifying the computer name, you can specify the IP address.  
  
 If scanning [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you must specify the name of the local computer. Upgrade Advisor scans local report servers only.  
  
 **Detect**  
 The **Detect** button accesses the specified computer and detects components to analyze:  
  
-   If you are analyzing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on a remote computer, you must enable the remote registry services on the remote computer.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is detected if an instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] is found in the computer's registry.  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is detected if an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is found in the computer's registry.  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is detected if [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is found in the computer's registry. However, Upgrade Advisor scans local report servers only.  
  
 **Components**  
 Select the components to analyze. You can click the **Detect** button to select all the components installed on the computer. A check mark will appear next to the components that are detected as installed on the computer. You can also manually select the components to analyze by selecting or clearing the check box next to each component.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)   
 [Upgrade Advisor User Interface Reference](../../../2014/sql-server/install/upgrade-advisor-user-interface-reference.md)  
  
  
