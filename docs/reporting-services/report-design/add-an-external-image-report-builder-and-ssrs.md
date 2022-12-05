---
title: "Add an external image to a paginated report | Microsoft Docs"
description: Learn how to add an image to your paginated report from an external source with appropriate verification and permissions in Report Builder. 
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 81fd4a1f-79a9-4967-86d6-6229413c0995
author: maggiesMSFT
ms.author: maggies
---
# Add an external image to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

External images can be on a report server in native mode or any other Web site, including a report server in SharePoint integrated mode (SQL Server 2016 or before). When you include external images in your paginated report, you must verify that the image exists and that the report reader has permissions to access the image. For more information, see [Images &#40;Report Builder&#41;](../../reporting-services/report-design/images-report-builder-and-ssrs.md).  
 
## To add an external image  
  
1.  In report design view, on the **Insert** tab, click **Image**.  
  
2.  On the design surface, click and then drag a box to the desired size of the image.  
  
3.  On the **General** tab of the **Image Properties** dialog box, type a name in the **Name** text box or accept the default.  
  
4.  (Optional) In the **Tooltip** text box, type text to display when the user hovers over the image in a report rendered for HTML.  
  
5.  In **Select the image source**, select **External**.  
  
    For an image on a report server in native mode, type a relative path to the image in the **Use this image** box-for example, ../images/image1.jpg.  
  
    For an image on any Web site (including a report server in SharePoint integrated mode), type a full URL to the image in the **Use this image** box: for example, `https://\<SharePointservername>/\<site>/Documents/images/image1.jpg`.  
  
    For more information, see [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
6.  (Optional) Click **Size**, **Visibility**, **Action**, or **Border** to set additional properties for the image report item.  
  
7.  Select **OK**.
  
## See Also  
 [Embed an Image in a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/embed-an-image-in-a-report-report-builder-and-ssrs.md)   
 [Add a Background Image &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-background-image-report-builder-and-ssrs.md)   
 [Image Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](./images-report-builder-and-ssrs.md)  
  
