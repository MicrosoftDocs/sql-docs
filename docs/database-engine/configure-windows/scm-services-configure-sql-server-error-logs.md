---
title: "Configure SQL Server Error Logs | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.configurelogs.configureerrorlogs.f1"
ms.assetid: 03f0d463-9b0b-4af9-a853-da936d75e5af
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SCM Services - Configure SQL Server Error Logs
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to view or modify the way [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error logs are recycled.  
  
## To open the Configure SQL Server Error Logs dialog box  
  
1.  In Object Explorer, expand the instance of SQL Server, expand **Management**, right-click **SQL Server Logs**, and then click **Configure**.  
  
2.  In the **Configure SQL Server Error Logs** dialog box, choose from the following options.  
  
     **Limit the number of the error log files before they are recycled**  
     Check to limit the number of error logs created before they are recycled. A new error log is created each time an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains backups of the previous six logs, unless you check this option, and specify a different maximum number of error log files below.  
  
     **Maximum number of error log files**  
     Specify the maximum number of error log files created before they are recycled. The default is 6, which is the number of previous backup logs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains before recycling them.  
  
  
