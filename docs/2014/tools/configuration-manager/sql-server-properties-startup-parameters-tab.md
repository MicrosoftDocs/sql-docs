---
title: "SQL Server Properties (Startup Parameters Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 16942624-5374-446c-8de4-ee6ed34d6e94
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Properties (Startup Parameters Tab)
  Use this dialog box to add or remove startup parameters for the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Startup parameters can have a large effect on the [!INCLUDE[ssDE](../../includes/ssde-md.md)] performance. Before adding or changing startup parameters, see the topic "Using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service Startup Options" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Options  
 **Specify a startup parameter**  
 To add a parameter, type the parameter, and then click **Add**.  
  
 To modify one of the required parameters, select the parameter in the **Existing parameters** box, change the values in the **Specify a startup parameter** box, and then click **Update**.  
  
 **Existing parameters**  
 To remove a parameter, select a parameter, and then click **Remove**.  
  
## Parameter Format  
 Do not enter a separator between parameters. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager automatically adds the separator. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager enforces the following parameter requirements.  
  
-   Leading and trailing spaces are trimmed from any startup parameter.  
  
-   All startup parameters start with a - (dash) and the second value is a letter.  
  
## Required Parameters  
 The following parameters are required. They can be changed but not removed.  
  
-   -d is the path of the **master.mdf** file (the master database data file).  
  
-   -l is the path of the **master.ldf** file (the master database log file).  
  
-   -e is the path of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log files.  
  
> [!CAUTION]  
>  If the file path parameters are incorrect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might not start.  
  
 For more information about how to move the master database, see the topic "Moving System Databases" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Optional Parameters  
 All of the supported startup parameters are described in the topic "Using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service Startup Options," in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online. A startup parameter of -T*trace#* indicates that an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be started with a specified trace flag (*trace#*) in effect. Trace flags are used to start the server with nonstandard behavior. For more information about trace flags, see the topic "Trace Flags ([!INCLUDE[tsql](../../includes/tsql-md.md)])" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
> [!CAUTION]  
>  You might see additional undocumented startup parameters and trace flags described on the Internet. Undocumented startup parameters and trace flags are created to address uncommon problems or to force certain conditions required for testing. Using undocumented startup parameters can have unexpected results. Do not use undocumented parameters unless directed by Microsoft Customer Support Services.  
  
 The following list describes some common optional parameters.  
  
|Parameter|Short description|  
|---------------|-----------------------|  
|-m|Starts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode.|  
|-T1204|Returns the resources and types of locks participating in a deadlock and also the current command affected.|  
|-T1224|Disables lock escalation based on the number of locks.|  
|-T3608|Prevents [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from automatically starting and recovering any database except the master database.|  
|-T7806|Enables a dedicated administrator connection (DAC) on [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].|  
  
> [!CAUTION]  
>  Some optional parameters can change server behavior and may affect performance.  
  
## Permissions  
 Use of this page is restricted to users who can change the related entries in the registry. This includes the following users.  
  
-   Members of the local administrators group.  
  
-   The domain account that is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is configured to run under a domain account.  
  
## Books Online References  
 For additional information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup parameters, see "How to: Configure Server Startup Options ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager)" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
  
