---
title: XML input file samples (DTA)
description: There are several sample XML input files that you can use with the dta command line tool to tune databases for improved query performance.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 1ed28805-a9ae-43ca-92da-101ba0c0c43a
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# XML Input File Samples (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section contains sample XML input files that you can use with the **dta** command line tool. This tool is one of the user interfaces to the Database Engine Tuning Advisor, a tool you can use to tune databases for improved query performance. Database Engine Tuning Advisor analyzes the effects of a workload against a database or multiple databases. A workload is a set of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that execute against databases that you want to tune. After Database Engine Tuning Advisor finishes analyzing the effects of the workload, it creates a recommendation for adding indexes, indexed views, or partitioning strategies that will improve query performance on your databases.

## In This Section

- [Simple XML Input File Sample &#40;DTA&#41;](../../tools/dta/simple-xml-input-file-sample-dta.md)  

- [XML Input File Sample with Inline Workload &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-inline-workload-dta.md)  

- [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md)  

## See Also

[Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md)