---
title: "sp_rxPredict | Microsoft Docs"
ms.custom: ""
ms.date: "07/14/201"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_rxPredict"
  - "sp_rxPredict_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_rxPredict procedure"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# sp_rxPredict  
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]  

Generates a predicted value based on a stored model.

Provides scoring on machine learning models in near real-time. `sp_rxPredict` is a stored procedure provided as a wrapper for the `rxPredict` function in [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler) and [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package). It is written in C+ and is optimized specifically for scoring operations. It supports both R or Python machine learning models.

**This topic applies to**:  
- SQL Server 2017  
- SQL Server 2016 R Services with upgrade to Microsoft R Server  

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

Requires one of the following editions:  
- SQL Server 2017 Machine Learning Services (includes Microsoft R Server 9.1.0)  
- Microsoft Machine Learning Server  
- SQL Server R Services 2016, with an upgrade of the R Services instance to Microsoft R Server 9.1.0 or later  

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

  For information about corresponding SQL types, see [SQL-CLR Type Mapping](https://msdn.microsoft.com/library/bb386947.aspx) or [Mapping CLR Parameter Data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).

