---
title: "SQL Server Properties (Log On Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 405073fc-eaa3-43c6-bf82-2cd58cacc1c3
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Properties (Log On Tab)
  Use the **Log On** tab of the **SQL Server Properties** dialog box to specify the account used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service, to change the password of an account, and to start and stop the service. Changing the password of an account takes effect immediately.  
  
> [!NOTE]  
>  When changing the account name used by a service on a clustered instance, the new account must be a member of the domain group specified during setup for the service being changed, or you must have permission to add members to that group. If you do not have permission to modify group membership, contact the domain administrator.  
>   
>  For more information about selecting an account to run the service, see "Setting Up Windows Service Accounts" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Options  
 **Built-in account**  
 **Local System**  
 -   Specify the local system account. This account does not require a password. However, the local system account may prevent the service from interacting with other servers, depending on the privileges granted to the account.  
  
 **Local Service**  
 -   Specify the local service account. This account does not require a password. However, the local service account may prevent the service from interacting with other servers, depending on the privileges granted to the account.  
  
 **Network Service**  
 -   We recommend that you do not use the Network Service account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent services. Local User or Domain User accounts are more appropriate for these services.  
  
 **This account**  
 Specify a local or domain user account that uses Windows Authentication. We recommend that you use a domain user account that has minimal rights for services.  
  
 **Account Name**  
 Specify the local or domain user account name.  
  
 **Password**  
 Type the password of the account.  
  
 **Confirm password**  
 Type the password of the account again.  
  
 **Start**  
 Start the service.  
  
 **Stop**  
 Stop the service.  
  
 **Pause**  
 Pause the service.  
  
 **Resume**  
 Resume a paused service.  
  
> [!IMPORTANT]  
>  By default, only members of the local administrators group can start, stop, pause, resume or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services in Windows Server 2003](https://support.microsoft.com/kb/325349). (The process is similar on other versions of Windows.)  
  
> [!NOTE]  
>  When starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a WMI error containing the phrase "not implemented [0x80004001]" may indicate that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not installed on the target computer.  
  
  
