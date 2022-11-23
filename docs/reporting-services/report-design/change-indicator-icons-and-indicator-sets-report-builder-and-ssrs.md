---
title: "Change indicator icons and indicator sets in a paginated report | Microsoft Docs"
description: Learn how to change the indicator icons and sets in a paginated report to include different, more, or fewer indicator icons enabling you to depict data better in Report Builder.  
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 8a056adf-4473-473d-9b0c-314675af7bfd
author: maggiesMSFT
ms.author: maggies
---
# Change indicator icons and indicator sets in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The preconfigured indicators sets that provides for paginated reports might not always depict your data effectively and work well in the delivered report. This topic provides procedures to change the appearance of indicator icons and change the indicator sets to include different, more, or fewer indicator icons.  
  
 Options such as colors can be set by using expressions. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
## To change the color of an indicator icon  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click the down arrow in the **Color** column next to the icon that you want to change and click the color to use, **No Color**, or **More colors**.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the value of the **Color** option.  
  
     If you clicked **More Colors**, the **Select Color** dialog box opens, where you can choose from a wide array of colors. For more information about its options, see [Select Color Dialog Box &#40;Report Builder and SSRS&#41;](./formatting-lines-colors-and-images-report-builder-and-ssrs.md). Click **OK** to close the **Select Color** dialog box.  
  
4.  Click **OK**.  
  
## To change the icon  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click the down arrow next to the icon that you want to change and select a different icon.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the value of the **Icon** option.  
  
4.  Click **OK**.  
  
## To use a custom image as an indicator icon  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click the down arrow next to the icon that you wan to change and select **Image**.  
  
4.  In the **Select the image source** list, click **External**, **Embedded**, or **Database**.  
  
5.  Depending on the source of the image, do the one of the following:  
  
    -   To use an image that is stored externally to the report, click **Browse** and locate the image. The report will include a reference to the image.  
  
    -   To use an image that is embedded in the report, click **Import** and locate the image. The image will be added to the report definition and saved with it.  
  
    -   To use an image that is in a database, in the **Use this field** list. select the field from the list and then in the **Use this MIME type** list, select the MIME type of the image.  
  
6.  Click **OK**.  
  
## To add an icon to the indicator set  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Click **Add**. An indicator is added, using the default icon and the **No Color** option.  
  
     Configure the indictor to use the icon and color you want. Procedures earlier in this topic describe the steps to do this.  
  
4.  Click **OK**.  
  
## To delete an icon to the indicator set  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  Select the icon to delete, and click **Delete**.  
  
4.  Click **OK**.  
  
## See Also  
 [Indicators &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md)  
  
