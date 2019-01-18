---
title: "Analysis Services Developer Documentation | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Analysis Services Developer Documentation
[!INCLUDE[ssas-appliesto-sqlas-aas](../includes/ssas-appliesto-sqlas-aas.md)]

In Analysis Services, almost every object and workload is programmable, and often there is more than one approach to choose from.  Options include writing managed code, script, or using open standards like XMLA and MSOLAP if your solution requirements preclude using the .NET framework.

## What you can accomplish in code
Typical programming scenarios include server and database deployment, administration, model and database creation, and data access from your custom applications and reports that consume Analysis Services data. Common to all these scenarios is a fixed architecture and object definition hierarchy, with well-understood operations that span data definition, processing, and query workloads.

Although objects and workloads are programmable, they are not extensible. Specifically, you cannot create custom data cartridges that retrieve data from unsupported data sources, customize or replace formula or storage engine behaviors, nor can you create new types of object metadata on a server, database, or model.

To further elaborate on the last point about creating new object types: while you cannot create a new type of object, you can create calculated objects built from expressions or code at run time. Not everything in your model needs to be predefined and mapped to an existing data structure. Additionally, you can extend the schema via Annotations in AMO to pass object-specific information to your client application.

## Choose a platform or approach to development
Analysis Services provides many ways to customize a solution through code, but most developers use the managed APIs or script.

- Managed APIs include [AMO and TOM](http://msdn.microsoft.com/library/mt436122.aspx) for data definition and administrative tasks, and [ADOMD.NET](http://msdn.microsoft.com/library/mt465769.aspx) for query support from client code. In SQL Server 2016, AMO is updated to use the new Tabular metadata for models created or upgraded to compatibility level 1200 and higher.

- Script can often achieve the same results as a program executable, with possibly less work.

  - You can write PowerShell script using Analysis Services PowerShell components that call AMO types directly. Within PowerShell, you can also create and execute ASSL/XMLA or TMSL (in JSON) script.

  - ASSL and TMSL are script languages that provide  objects used in discover and execute operations. Which type of script you use depends on the underlying server, database, or model.

  - Tabular models or databases at compatibility level 1200 and higher use the Tabular Model Scripting Language (TMSL), which is in JSON.

  - Multidimensional models and Tabular models at compatibility levels 1050-1103 use Analysis Services Scripting Language (ASSL), which is the Analysis Services extension of the XMLA open standard.

  - You can generate ASSL or TMSL script in Management Studio. You can also use **View Code** in SQL Server Data Tools to view the model definition in ASSL or TMSL.

- Although it is possible to build a solution based on the open standards of XMLA and MDX, it's quite rare to do so. There is no documentation other than XMLA and MDX reference to help you, and most community and forum support draws from experiences with .NET or native (MSOLAP) technologies.

## Programming in Analysis Services
[Data Mining Programming](../analysis-services/data-mining-programming.md)
Describes the approaches building solutions that include data mining objects.

[Multidimensional Model Programming](../analysis-services/multidimensional-models/multidimensional-model-programming.md)
Describes the development tasks and approaches for integrating multidimensional model objects in a custom solution.

[Tabular Model Programming for Compatibility Level 1200 and higher](../analysis-services/tabular-model-programming-compatibility-level-1200/tabular-model-programming-for-compatibility-level-1200.md)
**New in SQL Server 2016**.  Summarizes the interfaces and script languages used for working with Tabular 1200 and higher models programmatically.

[Tabular Model Programming for Compatibility Levels 1050 through 1103](../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/tabular-model-programming-for-compatibility-levels-1050-through-1103.md)
This documentation is intended for developers who support tabular models at earlier compatibility levels. It describes the CSDL extensions that define a tabular model in XML syntax. It also includes information about  tabular object model definitions and syntax.

[Analysis Services Management Objects (AMO)](https://msdn.microsoft.com/library/mt436122.aspx)
Developer reference documentation for the managed provider, Analysis Services Management Objects (AMO), for data definition and administration, including processing.

[ADOMD.NET](http://msdn.microsoft.com/library/mt465769.aspx)
Developer reference documentation for the managed provider, ADOMD.NET, used for programmatic data access and query workloads.

[Analysis Services Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/analysis-services-schema-rowsets)
Describes the schema rowsets that provide information about server state, server operations, and database objects.

[XML for Analysis  &#40;XMLA&#41; Reference](https://docs.microsoft.com/bi-reference/xmla/xml-for-analysis-xmla-reference)
Describes XMLA concepts that can help you understand how XMLA contributes to your custom solution. It also describes the level of compliance with the XMLA 1.1 specification.

[Analysis Services Scripting Language &#40;ASSL for XMLA&#41;](https://docs.microsoft.com/bi-reference/assl/analysis-services-scripting-language-assl-for-xmla)
Describes the ASSL extensions to XMLA. ASSL provides a data definition and manipulation language for Analysis Services multidimensional models that supplements the XMLA specification.

[Tabular Model Scripting Language &#40;TMSL&#41; Reference](https://docs.microsoft.com/bi-reference/tmsl/tabular-model-scripting-language-tmsl-reference)
TMSL is a JSON representation of Tabular models at compatibility level 1200 and higher. Object definitions are based on tabular metadata constructs like table, column, and relationship rather than multidimensional metadata that might be unfamiliar if you are new to Analysis Services data modeling in Tabular mode.

[Analysis Services PowerShell Reference](../analysis-services/powershell/analysis-services-powershell-reference.md)
Documents the cmdlets used for administrative functions, plus the general-purpose **Invoke-ASCmd** cmdlet that accepts any script or query as input.

## See Also
[Technical Reference ](../analysis-services/powershell/technical-reference-ssas.md)
[Query and Expression Language Reference &#40;Analysis Services&#41;](http://msdn.microsoft.com/library/gg492188.aspx)
