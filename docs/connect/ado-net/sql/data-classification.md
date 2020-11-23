---
title: "Data discovery and classification in SqlClient"
description: "Describes how to check if a SQL Server database supports data classification and how to access data classification information through a SqlDataReader object."
ms.date: "06/15/2020"
dev_langs: 
  - "csharp"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: johnnypham
ms.author: v-jopha
ms.reviewer: 
---
# Data discovery and classification in SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

[Data Discovery & Classification](../../../relational-databases/security/sql-data-discovery-and-classification.md) is a set of advanced services for discovering, classifying, labeling & reporting the sensitive data in your databases. SqlClient provides an API exposing read-only Data Discovery and Classification information when the underlying source supports the feature. This information is accessed through SqlDataReader.

Microsoft.Data.SqlClient v2.1.0 introduces support for Data Classification's `Sensitivity Rank` information. `Sensitivity Rank` is an identifier based on a predefined set of values which define sensitivity rank. It can be used by other services like Advanced Threat Protection to detect anomalies based on their rank. The following new APIs are now available in Microsoft.Data.SqlClient namespace:

```csharp
public class SensitivityClassification
{
  // Returns the sensitivity rank for the query associated with the active 'SqlDataReader'.
  public SensitivityRank SensitivityRank;
}

public class SensitivityProperty
{
  // Returns the sensitivity rank for this 'SensitivityProperty' Object
  public SensitivityRank SensitivityRank;
}

public enum SensitivityRank
{
    NOT_DEFINED = -1,
    NONE = 0,
    LOW = 10,
    MEDIUM = 20,
    HIGH = 30,
    CRITICAL = 40
}
```

> [!NOTE]
> Microsoft.Data.SqlClient reads `Sensitivity Rank` information only if SQL Server supports Data Classification with rank. For servers use old version of Data Classification without rank, the rank value for queries is "NOT DEFINED".

This sample application demonstrates how to access the Data Classification properties of SqlDataReader.

[!code-csharp [SqlDataReader_DataDiscoveryAndClassification#1](~/../sqlclient/doc/samples/SqlDataReader_DataDiscoveryAndClassification.cs#1)]


**See also**  

 - [SQL Server features and ADO.NET](sql-server-features-adonet.md)
 - [sys.sensitivity_classifications (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md)
 - [ADD SENSITIVITY CLASSIFICATION](../../../t-sql/statements/add-sensitivity-classification-transact-sql.md)