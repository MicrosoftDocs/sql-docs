---
title: Active Directory authentication for SQL Server on Linux
titleSuffix: SQL Server
description: This article provides an overview of Active Directory Authentication for SQL Server on Linux.
author: rothja
ms.date: 04/01/2019
ms.author: jroth 
manager: craigg
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux, seodec18"
ms.technology: linux
helpviewer_keywords: 
  - "Linux, AAD authentication"
---
# Active Directory authentication for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article provides an overview of Active Directory (AD) authentication for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. AD authentication is also known as Integrated authentication in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. 

## AD authentication overview

AD authentication enables domain-joined clients on either Windows or Linux to authenticate to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using their domain credentials and the Kerberos protocol.

AD Authentication has the following advantages over [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication:

- Users authenticate via single sign-on, without being prompted for a password.   
- By creating logins for AD groups, you can manage access and permissions in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using AD group memberships.  
- Each user has a single identity across your organization, so you don't have to keep track of which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] logins correspond to which people.   
- AD enables you to enforce a centralized password policy across your organization.   

## Configuration steps

In order to use Active Directory authentication, you must have an AD Domain Controller (Windows) on your network.

The details for how to configure AD authentication are provided in the tutorial, [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md). The following list provides a summary with a link to each section in the tutorial:

1. [Join a SQL Server host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).
1. [Create an AD user for SQL Server and set the ServicePrincipalName](sql-server-linux-active-directory-authentication.md#createuser).
1. [Configure the SQL Server service keytab](sql-server-linux-active-directory-authentication.md#configurekeytab).
1. [Secure the keytab file](sql-server-linux-active-directory-authentication.md#securekeytab).
1. [Configure SQL Server to use the keytab file for Kerberos authentication](sql-server-linux-active-directory-authentication.md#keytabkerberos).
1. [Create AD-based SQL Server logins in Transact-SQL](sql-server-linux-active-directory-authentication.md#createsqllogins).
1. [Connect to SQL Server using AD authentication](sql-server-linux-active-directory-authentication.md#connect).

## Known issues

- At this time, the only authentication method supported for database mirroring endpoint is CERTIFICATE. WINDOWS authentication method will be enabled in a future release.

## Next Steps

For more information on how to implement Active Directory authentication for SQL Server on Linux, see [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).
