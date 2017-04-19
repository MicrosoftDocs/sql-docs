---
title: "Configuration and Management for Machine Learning | Microsoft Docs"
ms.custom: ""
ms.date: "05/31/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e0fd4554-60c6-4181-ac4c-2e366fb434f6
caps.latest.revision: 7
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Configuration and Management for Machine Learning

This article provides links to more detailed information about how to configure a server to support machine learnign servies with SQL Server in thee products:

+ SQL Server 2016 R Services (In-Database)
+ SQL Server vNext Machine Learning Services (In-Databae)

> [!NOTE]
> **Support for Python**
> This content was originally written for the SQL Server 2016 release, which supported only the R language.
> In SQL Server vNext, support for Python has been added, but the underlying architecture and services framework is the same. Therefore, you can configure security, memory, resource governance and other options to support execution of Python scripts, the same way that you would for R scripts.
> 
> However, because support for Python is a new feature, detailed information about potential optimizations for Python workloads is not available yet. Please check back later.

## R Package Management  

These topics describe how to install new R packages on the SQL Server instance, manage R package libraries, and restore package libraries after a database restore.

+ [Installing and Managing R Packages](installing-and-managing-r-packages.md)
+ [Installing New R Packages](install-additional-r-packages-on-sql-server.md)
+ [Enable Package Management for an Instance using Database Roles](r-package-how-to-enable-or-disable.md)
+ [Create a Local Package Repository using miniCRAN](create-a-local-package-repository-using-minicran.md)
+ [Determine Which R Packages are Installed on SQl Server](determine-which-packages-are-installed-on-sql-server.md)
+ [Synchronizing R Packages between SQL Server and the File System](package-install-uninstall-and-sync.md)
+ [R Packages Installed in User Libraries](packages-installed-in-user-libraries.md)

## Service Configuration

These topics describe how to make changes to the underlying service architecture and to security principals associated with the extensibility service.

+ [Security Considerations](security-considerations-for-the-r-runtime-in-sql-server.md)
+ [Modify the User Account Pool for SQL Server R Services](../../advanced-analytics/r-services/modify-the-user-account-pool-for-sql-server-r-services.md)
+ [Configure and Manage Advanced Analytics Extensions](../../advanced-analytics/r-services/configure-and-manage-advanced-analytics-extensions.md)
+ [Enable Package Management for an Instance using Database Roles](r-package-how-to-enable-or-disable.md)
+ [Performance Tuning for R Services](sql-server-r-services-performance-tuning.md)

## Resource Governance

These topics describe how to implement resource management for R or Python jobs using the Resource Governor feature avaialble in Enterprise Edition.

+ [Resource Governance for R Services](../../advanced-analytics/r-services/resource-governance-for-r-services.md)
+ [How to Create a Resource Pool for R](../../advanced-analytics/r/how-to-create-a-resource-pool-for-r.md)

Also see:

+ [Monitor R Using Custom SSMS Reports](monitor-r-services-using-custom-reports-in-management-studio.md)

## Initial Setup

Additional help related to initial setup and configuration can be found in these topics:

[Upgrade and Installation FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md)
[Security Considerations](../r/security-considerations-for-the-r-runtime-in-sql-server.md)
[Known Issues for R Services](../../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md)

