---
title: Troubleshooting
description: Provides a starting point for working through known issues.
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/31/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Troubleshoot machine learning in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Use this article as a starting point for working through known issues.

## Known issues

The following articles describe known issues with the current and previous releases:

+ [Known issues for R Services](../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md)
+ [SQL Server 2016 release notes](../sql-server/sql-server-2016-release-notes.md)
+ [SQL Server 2017 release notes](../sql-server/sql-server-2017-release-notes.md)

## How to gather system information

If you have encountered an error, or need to understand an issue in your environment, it is important that you collect related information systematically. The following article provides a list of information that facilitates self-help troubleshooting, or a request for technical support.

+ [Data collection for machine learning troubleshooting](data-collection-ml-troubleshooting-process.md)

## Setup and configuration guides

Start here if you have not set up machine learning with SQL Server, or if you want to add the feature:

+ [Install SQL Server Machine Learning Services (In-Database)](install/sql-machine-learning-services-windows-install.md)
+ [Install SQL Server Machine Learning Server (Standalone)](install/sql-machine-learning-standalone-windows-install.md)
+ [Command prompt setup](install/sql-ml-component-commandline-install.md)
+ [Offline setup (no internet)](install/sql-ml-component-install-without-internet-access.md)

### Configuration

The following articles contain information about defaults, and how to customize the configuration for machine learning on an instance:

+ [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](administration/modify-user-account-pool.md)   
+ [Configure and manage advanced analytics extensions](r/configure-and-manage-advanced-analytics-extensions.md)  
+ [How to create a resource pool](r/how-to-create-a-resource-pool-for-r.md)
+ [Optimization for R workloads](r/operationalizing-your-r-code.md)
