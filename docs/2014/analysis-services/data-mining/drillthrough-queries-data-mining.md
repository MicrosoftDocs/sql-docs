---
title: "Drillthrough Queries (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "AllowDrillThrough property"
  - "drillthrough [Analysis Services]"
  - "drillthrough [DMX]"
ms.assetid: 246c784b-1b0c-4f0b-96f7-3af265e67051
author: minewiskan
ms.author: owend
manager: craigg
---
# Drillthrough Queries (Data Mining)
  A *drillthrough query* lets you retrieve details from the underlying cases or structure data, by sending a query to the mining model. Drillthrough is useful if you want to view the cases that were used to train the model, versus the cases that are used to test the model, or if you want to see additional details from the case data.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Data Mining provides two different options for drillthrough:  
  
-   Drilling through to the **model cases**  
  
     Drillthrough to model cases is used when you want to go from a specific pattern in the model-such as a cluster or branch of a decision tree-and view details about the individual cases.  
  
-   Drilling through to the **structure cases**  
  
     Drillthrough to structure cases is used when the structure contains information that might not be available in the model. For example, you would not use customer contact information in a clustering model, even if the data was included in the structure. However, after you create the model, you might want to retrieve contact information for customers who are grouped into a particular cluster.  
  
 This section provides examples of how you can create these queries.  
  
 [Using Drillthrough in Data Mining Designer](#bkmk_Designer)  
  
 [Creating Drillthrough Queries using DMX](#bkmk_DMX)  
  
 [Considerations When Using Drillthrough](#bkmk_Considerations)  
  
-   [Security Issues](#bkmk_Security)  
  
-   [Limitations](#bkmk_Limits)  
  
##  <a name="bkmk_Designer"></a> Using Drillthrough in Data Mining Designer  
 If a mining model has been configured to allow drillthrough, and if you have the appropriate permissions, when you browse the model, you can click on a node in the appropriate viewer and retrieve detailed information about the cases in that particular node.  
  
 [Drill Through to Case Data from a Mining Model](drill-through-to-case-data-from-a-mining-model.md).  
  
 If the training cases were cached when you processed the mining structure, and you have the necessary permissions, you can return information from the model cases and from the mining structure, including columns that were not included in the mining model.  
  
##  <a name="bkmk_DMX"></a> Creating Drillthrough Queries using DMX  
 You can drill through to case data by creating a DMX query, if you have permissions on the model or on the structure. For examples of the syntax for creating drillthrough queries in DMX, see the following topic:  
  
 [Create Drillthrough Queries using DMX](create-drillthrough-queries-using-dmx.md)  
  
##  <a name="bkmk_Considerations"></a> Considerations When Using Drillthrough  
  
-   If you use the Data Mining Wizard, the option to enable drillthrough to the model cases is on the final page of the wizard. Drillthrough is disabled by default. For more information, see [Completing the Wizard &#40;Data Mining Wizard&#41;](../completing-the-wizard-data-mining-wizard.md).  
  
-   You can add the ability to drill through on an existing mining model, but if you do, the model must be reprocessed before you can drill through to the data.  
  
-   Drillthrough works by retrieving information about the training cases that was cached when you processed the mining structure. Therefore, if you cleared the cached data after processing the structure by changing the <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> property to `ClearAfterProcessing`, drillthrough will not work. To enable drillthrough to structure columns, you must change the <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> property to `KeepTrainingCases` and then reprocess the structure.  
  
-   If the mining structure does not allow drillthrough but the mining model does, you can view information only from the model cases, and not from the mining structure.  
  
###  <a name="bkmk_Security"></a> Security Issues for Drillthrough  
 If you want to drill through to structure cases from the model, you must verify that both the mining structure and the mining model have the [AllowDrillThrough](https://docs.microsoft.com/bi-reference/assl/properties/allowdrillthrough-element-assl) property set to `True`. Moreover, you must be a member of a role that has drillthrough permissions on both the structure and the model. For information about how to create roles, see [Role Designer &#40;Analysis Services - Multidimensional Data&#41;](https://msdn.microsoft.com/library/ms189696(v=sql.120).aspx). see.  
  
 Drillthrough permissions are set separately on the structure and model. The model permission lets you drill through from the model, even if you do not have permissions on the structure. Drillthrough permissions on the structure provide the additional ability to include structure columns in drillthrough queries from the model, by using the [StructureColumn &#40;DMX&#41;](/sql/dmx/structurecolumn-dmx) function.  
  
> [!NOTE]  
>  If you enable drillthrough on both the mining structure and the mining model, any user who is a member of a role that has drillthrough permissions on the mining model can also view columns in the mining structure, even if those columns are not included in the mining model. Therefore, to protect sensitive data, you should set up the data source view to mask personal information, and allow drillthrough access on the mining structure only when necessary.  
  
###  <a name="bkmk_Limits"></a> Limitations on Drillthrough  
  
-   The following limitations apply to drillthrough operations on a model, depending on the algorithm that was used to create the model:  
  
|Algorithm name|Issue|  
|--------------------|-----------|  
|Microsoft Naïve Bayes algorithm|Not supported. These algorithms do not assign cases to specific nodes in the content.|  
|Microsoft Neural Network algorithm|Not supported. These algorithms do not assign cases to specific nodes in the content.|  
|Microsoft Logistic Regression algorithm|Not supported. These algorithms do not assign cases to specific nodes in the content.|  
|Microsoft Linear Regression algorithm|Supported. However, because the model creates a single node, `All`, drilling through returns all the training cases for the model. If the training set is large, loading the results may take a very long time.|  
|Microsoft Time Series algorithm|Supported. However, you cannot drill through to structure or case data by using the **Mining Model Viewer** in Data Mining Designer. You must create a DMX query instead.<br /><br /> Also, you cannot drill through to specific nodes, or write a DMX query to retrieve cases in specific nodes of a time series model. You can retrieve case data from either the model or the structure by using other criteria, such as date or attribute values.<br /><br /> You can also return the dates from the cases in the model, by using the [Lag &#40;DMX&#41;](/sql/dmx/lag-dmx) function.<br /><br /> If you wish to view details of the ARTXP and ARIMA nodes created by the Microsoft Time Series algorithm, you can use the [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).|  
  
##  <a name="bkmk_Tasks"></a> Related Tasks  
 Use the following links to work with drillthrough in specific scenarios.  
  
|Task|Link|  
|----------|----------|  
|Procedure describing use of drillthrough in the Data Mining Designer|[Drill Through to Case Data from a Mining Model](drill-through-to-case-data-from-a-mining-model.md)|  
|To alter an existing mining model to allow drillthrough|[Enable Drillthrough for a Mining Model](enable-drillthrough-for-a-mining-model.md)|  
|Enabling drillthrough on a mining structure by using the DMX WITH DRILLTHROUGH clause|[CREATE MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/create-mining-structure-dmx)|  
|For information about assigning permissions that apply to drillthrough on mining structures and mining models|[Grant permissions on data mining structures and models &#40;Analysis Services&#41;](../multidimensional-models/grant-permissions-on-data-mining-structures-and-models-analysis-services.md)|  
  
## See Also  
 [Data Mining Model Viewers](data-mining-model-viewers.md)   
 [Data Mining Queries](data-mining-queries.md)  
  
  
