---
title: "SHAPE (DMX)"
description: "&lt;source data query&gt; - SHAPE"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# &lt;source data query&gt; - SHAPE
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Combines queries from multiple data sources into a single hierarchical table (that is, a table with nested tables), which becomes the case table for the mining model.  
  
 The complete syntax of the **SHAPE** command is documented in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Data Access Components (MDAC) Software Development Kit (SDK).  
  
## Syntax  
  
```  
  
SHAPE {<primary query>}  
APPEND ({ <child table query> }   
     RELATE <primary column> TO <child column>)   
          AS <column table name>  
[  
     ({ <child table query> }   
     RELATE <primary column> TO <child column>)   
          AS < column table name>  
...  
]       
```  
  
## Arguments  
 *primary query*  
 The query returning the parent table.  
  
 *child table query*  
 The query returning the nested table.  
  
 *primary column*  
 The column in the parent table to identify child rows from the result of a child table query.  
  
 *child column*  
 The column in the child table to identify the parent row from the result of a primary query.  
  
 *column table name*  
 The newly appended column name in the parent table for the nested table.  
  
## Remarks  
 You must order the queries by the column that relates the parent table and child table.  
  
## Examples  
 You can use the following example within an [INSERT INTO &#40;DMX&#41;](../dmx/insert-into-dmx.md) statement to train a model containing a nested table. The two tables within the **SHAPE** statement are related through the **OrderNumber** column.  
  
```  
SHAPE {  
    OPENQUERY([Adventure Works DW Multidimensional 2012],'SELECT OrderNumber  
    FROM vAssocSeqOrders ORDER BY OrderNumber')  
} APPEND (  
    {OPENQUERY([Adventure Works DW Multidimensional 2012],'SELECT OrderNumber, model FROM   
    dbo.vAssocSeqLineItems ORDER BY OrderNumber, Model')}  
  RELATE OrderNumber to OrderNumber)   
```  
  
## See Also  
 [&#60;source data query&#62;](../dmx/source-data-query.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
