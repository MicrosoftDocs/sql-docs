---
title: Database Mail and email alerts with SQL Server Agent on Linux
description: Learn how to use Database Mail and how to set up Email Alerts with SQL Server Agent (mssql-server-agent) on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/30/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Database Mail and email alerts with SQL Server Agent on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article shows how to set up Database Mail and use it with SQL Server Agent (**mssql-server-agent**) on Linux.

## 1. Enable Database Mail

```sql
USE master;
GO

sp_configure 'show advanced options', 1;
GO

RECONFIGURE WITH OVERRIDE
GO

sp_configure 'Database Mail XPs', 1;
GO

RECONFIGURE;
GO
```

## 2. Create a new account

```sql
EXECUTE msdb.dbo.sysmail_add_account_sp @account_name = 'SQLAlerts',
    @description = 'Account for Automated DBA Notifications',
    @email_address = 'sqlagenttest@example.com',
    @replyto_address = 'sqlagenttest@example.com',
    @display_name = 'SQL Agent',
    @mailserver_name = 'smtp.example.com',
    @port = 587,
    @enable_ssl = 1,
    @username = 'sqlagenttest@example.com',
    @password = '<password>';
GO
```

## 3. Create a default profile

```sql
EXECUTE msdb.dbo.sysmail_add_profile_sp
    @profile_name = 'default',
    @description = 'Profile for sending Automated DBA Notifications';
GO
```

## 4. Add the Database Mail account to a Database Mail profile

```sql
EXECUTE msdb.dbo.sysmail_add_principalprofile_sp
    @profile_name = 'default',
    @principal_name = 'public',
    @is_default = 1;
GO
```

## 5. Add account to profile

```sql
EXECUTE msdb.dbo.sysmail_add_profileaccount_sp
    @profile_name = 'default',
    @account_name = 'SQLAlerts',
    @sequence_number = 1;
GO
```

## 6. Send test email

You might have to go to your email client and enable the **allow less secure clients to send mail** option. Not all clients recognize Database Mail as an email daemon.

```sql
EXECUTE msdb.dbo.sp_send_dbmail
    @profile_name = 'default',
    @recipients = 'recipient-email@example.com',
    @subject = 'Testing DBMail',
    @body = 'This message is a test for DBMail'
GO
```

## 7. Set Database Mail profile using mssql-conf or environment variable

You can use the **mssql-conf** utility, or environment variables, to register your Database Mail profile. In this case, let's call our profile `default`.

- Set via **mssql-conf**:

  ```bash
  sudo /opt/mssql/bin/mssql-conf set sqlagent.databasemailprofile default
  ```

- Set via environment variable:

  ```bash
  MSSQL_AGENT_EMAIL_PROFILE=default
  ```

## 8. Set up an operator for SQL Server Agent job notifications

```sql
EXEC msdb.dbo.sp_add_operator
    @name = N'JobAdmins',
    @enabled = 1,
    @email_address = N'recipient-email@example.com',
    @category_name = N'[Uncategorized]';
GO
```

## 9. Send email when 'Agent Test Job' succeeds

```sql
EXEC msdb.dbo.sp_update_job
    @job_name = 'Agent Test Job',
    @notify_level_email = 1,
    @notify_email_operator_name = N'JobAdmins'
GO
```

## Related content

- [Create and run SQL Server Agent jobs on Linux](sql-server-linux-run-sql-server-agent-job.md)
