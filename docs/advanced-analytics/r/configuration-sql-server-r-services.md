---
title: Configure and manage SQL Server Machine Learning Service instances | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Configure and manage machine learning components in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides links to more detailed information about how to configure a server to support machine learning services with SQL Server in these products:

+ SQL Server 2016 R Services (In-Database)
+ SQL Server 2017 Machine Learning Services (In-Database)

> [!NOTE]
> 
> This content was originally written for the SQL Server 2016 release, which supported only the R language.
> 
> In SQL Server 2017, support for Python has been added, but the underlying architecture and services framework is the same. Therefore, you can configure security, memory, resource governance and other options to support execution of Python scripts, the same way that you would for R scripts.
> 
> However, because support for Python is a new feature, detailed information about potential optimizations for the Python workload is not available yet. Please check back later.

## R Package Management

These articles describe how to install new R packages on the SQL Server instance, manage R package libraries, and restore package libraries after a database restore.

+ [Default R and Python packages in SQL Server](installing-and-managing-r-packages.md)
+ [Installing New R Packages](install-additional-r-packages-on-sql-server.md)
+ [Enable Package Management for an Instance using Database Roles](r-package-how-to-enable-or-disable.md)
+ [Create a Local Package Repository using miniCRAN](create-a-local-package-repository-using-minicran.md)
+ [Determine Which R Packages are Installed on SQl Server](determine-which-packages-are-installed-on-sql-server.md)
+ [Synchronizing R Packages between SQL Server and the File System](package-install-uninstall-and-sync.md)
+ [R Packages Installed in User Libraries](packages-installed-in-user-libraries.md)

## Service Configuration

These articles describe how to make changes to the underlying service architecture and how to manage security principals associated with the extensibility service.

+ [Security overview for the extensibility framework in SQL Server Machine Learning Services](../../advanced-analytics/concepts/security.md)
+ [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../../advanced-analytics/administration/modify-user-account-pool.md) 
+ [Configure and Manage Advanced Analytics Extensions](../../advanced-analytics/r/configure-and-manage-advanced-analytics-extensions.md)
+ [Enable Package Management for an Instance using Database Roles](r-package-how-to-enable-or-disable.md)
+ [Performance Tuning for R Services](sql-server-r-services-performance-tuning.md)

## Resource Governance

These articles describe how to implement resource management for R or Python jobs using the Resource Governor feature avaialble in Enterprise Edition.

+ [Resource Governance for R Services](../../advanced-analytics/r/resource-governance-for-r-services.md)
+ [How to Create a Resource Pool for R](../../advanced-analytics/r/how-to-create-a-resource-pool-for-r.md)

Also see:

+ [Monitor R Using Custom SSMS Reports](monitor-r-services-using-custom-reports-in-management-studio.md)

## Initial Setup

Additional help related to initial setup and configuration can be found in these articles:

+ [Upgrade and Installation FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md)
+ [Security overview for the extensibility framework in SQL Server Machine Learning Services](../../advanced-analytics/concepts/security.md)
+ [Known Issues for R Services](../../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md)

