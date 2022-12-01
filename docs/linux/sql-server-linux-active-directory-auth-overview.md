---
title: Active Directory authentication for SQL Server on Linux
titleSuffix: SQL Server
description: This article provides an overview of Active Directory Authentication for SQL Server on Linux.
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 09/27/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
helpviewer_keywords:
  - "Linux, AAD authentication"
---
# Active Directory authentication for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides an overview of Active Directory authentication for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. Active Directory authentication is also known as Integrated authentication in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

## Active Directory authentication overview

Active Directory authentication enables domain-joined clients on either Windows or Linux to authenticate to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using their domain credentials and the Kerberos protocol.

Active Directory Authentication has the following advantages over [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication:

- Users authenticate via single sign-on, without being prompted for a password.
- By creating logins for Active Directory groups, you can manage access and permissions in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using Active Directory group memberships.  
- Each user has a single identity across your organization, so you don't have to keep track of which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] logins correspond to which people.   
- Active Directory enables you to enforce a centralized password policy across your organization.

## Configuration steps

In order to use Active Directory authentication, you must have an Active Directory Domain Controller (Windows) on your network.

The details for how to configure Active Directory authentication are provided in the tutorial, [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md). The following list provides a summary with a link to each section in the tutorial:

1. [Join a SQL Server host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).
1. [Create an Active Directory user for SQL Server and set the Service Principal Name](sql-server-linux-active-directory-authentication.md#createuser).
1. [Configure the SQL Server service keytab](sql-server-linux-active-directory-authentication.md#configurekeytab).
1. [Secure the keytab file](sql-server-linux-active-directory-authentication.md#configurekeytab).
1. [Configure SQL Server to use the keytab file for Kerberos authentication](sql-server-linux-active-directory-authentication.md#configurekeytab).
1. [Create Active Directory-based SQL Server logins in Transact-SQL](sql-server-linux-active-directory-authentication.md#createsqllogins).
1. [Connect to SQL Server using Active Directory authentication](sql-server-linux-active-directory-authentication.md#connect).

## Known issues

- At this time, the only authentication method supported for database mirroring endpoint is `CERTIFICATE`. `WINDOWS` authentication method will be enabled in a future release.
- SQL Server on Linux does not support NTLM protocol for remote connections. Local connection may work using NTLM.

## Next steps

- [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md)
- [Understanding Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-understanding.md)
- [Troubleshooting Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)
