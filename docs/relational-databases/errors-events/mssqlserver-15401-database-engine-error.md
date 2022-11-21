---
description: "MSSQLSERVER_15401"
title: MSSQLSERVER_15401
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "15401 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_15401
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|15401|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|SEC_INVALIDLOGINORGROUP|
|Message Text|Windows NT user or group '%s' not found. Check the name again.|

## Explanation

This error occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unable to create a login based on Windows principal (such as a domain user or a Windows domain group). An error message like the following is reported to the user

> Error 15401: Windows NT user or group '%s' not found. Check the name again.

## Cause

The error can occur because of any of the following reasons:

- The login does not exist in the active directory.
- The domain controller is unavailable.
- You are not using BUILTIN as the domain name when adding a local account.
- Name resolution issues.

## User action

Review the following sections for actions you can take for each of the different causes mentioned above.

### Verify the login you are trying to add

1. Verify that the Windows login still exists in the domain. Your network administrator may have removed the Windows login for specific reasons, and you may not be able to grant that login access to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
1. Verify that you are spelling the domain and login name correctly and that you are using the following format: `Domain\User`
1. If the login exists, and it is correct, and you still receive the error, continue with the following sections in this article.

### Verify if the domain controller is available

You might receive error 15401 when the domain controller for the domain where the login resides (the same or a different domain) is not available for some reason.

If the login is in a different domain than the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], verify that the correct trusts exist between the domains.

Verify that the domain controller of the login is accessible by using the ping command from the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Check both the IP address and the name of the domain controller.

### Verify the domain name for local accounts

Local (non-domain) accounts require special handling. If you are trying to add a local account from the local computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ensure you are using BUILTIN as the domain name.

### Check for name resolution issues

If you have problems resolving the name of a computer that is involved in adding the login or group, you might receive error 15401.

Verify that your name resolution mechanism (such as, WINS, DNS, HOSTS, or LMHOSTS) is configured correctly.

## See also

- [Test a channel between the local computer and its domain](/powershell/module/microsoft.powershell.management/test-computersecurechannel#example-1--test-a-channel-between-the-local-computer-and-its-domain)
- [LogonSessions v1.4](/sysinternals/downloads/logonsessions)
- [sp_change_users_login (Transact-SQL)](../system-stored-procedures/sp-change-users-login-transact-sql.md)
