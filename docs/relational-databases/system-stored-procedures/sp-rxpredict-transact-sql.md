---
title: "sp_rxPredict | Microsoft Docs"
ms.custom: ""
ms.date: "08/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.component: "system-stored-procedures"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_rxPredict"
  - "sp_rxPredict_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_rxPredict procedure"
author: "HeidiSteen"
ms.author: "heidist"
manager: cgronlun
---
# sp_rxPredict  
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Generates a predicted value for a given input based on a machine learning model stored in a binary format in a SQL Server database.

Provides scoring on R and Python machine learning models in near real-time. `sp_rxPredict` is a stored procedure provided as a wrapper for the `rxPredict` R function in [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler) and [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package), and the [rx_predict](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict) Python function in [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package). It is written in C+ and is optimized specifically for scoring operations.

**This topic applies to**:  
- SQL Server 2017  
- SQL Server 2016 R Services with [upgraded R components](https://docs.microsoft.com/sql/advanced-analytics/r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server)

## Syntax

```
sp_rxPredict  ( @model, @input )
```

### Arguments

**model**

A pretrained model in a supported format. 

**input**

A valid SQL query

### Return values

A score column is returned, as well as any pass-through columns from the input data source.
Additional score columns, such as confidence interval, can be returned if the algorithm supports generation of such values.

## Remarks

To enable use of the stored procedure, SQLCLR must be enabled on the instance.

> [!NOTE]
> Consider the security implications before you enable this option.

The user needs `EXECUTE` permission on the database.

### Supported platforms
 
- SQL Server 2017 Machine Learning Services (includes R Server 9.2)  
- SQL Server 2017 Machine Learning Server (Standalone) 
- SQL Server R Services 2016, with an upgrade of the R Services instance to R Server 9.1.0 or later  

### Supported algorithms

For a list of supported algorithms, see [Real-time scoring](../../advanced-analytics/real-time-scoring.md).

The following model types are **not** supported:  
- Models containing other, unsupported types of R transformations  
- Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR  
- PMML models  
- Models created using other R libraries from CRAN or other repositories  
- Models containing any other kind of R transformation other than those listed here  

## Examples

```sql
DECLARE @model = SELECT @model 
FROM model_table 
WHERE model_name = 'rxLogit trained';

EXEC sp_rxPredict @model = @model,
@inputData = N'SELECT * FROM data';
```

In addition to being a valid SQL query, the input data in *@inputData* must include columns compatible with the columns in the stored model.

`sp_rxPredict` supports only the following .NET column types: double, float, short, ushort, long, ulong and string. You may need to filter out unsupported types in your input data before using it for real-time scoring. 

  For information about corresponding SQL types, see [SQL-CLR Type Mapping](/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping) or [Mapping CLR Parameter Data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).

