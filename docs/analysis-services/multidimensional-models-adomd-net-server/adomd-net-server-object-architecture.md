---
title: "ADOMD.NET Server Object Architecture | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: adomd
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ADOMD.NET Server Object Architecture
  The ADOMD.NET server objects are helper objects that can be used to create user defined functions (UDFs) or stored procedures in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  To use the **Microsoft.AnalysisServices.AdomdServer** namespace (and these objects), a reference to the msmgdsrv.dll must be added to UDF project or stored procedure.  
  
 ![Shows the object relationships in ADOMD.NET Server](../../analysis-services/multidimensional-models-adomd-net-server/media/adomdnetserverobjectmodel.gif "Shows the object relationships in ADOMD.NET Server")  
ADOMD.NET Object Model  
  
 Interaction with the ADOMD.NET object hierarchy typically starts with one or more of the objects in the topmost layer, as described in the following table.  
  
|To|Use this object|  
|--------|---------------------|  
|Evaluate Multidimensional Expressions (MDX) expressions|<xref:Microsoft.AnalysisServices.AdomdServer.Expression><br /> The <xref:Microsoft.AnalysisServices.AdomdServer.Expression> object provides a way to run an MDX expression and evaluate that expression under a specified tuple.|  
|Provide support for executing MDX functions without constructing the full MDX statement|<xref:Microsoft.AnalysisServices.AdomdServer.MDX><br /> The <xref:Microsoft.AnalysisServices.AdomdServer.MDX> object is convenient for calling predefined MDX functions without using the <xref:Microsoft.AnalysisServices.AdomdServer.Expression> object. Additional functions for the <xref:Microsoft.AnalysisServices.AdomdServer.MDX> object should be available in future releases.|  
|Represent the current execution context for the UDF|<xref:Microsoft.AnalysisServices.AdomdServer.Context><br /> The <xref:Microsoft.AnalysisServices.AdomdServer.Context> object exposes information such as the current cube or mining model and various metadata collections. One key use of the <xref:Microsoft.AnalysisServices.AdomdServer.Context> object is the <xref:Microsoft.AnalysisServices.AdomdServer.Hierarchy.CurrentMember%2A> property of the <xref:Microsoft.AnalysisServices.AdomdServer.Hierarchy> object. This key usage enables the author of the UDF or stored procedure to make decisions based on what member from a certain dimension the query is on.|  
|Create sets and tuples|<xref:Microsoft.AnalysisServices.AdomdServer.SetBuilder>, <xref:Microsoft.AnalysisServices.AdomdServer.TupleBuilder><br /> The <xref:Microsoft.AnalysisServices.AdomdServer.SetBuilder> provides a way to create immutable sets, while the <xref:Microsoft.AnalysisServices.AdomdServer.TupleBuilder> provides a way to create immutable tuples.|  
|Support implicit conversion and casting among the six basic types of the MDX language|<xref:Microsoft.AnalysisServices.AdomdServer.MDXValue><br /> The <xref:Microsoft.AnalysisServices.AdomdServer.MDXValue> object provides implicit conversion and casting among the following types:<br /><br /> <xref:Microsoft.AnalysisServices.AdomdServer.Hierarchy><br /><br /> <xref:Microsoft.AnalysisServices.AdomdServer.Level><br /><br /> <xref:Microsoft.AnalysisServices.AdomdServer.Member><br /><br /> <xref:Microsoft.AnalysisServices.AdomdServer.Tuple><br /><br /> <xref:Microsoft.AnalysisServices.AdomdServer.Set><br /><br /> Scalar, or value types|  
  
## See Also  
 [ADOMD.NET Server Programming](https://docs.microsoft.com/bi-reference/adomd/multidimensional-models-adomd-net-server/adomd-net-server-programming)  
  
  
