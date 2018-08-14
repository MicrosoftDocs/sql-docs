---
title: "Developing with Analysis Management Objects (AMO) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: amo
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Developing with Analysis Management Objects (AMO)
Analysis Management Objects (AMO) is the complete library of programmatically accessed objects that enables an application to manage a running instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].

This section explains AMO concepts, focusing on major objects, how and when to use them, and the way they are interrelated. For more information about specific objects or classes, see:

- [Microsoft.AnalysisServices Namespace](http://msdn.microsoft.com/library/microsoft.analysisservices.aspx), for reference documentation
- [Analysis Services Management Objects (AMO)](http://www.bing.com/search?q=Analysis+Services+Management+Objects+%28AMO%29), as a Bing.com general search.


 **New in SQL Server 2016**

In SQL Server 2016, AMO is refactored into multiple assemblies. Generic classes such as Server, Database, and Roles are in the **Microsoft.AnalysisServices.Core** Namespace. Multidimensional-specific APIs remain in [Microsoft.AnalysisServices Namespace](https://msdn.microsoft.com/library/ms146720.aspx).

Custom scripts and applications written against earlier versions of AMO will continue to work with no modification. However, if you have script or applications that target SQL Server 2016 specifically, or if you need to rebuild a custom solution, be sure to add the new assembly and namespace to your project.

## See Also
[Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../../../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md)
[Developing with XMLA in Analysis Services](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)
