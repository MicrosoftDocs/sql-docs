---
title: "Set Text Box Orientation (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 64bd53f4-2f31-4732-8c2e-64c7b54b6972
author: markingmyname
ms.author: maghan
manager: kfile
---
# Set Text Box Orientation (Report Builder and SSRS)
  A text box can be oriented in different directions: horizontally, vertically (text reading from top to bottom), or rotated by 270 degrees (text reading from bottom to top). Because orientation is set on the text box not the text, the orientation applies to all the text in the text box. You cannot specify different orientations for parts of the text. Size the column width and the row height manually to accommodate the rotated text.  
  
 The WritingMode property, which you use to specify text orientation, is not available in the **Text Box Properties** dialog box. You need to open the Properties pane and set the property there. The available values for the WritingMode property are **Horizontal** (text reading left to right), **Vertical** (text reading top to bottom), **Rotate270** (text reading bottom to top). You must manually size the column width and the row height to accommodate the text.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To set text orientation  
  
1.  Create a new report or open an existing report.  
  
2.  If the Properties pane is not open, click the **View** tab and select the **Properties** check box.  
  
3.  Click the text box for which you want to change text orientation.  
  
4.  Locate the WritingMode property in the Properties pane and in the drop-down list select the text orientation to apply to the text box.  
  
    > [!NOTE]  
    >  When the properties in the Properties pane are organized into categories, WritingMode is in the **Localization** category.  
  
5.  In the list box, select **Horizontal**, **Vertical**, or **Rotate270**.  
  
## See Also  
 [Text Boxes &#40;Report Builder and SSRS&#41;](text-boxes-report-builder-and-ssrs.md)   
 [Tutorial: Format Text &#40;Report Builder&#41;](../tutorial-format-text-report-builder.md)  
  
  
