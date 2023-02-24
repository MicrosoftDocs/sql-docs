---
description: "Database Mail"
title: "Database Mail"
ms.custom: ""
ms.date: "04/19/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Database Mail
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Database Mail is an enterprise solution for sending e-mail messages from the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] or [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-index). Your applications can send e-mail messages to users using Database Mail via an external SMTP server. The messages can contain query results, and can also include files from any resource on your network.  
  
> [!NOTE] 
> Database Mail is available in [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-index), but not in Azure SQL database singleton and elastic pools. For more information on using Database Mail in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], see [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance).

##  <a name="Benefits"></a> Benefits of using Database Mail  
 Database Mail is designed for reliability, scalability, security, and supportability.  
  
### Reliability  
  
-   Database Mail uses the standard Simple Mail Transfer Protocol (SMTP) to send mail. You can use Database Mail without installing an Extended MAPI client on the computer that runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Process isolation. To minimize the impact on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the component that delivers e-mail runs outside of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in a separate process. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will continue to queue e-mail messages even if the external process stops or fails. The queued messages will be sent once the outside process or SMTP server comes online.  
  
-   Failover accounts. A Database Mail profile allows you to specify more than one SMTP server. Should an SMTP server be unavailable, mail can still be delivered to another SMTP server.  
  
-   Cluster support. Database Mail is cluster-aware and is fully supported on a cluster.  
  
### Scalability  
  
-   Background Delivery: Database Mail provides background, or asynchronous, delivery. When you call `sp_send_dbmail` to send a message, Database Mail adds a request to a [!INCLUDE[ssSB](../../includes/sssb-md.md)] queue. The stored procedure returns immediately. The external e-mail component receives the request and delivers the e-mail.  
  
-   Multiple profiles: Database Mail allows you to create multiple profiles within a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Optionally, you can choose the profile that Database Mail uses when you send a message.  
  
-   Multiple accounts: Each profile can contain multiple failover accounts. You can configure different profiles with different accounts to distribute e-mail across multiple e-mail servers.  
  
-   64-bit compatibility: Database Mail is fully supported on 64-bit installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Security  
  
-   Off by default: To reduce the surface area of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Database Mail stored procedures are disabled by default.  
  
-   Mail Security:To send Database Mail, you must be a member of the **DatabaseMailUserRole** database role in the `msdb` database.  
  
-   Profile security: Database Mail enforces security for mail profiles. You choose the `msdb` database users or groups that have access to a Database Mail profile. You can grant access to either specific users, or all users in `msdb`. A private profile restricts access to a specified list of users. A public profile is available to all users in a database.  
  
-   Attachment size governor: Database Mail enforces a configurable limit on the attachment file size. You can change this limit by using the [sysmail_configure_sp](../../relational-databases/system-stored-procedures/sysmail-configure-sp-transact-sql.md) stored procedure.  
  
-   Prohibited file extensions: Database Mail maintains a list of prohibited file extensions. Users cannot attach files with an extension that appears in the list. You can change this list by using sysmail_configure_sp.  
  
-   Database Mail runs under the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Engine service account. To attach a file from a folder to an email, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine account should have permissions to access the folder with the file.  
  
### Supportability  
  
-   Integrated configuration: Database Mail maintains the information for e-mail accounts within [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. There is no need to manage a mail profile in an external client application. Database Mail Configuration Wizard provides a convenient interface for configuring Database Mail. You can also create and maintain Database Mail configurations using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
-   Logging. Database Mail logs e-mail activity to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the Microsoft Windows application event log, and to tables in the `msdb` database.  
  
-   Auditing: Database Mail keeps copies of messages and attachments sent in the `msdb` database. You can easily audit Database Mail usage and review the retained messages.  
  
-   Support for HTML: Database Mail allows you to send e-mail formatted as HTML.  
  
  
##  <a name="VisualElement"></a> Database Mail Architecture  

 Database Mail is designed on a queued architecture that uses service broker technologies. When users execute `sp_send_dbmail`, the stored procedure inserts an item into the mail queue and creates a record that contains the e-mail message. Inserting the new entry in the mail queue starts the external Database Mail process (DatabaseMail.exe). The external process reads the e-mail information and sends the e-mail message to the appropriate e-mail server or servers. The external process inserts an item in the Status queue for the outcome of the send operation. Inserting the new entry in the status queue starts an internal stored procedure that updates the status of the e-mail message. Besides storing the sent, or unsent, e-mail message, Database Mail also records any e-mail attachments in the system tables. Database Mail views provide the status of messages for troubleshooting, and stored procedures allow for administration of the Database Mail queue.  
  
 ![msdb sends messages to an SMTP mail server](../../relational-databases/database-mail/media/databasemail.gif "msdb sends messages to an SMTP mail server")  
  
  
## <a name="ComponentsAndConcepts"></a> Introduction to Database Mail components

 Database Mail consists of the following main components:  
  
-   Configuration and security components  
  
     Database Mail stores configuration and security information in the `msdb` database. Configuration and security objects create profiles and accounts used by Database Mail.  
  
-   Messaging components  
  
     The `msdb` database acts as the mail-host database that holds the messaging objects that Database Mail uses to send e-mail. These objects include the `sp_send_dbmail` stored procedure and the data structures that hold information about messages.  
  
-   Database Mail executable  
  
     The Database Mail executable is an external program that reads from a queue in the `msdb` database and sends messages to e-mail servers.  
  
-   Logging and auditing components  
  
     Database Mail records logging information in the `msdb` database and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application event log.  
  
 ### Configuring SQL Agent to use Database Mail
  
 SQL Server Agent can be configured to use Database Mail. This is required for alert notifications and automatic notification when a job completes.  
  
> [!WARNING]  
> Individual job steps within a job can also send e-mail without configuring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to use Database Mail. For example, a [!INCLUDE[tsql](../../includes/tsql-md.md)] job step can use Database Mail to send the results of a query to a list of recipients.  
  
 You can configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to send e-mail messages to predefined operators when:  
  
-   An alert is triggered. Alerts can be configured to send e-mail notification of specific events that occur. For example, alerts can be configured to notify an operator of a particular database event or operating system condition that may need immediate action. For more information about configuring alerts, see [Alerts](../../ssms/agent/alerts.md).  
  
-   A scheduled task, such as a database backup or replication event, succeeds or fails. For example, you can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Mail to notify operators if an error occurs during processing at the end of a month.  
  

## <a name="RelatedContent"></a> See also  
  
- [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)  
- [Database Mail Messaging Objects](../../relational-databases/database-mail/database-mail-messaging-objects.md)  
- [Database Mail External Program](../../relational-databases/database-mail/database-mail-external-program.md)  
- [Database Mail Log and Audits](../../relational-databases/database-mail/database-mail-log-and-audits.md)  

## Next steps

- [Configure Database Mail](configure-database-mail.md)
- [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)
- [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance)
  