---
title: "Log Shipping Monitor Settings | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.databaseproperties.logshipping.settings.monitor.f1"
ms.assetid: 45e2ba7d-b3aa-4643-9451-bcb991572314
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Log Shipping Monitor Settings
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this page to configure and to modify the properties of the log shipping monitor server.  
  
 For an explanation of log shipping concepts, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
## Options  
 **Monitor server instance**  
 Displays the name of the server instance that is currently configured as the monitor server for the log shipping configuration.  
  
 **Connect**  
 Choose and connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be used as the monitor server. The account used to connect must be a member of the sysadmin fixed server role on the secondary server instance.  
  
 **By impersonating the proxy account of the job**  
 Have log shipping impersonate the SQL Server Agent proxy account when connecting to the monitor server instance. The backup, copy, and restore processes must be able to connect to the monitor server to update the status of log shipping operations.  
  
 **Using the following SQL Server login**  
 Allow log shipping to use a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login when connecting to the monitor server instance. The backup, copy, and restore processes must be able to connect to the monitor server to update the status of log shipping operations. Choose this option if you want log shipping to use a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and then specify the login and password.  
  
 **Delete history after**  
 Specify the amount of time to retain log shipping history information on the monitor server before it is deleted.  
  
 **Job name**  
 Indicates the name of the SQL Server Agent alert job used by log shipping to raise alerts when backup or restore thresholds have been exceeded. When first creating this job, you can change the name by typing in the box.  
  
 **Schedule**  
 Indicates the current schedule of the SQL Server Agent alert job.  
  
 **Edit**  
 Modify the SQL Server Agent alert job parameters.  
  
 **Disable this job**  
 Suspend the SQL Server Agent alert job.  
  
  
