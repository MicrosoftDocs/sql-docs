---
title: "Security considerations for machine learning in SQL Server | Microsoft Docs"
ms.date: "11/16/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d5065197-69e6-4fce-9654-00acaecc148b
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Security considerations for machine learning in SQL Server

This article lists security considerations that the administrator or architect should bear in mind when using machine learning services.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Use a firewall to restrict network access

In a default installation, a Windows firewall rule is used to block all outbound network access from external runtime processes. Firewall rules should be created to prevent the external runtime processes from downloading packages or from making other network calls that could potentially be malicious.

If you are using a different firewall program, you can also create rules to block outbound network connection for external runtimes, by setting rules for the local user accounts or for the group represented by the user account pool.

We strongly recommend that you turn on Windows Firewall (or another firewall of your choice) to prevent unrestricted network access by the R or Python runtimes.

## Authentication methods supported for remote compute contexts

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] supports both Windows Integrated Authentication and SQL logins when creating connections between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a remote data science client.

For example, say you are developing an R solution on your laptop and want to perform computations on the SQL Server computer. You would create a SQL Server data source in R, by using the **rx** functions and defining a connection string based on your Windows credentials.

When you change the _compute context_ from your laptop to the SQL Server computer, all R code is executed on the SQL Server computer, if your Windows account has the necessary permissions. Moreover, any SQL queries executed as part of the R code are run under your credentials as well.

The use of SQL logins is also supported in this scenario. However, this requires that the SQL Server instance be configured to allow mixed mode authentication.

### Implied authentication

 In general, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] starts the external script runtime and executes scripts under its own account. However, if the external runtime makes an ODBC call, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] impersonates the credentials of the user that sent the command to ensure that the ODBC call does not fail. This is called *implied authentication*.
 
 > [!IMPORTANT]
 > For implied authentication to succeed, the Windows users group that contains the worker accounts (by default, **SQLRUser**) must have an account in the master database for the instance, and this account must be given permissions to connect to the instance.
 > 
 > The group **SQLRUser** is also used when running Python scripts. 

In general, we recommend that you move larger datasets into SQL Server beforehand, rather than try to read data using RODBC or another library. Also, use a SQL Server query or view as your primary data source, for better performance. 

For example, you might get your training data (typically the largest data) from SQL Server and get a list of factors from an external source. You can define a linked server to get data from most ODBC data sources. For more information, see [Create a linked server](https://docs.microsoft.com/sql/relational-databases/linked-servers/create-linked-servers-sql-server-database-engine).

To minimize dependency on ODBC calls to external data sources, you might also perform data engineering as a separate process, and save the results in a table, or use T-SQL. See this tutorial for a good example of data engineering in SQL vs. R: [Create data features using T-SQL](../tutorials/sqldev-create-data-features-using-t-sql.md).

## No support for encryption at rest

[Transparent Data Encryption (TDE)](https://docs.microsoft.com/sql/relational-databases/security/encryption/transparent-data-encryption) is not supported for data sent to or received from the external script runtime. The reason is that R (or Python) runs outside the SQL Server process; thus, data used by the external runtime is not protected by the encryption features of the database engine.  This behavior is no different than any other client running on the SQL Server computer that reads data from the database and makes a copy.

As a consequence, TDE **is not** applied to any data that you use in R or Python scripts, or to any data saved to disk, or to any persisted intermediate results. However, other types of encryption, such as Windows BitLocker encryption or third-party encryption applied at the file or folder level, still apply.

In the case of [Always Encrypted](https://docs.microsoft.com/sql/relational-databases/security/encryption/overview-of-key-management-for-always-encrypted), external runtimes do not have access to the encryption keys; therefore, data cannot be sent to the scripts.

## Resources

For more information about managing the service, and about how to provision the user accounts used to execute machine learning scripts, see [Configure and manage Advanced Analytics Extensions](../../advanced-analytics/r/configure-and-manage-advanced-analytics-extensions.md).

For an explanation of the general security architecture, see:

+ [Security overview for R](security-overview-sql-server-r.md)
+ [Security overview for Python](../python/security-overview-sql-server-python-services.md)
