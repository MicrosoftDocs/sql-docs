---
title: "Report Parts and Datasets in Report Builder | Microsoft Docs"
ms.date: 09/16/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: 1fe86481-9c41-4535-a4b7-c7c4d780cab6
author: markingmyname
ms.author: maghan
---
# Report Parts and Datasets in Report Builder
  In Report Builder, the easiest way to include data in a report is to add report parts from the Report Part Gallery. Report parts contain the datasets that they depend on, which are known as *dependent datasets*. Dependent datasets are based on shared data sources and can be either embedded datasets or shared datasets. Read more about [Report Parts](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md).  
  
 Another easy way to include data in a report is to use a shared dataset. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="Adding"></a> Adding a Report Part with Dependent Datasets to Your Report  
 When you add a report part to your report, the dependent datasets that it contains are also added to your report. Because a report part might include a rectangle that contains many other report items, it can add multiple dependent datasets to your report. Each shared dataset is an independent reference; the shared data source that it depends on is not added to your report. Each embedded dataset also adds the embedded or shared data source that it depends on.  
  
 The credentials for an embedded data source are not saved as part of the report part. If an embedded data source is added to your report, you will be prompted for credentials when you run the report. To avoid being prompted for credentials, use report parts that are based on shared data sources with stored credentials.  
  
 After you add a report part to your report, the added datasets are no different than embedded or shared datasets that you create. You can view the additional datasets in the Report Data pane. Embedded datasets appear under the corresponding shared data source and shared datasets appear under the Shared Datasets folder.  
  
##  <a name="Customizing"></a> Customizing Dependent Datasets  
 After you add report parts to your report, you might preview it and decide to make some changes to the data. What you can change depends on the type of dataset that you are working with.  
  
 To change data and data options for an embedded dataset, you can edit the dataset properties, including the query, just as if you had created the dataset yourself.  
  
 To change a data and data options for a shared dataset, you can change the shared dataset definition on the report server only you have sufficient permissions. You can also customize the instance of the shared dataset in your report by adding filters, adding calculated fields, and changing data options such as case sensitivity. For more information, see [Embedded and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/embedded-and-shared-datasets-report-builder-and-ssrs.md).  
  
 For more information about how to change the definition of a shared dataset or how to show the latest data changes for a shared dataset in your report, see [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md) and [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).  
  
##  <a name="Publishing"></a> Publishing Dependent Datasets as Shared Datasets  
 When you publish a report item that has dependent datasets, you have the option to publish each dataset as a shared dataset or as an embedded dataset that remains embedded in the report item.  
  
 When you select the shared dataset option, the dataset is saved to the report server as a shared dataset definition. In your report, every report item that uses that dataset is updated to point to the shared dataset that is now on the report server. Two things happen as a result:  
  
1.  In the Publish dialog box, each shared dataset that has been published is removed from the list of items that are available to publish.  
  
2.  When you exit Report Builder or start a new report, you are prompted to save your report. If you do not save your report, the next time you open this report and publish report items, you might publish new copies of the same datasets. To prevent saving multiple copies of shared datasets to the report server, we recommend that you save the report.  
  
> [!IMPORTANT]  
>  To ensure that you and others can continue to successfully use data from a shared dataset, you must understand the principles behind securing report items. For more information, see [Secure Shared Dataset Items](../../reporting-services/security/secure-shared-dataset-items.md).  
  
## See Also  
 [Report Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/report-design-view-report-builder.md)   
 [Security &#40;Report Builder&#41;](../../reporting-services/report-builder/security-report-builder.md)   
 [Report Parts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
