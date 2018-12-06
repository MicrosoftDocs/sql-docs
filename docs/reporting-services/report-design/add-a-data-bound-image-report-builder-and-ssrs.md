---
title: "Add a Data-Bound Image (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: df4c38d4-bfcc-41c4-aa6d-952ca6fd7a2e
author: maggiesMSFT
ms.author: maggies
---
# Add a Data-Bound Image (Report Builder and SSRS)
  A report can include a reference to an image that is stored in a database. Such an image is known as a *data-bound image*. The pictures that appear alongside product names in a product list are examples of data-bound images.  
  
 Adding a data-bound image to a page header or page footer requires additional steps. For more information, see [Page Headers and Footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a data-bound image  
  
1.  In report design view, create a table with a data source connection and a dataset with a field that contains binary image data. For more information, see [Tables &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md).  
  
2.  Insert a column in your table. For more information, see [Insert or Delete a Column &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/insert-or-delete-a-column-report-builder-and-ssrs.md).  
  
3.  On the **Insert** menu, click **Image**, and then click in the data row of the new column.  
  
4.  On the General page of the **Image Properties** dialog box, type a name in the **Name** text box or accept the default.  
  
5.  (Optional) In the **Tooltip** text box, type text to display when the user hovers over the image in the report rendered for HTML.  
  
6.  In **Select the image source**, select **Database**.  
  
7.  In **Use this Field**, select the field that contains images in your report.  
  
8.  In **Use this MIME type**, select the MIME type, or file format, of the image-for example, bmp.  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     An image placeholder appears on the report design surface.  
  
## See Also  
 [Images &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/images-report-builder-and-ssrs.md)   
 [Embed an Image in a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/embed-an-image-in-a-report-report-builder-and-ssrs.md)   
 [Add an External Image &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-an-external-image-report-builder-and-ssrs.md)   
 [Image Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/c2218b93-f7fe-46ef-995f-d7dadf9752ec)  
  
  
