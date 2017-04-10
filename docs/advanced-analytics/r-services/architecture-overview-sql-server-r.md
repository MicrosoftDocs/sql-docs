---
title: "Architecture Overview (SQL Server R Services) | Microsoft Docs"
ms.custom: ""
ms.date: "04/05/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6c4a4f66-ea3e-4a73-acf2-6c8aeafc94b0
caps.latest.revision: 9
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Architecture Overview (SQL Server R Services)

This section provides an overview of the architecture of SQL Server 2017 R Services, and SQL Server vNext Machine Learning Services. The architecture for the extensibility architecture is the same or very similar for the SQL Server 2016 and SQL Server vNext releases, and similar also for R and Python. However, to simplify the discussion, this topic discusses only the R components, including new components added in the SQL Server database engine to support external script execution, security, R libraries, and interoperability with open source R.

Additional details are provided in the links for each section.

## R Interoperability

Both SQL Server 2016 R Services and SQL Server Machine Learning Services (In-Database) install an open source distribution of R, as well as packages provided by Microsoft that support distributed and/or parallel processing. 

The architecture is designed such that open source R runs in a separate process from SQL Server, ensuring near-complete compatibility with standard R solutions. Current users of R should be able to port their R code and execute it in T-SQL with relatively minor modifications. 

However, if you need to scale your solution or use parallel processing, you can use the RevoScaleR packages. To get additional performance, you can also run standard R packages in the context of the server, by using rxExec or other functions in the RevoScaleR package that support remote compute contexts.

For more information about the R components that are installed, or the interaction of SQL Server with R, see [R Interoperability](../../advanced-analytics/r-services/r-interoperability-in-sql-server.md)

## New Components to Support R Integration

The extensibility framework introduced in SQL Server 2016 is continued in SQL Server vNext. These components are used by SQL Server to start the external runtime for R, pass data between R and the database engine, and coordinate parallel tasks needed for an R job.

The role of these additional components is to improve data exchange speed and compression, while providing a secure, high-performance platform for running external scripts.

For detailed description of the components that support R, such as the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] and RLauncher, see [New Components](../../advanced-analytics/r-services/new-components-in-sql-server-to-support-r.md).

## Security

When you run R using Machine Learning Services or SQL Server R Services, all R scripts are executed outside the SQL Server process, to provide security and greater manageability. This isolation of processes holds true regardless of whether you run the R script as part of a stored procedure, or connect to the SQL Server computer job from a remote computer and start a job that uses the server as the compute context. 

SQL Server intercepts all job requests, secures the task and its data using Windows job objects, and maintains security over data using SQL Server user accounts and database roles. 

Data is kept within the compliance boundary by enforcing SQL Server security at the table, database, and instance level. The database administrator can control who has the ability to run R jobs, and who has the ability to install or share R packages. The administrator can also monitor the use of R scripts by either rremote or  local users, and monitor and manage the resources consumed.

For details, see [Security for R](../../advanced-analytics/r-services/security-overview-sql-server-r.md)



## See Also
[R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md)

