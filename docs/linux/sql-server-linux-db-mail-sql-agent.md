---
title: DB Mail and Email Alerts with SQL Agent on Linux
description: Learn how to use DB Mail and how to set up Email Alerts with SQL Server Agent (mssql-server-agent) on Linux.
author: VanMSFT 
ms.author: vanto
ms.date: 02/20/2018
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: tbd
---
# DB Mail and Email Alerts with SQL Agent on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The following steps show you how to set up DB Mail and use it with SQL Server Agent (**mssql-server-agent**) on Linux. 

## 1. Enable DB Mail

```sql
USE master 
GO 
sp_configure 'show advanced options',1 
GO 
RECONFIGURE WITH OVERRIDE 
GO 
sp_configure 'Database Mail XPs', 1 
GO 
RECONFIGURE  
GO  
```

## 2. Create a new account
```sql
EXECUTE msdb.dbo.sysmail_add_account_sp 
@account_name = 'SQLAlerts', 
@description = 'Account for Automated DBA Notifications', 
@email_address = 'sqlagenttest@gmail.com', 
@replyto_address = 'sqlagenttest@gmail.com', 
@display_name = 'SQL Agent', 
@mailserver_name = 'smtp.gmail.com', 
@port = 587, 
@enable_ssl = 1, 
@username = 'sqlagenttest@gmail.com', 
@password = '<password>' 
GO
```

## 3. Create a default profile

```sql
EXECUTE msdb.dbo.sysmail_add_profile_sp 
@profile_name = 'default', 
@description = 'Profile for sending Automated DBA Notifications' 
GO
```

## 4. Add the Database Mail account to a Database Mail profile
```sql
EXECUTE msdb.dbo.sysmail_add_principalprofile_sp 
@profile_name = 'default', 
@principal_name = 'public', 
@is_default = 1 ; 
 ```
 
## 5. Add account to profile 
```sql
EXECUTE msdb.dbo.sysmail_add_profileaccount_sp   
@profile_name = 'default',   
@account_name = 'SQLAlerts',   
@sequence_number = 1;  
 ```
 
## 6. Send test email
> [!NOTE]
> You might have to go to your email client and enable the "allow less secure clients to send mail." Not all clients recognize DB Mail as an email daemon.

```
EXECUTE msdb.dbo.sp_send_dbmail 
@profile_name = 'default', 
@recipients = 'recipient-email@gmail.com', 
@Subject = 'Testing DBMail', 
@Body = 'This message is a test for DBMail' 
GO
```

## 7. Set DB Mail Profile using mssql-conf or environment variable
You can use the mssql-conf utility or environment variables to register your DB Mail profile. In this case, let's call our profile default.

```bash
# via mssql-conf
sudo /opt/mssql/bin/mssql-conf set sqlagent.databasemailprofile default
# via environment variable
MSSQL_AGENT_EMAIL_PROFILE=default
```

## 8. Set up an operator for SQLAgent job notifications 

```sql
EXEC msdb.dbo.sp_add_operator 
@name=N'JobAdmins',  
@enabled=1, 
@email_address=N'recipient-email@gmail.com',  
@category_name=N'[Uncategorized]' 
GO 
```

## 9. Send email when 'Agent Test Job' succeeds 

```
EXEC msdb.dbo.sp_update_job 
@job_name='Agent Test Job', 
@notify_level_email=1, 
@notify_email_operator_name=N'JobAdmins' 
GO
```

## Next steps
For more information on how to use SQL Server Agent to create, schedule, and run jobs, see [Run a SQL Server Agent job on Linux](sql-server-linux-run-sql-server-agent-job.md).
