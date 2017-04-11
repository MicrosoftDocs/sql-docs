---
title: "Security Considerations for the R Runtime in SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/01/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d5065197-69e6-4fce-9654-00acaecc148b
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Security Considerations for the R Runtime in SQL Server
  This topic provides an overview of security considerations for working with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 For more information about managing the service, and about how to provision the user accounts used to execute R scripts, see [Configure and Manage Advanced Analytics Extensions](../../advanced-analytics/r-services/configure-and-manage-advanced-analytics-extensions.md).  
  
## Use Firewall to Restrict Network Access by R  
 In the suggested installation method, a Windows Firewall rule is used to block all outbound network access from the R runtime processes. Firewall rules should be created to prevent the R runtime process from downloading packages or from making other network calls that could potentially be malicious.  
  
 We strongly recommend that you turn on Windows Firewall (or another firewall of your choice) to block network access by the R runtime.  
  
 If you are using a different firewall program, you can also create rules to block outbound network connection for the R runtime, by setting rules for the local user accounts or for the group represented by the user account pool.   
For more information, see [Configure and Manage Advanced Analytics Extensions](../../advanced-analytics/r-services/configure-and-manage-advanced-analytics-extensions.md).  
  
## Authentication Methods Supported for Remote Compute Contexts 
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] now supports both Windows Integrated Authentication and SQL logins when creating connections between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a remote data science client. 
  
 For example, if you are developing an R solution on your laptop and want to perform computations on the SQL Server computer, you would create a SQL Server data source in R, by using the **rx** functions and defining a connection string based on your Windows credentials. When you change the _compute context_ from your laptop to the SQL Server computer, if your Windows account has the necessary permissions, all R code will be executed on the SQL Server computer. Moreover, any SQL queries executed as part of the R code will be run under your credentials as well. 
 
 Although a SQL login can also be used in the connection string for a SQL Server data source, use of a login requires that the SQL Server instance allow mixed mode authentication.
 
 ### Implied authentication
  
 In general the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] starts the R runtime and executes R scripts under its own account. However, if the R script makes an ODBC call, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] will impersonate the credentials of the user that sent the command to ensure that the ODBC call does not fail. This is called *implied authentication*. 
 
 > [!IMPORTANT] 
 >
 > For implied authentication to succeed, the Windows users group that contains the worker accounts (by default, **SQLRUser**) must have an account in the master database for the instance, and this account must be given permissions to connect to the instance.  
  
## No Support for Encryption at Rest  
 Transparent Data Encryption is not supported for data sent to or received from the R runtime. As a consequence, encryption at rest **will not** be applied to any data that you use in R scripts, any data saved to disk, or any persisted intermediate results.  
 
 ## See Also
 [enter link description here](../../advanced-analytics/r-services/configuration-sql-server-r-services.md) 
  
