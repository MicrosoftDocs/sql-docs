---
title: "Deprecated Analysis Services Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services, backward compatibility"
  - "SSAS, backward compatibility"
  - "SQL Server Analysis Services, backward compatibility"
  - "deprecated features [Analysis Services]"
ms.assetid: 2c96ecfe-a170-41d0-bee3-74503f880197
author: minewiskan
ms.author: owend
manager: craigg
---
# Deprecated Analysis Services Features in SQL Server 2014
  This topic describes the deprecated [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features that are still available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. These features are scheduled to be removed in a future release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Deprecated features should not be used in new applications.  
  
## Features Not Supported in the Next Version of SQL Server  
 The following [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features will not be supported in the next version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Do not use these features in new development work, and modify applications that currently use these features as soon as possible.  
  
|Category|Deprecated feature|Replacement|  
|--------------|------------------------|-----------------|  
|MDX Function|CalculationPassValue function|None. The OLAP engine manages the calculation pass. This function is no longer needed.|  
|MDX Function|CalculationCurrentPass function|None. The OLAP engine manages the calculation pass. This function is no longer needed.|  
|Multidimensional Expressions (MDX)|NON_EMPTY_BEHAVIOR query optimizer hint was turned on by default.|The NON_EMPTY_BEHAVIOR query optimizer hint will be turned off by default in a future release. It is an MDX optimization hint that can produce incorrect results when it is not used correctly.|  
|Other|CELL_EVALUATION_LIST intrinsic cell property|Originally provided a list of evaluated formulas that apply to a cell. It is blank in this release of Analysis Services.  Solve order is now specified in MDX script. For more information, see [Understanding Pass Order and Solve Order &#40;MDX&#41;](multidimensional-models/mdx/mdx-data-manipulation-understanding-pass-order-and-solve-order.md)|  
|Objects|COM assemblies|COM assemblies can pose a security risk. Support for COM assemblies will be removed in a future release.|  
  
## Features Not Supported in a Future Version of SQL Server  
 The following [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features are supported in the next version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but will be removed in a later version. The specific version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has not been determined.  
  
|Category|Deprecated feature|Replacement|  
|--------------|------------------------|-----------------|  
|Multidimensional models|Remote partitions|None. Use local partitions instead. See [Create and Manage a Local Partition &#40;Analysis Services&#41;](multidimensional-models/create-and-manage-a-local-partition-analysis-services.md) for more information.|  
|Multidimensional models|Remote linked measure groups|A remote linked measure group is a linked measure group using a data source on a remote server. The ability to use a remote data source for a linked measure group is now scheduled for deprecation.<br /><br /> There is no replacement for this feature. We recommend using local linked measure groups instead. See [Linked Measure Groups](multidimensional-models/linked-measure-groups.md) for more information.|  
|Multidimensional models|Dimensional writeback|None. Use partition writeback if you require writeback capability. See [Set Partition Writeback](multidimensional-models/set-partition-writeback.md) for more information.|  
|Multidimensional models|Linked dimensions|None. Consider copying dimensions to additional models rather than linking to a dimension in another model.|  
|MDX|Non_Empty_Behavior property|None. When creating a calculated member, setting this property incorrectly increases the likelihood of returning invalid results. Recent optimizations to the OLAP engine have improved operations over sparse data sets, making this property less relevant.|  
  
## See Also  
 [Analysis Services Backward Compatibility](analysis-services-backward-compatibility.md)   
 [Discontinued Analysis Services Functionality in SQL Server 2014](discontinued-analysis-services-functionality-in-sql-server-2014.md)  
  
  
