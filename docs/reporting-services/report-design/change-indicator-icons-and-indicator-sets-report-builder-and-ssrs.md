---
title: "Change indicator icons and indicator sets in a paginated report"
description: Learn how to change the indicator icons and sets in a paginated report to include different, more, or fewer indicator icons to depict data better in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Change indicator icons and indicator sets in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The preconfigured indicators sets that provide for paginated reports might not always depict your data effectively and work well in the delivered report. This article provides procedures to change the appearance of indicator icons and change the indicator sets to include different, more, or fewer indicator icons.  
  
 Options such as colors can be set by using expressions. For more information, see [Expressions &#40;Report Builder&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
## Change the color of an indicator icon  
  
1.  Right-click the indicator you want to change and select **Indicator Properties**.  
  
1.  Select **Values and States** in the left pane.  
  
1.  Select the down arrow in the **Color** column next to the icon that you want to change and choose the color to use, **No Color**, or **More colors**.  
  
     Optionally, select the **Expression** (*fx*) button to edit an expression that sets the value of the **Color** option.  
  
     If you selected **More Colors**, the **Select Color** dialog opens, where you can choose from a wide array of colors. For more information about its options, see [Select Color dialog &#40;Report Builder&#41;](./formatting-lines-colors-and-images-report-builder-and-ssrs.md). Select **OK** to close the **Select Color** dialog.  
  
1.  Select **OK**.  
  
## Change the icon  
  
1.  Right-click the indicator you want to change and select **Indicator Properties**.  
  
1.  Select **Values and States** in the left pane.  
  
1.  Select the down arrow next to the icon that you want to change and select a different icon.  
  
     Optionally, select the **Expression** (*fx*) button to edit an expression that sets the value of the **Icon** option.  
  
1.  Select **OK**.  
  
## Use a custom image as an indicator icon  
  
1.  Right-click the indicator you want to change and select **Indicator Properties**.  
  
1.  Select **Values and States** in the left pane.  
  
1.  Select the down arrow next to the icon that you wan to change and choose **Image**.  
  
1.  In the **Select the image source** list, choose **External**, **Embedded**, or **Database**.  
  
1.  Depending on the source of the image, do the one of the following actions:  
  
    -   To use an image that is stored externally to the report, select **Browse** and locate the image. The report includes a reference to the image.  
  
    -   To use an image that is embedded in the report, select **Import** and locate the image. The image is added to the report definition and saved with it.  
  
    -   To use an image that is in a database, in the **Use this field** list. select the field from the list and then in the **Use this MIME type** list, select the MIME type of the image.  
  
1.  Select **OK**.  
  
## Add an icon to the indicator set  
  
1.  Right-click the indicator you want to change and select **Indicator Properties**.  
  
1.  Select **Values and States** in the left pane.  
  
1.  Select **Add**. An indicator is added, using the default icon and the **No Color** option.  
  
     Configure the indictor to use the icon and color you want. Procedures earlier in this article describe the steps to configure the indicator.  
  
1.  Select **OK**.  
  
## Delete an icon to the indicator set  
  
1.  Right-click the indicator you want to change and select **Indicator Properties**.  
  
1.  Select **Values and States** in the left pane.  
  
1.  Select the icon to delete, and choose **Delete**.  
  
1.  Select **OK**.  
  
## Related content 
 [Indicators &#40;Report Builder&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md)  
  
