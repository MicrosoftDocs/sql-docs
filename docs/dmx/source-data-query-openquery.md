---
title: "OPENQUERY (DMX)"
description: "&lt;source data query&gt; - OPENQUERY"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# &lt;source data query&gt; - OPENQUERY
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Replaces the source data query with a query to an existing data source. The INSERT, SELECT FROM PREDICTION JOIN, and SELECT FROM NATURAL PREDICTION JOIN statements support **OPENQUERY**.  
  
## Syntax  
  
```  
  
OPENQUERY(<named datasource>, <query syntax>)  
```  
  
## Arguments  
 *named datasource*  
 A data source that exists on the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 *query syntax*  
 A query syntax that returns a rowset.  
  
## Remarks  
 **OPENQUERY** provides a more secure way to access external data by supporting data source permissions. Because the connection string is stored in the data source, administrators can use the properties of the data source to manage access to the data. For more information about data sources, see [Supported Data Sources &#40;SSAS - Multidimensional&#41;](/analysis-services/multidimensional-models/supported-data-sources-ssas-multidimensional).  
  
 You can get a list of the data sources that are available on a server by querying the **MDSCHEMA_INPUT_DATASOURCES** schema rowset. For more information about using **MDSCHEMA_INPUT_DATASOURCES**, see [MDSCHEMA_INPUT_DATASOURCES Rowset](/previous-versions/sql/sql-server-2012/ms126243(v=sql.110)).  
  
 You can also return a list of data sources in the current Analysis Services database by using the following DMX query:  
  
 `SELECT * FROM $system.MDSCHEMA_INPUT_DATASOURCES`  
  
## Examples  
 The following example uses the MyDS data source already defined in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database to create a connection to the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database and query the **vTargetMail** view.  
  
```  
OPENQUERY (MyDS,'SELECT TOP 1000 * FROM vTargetMail')  
```  
  
## See Also  
 [&#60;source data query&#62;](../dmx/source-data-query.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
