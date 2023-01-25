---
description: "MSSQLSERVER_18482"
title: MSSQLSERVER_18482
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "18482 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_18482
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|18482|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|REMLOGIN_INVALID_SITE|
|Message Text|Could not connect to server '%.ls' because '%.ls' is not defined as a remote server. Verify that you have specified the correct server name. %.*ls|

## Explanation

This error occurs when you try to execute a remote procedure call (RPC) from one server to another (for example, by executing a stored procedure on a remote computer with a statement such as `EXEC SERV_REMOTE.pubs..byroyalty`). An error message similar to the following is reported to the user

> Error 18482: Could not connect to server \<SERV_REMOTE> because \<SERV_REMOTE> is not defined as a remote server. Verify that you have specified the correct server name.

## Cause

This error occurs when SQL Server cannot execute a remote procedure call. This can be caused by an improperly configured local server. To make a remote procedure call, SQL Server first determines who the local server is by looking for the server name with srvid = 0 in sysservers. If an entry with srvid = 0 is not found in sysservers, or if the server name with srvid = 0 belongs to a server name that is different from the local Windows computer name, you will receive the error.

## User action

To determine if the local server is configured correctly, examine the `srvstatus` column in master..sysservers. This value should be 0 for the local server.

For example, suppose your local server was named SERV_LOCAL, the remote server was named *SERV_REMOTE*, and sysservers contained the following information:

|srvid|srvstatus|srvname|srvname|
|---|---|---|---|
|1|2|SERV_LOCAL|SERV_LOCAL|
|2|1|SERV_REMOTE|SERV_REMOTE|

In the preceding output, SERV_LOCAL is the local server, but it has a srvid of 1; it should be 0. To correct this, follow these steps:

1. Run `sp_dropserver` local_server_name, droplogins (in this example, you would run `sp_dropserver SERV_LOCAL, droplogins`).
1. Run `sp_addserver` local_server_name, LOCAL (in this example, you would run `sp_addserver SERV_LOCAL, LOCAL`).
1. Stop and restart SQL Server.

After running those steps, the sysservers table should look like the following:

|srvid|srvstatus|srvname|srvname|
|---|---|---|---|
|0|0|SERV_LOCAL|SERV_LOCAL|
|2|1|SERV_REMOTE|SERV_REMOTE|

> [!NOTE]
> Server ID (srvid) should be 0 for the local server.

There may be some cases wherein the entries in sysservers table look correct, but when you run `select @@servername`, it returns NULL. In these scenarios, you should still run through Steps 1 through 3 listed above to fix the issue.

## More information

You may receive this error message when installing replication because the installation process makes remote procedure calls between the servers involved in replication.
