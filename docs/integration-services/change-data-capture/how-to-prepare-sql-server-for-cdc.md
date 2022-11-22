---
description: "How to Prepare SQL Server for CDC"
title: "How to Prepare SQL Server for CDC | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: a327fa18-58f4-4e69-bb87-44faf47e20ef
author: chugugrace
ms.author: chugu
---
# How to Prepare SQL Server for CDC

  The Oracle CDC service requires all target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances to contain the MSXDBCDC database. You create this database using the Prepare SQL Server action in the CDC Service Configuration Console.This task is done one time only for each target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 The following describes how to prepare a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database for Oracle Change Data Capture using the CDC Service Configuration Console. This process creates the MSXDBCDC database and defines the required tables, stored procedures, and other required artifacts.  
  
 Preparing the SQL Server for Oracle CDC is done by the Oracle CDC Service Administrator. For more information about the CDC Service Administrator role, see [User Roles](../../integration-services/change-data-capture/user-roles.md).  
  
### To enable SQL Server for CDC  
  
1.  From the **Start** menu, select the **CDC Service Configuration for Oracle**.  
  
2.  From the left pane, select **Local CDC Services** then from the **Actions** pane, click **Prepare SQL Server**.  
  
     You can also right-click **Local CDC Services** and select **Prepare SQL Server**.  
  
3.  Enter the required information in the Preparing SQL Server Instance for Oracle CDC dialog box. For information on how to enter the required information into this dialog box, see [Prepare SQL Server for CDC](../../integration-services/change-data-capture/prepare-sql-server-for-cdc.md).  
  
     To Prepare the SQL Server instance for Oracle CDC, the login must have write permission to the MSXDBCDC database. Enter the credentials for a login that has write permission to the MSXDBCDC database, such as a member of the `sysasmin` role.  
  
 **Note**: You can click **View Script** to view a read-only version of the setup script. A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator can copy this script into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Console to edit and execute it, if necessary.  
  
## See Also  
 [Prepare SQL Server for CDC](../../integration-services/change-data-capture/prepare-sql-server-for-cdc.md)  
  
  
