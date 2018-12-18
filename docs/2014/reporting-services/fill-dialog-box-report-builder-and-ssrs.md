---
title: "Fill Dialog Box (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.reportbody.fill.f1"
  - "10065"
  - "10501"
  - "sql12.rtp.rptdesigner.shared.filldv.f1"
  - "sql12.rtp.rptdesigner.rectangleproperties.fill.f1"
  - "10064"
  - "sql12.rtp.rptdesigner.textboxproperties.fill.f1"
  - "10124"
ms.assetid: 93a91d02-d558-4a0e-8d17-3fdf21e208d3
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Fill Dialog Box (Report Builder and SSRS)
  On the **Fill** tab, you can specify color options for the background of a single cell or multiple cells within a data region or a text box.  
  
## Options  
 **Fill Color**  
 Click the color button to select a fill color for the rectangle. Click the **Expression**_(fx)_ button to edit the expression, which can be a hexadecimal value for the RGB color or one of the predefined color names that are provided in the **Expression** dialog box. To see a list of predefined colors, in the **Item** pane, select **Web**. The color names listed in the **Title** pane can be typed into the expression text pane. Do not use an equal sign (=) or quotation marks ("") when typing the color name.  
  
 **Select the image source**  
 Indicate where the image is stored so that when the report is rendered, the report processor can display it.  
  
-   **External** Choose this option when you want the image to continue to exist as a file on a report server or Web server.  
  
-   **Embedded** Choose this option when you want to embed the image into the report.  
  
-   **Database** Choose this option when you want to include a database field name that represents the images that you want to include in your report.  
  
 **Use this image**  
 This option appears when you select the **Embedded** or **External** option.  
  
 If you are embedding the image, choose the picture that you want to add to the report from the drop-down list. Click **Import** to add the image to the drop-down list. If you added an image to the **Data** pane, you can select that image by choosing **Embedded** and then selecting the image from the drop-down list.  
  
 If you select the **External** option, type the URL of the image. For a report published to a report server configured for native mode, use a full or relative path (for example, http://*\<servername>*/images/image1.jpg). For a report published to a report server configured in SharePoint integrated mode, use a fully qualified URL (for example, http://*\<SharePointservername>/\<site>*/Documents/images/image1.jpg).  
  
 **Import**  
 Available when you select **Embedded**. Click to add an image to the **Use this image** drop-down list.  
  
 **Use this field**  
 Available when you select **Database**. Select the field name.  
  
 **Use this MIME type**  
 Choose the appropriate format of the pictures contained in the database (for example, .bmp, .jpeg, .gif, .png, or .x-png).  
  
## See Also  
 [Formatting Report Items &#40;Report Builder and SSRS&#41;](report-design/formatting-report-items-report-builder-and-ssrs.md)   
 [Formatting Text and Placeholders &#40;Report Builder and SSRS&#41;](report-design/formatting-text-and-placeholders-report-builder-and-ssrs.md)   
 [Images &#40;Report Builder and SSRS&#41;](report-design/images-report-builder-and-ssrs.md)  
  
  
