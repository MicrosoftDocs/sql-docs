---
title: "XML Input File Sample with User-specified Configuration (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sample applications [DTA]"
ms.assetid: b29c9716-e5c3-4003-9efb-3ade2197b630
author: stevestein
ms.author: sstein
manager: craigg
---
# XML Input File Sample with User-specified Configuration (DTA)
  Copy and paste this sample of an XML input file that specifies a user-specified configuration with the **Configuration** element into your favorite XML editor or text editor. This enables you to perform "what-if" analysis. "What-if" analysis involves using the **Configuration** element to specify a set of hypothetical physical design structures for the database you want to tune. Then you use Database Engine Tuning Advisor to analyze the effects of running a workload against this hypothetical configuration to find out whether it improves query processing performance. This type of analysis provides the advantage of evaluating the new configuration without incurring the overhead of actually implementing it. If your hypothetical configuration does not provide the performance improvements you want, it is easy to change it and analyze it again until you reach the configuration that produces the results you need.  
  
 After copying this sample into your editing tool, replace the values specified for the **Server**, **Database**, **Schema**, **Table**, **Workload**, **TuningOptions**, and **Configuration** elements with those for your specific tuning session. For more information about all of the attributes and child elements that you can use with these elements, see the [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md). The following sample uses only a subset of available attribute and child element options.  
  
## Code  
 [!code-xml[InputFileSamples#UserSpecifiedConfigInputFile](../../snippets/xml/SQL14/dta_xml/inputfilesamples/xml/dta_xml_input_file_samples.xml#userspecifiedconfiginputfile)]  
  
## Comments  
  
-   Dropping physical design structures is not supported if you specify the **Absolute** mode for the **Configuration** element (`Configuration SpecificationMode="Absolute"`).  
  
-   You cannot create and drop the same physical design structure in either mode (**Relative** or **Absolute**) of the **Configuration** element.  
  
## See Also  
 [Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)   
 [View and Work with the Output from the Database Engine Tuning Advisor](../../relational-databases/performance/view-and-work-with-the-output-from-the-database-engine-tuning-advisor.md)   
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
