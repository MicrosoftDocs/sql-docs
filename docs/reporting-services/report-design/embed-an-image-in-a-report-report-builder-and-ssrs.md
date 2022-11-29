---
title: "Embed an image in a paginated report | Microsoft Docs"
description: Make an image readily available to a paginated report. Use an image that's embedded in the report and listed in the Report Data pane of Report Builder.
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
f1_keywords: 
  - "sql13.rtp.rptdesigner.embeddedimages.f1"
author: maggiesMSFT
ms.author: maggies
ms.custom: ""
ms.date: 03/01/2017
---
# Embed an image in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

A paginated report can include an embedded image. Embedding an image ensures that the image is always available to a report, but can affect the size of the report definition, the file that defines the report. The images embedded in a report are listed in the Report Data pane.  
  
You might want to embed an image in the report definition before adding the image to the design surface. For more information, see [Add a Background Image &#40;Report Builder&#41;](../../reporting-services/report-design/add-a-background-image-report-builder-and-ssrs.md).  

## To embed an image in a report  
  
1. In report design view, on the **Insert** tab, click **Image**.  
  
2. On the design surface, click and then drag a box to the desired size of the image.  
  
3. In the **General** page of the **Image Properties** dialog box, type a name in the **Name** text box or accept the default.  
  
4. (Optional) In the **ToolTip** text box, type the text that you want to appear when the user hovers over the image in the rendered report.  
  
5. In **Select the image source**, select **Embedded**.  
  
6. Click the **Import** button next to the **Use this image** text box  
  
7. In **Files of type**, select the image file type, navigate to the file, and then click **Open**.  
  
8. In the **Image Properties** dialog box, click **OK**.  
  
     The image is displayed in the box you drew on the design surface, and the file is displayed under the Images folder in the Report Data pane.  
  
    > [!NOTE]  
    >  The MIME type (for example, bmp) is derived automatically when the image is imported. To change the MIME type, see the next procedure.  
  
## (optional) To change the MIME type of an imported image  
  
1. Open the report in Design view.  
  
2. Select the image on the design surface. The **Properties** pane displays the image properties.  
  
    > [!NOTE]  
    >  If the Properties pane is not visible, on the **View** tab, click **Properties**.  
  
3. Click in the text box next to the **MIMEType** property and select a new MIME type from the drop-down list.  
  
## See Also

- [Images &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/images-report-builder-and-ssrs.md)
- [Add a Data-Bound Image &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-data-bound-image-report-builder-and-ssrs.md)
- [Image Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](./images-report-builder-and-ssrs.md)