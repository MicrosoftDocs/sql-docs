---
title: "MSSQLSERVER_18452"
description: "MSSQLSERVER_18452: The user attempted to sign in with credentials that can't be validated."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 11/08/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "18456 (Database Engine error)"
  - "18452 (Database Engine error)"
---
# MSSQLSERVER_18452

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 18452 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | LOGON_INVALID_CONNECT |
| Message Text | Login failed. The login is from an untrusted domain and cannot be used with Integrated authentication.%.\*ls |

## Explanation

The user attempted to sign in with credentials that can't be validated. Possible causes are:

- The login might be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login but the server only accepts Windows Authentication.

- You're trying to connect using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication but the login used doesn't exist on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- The login might use Windows Authentication but the login is an unrecognized Windows principal. An unrecognized Windows principal means that the login can't be verified by Windows. This could be because the Windows login is from an untrusted domain.

- This issue might be related to consistent authentication where the trust level between domains might cause failures in account authentication or the visibility of service principal names (SPNs).

Similar problems can cause the less-specific error 18456.

## User action

If you're trying to connect using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, verify that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is configured in Mixed Authentication mode.

If you're trying to connect using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, verify that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login exists.

If you're trying to connect using Windows Authentication, verify that you're properly logged into the correct domain.

You can run the `setspn` and `runas` commands to test the trust relationship independent of your application.
