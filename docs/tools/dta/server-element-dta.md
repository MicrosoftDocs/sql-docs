---
title: "Server Element (DTA)"
description: In the dta utility, the Server element contains the identifying information for the server on which the databases reside that you want to tune.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Server element"
ms.assetid: 9fe0bfb4-3aa6-4eb2-a83e-c0d0e7d4e9f6
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Server Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains the identifying information for the server on which the databases reside that you want to tune.  
  
## Syntax  
  
```  
  
<DTAInput>  
    <Server>  
    ...code removed here...  
    </Server>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once per **DTAInput** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[DTAInput Element &#40;DTA&#41;](../../tools/dta/dtainput-element-dta.md)|  
|**Child elements**|[Name Element for Server &#40;DTA&#41;](../../tools/dta/name-element-for-server-dta.md)<br /><br /> [Database Element for Server &#40;DTA&#41;](../../tools/dta/database-element-for-server-dta.md)|  
  
## Remarks  
 You can specify only one **Server** element for the **DTAInput** element. This element is of the **ServerDetailsTypecomplexType** name in the DTA XML schema. Do not confuse this **Server** element with the one that is the child of the **Configuration** element. For more information, see [Server Element for Configuration &#40;DTA&#41;](../../tools/dta/server-element-for-configuration-dta.md).  
  
## Example  
 The following example shows how to specify the **Sales.SalesPerson** table in the **AdventureWorks** database on SERVER001:  
  
```xml  
<Server>  
  <Name>SERVER001</Name>  
  <Database>  
    <Name>AdventureWorks</Name>  
    <Schema>  
      <Name>Sales</Name>  
      <Table>  
        <Name>SalesPerson</Name>  
      </Table>  
    </Schema>  
  </Database>  
</Server  
```  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
