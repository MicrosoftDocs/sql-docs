---
description: "MSSQLSERVER_18483"
title: MSSQLSERVER_18483
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "18483 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_18483
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|18483|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|REMLOGIN_INVALID_USER|
|Message Text|Could not connect to server '%.ls' because '%.ls' is not defined as a remote login at the server. Verify that you have specified the correct login name. %.*ls.|

## Explanation

This error occurs when you try to configure a replication distributor on a system that was restored using the hard disk image of another computer where the SQL instance was originally installed. An error message similar to the following is reported to the user:

> SQL Server Management Studio could not configure '\<Server>\<Instance>' as the Distributor for '\<Server>\<Instance>' . Error 18483: Could not connect to server '\<Server>\<Instance>' because 'distributor_admin' is not defined as a remote login at the server. Verify that you have specified the correct login name. %.*ls.

## Cause

When you deploy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a hard disk image of another computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, the network name of the imaged computer is retained in the new installation. The incorrect network name causes the configuration of the replication distributor to fail. The same problem occurs if you rename the computer after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.

## User action

To work around this problem, replace the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] server name with the correct network name of the computer. To do so, follow these steps:

1. Log on to the computer where you deployed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the disk image, and then run the following Transact-SQL statement in SSMS:

    ```sql
    -- Use the Master database
    USE master
    GO

    -- Declare local variables
    DECLARE @serverproperty_servername varchar(100),
    @servername varchar(100);

    -- Get the value returned by the SERVERPROPERTY system function
    SELECT @serverproperty_servername = CONVERT(varchar(100), SERVERPROPERTY('ServerName'));

    -- Get the value returned by @@SERVERNAME global variable
    SELECT @servername = CONVERT(varchar(100), @@SERVERNAME);

    -- Drop the server with incorrect name
    EXEC sp_dropserver @server=@servername;

    -- Add the correct server as a local server
    EXEC sp_addserver @server=@serverproperty_servername, @local='local';
    ```

2. Restart the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
3. To verify that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] name and the network name of the computer are the same, run the following Transact-SQL statement:

    ```sql
    SELECT @@SERVERNAME, SERVERPROPERTY('ServerName');
    ```

## More information

You can use the `@@SERVERNAME` global variable or the `SERVERPROPERTY`('ServerName') function in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to find the network name of the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The ServerName property of the `SERVERPROPERTY` function automatically reports the change in the network name of the computer when you restart the computer and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. The `@@SERVERNAME` global variable retains the original [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer name until the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] name is manually reset.

### Steps to Reproduce the Problem

On the computer where you deployed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a disk image, follow these steps:

1. Start [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)].
2. In the **Object Explorer**, expand your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name.
3. Right-click on the **Replication** folder and click **Configure distribution Replication**, and then click **Configure Publishing, Subscribers, and Distribution**.
4. In the **Configure Distribution** Wizard dialog box, click **Next**.
5. In the **Distributor** dialog box, click to select the '\<**Server**>\<**Instance**>' will act as its own Distributor; SQL Server will create a distribution database and log radio button, and then click **Next**.
6. In the **SQL Server Agent Start** dialog box, click **Next**.
7. In the **Snapshot Folder** dialog box, click **Next**.

    > [!NOTE]
    > If you receive a message to confirm the snapshot folder path, click **Yes**.
8. In the **Distribution Database** dialog box, click **Next**.
9. In the **Publishers** dialog box, click **Next**.
10. In the **Wizard Actions** dialog box, click **Next**.
11. In the **Complete the Wizard** dialog box, click **Finish**.

## See also

- [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)
- [@@SERVERNAME (Transact-SQL)](../../t-sql/functions/servername-transact-sql.md)
- [SERVERPROPERTY (Transact-SQL)](../../t-sql/functions/serverproperty-transact-sql.md)
- [sp_addserver (Transact-SQL)](../system-stored-procedures/sp-addserver-transact-sql.md)
