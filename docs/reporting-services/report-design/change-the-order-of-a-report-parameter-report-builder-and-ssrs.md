---
title: "Change the order of a paginated report parameter"
description: Learn how to change the order of paginated report parameters when you have a dependent parameter that is listed out of order in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Change the order of a paginated report parameter (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Change the order of paginated report parameters when you have a dependent parameter that is listed before the parameter it's dependent on. Parameter order is important when you have cascading parameters, or when you want to show users the default value for one parameter before they choose values for other parameters. A dependent report parameter contains a reference, in either its default values query or valid values query, to a query parameter that points to a report parameter that is after it in the parameter list in the **Report Data** pane.  
  
 The order that you see parameters display on the report viewer toolbar when you run the report, is determined by the order of the parameters in the **Report Data** pane and the location of the parameters in the custom parameters pane. For more information, see [Customize the Parameters pane in a report &#40;Report Builder&#41;](../../reporting-services/report-design/customize-the-parameters-pane-in-a-report-report-builder.md)  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Change the order of report parameters  
  
You can change the order of report parameters by doing either of the following actions:  
  
-   Select a parameter in the **Report Data** pane, and use the up and down arrow buttons to move the parameter higher or lower in the list, as shown in the following image. When you change the order of the parameter in the **Report Data** pane, the location of the parameter in the parameters pane is changed.  
  
     :::image type="content" source="../../reporting-services/report-design/media/ssrs-changeorderofparameters-reportdata.png" alt-text="Screenshot of where you change the order of the parameters in the Report Data pane" lightbox="../../reporting-services/report-design/media/ssrs-changeorderofparameters-reportdata.png":::
  
-   In the parameters pane, drag the parameter to a new column or row in the pane. When you change the location of the parameter in the pane, the parameter order changes in the **Report Data** pane. For more information about moving parameters in the pane, see [Customize the Parameters pane in a Report &#40;Report Builder&#41;](../../reporting-services/report-design/customize-the-parameters-pane-in-a-report-report-builder.md).  
  
## Related content

- [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)
- [Add cascading parameters to a report &#40;Report Builder&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)
- [Tutorial: Add a parameter to your report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)
- [Add dataset filters, data region filters, and group filters &#40;Report Builder&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)
- [Parameters collection references &#40;Report Builder&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)
