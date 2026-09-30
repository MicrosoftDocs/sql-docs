---
title: Active Directory Authentication for SQL Server on Linux
titleSuffix: SQL Server
description: This article provides an overview of Active Directory Authentication for SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 07/03/2025
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
helpviewer_keywords:
  - "Linux, AAD authentication"
---
# Active Directory authentication for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This article provides an overview of Active Directory authentication for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux. Active Directory authentication is also known as integrated authentication in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

## Active Directory authentication overview

Active Directory authentication enables domain-joined clients on either Windows or Linux to authenticate to [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] using their domain credentials and the Kerberos protocol.

Active Directory authentication has the following advantages over [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] authentication:

- Users authenticate via single sign-on, without being prompted for a password.
- By creating logins for Active Directory groups, you can manage access and permissions in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] using Active Directory group memberships.
- Each user has a single identity across your organization, so you don't have to keep track of which [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] logins correspond to which people.
- Active Directory enables you to enforce a centralized password policy across your organization.

## Configuration steps

To use Active Directory authentication, you must have a computer running Windows Server as an Active Directory domain controller on your network.

The details for how to configure Active Directory authentication are provided in the tutorial, [Tutorial: Use Active Directory authentication with SQL Server on Linux](active-directory-tutorial.md). The following list summarizes the tutorial and links to the relevant sections:

1. [Join SQL Server on a Linux host to an Active Directory domain](active-directory-join-domain.md).
1. [Create an Active Directory user for SQL Server and set the Service Principal Name](active-directory-tutorial.md#createuser).
1. [Configure the SQL Server service keytab](active-directory-tutorial.md#configurekeytab).
1. [Secure the keytab file](active-directory-tutorial.md#configurekeytab).
1. [Configure SQL Server to use the keytab file for Kerberos authentication](active-directory-tutorial.md#configurekeytab).
1. [Create Active Directory-based SQL Server logins in Transact-SQL](active-directory-tutorial.md#createsqllogins).
1. [Connect to SQL Server using Active Directory authentication](active-directory-tutorial.md#connect).

## Known issues

- At this time, the only authentication method supported for a database mirroring endpoint is `CERTIFICATE`. The `WINDOWS` authentication method will be enabled in a future release.

- SQL Server on Linux doesn't support the NTLM protocol for remote connections. A local connection might work using NTLM.

## Related content

- [Tutorial: Use Active Directory authentication with SQL Server on Linux](active-directory-tutorial.md)
- [Understand Active Directory authentication for SQL Server on Linux and containers](understand-active-directory.md)
- [Troubleshoot Active Directory authentication for SQL Server on Linux and containers](troubleshoot-active-directory.md)
