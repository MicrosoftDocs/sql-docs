---
title: "DTAXML Element (DTA)"
description: In the dta utility, the DTAXML element contains all elements that describe tuning input and output that the Database Engine Tuning Advisor generates.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "DTAXML element"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# DTAXML Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The root element of a Database Engine Tuning Advisor XML input or output file, **DTAXML** contains all elements that describe tuning input and the tuning output that Database Engine Tuning Advisor generates.  
  
## Syntax  
  
```  
  
<DTAXML   
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"   
    xmlns="https://schemas.microsoft.com/sqlserver/2004/07/dta">  
    ...code removed here...  
</DTAXML>  
```  
  
## Element Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|**xmlns:xsi**|Required. Identifies the XML Schema Instance namespace. Attributes from this namespace are used to reference the schema that is used to validate the Database Engine Tuning Advisor XML file.<br /><br /> Required value: [http://www.w3.org/2001/XMLSchema-instance](http://www.w3.org/2001/XMLSchema-instance)|  
|**xmlns**|Required. Identifies the Database Engine Tuning Advisor namespace.<br /><br /> If you edit the Database Engine Tuning Advisor XML file using the XML editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], this value is used by F1 Help and Dynamic Help to locate possible reference topics in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.<br /><br /> Required value:<br /><br /> [Database Engine Tuning Advisor XML Schema](https://go.microsoft.com/fwlink/?LinkId=43100) Namespace|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once per DTA XML file.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|None|  
|**Child elements**|[DTAInput Element &#40;DTA&#41;](../../tools/dta/dtainput-element-dta.md)<br /><br /> **DTAOutput** Element (see [Database Engine Tuning Advisor XML schema](https://schemas.microsoft.com/sqlserver/) for information)|  
  
## Remarks  
 For more information about XML namespaces, see [Namespaces in an XML Document](/dotnet/standard/data/xml/managing-namespaces-in-an-xml-document). 
  
## Example  
 For examples of typical **DTAXML** elements, see [XML Input File Samples &#40;DTA&#41;](../../tools/dta/xml-input-file-samples-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)   
 [Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)  
  
