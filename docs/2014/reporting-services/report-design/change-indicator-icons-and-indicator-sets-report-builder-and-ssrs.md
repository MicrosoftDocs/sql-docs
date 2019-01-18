---
title: "Change Indicator Icons and Indicator Sets (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 8a056adf-4473-473d-9b0c-314675af7bfd
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Change Indicator Icons and Indicator Sets (Report Builder and SSRS)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides preconfigured indicators sets, which might not always depict your data effectively and work well in the delivered report. This topic provides procedures to change the appearance of indicator icons and change the indicator sets to include different, more, or fewer indicator icons.  
  
 Options such as colors can be set by using expressions. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To change the color of an indicator icon  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click the down arrow in the **Color** column next to the icon that you want to change and click the color to use, **No Color**, or **More colors**.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the value of the **Color** option.  
  
     If you clicked **More Colors**, the **Select Color** dialog box opens, where you can choose from a wide array of colors. For more information about its options, see [Select Color Dialog Box &#40;Report Builder and SSRS&#41;](../select-color-dialog-box-report-builder-and-ssrs.md). Click **OK** to close the **Select Color** dialog box.  
  
4.  Click **OK**.  
  
### To change the icon  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click the down arrow next to the icon that you want to change and select a different icon.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the value of the **Icon** option.  
  
4.  Click **OK**.  
  
### To use a custom image as an indicator icon  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click the down arrow next to the icon that you wan to change and select **Image**.  
  
4.  In the **Select the image source** list, click **External**, **Embedded**, or **Database**.  
  
5.  Depending on the source of the image, do the one of the following:  
  
    -   To use an image that is stored externally to the report, click **Browse** and locate the image. The report will include a reference to the image.  
  
    -   To use an image that is embedded in the report, click **Import** and locate the image. The image will be added to the report definition and saved with it.  
  
    -   To use an image that is in a database, in the **Use this field** list. select the field from the list and then in the **Use this MIME type** list, select the MIME type of the image.  
  
6.  Click **OK**.  
  
### To add an icon to the indicator set  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click **Add**. An indicator is added, using the default icon and the **No Color** option.  
  
     Configure the indictor to use the icon and color you want. Procedures earlier in this topic describe the steps to do this.  
  
4.  Click **OK**.  
  
### To delete an icon to the indicator set  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Select the icon to delete, and click **Delete**.  
  
4.  Click **OK**.  
  
## See Also  
 [Indicators &#40;Report Builder and SSRS&#41;](indicators-report-builder-and-ssrs.md)  
  
  
