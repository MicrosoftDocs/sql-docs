---
title: Manage and monitor machine learning solutions in SQL Server | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Managing and monitoring machine learning solutions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes features in SQL Server Machine Learning Services that are relevant to database administrators who need to begin working with R and Python solutions.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Security

Database administrators must provide data access not just to data scientists but to a variety of report developers, business analysts, and business data consumers. The integration of R (and now Python) into SQL Server  provides many benefits to the database administrator who supports the data science role.

+ The architecture of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] keeps your databases secure and isolates the execution of R sessions from the operation of the database instance.

+ You can specify who has permission to execute the R scripts and ensure that the data used in R jobs is managed using the same security roles that are defined in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

+ The database administrator can use roles to manage the installation of R packages and the execution of R and Python scripts.

For more information, see these resources:

+ [Security overview for the extensibility framework in SQL Server Machine Learning Services](../../advanced-analytics/concepts/security.md)

+ [Default R and Python packages in SQL Server](installing-and-managing-r-packages.md)

## Configuration and management

Database administrators must integrate competing projects and priorities into a single point of contact: the database server. They must support analytics while maintaining the health of operational and reporting data stores. The integration of machine learning with SQL Server provides many benefits to the database administrator, who increasingly serves a critical role in deploying an efficient infrastructure for data science.

+ R and Python sessions are executed in a separate process to ensure that your server continues to run as usual even if the external script runtime has problems.

+ Low privilege physical user accounts are used to contain and isolate external script activity.

+ The DBA can use standard SQL Server resource management tools to control the amount of resources allocated to the R runtime, to prevent massive computations from jeopardizing the overall server performance.

For more information, see these resources:

+ [Resource governance for R Services](../r/resource-governance-for-r-services.md)

+ [Configure and manage Advanced Analytics Extensions](../r/configure-and-manage-advanced-analytics-extensions.md)
