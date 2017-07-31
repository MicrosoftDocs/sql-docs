---
title: "Security considerations for machine learning in SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/31/2017"
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
# Security considerations for machine learning in SQL Server

This article lists security considerations that the administrator or architect should bear in mind when using machine learning services.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Use a firewall to restrict network access by R

In a default installation, a Windows firewall rule is used to block all outbound network access from the R runtime processes. Firewall rules should be created to prevent the R runtime process from downloading packages or from making other network calls that could potentially be malicious.

If you are using a different firewall program, you can also create rules to block outbound network connection for the R runtime, by setting rules for the local user accounts or for the group represented by the user account pool.

We strongly recommend that you turn on Windows Firewall (or another firewall of your choice) to prevent unfettered network access by the R or Python runtimes.

## Authentication methods supported for remote compute contexts

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] supports both Windows Integrated Authentication and SQL logins when creating connections between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a remote data science client.

For example, if you are developing an R solution on your laptop and want to perform computations on the SQL Server computer, you would create a SQL Server data source in R, by using the **rx** functions and defining a connection string based on your Windows credentials.

When you change the _compute context_ from your laptop to the SQL Server computer, if your Windows account has the necessary permissions, all R code will be executed on the SQL Server computer. Moreover, any SQL queries executed as part of the R code will be run under your credentials as well.

The use of SQL logins is also supported in this scenario, which requires that the SQL Server instance be configured to allow mixed mode authentication.

### Implied authentication

 In general the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] starts the external script runtime and executes scripts under its own account. However, if the external runtime makes an ODBC call, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] will impersonate the credentials of the user that sent the command to ensure that the ODBC call does not fail. This is called *implied authentication*.
 
 > [!IMPORTANT]
 >
 > For implied authentication to succeed, the Windows users group that contains the worker accounts (by default, **SQLRUser**) must have an account in the master database for the instance, and this account must be given permissions to connect to the instance.
 > 
 > The group **SQLRUser** is also used when running Python scripts. 

## No support for encryption at rest

Transparent Data Encryption is not supported for data sent to or received from the external script runtime. As a consequence, encryption at rest **will not** be applied to any data that you use in R or Python scripts, any data saved to disk, or any persisted intermediate results.

## Resources

For more information about managing the service, and about how to provision the user accounts used to execute R scripts, see [Configure and manage Advanced Analytics Extensions](../../advanced-analytics/r/configure-and-manage-advanced-analytics-extensions.md).

For a description of the security architecture, see:

+ [Security overview for R](security-overview-sql-server-r.md)
+ [Security overview for Python](../python/security-overview-sql-server-python-services.md)