---
title: "API reference for SQL Server Machine Learning Services | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# API reference for SQL Server Machine Learning Services

This article provides links to the reference documentation for machine learning APIs used by SQL Server. 

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

For the most part, SQL Server consumes the same R and Python libraries that are provided in Microsoft R Server and Microsoft Machine Learning Server. 

> [!NOTE]
> Documentation for all APIs is derived from source code and is not post-edited.

## R

+ [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)

    Scalable algorithms that support remote compute contexts and multiple data sources.

+ [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package)

    Fast, scalable machine learning algorithms and transforms for R. Requires RevoScaleR.

+ [olapR](https://docs.microsoft.com/r-server/r-reference/olapr/olapr)

   Reads the schema of OLAP data sources and executes MDX queries.

+ [sqlrutils](https://docs.microsoft.com/r-server/r-reference/sqlrutils/sqlrutils)

    Helper functions for generating a well-formed stored procedure from R code.

+ [mrsdeploy](https://docs.microsoft.com/r-server/r-reference/mrsdeploy/mrsdeploy-package)

   Functions for establishing a remote session in a console application and for publishing and managing a web service that uses R or Python code.

## Python

+ [revoscalepy](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package)

    Python equivalent of the RevoScaleR package for the R language. Supports the same compute contexts and data sources.

+ [Microsoftml for Python](https://docs.microsoft.com/r-server/python-reference/microsoftml/microsoftml-package)

    Python equivalent of the MicrosoftML package for R. Supports the same compute contexts and data sources, and includes fast, scalable algorithms and transformations from Microsoft.

## Related APIs

+ [RevoPEMAR function reference](https://docs.microsoft.com/r-server/r-reference/revopemar/pemar)

    Supports development of parallel algorithms

+ [RevoUtils](https://docs.microsoft.com/r-server/r-reference/revoutils/revoutils)

    Utility functions for use with RevoScaleR environments

## Other

"How-to" topics and summaries specific to use of these R or Python APIs in SQL Server can be found here:

+ [ScaleR functions for working with SQL Server](scaler-functions-for-working-with-sql-server-data.md)
+ [Generate a stored procedure using sqlrutils](generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md)
+ [Read MDX Data into R using olapR](how-to-create-mdx-queries-using-olapr.md)
