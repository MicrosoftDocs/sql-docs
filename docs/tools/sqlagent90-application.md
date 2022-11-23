---
title: "sqlagent90 Application"
description: The sqlagent90 application starts SQL Server Agent from the command prompt. Use it when diagnosing SQL Server Agent or when directed by your support provider.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "starting SQL Server Agent"
  - "sqlagent90 application"
  - "SQL Server Agent, starting"
  - "command prompt utilities [SQL Server], sqlagent90"
ms.assetid: e8b80e8d-d0c9-4500-a868-0ce08233da08
author: markingmyname
ms.author: maghan
---
# sqlagent90 Application
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]
  The **sqlagent90** application starts [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent from the command prompt. Usually, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent should be run from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or by using SQL-SMO methods in an application. Only run **sqlagent90** from the command prompt when you are diagnosing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent, or when you are directed to it by your primary support provider.  
  
## Syntax  
  
```  
  
sqlagent90  
-c [-v] [-i instance_name]  
```  
  
## Arguments  
 **-c**  
 Indicates that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent is running from the command prompt and is independent of the Microsoft Windows Services Control Manager. When **-c** is used, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent cannot be controlled from either the Services application in Administrative Tools or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager. This argument is mandatory.  
  
 **-v**  
 Indicates that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent runs in verbose mode and writes diagnostic information to the command-prompt window. The diagnostic information is the same as the information written to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent error log.  
  
 **-i** *instance_name*  
 Indicates that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent connects to the named [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance specified by *instance_name*.  
  
## Remarks  
 After displaying a copyright message, **sqlagent90** displays output in the command prompt window only when the **-v** switch is specified. To stop **sqlagent90**, press CTRL+C at the command prompt. Do not close the command-prompt window before stopping **sqlagent90**.  
  
## See Also  
 [Automated Administration Tasks &#40;SQL Server Agent&#41;](../ssms/agent/automated-administration-tasks-sql-server-agent.md)  
  
