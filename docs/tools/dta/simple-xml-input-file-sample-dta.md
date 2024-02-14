---
title: "Simple XML Input File Sample (DTA)"
description: This article contains a sample XML input file to use for tuning workloads to use with Database Engine Tuning Advisor.
author: markingmyname
ms.author: maghan
ms.date: 03/01/2017
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
helpviewer_keywords:
  - "sample applications [DTA]"
dev_langs:
  - "XML"
---

# Simple XML Input File Sample (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Copy and paste this sample of a simple XML input file to use for tuning workloads into your favorite XML editor or text editor. Then replace the values specified for the **Server**, **Database**, **Schema**, **Table**, **Workload**, and **TuningOptions** elements with those for your specific tuning session. For more information about the attributes and child elements that you can use with these elements, see the [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md). The following sample uses only a subset of available attribute and child element options.  
  
## Code  
 [!code-xml[InputFileSamples#SimpleXMLInputFile](../../tools/dta/codesnippet/xml/simple-xml-input-file-sa_1.xml)]  
  
## See Also  
 [Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)   
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
