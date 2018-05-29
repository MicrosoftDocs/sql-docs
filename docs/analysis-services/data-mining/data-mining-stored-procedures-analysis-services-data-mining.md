---
title: "Data Mining Stored Procedures (Analysis Services - Data Mining) | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Stored Procedures (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports stored procedures that can be written in any managed language. The managed languages that are supported include [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] .NET, C#, and managed C++. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can call the stored procedures directly by using the **CALL** statement, or as part of a Data Mining Extensions (DMX) query.  
  
 For more information about calling [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stored procedures, see [Calling Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/calling-stored-procedures.md).  
  
 For general information about programmability, see [Data Mining Programming](../../analysis-services/data-mining-programming.md).  
  
 For additional information about how to program data mining objects, see the article, "[SQL Server Data Mining Programmability](http://go.microsoft.com/fwlink/?LinkId=93735)", in the MSDN library.  
  
> [!NOTE]  
>  When you query mining models, especially when you test new data mining solutions, you might find it convenient to call the system stored procedures that are used internally by the data mining engine. You can view the names of these system stored procedures by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to create a trace on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server, and then creating, browsing, and querying the data mining models. However, [!INCLUDE[msCoName](../../includes/msconame-md.md)] does not guarantee the compatibility of system stored procedures between versions, and you should never use calls to the system stored procedures in a production system. Instead, for compatibility, you should create your own queries by using DMX or XML/A.  
  
## In This Section  
  
-   [SystemGetCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md)  
  
-   [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md)  
  
-   [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md)  
  
-   [SystemGetClusterAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md)  
  
## See Also  
 [Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md)   
 [Cross-Validation Tab &#40;Mining Accuracy Chart View&#41;](http://msdn.microsoft.com/library/bd215a68-1ad7-4046-9c44-ec8e2be13a64)   
 [Calling a Stored Procedure](../../relational-databases/native-client-odbc-stored-procedures/calling-a-stored-procedure.md)  
  
  
