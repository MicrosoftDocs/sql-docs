---
title: "How to: Run the Upgrade Advisor Analysis Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade Advisor Analysis Wizard"
ms.assetid: d7d2a1e2-1179-4c05-9b0f-555b04dd1199
author: mashamsft
ms.author: mathoma
manager: craigg
---
# How to: Run the Upgrade Advisor Analysis Wizard
  You start the Upgrade Advisor Analysis Wizard from the Upgrade Advisor start page. This topic describes how to run the Upgrade Advisor Analysis Wizard.  
  
> [!IMPORTANT]
>  When you run the Upgrade Advisor Analysis Wizard, Upgrade Advisor saves the reports in the default report folder. However, the report viewer displays only the five latest saved reports. The default location for the reports is My Documents\\[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor\110\Reports.  
  
### To run the Upgrade Advisor Analysis Wizard  
  
1.  On the Upgrade Advisor start page, click **Launch Upgrade Advisor Analysis Wizard**.  
  
2.  On the **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Components** page, enter the name of the server to scan in the **Server name** box, and then click **Detect**. Use the following guidelines for the server name:  
  
    -   To scan non-clustered instances, enter the computer name.  
  
    -   To scan clustered instances, enter the virtual [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] name.  
  
    -   To scan non-clustered components that are installed on a node of a cluster, enter the node name.  
  
    > [!WARNING]  
    >  Upgrade Advisor does not support connecting to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that is not set to use the standard port (1433) for client connections. If you want to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that does not use the standard port (1433), create an alias using the IP address and the port. For more information about configuring client protocols and creating an alias for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances, see [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md).  
    >   
    >  If you do not have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on the computer on which you are running Upgrade Advisor, click **Start**, and then run  `cliconfg`. This opens the **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Client Network Utility** dialog box. Use the **Alias** tab to create the alias.  
  
3.  Review the list of components detected, modify the selections as necessary, and then click **Next**.  
  
4.  On the **Connection Parameters** page, select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you want to scan, select the authentication method and, if necessary, enter user name and password information, and then click **Next**.  
  
     The default instance name is MSSQLSERVER.  
  
5.  For selected components, enter the requested information. For more information about individual dialog boxes, see [Upgrade Advisor User Interface Reference](../../../2014/sql-server/install/upgrade-advisor-user-interface-reference.md).  
  
6.  On the **Confirm Upgrade Advisor Setting** page, review the information that you entered. You can select **Send reports to [!INCLUDE[msCoName](../../includes/msconame-md.md)]** if you want to submit your upgrade report. You can also review the privacy policy.  
  
7.  Click **Run** to analyze the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
8.  When the analysis is finished, click **Launch Report** to view the detected upgrade issues.  
  
## See Also  
 [How to: Launch Upgrade Advisor](../../../2014/sql-server/install/how-to-launch-upgrade-advisor.md)   
 [Running Upgrade Advisor &#40;User Interface&#41;](../../../2014/sql-server/install/running-upgrade-advisor-user-interface.md)   
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
