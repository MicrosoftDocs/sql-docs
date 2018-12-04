---
title: "Create a User-Defined Event | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent alerts, user-defined events"
  - "user-defined events [SQL Server]"
  - "multiple language support [SQL Server], alerts"
  - "languages [SQL Server], alerts"
  - "severity levels [SQL Server]"
  - "global considerations [SQL Server], alerts"
  - "events [SQL Server], user-defined"
  - "SQL Server Agent alerts, multiple-language environments"
  - "alerts [SQL Server], user-defined events"
  - "alerts [SQL Server], multiple-language environments"
  - "custom events [SQL Server Agent]"
  - "international considerations [SQL Server], alerts"
ms.assetid: 03d71a35-97fa-4bba-aa9a-23ac9c9cf879
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Create a User-Defined Event
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

You can create user-defined events if you want to monitor events other than events that are predefined by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can also assign a severity level to each user-defined event.  
  
> [!NOTE]  
> When using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the **Write to Windows application event log** option for each user-defined event message, to ensure that the messages are logged. By default, user-defined messages of severity lower than 19 are not sent to the [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows application log when they occur. User-defined messages of severity lower than 19 therefore do not trigger SQL Server Agent alerts.  
  
User-defined events must have a unique message number. Message numbers for a user-defined event must be greater than 50,000. You can define messages for the event in multiple languages. However, an **En-US** error message must exist before messages in other languages can be added.  
  
If you administer a multiple-language [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment, create user-defined messages in each of the languages that are supported. For example, if you are creating a new event message to be used on both an English and a German server, use the same message number and severity for both, but assign a different language to each.  
  
The following tasks provide information about how to create user-defined events and alerts that respond to them:  
  
**To create an alert based on a message number**  
  
-   [SQL Server Management Studio](../../ssms/agent/create-an-alert-using-an-error-number.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/d9b41853-e22d-4813-a79f-57efb4511f09)  
  
**To create an alert based on severity levels**  
  
-   [SQL Server Management Studio](../../ssms/agent/create-an-alert-using-severity-level.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/d9b41853-e22d-4813-a79f-57efb4511f09)  
  
**To define the response to an alert**  
  
-   [SQL Server Management Studio](../../ssms/agent/define-the-response-to-an-alert-sql-server-management-studio.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/0525e0a2-ed0b-4e69-8a4c-a9e3e3622fbd)  
  
**To create a user-defined event error message**  
  
-   [Transact-SQL](https://msdn.microsoft.com/54746d30-f944-40e5-a707-f2d9be0fb9eb)  
  
**To modify a user-defined event error message**  
  
-   [Transact-SQL](https://msdn.microsoft.com/1b28f280-8ef9-48e9-bd99-ec14d79abaca)  
  
**To delete a user-defined event error message**  
  
-   [Transact-SQL](https://msdn.microsoft.com/17287a15-cdde-43d1-bb18-9f920bc15db8)  
  
**To disable or reactivate an alert**  
  
-   [SQL Server Management Studio](../../ssms/agent/disable-or-reactivate-an-alert.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/4bbaeaab-8aca-4c9e-abc1-82ce73090bd3)  
  
## See Also  
[sp_update_alert (Transact-SQL)](https://msdn.microsoft.com/4bbaeaab-8aca-4c9e-abc1-82ce73090bd3)  
  
