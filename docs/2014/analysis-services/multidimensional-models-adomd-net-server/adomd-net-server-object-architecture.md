---
title: "ADOMD.NET Server Object Architecture | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "ADOMD.NET, object model"
  - "object model [ADOMD.NET]"
ms.assetid: bdc81de9-b390-4654-b62a-cd6c0c9ca10d
author: minewiskan
ms.author: owend
---
# ADOMD.NET Server Object Architecture
  The ADOMD.NET server objects are helper objects that can be used to create user defined functions (UDFs) or stored procedures in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  To use the `Microsoft.AnalysisServices.AdomdServer` namespace (and these objects), a reference to the msmgdsrv.dll must be added to UDF project or stored procedure.  
  
 ![Shows the object relationships in ADOMD.NET Server](../../analysis-services/dev-guide/media/adomdnetserverobjectmodel.gif "Shows the object relationships in ADOMD.NET Server")  
ADOMD.NET Object Model  
  
 Interaction with the ADOMD.NET object hierarchy typically starts with one or more of the objects in the topmost layer, as described in the following table.  
  
|To|Use this object|  
|--------|---------------------|  
|Evaluate Multidimensional Expressions (MDX) expressions|[Microsoft.AnalysisServices.AdomdServer.Expression](/previous-versions/sql/sql-server-2014/ms143609)<br /> The [Microsoft.AnalysisServices.AdomdServer.Expression](/previous-versions/sql/sql-server-2014/ms143609) object provides a way to run an MDX expression and evaluate that expression under a specified tuple.|  
|Provide support for executing MDX functions without constructing the full MDX statement|[Microsoft.AnalysisServices.AdomdServer.MDX](/previous-versions/sql/sql-server-2014/ms143616)<br /> The [Microsoft.AnalysisServices.AdomdServer.MDX](/previous-versions/sql/sql-server-2014/ms143616) object is convenient for calling predefined MDX functions without using the [Microsoft.AnalysisServices.AdomdServer.Expression](/previous-versions/sql/sql-server-2014/ms143609) object. Additional functions for the [Microsoft.AnalysisServices.AdomdServer.MDX](/previous-versions/sql/sql-server-2014/ms143616) object should be available in future releases.|  
|Represent the current execution context for the UDF|[Microsoft.AnalysisServices.AdomdServer.Context](/previous-versions/sql/sql-server-2014/ms143353(v=sql.120))<br /> The [Microsoft.AnalysisServices.AdomdServer.Context](/previous-versions/sql/sql-server-2014/ms143353(v=sql.120)) object exposes information such as the current cube or mining model and various metadata collections. One key use of the [Microsoft.AnalysisServices.AdomdServer.Context](/previous-versions/sql/sql-server-2014/ms143353(v=sql.120)) object is the [Microsoft.AnalysisServices.AdomdServer.Hierarchy.CurrentMember*](/previous-versions/sql/sql-server-2014/ms137044) property of the [Microsoft.AnalysisServices.AdomdServer.Hierarchy](/previous-versions/sql/sql-server-2014/ms143578) object. This key usage enables the author of the UDF or stored procedure to make decisions based on what member from a certain dimension the query is on.|  
|Create sets and tuples|[Microsoft.AnalysisServices.AdomdServer.SetBuilder](/previous-versions/sql/sql-server-2014/ms144510), [Microsoft.AnalysisServices.AdomdServer.TupleBuilder](/previous-versions/sql/sql-server-2014/ms145407)<br /> The [Microsoft.AnalysisServices.AdomdServer.SetBuilder](/previous-versions/sql/sql-server-2014/ms144510) provides a way to create immutable sets, while the [Microsoft.AnalysisServices.AdomdServer.TupleBuilder](/previous-versions/sql/sql-server-2014/ms145407) provides a way to create immutable tuples.|  
|Support implicit conversion and casting among the six basic types of the MDX language|[Microsoft.AnalysisServices.AdomdServer.MDXValue](/previous-versions/sql/sql-server-2014/ms143573(v=sql.120))<br /> The [Microsoft.AnalysisServices.AdomdServer.MDXValue](/previous-versions/sql/sql-server-2014/ms143573(v=sql.120)) object provides implicit conversion and casting among the following types:<br /><br /> -   [Microsoft.AnalysisServices.AdomdServer.Hierarchy](/previous-versions/sql/sql-server-2014/ms143578)<br />-   [Microsoft.AnalysisServices.AdomdServer.Level](/previous-versions/sql/sql-server-2014/ms143581)<br />-   [Microsoft.AnalysisServices.AdomdServer.Member](/previous-versions/sql/sql-server-2014/ms143820)<br />-   [Microsoft.AnalysisServices.AdomdServer.Tuple](/previous-versions/sql/sql-server-2014/ms145330)<br />-   [Microsoft.AnalysisServices.AdomdServer.Set](/previous-versions/sql/sql-server-2014/ms144530)<br />-   Scalar, or value types|  
  
## See Also  
 [ADOMD.NET Server Programming](https://docs.microsoft.com/bi-reference/adomd/multidimensional-models-adomd-net-server/adomd-net-server-programming)  
  
  
