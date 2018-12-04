---
title: "Configure SQL Server Agent Error Logs (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql12.ag.errorlog.configure.f1"
ms.assetid: e08de622-6f87-470c-aee0-b2d6cb6cca88
author: stevestein
ms.author: sstein
manager: craigg
---
# Configure SQL Server Agent Error Logs (General Page)
  Use this screen to view and update settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent error logging.  
  
## Options  
 **Error log file**  
 Specifies the file to which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent writes error logs.  
  
 **...**  
 Browse to the error log file.  
  
 **Write OEM error log**  
 Writes the error log file as a non-Unicode file. This reduces the amount of disk space consumed by the log file. However, messages that include Unicode data may be more difficult to read when this option is enabled.  
  
 **Errors**  
 Writes only errors and informational messages to the log file.  
  
 **Warnings**  
 Writes only warnings and informational messages to the log file.  
  
 **Information**  
 Writes only informational messages to the log file.  
  
## See Also  
 [SQL Server Agent Error Log](sql-server-agent-error-log.md)  
  
  
