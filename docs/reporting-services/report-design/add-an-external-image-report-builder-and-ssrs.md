---
title: "Add an external image to a paginated report"
description: Learn how to add an image to your paginated report from an external source with appropriate verification and permissions in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add an external image to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

External images can be on a report server in native mode or any other web site, including a report server in SharePoint integrated mode (SQL Server 2016 or prior). When you include external images in your paginated report, you must verify that the image exists and that the report reader has permissions to access the image. For more information, see [Images &#40;Report Builder&#41;](../../reporting-services/report-design/images-report-builder-and-ssrs.md).  
 
## Add an external image  
  
1.  In report design view, on the **Insert** tab, select **Image**.  
  
1.  On the design surface, select and then drag a box to the desired size of the image.  
  
1.  On the **General** tab of the **Image Properties** dialog, enter a name in the **Name** text box or accept the default.  
  
1.  (Optional) In the **Tooltip** text box, type text to display when the user hovers over the image in a report rendered for HTML.  
  
1.  In **Select the image source**, select **External**.  
  
    For an image on a report server in native mode, enter a relative path to the image in the **Use this image** box. For example, use `../images/image1.jpg`.  
  
    For an image on any web site (including a report server in SharePoint integrated mode), enter a full URL to the image in the **Use this image** box. For example, use `https://\<SharePointservername>/\<site>/Documents/images/image1.jpg`.  
  
    For more information, see [Specify paths to external items &#40;Report Builder&#41;](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
1.  (Optional) Select **Size**, **Visibility**, **Action**, or **Border** to set other properties for the image report item.  
  
1.  Select **OK**.
  
## Related content 
 [Embed an image in a report &#40;Report Builder&#41;](../../reporting-services/report-design/embed-an-image-in-a-report-report-builder-and-ssrs.md)   
 [Add a background image &#40;Report Builder&#41;](../../reporting-services/report-design/add-a-background-image-report-builder-and-ssrs.md)   
 [Image properties dialog, general &#40;Report Builder&#41;](./images-report-builder-and-ssrs.md)  
  
