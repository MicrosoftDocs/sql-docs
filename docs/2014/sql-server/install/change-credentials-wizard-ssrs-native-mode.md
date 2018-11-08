---
title: "Change Credentials Wizard (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.changecredentialswizard.F1"
helpviewer_keywords: 
  - "Change Credentials Wizard"
  - "report server database, reconfigure"
ms.assetid: 9eb4060a-9c3e-41e0-8767-3cfaebc45de7
author: markingmyname
ms.author: maghan
manager: craigg
---
# Change Credentials Wizard (SSRS Native Mode)
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager provides the Change Credentials Wizard to guide you through the steps of reconfiguring the account that the report server uses to connect to the report server database. When you change credentials, the Configuration Manager will update all permissions and database login information on the database server for the report server database that is actively used by the report server.  
  
 To start the wizard, click **Change Credentials** on the Database page in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. For instructions on how to start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
## Options  
 **Database Server**  
 Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that runs the report server database.  
  
 To connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, you must use credentials that have permission to log on to the server and update database information. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager uses your current Windows credentials, but if you do not have a login or database permissions, you can specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login.  
  
 You cannot specify different Windows credentials. If you want to connect as a different Windows user, login as that user and then start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager.  
  
 **Credentials**  
 Specifies the account by which the report server connects to the report server database. Valid values include the service account of the Report Server Web service, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login defined on the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance you are using to host the report server, or a Windows account. If you are using a Windows account, you can specify a local account (*\<computername>\\<username\>*) if the report server and the database are on the same computer, or a domain user account (*\<domain>\\<username\>*) if they are on different computers in the same domain.  
  
 The report server will create a database login and assign database permissions for the account you specify.  
  
 The report server does not create the account itself. The account you specify must already exist and it must be valid for your deployment configuration. Specifically, if the database is on a remote computer and you want to use a Windows account, you must specify an account that has log on permissions on that computer.  
  
 If the computer is in a different or non-trusted domain, consider using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login. For more information about choosing an account, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
 **Summary**  
 Verify the settings before the wizard runs.  
  
 **Progress and Finish**  
 Monitor the progress of each task.  
  
## See Also  
 [Database &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/database-ssrs-native-mode.md)   
 [Change Database Wizard &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/change-database-wizard-ssrs-native-mode.md)   
 [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md)   
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)   
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)  
  
  
