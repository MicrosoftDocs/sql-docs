---
title: "Dimension Types | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Database Dimension Properties - Types
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The **Type** property setting provides information about the contents of a dimension to server and client applications. In some cases, the **Type** setting only provides guidance for client applications and is optional. In other cases, such as **Accounts** or **Time** dimensions, the **Type** property settings for the dimension and its attributes determine specific server-based behaviors and may be required to implement certain behaviors in the cube. For example, the **Type** property of a dimension can be set to **Accounts** to indicate to client applications that the standard dimension contains account attributes. For more information about time, account, and currency dimensions, see [Create a Date type Dimension](../../analysis-services/multidimensional-models/database-dimensions-create-a-date-type-dimension.md), [Create a Finance Account of parent-child type Dimension](../../analysis-services/multidimensional-models/database-dimensions-finance-account-of-parent-child-type.md), and [Create a Currency type Dimension](../../analysis-services/multidimensional-models/database-dimensions-create-a-currency-type-dimension.md).  
  
 The default setting for the dimension type is **Regular**, which makes no assumptions about the contents of the dimension. This is the default setting for all dimensions when you initially define a dimension unless you specify **Time** when defining the dimension using the Dimension Wizard. You should also leave **Regular** as the dimension type if the Dimension Wizard does not list an appropriate type for Dimension type.  
  
## Available Dimension Types  
 The following table describes the dimension types available in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
|Dimension type|Description|  
|--------------------|-----------------|  
|Regular|A dimension whose type has not been set to a special dimension type.|  
|Time|A dimension whose attributes represent time periods, such as years, semesters, quarters, months, and days.|  
|Organization|A dimension whose attributes represent organizational information, such as employees or subsidiaries.|  
|Geography|A dimension whose attributes represent geographic information, such as cities or postal codes.|  
|BillOfMaterials|A dimension whose attributes represent inventory or manufacturing information, such as parts lists for products.|  
|Accounts|A dimension whose attributes represent a chart of accounts for financial reporting purposes.|  
|Customers|A dimension whose attributes represent customer or contact information.|  
|Products|A dimension whose attributes represent product information.|  
|Scenario|A dimension whose attributes represent planning or strategic analysis information.|  
|Quantitative|A dimension whose attributes represent quantitative information.|  
|Utility|A dimension whose attributes represent miscellaneous information.|  
|Currency|This type of dimension contains currency data and metadata.|  
|Rates|A dimension whose attributes represent currency rate information.|  
|Channel|A dimension whose attributes represent channel information.|  
|Promotion|A dimension whose attributes represent marketing promotion information.|  
  
## See Also  
 [Create a Dimension by Using an Existing Table](../../analysis-services/multidimensional-models/create-a-dimension-by-using-an-existing-table.md)   
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)  
  
  
