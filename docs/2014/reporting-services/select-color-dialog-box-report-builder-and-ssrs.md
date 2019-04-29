---
title: "Select Color Dialog Box (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.selectcolor.f1"
  - "10090"
helpviewer_keywords: 
  - "Select Color dialog box"
ms.assetid: ac7089a3-5c7b-4f53-8348-180610e86da2
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Select Color Dialog Box (Report Builder and SSRS)
  Use the **Select Color** dialog box to specify color options for the background of a single cell or multiple cells within a data region or a text box, or for a chart.  
  
## Options  
 **Color selector**  
 Choose from three options that specify how you want to select colors:  
  
-   **Picker - Color circle** Choose a color using Hue/Saturation/Brightness (HSB) color values.  
  
-   **Picker - Color square** Choose a color using Red/Green/Blue (RGB) color values.  
  
-   **Palette - Standard colors** Choose a color from a predefined list of color values.  
  
 **Color circle**  
 Use for HSB colors because HSB values are mapped onto a cylindrical coordinate system. Hue is the actual color, saturation is purity of color, brightness is relative brightness or darkness.  
  
 When you pick a color, the center of the circle determines the color. Use the color slider to change the hue. The x and y coordinates represent saturation and brightness values respectively.  
  
 **Color square**  
 Use for RGB colors because RGB values are mapped to a Cartesian coordinate system. R is the value for Red, G is the value for Green, B is the value for Blue.  
  
 When you pick a color, the center of the square determines the color. Use the color slider to change the range of the chosen color. The x and y coordinates represent the other two colors. For example, if you pick green, the slider shows the range of green values, the x and y coordinates represent red and blue values respectively.  
  
 **Standard palette color**  
 Use for named colors from the [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] `KnownColor` enumeration.  
  
 **Color system**  
 Specify RGB or HSB colors. This choice changes the display to show RGB or HSB values, which are updated interactively when you use a color circle or color square for the **Color selector**.  
  
 The **Alpha** value displays for some properties when a color can include a transparency value. For example, Chart series fill. For properties that do not support transparency, this value is disabled.  
  
 **Red**  
 The decimal value for the red part of the RGB color. Use the spin box to change the value or type in a value between 0 and 255.  
  
 **Green**  
 The decimal value for the green part of the RGB color. Use the spin box to change the value or type in a value between 0 and 255.  
  
 **Blue**  
 The decimal value for the blue part of the RGB color. Use the spin box to change the value or type in a value between 0 and 255.  
  
 **Alpha**  
 The decimal value for the alpha or transparency part of the color. When this value is enabled, you can use the slider switch to adjust the degree of transparency you want.  
  
 **Hue**  
 The decimal value for the hue of the HSB color. Use the spin box to change the value or type in a value between 0 and 255.  
  
 **Saturation**  
 The decimal value for the saturation of the HSB color. Use the spin box to change the value or type in a value between 0 and 255.  
  
 **Brightness**  
 The decimal value for the brightness of the HSB color. Use the spin box to change the value or type in a value between 0 and 255.  
  
 **Color sample**  
 Shows the current color on the left half of the pane and interactively shows the new color you are choosing on the right half of the pane. If there is no default color, the left half of the pane is white. Most RDL properties have no default color.  
  
## See Also  
 [Formatting Report Items &#40;Report Builder and SSRS&#41;](report-design/formatting-report-items-report-builder-and-ssrs.md)   
 [Formatting Text and Placeholders &#40;Report Builder and SSRS&#41;](report-design/formatting-text-and-placeholders-report-builder-and-ssrs.md)  
  
  
