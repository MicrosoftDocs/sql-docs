---
title: "Describes how to create calculation groups in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/20/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create calculation groups
 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]

See [Calculation groups](as-calculation-groups.md) to learn more.

## Create calculation groups

Currently, creating calculation groups in SQL Server Data Tools or Visual Studio with Analysis Services extensions is not supported. It will be included in updates closer to SQL Server 2019 general availability (GA). Until that time, you can create calculation groups by using TMSL script in SQL Server Management Studio (SSMS) or by using the open source Tabular Editor.

## By using Tabular Editor

[Tabular Editor](https://github.com/otykier/TabularEditor) is an open source tool available on GitHub. Version 2.9.1 and higher support creating calculation groups. 

#### To create a new calculation group and calculation items

1. Press **Alt** + **7**, or right click **Tables** > **Create New**, **Calculation Group**.
2. Rename your new calculation group, for example Time Intelligence.
1. Right click the calculation group, 

## By using TMSL

1. In SSMS, right click the model database > **Script** > **Script Database as** > **CREATE OR REPLACE to ** > **New Query Editor Window**.



## See also  

  
  
