---
description: "Designate an Events Forwarding Server"
title: Designate an Events Forwarding Server
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "event forwarding servers [SQL Server]"
  - "events [SQL Server], forwarding"
  - "alerts [SQL Server], forwarded events"
ms.assetid: 81dfcbe4-3000-4e77-99de-bf85fef63a12
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Designate an Events Forwarding Server
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to designate a server to which [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] forwards events in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio . Note that event forwarding applies to events forwarded between servers, not to events forwarded between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] hosted on a single computer. Also note that in order to receive forwarded events, the alerts management server must be a default instance of SQL Server.  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Requires membership in the **sysadmin** fixed server role.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To designate an events forwarding server  
  
1.  In **Object Explorer,** click the plus sign to expand the server from which you want to forward events to another server.  
  
2.  Right-click **SQL Server Agent** and select **Properties**.  
  
3.  In the **SQL Server Agent Properties -**_server_name_ dialog box, under **Select a page**, select **Advanced**.  
  
4.  Under **SQL Server event forwarding**, select the **Forward events to a different server** check box.  
  
5.  In the **Server** list, select a server, and then, under **Events**, select one of the following:  
  
    -   Select **Unhandled events** to forward only the events that have not been handled by local alerts.  
  
    -   Select **All events** to forward all events regardless of whether they have been handled by local alerts.  
  
6.  In the **If event has severity at or above** list, click the severity level at which events are forwarded to the selected server.  
  
7.  When finished, click **OK**.  
