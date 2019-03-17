---
title: "Built-in Collections in Expressions (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 78d5e3b8-9320-4e4b-a025-e2de3cf7afa7
author: markingmyname
ms.author: maghan
---
# Built-in Collections in Expressions (Report Builder)
  In an expression in a report, you can include references to the following built-in collections: ReportItems, Parameters, Fields, DataSets, DataSources, Variables, and built-in fields for global information such as the report name. Not all collections appear in the **Expression** dialog box. The DataSets and DataSources collections are available only at run-time for published reports on a report server. The ReportItems collection is collection of text boxes in a report region, for example, the text boxes on a page or in a page header.  
  
 For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="Collections"></a> Understanding Built-in Collections  
 The following table lists the built-in collections available when you write an expression. Each row includes the case-sensitive programmatic name for the collection, whether you can use the Expression dialog box to interactively add a reference to the collection, an example, and a description that includes when the collection values are initialized and available for use.  
  
|Built-in Collection|Category in the Expression dialog box|Example|Description|  
|--------------------------|-------------------------------------------|-------------|-----------------|  
|**Globals**|Built-in Fields|`=Globals.ReportName`<br /><br /> `- or -`<br /><br /> `=Globals.PageNumber`|Represents global variables useful for reports, such as the report name or page number. Always available.<br /><br /> For more information, see [Built-in Globals and Users References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-built-in-globals-and-users-references-report-builder.md).|  
|**User**|Built-in Fields|`=User.UserID`<br /><br /> - or -<br /><br /> `=User.Language`|Represents a collection of data about the user running the report, such as the language setting or the user ID. Always available.<br /><br /> For more information, see [Built-in Globals and Users References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-built-in-globals-and-users-references-report-builder.md).|  
|**Parameters**|Parameters|`=Parameters("ReportMonth").Value`<br /><br /> - or -<br /><br /> `=Parameters!ReportYear.Value`|Represents the collection of report parameters, each of which can be single-value or multivalue. Not available until processing initialization is complete. For more information, see [Parameters Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md).|  
|**Fields(** *\<Dataset>* **)**|Fields|`=Fields!Sales.Value`|Represents the collection of fields of the dataset that are available to the report. Available after data is retrieved from a data source into a dataset. For more information, see [Dataset Fields Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-dataset-fields-collection-references-report-builder.md).|  
|**DataSets**|Not Displayed|`=DataSets("TopEmployees").CommandText`|Represents the collection of datasets referenced from the body of a report definition. Does not include data sources used only in page headers or page footers. Not available in local preview. For more information, see [DataSources and DataSets Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-datasources-and-datasets-references-report-builder.md).|  
|**DataSources**|Not Displayed|`=DataSources("AdventureWorks2012").Type`|Represents the collection of data sources referenced from within the body of a report. Does not include data sources used only in page headers or page footers. Not available in local preview. For more information, see [DataSources and DataSets Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-datasources-and-datasets-references-report-builder.md).|  
|**Variables**|`Variables`|`=Variables!CustomTimeStamp.Value`|Represents the collection of report variables and group variables. For more information, see [Report and Group Variables Collections References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-report-and-group-variables-references-report-builder.md).|  
|**ReportItems**|Not Displayed|`=ReportItems("Textbox1").Value`|Represents the collection of text boxes for a report item. This collection can be used to summarize items on the page for including in a page header or page footer. For more information, see [ReportItems Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-reportitems-collection-references-report-builder.md).|  
  
##  <a name="Syntax"></a> Using Collection Syntax in an Expression  
 To refer to a collection from an expression, use standard [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] syntax for an item in a collection. The following table shows examples of collection syntax.  
  
|Syntax|Example|  
|------------|-------------|  
|*Collection!ObjectName.Property*|`=Fields!Sales.Value`|  
|*Collection!ObjectName("Property")*|`=Fields!Sales("Value")`|  
|*Collection("ObjectName").Property*|`=Fields("Sales").Value`|  
|*Collection("Member")*|`=User("Language")`|  
|*Collection.Member*|`=User.Language`|  
  
## See Also  
 [Add an Expression &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-an-expression-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)  
  
  
