---
title: "OPENROWSET (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# &lt;source data query&gt; - OPENROWSET
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Replaces the source data query with a query to an external provider. The INSERT, SELECT FROM PREDICTION JOIN, and SELECT FROM NATURAL PREDICTION JOIN statements support **OPENROWSET**.  
  
## Syntax  
  
```  
  
OPENROWSET(provider_name,provider_string,query_syntax)  
```  
  
## Arguments  
 *provider_name*  
 An OLE DB provider name.  
  
 *provider_string*  
 The OLE DB connection string for the specified provider.  
  
 *query_syntax*  
 A query syntax that returns a rowset.  
  
## Remarks  
 The data mining provider will establish a connection to the data source object by using *provider_name* and *provider_string,* and will execute the query specified in *query_syntax* to retrieve the rowset from the source data.  
  
## Examples  
 The following example can be used within a PREDICTION JOIN statement to retrieve data from the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database by using a [!INCLUDE[tsql](../includes/tsql-md.md)] SELECT statement.  
  
```  
OPENROWSET  
(  
'SQLOLEDB.1',  
'Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security     Info=False;Initial Catalog=AdventureWorksDW2012;Data Source=localhost',  
'SELECT TOP 1000 * FROM vTargetMail'  
)  
```  
  
## See Also  
 [&#60;source data query&#62;](../dmx/source-data-query.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
