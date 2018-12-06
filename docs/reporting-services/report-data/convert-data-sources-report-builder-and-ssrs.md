---
title: "Convert Data Sources (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [Reporting Services], embedded"
  - "data sources [Reporting Services], shared"
ms.assetid: 0e099c7e-8c03-43eb-9ea3-76e52d9ebbe3
author: markingmyname
ms.author: maghan
---
# Convert Data Sources (Report Builder and SSRS)
  Each data source in the Report Data pane is embedded and specific to the report or is shared. In Report Builder, a shared data source points to a published shared data source on a report server or SharePoint site. In Report Designer, a shared data source points to a shared data source in the **Shared Data Sources** folder in Solution Explorer.  
  
 For more information about the differences between embedded and shared data sources, see [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/f417782c-b85a-4c4d-8a40-839176daba56).  
  
 For more information on how to create a shared data source, see [Create an Embedded or Shared Data Source &#40;SSRS&#41;](https://msdn.microsoft.com/library/b111a8d0-a60d-4c8b-b00a-51644b19c34b).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Report Designer  
  
#### To convert a data source from embedded to shared  
  
-   In the Report Data pane, right-click the data source, and then click **Convert to Shared Data Source**.  
  
    > [!NOTE]  
    >  If the Report Data pane is not visible, on the **View** menu, click **Report Data**. If the pane opens as a floating window, you can dock it. For more information, see [Dock the Report Data Pane in Report Designer &#40;SSRS&#41;](../../reporting-services/tools/dock-the-report-data-pane-in-report-designer-ssrs.md).  
  
     In the Report Data pane, the data source icon changes to the shared data source icon. In Solution Explorer, a shared data source with the same name appears under the **Shared Data Source** folder.  
  
### To convert a data source from shared to embedded  
  
-   In the Report Data pane, right-click the data source, open the **Data Source Properties** dialog box, and then click **Embedded Connection**. Enter the required information.  
  
     In the Report Data pane, the data source icon changes to the shared data source icon.  
  
## Report Builder  
  
#### To convert a data source from embedded to shared  
  
-   In the Report Data pane, right-click the data source to open the **Data Source Properties** dialog box, and then click **Embedded Connection**. Enter the required information.  
  
     In the Report Data pane, the data source icon changes to the shared data source icon.  
  
#### To convert a data source from shared to embedded  
  
-   In the Report Data pane, right-click the data source, open the **Data Source Properties** dialog box, and then click **Embedded Connection**. Enter the required information.  
  
     In the Report Data pane, the data source icon changes to the shared data source icon.  
  
## See Also  
 [Manage Report Data Sources](../../reporting-services/report-data/manage-report-data-sources.md)   
 [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)  
  
  
