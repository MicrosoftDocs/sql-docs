---
description: "MSSQLSERVER_15581"
title: MSSQLSERVER_15581
ms.custom: ""
ms.date: 09/03/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha, VenCher
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "15581 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_15581
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|15581|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|SEC_NODBMASTERKEYERR|
|Message Text|Please create a master key in the database or open the master key in the session before performing this operation.|

## Explanation

Error 15581 is raised when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not able to recover a database that is enabled for transparent data encryption (TDE). An error message like the following is logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log

> 2020-01-14 22:16:26.47 spid20s Error: 15581, Severity: 16, State: 3.  
2020-01-14 22:16:26.47 spid20s Please create a master key in the database or open the master key in the session before performing this operation.

## Possible cause

This issue occurs when service master key encryption for the database master key in the master database is removed when the following command is run:

```sql
Use master
go
alter master key drop encryption by service master key
```

The service master key is used to encrypt the certificate that is used by the database master key. Any attempt to use the TDE-enabled database requires access to the database master key in the master database. A master key that is not encrypted by the service master key must be opened by using the [OPEN MASTER KEY (Transact-SQL)](../../t-sql/statements/open-master-key-transact-sql.md) statement together with a password on each session that requires access to the master key. Because this command cannot be run on system sessions, recovery cannot be completed on TDE-enabled databases.

## User action

To resolve the issue, enable automatic decryption of the master key. To do this, run the following commands:

```sql
Use master
go
open master key DECRYPTION BY PASSWORD = 'password'
alter master key add encryption by service master key
```

Use the following query to determine whether automatic decryption of the master key by the service master key was disabled for the master database:

```sql
select is_master_key_encrypted_by_server from sys.databases where name = 'master'
```

If this query returns a value of 0, automatic decryption of the master key by the service master key was disabled.

## More information

In some cases, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may appear unresponsive. If you query `sys.dm_exec_requests` dynamic management view, you notice that the `LogWriter` thread and other threads that are performing DML operations are waiting indefinitely with WRITELOG wait_type. Other sessions may also be waiting while they try to obtain locks.
