---
title: Issue Element
description: In SQL Server, the Issue element reports an issue found by the ssbdiagnose utility. The XML output file has one Issue element per issue reported.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "issue element"
  - "XML output file format [ssbdiagnose], issue element"
  - "ssbdiagnose"
ms.assetid: 2246a886-686b-44ca-9771-b155cedad8be
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Issue Element (ssbdiagnose)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports an issue that was found by the **ssbdiagnose** utility. The **ssbdiagnose** XML output file has one Issue element per issue reported.  
  
## Syntax  
  
```  
  
<Issue  
    type="..."   
    code="..."   
    server="..."   
    database="..."   
    object="...">   
    ...   
</Issue>  
```  
  
## Element Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|**type**|Identifies which category of problem the Issue element is reporting:<br /><br /> **"Diagnosis"** Reports a configuration issue found when you analyze a [!INCLUDE[ssSB](../../includes/sssb-md.md)] configuration.<br /><br /> **"Problem"** Reports an issue that has prevented **ssbdiagnose** from completing its analysis. Correct the problem and rerun **ssbdiagnose**.<br /><br /> **"Event"** Reports a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] event found when you run a **-RUNTIME** check. Events are only reported if **-SHOWEVENTS** is specified.|  
|**code**|Identifies the error number for the message.|  
|**server**|Identifies the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in which the problem was found. If the problem was in a default instance, the server attribute only has the computer name. If the problem was in a named instance, the server attribute is in the form ComputerName\InstanceName.|  
|**database**|Identifies the name of the database in which the problem was found.|  
|**object**|Identifies the name of the object in which the problem was found. If the problem was an instance or database level issue, the object attribute repeats the instance or database name.|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, length is unlimited.|  
|**Value**|Returns the text of the error message.|  
|**Occurrence**|Once per reported error.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[DiagnosticInformation Element &#40;ssbdiagnose&#41;](../../tools/ssbdiagnose/diagnosticinformation-element-ssbdiagnose.md)|  
|**Child elements**|None|  
  
## Example  
 This element reports an 1102 error for a database that does not have a master key, where the error was found when you analyzed a [!INCLUDE[ssSB](../../includes/sssb-md.md)] configuration.  
  
```  
<Issue type="Diagnosis" code="1102" server="TestComputer" database="TargetDB" object="TargetDB">The master key was not found</diagnostic>  
```  
  
## See Also  
 [ssbdiagnose Utility &#40;Service Broker&#41;](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)  
  
  
