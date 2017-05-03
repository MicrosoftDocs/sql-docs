---
title: "SQL Server R Services Performance Tuning | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: cf6f3b7d-f9f9-4e45-b0d1-07850b53e0c5
caps.latest.revision: 20
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server R Services Performance Tuning
[!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)] provides a platform for developing intelligent applications that uncover new insights. A data scientist can use the power of R language to train and create models using data stored inside [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. Once the model is ready for production, a data scientist can work with database administrators and SQL engineers to deploy their solution in production. The information in this section provides high level guidance on tuning performance both when creating and training models, and when deploying models to production.

The information in this document assumes that you are familiar with [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)] concepts and terminology. For general information on R Services, see [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md).

> [!NOTE]
> While much of the information in this section is general guidance on configuring [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], some information is specific to RevoScaleR analytic functions.

## In This Section

* [SQL Server Configuration](../../advanced-analytics/r-services/sql-server-configuration-r-services.md): This document provides guidance for configuring the hardware that [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] is installed on. It is most useful to __Database Administrators__.

* [R and Data Optimization](../../advanced-analytics/r-services/r-and-data-optimization-r-services.md): This document provides guidance on using R scripts with R Services. It is most useful to __Data Scientists__.

* [Performance Case Study](../../advanced-analytics/r-services/performance-case-study-r-services.md): This document provides test data and R scripts that can be used to test the impact of guidance provided in the previous documents.

## References

The following are links to information used in the development of this document.

* [How to determine the appropriate page file size for 64-bit versions of Windows](https://support.microsoft.com/kb/2860880)

* [Data Compression](../../relational-databases/data-compression/data-compression.md)

* [Enable Compression on a Table or Index](../../relational-databases/data-compression/enable-compression-on-a-table-or-index.md)

* [Disable Compression on a Table or Index](../../relational-databases/data-compression/disable-compression-on-a-table-or-index.md)

* [DISKSPD storage load generator/performance test tool](https://github.com/microsoft/diskspd)

* [FSUtil utility reference](https://technet.microsoft.com/library/cc753059.aspx)

* [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md)

* [R Services](../../advanced-analytics/r-services/r-services.md)

* [Performance Tuning Options for rxDForest and rxDTree](https://support.microsoft.com/kb/3104235)

* [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)

* [Explore R and ScaleR in 25 Functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started-tutorial)

* [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)

## See Also

 
 [SQL Server Configuration for R Services](../../advanced-analytics/r-services/sql-server-configuration-r-services.md)
 
 [Performance Case Study](../../advanced-analytics/r-services/performance-case-study-r-services.md)
 
 [R and Data Optimization](../../advanced-analytics/r-services/r-and-data-optimization-r-services.md)
