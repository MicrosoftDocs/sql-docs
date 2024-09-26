---
title: "Browse for report parts and set a default folder (Report Builder)"
description: Learn how to add existing report parts, such as tables and charts, to your report from the Report Part Gallery in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Browse for report parts and set a default folder (Report Builder)

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]

The easiest way to create a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report is to add existing report parts, such as tables and charts, to your report from the Report Part Gallery. When you add a report part to your report, you're also adding everything it must have to work. For example, any report part that displays data depends on a dataset, that is, a query and a connection to a data source. After you add the report part to your report, you can modify it as much as you need.  
  
 You can set a default folder to publish report parts to the report server or SharePoint site integrated with a report server.  
  
 For more information, see [Report Parts &#40;Report Builder&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md).  
  
## Browse for report parts  
  
1.  On the **Insert** menu, select **Report Parts**.  
  
     If you aren't already connected, select **Connect to a report server**, and enter the name.  
  
    > [!NOTE]  
    >  You must be connected to a report server to browse for report parts.  
  
1.  You can narrow your search by specifying details about the report part. Enter all or part of the name and description in the **Search** box, or choose **Add Criteria** and add values for any or all of these fields:  
  
    -   Created by  
  
    -   Date created  
  
    -   Date last modified  
  
    -   Last modified by  
  
    -   Server folder  
  
    -   Type  
  
     For example, to find an image, select **Add Criteria**, and then choose **Type**. In the list, select the **Image** box, select Enter, and then select the Search magnifying glass.  
  
    > [!NOTE]  
    >  For the **Created by** and **Last modified by** values, search for the person's user name as the name is represented on the report server.  
  
## Set a default folder for report parts  
  
1.  Select **Report Builder**, and then choose **Options**.  
  
1.  In the **Options** dialog, on the **Settings** tab, type a folder name in the **Publish report parts to this folder by default** textbox.  
  
 Report Builder creates this folder if you have permissions to create folders on the report server and the folder doesn't exist yet.  
  
 You don't need to restart Report Builder for this setting to take effect.  
  
## Related content

- [Report parts &#40;Report Builder&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md)
- [Report parts and datasets in Report Builder](../../reporting-services/report-data/report-parts-and-datasets-in-report-builder.md)
- [Publish and republish report parts &#40;Report Builder&#41;](../../reporting-services/report-design/publish-and-republish-report-parts-report-builder-and-ssrs.md)
