---
title: "Image Properties Dialog Box, General (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10051"
  - "sql12.rtp.rptdesigner.pictureproperties.general.f1"
ms.assetid: c2218b93-f7fe-46ef-995f-d7dadf9752ec
author: markingmyname
ms.author: maghan
manager: kfile
---
# Image Properties Dialog Box, General (Report Builder and SSRS)
  Select **General** on the **Image Properties** dialog box to add a picture, change the default name of the image, and add ToolTip text.  
  
## Options  
 **Name**  
 Type a name for the item. The name must be unique within the report. By default, a general name, such as Image1 or Image2, is assigned.  
  
 **Tooltip**  
 Type text or an expression that evaluates to a ToolTip. Click the Expression (*fx*) button to edit the expression. The **Tooltip** appears when the user pauses the pointer over the item in an HTML report.  
  
 **Select the image source**  
 Indicate where the image is stored so that when the report is rendered, the report processor will know where to get the image from.  
  
-   **External** Choose this option when you want the image to continue to exist as a file on a report server or Web server.  
  
-   **Embedded** Choose this option when you want to embed the image into the report.  
  
-   **Database** Choose this option when you want to include a database field name that represents the images that you want to include in your report.  
  
 **Use this image**  
 This option appears when you select the **Embedded** or **External** option.  
  
 If you are embedding the image, choose the image that you want to add to the report from the drop-down list. Click the **Import** button to add the image to the drop-down list.  
  
 If you select the **External** option, type the URL of the image. For a report published to a report server configured for native mode, use a full or relative path. For example, http://\<servername>/images/image1.jpg. For a report published to a report server configured in SharePoint integrated mode, use a fully qualified URL. For example, http://\<*SharePointservername*>/\<*site*>/Documents/images/image1.jpg.  
  
 **Import**  
 Click to add an image to the **Use this image** drop-down list.  
  
 **Use this field**  
 This option appears if you select the **Database** option. Select the field name.  
  
 **Use this MIME type**  
 Choose the appropriate format of the images contained in the database, for example, .bmp, .jpeg, .gif, .png, and .x-png.  
  
## See Also  
 [Expression Examples &#40;Report Builder and SSRS&#41;](report-design/expression-examples-report-builder-and-ssrs.md)   
 [Images &#40;Report Builder and SSRS&#41;](report-design/images-report-builder-and-ssrs.md)   
 [Report Builder Help for Dialog Boxes, Panes, and Wizards](../../2014/reporting-services/report-builder-help-for-dialog-boxes-panes-and-wizards.md)  
  
  
