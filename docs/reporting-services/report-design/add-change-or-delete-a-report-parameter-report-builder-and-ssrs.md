---
title: "Add, change, or delete a paginated report parameter"
description: Choose report data, connect related reports, and vary the report presentation with the addition of report parameters in a paginated report in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add, change, or delete a paginated report parameter (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  A paginated report parameter provides a way to choose report data, connect related reports together, and vary the report presentation. You can provide a default value and a list of available values, and the user can change the selection.  
  
 After you publish a report, you can change the default values, the available values, and other properties for a report parameter on the report server. You can provide multiple sets of default parameter values by creating linked reports. For more information, see [Report parameters &#40;Report Builder&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
 This article is about adding report parameters to a paginated report in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] or Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You can also add report parameters to mobile reports in  [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)]. For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]
>
> However, SQL Server Mobile Report Publisher is deprecated for all releases of SQL Server Reporting Services after SQL Server Reporting Services 2019.
  
### Add or edit a report parameter  
  
1.  In [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] or Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in the **Report Data** pane, right-click the **Parameters** node and select **Add Parameter**. The **Report Parameter Properties** dialog opens.  
  
1.  In **Name**, enter the name of the parameter or accept the default name.  
  
1.  In **Prompt**, enter the text that appears next to the parameter text box when the user runs the report.  
  
1.  In **Data type**, select the data type for the parameter value.  
  
1.  If the parameter can contain a blank value, select **Allow blank value**.  
  
1.  If the parameter can contain a null value, select **Allow null value**.  
  
1.  To allow a user to select more than one value for the parameter, select **Allow multiple values**.  
  
1.  Set the visibility option.  
  
    -   To show the parameter on the toolbar at the top of the report, select **Visible**.  
  
    -   To hide the parameter so that it doesn't display on the toolbar, select **Hidden**.  
  
    -   To hide the parameter and protect it from being modified on the report server after the report is published, select **Internal**. The report parameter can then only be viewed in the report definition. For this option, you must set a default value or allow the parameter to accept a null value.  
  
1. Select **OK**.
  
### Delete a report parameter  
  
1.  In the **Report Data** pane, expand the **Parameters** node.  
  
1.  Right-click the report parameter and select **Delete**.  
  
## Related content

- [Add, change, or delete available values for a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-available-values-for-a-report-parameter.md)
- [Add, change, or delete default values for a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-default-values-for-a-report-parameter.md)
- [Change the order of a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)
- [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)
- [Add cascading parameters to a report &#40;Report Builder&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)
- [Tutorial: Add a parameter to your report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)
- [Add dataset filters, data region filters, and group filters &#40;Report Builder&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)
- [Parameters collection references &#40;Report Builder&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)
- [Add a multi-value parameter to a report](../../reporting-services/report-design/add-a-multi-value-parameter-to-a-report.md)
