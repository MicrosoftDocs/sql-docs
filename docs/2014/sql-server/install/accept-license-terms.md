---
title: "Accept License Terms | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "license terms"
helpviewer_keywords: 
  - "Registration Information page [SQL Server Installation Wizard]"
  - "SQL Server Installation Wizard, Registration Information page"
ms.assetid: 08dd739d-5817-4418-bcff-74ab7f8bbd33
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Accept License Terms
  Use the **Accept License Terms** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to accept the license terms for this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 You can print the license agreement or copy it to the Clipboard. To continue, accept the license terms, and then click **Next**. To close the installation, click **Cancel**.  
  
## Customer Experience Improvement Program (CEIP)  
 If you enable CEIP reporting, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to periodically send a report to [!INCLUDE[msCoName](../../includes/msconame-md.md)]. Reports include information about your hardware configuration and how you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and components. [!INCLUDE[msCoName](../../includes/msconame-md.md)] will use feature usage data to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components monitored by this feature include the following:  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
  
-   Replication  
  
-   [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup  
  
 Information about feature use is sent to [!INCLUDE[msCoName](../../includes/msconame-md.md)], where it is stored with limited access.  
  
 To disable CEIP reporting after Setup completes, use the **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Error and Usage Reporting** tool on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**Configuration Tools** menu.  
  
 For Setup actions like installation, upgrade, repair, and so on, information is collected and uploaded only during the Setup program execution  
  
 For all other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, information is collected one time per day for all enabled instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, the time of collection is midnight to minimize the load on the server. If you want to change the time of collection, you can manually edit the registry key that controls the collection time. Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has its own registry key:  
  
 HKLM\Software\\[!INCLUDE[msCoName](../../includes/msconame-md.md)]\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL12.\<INSTANCEID>\CPE\TimeofReporting  
  
 The value of this registry key contains the time for the collection as the number of minutes from 00:00 (midnight) to run. For example, a value of 60 would run the collection at 1:00 a.m., a value of 1200 would run the collection at 8:00 p.m., and so on.  
  
## Error Reporting  
 Use the **Error and Usage Report Settings** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to enable feature error and usage reporting functionality for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Options  
 By default, the Feature Usage data collection and Error Reporting features are disabled for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and its components in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Error Reporting  
 If you enable the Error Reporting feature, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to send a report to [!INCLUDE[msCoName](../../includes/msconame-md.md)] automatically if a fatal error occurs in any of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components:  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
  
-   Replication  
  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] uses error reports to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality and treats all information as confidential.  
  
 Information about errors is sent over a secure (https) connection to [!INCLUDE[msCoName](../../includes/msconame-md.md)], where it is stored with limited access. Alternatively, error reports can be sent to your own Corporate Error Reporting server.  
  
 Error reports contain the following information:  
  
-   The condition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when the problem occurred.  
  
-   The operating system version and computer hardware information.  
  
-   Your Digital Product ID, which is not used to identify your license.  
  
-   The network IP address of your computer or proxy server.  
  
-   Information from memory or file(s) of the process that caused the error.  
  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] does not intentionally collect your files, name, address, e-mail address, or any other form of personal information. The error report can, however, contain personal information from the memory or files of the process that caused the error. Although this information can potentially be used to determine your identity, [!INCLUDE[msCoName](../../includes/msconame-md.md)] does not use this information for that purpose.  
  
 For more information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] privacy and data collection policy, see [Microsoft SQL Server Privacy Statement](../../../2014/getting-started/microsoft-sql-server-privacy-statement.md).  
  
 If you enable Error Reporting and a fatal error occurs, you might see a response from [!INCLUDE[msCoName](../../includes/msconame-md.md)] in the Windows Event log that points to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base article on a particular error.  
  
 To disable Error or Feature Usage reporting for all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and its components after Setup completes, go to the **Error and Usage Report Settings** dialog and clear the check boxes for **Feature Usage**. If **Error Reporting** is enabled for multiple components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and shared components) you can disable Error Reporting for each instance of an individual component as well as shared components, listed as **Others**.  
  
## See Also  
 [About the SQL Server License Terms](../../../2014/getting-started/about-the-sql-server-license-terms.md)  
  
  
