---
title: "Specify an Image as a Pointer on a Gauge (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 9d73b3c3-a068-4868-a2be-0cd261b6e92b
author: markingmyname
ms.author: maghan
manager: kfile
---
# Specify an Image as a Pointer on a Gauge (Report Builder and SSRS)
  The gauge contains three built-in styles that can be used to customize the appearance of the pointer. For a radial gauge, the built in styles are: Needle, Marker and Bar. For a linear gauge, the built-in styles are Marker, Bar, and Thermometer. If a unique pointer is required, the user can create and specify an image which can be used as a fully functional pointer.  
  
 When you are specifying an image for the pointer, your image must have the following specifications:  
  
-   The origin of the pointer, or center of rotation, must be at the top of the image.  
  
-   The end of the pointer must be pointing down.  
  
 Since the pointer is an irregular shape, you will need to specify a transparency color to hide the unnecessary portions of the image. Consider using a color that would not normally be shown on the gauge as the transparency color, since the colors specified will not appear on the gauge.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../includes/ssrbrddup-md.md)]  
  
### To specify an image as a pointer on the gauge  
  
1.  In Design view, click the pointer of the gauge.  
  
2.  (Optional) If no pointer exists on the gauge, right-click on the gauge and select **Add Pointer**. A pointer is added to the gauge.  
  
3.  Click the **Insert** tab on the ribbon and double-click the image icon. The **Image Properties** dialog box opens.  
  
4.  Add an image to your report. For more information, see [Embed an Image in a Report &#40;Report Builder and SSRS&#41;](report-design/embed-an-image-in-a-report-report-builder-and-ssrs.md).  
  
5.  Open the Properties pane.  
  
6.  On the design surface, click on the pointer. The properties for the pointer are displayed in the Properties pane.  
  
7.  Expand the PointerImage node.  
  
8.  In **Source**, select **Embedded** from the drop-down list.  
  
    > [!NOTE]  
    >  If your image is stored in the database or on the web, you can specify the appropriate option for this property. For more information, see [Image Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/image-properties-dialog-box-general-report-builder-and-ssrs.md).  
  
9. In **Value**, select the name of your image from the drop-down list.  
  
10. In **TransparentColor**, pick a color value that you want to remove from the image. This will create a seamless appearance for the pointer in the gauge.  
  
## See Also  
 [Formatting Pointers on a Gauge &#40;Report Builder and SSRS&#41;](report-design/formatting-pointers-on-a-gauge-report-builder-and-ssrs.md)   
 [Add a Gauge to a Report &#40;Report Builder and SSRS&#41;](report-design/add-a-gauge-to-a-report-report-builder-and-ssrs.md)   
 [Formatting Lines, Colors, and Images &#40;Report Builder and SSRS&#41;](report-design/images-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](report-design/gauges-report-builder-and-ssrs.md)  
  
  
