---
title: "Workload Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Workload element"
ms.assetid: 68ffd473-6546-4015-98d0-3763165de65c
author: stevestein
ms.author: sstein
manager: craigg
---
# Workload Element (DTA)
  Specifies the workload to be used for a tuning session.  
  
## Syntax  
  
```  
  
<DTAInput>  
    <Server>  
...code removed...  
    <Workload>...</Workload>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once for each `DTAInput` element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)|  
|**Child elements**|[File Element &#40;DTA&#41;](file-element-dta.md)<br /><br /> [Database Element for Workload &#40;DTA&#41;](database-element-for-workload-dta.md)<br /><br /> [EventString Element &#40;DTA&#41;](eventstring-element-dta.md)|  
  
## Remarks  
 A workload is a set of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that execute against a database or databases that you want to tune. The Database Engine Tuning Advisor can use [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, trace files, and trace tables as workloads.  
  
 If you specify a workload in an XML input file and a workload on the command line with the **dta** tool, the workload specified on the command line will be used for tuning. All tuning options specified on the command line override those that are specified in an XML input file. The only exception is if a user-specified configuration is entered in the evaluate mode in the XML input file. For example, if a configuration is entered in the `Configuration` element of the XML input file and the `EvaluateConfiguration` element is also specified as one of the tuning options, the tuning options specified in the XML input file will override any tuning options entered at the command line.  
  
 One workload must be specified for each tuning session.  
  
## Example  
 The following code example specifies the **MyDatabase.MyDBOwner.TuningTable001** trace table for the `Workload` element. The **TuningTable001** was created by using the Tuning template with SQL Server Profiler and saving the trace output as a table.  
  
```  
<DTAXML ...>  
  <DTAInput>  
    <Server>  
...code removed here...  
    </Server>  
    <Workload>  
      <Database>  
        <Name>MyDatabase</Name>  
        <Schema>  
          <Name>MyDBOwner</Name>  
            <Table>  
              <Name>TuningTable001</Name>  
            </Table>  
        </Schema>  
      </Database>  
    </Workload>  
...code removed here...  
  </DTAInput>  
</DTAXML>  
```  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
