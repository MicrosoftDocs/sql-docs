---
title: "XML Input File Sample with Inline Workload (DTA) | Microsoft Docs"
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
ms.assetid: 7c04fe1d-6669-44a1-8b73-36d469e9b002
author: stevestein
ms.author: sstein
manager: craigg
---
# XML Input File Sample with Inline Workload (DTA)
  Copy and paste this sample of an XML input file that specifies a workload with the **EventString** element into your favorite XML editor or text editor. You can use the **EventString** element to specify a [!INCLUDE[tsql](../../includes/tsql-md.md)] script workload in the XML input file instead of using a separate workload file. After copying this sample into your editing tool, replace the values specified for the **Server**, **Database**, **Schema**, **Table**, **Workload**, **EventString**, and **TuningOptions** elements with those for your specific tuning session. For more information about all of the attributes and child elements that you can use with these elements, see the [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md). The following sample uses only a subset of available attribute and child element options.  
  
## Code  
 [!code-xml[InputFileSamples#InlineWorkloadInputFile](../../snippets/xml/SQL14/dta_xml/inputfilesamples/xml/dta_xml_input_file_samples.xml#inlineworkloadinputfile)]  
  
## Comments  
 `USE database_name` statements can be specified in the inline workload that is contained in the **EventString** element.  
  
## See Also  
 [Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)   
 [View and Work with the Output from the Database Engine Tuning Advisor](../../relational-databases/performance/view-and-work-with-the-output-from-the-database-engine-tuning-advisor.md)   
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
