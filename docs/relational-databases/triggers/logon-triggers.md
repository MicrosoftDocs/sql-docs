---
description: "Logon Triggers"
title: "Logon Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "03/19/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: conceptual
f1_keywords: 
  - "logon triggers"
  - "login triggers"
helpviewer_keywords: 
  - "triggers [SQL Server], logon"
ms.assetid: 2f0ebb2f-de10-482d-9806-1a5de5b312b8
author: MikeRayMSFT
ms.author: mikeray
---
# Logon Triggers
[!INCLUDE[tsql-appliesto-ss2008-appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]
  Logon triggers fire stored procedures in response to a LOGON event. This event is raised when a user session is established with an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Logon triggers fire after the authentication phase of logging in finishes, but before the user session is actually established. Therefore, all messages originating inside the trigger that would typically reach the user, such as error messages and messages from the PRINT statement, are diverted to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. Logon triggers do not fire if authentication fails.  
  
 You can use logon triggers to audit and control server sessions, such as by tracking login activity, restricting logins to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or limiting the number of sessions for a specific login. For example, in the following code, the logon trigger denies log in attempts to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] initiated by login *login_test* if there are already three user sessions created by that login.  
  
```  
USE master;  
GO  
CREATE LOGIN login_test WITH PASSWORD = N'3KHJ6dhx(0xVYsdf' MUST_CHANGE,  
    CHECK_EXPIRATION = ON;  
GO  
GRANT VIEW SERVER STATE TO login_test;  
GO  
CREATE TRIGGER connection_limit_trigger  
ON ALL SERVER WITH EXECUTE AS N'login_test'  
FOR LOGON  
AS  
BEGIN  
IF ORIGINAL_LOGIN()= N'login_test' AND  
    (SELECT COUNT(*) FROM sys.dm_exec_sessions  
            WHERE is_user_process = 1 AND  
                original_login_name = N'login_test') > 3  
    ROLLBACK;  
END;  
```  
  
 Note that the LOGON event corresponds to the AUDIT_LOGIN SQL Trace event, which can be used in [Event Notifications](../../relational-databases/service-broker/event-notifications.md). The primary difference between triggers and event notifications is that triggers are raised synchronously with events, whereas event notifications are asynchronous. This means, for example, that if you want to stop a session from being established, you must use a logon trigger. An event notification on an AUDIT_LOGIN event cannot be used for this purpose.  
  
## Specifying First and Last Trigger  
 Multiple triggers can be defined on the LOGON event. Any one of these triggers can be designated the first or last trigger to be fired on an event by using the [sp_settriggerorder](../../relational-databases/system-stored-procedures/sp-settriggerorder-transact-sql.md) system stored procedure. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not guarantee the execution order of the remaining triggers.  
  
## Managing Transactions  
 Before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fires a logon trigger, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates an implicit transaction that is independent from any user transaction. Therefore, when the first logon trigger starts firing, the transaction count is 1. After all the logon triggers finish executing, the transaction commits. As with other types of triggers, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error if a logon trigger finishes execution with a transaction count of 0. The ROLLBACK TRANSACTION statement resets the transaction count to 0, even if the statement is issued inside a nested transaction. COMMIT TRANSACTION might decrement the transaction count to 0. Therefore, we advise against issuing COMMIT TRANSACTION statements inside logon triggers.  
  
 Consider the following when you are using a ROLLBACK TRANSACTION statement inside logon triggers:  
  
-   Any data modifications made up to the point of ROLLBACK TRANSACTION are rolled back. These modifications include those made by the current trigger and those made by previous triggers that executed on the same event. Any remaining triggers for the specific event are not executed.  
  
-   The current trigger continues to execute any remaining statements that appear after the ROLLBACK statement. If any of these statements modify data, the modifications are not rolled back.  
  
 A user session is not established if any one of the following conditions occur during execution of a trigger on a LOGON event:  
  
-   The original implicit transaction is rolled back or fails.  
  
-   An error that has severity greater than 20 is raised inside the trigger body.  
  
## Disabling a Logon Trigger  
 A logon trigger can effectively prevent successful connections to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for all users, including members of the **sysadmin** fixed server role. When a logon trigger is preventing connections, members of the **sysadmin** fixed server role can connect by using the dedicated administrator connection, or by starting the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in minimal configuration mode (-f). For more information, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).  
  
## Related Tasks  
  
|Task|Topic|  
|----------|-----------|  
|Describes how to create logon triggers. Logon triggers can be created from any database, but are registered at the server level and reside in the **master** database.|[CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)|  
|Describes how to modify logon triggers.|[ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)|  
|Describes how to delete logon triggers.|[DROP TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-trigger-transact-sql.md)|  
|Describes how to return information about logon triggers.|[sys.server_triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md)<br /><br /> [sys.server_trigger_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-trigger-events-transact-sql.md)|  
|Describes how to capture logon trigger event data.||  
  
## See Also  
 [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md)  
  
  
