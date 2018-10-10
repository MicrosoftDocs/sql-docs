---
title: Advanced configuration for SQL Server Machine Learning Services | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Advanced configuration options for SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes changes that you can make after setup, to modify the configuration of the external script runtime and other services associated with machine learning in SQL Server.

##  <a name="bkmk_Provisioning"></a> Provision additional user accounts for machine learning

External script processes in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run in the context of low-privilege local user accounts. Running these processes in individual low-privilege accounts has the following benefits:

+ Reduces privileges of the external script runtime processes on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer
+ Provides isolation between sessions of an external runtime such as R or Python.

As part of setup, a new Windows *user account pool* is created that contains the local user accounts required for running external runtime processes, such as R or Python. You can modify the number of users as needed to support machine learning tasks. 

Additionally, your database administrator must give this group permission to connect to any instance where machine learning has been enabled. For more information, see [Security configuration for SQLRUserGroup](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).

To protext sensitive resources on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can define an access control list (ACL) for this group. By specifying resources that the group is denied access to, you can prevent access by external processes such as the R or Python runtimes.

+ The user account pool is linked to a specific instance. A separate pool of worker accounts is needed for each instance on which machine learning has been enabled. Accounts cannot be shared between instances.

+ User account names in the pool are of the format SQLInstanceName*nn*. For example, if you are using the default instance for machine learning, the user account pool supports account names such as MSSQLSERVER01, MSSQLSERVER02, and so forth.

+ The size of the user account pool is static and the default value is 20. The number of external runtime sessions that can be launched simultaneously is limited by the size of this user account pool. To change this limit, an administrator should use SQL Server Configuration Manager.

For more information about how to make changes to the user account pool, see [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../../advanced-analytics/administration/modify-user-account-pool.md).

##  <a name="bkmk_ManagingMemory"></a> Manage memory used by external script processes

By default, the external script runtimes for machine learning are limited to no more than 20% of total machine memory. It depends on your system, but in general, you might find this limit inadequate for serious machine learning tasks such as training a model or predicting on many rows of data. 

To support machine learning, an administrator can increase this limit. When you do so, you might need to reduce the amount of memory reserved for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or for other services. You should also consider using Resource Governor to define an external resource pool or pools, so that you can allocate specific resource pools to R or Python jobs.

For more information, see [Resource governance for machine learning](../../advanced-analytics/r/resource-governance-for-r-services.md).

## See Also

[Security overview for the extensibility framework in SQL Server Machine Learning Services](../../advanced-analytics/concepts/security.md)
