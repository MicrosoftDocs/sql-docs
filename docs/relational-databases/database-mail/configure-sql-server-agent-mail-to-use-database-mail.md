---
title: "Configure SQL Server Agent Mail to Use Database Mail | Microsoft Docs"
ms.custom: ""
ms.date: "08/05/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Database Mail [SQL Server], SQL Server Agent Mail"
  - "SQL Server Agent Mail"
ms.assetid: 4b8b61bd-4bd1-43cd-b6e5-c6ed2e101dce
caps.latest.revision: 31
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Configure SQL Server Agent Mail to Use Database Mail
  This topic describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to use Database Mail to send notification and alerts in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  For information on how to enable and configure Database Mail, see [Configure Database Mail](../../relational-databases/database-mail/configure-database-mail.md).  For an example using [!INCLUDE[tsql](../../includes/tsql-md.md)], see [Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md).
  
-   **Before you begin:**  
  
-   [Prerequisites](#Prerequisites)  
  
-   [Security](#Security)  
  
-   [To Configure SQL Server Agent to use Database Mail, using SQL Server Management Studio](#SSMSProcedure)  
  
-   [Follow-up Tasks](#Follow_Up)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   [Enable Database Mail](../../relational-databases/database-mail/configure-database-mail.md).  
  
-    [Create a Database Mail account](../../relational-databases/database-mail/create-a-database-mail-account.md) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account to use.  
  
-   [Create a Database Mail profile](../../relational-databases/database-mail/create-a-database-mail-profile.md) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account to use and add the user to the **DatabaseMailUserRole** in the **msdb** database.  
  
-   Set the profile as the default profile for the **msdb** database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The user creating the profiles accounts and executing stored procedures should be a member of the sysadmin fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To configure SQL Server Agent to use Database Mail**  
  
-   In Object Explorer, expand a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   Right-click **SQL Server Agent**, and then click **Properties**.  
  
-   Click **Alert System**.  
  
-   Select **Enable Mail Profile**.  
  
-   In the **Mail system** list, select **Database Mail**.  
  
-   In the **Mail profile list**, select a mail profile for Database Mail.  
  
-   Restart SQL Server Agent.  
  
##  <a name="Follow_Up"></a> Follow-up Tasks  
 The following tasks are necessary to complete the configuration of Agent to send alerts and notifications.  
  
-   [Alerts](http://msdn.microsoft.com/library/3f57d0f0-4781-46ec-82cd-b751dc5affef)  
  
     Alerts can be configured to notify an operator of a particular database event or operating system condition.  
  
-   [Operators](http://msdn.microsoft.com/library/38e8488f-2669-4cea-b9c3-5f394a663678)  
  
     Operators are aliases for people or groups that can receive electronic notification  
  
  
