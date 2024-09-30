---
title: "Add a background image to a paginated report"
description: Discover how to embed an image in the paginated report definition to add the image to various report items including text boxes, lists, and page header.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add a background image to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can add a background image to a paginated report item such as a rectangle, text box, list, matrix, table, and some parts of a chart. Or, you can add an image to a report section such as the page header, page footer, or report body. You can define a background image for any selected item on the report design surface that displays **BackgroundImage** in the **Properties** pane. Like other images, the background image can be a URL to an image on the report server, an image from a dataset field, or an image embedded in the report definition. To use an image embedded in the report, you must first add the image to the report definition before you can add the image to the design surface.  
   
## Embed an image in the report definition  
  
1.  In the Report Data pane, right-click the **Images** node, and then select **Add Image**.  
  
    > [!NOTE]  
    >  If the Report Data pane isn't visible, on the **View** tab, select **Report Data**.  
  
1.  Navigate to the image you want to embed in your report definition, and then select **OK**.  
  
## Add a background image  
  
1.  In report design view, select the report item to which you want to add a background image.  
  
1.  If the **Properties** pane isn't visible, on the **View** tab, select **Properties**.  
  
1.  In the **Properties** pane, expand **BackgroundImage**, and then do the following actions:  
  
    -   For an embedded image:  
  
         Set **Source** to **Embedded**.  
  
         Set **Value** to the name of an image that is embedded in the report.  
  
    -   For an external image:  
  
         Set **Source** to **External**.  
  
         Set **Value** to a valid path to an image. This image can be on a report server in native mode or SharePoint integrated mode. Or, it can be on any other Web site. For more information, see [Add an external image &#40;Report Builder&#41;](../../reporting-services/report-design/add-an-external-image-report-builder-and-ssrs.md).  
  
    -   For an image contained in a database field to which the report item is connected:  
  
         Set **Source** to **Database**.  
  
         Set **Value** to the name of a field in the report dataset. For more information, see [Add a data-bound image &#40;Report Builder&#41;](../../reporting-services/report-design/add-a-data-bound-image-report-builder-and-ssrs.md).  
  
         For **MIMEType**, or file format, select the appropriate MIME type for the image-for example, .bmp.  
  
        > [!NOTE]  
        >  **MIMEType** applies only if the **Source** property is set to **Database**. If the **Source** property is set to **External** or **Embedded**, the value of **MIMEType** is ignored.  
  
    -   For **BackgroundRepeat**, select an expression, **Default**, **Repeat**, **RepeatX**, or **RepeatY**, or **Clip**.  
  
         For background images in a chart, **BackgroundRepeat** can be set to **Default**, **Repeat**, **Fit**, and **Clip**, but not **RepeatX** or **RepeatY**.  
  
## Related content

- [Images &#40;Report Builder&#41;](../../reporting-services/report-design/images-report-builder-and-ssrs.md)
- [Image Properties dialog, General &#40;Report Builder&#41;](./images-report-builder-and-ssrs.md)
