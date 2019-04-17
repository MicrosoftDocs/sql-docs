---
title: "Add, Change, or Delete a Report Parameter (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: d44a8e0a-10cf-4502-9391-09743ffc9bad
author: markingmyname
ms.author: maghan
---
# Add, Change, or Delete a Report Parameter (Report Builder and SSRS)
  A report parameter provides a way to choose report data, connect related reports together, and vary the report presentation. You can provide a default value and a list of available values, and the user can change the selection.  
  
 After you publish a report, you can change the default values, the available values, and other properties for a report parameter on the report server. You can provide multiple sets of default parameter values by creating linked reports. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
 This article is about adding report parameters to a paginated report in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] or Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You can also add report parameters to mobile reports in  [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)]. See [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md) for more information.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add or edit a report parameter  
  
1.  In [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] or Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in the **Report Data** pane, right-click the **Parameters** node and click **Add Parameter**. The **Report Parameter Properties** dialog box opens.  
  
2.  In **Name**, type the name of the parameter or accept the default name.  
  
3.  In **Prompt**, type the text that appears next to the parameter text box when the user runs the report.  
  
4.  In **Data type**, select the data type for the parameter value.  
  
5.  If the parameter can contain a blank value, select **Allow blank value**.  
  
6.  If the parameter can contain a null value, select **Allow null value**.  
  
7.  To allow a user to select more than one value for the parameter, select **Allow multiple values**.  
  
8.  Set the visibility option.  
  
    -   To show the parameter on the toolbar at the top of the report, select **Visible**.  
  
    -   To hide the parameter so that it does not display on the toolbar, select **Hidden**.  
  
    -   To hide the parameter and protect it from being modified on the report server after the report is published, select **Internal**. The report parameter can then only be viewed in the report definition. For this option, you must set a default value or allow the parameter to accept a null value.  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To delete a report parameter  
  
1.  In the **Report Data** pane, expand the **Parameters** node.  
  
2.  Right-click the report parameter and click **Delete**.  
  
## See Also  
 [Add, Change, or Delete Available Values for a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-change-or-delete-available-values-for-a-report-parameter.md)   
 [Add, Change, or Delete Default Values for a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-change-or-delete-default-values-for-a-report-parameter.md)   
 [Change the Order of a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)   
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
 [Add Cascading Parameters to a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)   
 [Tutorial: Add a Parameter to Your Report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)   
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Parameters Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)   
 [Add a multi-value parameter to a Report](../../reporting-services/report-design/add-a-multi-value-parameter-to-a-report.md)  
  
  
