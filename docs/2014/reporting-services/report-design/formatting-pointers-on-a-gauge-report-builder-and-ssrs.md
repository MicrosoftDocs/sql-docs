---
title: "Formatting Pointers on a Gauge (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 2fdf670a-5237-48fe-813d-97657c5c77d2
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Formatting Pointers on a Gauge (Report Builder and SSRS)
  A gauge pointer indicates the current value of the gauge. By default, when a field is added, the values that are contained in the field are aggregated into one value that is shown by the pointer on the gauge. You can add multiple pointers to the gauge to point at multiple values on the same scale, or add multiple scales and a pointer for every scale you have added. After you add a field to a gauge, you must set the maximum and minimum values on the corresponding scale to give context to the pointer value. You also have the option of setting the minimum and maximum values on a range, which shows a critical area on the scale.  
  
 You can set appearance properties on the pointer by right-clicking on the pointer and selecting **Radial Pointer Properties** or **Linear Pointer Properties**. Each gauge pointer contains the same set of properties. There are also corresponding appearance properties unique to each gauge type:  
  
-   On a radial gauge, you can specify a needle pointer and a needle cap.  
  
-   On a linear gauge, you can specify a thermometer pointer, which is a variation of the bar pointer. The thermometer pointer lets you specify the shape of the bulb.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="HowPointer"></a> How the Pointer is Connected to Data  
 By default, when a gauge is added, it contains one pointer that has no associated field. This is known as an empty pointer. It will display zero until a field is added to the data pane. When you add a field to the data pane, the pointer is connected to that field. If you delete a field from the data pane, the pointer that is associated with that field is also deleted.  
  
 After data is added, when you right-click on the pointer, you will get **Clear Pointer** and **Delete Pointer** options. The **Clear Pointer Value** option will remove the field that is attached to the gauge, but the pointer will still appear on the gauge. The **Delete Pointer** option will remove the field from the gauge and delete the pointer from view. If you re-add a field to the gauge, the default pointer will reappear. When you set the pointer's **Hidden** property to `True`, the pointer is not hidden on the design surface, but it is hidden at run time.  
  
  
##  <a name="DisplayingMultiple"></a> Displaying Multiple Pointers on the Gauge  
 You can add multiple pointers to the gauge to point at multiple values on the same scale. This can be useful for showing a low and a high value at the same time. To specify more than one pointer on the gauge for the same scale, right-click anywhere inside the gauge and click **Add Pointer** on the shortcut menu. Alternatively, you can add a scale by right-clicking anywhere in the gauge and clicking **Add Scale**. Then you can add a new pointer, and it will automatically be associated with the last scale.  
  
 When pointers overlap, the drawing order of the pointers is determined by the order in which they are added to the gauge. You cannot reorder the drawing order of the pointers by changing the order of the fields in the data pane. To change the order of drawing for multiple pointers, open the Properties pane and click **Pointers (...)**. Then, change the order of the pointers in the Pointer collection.  
  
  
##  <a name="SettingGradients"></a> Setting Gradients on a Needle Cap  
 You can specify a needle cap that can be drawn on top or underneath the pointer on a radial gauge only. All needle cap styles are drawn by using built-in gradients that cannot be modified. The exception is the `RoundedDark` style, where you can specify a gradient color and gradient style.  
  
  
##  <a name="SettingSnappingInterval"></a> Setting a Snapping Interval  
 A snapping interval defines the multiple to which values are rounded. By default, the gauge will point to the exact value of the field you have specified in the data pane. However, you might want to round the exact value up or down so that the pointer will snap to a pre-set interval. For example, if the value on your gauge is 34.2 and you specify a snapping interval of 5, the gauge pointer will point to 35. If the value on your gauge is 31.2 and you specify a snapping interval of 5, the gauge pointer will point to 30. For more information, see [Set a Snapping Interval on a Gauge &#40;Report Builder and SSRS&#41;](../set-a-snapping-interval-on-a-gauge-report-builder-and-ssrs.md).  
  
  
##  <a name="SpecifyingImage"></a> Specifying an Image as a Pointer on a Radial Gauge  
 In addition to the built-in list of pointer styles, you can specify an image as a pointer. This is most effective when you use an image to replace an existing needle pointer style. The image is superimposed over the pointer, but all pointer functionality is applicable. Color and gradient options are not applicable when an image is used for the pointer.  
  
 If the pointer image is an irregular shape, you should define the color as transparent to hide the areas of your image that should not appear on the gauge. When you define a transparent color, the gauge transposes the image on top of your existing pointer and trims the image so that only the shape of the pointer appears. The gauge rescales the image to fit the size of your pointer. When you specify an image for a pointer, any subsequent pointer that is added on top of the gauge will be drawn underneath the image. For this reason, it is better not to specify an image for the pointer if there are multiple pointers on the gauge. For more information, see [Specify an Image as a Pointer on a Gauge &#40;Report Builder and SSRS&#41;](../specify-an-image-as-a-pointer-on-a-gauge-report-builder-and-ssrs.md).  
  
  
## See Also  
 [Formatting Scales on a Gauge &#40;Report Builder and SSRS&#41;](formatting-scales-on-a-gauge-report-builder-and-ssrs.md)   
 [Formatting Ranges on a Gauge &#40;Report Builder and SSRS&#41;](formatting-ranges-on-a-gauge-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](gauges-report-builder-and-ssrs.md)  
  
  
