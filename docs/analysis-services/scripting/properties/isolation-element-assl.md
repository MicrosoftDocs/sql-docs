---
title: "Isolation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Isolation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "Isolation element"
ms.assetid: 28c98c6f-668e-4547-8d25-127cc3995a7d
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Isolation Element (ASSL)
  Indicates the isolation level for an element that is derived from the [DataSource](../../../analysis-services/scripting/data-type/datasource-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   ...  
   <Isolation>...</Isolation>  
   ...  
</DataSource>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*ReadCommitted*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataSource](../../../analysis-services/scripting/data-type/datasource-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value for this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ReadCommitted*|Specifies that statements cannot read data that has been modified but not committed by other transactions. This prevents dirty reads. Other transactions can change data between individual statements within the current transaction. This results in nonrepeatable reads or phantom data. This value is the default for the **Isolation** element.|  
|*Snapshot*|Specifies that the data that is read by any statement in a transaction will be the transactionally consistent version of the data that existed at the start of the transaction. The transaction can only see data modifications that were committed before the start of the transaction. Data modifications that are made by other transactions after the start of the current transaction are not visible to statements that are executing in the current transaction. The effect is as if the statements in a transaction get a snapshot of the committed data as it existed at the start of the transaction.|  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  