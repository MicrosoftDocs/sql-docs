---
title: "Database Mail: Send mail from remote server when troubleshooting local server | Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Database Mail: Send mail from remote server when troubleshooting local server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This topic describes how you can use a remote server to temporarily send e-mail that is generated on a local server. This procedure is helpful when you are troubleshooting and repairing the configuration of Database Mail on the local server. This topic only applies to e-mail sent by Database Mail. E-mail sent by using **xp_sendmail** or third-party utilities is unaffected.

   > [!NOTE]
   > In this topic, local server refers to the server that is being troubleshot, and remote server refers to another server that is sending that local server's e-mail when it is being troubleshot.


E-mail can be sent from only one server at a time. When Database Mail on the remote server is configured to send e-mail from the local server, the remote server can no longer send its own e-mail.

When the remote server is sending e-mail that is generated on the local server, all insertions into msdb tables for that e-mail and any Database Mail Log error entries for that e-mail occur on the local server. If the local server resumes sending e-mail when the remote server is configured to send the local server's e-mail, either server can send any e-mail message. If you want to know which server sent an e-mail message, you must examine the header of the message.

  > [!NOTE]
  > To run the following procedures, both the local and remote servers must be running SQL Server 2005 Service Pack 2 or later version.

## Permissions
EXECUTE permission for **dbo.sysmail_start_sp** and **sysmail_configure_sp** and INSERT permission for **dbo.sysmail_configuration** are granted to members of the sysadmin fixed server role by default.

NTFS permission is required to create the configuration file in \MSSQL\Binn.

We recommend that the login account that is executing these troubleshooting procedures be a member of the sysadmin fixed server role on both the local and remote servers.

## Configure remote server to send the local server's e-mail messages

1. Make sure that Database Mail is set up on the remote server.

1. Create a profile on the remote server that is identical to each profile on the local server that will be used by the e-mail that is generated during the troubleshooting. Each profile must have one valid account, but the account does not have to be identical to the account associated with the same profile on the local server.

1. Make sure that any Windows authenticated login account that is a member of the **DatabaseMailUserRole** on the local server is also a member of the same role on the remote server, if that membership is required to send e-mail generated on the local server.

1. Create a text file that is named DatabaseMail90.exe.config with the following content. Replace LocalServerName with the name of the local server and keep msdb for DatabaseName.

    ```
    <configuration>
        <appSettings>
            <add key="DatabaseServerName" value ="LocalServerName" />
            <add key="DatabaseName" value ="msdb" />
        </appSettings>
    </configuration>
    ```
1. Save the file on the remote server in the same folder, \MSSQL\Binn, as DatabaseMail90.exe. The default path is `<drive>:\Program Files\Microsoft SQL Server\MSSQL???.MSSQLSERVER\MSSQL\Binn` where the question marks correspond to the version of SQL Server.
1. To configure Database Mail to send e-mail from another server, run the following code on the remote server.

  > [!IMPORTANT]
  > This code inserts a record into a system table. Do not modify the code. A constraint will prevent the record from being inserted more than one time. Do not otherwise directly modify the data in this system table.

    ```sql
    USE msdb;
    GO
    INSERT INTO [msdb].[dbo].[sysmail_configuration]
        (
        [paramname]
        ,[paramvalue]
        ,[description]
        )
    VALUES
        (
        N'ReadFromConfigurationFile'
        ,N'1'
        ,N'Send mail from mail server in configuration file'
        );
    GO
    ```

7. Restart Database Mail by executing dbo.sysmail_start_sp on the remote server. You must run this stored procedure every time that the **paramvalue** for the **ReadFromConfigurationFile** record in **dbo.sysmail_configuration** or the value of the DatabaseServerName key in the DatabaseMail90.exe.config configuration file is changed for the change to take effect.

    ```sql
    USE msdb;
    GO
    EXEC dbo.sysmail_start_sp;
    GO
    ```

## Configure remote server to stop sending the local server's e-mail messages

1. To resume sending e-mail from the local server, run the following code on the remote server. This code configures the remote server to stop sending e-mail that is generated on the local server and return to sending e-mail that is generated on the remote server.

    ```sql
    USE msdb;
    GO
    EXEC sysmail_configure_sp 
        @parameter_name = N'ReadFromConfigurationFile'
        ,@parameter_value = N'0';
    GO
    ```

1. Restart Database Mail by executing dbo.sysmail_start_sp on the remote server. You must run this stored procedure every time that the **paramvalue** for the **ReadFromConfigurationFile** record in **dbo.sysmail_configuration** or the value of the DatabaseServerName key in the DatabaseMail90.exe.config configuration file is changed for the change to take effect.

    ```sql
    USE msdb;
    GO
    EXEC dbo.sysmail_start_sp;
    GO
    ```

## Switch configuration of database mail on the remote server

To switch the remote server to send e-mail messages that are generated on another server, in msdb.dbo.sysmail_configuration, set the parameter_value for the ReadFromConfigurationFile record to 1, and then execute **msdb.dbo.sysmail_start_sp**. To switch the remote server to send e-mail that is generated on the remote server, set the parameter_value to 0, and then execute **msdb.dbo.sysmail_start_sp**.

    ```sql
    USE msdb;
    GO
    EXEC sysmail_configure_sp 
        @parameter_name = N'ReadFromConfigurationFile'
        ,@parameter_value = N'0|1';
    GO
    ```

To configure the remote server to send e-mail that is generated on a different local server, change the value of the DatabaseServerName key in the DatabaseMail.exe.config configuration file to the name of that local server, and then execute **msdb.dbo.sysmail_start_sp**.

    

##  <a name="RelatedContent"></a> Related content 
  
-  [Database mail overview](database-mail.md)

  
  
